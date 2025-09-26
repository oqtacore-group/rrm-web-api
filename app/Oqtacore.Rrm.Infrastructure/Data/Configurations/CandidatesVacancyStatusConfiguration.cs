using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class CandidatesVacancyStatusConfiguration : IEntityTypeConfiguration<CandidatesVacancyStatus>
    {
        public void Configure(EntityTypeBuilder<CandidatesVacancyStatus> builder)
        {
            builder.ToTable("CandidatesVacancyStatus");
            builder.HasKey(u => u.Id);
        }
    }
}
