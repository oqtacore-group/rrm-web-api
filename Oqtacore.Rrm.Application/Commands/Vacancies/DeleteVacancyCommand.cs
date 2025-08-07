using System;

namespace Oqtacore.Rrm.Application.Commands.Vacancies
{
    public class DeleteVacancyCommand : ICommand<DeleteVacancyResult>
    {
        public int Id { get; set; }
    }
    public class DeleteVacancyResult : Result
    {
    }
}