using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();
            builder.HasQueryFilter(c => c.IsDeleted == null || c.IsDeleted == false);
        }
    }
}