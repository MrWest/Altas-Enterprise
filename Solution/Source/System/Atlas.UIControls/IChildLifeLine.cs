using System;

namespace CompanyName.Atlas.UIControls
{
    // TODO: Comment
    public interface IChildLifeLine
    {
        /// <summary>
        ///     Gets the start date of the currnet lifeline.
        /// </summary>
        DateTime Start { get; }

        /// <summary>
        ///     Gets the end date of the currnet lifeline.
        /// </summary>
        DateTime End { get; }
    }
}