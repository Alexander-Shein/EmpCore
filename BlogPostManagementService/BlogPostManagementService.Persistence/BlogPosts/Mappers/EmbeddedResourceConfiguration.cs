using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPostManagementService.Persistence.BlogPosts.Mappers
{
    public class EmbeddedResourceConfiguration : IEntityTypeConfiguration<EmbeddedResource>
    {
        public void Configure(EntityTypeBuilder<EmbeddedResource> builder)
        {
            builder.HasNoKey();
            builder.ToTable(nameof(EmbeddedResource));

            builder.Property(p => p.Url).HasColumnName("Url").HasColumnType("NVARCHAR(2048)").IsRequired();
            builder.Property(p => p.Caption).HasColumnName("Caption").HasColumnType("NVARCHAR(MAX)").IsRequired();
        }
    }
}
