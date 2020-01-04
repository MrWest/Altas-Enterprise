using System;
using System.Windows.Data;
using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Presentation.Services
{
    /// <summary>
    /// This interface describes the contract of the object allowing to interact with the system's main menu.
    /// </summary>
    public interface IMainMenuServices
    {
        /// <summary>
        /// Adds a menu item to the main menu given the new menu item identifier and text.
        /// </summary>
        /// <param name="identifier">The unique identifier of the menu to add.</param>
        /// <param name="text">The text to be displayed by the added menu item.</param>
        void AddMenu(Guid identifier, string text);

        /// <summary>
        /// Adds a separator after the menu item given the two identifiers.
        /// </summary>
        /// <param name="menuItemId">The unique identifier of the menu item after which the separator will be added.</param>
        /// <param name="identifier">The unique identifier of the separator.</param>
        void AddSeparatorAfter(Guid menuItemId, Guid identifier);

        /// <summary>
        /// Adds a menu item to the view menu with an identifier given the command binding and the text.
        /// </summary>
        /// <param name="identifier">The unique identifier of the menu to add.</param>
        /// <param name="commandBinding">
        /// The command binding tiying the menu item with its corresponding command.
        /// </param>
        /// <param name="text">The text to be displayed by the added menu item.</param>
        void AddMenuItemToViewMenu(Guid identifier, CommandBinding commandBinding, string text);

        /// <summary>
        /// Adds a menu item to the menu item with an identifier given the command binding and text for the new menu item.
        /// </summary>
        /// <param name="parent">The unique identifier of the parent menu to which will be added the menu item.</param>
        /// <param name="identifier">The unique identifier of the menu to add.</param>
        /// <param name="commandBinding">
        /// The command binding tiying the menu item with its corresponding command.
        /// </param>
        /// <param name="text">The text to be displayed by the added menu item.</param>
        void AddMenuTo(Guid parent, Guid identifier, CommandBinding commandBinding, string text);

        /// <summary>
        /// Adds a menu item to the menu item with an identifier given the command binding, a bindind for the command
        /// parameter and text for the new menu item.
        /// </summary>
        /// <param name="parent">The unique identifier of the parent menu to which will be added the menu item.</param>
        /// <param name="identifier">The unique identifier of the menu to add.</param>
        /// <param name="commandBinding">
        /// The command binding tiying the menu item with its corresponding command.
        /// </param>
        /// <param name="commandParameterBinding">
        /// The binding to be used in order to pass the command parameter to the command set for the new menu item.
        /// </param>
        /// <param name="text">The text to be displayed by the added menu item.</param>
        void AddMenuTo(Guid parent, Guid identifier, CommandBinding commandBinding, BindingBase commandParameterBinding, string text);
    }
}
