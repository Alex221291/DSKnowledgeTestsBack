using System.ComponentModel.DataAnnotations.Schema;

namespace dsknowledgetestsback.Models
{
    public class CompletedTest
    {
        public Guid Id { get; set; }
        public DateTime DataCompleted { get; set; }
        public int CountTrue { get; set; }
        public int CountFalse { get; set; }

        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
