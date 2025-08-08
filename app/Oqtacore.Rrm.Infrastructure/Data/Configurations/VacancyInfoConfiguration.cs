using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class VacancyInfoConfiguration : IEntityTypeConfiguration<VacancyInfo>
    {
        public void Configure(EntityTypeBuilder<VacancyInfo> builder)
        {
            builder.ToTable("vwVacancy");
            builder.HasKey(u => u.Id);
        }
    }
}
