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
    public class RegularGroupViewModel : PriceSystemGroupViewModel<IRegularGroup,IRegularGroupPresenter,IRegularGroupManagerApplicationServices>, IRegularGroupViewModel
    {
        public IOverGroupPresenter OverGroup { get; set; }

        protected override INavigable Parent { get { return OverGroup; } }
        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (IRegularGroupPresenter presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (IRegularGroup item in GetItems(services))
                {
                    IRegularGroupPresenter presenter = CreatePresenterFor(item);

                    presenter.OverGroup = OverGroup;


                    presenter.PropertyChanged += OnPresenterPropertyChanged;

                    Items.Add(presenter);
                }
            });
        }

        protected override IRegularGroupManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OverGroup = OverGroup.Object;
            return service;
        }

        //protected override void OnAddedItem(object sender, EventArgs e)
        //{
        //    base.OnAddedItem(sender, e);
        //    OverGroup.Notify();
        //}

        //protected override void OnDeletedItem(object sender, EventArgs e)
        //{
        //    base.OnDeletedItem(sender, e);
        //    OverGroup.Notify();
        //}

        protected override IRegularGroupPresenter CreatePresenterFor(IRegularGroup item)
        {
            var regular = base.CreatePresenterFor(item);
            regular.OverGroup = OverGroup;
            return regular;
        }

       
    }
}
