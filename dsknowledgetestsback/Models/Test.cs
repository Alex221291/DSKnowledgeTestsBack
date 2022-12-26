namespace dsknowledgetestsback.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Discription { get; set; }
        public DateTime DataCreated { get; set; }
        public bool IsDeleted { get; set; }
        public int CountQuestion { get; set; }

        public Guid TestCategoryId { get; set; }
        public TestCategory TestCategory { get; set; }
        public Guid UserCreatedId { get; set; }
        public User User { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<CompletedTest> CompletedTests { get; set; }

    }
}
