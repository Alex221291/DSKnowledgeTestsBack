namespace dsknowledgetestsback.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public DateTime CreateData { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid FeedbackCategoryId { get; set; }
        public FeedbackCategory FeedbackCategory { get; set; }
    }
}
