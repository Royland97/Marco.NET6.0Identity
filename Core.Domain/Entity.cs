using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; protected set; }
    }
}
