namespace BlogPostManagementService.Contracts
{
    public class BlogPostPublishedEvent : EventBase
    {
        public Guid BlogPostId { get; }

        public Guid AuthorId { get; }
        public DateTime PublishDateTime { get; }
        public string FeedbackEmailAddress { get; }

        public BlogPostPublishedEvent(
            Guid blogPostId,
            Guid authorId,
            DateTime publishDateTime,
            string feedbackEmailAddress,
            DateTime createdAt) : base(createdAt)
        {
            BlogPostId = blogPostId;
            AuthorId = authorId;
            PublishDateTime = publishDateTime;
            FeedbackEmailAddress = feedbackEmailAddress;
        }
    }
}