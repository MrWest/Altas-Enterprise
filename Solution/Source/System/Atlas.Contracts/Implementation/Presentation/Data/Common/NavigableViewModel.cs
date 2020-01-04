using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.Prism.Commands;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Contracts.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
   public class NavigableViewModel<T, TPresenter,TService> : CrudViewModelBase<T,TPresenter,TService>, INavigableViewModel<T, TPresenter>
         where T : class, IEntity
        where TPresenter : class,IPresenter<T>, INavigable
         where TService : class, IItemManagerApplicationServices<T>
    {
        private ICommand _filterCommand;
        private IStatusBarServices _statusBarServices;
        protected BackgroundWorker _BackgroundWorker;

        public NavigableViewModel()
        {
            _BackgroundWorker = new BackgroundWorker();
            _BackgroundWorker.DoWork += DeleteOnDoWork;
            _BackgroundWorker.RunWorkerCompleted += DeleteOnRunWorkerCompleted;
        }

        protected void DeleteOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            TPresenter presenter = e.Result as TPresenter;
            Items.Remove(presenter);

            RaiseDeletedItemEvent(presenter);
            SignalStatus(GetSuccessfullyDeletedElementMessage(presenter));
        }

        protected void DeleteOnDoWork(object sender, DoWorkEventArgs e)
        {
            TPresenter presenter = e.Argument as TPresenter;
            ExecuteUsingServices(services =>
            {
                try
                {
                    services.Delete(presenter.Object);

                    
                }
                catch (Exception ex)
                {
                    ParseDeletionException(presenter, ex);
                }
            });

            e.Result = presenter;
        }

       
        public override void Delete(TPresenter presenter)
        {
            if (Equals(presenter, null))
                throw new ArgumentNullException("presenter");

            IConfirmation confirmation = new Confirmation() { Confirmed = true, Content = presenter.Object };
            if (presenter.GetType().Implements<IConfirmable>())
                confirmation = Confirm(GetDeleteConfirmationMessage(presenter), GetDeleteConfirmationTitle(presenter));

            if (!confirmation.Confirmed)
                return;

            StatusBarServices.SignalWaitOperation();

            _BackgroundWorker.RunWorkerAsync(presenter);
        }

        public ICommand FilterCommand
        {
            get
            {
                if (_filterCommand == null)
                {
                    _filterCommand = new DelegateCommand<string>(Filter, CanFilter);
                }

                return _filterCommand;
            }
        }

       public  IEnumerable<INavigable> FiltredItems
        {
            get
            {
                if (Filtred)
                    return _filtredItems;
                return base.Items;
            }
        }

       
        private bool CanFilter(string s)
        {
            return Items.Count > 0;
        }


        private IEnumerable<INavigable> _filtredItems;
        private bool _filtred = false;
        
        protected virtual void Filter(string s)
        {
            if (s == string.Empty)
            {
                _filtredItems = Items;
                Filtred = false;
               
                OnPropertyChanged(()=>FiltredItems);
                OnPropertyChanged(()=>Filtred);
                Change = !Change;
                OnPropertyChanged(()=>Change);
                return;
            }


            _filtredItems = new List<INavigable>();

            foreach (INavigable navigable in Items)
            {
                navigable.Filter(ref _filtredItems, s);
            }
            Filtred = true;
           
            OnPropertyChanged( ()=> FiltredItems);
            OnPropertyChanged(() => Filtred);
            Change = !Change;
            OnPropertyChanged(() => Change);
        }

        /// <summary>
        /// Gets or sets the text for the Add button.
        /// </summary>
        public bool Filtred
        {
            get { return _filtred; }
            set
            {

                _filtred = value;
            }
        }
        protected virtual INavigable Parent { get { return null; } }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            if (StatusBarServices.StatusText == Resources.Ready)
                StatusBarServices.SignalLoading();
            base.OnAddedItem(sender, e);
            if (Parent != null)
            {
                Parent.IsExpanded = true;
                //Parent.DoNotify();
            }
        }

        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                if (StatusBarServices.StatusText == Resources.Ready)
                    StatusBarServices.SignalLoading();
                Parent.IsExpanded = true;
                Parent.DoNotify();
            }
        }


        public override void Load()
        {
            //if(StatusBarServices.StatusText== Resources.Ready)
            //StatusBarServices.SignalLoading();
            base.Load();
            OnPropertyChanged(()=>FiltredItems);
        }
        public override bool CanDelete(TPresenter presenter)
        {
            return true;
        }
    }
}
