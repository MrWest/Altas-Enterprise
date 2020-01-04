namespace System.Windows.Input
{
    /// <summary>
    /// Defines a command that is always disabled.
    /// </summary>
    public sealed class DisabledCommand : ICommand
    {
        /// <summary>
        /// Not used.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Not used.</param>
        /// <returns>Returns false.</returns>
        public bool CanExecute(object parameter)
        {
            return false;
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="parameter">Not used.</param>
        public void Execute(object parameter)
        {
        }
    }
}
