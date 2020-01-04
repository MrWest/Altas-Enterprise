using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.ViewModels
{
    /// <summary>
    /// This is the view model that must be placed behind the status bar of the shell.
    /// </summary>
    public class ShellViewModel : ViewModelBase, IStatusBarServices,IView
    {
        private string _statusText = Resources.Welcome;
        private readonly int _maxTimePerCharToReadyMessage = Settings.Default.MaxTimePerCharToReadyStatus;
        private Task _callbackTask;
        private readonly ConcurrentQueue<string> _messages = new ConcurrentQueue<string>();
        //private IAtlasUserViewModel _atlasUserViewModel;
        //public IAtlasUserViewModel AtlasUserViewModel
        //{
        //    get
        //    {
        //        return _atlasUserViewModel ?? (_atlasUserViewModel = CreateAndInitialize<IAtlasUserViewModel>());
        //    }
        //}

        ///// <summary>
        ///// Gets a new instance of a crud view model and loads it.
        ///// </summary>
        ///// <returns>A new and initialized instance of <typeparamref name="TViewModel"/>.</returns>
        //protected TViewModel CreateAndInitialize<TViewModel>()
        //    where TViewModel : ICrudViewModel
        //{
        //    var viewModel = ServiceLocator.Current.GetInstance<TViewModel>();

        //    viewModel.Raised += OnInteractionRequested;
        //    viewModel.Load();
        //    viewModel.Raised -= OnInteractionRequested;

        //    return viewModel;
        //}
        protected internal virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            ((IView)sender).Notify(e);
        }

        #region IStatusBarServices Members

        public bool isTextReallyShowed
        {
            get
            {
                return _isTextReallyShowed;
            }
            set
            {
                _isTextReallyShowed = value;
                OnPropertyChanged("isTextReallyShowed");
            }
        }


        private bool _isTextReallyShowed;

        /// <summary>
        /// Gets the current status text.
        /// </summary>
    public string StatusText
        {
            get
            {
                
                return _statusText;
            }
            private set
            {
                _isTextReallyShowed = false;
                SetProperty(ref _statusText, value);
                
            }
        }

       
    

        /// <summary>
        /// Places the ready status in the status bar.
        /// </summary>
        public void SignalReady()
        {
            StatusText = Resources.Ready;
            OnPropertyChanged(() => StatusText);

        }

        /// <summary>
        /// Places the ready status in the status bar.
        /// </summary>
        public async void SignalWaitOperation()
        {

            //Thread thread = new Thread(LoadModule3);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Highest;
            //thread.Start();

            StatusText = Resources.WaitOperation;
            OnPropertyChanged(() => StatusText);


        }
        /// <summary>
        /// Places the loading status in the status bar.
        /// </summary>
        public void SignalLoading()
        {
            InternalSignalText(Resources.Loading);
            // StatusText = Resources.Loading;
        }

        public string GetSomeContractText(int choice)
        {
            if (choice == 1)
                return Resources.WaitOperation;
           return Resources.Ready;
        }
        /// <summary>
        /// Displays the specified text in the status bar.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is null or empty.</exception>
        public void SignalText(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            InternalSignalText(text);
        }
        /// <summary>
        /// Displays the specified text in the status bar.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is null or empty.</exception>
        public void SignalText2(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            InternalSignalText2(text);
        }
        public void ForceSignalLoading()
        {
            StatusText = Resources.Loading;
            OnPropertyChanged(() => StatusText);

            Thread thread = new Thread(LoadModule);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

        }

        public void ForceSignalLoading2()
        {
            StatusText = Resources.Loading;
            OnPropertyChanged(() => StatusText);

            Thread thread = new Thread(LoadModule2);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            
        }
        private void LoadModule()
        {

            // Get the dispatcher from the current window, and use it to invoke
            // the update code.
       
                InternalSignalText(Resources.Loading);

         
        }

        private void LoadModule3()
        {

            // Get the dispatcher from the current window, and use it to invoke
            // the update code.

            InternalSignalText(Resources.WaitOperation);


        }

        private void LoadModule2()
        {

            // Get the dispatcher from the current window, and use it to invoke
            // the update code.

            InternalSignalText2(Resources.Loading);


        }

        private async void InternalSignalText(string text)
        {
            // Cancel the current ready singaling, in any
            _messages.Enqueue(text != null ?
                (text.Length > 100 ? text.Substring(0, 100) + "..." : text) 
                : string.Empty);

            if (_callbackTask == null || _callbackTask.IsCompleted)
                _callbackTask = Task.Factory.StartNew(AsyncSignalText);

            await _callbackTask;
        }
        private async void InternalSignalText2(string text)
        {
            // Cancel the current ready singaling, in any
            _messages.Enqueue(text != null ?
                (text.Length > 100 ? text.Substring(0, 100) + "..." : text)
                : string.Empty);

            if (_callbackTask == null || _callbackTask.IsCompleted)
                _callbackTask = Task.Factory.StartNew(AsyncSignalText2);

            await _callbackTask;
        }
        private void AsyncSignalText()
        {
            while (SpinWait.SpinUntil(() => _messages.Any(), _maxTimePerCharToReadyMessage * StatusText.Length))
            {
                string message;
                if (_messages.TryDequeue(out message))
                {
                    StatusText = message;
                    OnPropertyChanged(() => StatusText);
                }
                   
            }

            SignalReady();
        }

        private void AsyncSignalText2()
        {
            while (SpinWait.SpinUntil(() => _messages.Any(), _maxTimePerCharToReadyMessage * StatusText.Length*4))
            {
                string message;
                if (_messages.TryDequeue(out message))
                {
                    StatusText = message;
                    OnPropertyChanged(() => StatusText);
                }

            }

          //  SignalReady();
        }

        #endregion

        public object DataContext { get; set; }
    }
}
