namespace CompanyName.Atlas.Contracts.Presentation.Services
{
    /// <summary>
    /// This is the contract for the services allowing to interact with the status bar of the Atlas main UI.
    /// </summary>
    public interface IStatusBarServices
    {
        /// <summary>
        /// Gets the current status text.
        /// </summary>
        string StatusText { get; }


        /// <summary>
        /// Places the ready status in the status bar.
        /// </summary>
        void SignalReady();

        string GetSomeContractText(int choice);

         void SignalWaitOperation();

        /// <summary>
        /// Places the loading status in the status bar.
        /// </summary>
        void SignalLoading();

        void ForceSignalLoading();
        void ForceSignalLoading2();

        /// <summary>
        /// Displays the specified text in the status bar.
        /// </summary>
        /// <param name="text">The text to display.</param>
        void SignalText(string text);
        void SignalText2(string text);

        bool isTextReallyShowed { get; set; }
    }
}
