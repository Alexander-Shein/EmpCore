namespace BlogPostManagementService.Contracts
{
    public abstract class EventBase
    {
        public DateTime CreatedAt { get; }

        protected EventBase(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
    }
}