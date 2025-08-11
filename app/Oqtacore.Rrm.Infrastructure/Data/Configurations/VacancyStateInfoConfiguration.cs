using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    internal class VacancyStateInfoConfiguration : IEntityTypeConfiguration<VacancyStateInfo>
    {
        public void Configure(EntityTypeBuilder<VacancyStateInfo> builder)
        {
            builder.ToTable("VacancyStateListViewModel");
            builder.HasNoKey();
        }
    }
}
