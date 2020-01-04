using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    /// <summary>
    /// Base class of the view models to be placed as data context of items composed controls.
    /// </summary>
    /// <typeparam name="T">The type of the objects in the view model.</typeparam>
    /// <typeparam name="TPresenter">
    /// This is the type of the presenter view models used to decorate the items managed in the current crud view
    /// model.
    /// </typeparam>
    /// <typeparam name="TServices">The type application service to be used by this view model.</typeparam>
    public abstract class CrudViewModelBase<T, TPresenter, TServices> :
        ServiceBackedViewModel<TServices>,
        ICrudViewModel<T, TPresenter>
        where T : class, IEntity
        where TPresenter : class, IPresenter<T>
        where TServices : IItemManagerApplicationServices<T>
    {
        protected readonly ObservableCollection<TPresenter> _items = new ObservableCollection<TPresenter>();
        protected TPresenter _selectedItem;

        private EventHandlerManager<ItemEventArgs> _addedItemEventHandlers = new EventHandlerManager<ItemEventArgs>();
        private EventHandlerManager<ItemEventArgs> _deletedItemEventHandlers = new EventHandlerManager<ItemEventArgs>();

        protected bool _isLoaded=false;
        /// <summary>
        /// Initializes a new instance of a crud view model deriver.
        /// </summary>
        protected CrudViewModelBase()
        {
            AddCommand = new DelegateCommand<TPresenter>(Add, CanAdd);
            DeleteCommand = new DelegateCommand<TPresenter>(Delete, CanDelete);
            SimpleFilterCommand = new DelegateCommand<string>(SimpleFilter, CanSimpleFilter);

            AddedItem += OnAddedItem;
            DeletedItem += OnDeletedItem;
            PropertyChanged += OnPropertyHasChanged;

           
        }

        private bool CanSimpleFilter(string arg)
        {
            return true;
        }

        private void SimpleFilter(string searchText)
        {
            Load();
          
            var filterList =_items.Where(x => x.GetType().GetProperties().Any(p => p.GetValue(x) != null
                          && p.GetValue(x).ToString().ToLower().Contains(searchText.ToLower())));
            ObservableCollection<TPresenter> _filtred = new ObservableCollection<TPresenter>();
            foreach (TPresenter presenter in filterList)
            {
                _filtred.Add(presenter);
            }
            Items.Clear();
            foreach (TPresenter presenter in _filtred)
            {
                Items.Add(presenter);
            }

            Change = !Change;
           // _isLoaded = false;
            OnPropertyChanged(() => Change);
            OnPropertyChanged(()=>Items);
        }


        #region ICrudViewModel<T, TPresenter> Members

        /// <summary>
        /// Gets the list of presenter view models in the current view model.
        /// </summary>
        public virtual IList<TPresenter> Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets or sets the presenter view model currently selected.
        /// </summary>
        public virtual TPresenter SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }


        /// <summary>
        /// Adds a new item to the current view model.
        /// </summary>
        /// <param name="presenter">The item to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="presenter"/> is null.</exception>
        public virtual void Add(TPresenter presenter)
        {
            ExecuteUsingServices(services =>
            {
                try
                {
                    if (Equals(presenter, null))
                    {
                        T item = services.Create();
                        presenter = CreatePresenterFor(item);
                    }

                    services.Add(presenter.Object);
                    presenter = CreatePresenterFor(presenter.Object);
                    Items.Add(presenter);

                    RaiseAddedItemEvent(presenter);
                   
                    SignalStatus(GetSuccessfullyAddedElementMessage(presenter));
                }
                catch (Exception e)
                {
                    ParseAdditionException(presenter, e);
                }
            });
        }

        /// <summary>
        /// Deletes an presenter view model from the current view model.
        /// </summary>
        /// <param name="presenter">The presenter view model to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="presenter"/> is null.</exception>
        public virtual void Delete(TPresenter presenter)
        {
            if (Equals(presenter, null))
                throw new ArgumentNullException("presenter");

            IConfirmation confirmation = new Confirmation() {Confirmed = true, Content = presenter.Object};
            if(presenter.GetType().Implements<IConfirmable>())
                 confirmation = Confirm(GetDeleteConfirmationMessage(presenter), GetDeleteConfirmationTitle(presenter));

            if (!confirmation.Confirmed)
                return;

            ExecuteUsingServices(services =>
            {
                try
                {
                    services.Delete(presenter.Object);

                    Items.Remove(presenter);

                    RaiseDeletedItemEvent(presenter);
                    SignalStatus(GetSuccessfullyDeletedElementMessage(presenter));
                }
                catch (Exception e)
                {
                    ParseDeletionException(presenter, e);
                }
            });
        }

        /// <summary>
        /// Finds the object with the given identifier.
        /// </summary>
        /// <param name="id">The identifier of the object to find.</param>
        /// <returns>The object which identifier is the given one at <paramref name="id"/>.</returns>
        /// <exception cref="System.InvalidOperationException">There is more than one element with the given identifier.</exception>
        public virtual TPresenter Find(object id)
        {
            return Items.SingleOrDefault(x => Equals(x.Object.Id, id));
        }

        /// <summary>
        /// Gets the title of the confirmation sent to the user when a deletion is about to happen.
        /// </summary>
        protected virtual string GetDeleteConfirmationTitle(TPresenter presenter)
        {
            return Resources.SureToDeleteTitle.EasyFormat(presenter);
        }

        /// <summary>
        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        /// with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected virtual string GetDeleteConfirmationMessage(TPresenter presenter)
        {
            return Resources.SureToDelete.EasyFormat(presenter);
        }

        /// <summary>
        /// Gets the message that should be displayed in the status component when there was added an element.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        /// A string representing the full message to display in order to notify the user that there was successfully
        /// added the given presenter element at <paramref name="addedPresenter"/>.
        /// </returns>
        protected virtual string GetSuccessfullyAddedElementMessage(TPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedItemStatusMessage.EasyFormat(addedPresenter);
        }

        /// <summary>
        /// Gets the message that should be displayed in the status component when there was deleted an element.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        /// A string representing the full message to display in order to notify the user that there was successfully
        /// deleted the given presenter element at <paramref name="deletedPresenter"/>.
        /// </returns>
        protected virtual string GetSuccessfullyDeletedElementMessage(TPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedItemStatusMessage.EasyFormat(deletedPresenter);
        }

        /// <summary>
        /// Gets the error notification that is to be displayed to the user on an addition failure.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> being the cause of the error.</param>
        /// <param name="presenter">The <typeparamref name="TPresenter"/> which addition failed.</param>
        /// <returns>An <see cref="String"/> representing the message to display to the user when failed adding <paramref name="presenter"/>.</returns>
        protected virtual string GetErrorMessageInAddition(Exception exception, TPresenter presenter)
        {
            return Resources.ErrorWhileAddingItem.EasyFormat(presenter, exception.Message);
        }

        /// <summary>
        /// Gets the title of the error notification that is to be displayed to the user on an addition failure.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> being the cause of the error.</param>
        /// <param name="presenter">The <typeparamref name="TPresenter"/> which addition failed.</param>
        /// <returns>An <see cref="String"/> representing the message to display to the user when failed adding <paramref name="presenter"/>.</returns>
        protected virtual string GetErrorTitleInAddition(Exception exception, TPresenter presenter)
        {
            return Resources.ErrorWhileAddingItemTitle;
        }

        /// <summary>
        /// Gets the error notification that is to be displayed to the user on an deletion failure.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> being the cause of the error.</param>
        /// <param name="presenter">The <typeparamref name="TPresenter"/> which deletion failed.</param>
        /// <returns>An <see cref="String"/> representing the message to display to the user when failed deleting <paramref name="presenter"/>.</returns>
        protected virtual string GetErrorMessageInDeletion(Exception exception, TPresenter presenter)
        {
            return Resources.ErrorWhileDeletingItem.EasyFormat(presenter, exception.Message);
        }

        /// <summary>
        /// Gets the title of the error notification that is to be displayed to the user on an deletion failure.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> being the cause of the error.</param>
        /// <param name="presenter">The <typeparamref name="TPresenter"/> which deletion failed.</param>
        /// <returns>An <see cref="String"/> representing the message to display to the user when failed deleting <paramref name="presenter"/>.</returns>
        protected virtual string GetErrorTitleInDeletion(Exception exception, TPresenter presenter)
        {
            return Resources.ErrorWhileDeletingItemTitle;
        }

        #endregion


        #region ICrudViewModel Members

        /// <summary>
        /// Gets the list of items in the current view model.
        /// </summary>
        IList ICrudViewModel.Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets the command allowing to the add an item to the view model.
        /// </summary>
        public ICommand AddCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command allowing to the delete an item from the view model.
        /// </summary>
        public ICommand DeleteCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the item currently selected.
        /// </summary>
        object ICrudViewModel.SelectedItem
        {
            get { return SelectedItem; }
            set { SelectedItem = (TPresenter)value; }
        }


        /// <summary>
        /// Occurs when a new item has been added.
        /// </summary>
        public event EventHandler<ItemEventArgs> AddedItem
        {
            add { _addedItemEventHandlers += value; }
            remove { _addedItemEventHandlers -= value; }
        }

        /// <summary>
        /// Occurs when an item has been deleted.
        /// </summary>
        public event EventHandler<ItemEventArgs> DeletedItem
        {
            add { _deletedItemEventHandlers += value; }
            remove { _deletedItemEventHandlers -= value; }
        }


        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public virtual void Load()
        {
            foreach (TPresenter presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            _items.Clear();

            ExecuteUsingServices(services =>
            {
                var itemList = GetItems(services);
                foreach (T item in itemList)
                {
                    TPresenter presenter = CreatePresenterFor(item);
                    presenter.PropertyChanged += OnPresenterPropertyChanged;

                    //if(_items.All(x=>x.Object.Id.ToString()!=item.Id.ToString()))
                    _items.Add(presenter);
                }
            });

            _isLoaded = true;
        }


        object ICrudViewModel.Find(object id)
        {
            return Find(id);
        }

        #endregion


        #region IEnumerable Members

        /// <summary>
        /// Gets an enumerator allowing iteration over the objects in the current view model.
        /// </summary>
        /// <returns>An enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


        #region IEnumerable<T> Members

        /// <summary>
        /// Gets an enumerator allowing iteration over the objects in the current view model.
        /// </summary>
        /// <returns>An enumerator</returns>
        public IEnumerator<TPresenter> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion


        /// <summary>
        /// Determines whether there can be added an presenter view model to the current view model.
        /// </summary>
        /// <param name="presenter">The presenter view model to determine whether can be added, can be null.</param>
        /// <returns>
        /// True if given an non null presenter view model of <typeparamref name="T"/> type of a null item; false
        /// otherwise.
        /// </returns>
        public virtual bool CanAdd(TPresenter presenter)
        {
            return ExecuteUsingServices(services =>
            {
                return presenter != null ? services.CanAdd(presenter.Object) : services.CanAddNew();
            });
        }

        /// <summary>
        /// Determines whether there can be deleted an presenter view model from the current view model.
        /// </summary>
        /// <param name="presenter">The presenter view model to determine whether can be deleted.</param>
        /// <returns>
        /// True if given an non null presenter view model of <typeparamref name="T"/> type; false otherwise.
        /// </returns>
        public virtual bool CanDelete(TPresenter presenter)
        {
            if (presenter == null)
                return false;

            return ExecuteUsingServices(services => services.CanDelete(presenter.Object));
        }


        /// <summary>
        /// Gets the items requesting them all to the given service.
        /// </summary>
        /// <param name="service">The instance of the service to used in the retrieving of the items.</param>
        /// <returns>An enumerable allowing iteration over items gotten from the service.</returns>
        protected virtual IEnumerable<T> GetItems(TServices service)
        {
            return service.Items;
        }

        /// <summary>
        /// Creates a new presenter view model for the given item.
        /// </summary>
        /// <param name="item">The item a presenter view model is going to be created for.</param>
        /// <returns>
        /// A new instance of type <typeparamref name="TPresenter"/> decorating the item given in
        /// <paramref name="item"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        protected virtual TPresenter CreatePresenterFor(T item)
        {
            if (Equals(item, null))
                throw new ArgumentNullException("item");

            var presenter = ServiceLocator.Current.GetInstance<TPresenter>();
            presenter.Object = item;

            return presenter;
        }


        private void ParseAdditionException(TPresenter presenter, Exception exception)
        {
            string textHeader = Resources.ItemNotAddedDueToValidationErrors.EasyFormat(presenter);
            string title = GetErrorTitleInAddition(exception, presenter);

            ParseException(title, textHeader, exception);
        }

        protected void ParseDeletionException(TPresenter presenter, Exception exception)
        {
            string textHeader = Resources.ItemNotDeletedDueToValidationErrors.EasyFormat(presenter);
            string title = GetErrorTitleInAddition(exception, presenter);

            ParseException(title, textHeader, exception);
        }

        private void RaiseAddedItemEvent(TPresenter presenter)
        {
            _addedItemEventHandlers.CallEventHandlers(this, new ItemEventArgs<TPresenter>(presenter));
        }

        protected void RaiseDeletedItemEvent(TPresenter presenter)
        {
            _deletedItemEventHandlers.CallEventHandlers(this, new ItemEventArgs<TPresenter>(presenter));
        }

        protected bool CheckIsItemEventArgs(EventArgs e, out ItemEventArgs<TPresenter> itemEventArgs)
        {
            itemEventArgs = e as ItemEventArgs<TPresenter>;
            return itemEventArgs != null;
        }

        public bool Change { get; set; }
        /// <summary>
        /// Handles the PropertyChanged event on all the presenters currently contained in the current crud view model. When any change is notified, then an Update
        /// command is sent to the application services.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Arguments relative to the event.</param>
        protected virtual void OnPresenterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// When overridden in a deriver it allows it handle the AddedItem event.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="e">The information generated in the event with details about the addition.</param>
        protected virtual void OnAddedItem(object sender, EventArgs e)
        {
            ItemEventArgs<TPresenter> arguments;
            if (!CheckIsItemEventArgs(e, out arguments))
                return;

            arguments.Item.PropertyChanged += OnPresenterPropertyChanged;

            Change = !Change;
            _isLoaded = false;
            OnPropertyChanged(()=>Change);
        }

        /// <summary>
        /// When overridden in a deriver it allows it handle the DeletedItem event.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="e">The information generated in the event with details about the deletion.</param>
        protected virtual void OnDeletedItem(object sender, EventArgs e)
        {
            ItemEventArgs<TPresenter> arguments;
            if (!CheckIsItemEventArgs(e, out arguments))
                return;
          
            arguments.Item.PropertyChanged -= OnPresenterPropertyChanged;
            Change = !Change;
            _isLoaded = false;
            OnPropertyChanged(() => Change);
        }

        /// <summary>
        /// Handles the PropertyChanged event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">The arguments containing the name of the changed property.</param>
        protected virtual void OnPropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public bool IsLoaded
        {
            get { return _isLoaded; }
        }

        public ICommand SimpleFilterCommand { get; }
        void ICrudViewModel.Change()
        {
            Change = !Change;
            OnPropertyChanged(() => Change);
        }

        // public bool isLoaded { get { return _isLoaded; }  }
    }
}
