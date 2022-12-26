namespace dsknowledgetestsback.Models
{
    public class QuestionType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
