using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class ContactDataConfiguration : IEntityTypeConfiguration<ContactData>
    {
        public void Configure(EntityTypeBuilder<ContactData> builder)
        {
            builder.ToTable(nameof(ContactData));
            builder.HasKey(x => x.Id);
        }
    }
}
