namespace dsknowledgetestsback.ViewModels.UserViewModel
{
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string EducationName { get; set; }
        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }
    }
}
