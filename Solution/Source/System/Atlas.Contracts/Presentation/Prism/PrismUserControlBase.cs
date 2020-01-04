using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using IView = Microsoft.Practices.Prism.Mvvm.IView;

namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// This is the base class for the user controls using Prism functionality. Every user control in the system must derive
    /// this class.
    /// </summary>
    public class PrismUserControlBase : UserControl, IView
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PrismUserControlBase"/>.
        /// </summary>
        public PrismUserControlBase()
        {
            DataContextChanged += OnDataContextChanged;
        }

       


        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new datacontext are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        }

        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }
    }
}
