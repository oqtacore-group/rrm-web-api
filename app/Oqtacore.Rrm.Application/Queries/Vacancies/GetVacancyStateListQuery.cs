using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oqtacore.Rrm.Application.Queries.Vacancies
{
    public class GetVacancyStateListQuery : ListQuery<GetVacancyStateListQueryResult>
    {
        public int ClientId { get; set; }
    }
    public class GetVacancyStateListQueryResult: ListQueryResult<GetVacancyStateListQueryResultItem>
    {
    }
    public class GetVacancyStateListQueryResultItem
    {
        public int VacancyId { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public int? SuccessCount { get; set; }
        public int? WorkplaceNumber { get; set; }
        public int? CandidateCount { get; set; }
        public int? ClientId { get; set; }
        public int? CreatedBy { get; set; }
    }
}
