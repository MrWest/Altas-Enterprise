using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    public class CommonViewModel<TEntity, TPresenter> : CrudViewModelBase<TEntity, TPresenter, ICommonManagerApplicationServices<TEntity>>, ICommonViewModel<TEntity, TPresenter>
        where TEntity : class , IEntity
        where TPresenter : class , IPresenter<TEntity>
    {
        private static ICommonViewModel<TEntity, TPresenter> _commonProvider;


        ///// <summary>
        /////     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        /////     containing the <see cref="IWageScale" /> domain entities in the system.
        ///// </summary>
        public static IEnumerable<IPresenter> Presenters
        {
            get
            {
                if (_commonProvider == null)
                {
                    _commonProvider = ServiceLocator.Current.GetInstance<ICommonViewModel<TEntity,TPresenter>>();

                    _commonProvider.Load();
                }




                return _commonProvider.Items;
            }
        }

       

        ///// <summary>
        /////     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        /////     containing the <see cref="IWageScale" /> domain entities in the system.
        ///// </summary>
        //IEnumerable<IPresenter> Presenters
        //{
        //    get
        //    {
        //        Load();
        //        var crap = this;
        //        return Items;
        //    }
        //}
        public override bool CanAdd(TPresenter presenter)
        {
            return true;
        }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
           /// Load();
            base.OnAddedItem(sender, e);
            //Load();
            OnPropertyChanged(() => Items);
            OnPropertyChanged("Items");
         //   OnPropertyChanged(() => Presenters);
            OnPropertyChanged("Presenters");
        }

        public ICommand DeleteMySelfCommand { get { return DeleteCommand; }}

        IEnumerable<IPresenter> ICommonViewModel<TEntity, TPresenter>.Presenters
        {
            get { return Presenters; }
        }
    }
}
