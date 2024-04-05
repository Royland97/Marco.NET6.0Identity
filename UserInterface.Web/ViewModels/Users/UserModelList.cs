namespace UserInterface.Web.ViewModels.Users
{
    /// <summary>
    /// Model used for list operations
    /// </summary>
    public class UserModelList
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
