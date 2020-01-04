using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    [Obsolete]
    public class UnderGroupResourcePresenter: UnderGroupItemPresenter<IUnderGroupResource, IUnderGroupResourcesManagerApplicationServices>, IUnderGroupResourcePresenter
    {
        protected override IUnderGroupResourcesManagerApplicationServices CreateServices()
        {
            var services = base.CreateServices();
            services.UnderGroup = UnderGroup.Object;
            return services;
        }
        /// <summary>
        /// Gets or sets the Norm for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public decimal Norm
        {
            get
            {
                return Object.Norm;
            }
            set
            {
                SetProperty(v => Object.Norm = v, value);
                NotifyDown();
                NotifyUp();
            }
        }

        public IWeightPresenter Weight { get; set; }
        public IVolumePresenter Volume { get; set; }

        public ResourceKind ResourceKind
        {
            get { return Object.ResourceKind; }
            set
            {
                SetProperty(v => Object.ResourceKind = v, value);
                OnPropertyChanged(() => ResourceKind);

            }
        }

        public decimal WasteCoefficient
        {
            get
            {
                return Object.WasteCoefficient;
            }
            set
            {
                SetProperty(v => Object.WasteCoefficient = v, value);

            }

        }

        public IWageScalePresenter WageScale
        {
            get
            {

                return Object.WageScale != null ? ServiceLocator.Current.GetInstance<IWageScaleProvider>().WageScales.SingleOrDefault(x => x.Id.ToString() == Object.WageScale.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.WageScale = v.Id, value);
                    OnPropertyChanged(() => WageScale);
                    OnPropertyChanged(() => UnitaryCost);
                    OnPropertyChanged(() => Cost);
                }
            }
        }

        public int MenNumber
        {
            get
            {
                return Object.MenNumber;
            }
            set
            {
                SetProperty(v => Object.MenNumber = v, value);
                OnPropertyChanged(() => UnitaryCost);
                OnPropertyChanged(() => Cost);
            }

        }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "Resource"; }
        }
        public virtual string DeleteText
        {
            get { return Resources.DeleteResourse; }
        }
        public override void NotifyUp()
        {
            OnPropertyChanged(() => Quantity);
            OnPropertyChanged(() => UnitaryCost);
            OnPropertyChanged(() => Cost);

            UnderGroup.DoNotify();
        }

        public DateTime StartDate()
        {
            throw new NotImplementedException();
        }

        public DateTime FinishDate()
        {
            throw new NotImplementedException();
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
    }
}