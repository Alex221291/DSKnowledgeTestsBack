namespace dsknowledgetestsback.Models
{
    public class Education
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}