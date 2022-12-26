namespace dsknowledgetestsback.Models
{
    public class TestCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}
