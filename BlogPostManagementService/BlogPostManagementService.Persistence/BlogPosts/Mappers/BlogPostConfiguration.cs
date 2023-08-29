using BlogPostManagementService.Domain.BlogPosts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPostManagementService.Persistence.BlogPosts.Mappers
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable(nameof(BlogPost));

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedNever();

            builder.OwnsOne(p => p.Author, a =>
            {
                a.Property(u => u.Id).HasColumnName("AuthorId").HasColumnType("VARCHAR(128)").IsRequired();

                a.OwnsOne(x => x.FeedbackEmailAddress, c =>
                {
                    c.Property(f => f.Value).HasColumnName("FeedbackEmailAddress").HasColumnType("NVARCHAR(256)").IsRequired();
                });
            });

            builder.OwnsOne(p => p.Title, c =>
            {
                c.Property(x => x.Value).HasColumnName("Title").HasColumnType("NVARCHAR(1024)").IsRequired();
            });

            builder.OwnsOne(p => p.Content, c =>
            {
                c.Property(x => x.Text).HasColumnName("Content").HasColumnType("NVARCHAR(MAX)").IsRequired();
                c.Ignore(x => x.EmbeddedResources);
            });

            builder.OwnsOne(p => p.PublishStatus, c =>
            {
                c.Property(x => x.Value).HasColumnName("PublishStatus").HasColumnType("NVARCHAR(24)").IsRequired();
            });

            builder.Property(p => p.PublishDateTime).HasColumnName("PublishDateTime").HasColumnType("DATETIME2");
            builder.Property(p => p.IsDeleted).HasColumnName("IsDeleted").HasColumnType("BIT").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIME2").IsRequired();
            builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIME2").IsRequired();

            builder.Ignore(p => p.DomainEvents);
        }
    }
}
