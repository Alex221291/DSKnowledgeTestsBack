using Microsoft.EntityFrameworkCore;

namespace dsknowledgetestsback.Models
{
    [Index("Email", IsUnique = true)]
    [Index("Login", IsUnique = true)]
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DataCreated { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid EducationId { get; set; }
        public Education Education { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Test> Test { get; set; }
        public CompletedTest CompletedTest { get; set; }    
    }
}