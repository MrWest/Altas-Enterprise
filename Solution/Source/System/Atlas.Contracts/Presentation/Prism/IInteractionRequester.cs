using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    /// <summary>
    /// This interface describes an object capable of start interactions with the user.
    /// </summary>
    public interface IInteractionRequester : IInteractionRequest
    {
        /// <summary>
        /// Starts an interaction using a given context.
        /// </summary>
        /// <typeparam name="TContext">The type of the context describing the kind of interaction request.</typeparam>
        /// <param name="context">The context to be used in the interaction.</param>
        /// <param name="callback">The callback used in the interaction.</param>
        /// <returns>The modified context with the results of the interaction.</returns>
        TContext Interact<TContext>(TContext context, Action callback) where TContext : INotification;
    }
}
