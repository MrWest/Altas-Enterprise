using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.ComponentModel.Edition
{
    /// <summary>
    ///     Base class of the edition strategies.
    /// </summary>
    /// <typeparam name="T">The type of the object to be edited by this strategy.</typeparam>
    public abstract class EditionStrategyBase<T> : IEditionStrategy<T> where T : class, new()
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();
        private bool _inEdition;
        
        
        /// <summary>
        ///     Initializes a new instance of <see cref="System.ComponentModel.Edition.EditionStrategyBase{T}" />.
        /// </summary>
        protected EditionStrategyBase()
        {
            BeganEdition += OnBeganEdition;
            EndedEdition += OnEndedEdition;
            CancelledEdition += OnCancelledEdition;
        }


        /// <summary>
        ///     Gets the object to control its edition.
        /// </summary>
        public T EditingObject { get; set; }

        /// <summary>
        ///     Gets a dictionary to be used to set predefined values for properties in the editable object. These
        ///     properties will get the values defined here when the edition is ended.
        /// </summary>
        public IDictionary<string, object> Values
        {
            get { return _values; }
        }

        /// <summary>
        ///     When overridden in a deriver it gets whether there were changes made to this editable object.
        /// </summary>
        public abstract bool HasChanges { get; }

        /// <summary>
        ///     Gets whether the current editing object is in edition;
        /// </summary>
        [ExcludeFromCodeCoverage]
        protected virtual bool InEdition
        {
            get { return _inEdition; }
        }

        /// <summary>
        ///     Occurs when there has been started the edition.
        /// </summary>
        public event EventHandler BeganEdition;
        
        /// <summary>
        ///     Occurs when there has been ended the edition.
        /// </summary>
        public event EventHandler EndedEdition;
        
        /// <summary>
        ///     Occurs when there has been cancelled the edition.
        /// </summary>
        public event EventHandler CancelledEdition;

        
        /// <summary>
        ///     Records the state of the given object to start its edition.
        /// </summary>
        public void BeginEdition()
        {
            if (_inEdition)
                return;

            InternalBeginEdition();
            BeganEdition(this, EventArgs.Empty);
            _inEdition = true;
        }

        /// <summary>
        ///     Rolls back the changes made to the editable object during the
        ///     edition.
        /// </summary>
        public void CancelEdition()
        {
            if (!_inEdition)
                return;

            InternalCancelEdition();
            CancelledEdition(this, EventArgs.Empty);
            _inEdition = false;
        }

        /// <summary>
        ///     Makes effective the changes made to the editable object during
        ///     the edition.
        /// </summary>
        public void EndEdition()
        {
            if (!_inEdition)
                return;

            InternalEndEdition();
            EndedEdition(this, EventArgs.Empty);
            _inEdition = false;
        }


        /// <summary>
        ///     When overridden in a derived class it performs the edition begining logic.
        /// </summary>
        protected virtual void InternalBeginEdition()
        {
        }

        /// <summary>
        ///     When overridden in a derived class it performs the edition ending logic.
        /// </summary>
        protected virtual void InternalEndEdition()
        {
        }

        /// <summary>
        ///     When overridden in a derived class it performs the edition cancellation logic.
        /// </summary>
        protected virtual void InternalCancelEdition()
        {
        }
        
        /// <summary>
        ///     When overridden in a derived class it handles the BeganEdition event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">
        ///     An instance of <see cref="System.EventArgs" /> containing information regarding to the event.
        /// </param>
        protected virtual void OnBeganEdition(object sender, EventArgs e)
        {
        }
        
        /// <summary>
        ///     When overridden in a derived class it handles the EndedEdition event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">
        ///     An instance of <see cref="System.EventArgs" /> containing information regarding to the event.
        /// </param>
        protected virtual void OnEndedEdition(object sender, EventArgs e)
        {
        }
        
        /// <summary>
        ///     When overridden in a derived class it handles the CancelledEdition event.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">
        ///     An instance of <see cref="System.EventArgs" /> containing information regarding to the event.
        /// </param>
        protected virtual void OnCancelledEdition(object sender, EventArgs e)
        {
        }
    }
}