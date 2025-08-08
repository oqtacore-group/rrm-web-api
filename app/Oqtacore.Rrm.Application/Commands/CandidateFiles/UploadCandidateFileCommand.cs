using System;

namespace Oqtacore.Rrm.Application.Commands.CandidateFiles
{
    public class UploadCandidateFileCommand : ICommand<UploadCandidateFileResult>
    {
        public int CandidateId { get; set; }
        public string? FileName { get; set; }
        public string? FileUrl { get; set; }
    }
    public class  UploadCandidateFileResult : Result
    {
    }
}