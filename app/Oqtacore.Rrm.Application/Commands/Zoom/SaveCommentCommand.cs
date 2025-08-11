using MediatR;

namespace Oqtacore.Rrm.Application.Commands.Zoom
{
    public class SaveCommentCommand : ICommand<SaveCommentResult>
    {
        public int UserId { get; set; }
        public int VacancyId { get; set; }
        public string Comment { get; set; }
    }

    public class SaveCommentResult : Result
    {
        public string Result { get; set; }
    }

}
