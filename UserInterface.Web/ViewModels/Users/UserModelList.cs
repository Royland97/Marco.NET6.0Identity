namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// User Model used for list operations
    /// </summary>
    public class UserModelList
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string UserName { get; set; }
        public Guid UserGuid { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public bool Active { get; set; }
    }
}
