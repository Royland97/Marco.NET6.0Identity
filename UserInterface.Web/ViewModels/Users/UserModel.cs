namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string Password { get; set; }
        public List<int> RoleIds { get; set; }
        public bool Active { get; set; }
    }
}
