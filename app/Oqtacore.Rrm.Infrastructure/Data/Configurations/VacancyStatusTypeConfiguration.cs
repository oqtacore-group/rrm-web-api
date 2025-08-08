using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class VacancyStatusTypeConfiguration : IEntityTypeConfiguration<VacancyStatusType>
    {
        public void Configure(EntityTypeBuilder<VacancyStatusType> builder)
        {
            builder.ToTable("VacancyStatusType");
            builder.HasKey(u => u.Id);
        }
    }
}
