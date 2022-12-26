using dsknowledgetestsback.Models;

namespace dsknowledgetestsback.ViewModels.FeedbackViewModel
{
    public class CreateFeedbackViewModel
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string FeedbackCategoryName { get; set; }
    }
}
