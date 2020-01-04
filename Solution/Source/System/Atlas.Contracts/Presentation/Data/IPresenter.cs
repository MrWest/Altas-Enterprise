using System.ComponentModel;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    /// <summary>
    /// This describes the contract of the view models representing certain object. It serves as an indirection or
    /// separation object to allow to the crud view models to be separated from the actual objects it its managing.
    /// </summary>
    public interface IPresenter : INotifyPropertyChanged, IInteractionRequester
    {
        /// <summary>
        /// Gets the object being represented in the user interface by this presenter view model.
        /// </summary>
        object Object { get; set; }

        void Notify();
    }


    /// <summary>
    /// This describes the contract of the view models representing certain object. It serves as an indirection or
    /// separation object to allow to the crud view models to be separated from the actual objects it its managing.
    /// </summary>
    /// <typeparam name="T">The type of the object being represented.</typeparam>
    public interface IPresenter<T> : IPresenter
    {
        /// <summary>
        /// Gets the object being represented in the user interface by this presenter view model.
        /// </summary>
        new T Object { get; set; }

       
    }

    
   
}
