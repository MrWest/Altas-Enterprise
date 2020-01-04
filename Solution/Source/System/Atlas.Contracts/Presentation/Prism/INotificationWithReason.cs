namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// Defines the contract to be implemented by those notifications having a clear reason to be displayed.
    /// </summary>
    public interface INotificationWithReason
    {
        /// <summary>
        /// Gets or sets the intention of the current notification.
        /// </summary>
        NotificationReason Reason { get; set; }
    }
}