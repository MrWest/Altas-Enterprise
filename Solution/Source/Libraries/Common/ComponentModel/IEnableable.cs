namespace System.ComponentModel
{
    /// <summary>
    /// Describes the interface of an object that can be disabled and enabled.
    /// </summary>
    public interface IEnableable
    {
        /// <summary>
        /// Gets or sets wether this object is enabled or disabled.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
