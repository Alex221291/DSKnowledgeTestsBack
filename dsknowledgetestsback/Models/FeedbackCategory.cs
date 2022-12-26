namespace dsknowledgetestsback.Models
{
    public class FeedbackCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
