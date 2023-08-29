using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CommentManagementService.Domain.Comments;

namespace CommentManagementService.Persistence.Comments.Mappers;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment));

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.PublishedBlogPost)
            .WithMany()
            .HasForeignKey("PublishedBlogPostId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Commentor)
            .WithMany()
            .HasForeignKey("CommentorId");

        builder
            .HasOne(x => x.ParentComment)
            .WithMany()
            .HasForeignKey("ParentCommentId");

        builder.OwnsOne(p => p.Message, c =>
        {
            c.Property(x => x.Value).HasColumnName("Message").HasColumnType("NVARCHAR(1024)").IsRequired();
        });

        builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").HasColumnType("DATETIME2").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt").HasColumnType("DATETIME2").IsRequired();

        builder.Ignore(p => p.DomainEvents);
    }
}
