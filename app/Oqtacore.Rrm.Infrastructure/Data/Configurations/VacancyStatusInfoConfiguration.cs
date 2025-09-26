using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class CurrentVacancyStatusInfoConfiguration : IEntityTypeConfiguration<CurrentVacancyStatusInfo>
    {
        public void Configure(EntityTypeBuilder<CurrentVacancyStatusInfo> builder)
        {
            builder.ToTable("CurrentVacancyStatusListViewModel");
            builder.HasNoKey();
        }
    }
    public class AllVacancyStatusInfoConfiguration : IEntityTypeConfiguration<AllVacancyStatusInfo>
    {
        public void Configure(EntityTypeBuilder<AllVacancyStatusInfo> builder)
        {
            builder.ToTable("AllVacancyStatusListingViewModel");
            builder.HasNoKey();
        }
    }
    public class VacancyStatusInfoConfiguration : IEntityTypeConfiguration<VacancyStatusInfo>
    {
        public void Configure(EntityTypeBuilder<VacancyStatusInfo> builder)
        {
            builder.ToTable("VacancyStatusListingViewModel");
            builder.HasNoKey();
        }
    }
}
