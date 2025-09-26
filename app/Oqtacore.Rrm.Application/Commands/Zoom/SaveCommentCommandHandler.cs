using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Commands.Zoom
{
    public class SaveCommentCommandHandler : RequestHandler<SaveCommentCommand, SaveCommentResult>
    {
        private readonly ApplicationContext _dataContext;

        public SaveCommentCommandHandler(IRepository repository, IHttpService httpService, ApplicationContext dataContext): base(repository, httpService)
        {
            _dataContext = dataContext;
        }

        protected override async Task<SaveCommentResult> Execute(SaveCommentCommand request, CancellationToken cancellationToken)
        {
            var result = new SaveCommentResult();

            var candidateExists = await _dataContext.Candidate.AnyAsync(c => c.id == request.UserId, cancellationToken);

            var vacancyId = await _dataContext.CandidatesVacancyComment
            .Where(c => c.CandidateId == request.UserId)
            .Select(c => c.VacancyId)
            .FirstOrDefaultAsync(cancellationToken);

            var vacancyExists = await _dataContext.CandidatesVacancyComment.AnyAsync(v => v.id == request.UserId, cancellationToken);

            if (!candidateExists || vacancyId == 0)
            {
                result.Success = false;
                result.Message = "Candidate or vacancy not found";
                return result;
            }

            var comment = new CandidatesVacancyComment
            {
                CandidateId = request.UserId,
                VacancyId = vacancyId,
                Note = request.Comment,
                DateAdded = DateTime.UtcNow
            };

            _dataContext.CandidatesVacancyComment.Add(comment);

            try
            {
                await _dataContext.SaveChangesAsync(cancellationToken);
                result.Success = true;
                result.Message = "Comment saved successfully";
                result.Result = "OK";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Save failed";
                result.Result = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
    }
}
