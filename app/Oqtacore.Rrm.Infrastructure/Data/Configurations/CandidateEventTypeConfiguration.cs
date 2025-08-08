using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class CandidateEventTypeConfiguration : IEntityTypeConfiguration<CandidateEventType>
    {
        public void Configure(EntityTypeBuilder<CandidateEventType> builder)
        {
            builder.ToTable("CandidateEventType");
            builder.HasKey(u => u.Id);
        }
    }
}
