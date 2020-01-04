using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IPriceSystemGroupViewModel<TGroup,TPresenter> : INavigableViewModel<TGroup, TPresenter>
        where TGroup:class,IPriceSystemGroup
        where TPresenter : class, IPriceSystemGroupPresenter<TGroup>
    {
        void AddFromScratch(string code, string name);
        TPresenter CreatePresenter( TGroup group);
    }
}
