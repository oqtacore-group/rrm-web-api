using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oqtacore.Rrm.Domain.Entity;

namespace Oqtacore.Rrm.Infrastructure.Data.Configurations
{
    public class ClientContactConfiguration : IEntityTypeConfiguration<ClientContact>
    {
        public void Configure(EntityTypeBuilder<ClientContact> builder)
        {
            builder.ToTable("ClientContact");
            builder.HasKey(u => u.Id);
        }
    }
}