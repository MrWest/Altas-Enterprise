using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class InvestmentDocumentPresenter : DocumentPresenter<IInvestmentDocument, IInvestmentDocumentManagerApplicationServices>, IInvestmentDocumentPresenter
    {
        public InvestmentDocumentPresenter(IInvestmentDocument nomenclator) : base(nomenclator)
        {
        }

      
        public DocumentType DocumentType
        {
            get { return Object.DocumentType; }
            set
            {
                SetProperty(v => Object.DocumentType = v, value);
                OnPropertyChanged(() => DocumentType);
            }
        }

      
        public string Institution
        {
            get { return Object.Institution; }
            set
            {
                SetProperty(v => Object.Institution = v, value);
                OnPropertyChanged(() => Institution);
            }
        }

        /// <summary>
        /// Get or sets the Osde of the current <see cref="IInvestmentPresenter"/>.
        /// </summary>
        public IOsdePresenter Osde
        {

            get
            {

                return Object.Osde != null ? ServiceLocator.Current.GetInstance<IOsdeProvider>().Osdes.SingleOrDefault(x => x.Id.ToString() == Object.Osde.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Osde = v.Id, value);
                    OnPropertyChanged(() => Osde);
                }
            }
        }
        /// <summary>
        /// Get or sets the Oace of the current <see cref="IInvestmentPresenter"/>.
        /// </summary>
        public IOacePresenter Oace
        {

            get
            {

                return Object.Oace != null ? ServiceLocator.Current.GetInstance<IOaceProvider>().Oaces.SingleOrDefault(x => x.Id.ToString() == Object.Oace.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Oace = v.Id, value);
                    OnPropertyChanged(() => Oace);
                }
            }
        }

        public DateTime RecieveDate
        {
            get { return Object.RecieveDate; }
            set
            {
                SetProperty(v => Object.RecieveDate = v, value);
                OnPropertyChanged(() => RecieveDate);
            }
        }

        public DateTime DeliverDate
        {
            get { return Object.DeliverDate; }
            set
            {
                SetProperty(v => Object.DeliverDate = v, value);
                OnPropertyChanged(() => DeliverDate);
            }
        }

       

      
    }
}
