using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CommentManagementService.Domain.BlogPosts;
using CommentManagementService.Domain.Comments;

namespace CommentManagementService.Persistence.BlogPosts.Mappers;

public class PublishedBlogPostConfiguration : IEntityTypeConfiguration<PublishedBlogPost>
{
    public void Configure(EntityTypeBuilder<PublishedBlogPost> builder)
    {
        builder.ToTable(nameof(PublishedBlogPost));

        builder.HasKey(x => x.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();

        builder.Ignore(p => p.DomainEvents);
    }
}
