using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidate");
            builder.HasKey(u => u.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(c => c.IsDeleted == null || c.IsDeleted == false);
        }
    }
}
