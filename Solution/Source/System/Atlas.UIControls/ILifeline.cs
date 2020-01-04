using System;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    ///     This represents the interface of a lifetime.
    /// </summary>
    public interface ILifeline
    {
        /// <summary>
        ///     Gets  and sets the  start date of the currnet lifeline.
        /// </summary>
        DateTime Start { get; set; }

        /// <summary>
        ///     Gets and sets the end date of the currnet lifeline.
        /// </summary>
        DateTime End { get; set; }

        int Percent { get; }
        int StartPercent { get; }

        void TellChange();

    }
}