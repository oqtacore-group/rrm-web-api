using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class RecruiterStatisticInfoConfiguration : IEntityTypeConfiguration<RecruiterStatisticInfo>
    {
        public void Configure(EntityTypeBuilder<RecruiterStatisticInfo> builder)
        {
            builder.ToTable("RecruiterStatisticViewModel");
            builder.HasKey(u => u.Id);
        }
    }
}