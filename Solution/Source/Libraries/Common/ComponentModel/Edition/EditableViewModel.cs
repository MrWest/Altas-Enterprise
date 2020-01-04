using Microsoft.Practices.Prism.Mvvm;

namespace System.ComponentModel.Edition
{
    /// <summary>
    ///     This class represents the base class of all the ediatable view models. But edition logic ins not contained
    ///     in this class, but it's injected and defined somewhere else.
    /// </summary>
    public class EditableViewModel<T> : BindableBase, IEditableObject where T : class, INotifyPropertyChanged, new()
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="Edition.EditableViewModel{T}" /> with an object
        ///     and an edition strategy.
        /// </summary>
        /// <param name="obj">The object to be edited.</param>
        /// <param name="strategy">An strategy to be used when engaging edition.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="obj" /> or <paramref name="strategy" /> is null.
        /// </exception>
        public EditableViewModel(T obj, IEditionStrategy<T> strategy)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (strategy == null)
                throw new ArgumentNullException("strategy");

            Strategy = strategy;
            Strategy.EditingObject = obj;

            obj.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="Edition.EditableViewModel{T}" />.
        /// </summary>
        public EditableViewModel()
        {
            Strategy = new EditionOverCopyStrategy<T>
            {
                EditingObject = new T()
            };
        }


        /// <summary>
        ///     Gets the strategy used to handle edition cycles.
        /// </summary>
        public IEditionStrategy<T> Strategy { get; protected set; }

        /// <summary>
        ///     Gets the object to edit.
        /// </summary>
        public T Object
        {
            get { return Strategy.EditingObject; }
        }

        /// <summary>
        ///     Gets whether there were changes made to this editable object.
        /// </summary>
        public virtual bool HasChanges
        {
            get { return Strategy.HasChanges; }
        }


        /// <summary>
        ///     Establishes a snapshot of the current state of this object.
        /// </summary>
        public virtual void BeginEdit()
        {
            Strategy.BeginEdition();
            OnPropertyChanged("Object");
        }

        /// <summary>
        ///     Makes effective the changes there were made to this object since the last call to BeginEdit.
        /// </summary>
        public virtual void EndEdit()
        {
            Strategy.EndEdition();
            OnPropertyChanged("Object");
        }

        /// <summary>
        ///     Cancels the changes there were made to this object since the last call to BeginEdit by restoring the
        ///     state this object was at when started the edition.
        /// </summary>
        public virtual void CancelEdit()
        {
            Strategy.CancelEdition();
            OnPropertyChanged("Object");
        }

        /// <summary>
        /// Invoked when the PropertyChanged event has been raised handling such event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">The argument of the event.</param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}