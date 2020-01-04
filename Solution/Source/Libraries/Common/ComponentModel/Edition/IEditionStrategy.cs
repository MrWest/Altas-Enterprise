using System.Collections.Generic;

namespace System.ComponentModel.Edition
{
    /// <summary>
    ///     Describes the behavior of an edition strategy.
    /// </summary>
    /// <typeparam name="T">The type of the editable object to edit in a customized way.</typeparam>
    public interface IEditionStrategy<T> where T : class, new()
    {
        /// <summary>
        ///     Gets or sets the object to control its edition.
        /// </summary>
        T EditingObject { get; set; }

        /// <summary>
        ///     Gets a dictionary to be used to set predefined values for properties in the editable object. These
        ///     properties will get the values defined here when the edition is ended.
        /// </summary>
        IDictionary<string, object> Values { get; }

        /// <summary>
        ///     Gets whether there were changes made to the editable object.
        /// </summary>
        bool HasChanges { get; }
        
        
        /// <summary>
        ///     Occurs when there has been started the edition.
        /// </summary>
        event EventHandler BeganEdition;
        
        /// <summary>
        ///     Occurs when there has been ended the edition.
        /// </summary>
        event EventHandler EndedEdition;
        
        /// <summary>
        ///     Occurs when there has been cancelled the edition.
        /// </summary>
        event EventHandler CancelledEdition;


        /// <summary>
        ///     Records the state of the given object to start its edition.
        /// </summary>
        void BeginEdition();

        /// <summary>
        ///     Rollbacks the changes made to the editable object during the edition.
        /// </summary>
        void CancelEdition();

        /// <summary>
        ///     Makes effective the changes made to the editable object during the edition.
        /// </summary>
        void EndEdition();
    }
}