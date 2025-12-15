namespace Core.Tools.Exceptions
{
    /// <summary>
    /// Exception to throw when an entity is not found in the backing store.
    /// </summary>
    public class EntityNotFoundException: Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException" /> class.
        /// </summary>
        /// 
        /// <param name="message">The message that describes the error.</param>
        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        #endregion
    }
}
