namespace Oqtacore.Rrm.Domain.Models
{
    public class CandidateFileDataModel
    {
        public int id { get; set; }
        public int candidateId { get; set; }
        public string fileName { get; set; }
        public string fileUrl { get; set; }
        public System.DateTime DateAdded { get; set; }
    }
}
