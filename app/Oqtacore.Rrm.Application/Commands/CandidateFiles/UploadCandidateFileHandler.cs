using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Application.Commands.CandidateFiles
{
    public class UploadCandidateFileHandler : RequestHandler<UploadCandidateFileCommand, UploadCandidateFileResult>
    {
        public UploadCandidateFileHandler(IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
        }
        protected override async Task<UploadCandidateFileResult> Execute(UploadCandidateFileCommand request, CancellationToken cancellationToken)
        {
            var result = new UploadCandidateFileResult();


            return result;
        }
    }
}
