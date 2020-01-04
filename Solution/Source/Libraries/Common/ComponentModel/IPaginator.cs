namespace System.ComponentModel
{
    /// <summary>
    ///     Describes the contract of a paginator object.
    /// </summary>
    public interface IPaginator
    {
        /// <summary>
        ///     Gets or sets the current page.
        /// </summary>
        int Page { get; set; }

        /// <summary>
        ///     Gets or sets the count of items per page.
        /// </summary>
        int ItemsPerPage { get; set; }

        /// <summary>
        ///     Gets the total pages.
        /// </summary>
        int TotalPages { get; }
    }
}