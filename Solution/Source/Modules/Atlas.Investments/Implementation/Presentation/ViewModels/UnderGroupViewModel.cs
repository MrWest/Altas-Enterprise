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
    public class UnderGroupViewModel : PriceSystemGroupViewModel<IUnderGroup,IUnderGroupPresenter,IUnderGroupManagerApplicationServices>, IUnderGroupViewModel
    {
        public IRegularGroupPresenter RegularGroup { get; set; }
        protected override INavigable Parent { get { return RegularGroup; } }
        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (IUnderGroupPresenter presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (IUnderGroup item in GetItems(services))
                {
                    IUnderGroupPresenter presenter = CreatePresenterFor(item);

                    presenter.RegularGroup = RegularGroup;


                    presenter.PropertyChanged += OnPresenterPropertyChanged;


                    Items.Add(presenter);
                }
            });
        }

        protected override IUnderGroupManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.RegularGroup = RegularGroup.Object;
            return service;
        }

        //protected override void OnAddedItem(object sender, EventArgs e)
        //{
        //    base.OnAddedItem(sender, e);
        //    //RegularGroup.Notify();
        //}

        //protected override void OnDeletedItem(object sender, EventArgs e)
        //{
        //    base.OnDeletedItem(sender, e);
        //    //RegularGroup.Notify();
        //}
        protected override IUnderGroupPresenter CreatePresenterFor(IUnderGroup item)
        {
            var under = base.CreatePresenterFor(item);
            under.RegularGroup = RegularGroup;
            return under;
        }

      
    }
}
