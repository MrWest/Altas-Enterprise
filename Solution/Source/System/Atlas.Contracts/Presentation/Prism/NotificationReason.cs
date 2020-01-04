namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// Enumerates the different reasons by which a notification is displayed to the user.
    /// </summary>
    public enum NotificationReason
    {
        /// <summary>
        /// Represents a confirmation.
        /// </summary>
        Question,

        /// <summary>
        /// Represents a notification of a normal event.
        /// </summary>
        Info,

        /// <summary>
        /// Represents a notification of a critical event.
        /// </summary>
        Warning,

        /// <summary>
        /// Represents a notification of an erroneous event.
        /// </summary>
        Error
    }
}
