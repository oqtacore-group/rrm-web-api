using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.ToTable("Vacancy");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(c => c.IsDeleted == null || c.IsDeleted == false);
        }
    }
}
