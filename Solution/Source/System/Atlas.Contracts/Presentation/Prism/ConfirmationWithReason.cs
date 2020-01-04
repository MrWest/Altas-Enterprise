using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// This class identifies a confirmation having an well defined intention.
    /// </summary>
    public class ConfirmationWithReason<TConfirmation> : IConfirmation, INotificationWithReason where TConfirmation : class, IConfirmation
    {
        private readonly TConfirmation _confirmation;


        /// <summary>
        /// Initializes a new instance of a confirmation having a reason.
        /// </summary>
        /// <param name="confirmation">
        /// The actual confirmation object wrapped by the current one in order to provide it a reason besides all the other fields.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="confirmation"/> is null.</exception>
        public ConfirmationWithReason(TConfirmation confirmation)
        {
            if (confirmation == null) throw new ArgumentNullException("confirmation");
            
            _confirmation = confirmation;
        }


        /// <summary>
        /// Gets or sets the intention of the current confirmation.
        /// </summary>
        public NotificationReason Reason { get; set; }

        /// <summary>
        /// Gets or sets the title to use for the confirmation.
        /// </summary>
        public string Title
        {
            get { return _confirmation.Title; }
            set { _confirmation.Title = value; }
        }

        /// <summary>
        /// Gets or sets the content of the confirmation.
        /// </summary>
        public object Content
        {
            get { return _confirmation.Content; }
            set { _confirmation.Content = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating that the confirmation is confirmed.
        /// </summary>
        public bool Confirmed
        {
            get { return _confirmation.Confirmed; }
            set { _confirmation.Confirmed = value; }
        }
    }
}
