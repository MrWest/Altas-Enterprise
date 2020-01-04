using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public abstract class PriceSystemGroupPresenter<TGroup,TService> : NavigableNomenclator<TGroup, TService>,IPriceSystemGroupPresenter<TGroup>
        where TGroup : class, IPriceSystemGroup
        where TService : class, IPriceSystemGroupManagerApplicationServices<TGroup>
    {
        //public PriceSystemGroupPresenter(TGroup nomenclator) : base(nomenclator)
        //{
        //}

        /// <summary>
        ///     Gets or sets the code of the underlying nomenclator.
        /// </summary>
        public string Code
        {
            get { return Object.Code; }
            set { SetProperty(v => Object.Code = v, value); }
        }

        public override decimal Cost { get { return 0; } }
        //public abstract bool HasItems { get; }
        //public abstract Thickness DeepThickness { get;  }
        //public abstract string ItemKind { get;}

        //private bool _isExpanded;
        //public bool IsExpanded{ get { return _isExpanded; }
        //    set
        //    {
        //        _isExpanded = value; 
        //        OnPropertyChanged(()=>IsExpanded);
        //        OnPropertyChanged(() => IconData);

        //    } }

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override string IconData
        {
            get
            {
                if (HasItems)
                {
                    if (IsExpanded)
                        return "F1 M 0,12 0,8 20,8 20,12 Z ";

                    return "F1 M 0,12 0,8 8,8 8,0 12,0 12,8 20,8 20,12 12,12 12,20 8,20 8,12 Z ";
                }

                return "";
            }

        }

       


        //public abstract ICommand AddNewItem { get; }
        //public abstract ICommand DeleteMySelf { get; }

        ///// <summary>
        ///// Invoked when the current view's datacontext has requested an interaction with the current view.
        ///// </summary>
        ///// <param name="sender">The object sending the event invoking this method.</param>
        ///// <param name="e">The event arguments containing the details about the interaction request.</param>
        //protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        //{
        //    this.Execute(e);
        //}

        //public object DataContext { get; set; }
    }
}
