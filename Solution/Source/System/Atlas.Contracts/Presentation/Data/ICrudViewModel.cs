using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using CompanyName.Atlas.Contracts.Domain;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    /// <summary>
    /// Represents the contract of a view model used to be used in items composed controls.
    /// </summary>
    public interface ICrudViewModel : INotifyPropertyChanged, IEnumerable, IInteractionRequester
    {
        /// <summary>
        /// Gets the list of presenter view models in the current view model.
        /// </summary>
        IList Items { get; }

        /// <summary>
        /// Gets the command allowing to the add an presenter view model to the view model.
        /// </summary>
        ICommand AddCommand { get; }

        /// <summary>
        /// Gets the command allowing to the delete an presenter view model from the view model.
        /// </summary>
        ICommand DeleteCommand { get; }

        /// <summary>
        /// Gets the command allowing to the delete an presenter view model from the view model.
        /// </summary>
        ICommand SimpleFilterCommand { get; }

        /// <summary>
        /// Gets or sets the presenter view model currently selected.
        /// </summary>
        object SelectedItem { get; set; }


        /// <summary>
        /// Occurs when a new item has been added.
        /// </summary>
        event EventHandler<ItemEventArgs> AddedItem;

        /// <summary>
        /// Occurs when an item has been deleted.
        /// </summary>
        event EventHandler<ItemEventArgs> DeletedItem;


        /// <summary>
        /// Loads the presenter view model from the data source.
        /// </summary>
        void Load();

        /// <summary>
        /// Finds the object with the given identifier.
        /// </summary>
        /// <param name="id">The identifier of the object to find.</param>
        /// <returns>The object which identifier is the given one at <paramref name="id"/>.</returns>
        object Find(object id);

        bool IsLoaded { get; }

        void Change();


        // bool isLoaded { get;  }
    }


    /// <summary>
    /// Represents the contract of a view model used to be used in items composed controls.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the objects managed by the presenter view models with which deals the current crud view model.
    /// </typeparam>
    /// <typeparam name="TPresenter">
    /// The type of the presenter that must be used to decorate the objects handled in the current crud view model.
    /// </typeparam>
    public interface ICrudViewModel<T, TPresenter> : ICrudViewModel, IEnumerable<TPresenter>
        where T : IEntity
        where TPresenter : class, IPresenter<T>
    {
        /// <summary>
        /// Gets the list of presenter view models in the current view model.
        /// </summary>
        new IList<TPresenter> Items { get; }

        /// <summary>
        /// Gets or sets the presenter view model currently selected.
        /// </summary>
        new TPresenter SelectedItem { get; set; }


        /// <summary>
        /// Adds a new presenter view model to the current view model.
        /// </summary>
        /// <param name="presenter">The presenter view model to add.</param>
        void Add(TPresenter presenter);

        /// <summary>
        /// Deletes an presenter view model from the current view model.
        /// </summary>
        /// <param name="presenter">The presenter view model to delete.</param>
        void Delete(TPresenter presenter);

        /// <summary>
        /// Finds the object with the given identifier.
        /// </summary>
        /// <param name="id">The identifier of the object to find.</param>
        /// <returns>The object which identifier is the given one at <paramref name="id"/>.</returns>
        new TPresenter Find(object id);

       
    }
}
