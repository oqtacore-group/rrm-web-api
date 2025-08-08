using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Domain.Models
{
    public class VacancyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int WorkplaceNumber { get; set; }
        public string? Status { get; set; }
        public int? SalaryLowerEnd { get; set; }
        public int? SalaryHighEnd { get; set; }
        public string? SalaryCurrency { get; set; }
        public decimal? Experience { get; set; }
        public string? Location { get; set; }
        public string? LocalTime { get; set; }
        public bool? RelocationHelp { get; set; }
        public bool? RemoteWorkPlace { get; set; }
        public int CreatedBy { get; set; }
        public bool Opened { get; set; }
        public string? Responsibility { get; set; }
        public string? Skills { get; set; }
        public string? PersonalQuality { get; set; }
        public string? Languages { get; set; }
        public string? Notes { get; set; }
    }
}
