using System.Windows.Input;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Investments.Application.Reporting;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class InvestmentChildCustomReportSettingsPresenter: ChildCustomReportSettingsPresenter<IInvestmentChildCustomReportSettings, IInvestmentChildCustomReportSettingsApplicationServices, IInvestmentChildCustomReportSettings>, IInvestmentChildCustomReportSettingsPresenter
    {
        private IInvestmentChildCustomReportSettingsViewModel _childCustomReports;
        public override IChildCustomReportSettingsViewModel ChildReports
        {
            get
            {
                if (_childCustomReports == null)
                {
                    _childCustomReports =
                        ServiceLocator.Current.GetInstance<IInvestmentChildCustomReportSettingsViewModel>();
                    _childCustomReports.ParentReport = this;
                    _childCustomReports.Load();

                    _childCustomReports.Raised += OnInteractionRequested;
                }

                return _childCustomReports;
            }
        }

        public override ICrudViewModel Items
        {
            get { return ChildReports; }
        }


        public override INavigable Parent
        {
            get { return ParentReport;  }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>

        public override string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 38 ? Name.Substring(0, 38) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string EvenShortterName
        {
            get { return Name != null ? (Name.Length > 5 ? Name.Substring(0, 5) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => EvenShortterName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string LimitedName
        {
            get { return Name != null ? (Name.Length > 50 ? Name.Substring(0, 50) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
            }
        }

        public bool ShowInvestmentElements { get { return Object.ShowInvestmentElements; } set { SetProperty(v => Object.ShowInvestmentElements = v, value); NotifyAll(); } }
        public bool ShowBudgetComponents { get { return Object.ShowBudgetComponents; } set { SetProperty(v => Object.ShowBudgetComponents = v, value); NotifyAll(); } }
        public bool ShowSubSpecialities { get { return Object.ShowSubSpecialities; } set { SetProperty(v => Object.ShowSubSpecialities = v, value); NotifyAll(); } }
        public bool ShowActivities { get { return Object.ShowActivities; } set { SetProperty(v => Object.ShowActivities = v, value); NotifyAll(); } }
        public bool ShowResources { get { return Object.ShowResources; } set { SetProperty(v => Object.ShowResources = v, value); NotifyAll(); } }
        public bool ShowEquipment { get { return Object.ShowEquipment; } set { SetProperty(v => Object.ShowEquipment = v, value); NotifyAll(); } }
        public bool ShowConstruction { get { return Object.ShowConstruction; } set { SetProperty(v => Object.ShowConstruction = v, value); NotifyAll(); } }
        public bool ShowOthers { get { return Object.ShowOthers; } set { SetProperty(v => Object.ShowOthers = v, value); NotifyAll(); } }
        public bool ShowWorkCapital { get { return Object.ShowWorkCapital; } set { SetProperty(v => Object.ShowWorkCapital = v, value); NotifyAll(); } }
        public bool ShowMU { get { return Object.ShowMU; } set { SetProperty(v => Object.ShowMU = v, value); NotifyAll(); } }
        public bool ShowQuantity { get { return Object.ShowQuantity; } set { SetProperty(v => Object.ShowQuantity = v, value); NotifyAll(); } }
        public bool ShowCurrency { get { return Object.ShowCurrency; } set { SetProperty(v => Object.ShowCurrency = v, value); NotifyAll(); } }
        public bool ShowUC { get { return Object.ShowUC; } set { SetProperty(v => Object.ShowUC = v, value); NotifyAll(); } }
        public bool ShowCost { get { return Object.ShowCost; } set { SetProperty(v => Object.ShowCost = v, value); NotifyAll(); } }

        private void NotifyAll()
        {

            OnPropertyChanged(() => ShowInvestmentElements);
            OnPropertyChanged(() => ShowBudgetComponents);
            OnPropertyChanged(() => ShowSubSpecialities);
            OnPropertyChanged(() => ShowSubExpeseConcepts);
            OnPropertyChanged(() => ShowCategories);
            OnPropertyChanged(() => ShowActivities);
            OnPropertyChanged(() => ShowResources);
            OnPropertyChanged(() => ShowEquipment);
            OnPropertyChanged(() => ShowConstruction);
            OnPropertyChanged(() => ShowOthers);
            OnPropertyChanged(() => ShowWorkCapital);
            OnPropertyChanged(() => ShowMU);
            OnPropertyChanged(() => ShowQuantity);
            OnPropertyChanged(() => ShowCurrency);
            OnPropertyChanged(() => ShowUC);
            OnPropertyChanged(() => ShowCost);
        }

        public bool ShowSubExpeseConcepts { get { return Object.ShowSubExpeseConcepts; } set { SetProperty(v => Object.ShowSubExpeseConcepts = v, value); NotifyAll(); } }

        public bool ShowCategories { get { return Object.ShowCategories; } set { SetProperty(v => Object.ShowCategories = v, value); NotifyAll(); } }
    }
    public class InvestmentMainCustomReportSettingsPresenter : CustomReportSettingsPresenter<IInvestmentMainCustomReportSettings, IInvestmentMainCustomReportSettingsApplicationServices>, IInvestmentMainCustomReportSettingsPresenter
    {
        private IInvestmentChildMainCustomReportSettingsViewModel _childCustomReports;
        public override IChildCustomReportSettingsViewModel ChildReports
        {
            get
            {
                if (_childCustomReports == null)
                {
                    _childCustomReports =
                        ServiceLocator.Current.GetInstance<IInvestmentChildMainCustomReportSettingsViewModel>();
                    _childCustomReports.ParentReport = this;
                    _childCustomReports.Load();

                    _childCustomReports.Raised += OnInteractionRequested;
                }

                return _childCustomReports;
            }
        }

        private IInvestmentMainCustomReportSettingsViewModel _viewmodel;
        public override ICommand DeleteMySelfCommand
        {
            get
            {
                if (_viewmodel == null)
                {
                    _viewmodel = ServiceLocator.Current.GetInstance<IInvestmentMainCustomReportSettingsViewModel>();
                    _viewmodel.Raised += OnInteractionRequested;
                }
                

                return _viewmodel.DeleteCommand;
            }
        }

        public override ICrudViewModel Items
        {
            get { return ChildReports; }
        }

        
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>

        public override string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 38 ? Name.Substring(0, 38) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string EvenShortterName
        {
            get { return Name != null ? (Name.Length > 5 ? Name.Substring(0, 5) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => EvenShortterName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string LimitedName
        {
            get { return Name != null ? (Name.Length > 50 ? Name.Substring(0, 50) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
            }
        }

        public bool ShowInvestmentElements { get { return Object.ShowInvestmentElements; } set { SetProperty(v => Object.ShowInvestmentElements = v, value); NotifyAll(); } }
        public bool ShowBudgetComponents { get { return Object.ShowBudgetComponents; } set { SetProperty(v => Object.ShowBudgetComponents = v, value); NotifyAll(); } }
        public bool ShowSubSpecialities { get { return Object.ShowSubSpecialities; } set { SetProperty(v => Object.ShowSubSpecialities = v, value); NotifyAll(); } }
        public bool ShowActivities { get { return Object.ShowActivities; } set { SetProperty(v => Object.ShowActivities = v, value); NotifyAll(); } }
        public bool ShowResources { get { return Object.ShowResources; } set { SetProperty(v => Object.ShowResources = v, value); NotifyAll(); } }
        public bool ShowEquipment { get { return Object.ShowEquipment; } set { SetProperty(v => Object.ShowEquipment = v, value); NotifyAll(); } }
        public bool ShowConstruction { get { return Object.ShowConstruction; } set { SetProperty(v => Object.ShowConstruction = v, value); NotifyAll(); } }
        public bool ShowOthers { get { return Object.ShowOthers; } set { SetProperty(v => Object.ShowOthers = v, value); NotifyAll(); } }
        public bool ShowWorkCapital { get { return Object.ShowWorkCapital; } set { SetProperty(v => Object.ShowWorkCapital = v, value); NotifyAll(); } }
        public bool ShowMU { get { return Object.ShowMU; } set { SetProperty(v => Object.ShowMU = v, value); NotifyAll(); } }
        public bool ShowQuantity { get { return Object.ShowQuantity; } set { SetProperty(v => Object.ShowQuantity = v, value); NotifyAll(); } }
        public bool ShowCurrency { get { return Object.ShowCurrency; } set { SetProperty(v => Object.ShowCurrency = v, value); NotifyAll(); } }
        public bool ShowUC { get { return Object.ShowUC; } set { SetProperty(v => Object.ShowUC = v, value); NotifyAll(); } }
        public bool ShowCost { get { return Object.ShowCost; } set { SetProperty(v => Object.ShowCost = v, value); NotifyAll(); } }

        private void NotifyAll()
        {
            
            OnPropertyChanged(()=> ShowInvestmentElements);
            OnPropertyChanged(()=> ShowBudgetComponents);
            OnPropertyChanged(()=> ShowSubSpecialities);
            OnPropertyChanged(() => ShowSubExpeseConcepts);
            OnPropertyChanged(() => ShowCategories);
            OnPropertyChanged(()=> ShowActivities);
            OnPropertyChanged(()=> ShowResources);
            OnPropertyChanged(()=> ShowEquipment);
            OnPropertyChanged(()=> ShowConstruction);
            OnPropertyChanged(()=> ShowOthers);
            OnPropertyChanged(()=> ShowWorkCapital);
            OnPropertyChanged(()=> ShowMU);
            OnPropertyChanged(()=> ShowQuantity);
            OnPropertyChanged(()=> ShowCurrency);
            OnPropertyChanged(()=> ShowUC);
            OnPropertyChanged(()=> ShowCost);
    }

        public bool ShowSubExpeseConcepts { get { return Object.ShowSubExpeseConcepts; } set { SetProperty(v => Object.ShowSubExpeseConcepts = v, value); NotifyAll(); } }

        public bool ShowCategories { get { return Object.ShowCategories; } set { SetProperty(v => Object.ShowCategories = v, value); NotifyAll(); } }

    }
    public class InvestmentChildMainCustomReportSettingsPresenter : ChildCustomReportSettingsPresenter<IInvestmentChildCustomReportSettings, IInvestmentChildCustomReportSettingsApplicationServices, IInvestmentMainCustomReportSettings>, IInvestmentChildMainCustomReportSettingsPresenter
    {
        private IInvestmentChildCustomReportSettingsViewModel _childCustomReports;
        public override IChildCustomReportSettingsViewModel ChildReports
        {
            get
            {
                if (_childCustomReports == null)
                {
                    _childCustomReports =
                        ServiceLocator.Current.GetInstance<IInvestmentChildCustomReportSettingsViewModel>();
                    _childCustomReports.ParentReport = this;
                    _childCustomReports.Load();

                    _childCustomReports.Raised += OnInteractionRequested;
                }

                return _childCustomReports;
            }
        }

        public override ICrudViewModel Items
        {
            get { return ChildReports; }
        }

        public override INavigable Parent
        {
            get { return ParentReport; }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>

        public override string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => Name);
            }
        }

        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 38 ? Name.Substring(0, 38) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string EvenShortterName
        {
            get { return Name != null ? (Name.Length > 5 ? Name.Substring(0, 5) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => EvenShortterName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string LimitedName
        {
            get { return Name != null ? (Name.Length > 50 ? Name.Substring(0, 50) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
            }
        }

        public bool ShowInvestmentElements { get { return Object.ShowInvestmentElements; } set { SetProperty(v => Object.ShowInvestmentElements = v, value); NotifyAll(); } }
        public bool ShowBudgetComponents { get { return Object.ShowBudgetComponents; } set { SetProperty(v => Object.ShowBudgetComponents = v, value); NotifyAll(); } }
        public bool ShowSubSpecialities { get { return Object.ShowSubSpecialities; } set { SetProperty(v => Object.ShowSubSpecialities = v, value); NotifyAll(); } }
        public bool ShowActivities { get { return Object.ShowActivities; } set { SetProperty(v => Object.ShowActivities = v, value); NotifyAll(); } }
        public bool ShowResources { get { return Object.ShowResources; } set { SetProperty(v => Object.ShowResources = v, value); NotifyAll(); } }
        public bool ShowEquipment { get { return Object.ShowEquipment; } set { SetProperty(v => Object.ShowEquipment = v, value); NotifyAll(); } }
        public bool ShowConstruction { get { return Object.ShowConstruction; } set { SetProperty(v => Object.ShowConstruction = v, value); NotifyAll(); } }
        public bool ShowOthers { get { return Object.ShowOthers; } set { SetProperty(v => Object.ShowOthers = v, value); NotifyAll(); } }
        public bool ShowWorkCapital { get { return Object.ShowWorkCapital; } set { SetProperty(v => Object.ShowWorkCapital = v, value); NotifyAll(); } }
        public bool ShowMU { get { return Object.ShowMU; } set { SetProperty(v => Object.ShowMU = v, value); NotifyAll(); } }
        public bool ShowQuantity { get { return Object.ShowQuantity; } set { SetProperty(v => Object.ShowQuantity = v, value); NotifyAll(); } }
        public bool ShowCurrency { get { return Object.ShowCurrency; } set { SetProperty(v => Object.ShowCurrency = v, value); NotifyAll(); } }
        public bool ShowUC { get { return Object.ShowUC; } set { SetProperty(v => Object.ShowUC = v, value); NotifyAll(); } }
        public bool ShowCost { get { return Object.ShowCost; } set { SetProperty(v => Object.ShowCost = v, value); NotifyAll(); } }

        private void NotifyAll()
        {

            OnPropertyChanged(() => ShowInvestmentElements);
            OnPropertyChanged(() => ShowBudgetComponents);
            OnPropertyChanged(() => ShowSubSpecialities);
            OnPropertyChanged(() => ShowSubExpeseConcepts);
            OnPropertyChanged(() => ShowCategories);
            OnPropertyChanged(() => ShowActivities);
            OnPropertyChanged(() => ShowResources);
            OnPropertyChanged(() => ShowEquipment);
            OnPropertyChanged(() => ShowConstruction);
            OnPropertyChanged(() => ShowOthers);
            OnPropertyChanged(() => ShowWorkCapital);
            OnPropertyChanged(() => ShowMU);
            OnPropertyChanged(() => ShowQuantity);
            OnPropertyChanged(() => ShowCurrency);
            OnPropertyChanged(() => ShowUC);
            OnPropertyChanged(() => ShowCost);
        }

        public bool ShowSubExpeseConcepts { get { return Object.ShowSubExpeseConcepts; } set { SetProperty(v => Object.ShowSubExpeseConcepts = v, value); NotifyAll(); } }

        public bool ShowCategories { get { return Object.ShowCategories; } set { SetProperty(v => Object.ShowCategories = v, value); NotifyAll(); } }

    }


    public class InvestmentChildMainCustomReportSettingsViewModel : ChildCustomReportSettingsViewModel<IInvestmentChildCustomReportSettings, IInvestmentChildMainCustomReportSettingsPresenter, IInvestmentChildCustomReportSettingsApplicationServices, IInvestmentMainCustomReportSettings>, IInvestmentChildMainCustomReportSettingsViewModel
    {

    }

    public class InvestmentChildCustomReportSettingsViewModel : ChildCustomReportSettingsViewModel<IInvestmentChildCustomReportSettings, IInvestmentChildCustomReportSettingsPresenter, IInvestmentChildCustomReportSettingsApplicationServices, IInvestmentChildCustomReportSettings>, IInvestmentChildCustomReportSettingsViewModel
    {

    }
    public class InvestmentMainCustomReportSettingsViewModel : CustomReportSettingsViewModel<IInvestmentMainCustomReportSettings, IInvestmentMainCustomReportSettingsPresenter, IInvestmentMainCustomReportSettingsApplicationServices>, IInvestmentMainCustomReportSettingsViewModel
    {

    }
}