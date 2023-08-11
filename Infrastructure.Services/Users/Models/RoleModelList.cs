namespace Infrastructure.Services.Users.Models
{
    /// <summary>
    /// Role Model for list operations
    /// </summary>
    public class RoleModelList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }
        public bool Active { get; set; }
    }
}
