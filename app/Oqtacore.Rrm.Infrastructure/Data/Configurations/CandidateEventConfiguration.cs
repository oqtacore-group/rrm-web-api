using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class CandidateEventConfiguration : IEntityTypeConfiguration<CandidateEvent>
    {
        public void Configure(EntityTypeBuilder<CandidateEvent> builder)
        {
            builder.ToTable("CandidateEvent");
            builder.HasKey(u => u.Id);
        }
    }
}
