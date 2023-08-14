namespace Infrastructure.Services.Users.Models
{
    /// <summary>
    /// Role Model
    /// </summary>
    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSystemRole { get; set; }
        public List<int> ResourcesIds { get; set; }
        public bool Active { get; set; }
    }
}
