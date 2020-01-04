using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>
    /// implements presentation for a DossificatorPresenter
    /// </summary>
    //public class DossificatorPresenter : InvestmentElementPresenterBase<IDossificator, IDossificatorApplicationService>, IDossificatorPresenter
    //{
    //    private IInvestmentComponentViewModel _elements;


    //    //public DossificatorPresenter(IDossificator investmentElement) : base(investmentElement)
    //    //{
    //    //}

    //    ///// <summary>
    //    /////     Gets or sets the crud view model handling the investment components presenters being children of the current investment element presenter.
    //    ///// </summary>
    //    //public IInvestmentComponentViewModel Elements
    //    //{
    //    //    get
    //    //    {
    //    //        if (_elements == null)
    //    //        {
    //    //            _elements = ServiceLocator.Current.GetInstance<IInvestmentComponentViewModel>();
    //    //            _elements.InvestmentElement = this;
    //    //            _elements.Load();
    //    //        }

    //    //        return _elements;
    //    //    }
    //    //}

       
    //}
}
