namespace dsknowledgetestsback.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
