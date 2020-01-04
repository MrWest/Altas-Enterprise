using System.Linq;
using System.Windows.Input;

namespace System.Windows
{
    /// <summary>
    /// Provides some extensions for the <see cref="UIElement"/> objects.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Gets the command binding with the given command.
        /// </summary>
        /// <param name="element">
        /// The <see cref="UIElement"/> to search for the command binding with the specified command at
        /// <paramref name="command"/>.
        /// </param>
        /// <param name="command">The command which command binding will be searched.</param>
        /// <returns>
        /// A <see cref="CommandBinding"/> containig the given command at <paramref name="command"/> parameter.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// There are more then one or none at all command bindings with the given command.
        /// </exception>
        public static CommandBinding FindCommandBinding(this UIElement element, ICommand command)
        {
            return element.CommandBindings.OfType<CommandBinding>().Single(x => x.Command == command);
        }
    }
}
