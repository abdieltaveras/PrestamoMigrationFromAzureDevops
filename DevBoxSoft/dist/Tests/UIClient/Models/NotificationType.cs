namespace UIClient.Models
{
    /// <summary>
    /// Types for notification messages.  Can be combined
    /// Ex.  Info|MustReload
    /// </summary>
    [System.Flags]
    public enum NotificationType
    {
        /// <summary>
        /// Indicates an inforamation message
        /// </summary>
        Info,
        /// <summary>
        /// Indicates an error message
        /// </summary>
        Error,
        /// <summary>
        /// Indicates that the user should reload the target component
        /// </summary>
        MustReload,
        /// <summary>
        /// Indicates that the user should logout and log back in
        /// </summary>
        MustRestart,
        /// <summary>
        /// Indicate that the system should not wait for the user to take action
        /// </summary>
        Force
    }
}