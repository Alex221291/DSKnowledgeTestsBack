using System.ComponentModel.DataAnnotations.Schema;

namespace dsknowledgetestsback.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public int Mark { get; set; }
        [NotMapped]
        public ICollection<string> ListAnswers
        {
            get
            {
                return this.ListAnswersToString.Split(',').ToList();
            }
            set
            {
                this.ListAnswersToString = string.Join(",", value);
            }
        }

        public string ListAnswersToString { get; set; }
        [NotMapped]
        public ICollection<string> ListCurrentAnswers
        {
            get
            {
                return this.ListCurrentAnswersToString.Split(',').ToList();
            }
            set
            {
                this.ListCurrentAnswersToString = string.Join(",", value);
            }
        }
        public string ListCurrentAnswersToString { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public Guid QuestionTypeId { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
