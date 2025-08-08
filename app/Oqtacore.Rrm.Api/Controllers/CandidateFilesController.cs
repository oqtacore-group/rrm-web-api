using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Oqtacore.Rrm.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oqtacore.Rrm.Api.Helpers;
using Oqtacore.Rrm.Infrastructure.Data;
using Oqtacore.Rrm.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Oqtacore.Rrm.Api.Controllers
{
    public class UploadCandidateFileRequest
    {
        public int candidateid { get; set; }
        public IFormFile file { get; set; }
        public string? _file_name { get; set; }
    }

    [Route("api/[controller]")]
    public class CandidateFilesController : ApiControllerBase
    {
        private readonly ApplicationContext _dataContext;
        private readonly IConfiguration _configuration;
        private readonly IAmazonS3 _s3Client;
        private readonly string bucketName;
        public CandidateFilesController(ApplicationContext dataContext, IMediator mediator, IConfiguration configuration, IAmazonS3 s3Client, IWebHostEnvironment environment) : base(mediator)
        {
            _dataContext = dataContext;
            _configuration = configuration;
            _s3Client = s3Client;

            string env = EnvironmentManager.GetEnvironmentName(environment);
            bucketName = $"{env}-rrm-candidate-files";
        }

        [HttpPost("upload")]
        public async Task<ActionResult> UploadFiles([FromForm] UploadCandidateFileRequest request)
        {
            if (request.file == null || request.file.Length == 0)
            {
                return BadRequest(new { success = false, message = "No file uploaded." });
            }

            string fileName = Path.GetFileName(request.file.FileName).Replace(" ", "");
            string newFileName = Guid.NewGuid().ToString() + "_" + fileName;

            using (var newMemoryStream = new MemoryStream())
            {
                request.file.CopyTo(newMemoryStream);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = newMemoryStream,
                    Key = newFileName,
                    BucketName = bucketName,
                };

                var fileTransferUtility = new TransferUtility(_s3Client);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }

            string resultUrl = $"{_configuration["DomainUrl"]}/api/CandidateFiles/Download/{newFileName}";

            var new_file = new CandidateFile()
            {
                candidateId = request.candidateid,
                DateAdded = DateTime.UtcNow,
                fileName = request._file_name,
                fileUrl = newFileName
            };

            _dataContext.CandidateFile.Add(new_file);
            await _dataContext.SaveChangesAsync();

            var model = new
            {
                result = true,
                id = new_file.id,
                stringData = resultUrl
            };

            return Ok(model);
        }

        [HttpGet("Download/{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            try
            {
                var candidateFile = await _dataContext.CandidateFile.FirstOrDefaultAsync(x => x.fileUrl == fileName);
                if(candidateFile == null)
                    return BadRequest(new { success = false, message = "File not found." });

                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = candidateFile.fileUrl
                };

                using (GetObjectResponse response = await _s3Client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                {
                    var memoryStream = new MemoryStream();
                    await responseStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    return File(memoryStream, response.Headers["Content-Type"], candidateFile.fileName);
                }
            }
            catch (AmazonS3Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }

        [HttpDelete("Delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            try
            {
                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                await _s3Client.DeleteObjectAsync(deleteObjectRequest);

                // Optionally, remove the file record from the database
                var fileRecord = _dataContext.CandidateFile.FirstOrDefault(f => f.fileUrl.Contains(fileName));
                if (fileRecord != null)
                {
                    _dataContext.CandidateFile.Remove(fileRecord);
                    await _dataContext.SaveChangesAsync();
                }

                return Ok(new { success = true, message = "File deleted successfully." });
            }
            catch (AmazonS3Exception e)
            {
                return BadRequest(new { success = false, message = e.Message });
            }
        }
    }
}
