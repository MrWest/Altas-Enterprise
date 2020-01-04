using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    //class VariantLinesHolderViewModel : CrudViewModelBase<IVariantLinesHolder, IVariantLinesHolderPresenter, IVariantLinesHolderManagerApplicationServices>, IVariantLinesHolderViewModel
    //{

    //    public void Load()
    //    {
    //        base.Load();

    //        // Delete(Dossificator);
    //        if (Items.Count == 0)
    //        {

    //            var variantlineHolder = ServiceLocator.Current.GetInstance<IVariantLinesHolder>();

    //            Add(CreatePresenterFor(variantlineHolder));
    //            base.Load();
    //        }
    //    }

    //    public IVariantLinesHolderPresenter VariantLinesHolder
    //    {
    //        get { return Items[0]; }
    //    }

    //}
}
