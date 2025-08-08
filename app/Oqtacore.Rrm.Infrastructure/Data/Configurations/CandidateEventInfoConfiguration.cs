using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class CandidateEventInfoConfiguration : IEntityTypeConfiguration<CandidateEventInfo>
    {
        public void Configure(EntityTypeBuilder<CandidateEventInfo> builder)
        {
            builder.ToTable("AllCandidateEventViewModel");
            builder.HasKey(u => u.Id);
        }
    }
}
