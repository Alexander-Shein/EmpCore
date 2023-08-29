using BlogPostManagementService.Domain.BlogPosts.ValueObjects;
using EmpCore.Domain;

namespace BlogPostManagementService.Domain.BlogPosts;

public class Author : Entity<string>
{
    public EmailAddress FeedbackEmailAddress { get; private set; }

    public static Result<Author> Create(string id, EmailAddress feedbackEmailAddress)
    {
        if (String.IsNullOrWhiteSpace(id)) throw new ArgumentException("Empty author id.", nameof(id));

        var author = new Author
        {
            Id = id,
            FeedbackEmailAddress = feedbackEmailAddress ?? throw new ArgumentNullException(nameof(feedbackEmailAddress))
        };

        return Result.Success(author);
    }
}