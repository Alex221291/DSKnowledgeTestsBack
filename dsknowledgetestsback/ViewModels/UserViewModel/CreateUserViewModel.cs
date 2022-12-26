namespace dsknowledgetestsback.ViewModels.UserViewModel
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public Guid EducationId { get; set; }
        public bool IsActivated { get; set; }
    }
}
