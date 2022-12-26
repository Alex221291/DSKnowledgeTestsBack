using dsknowledgetestsback.Models;

namespace dsknowledgetestsback.ViewModels.TestViewModel
{
    public class TestViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Discription { get; set; }
        public string DataCreated { get; set; }
        public bool IsDeleted { get; set; }
        public int CountQuestion { get; set; }
        public string TestCategoryName { get; set; }
        public Guid UserCreatedId { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
