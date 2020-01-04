using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{

    public abstract class PriceSystemGroupViewModel<TGroup, TPresenter, TService> : NavigableViewModel<TGroup, TPresenter, TService>, IPriceSystemGroupViewModel<TGroup,TPresenter> 
        where TGroup : class, IPriceSystemGroup
        where TPresenter : class, IPriceSystemGroupPresenter<TGroup>
        where TService : class, IPriceSystemGroupManagerApplicationServices<TGroup>
    {

        //protected override void OnAddedItem(object sender, EventArgs e)
        //{
        //    base.OnAddedItem(sender, e);
        //    ItemEventArgs<TPresenter> arguments;
        //    if (!CheckIsItemEventArgs(e, out arguments))
        //        return;
        //    Load();
        //}

        //protected override void OnDeletedItem(object sender, EventArgs e)
        //{
        //    base.OnDeletedItem(sender, e);
        //    ItemEventArgs<TPresenter> arguments;
        //    if (!CheckIsItemEventArgs(e, out arguments))
        //        return;
        //    Load();
        //}

        public override bool CanDelete(TPresenter presenter)
        {
            return true;
        }

        public void AddFromScratch(string code, string name)
        {
            ExecuteUsingServices(services =>
            {
               var group = services.AddFromScratch(code, name);
               
                Items.Add(CreatePresenterFor(group));

            });
            
           
        }

        public TPresenter CreatePresenter(TGroup @group)
        {
            return CreatePresenterFor(group);
        }
    }
}
