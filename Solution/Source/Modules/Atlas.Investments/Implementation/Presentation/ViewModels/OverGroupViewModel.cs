using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class OverGroupViewModel : PriceSystemGroupViewModel<IOverGroup,IOverGroupPresenter,IOverGroupManagerApplicationServices>, IOverGroupViewModel
    {
        public IPriceSystemPresenter PriceSystem { get; set; }

       

        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (IOverGroupPresenter presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (IOverGroup item in GetItems(services))
                {
                    IOverGroupPresenter presenter = CreatePresenterFor(item);

                    presenter.PriceSystem = PriceSystem;


                    presenter.PropertyChanged += OnPresenterPropertyChanged;


                    Items.Add(presenter);
                }
            });
        }

        protected override IOverGroupManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.PriceSystem = PriceSystem.Object;
            return service;
        }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
          //  PriceSystem.Notify();
        }

        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            //PriceSystem.Notify();
        }

        protected override IOverGroupPresenter CreatePresenterFor(IOverGroup item)
        {
            var over = base.CreatePresenterFor(item);
            over.PriceSystem = PriceSystem;
            return over;
        }

        
    }
}
