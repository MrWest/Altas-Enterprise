using System;
using System.Reflection;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Implementation.Application.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Modularity;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Application.Reporting;
using CompanyName.Atlas.Investments.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Application;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Application.Reporting;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities.Reporting;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Construction;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Equipment;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Reporting;
using CompanyName.Atlas.Investments.Implementation.Presentation;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Reporting;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting;
using CompanyName.Atlas.Investments.Infrastructure;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data.Reporting;
using CompanyName.Atlas.Investments.Presentation;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Views;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments
{
    /// <summary>
    /// This is the representant of the current subsystem: Investments. Through this class the Atlas suite will detect, load and initialize
    /// such subsystem, to them be able to use its logic.
    /// </summary>
    [Module(ModuleName = SubsystemName)]
    public class InvestmentsSubsystem : SubsystemBase
    {
        /// <summary>
        /// Public name of the Investments subsystem.
        /// </summary>
        public const string SubsystemName = "Atlas.Investments";

        // FIX: Take this to the base class, the SubsystemBase, its repeat here and in Investments.
        private readonly IUnityContainer _container;
        private readonly IVisualResourcesServices _visualResourcesServices;
        private readonly INavigationServices _navigationServices;
        private readonly ILoggerFacade _logger;
        private readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name;


        /// <summary>
        /// Initializes a new instance of the Investments module (representing by the class <see cref="InvestmentsSubsystem"/>) given some services.
        /// </summary>
        /// <param name="container">The unity container to use for dependency injection.</param>
        /// <param name="navigationServices">The services allowing to configure and interact the navigation to the resources of the current subsystem.</param>
        /// <param name="visualResourcesServices">
        /// The visual resource services used to handle the resource dictionaries this subsystem needs to integrate to those in the suite.
        /// </param>
        /// <param name="logger">The facade ysed used to access to the logging features.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="container"/>, <paramref name="visualResourcesServices"/> or <paramref name="logger"/> is null.
        /// </exception>
        public InvestmentsSubsystem(IUnityContainer container, INavigationServices navigationServices, IVisualResourcesServices visualResourcesServices, ILoggerFacade logger)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            if (navigationServices == null)
                throw new ArgumentNullException("navigationServices");
            if (visualResourcesServices == null)
                throw new ArgumentNullException("visualResourcesServices");
            if (logger == null)
                throw new ArgumentNullException("logger");

            _container = container;
            _navigationServices = navigationServices;
            _visualResourcesServices = visualResourcesServices;
            _logger = logger;
        }


        /// <summary>
        /// Initializes the current subsystem.
        /// </summary>
        public override void Initialize()
        {
            _logger.Log("Initializing module: Investments..", Microsoft.Practices.Prism.Logging.Category.Debug, Priority.High);

            MergeResourceDictionaries();
            SetupViewModelMappings();
            SetupDependencies();
            SetupNavigation();

            _logger.Log("Initialized module: Investments", Microsoft.Practices.Prism.Logging.Category.Debug, Priority.Low);
        }

        private void MergeResourceDictionaries()
        {
            _visualResourcesServices.Merge("/{0};component/Presentation/Views/Themes/Light.xaml".EasyFormat(_assemblyName));
        }

        private void SetupViewModelMappings()
        {
            ViewModelLocationProvider.Register(typeof(InvestmentElementsView).FullName, GetInvestmentsMainViewModel);
            //ViewModelLocationProvider.Register(typeof(InvestmentView).FullName, GetInvestmentsMainViewModel2);
            ViewModelLocationProvider.Register(typeof(ModuleAccessView).FullName, GetModuleAccessViewModel);
            ViewModelLocationProvider.Register(typeof(ModuleSubjectView).FullName, GetAtlasModuleMainSubjectViewModel);
            ViewModelLocationProvider.Register(typeof(OaceEditor).FullName, GetOaceViewModel);
            ViewModelLocationProvider.Register(typeof(OsdeEditor).FullName, GetOsdeViewModel);
            ViewModelLocationProvider.Register(typeof(PhaseEditor).FullName, GetPhaseViewModel);
            ViewModelLocationProvider.Register(typeof(CategoryEditor).FullName, GetCategoryViewModel);
            ViewModelLocationProvider.Register(typeof(InvestmentTypeEditor).FullName, GetInvestmentTypeViewModel);
            ViewModelLocationProvider.Register(typeof(ExpenseConceptEditor).FullName, GetExpenseConceptViewModel);
            ViewModelLocationProvider.Register(typeof(SpecialityEditor).FullName, GetSpecialityViewModel);
            ViewModelLocationProvider.Register(typeof(WageScaleEditor).FullName, GetWageScaleViewModel);
            ViewModelLocationProvider.Register(typeof(WorkForceEditor).FullName, GetWorkForceViewModel);
            ViewModelLocationProvider.Register(typeof(CustomReportTreeViewContainer).FullName, GetInvestmentMainCustomReportSettingsViewModel);
            ViewModelLocationProvider.Register(typeof(MeasurementUnitEditor).FullName, GetMeasurementUnitViewModel);
            ViewModelLocationProvider.Register(typeof(CurrencyEditor).FullName, GetCurrencyViewModel);
            ViewModelLocationProvider.Register(typeof(PriceSystems).FullName, GetPriceSystemViewModel);

            ViewModelLocationProvider.Register(typeof(ActivityExecutorEditor).FullName, GetActivityExecutorViewModel);
            ViewModelLocationProvider.Register(typeof(ResourceProviderEditor).FullName, GetResourceProviderViewModel);
            ViewModelLocationProvider.Register(typeof(ResourceSupplierEditor).FullName, GetResourceSupplierViewModel);
            //ViewModelLocationProvider.Register(typeof(DossificatorView).FullName, GetDossificatorViewModel);
            //ViewModelLocationProvider.Register(typeof(VariantLinesViewer).FullName, GetVariantLinesHolderViewModel);
        }

       
        #region Dependencies configuration

        private void SetupDependencies()
        {
            RegisterDomainEntities();
            RegisterDomainServices();
            RegisterRepositories();
            RegisterApplicationServices();
            RegisterCrudViewModels();
            RegisterPresenterViewModels();
        }

        private void RegisterApplicationServices()
        {
            InjectionMember[] policies = Policies.ItemManagerApplicationServicesPolicies;

           // _container.RegisterType<IInvestmentElementManagerApplicationServices, InvestmentElementManagerApplicationServices>(policies);
            _container.RegisterType<IInvestmentManagerApplicationServices, InvestmentManagerApplicationServices>(policies);
            _container.RegisterType<IInvestmentComponentManagerApplicationServices, InvestmentComponentManagerApplicationServices>(policies);

             _container.RegisterType<IBudgetComponentResourceManagerApplicationServices<IPlannedActivity>, BudgetComponentResourceManagerApplicationService<IPlannedActivity>>(policies);
            _container.RegisterType<IBudgetComponentResourceManagerApplicationServices<IExecutedActivity>, BudgetComponentResourceManagerApplicationService<IExecutedActivity>>(policies);
            _container.RegisterType<IBudgetComponentResourceManagerApplicationServices<IPlannedResource>, BudgetComponentResourceManagerApplicationService<IPlannedResource>>(policies);
            _container.RegisterType<IBudgetComponentResourceManagerApplicationServices<IUnderGroupActivity>, BudgetComponentResourceManagerApplicationService<IUnderGroupActivity>>(policies);
            _container.RegisterType<IBudgetComponentResourceManagerApplicationServices<IUnderGroupResource>, BudgetComponentResourceManagerApplicationService<IUnderGroupResource>>(policies);


            _container.RegisterType<IPlannedActivityManagerApplicationServices, PlannedActivityManagerApplicationServices>(policies);
            _container.RegisterType<IExecutedActivityItemManagerApplicationServices, ExecutedActivityItemManagerApplicationServicesBase>(policies);


            _container.RegisterType<IEquipmentPlannedActivityManagerApplicationServices, EquipmentPlannedActivityManagerApplicationServices>(policies);
          //  _container.RegisterType<IEquipmentExecutedResourceManagerApplicationServices, EquipmentExecutedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IEquipmentExecutedActivityManagerApplicationServices, EquipmentExecutedActivityManagerApplicationServices>(policies);
           
         //   _container.RegisterType<IConstructionPlannedResourceManagerApplicationServices, ConstructionPlannedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IConstructionPlannedActivityManagerApplicationServices, ConstructionPlannedActivityManagerApplicationServices>(policies);
          //  _container.RegisterType<IConstructionExecutedResourceManagerApplicationServices, ConstructionExecutedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IConstructionExecutedActivityManagerApplicationServices, ConstructionExecutedActivityManagerApplicationServices>(policies);

        //    _container.RegisterType<IOtherExpensesPlannedResourceManagerApplicationServices, OtherExpensesPlannedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IOtherExpensesPlannedActivityManagerApplicationServices, OtherExpensesPlannedActivityManagerApplicationServices>(policies);
       //     _container.RegisterType<IOtherExpensesExecutedResourceManagerApplicationServices, OtherExpensesExecutedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IOtherExpensesExecutedActivityManagerApplicationServices, OtherExpensesExecutedActivityManagerApplicationServices>(policies);
           
        //    _container.RegisterType<IWorkCapitalPlannedResourceManagerApplicationServices, WorkCapitalPlannedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IWorkCapitalPlannedActivityManagerApplicationServices, WorkCapitalPlannedActivityManagerApplicationServices>(policies);
       //     _container.RegisterType<IWorkCapitalExecutedResourceManagerApplicationServices, WorkCapitalExecutedResourceManagerApplicationServices>(policies);
            _container.RegisterType<IWorkCapitalExecutedActivityManagerApplicationServices, WorkCapitalExecutedActivityManagerApplicationServices>(policies);


      
            _container
                .RegisterType
                <IEquipmentPlannedActivityManagerApplicationServices,
                    EquipmentPlannedActivityManagerApplicationServices>(policies);
            _container
               .RegisterType
               <IConstructionPlannedActivityManagerApplicationServices,
                   ConstructionPlannedActivityManagerApplicationServices>(policies);

            _container
               .RegisterType
               <IOtherExpensesPlannedActivityManagerApplicationServices,
                   OtherExpensesPlannedActivityManagerApplicationServices>(policies);
            _container
               .RegisterType
               <IWorkCapitalPlannedActivityManagerApplicationServices,
                   WorkCapitalPlannedActivityManagerApplicationServices>(policies);


            _container
               .RegisterType
               <IEquipmentExecutedActivityManagerApplicationServices,
                   EquipmentExecutedActivityManagerApplicationServices>(policies);
            _container
               .RegisterType
               <IConstructionExecutedActivityManagerApplicationServices,
                   ConstructionExecutedActivityManagerApplicationServices>(policies);

            _container
               .RegisterType
               <IOtherExpensesExecutedActivityManagerApplicationServices,
                   OtherExpensesExecutedActivityManagerApplicationServices>(policies);
            _container
               .RegisterType
               <IWorkCapitalExecutedActivityManagerApplicationServices,
                   WorkCapitalExecutedActivityManagerApplicationServices>(policies);

       
            _container
        .RegisterType
        <IUnderGroupActivityManagerApplicationServices,
           UnderGroupActivityManagerApplicationServices>(policies);


            
            _container.RegisterType<IOaceManagerApplicationServices, OaceManagerApplicationServices>(policies);
            _container.RegisterType<IOsdeManagerApplicationServices, OsdeManagerApplicationServices>(policies);
            _container.RegisterType<IPhaseManagerApplicationServices, PhaseManagerApplicationServices>(policies);
            _container.RegisterType<ICategoryManagerApplicationServices, CategoryManagerApplicationServices>(policies);
            _container.RegisterType<IInvestmentTypeManagerApplicationServices, InvestmentTypeManagerApplicationServices>(policies);
            _container.RegisterType<IExpenseConceptManagerApplicationServices, ExpenseConceptManagerApplicationServices>(policies);
            _container.RegisterType<ISubExpenseConceptManagerApplicationServices, SubExpenseConceptManagerApplicationServices>(policies);
            _container.RegisterType<ISpecialityManagerApplicationServices, SpecialityManagerApplicationServices>(policies);
            _container.RegisterType<ISubSpecialityManagerApplicationServices, SubSpecialityManagerApplicationServices>(policies);
            _container.RegisterType<IWageScaleManagerApplicationServices, WageScaleManagerApplicationServices>(policies);
            _container.RegisterType<IWorkForceManagerApplicationServices, WorkForceManagerApplicationServices>(policies);

            _container.RegisterType<IConvertibleEntityManagerApplicationServices<ICurrency>, ConvertibleEntityManagerApplicationServices<ICurrency,ICurrencyDomainService>>(policies);
            _container.RegisterType<IConvertibleEntityManagerApplicationServices<IMeasurementUnit>, ConvertibleEntityManagerApplicationServices<IMeasurementUnit,IMeasurementUnitDomainService>>(policies);
            _container.RegisterType<IMeasurementUnitManagerApplicationServices, MeasurementUnitManagerApplicationServices>(policies);
            _container.RegisterType<ICurrencyManagerApplicationServices, CurrencyManagerApplicationServices>(policies);
            //_container.RegisterType<IInvestmentElementPeriodManagerApplicationServices<IInvestmentElementPeriod>, InvestmentElementPeriodManagerApplicationServices>(policies);

            _container.RegisterType<IPriceSystemManagerApplicationService, PriceSystemManagerApplicationService>(policies);
            _container.RegisterType<ISectionManagerApplicationService, SectionManagerApplicationService>(policies);

            
            _container.RegisterType<IDossificatorApplicationService, DossificatorApplicationServices>();
            //_container.RegisterType<IVariantLinesHolderManagerApplicationServices, VariantLinesHolderManagerApplicationServices>();

            _container.RegisterType<ICashMovementCategoryManagerApplicationServices<ICashMovementCategory>, CashMovementCategoryManagerApplicationServices<ICashMovementCategory>>();
            _container.RegisterType<ICashMovementCategoryManagerApplicationServices<ICashEntry>, CashMovementCategoryManagerApplicationServices<ICashEntry>>();
            _container.RegisterType<ICashMovementCategoryManagerApplicationServices<ICashOutgoing>, CashMovementCategoryManagerApplicationServices<ICashOutgoing>>();

            _container.RegisterType<ICashFlowCashMovementCategoryManagerApplicationServices<ICashEntry>, CashFlowCashMovementCategoryManagerApplicationServices<ICashEntry>>();
            _container.RegisterType<ICashFlowCashMovementCategoryManagerApplicationServices<ICashOutgoing>, CashFlowCashMovementCategoryManagerApplicationServices<ICashOutgoing>>();

            _container.RegisterType<IWorkCapitalCashFlowItemManagerApplicationServices, WorkCapitalCashFlowItemManagerApplicationServices>();

            
            

            _container.RegisterType<ICashMovementManagerApplicationServices, CashMovementManagerApplicationServices>();

            _container.RegisterType<IExecutionManagerApplicationServices, ExecutionManagerApplicationServices>();
            _container.RegisterType<IUnitConverterManagerApplicationServices, UnitConverterManagerApplicationServices>();
            _container.RegisterType<IPeriodManagerApplicationServices, PeriodManagerApplicationServices>();
            _container.RegisterType<IMeasurableUnitManagerApplicationServices<IWeight>, MeasurableUnitManagerApplicationServices<IWeight>>();
            _container.RegisterType<IMeasurableUnitManagerApplicationServices<IVolume>, MeasurableUnitManagerApplicationServices<IVolume>>();

            _container.RegisterType<IOverGroupManagerApplicationServices, OverGroupManagerApplicationServices>();
            _container.RegisterType<IRegularGroupManagerApplicationServices, RegularGroupManagerApplicationServices>();
            _container.RegisterType<IUnderGroupManagerApplicationServices, UnderGroupManagerApplicationServices>();
            _container.RegisterType<IUnderGroupActivityManagerApplicationServices, UnderGroupActivityManagerApplicationServices>();
            _container.RegisterType<IUnderGroupResourcesManagerApplicationServices, UnderGroupResourcesManagerApplicationServices>();



            _container.RegisterType<IEntityProviderManagerApplicationServices<ISubExpenseConcept>, EntityProviderManagerApplicationServices<ISubExpenseConcept>>();
            _container.RegisterType<IEntityProviderManagerApplicationServices<ISubSpeciality>, EntityProviderManagerApplicationServices<ISubSpeciality>>();
            _container.RegisterType<IEntityProviderManagerApplicationServices<IWageScale>, EntityProviderManagerApplicationServices<IWageScale>>();
            _container.RegisterType<IEntityProviderManagerApplicationServices<ICurrency>, EntityProviderManagerApplicationServices<ICurrency>>();
            _container.RegisterType<IEntityProviderManagerApplicationServices<IMeasurementUnit>, EntityProviderManagerApplicationServices<IMeasurementUnit>>();
            _container.RegisterType<IEntityProviderManagerApplicationServices<INomenclator>, EntityProviderManagerApplicationServices<INomenclator>>();
            _container.RegisterType<IInvestmentDocumentManagerApplicationServices, InvestmentDocumentManagerApplicationServices>();

            //new from refactoring subspecialities
            _container.RegisterType<IPlannedSubSpecialityHolderManagerApplicationServices, PlannedSubSpecialityHolderManagerApplicationServices>();
            _container.RegisterType<IExecutedSubSpecialityHolderManagerApplicationServices, ExecutedSubSpecialityHolderManagerApplicationServices>();

            _container.RegisterType<IActivityExecutorManagerApplicationServices, ActivityExecutorManagerApplicationServices>();
            _container.RegisterType<IResourceSupplierManagerApplicationServices, ResourceSupplierManagerApplicationServices>();
            _container.RegisterType<IResourceProviderManagerApplicationServices, ResourceProviderManagerApplicationServices>();

            //reporting
            _container.RegisterType<IInvestmentMainCustomReportSettingsApplicationServices, InvestmentMainCustomReportSettingsApplicationServices>();
            _container.RegisterType<IInvestmentChildCustomReportSettingsApplicationServices, InvestmentChildCustomReportSettingsApplicationServices>();

        }

        private void RegisterRepositories()
        {
            // new ones
            _container.RegisterType<IEntityFrameworkDbContext<Budget>, EntityFrameworkDbContext<Budget>>();
            _container.RegisterType<IEntityFrameworkDbContext<BudgetComponentBase>, EntityFrameworkDbContext<BudgetComponentBase>>();
            _container.RegisterType<IEntityFrameworkDbContext<EquipmentComponent>, EntityFrameworkDbContext<EquipmentComponent>>();
            _container.RegisterType<IEntityFrameworkDbContext<ConstructionComponent>, EntityFrameworkDbContext<ConstructionComponent>>();
            _container.RegisterType<IEntityFrameworkDbContext<OtherExpensesComponent>, EntityFrameworkDbContext<OtherExpensesComponent>>();
            _container.RegisterType<IEntityFrameworkDbContext<WorkCapitalComponent>, EntityFrameworkDbContext<WorkCapitalComponent>>();
            _container.RegisterType<IEntityFrameworkDbContext<WorkCapitalCashFlow>, EntityFrameworkDbContext<WorkCapitalCashFlow>>();
            _container.RegisterType<IEntityFrameworkDbContext<CashEntry>, EntityFrameworkDbContext<CashEntry>>();
            _container.RegisterType<IEntityFrameworkDbContext<CashOutgoing>, EntityFrameworkDbContext<CashOutgoing>>();


            _container.RegisterType<IInvestmentElementRepository, InvestmentElementRepository>();
            _container.RegisterType<IInvestmentRepository, InvestmentRepositoryEF>();
            _container.RegisterType<IInvestmentComponentRepository, InvestmentComponentRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Investment>, EntityFrameworkDbContext<Investment>>();
            _container.RegisterType<IEntityFrameworkDbContext<InvestmentComponent>, EntityFrameworkDbContext<InvestmentComponent>>();


            _container.RegisterType<IPlannedActivityRepository, PlannedActivityRepositoryBaseEF>();
            _container.RegisterType<IEntityFrameworkDbContext<PlannedActivity>, EntityFrameworkDbContext<PlannedActivity>>();

            _container.RegisterType<IExecutedActivityRepository, ExecutedActivityRepositoryBaseEF>();
            _container.RegisterType<IEntityFrameworkDbContext<ExecutedActivity>, EntityFrameworkDbContext<ExecutedActivity>>();

            _container.RegisterType<IEquipmentPlannedActivityRepository, EquipmentPlannedActivityRepository>();
          //  _container.RegisterType<IEquipmentExecutedResourceRepository, EquipmentExecutedResourceRepository>();
            _container.RegisterType<IEquipmentExecutedActivityRepository, EquipmentExecutedActivityRepository>();

           // _container.RegisterType<IConstructionPlannedResourceRepository, ConstructionPlannedResourceRepository>();
            _container.RegisterType<IConstructionPlannedActivityRepository, ConstructionPlannedActivityRepository>();
           // _container.RegisterType<IConstructionExecutedResourceRepository, ConstructionExecutedResourceRepository>();
            _container.RegisterType<IConstructionExecutedActivityRepository, ConstructionExecutedActivityRepository>();

           // _container.RegisterType<IOtherExpensesPlannedResourceRepository, OtherExpensesPlannedResourceRepository>();
            _container.RegisterType<IOtherExpensesPlannedActivityRepository, OtherExpensesPlannedActivityRepository>();
          //  _container.RegisterType<IOtherExpensesExecutedResourceRepository, OtherExpensesExecutedResourceRepository>();
            _container.RegisterType<IOtherExpensesExecutedActivityRepository, OtherExpensesExecutedActivityRepository>();

      //      _container.RegisterType<IWorkCapitalPlannedResourceRepository, WorkCapitalPlannedResourceRepository>();
            _container.RegisterType<IWorkCapitalPlannedActivityRepository, WorkCapitalPlannedActivityRepository>();
     //       _container.RegisterType<IWorkCapitalExecutedResourceRepository, WorkCapitalExecutedResourceRepository>();
            _container.RegisterType<IWorkCapitalExecutedActivityRepository, WorkCapitalExecutedActivityRepository>();


        
            _container.RegisterType<IBudgetComponentResourceRepository<IPlannedActivity>, PlannedResourceRepositoryBaseEF<IPlannedActivity>>();
            _container.RegisterType<IBudgetComponentResourceRepository<IExecutedActivity>, PlannedResourceRepositoryBaseEF<IExecutedActivity>>();
            _container.RegisterType<IBudgetComponentResourceRepository<IPlannedResource>, PlannedResourceRepositoryBaseEF<IPlannedResource>>();
            _container.RegisterType<IBudgetComponentResourceRepository<IUnderGroupActivity>, PlannedResourceRepositoryBaseEF<IUnderGroupActivity>>();
            _container.RegisterType<IBudgetComponentResourceRepository<IUnderGroupResource>, PlannedResourceRepositoryBase<IUnderGroupResource>>();

            _container.RegisterType<IEntityFrameworkDbContext<PlannedResource>, EntityFrameworkDbContext<PlannedResource>>();

            _container.RegisterType<IWorkCapitalCashFlowRepository, WorkCapitalCashFlowRepository>();

            
            //_container.RegisterType<IBudgetComponentResourceRepository<IExecutedActivity>, PlannedResourceRepositoryBase<IExecutedActivity>>();
            //_container.RegisterType<IBudgetComponentResourceRepository<IExecutedActivity>, PlannedResourceRepositoryBase<IExecutedActivity>>();
            //_container
            //    .RegisterType
            //    <IBudgetComponentResourceRepository<IExecutedActivity>, PlannedResourceRepositoryBase<IExecutedActivity>>();




             _container.RegisterType<IConstructionExecutedActivityRepository, ConstructionExecutedActivityRepository>();
             _container.RegisterType<IEquipmentExecutedActivityRepository, EquipmentExecutedActivityRepository>();
             _container.RegisterType<IOtherExpensesExecutedActivityRepository, OtherExpensesExecutedActivityRepository>();
             _container.RegisterType<IWorkCapitalExecutedActivityRepository, WorkCapitalExecutedActivityRepository>();

            _container.RegisterType<IOaceRepository, OaceRepository>();
            _container.RegisterType<IEntityFrameworkDbContext<Oace>, EntityFrameworkDbContext<Oace>>();
            _container.RegisterType<IOsdeRepository, OsdeRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Osde>, EntityFrameworkDbContext<Osde>>();
            _container.RegisterType<IPhaseRepository, PhaseRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Phase>, EntityFrameworkDbContext<Phase>>();
            _container.RegisterType<ICategoryRepository, CategoryRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Implementation.Domain.Entities.Category>, EntityFrameworkDbContext<Implementation.Domain.Entities.Category>>();
            _container.RegisterType<IInvestmentTypeRepository, InvestmentTypeRepository>();
            _container.RegisterType<IExpenseConceptRepository, ExpenseConceptRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<ExpenseConcept>, EntityFrameworkDbContext<ExpenseConcept>>();
            _container.RegisterType<ISubExpenseConceptRepository, SubExpenseConceptRepositoryEF>();
            _container.RegisterType<ICommonDomainService<ISubExpenseConcept>, CommonDomainService<ISubExpenseConcept>>();
            _container.RegisterType<ICommonRepository<ISubExpenseConcept>, CommonRepositoryEF<ISubExpenseConcept, SubExpenseConcept>>();

            _container.RegisterType<IWorkCapitalCashFlowDomainServices, WorkCapitalCashFlowDomainServices>();

           
            _container.RegisterType<IEntityFrameworkDbContext<SubExpenseConcept>, EntityFrameworkDbContext<SubExpenseConcept>>();
            _container.RegisterType<ISpecialityRepository, SpecialityRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Speciality>, EntityFrameworkDbContext<Speciality>>();
            _container.RegisterType<ISubSpecialityRepository, SubSpecialityRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<SubSpeciality>, EntityFrameworkDbContext<SubSpeciality>>();
            _container.RegisterType<IWageScaleRepository, WageScaleRepository>();
            _container.RegisterType<IWorkForceRepository, WorkForceRepository>();

            _container.RegisterType<IConvertibleEntityRepository<IConvertibleEntity>, ConvertibleEntityRepository<IConvertibleEntity>>();
            //_container.RegisterType<IConvertibleEntityRepository<IMeasurementUnit>, ConvertibleEntityRepository<IMeasurementUnit>>();
            //_container.RegisterType<IConvertibleEntityRepository<ICurrency>, ConvertibleEntityRepository<ICurrency>>();
            _container.RegisterType<IConvertibleEntityRepository<IMeasurementUnit>, ConvertibleEntityRepositoryEF<IMeasurementUnit, MeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityRepository<ICurrency>, ConvertibleEntityRepositoryEF<ICurrency, Currency>>();
            _container.RegisterType<IEntityFrameworkDbContext<MeasurementUnit>, EntityFrameworkDbContext<MeasurementUnit>>();
            _container.RegisterType<IEntityFrameworkDbContext<Currency>, EntityFrameworkDbContext<Currency>>();

            _container.RegisterType<IPeriodRepository, PeriodRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Period>, EntityFrameworkDbContext<Period>>();

            //_container.RegisterType<IPeriodRepository<IInvestmentElementPeriod>, PeriodRepository<IInvestmentElementPeriod>>();

            _container.RegisterType<IPriceSystemRepository, PriceSystemRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<PriceSystem>, EntityFrameworkDbContext<PriceSystem>>();
            _container.RegisterType<ISectionRepository, SectionRepository>();
             
            //_container.RegisterType<IBudgetComponentItemRepository<IPlannedActivity, ISection>, ActivityRepositoryBase<ISection>>();
            //_container.RegisterType<IBudgetComponentItemRepository<IPlannedActivity, IVariantLinesHolder>, ActivityRepositoryFullBase<IVariantLinesHolder>>();
            //_container.RegisterType<IBudgetComponentItemRepository<IPlannedResource, IVariantLinesHolder>, BudgetComponentResourceRepositoryBase<IVariantLinesHolder>>();

            _container.RegisterType<IDossificatorRepository, DossificatorRepository>();
            _container.RegisterType<IVariantLinesHolderRepository, VariantLinesHolderRepository>();

            _container.RegisterType<ICashMovementCategoryRepository<ICashMovementCategory>, CashMovementCategoryRepositoryEF<ICashMovementCategory, CashMovementCategory>>();
            _container.RegisterType<IEntityFrameworkDbContext<CashMovementCategory>, EntityFrameworkDbContext<CashMovementCategory>>();
            _container.RegisterType<ICashMovementCategoryRepository<ICashEntry>, CashMovementCategoryRepositoryEF<ICashEntry, CashEntry>>();
            _container.RegisterType<IEntityFrameworkDbContext<CashEntry>, EntityFrameworkDbContext<CashEntry>>();
            _container.RegisterType<ICashMovementCategoryRepository<ICashOutgoing>, CashMovementCategoryRepositoryEF<ICashOutgoing, CashOutgoing>>();
            _container.RegisterType<IEntityFrameworkDbContext<CashOutgoing>, EntityFrameworkDbContext<CashOutgoing>>();

            _container.RegisterType<ICashMovementRepository, CashMovementRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<CashMovement>, EntityFrameworkDbContext<CashMovement>>();

            _container.RegisterType<ICashFlowCashMovementCategoryRepository<ICashEntry>, CashFlowCashMovementCategoryRepository<ICashEntry>>();
            _container.RegisterType<ICashFlowCashMovementCategoryRepository<ICashOutgoing>, CashFlowCashMovementCategoryRepository<ICashOutgoing>>();

            _container.RegisterType<IExecutionRepository, ExecutionRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Execution>, EntityFrameworkDbContext<Execution>>();
            _container.RegisterType<IUnitConverterRepository, UnitConverterRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<UnitConverter>, EntityFrameworkDbContext<UnitConverter>>();

            _container.RegisterType<IMeasurableUnitRepository<IWeight>, MeasurableUnitRepositoryEF<IWeight, Weight>>();
            _container.RegisterType<IEntityFrameworkDbContext<Weight>, EntityFrameworkDbContext<Weight>>();
            _container.RegisterType<IMeasurableUnitRepository<IVolume>, MeasurableUnitRepositoryEF<IVolume, Volume>>();
            _container.RegisterType<IEntityFrameworkDbContext<Volume>, EntityFrameworkDbContext<Volume>>();
            _container.RegisterType<ICommonRepository<IWeight>, CommonRepositoryEF<IWeight, Weight>>();
            _container.RegisterType<ICommonRepository<IVolume>, CommonRepositoryEF<IVolume, Volume>>();

            _container.RegisterType<ICommonRepository<ICurrency>, CommonRepositoryEF<ICurrency, Currency>>();
            _container.RegisterType<ICommonRepository<IMeasurementUnit>, CommonRepositoryEF<IMeasurementUnit, MeasurementUnit>>();
            _container.RegisterType<ICommonRepository<ISubSpeciality>, CommonRepositoryEF<ISubSpeciality, SubSpeciality>>();
            _container.RegisterType<IEntityFrameworkDbContext<SubSpeciality>, EntityFrameworkDbContext<SubSpeciality>>();
            _container.RegisterType<ICommonRepository<ISpeciality>, CommonRepositoryEF<ISpeciality, Speciality>>();
            _container.RegisterType<ICommonRepository<IExpenseConcept>, CommonRepositoryEF<IExpenseConcept, ExpenseConcept>>();
            _container.RegisterType<ICommonRepository<ISubExpenseConcept>, CommonRepositoryEF<ISubExpenseConcept, SubExpenseConcept>>();
            _container.RegisterType<ICommonRepository<IOsde>, CommonRepositoryEF<IOsde, Osde>>();
            _container.RegisterType<ICommonRepository<IOace>, CommonRepositoryEF<IOace, Oace>>();
            _container.RegisterType<ICommonRepository<IPhase>, CommonRepositoryEF<IPhase, Phase>>();
            _container.RegisterType<ICommonRepository<IInvestmentType>, CommonRepositoryEF<IInvestmentType, InvestmentType>>();
            _container.RegisterType<ICommonRepository<ICategory>, CommonRepositoryEF<ICategory, Implementation.Domain.Entities.Category>>();
            _container.RegisterType<ICommonRepository<IPlannedActivity>, CommonRepositoryEF<IPlannedActivity, PlannedActivity>>();
            _container.RegisterType<IEntityFrameworkDbContext<PlannedActivity>, EntityFrameworkDbContext<PlannedActivity>>();
            _container.RegisterType<ICommonRepository<IExecutedActivity>, CommonRepositoryEF<IExecutedActivity, ExecutedActivity>>();
            _container.RegisterType<IEntityFrameworkDbContext<ExecutedActivity>, EntityFrameworkDbContext<ExecutedActivity>>();
            _container.RegisterType<ICommonRepository<IUnderGroupActivity>, CommonRepositoryEF<IUnderGroupActivity, UnderGroupActivity>>();
            _container.RegisterType<IEntityFrameworkDbContext<UnderGroupActivity>, EntityFrameworkDbContext<UnderGroupActivity>>();
            _container.RegisterType<ICommonRepository<IPlannedResource>, CommonRepositoryEF<IPlannedResource, PlannedResource>>();
            _container.RegisterType<IEntityFrameworkDbContext<PlannedResource>, EntityFrameworkDbContext<PlannedResource>>();
            _container.RegisterType<ICommonRepository<ISubjectConceptContent>, CommonRepositoryEF<ISubjectConceptContent, SubjectConceptContent>>();
            _container.RegisterType<IEntityFrameworkDbContext<SubjectConceptContent>, EntityFrameworkDbContext<SubjectConceptContent>>();
            _container.RegisterType<ICommonRepository<ISubjectConceptDefinition>, CommonRepositoryEF<ISubjectConceptDefinition, SubjectConceptDefinition>>();
            _container.RegisterType<IEntityFrameworkDbContext<SubjectConceptDefinition>, EntityFrameworkDbContext<SubjectConceptDefinition>>();
            _container.RegisterType<ICommonRepository<ISubjectConceptExample>, CommonRepositoryEF<ISubjectConceptExample, SubjectConceptExample>>();
            _container.RegisterType<IEntityFrameworkDbContext<SubjectConceptExample>, EntityFrameworkDbContext<SubjectConceptExample>>();
            ////_container.RegisterType<ICommonRepository<ISubExpenseConcept>, CommonRepository<ISubExpenseConcept>>();
            _container.RegisterType<ICommonRepository<IInvestment>, CommonRepositoryEF<IInvestment, Investment>>();
            


            _container.RegisterType<IOverGroupRepository, OverGroupRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<OverGroup>, EntityFrameworkDbContext<OverGroup>>();
            _container.RegisterType<IRegularGroupRepository, RegularGroupRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<RegularGroup>, EntityFrameworkDbContext<RegularGroup>>();
            _container.RegisterType<IUnderGroupRepository, UnderGroupRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<UnderGroup>, EntityFrameworkDbContext<UnderGroup>>();
            _container.RegisterType<IUnderGroupActivityRepository, UnderGroupActivityRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<UnderGroupActivity>, EntityFrameworkDbContext<UnderGroupActivity>>();
            _container.RegisterType<IUnderGroupResourceRepository, UnderGroupResourceRepository>();
            _container.RegisterType<INomenclatorRepository<IPlannedActivity>, NomenclatorRepositoryInheritanceEF<IPlannedActivity, PlannedActivity, UnderGroupActivity>>();
            _container.RegisterType<IEntityFrameworkDbContext<Activity>, EntityFrameworkDbContext<Activity>>();

            _container.RegisterType<IRepository<ISubExpenseConcept>, StandarDb4ORepository<ISubExpenseConcept>>();
            _container.RegisterType<IRepository<ISubSpeciality>, StandarDb4ORepository<ISubSpeciality>>();
            _container.RegisterType<IRepository<IWageScale>, StandarDb4ORepository<IWageScale>>();
            _container.RegisterType<IRepository<ICurrency>, StandarDb4ORepository<ICurrency>>();
            _container.RegisterType<IRepository<IMeasurementUnit>, StandarDb4ORepository<IMeasurementUnit>>();
            _container.RegisterType<IInvestmentDocumentRepository, InvestmentDocumentRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<InvestmentDocument>, EntityFrameworkDbContext<InvestmentDocument>>();

            //new from refactoring subspecialities
            _container.RegisterType<IPlannedSubSpecialityHolderRepository, PlannedSubSpecialityHolderRepositoryEF>();

            _container.RegisterType<IExecutedSubSpecialityHolderRepository, ExecutedSubSpecialityHolderRepositoryEF>();

            _container.RegisterType<IEntityFrameworkDbContext<PlannedSubSpecialityHolder>, EntityFrameworkDbContext<PlannedSubSpecialityHolder>>();
            _container.RegisterType<IEntityFrameworkDbContext<ExecutedSubSpecialityHolder>, EntityFrameworkDbContext<ExecutedSubSpecialityHolder>>();

            //_container.RegisterType<ICommonRepository<ISubSpeciality>, CommonRepository<ISubSpeciality>>();

            _container.RegisterType<IActivityExecutorRepository, ActivityExecutorRepository>();
            _container.RegisterType<IResourceSupplierRepository, ResourceSupplierRepository>();
            _container.RegisterType<IResourceProviderRepository, ResourceProviderRepository>();

            //reporting
            _container.RegisterType<IInvestmentMainCustomReportSettingsRepository, InvestmentMainCustomReportSettingsRepository>();
            _container.RegisterType<IInvestmentChildCustomReportSettingsRepository, InvestmentChildCustomReportSettingsRepository>();

        }

        private void RegisterDomainServices()
        {
            //_container.RegisterType<IInvestmentElementDomainServices, InvestmentElementDomainServices>();
            _container.RegisterType<IInvestmentDomainServices, InvestmentDomainServices>();
            _container.RegisterType<IInvestmentComponentDomainServices, InvestmentComponentDomainServices>();

           // _container.RegisterType<IEquipmentPlannedResourceDomainServices, EquipmentPlannedResourceDomainServices>();

            //_container.RegisterType<IBudgetComponentResourceDomainServices<IPlannedResource, IBudgetComponentItem>, BudgetComponentResourceDomainServices<IPlannedResource, IBudgetComponentItem>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IPlannedActivity>, BudgetComponentResourceDomainServices<IPlannedActivity>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IExecutedActivity>, BudgetComponentResourceDomainServices< IExecutedActivity>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IPlannedResource>, BudgetComponentResourceDomainServices<IPlannedResource>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IUnderGroupActivity>, BudgetComponentResourceDomainServices<IUnderGroupActivity>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IUnderGroupResource>, BudgetComponentResourceDomainServices<IUnderGroupResource>>();
            //_container.RegisterType<IBudgetComponentActivityDomainServices<IPlannedActivity, IBudgetComponent>, BudgetComponentActivityDomainServices<IPlannedActivity, IBudgetComponent>>();
            //_container.RegisterType<IBudgetComponentActivityDomainServices<IPlannedActivity, IVariantLinesHolder>, BudgetComponentActivityDomainServices<IPlannedActivity, IVariantLinesHolder>>();
            _container.RegisterType<IUnderGroupActivityDomainServices, UnderGroupActivityDomainServices>();

            _container.RegisterType<IPlannedActivityDomainServices, PlannedActivityDomainServices>();
            _container.RegisterType<IExecutedActivityDomainServices, ExecutedActivityDomainServicesBase>();

            _container.RegisterType<IEquipmentPlannedActivityDomainServices, EquipmentPlannedActivityDomainServices>();
            //_container.RegisterType<IEquipmentExecutedResourceDomainServices, EquipmentExecutedResourceDomainServices>();

            _container.RegisterType<IEquipmentExecutedActivityDomainServices, EquipmentExecutedActivityDomainServices>();

          //  _container.RegisterType<IConstructionPlannedResourceDomainServices, ConstructionPlannedResourceDomainServices>();
           
            
            _container.RegisterType<IConstructionPlannedActivityDomainServices, ConstructionPlannedActivityDomainServices>();
           // _container.RegisterType<IConstructionExecutedResourceDomainServices, ConstructionExecutedResourceDomainServices>();
            _container.RegisterType<IConstructionExecutedActivityDomainServices, ConstructionExecutedActivityDomainServices>();

           // _container.RegisterType<IOtherExpensesPlannedResourceDomainServices, OtherExpensesPlannedResourceDomainServices>();
            _container.RegisterType<IOtherExpensesPlannedActivityDomainServices, OtherExpensesPlannedActivityDomainServices>();
            //_container.RegisterType<IOtherExpensesExecutedResourceDomainServices, OtherExpensesExecutedResourceDomainServices>();
            _container.RegisterType<IOtherExpensesExecutedActivityDomainServices, OtherExpensesExecutedActivityDomainServices>();

           // _container.RegisterType<IWorkCapitalPlannedResourceDomainServices, WorkCapitalPlannedResourceDomainServices>();
            _container.RegisterType<IWorkCapitalPlannedActivityDomainServices, WorkCapitalPlannedActivityDomainServices>();
            //_container.RegisterType<IWorkCapitalExecutedResourceDomainServices, WorkCapitalExecutedResourceDomainServices>();
            _container.RegisterType<IWorkCapitalExecutedActivityDomainServices, WorkCapitalExecutedActivityDomainServices>();
            _container.RegisterType<ICashFlowCashMovementCategoryDomainService<ICashEntry>, CashFlowCashMovementCategoryDomainService<ICashEntry>>();
            _container.RegisterType<ICashFlowCashMovementCategoryDomainService<ICashOutgoing>, CashFlowCashMovementCategoryDomainService<ICashOutgoing>>();
            
          



            _container.RegisterType<IBudgetComponentResourceDomainServices<IPlannedResource>, BudgetComponentResourceDomainServices<IPlannedResource>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IPlannedActivity>, BudgetComponentResourceDomainServices<IPlannedActivity>>();
            _container.RegisterType<IBudgetComponentResourceDomainServices<IExecutedActivity>, BudgetComponentResourceDomainServices<IExecutedActivity>>();
            //_container.RegisterType<IExecutedBudgetComponentItemDomainServices<IExecutedActivity, IBudgetComponent>, ExecutedBudgetComponentItemDomainServicesBase<IExecutedActivity, IBudgetComponent>>();



            _container.RegisterType<IOaceDomainServices, OaceDomainServices>();
            _container.RegisterType<IOsdeDomainServices, OsdeDomainServices>();
            _container.RegisterType<IPhaseDomainServices, PhaseDomainServices>();
            _container.RegisterType<ICategoryDomainServices, CategoryDomainServices>();
            _container.RegisterType<IInvestmentTypeDomainServices, InvestmentTypeDomainServices>();
            _container.RegisterType<IExpenseConceptDomainServices, ExpenseConceptDomainServices>();
            _container.RegisterType<ISubExpenseConceptDomainService, SubExpenseConceptDomainService>();
            _container.RegisterType<ISpecialityDomainServices, SpecialityDomainServices>();
            _container.RegisterType<ISubSpecialityDomainService, SubSpecialityDomainService>();
            _container.RegisterType<IWageScaleDomainServices, WageScaleDomainServices>();
            _container.RegisterType<IWorkForceDomainServices, WorkForceDomainServices>();

            _container.RegisterType<IConvertibleEntityDomainService<IConvertibleEntity>, ConvertibleEntityDomainService<IConvertibleEntity>>();
            _container.RegisterType<IConvertibleEntityDomainService<IMeasurementUnit>, ConvertibleEntityDomainService<IMeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityDomainService<ICurrency>, ConvertibleEntityDomainService<ICurrency>>();
            _container.RegisterType<IMeasurementUnitDomainService, MeasurementUnitDomainService>();
            _container.RegisterType<ICurrencyDomainService, CurrencyDomainService>();

            _container.RegisterType<IPeriodDomainService, PeriodDomainService>();
            //_container.RegisterType<IPeriodDomainService<IPeriod>, PeriodDomainService<IPeriod>>();

            _container.RegisterType<IPriceSystemDomainService, PriceSystemDomainService>();
            _container.RegisterType<ISectionDomainService, SectionDomainService>();

            //_container.RegisterType<IActivityDomainServices<IPlannedActivity,ISection>, BudgetComponentActivityDomainServices<IPlannedActivity,ISection>>();
                                    
            _container.RegisterType<IDossificatorDomainService, DossificatorDomainService>();
            _container.RegisterType<IVariantLinesHolderDomainService, VariantLinesHolderDomainService>();
            //_container.RegisterType<IActivityDomainServices<IPlannedActivity, IVariantLinesHolder>, BudgetComponentActivityDomainServices<IPlannedActivity, IVariantLinesHolder>>();
           

            _container.RegisterType<ICashMovementCategoryDomainService<ICashMovementCategory>, CashMovementCategoryDomainService<ICashMovementCategory>>();
            _container.RegisterType<ICashMovementCategoryDomainService<ICashEntry>, CashMovementCategoryDomainService<ICashEntry>>();
            _container.RegisterType<ICashMovementCategoryDomainService<ICashOutgoing>, CashMovementCategoryDomainService<ICashOutgoing>>();
            _container.RegisterType<IDomainServices<IWorkCapitalCashFlow>, DomainServicesBase<IWorkCapitalCashFlow>>();
            _container.RegisterType<ICashMovementDomainService, CashMovementDomainService>();
            _container.RegisterType<IExecutionDomainService, ExecutionDomainService>();
            _container.RegisterType<IUnitConverterDomainService, UnitConverterDomainService>();

            _container.RegisterType<IWeightDomainService, WeightDomainService>();
            _container.RegisterType<IVolumeDomainService, VolumeDomainService>();

            _container.RegisterType<IMeasurableUnitDomainService<IWeight>, MeasurableUnitDomainService<IWeight>>();
            _container.RegisterType<IMeasurableUnitDomainService<IVolume>, MeasurableUnitDomainService<IVolume>>();

            _container.RegisterType<IOverGroupDomainService, OverGroupDomainService>();
            _container.RegisterType<IRegularGroupDomainService, RegularGroupDomainService>();
            _container.RegisterType<IUnderGroupDomainService, UnderGroupDomainService>();

            _container.RegisterType<IDomainServices<ISubExpenseConcept>, StandarDomainService<ISubExpenseConcept>>();
            _container.RegisterType<IDomainServices<ISubSpeciality>, StandarDomainService<ISubSpeciality>>();
            _container.RegisterType<IDomainServices<IWageScale>, StandarDomainService<IWageScale>>();
            _container.RegisterType<IDomainServices<ICurrency>, StandarDomainService<ICurrency>>();
            _container.RegisterType<IDomainServices<IMeasurementUnit>, StandarDomainService<IMeasurementUnit>>();
            _container.RegisterType<IInvestmentDocumentDomainService, InvestmentDocumentDomainService>();
            _container.RegisterType<IUnderGroupActivityDomainServices, UnderGroupActivityDomainServices>();
            _container.RegisterType<IUnderGroupResourceDomainService, UnderGroupResourceDomainServices>();

            //new from refactoring subspecialities
            _container.RegisterType<IPlannedSubSpecialityHolderDomainServices, PlannedSubSpecialityHolderDomainServices>();
            _container.RegisterType<IExecutedSubSpecialityHolderDomainServices, ExecutedSubSpecialityHolderDomainServices>();

            _container.RegisterType<IActivityExecutorDomainServicesBase, ActivityExecutorDomainServicesBase>();
            _container.RegisterType<IResourceSupplierDomainServices, ResourceSupplierDomainServices>();
            _container.RegisterType<IResourceProviderDomainServices, ResourceProviderDomainServices>();

            //reporting
            _container.RegisterType<ICustomReportSettingsDomainServices<IInvestmentMainCustomReportSettings>, CustomReportSettingsDomainServices<IInvestmentMainCustomReportSettings>>();
            _container.RegisterType<ICustomReportSettingsDomainServices<IInvestmentChildCustomReportSettings>, CustomReportSettingsDomainServices<IInvestmentChildCustomReportSettings>>();

        }

        private void RegisterDomainEntities()
        {
            _container.RegisterType<IInvestmentElement, InvestmentElement>();
            _container.RegisterType<IInvestment, Investment>();
            _container.RegisterType<IInvestmentComponent, InvestmentComponent>();

            _container.RegisterType<IBudget, Budget>();
            _container.RegisterType<IEquipmentComponent, EquipmentComponent>();
            _container.RegisterType<IConstructionComponent, ConstructionComponent>();
            _container.RegisterType<IOtherExpensesComponent, OtherExpensesComponent>();
            _container.RegisterType<IWorkCapitalComponent, WorkCapitalComponent>();

            _container.RegisterType<IPlannedResource, PlannedResource>();
          //  _container.RegisterType<IExecutedResource, ExecutedResource>();
            _container.RegisterType<IPlannedActivity, PlannedActivity>();
            _container.RegisterType<IExecutedActivity, ExecutedActivity>();

            _container.RegisterType<IOace, Oace>();
            _container.RegisterType<IOsde, Osde>();
            _container.RegisterType<IPhase, Phase>();
            _container.RegisterType<ICategory, CompanyName.Atlas.Investments.Implementation.Domain.Entities.Category>();
            _container.RegisterType<IInvestmentType, InvestmentType>();
            _container.RegisterType<IExpenseConcept, ExpenseConcept>();
            _container.RegisterType<ISubExpenseConcept, SubExpenseConcept>();
            _container.RegisterType<ISpeciality, Speciality>();
            _container.RegisterType<ISubSpeciality, SubSpeciality>();

            _container.RegisterType<IWageScale, WageScale>();
            _container.RegisterType<IWorkForce, WorkForce>();

            _container.RegisterType<IConvertibleEntity, ConvertibleEntity>();
            _container.RegisterType<IMeasurementUnit, MeasurementUnit>();
            _container.RegisterType<ICurrency, Currency>();

            //price system & dossificator

            _container.RegisterType<IPriceSystem, PriceSystem>();
            _container.RegisterType<ISection, Section>();
            //Besides Registrations
            //common domain entities
            _container.RegisterType<IPeriod, Period>();
            _container.RegisterType<IInvestmentElementPeriod, InvestmentElementPeriod>();
            //tools entities
            //_container.RegisterType<ILifeline, Lifeline>();
            _container.RegisterType<IDossificator, Dossificator>();

            _container.RegisterType<IVariantLinesHolder, VariantLinesHolder>();

            _container.RegisterType<ICashEntry, CashEntry>();
            _container.RegisterType<ICashOutgoing, CashOutgoing>();
            _container.RegisterType<ICashMovementCategory, CashMovementCategory>();
            _container.RegisterType<IWorkCapitalCashFlow, WorkCapitalCashFlow>();
            _container.RegisterType<ICashMovement, CashMovement>();

            _container.RegisterType<IExecution, Execution>();
            _container.RegisterType<IUnitConverter, UnitConverter>();
            _container.RegisterType<IWeight, Weight>();
            _container.RegisterType<IVolume, Volume>();

            _container.RegisterType<IOverGroup, OverGroup>();
            _container.RegisterType<IRegularGroup, RegularGroup>();
            _container.RegisterType<IUnderGroup, UnderGroup>();
            _container.RegisterType<IUnderGroupActivity, UnderGroupActivity>();
            _container.RegisterType<IUnderGroupResource, UnderGroupResource>();


            _container.RegisterType<IInvestmentDocument, InvestmentDocument>();

            //new from refactoring subspecialities
            _container.RegisterType<IPlannedSubSpecialityHolder, PlannedSubSpecialityHolder>();
            _container.RegisterType<IExecutedSubSpecialityHolder, ExecutedSubSpecialityHolder>();

            _container.RegisterType<IActivityExecutor, ActivityExecutor>();
            _container.RegisterType<IResourceSupplier, ResourceSupplier>();
            _container.RegisterType<IResourceProvider, ResourceProvider>();

            //reporting
            _container.RegisterType<IInvestmentMainCustomReportSettings, InvestmentMainCustomReportSettings>();
            _container.RegisterType<IInvestmentChildCustomReportSettings, InvestmentChildCustomReportSettings>();


        }

        private void RegisterCrudViewModels()
        {
           // _container.RegisterType<IInvestmentElementViewModel, InvestmentElementViewModel>();
            _container.RegisterType<IInvestmentViewModel, InvestmentViewModel>();
            _container.RegisterType<IInvestmentComponentViewModel<IInvestment>, InvestmentComponentViewModel<IInvestment>>();
            _container.RegisterType<IInvestmentComponentViewModel<IInvestmentComponent>, InvestmentComponentViewModel<IInvestmentComponent>>();



            _container.RegisterType<IPlannedActivityViewModel, PlannedActivityViewModelBase>();
            _container.RegisterType<IExecutedActivityViewModel, ExecutedActivityViewModelBase>();

    
            //new from refactoring subspecialities

            // _container.RegisterType<IEquipmentPlannedResourceViewModel, EquipmentPlannedResourceViewModel>();
            _container.RegisterType<IEquipmentPlannedSubSpecialityHolderViewModel, EquipmentPlannedSubSpecialityHolderViewModel>();
            _container.RegisterType<IEquipmentExecutedSubSpecialityHolderViewModel, EquipmentExecutedSubSpecialityHolderViewModel>();

            //  _container.RegisterType<IConstructionPlannedResourceViewModel, ConstructionPlannedResourceViewModel>();
            _container.RegisterType<IConstructionPlannedSubSpecialityHolderViewModel, ConstructionPlannedSubSpecialityHolderViewModel>();
            _container.RegisterType<IConstructionExecutedSubSpecialityHolderViewModel, ConstructionExecutedSubSpecialityHolderViewModel>();

            //        _container.RegisterType<IOtherExpensesPlannedResourceViewModel, OtherExpensesPlannedResourceViewModel>();
            _container.RegisterType<IOtherExpensesPlannedSubSpecialityHolderViewModel, OtherExpensesPlannedSubSpecialityHolderViewModel>();
            _container.RegisterType<IOtherExpensesExecutedSubSpecialityHolderViewModel, OtherExpensesExecutedSubSpecialityHolderViewModel>();

            //    _container.RegisterType<IWorkCapitalPlannedResourceViewModel, WorkCapitalPlannedResourceViewModel>();
            _container.RegisterType<IWorkCapitalPlannedSubSpecialityHolderViewModel, WorkCapitalPlannedSubSpecialityHolderViewModel>();
           _container.RegisterType<IWorkCapitalExecutedSubSpecialityHolderViewModel, WorkCapitalExecutedSubSpecialityHolderViewModel>();


            _container.RegisterType<IPlannedResourceViewModel<IBudgetComponentItem>, PlannedResourceViewModel<IBudgetComponentItem>>();
            _container.RegisterType<IPlannedResourceViewModel<IPlannedActivity>, PlannedResourceViewModel<IPlannedActivity>>();
            _container.RegisterType<IPlannedResourceViewModel<IExecutedActivity>, PlannedResourceViewModel<IExecutedActivity>>();
            _container.RegisterType<IPlannedResourceViewModel<IPlannedResource>, PlannedResourceViewModel<IPlannedResource>>();
            _container.RegisterType<IPlannedResourceViewModel<IUnderGroupActivity>, PlannedResourceViewModel<IUnderGroupActivity>>();
            _container.RegisterType<IPlannedResourceViewModel<IUnderGroupResource>, PlannedResourceViewModel<IUnderGroupResource>>();



            _container.RegisterType<IOaceViewModel, OaceViewModel>();
            _container.RegisterType<IOaceProvider, OaceViewModel>();
            _container.RegisterType<IOsdeViewModel, OsdeViewModel>();
            _container.RegisterType<IOsdeProvider, OsdeViewModel>();
            _container.RegisterType<IPhaseViewModel, PhaseViewModel>();
            _container.RegisterType<IPhaseProvider, PhaseViewModel>();
            _container.RegisterType<ICategoryViewModel, CategoryViewModel>();
            _container.RegisterType<IInvestmentTypeViewModel, InvestmentTypeViewModel>();
            _container.RegisterType<IInvestmentTypeProvider, InvestmentTypeViewModel>();
            _container.RegisterType<IExpenseConceptViewModel, ExpenseConceptViewModel>();
            _container.RegisterType<ISubExpenseConceptViewModel, SubExpenseConceptViewModel>();
            _container.RegisterType<ISpecialityViewModel, SpecialityViewModel>();
            _container.RegisterType<ISubSpecialityViewModel, SubSpecialityViewModel>();
          
            _container.RegisterType<IWageScaleViewModel, WageScaleViewModel>();
            _container.RegisterType<IWorkForceViewModel, WorkForceViewModel>();

            _container.RegisterType<IConvertibleEntityViewModel<IConvertibleEntity>, ConvertibleEntityViewModel<IConvertibleEntity>>();
            _container.RegisterType<IConvertibleEntityViewModel<IMeasurementUnit>, ConvertibleEntityViewModel<IMeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityViewModel<ICurrency>, ConvertibleEntityViewModel<ICurrency>>();
            _container.RegisterType<IConvertibleEntityViewModel<ICurrency,ICurrencyPresenter>, ConvertibleEntityViewModel<ICurrency,ICurrencyPresenter,ICurrencyManagerApplicationServices>>();
            _container.RegisterType<IConvertibleEntityViewModel<IMeasurementUnit,IMeasurementUnitPresenter>, ConvertibleEntityViewModel<IMeasurementUnit,IMeasurementUnitPresenter,IMeasurementUnitManagerApplicationServices>>();
            _container.RegisterType<IMeasurementUnitViewModel, MeasurementUnitViewModel>();
            _container.RegisterType<ICurrencyViewModel, CurrencyViewModel>();

            //PS&D
            _container.RegisterType<IPriceSystemViewModel, PriceSystemViewModel>();
            _container.RegisterType<IPriceSystemProvider, PriceSystemViewModel>();
            _container.RegisterType<ISectionViewModel, SectionViewModel>();

            //_container.RegisterType<IPlannedActivityViewModel<ISection, IPlannedActivityPresenter<ISection>>, PlannedActivityViewModelBase2<ISection, IPlannedActivityPresenter<ISection>, IActivityManagerApplicationServices<IPlannedActivity, ISection>>>();

            //_container.RegisterType<IDossificatorViewModel, DossificatorViewModel>();
            //_container.RegisterType<IVariantLinesHolderViewModel, VariantLinesHolderViewModel>();

            //_container.RegisterType<IPlannedActivityViewModel<IVariantLinesHolder, IPlannedActivityPresenter<IVariantLinesHolder>>, PlannedActivityViewModelBase3<IVariantLinesHolder, IPlannedActivityPresenter<IVariantLinesHolder>, IActivityFullManagerApplicationServices<IPlannedActivity, IVariantLinesHolder>>>();

           


            _container.RegisterType<ICashMovementCategoryViewModel<ICashMovementCategory>, CashMovementCategoryViewModel<ICashMovementCategory>>();
            _container.RegisterType<ICashMovementCategoryViewModel<ICashEntry>, CashMovementCategoryViewModel<ICashEntry>>();
            _container.RegisterType<ICashMovementCategoryViewModel<ICashOutgoing>, CashMovementCategoryViewModel<ICashOutgoing>>();

            _container.RegisterType<ICashMovementViewModel<ICashOutgoing>, CashMovementViewModel<ICashOutgoing>>();
            _container.RegisterType<ICashMovementViewModel<ICashEntry>, CashMovementViewModel<ICashEntry>>();

            _container.RegisterType<IWorkCapitalCashFlowCashMovementCategoryViewModel<ICashEntry>, WorkCapitalCashFlowCashMovementCategoryViewModel<ICashEntry>>();
            _container.RegisterType<IWorkCapitalCashFlowCashMovementCategoryViewModel<ICashOutgoing>, WorkCapitalCashFlowCashMovementCategoryViewModel<ICashOutgoing>>();
                      
           
            
            _container.RegisterType<IExecutionViewModel, ExecutionViewModel>();
            //_container.RegisterType<IExecutionViewModel<IConstructionComponent>, ExecutionViewModel<IConstructionComponent>>();
            //_container.RegisterType<IExecutionViewModel<IOtherExpensesComponent>, ExecutionViewModel<IOtherExpensesComponent>>();
            //_container.RegisterType<IExecutionViewModel<IWorkCapitalComponent>, ExecutionViewModel<IWorkCapitalComponent>>();

            _container.RegisterType<IUnitConverterViewModel<IConvertibleEntity>, UnitConverterViewModel<IConvertibleEntity>>();
            _container.RegisterType<IUnitConverterViewModel<IMeasurementUnit>, UnitConverterViewModel<IMeasurementUnit>>();
            _container.RegisterType<IUnitConverterViewModel<ICurrency>, UnitConverterViewModel<ICurrency>>();

            _container.RegisterType<IWeightPresenter, WeightPresenter>();
            _container.RegisterType<IVolumePresenter, VolumePresenter>();
            //_container.RegisterType<IEntityProviderManagerApplicationServices<IWeight>, EntityProviderManagerApplicationServices<IWeight>>();
            //_container.RegisterType<IEntityProviderManagerApplicationServices<IVolume>, EntityProviderManagerApplicationServices<IVolume>>();

            

            _container.RegisterType<IOverGroupViewModel, OverGroupViewModel>();
            _container.RegisterType<IRegularGroupViewModel, RegularGroupViewModel>();
            _container.RegisterType<IUnderGroupViewModel, UnderGroupViewModel>();
            _container.RegisterType<IUnderGroupActivityViewModel, UnderGroupActivityViewModel>();
            _container.RegisterType<IUnderGroupResourceViewModel, UnderGroupResourceViewModel>();

            _container.RegisterType<IInvestmentDocumentViewModel, InvestmentDocumentViewModel>();


            _container.RegisterType<IActivityExecutorViewModel, ActivityExecutorViewModel>();
            _container.RegisterType<IResourceSupplierViewModel, ResourceSupplierViewModel>();
            _container.RegisterType<IResourceProviderViewModel, ResourceProviderViewModel>();

            //reporting
            _container.RegisterType<IInvestmentMainCustomReportSettingsViewModel, InvestmentMainCustomReportSettingsViewModel>();
            _container.RegisterType<IInvestmentChildCustomReportSettingsViewModel, InvestmentChildCustomReportSettingsViewModel>();
            _container.RegisterType<IInvestmentChildMainCustomReportSettingsViewModel, InvestmentChildMainCustomReportSettingsViewModel>();

            

            //be side
            //  _container.RegisterType<IPeriodViewModel, PeriodViewModel>();
        }

        private void RegisterPresenterViewModels()
        {
           // _container.RegisterType<IInvestmentElementPresenter, InvestmentElementPresenter>();
            _container.RegisterType<IInvestmentPresenter, InvestmentPresenter>();
            _container.RegisterType<IInvestmentComponentPresenter<IInvestment>, InvestmentComponentPresenter<IInvestment>>();
            _container.RegisterType<IInvestmentComponentPresenter<IInvestmentComponent>, InvestmentComponentPresenter<IInvestmentComponent>>();
            _container.RegisterType<IBudgetPresenter, BudgetPresenter>();
            _container.RegisterType<IEquipmentComponentPresenter, EquipmentComponentPresenter>();
            _container.RegisterType<IConstructionComponentPresenter, ConstructionComponentPresenter>();
            _container.RegisterType<IOtherExpensesComponentPresenter, OtherExpensesComponentPresenter>();
            _container.RegisterType<IWorkCapitalComponentPresenter, WorkCapitalComponentPresenter>();

            _container.RegisterType<IPlannedResourcePresenter<IPlannedActivity>, PlannedResourcePresenterBase<IPlannedActivity>>();
            _container.RegisterType<IPlannedResourcePresenter<IExecutedActivity>, PlannedResourcePresenterBase<IExecutedActivity>>();
            _container.RegisterType<IPlannedResourcePresenter<IPlannedResource>, PlannedResourcePresenterBase<IPlannedResource>>();
            _container.RegisterType<IPlannedResourcePresenter<IUnderGroupActivity>, PlannedResourcePresenterBase<IUnderGroupActivity>>();
            _container.RegisterType<IPlannedResourcePresenter<IUnderGroupResource>, PlannedResourcePresenterBase<IUnderGroupResource>>();

            //_container.RegisterType<IPlannedResourcePresenter<IVariantLinesHolder>, PlannedResourcePresenterBase<IVariantLinesHolder, IBudgetComponentResourceManagerApplicationServices<IPlannedResource, IVariantLinesHolder>>>();

            _container.RegisterType<IPlannedActivitiesForNomenclatorViewModel, PlannedActivitiesForNomenclatorViewModel>();
            _container.RegisterType<INomenclatorPresenter<IPlannedActivity>, NomenclatorPresenter<IPlannedActivity>>();
            _container.RegisterType<INomenclatorManagerApplicationServices<IPlannedActivity>, NomenclatorManagerApplicationServices<IPlannedActivity>>();
            _container.RegisterType<ICommonDomainService<IPlannedActivity>, CommonDomainService<IPlannedActivity>>();
            _container.RegisterType<ICommonRepository<IPlannedActivity>, CommonRepositoryInheritanceEF<IPlannedActivity, PlannedActivity, UnderGroupActivity>>();
            //_container.RegisterType<IBudgetComponentItemForNomenclatorRepositoryEF<IPlannedActivity>, BudgetComponentItemForNomenclatorRepositoryEF<IPlannedActivity>>();



            _container.RegisterType<IPlannedResourceForNomenclatorViewModel, PlannedResourceForNomenclatorViewModel>();
            _container.RegisterType<INomenclatorPresenter<IPlannedResource>, NomenclatorPresenter<IPlannedResource>>();
            _container.RegisterType<INomenclatorRepository<IPlannedResource>, NomenclatorRepositoryEF<IPlannedResource, PlannedResource>>();
            _container.RegisterType<INomenclatorManagerApplicationServices<IPlannedResource>, NomenclatorManagerApplicationServices<IPlannedResource>>();
            _container.RegisterType<ICommonDomainService<IPlannedResource>, CommonDomainService<IPlannedResource>>();
           // _container.RegisterType<ICommonRepository<IPlannedResource>, CommonRepository<IPlannedResource>>();
            //_container.RegisterType<IBudgetComponentItemForNomenclatorRepositoryEF<IPlannedResource>, BudgetComponentItemForNomenclatorRepositoryEF<IPlannedResource>>();


            _container.RegisterType<IPlannedActivityPresenter, PlannedActivityPresenter>();
            _container.RegisterType<IExecutedActivityPresenter, ExecutedActivityPresenterBase>();

  
           
            _container.RegisterType<IEquipmentPlannedSubSpecialityHolderPresenter, EquipmentPlannedSubSpecialityHolderPresenter>();
            
            _container.RegisterType<IEquipmentExecutedSubSpecialityHolderPresenter, EquipmentExecutedSubSpecialityHolderPresenter>();

           _container.RegisterType<IConstructionPlannedSubSpecialityHolderPresenter, ConstructionPlannedSubSpecialityHolderPresenter>();
           
            _container.RegisterType<IConstructionExecutedSubSpecialityHolderPresenter, ConstructionExecutedSubSpecialityHolderPresenter>();
            
            _container.RegisterType<IOtherExpensesPlannedSubSpecialityHolderPresenter, OtherExpensesPlannedSubSpecialityHolderPresenter>();
           
            _container.RegisterType<IOtherExpensesExecutedSubSpecialityHolderPresenter, OtherExpensesExecutedSubSpecialityHolderPresenter>();
            
            _container.RegisterType<IWorkCapitalPlannedSubSpecialityHolderPresenter, WorkCapitalPlannedSubSpecialityHolderPresenter>();

            _container.RegisterType<IWorkCapitalExecutedSubSpecialityHolderPresenter, WorkCapitalExecutedSubSpecialityHolderPresenter>();


            _container.RegisterType<IOacePresenter, OacePresenter>();
            _container.RegisterType<IOsdePresenter, OsdePresenter>();
            _container.RegisterType<IPhasePresenter, PhasePresenter>();
            _container.RegisterType<ICategoryPresenter, CategoryPresenter>();
            _container.RegisterType<IInvestmentTypePresenter, InvestmentTypePresenter>();
            _container.RegisterType<IExpenseConceptPresenter, ExpenseConceptPresenter>();
            _container.RegisterType<ISubExpenseConceptPresenter, SubExpenseConceptPresenter>();
            _container.RegisterType<ISpecialityPresenter, SpecialityPresenter>();
            _container.RegisterType<ISubSpecialityPresenter, SubSpecialityPresenter>();
            //for subspeciality Nomenclator Provider
            _container.RegisterType<ISubSpecialityForNomenclatorViewModel, SubSpecialityForNomenclatorViewModel>();
            _container.RegisterType<INomenclatorPresenter<ISubSpeciality>, NomenclatorPresenter<ISubSpeciality>>();
            _container.RegisterType<INomenclatorRepository<ISubSpeciality>, NomenclatorRepositoryEF<ISubSpeciality, SubSpeciality>>();
            _container.RegisterType<INomenclatorManagerApplicationServices<ISubSpeciality>, NomenclatorManagerApplicationServices<ISubSpeciality>>();
            _container.RegisterType<ICommonDomainService<ISubSpeciality>, CommonDomainService<ISubSpeciality>>();
            //_container.RegisterType<ICommonRepository<ISubSpeciality>, CommonRepository<ISubSpeciality>>();

            _container.RegisterType<ISubSpecialityForNomenclatorViewModel, SubSpecialityForNomenclatorViewModel>();
            _container.RegisterType<IWageScalePresenter, WageScalePresenter>();
            _container.RegisterType<IWorkForcePresenter, WorkForcePresenter>();

            _container.RegisterType<IConvertibleEntityPresenter<IConvertibleEntity>, ConvertibleEntityPresenter<IConvertibleEntity>>();
            _container.RegisterType<IConvertibleEntityPresenter<IMeasurementUnit>, ConvertibleEntityPresenter<IMeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityPresenter<ICurrency>, ConvertibleEntityPresenter<ICurrency>>();
            _container.RegisterType<IMeasurementUnitPresenter, MeasuremetUnitPresenter>();
            _container.RegisterType<ICurrencyPresenter, CurrencyPresenter>();

            //PS&d

            _container.RegisterType<IPriceSystemPresenter, PriceSystemPresenter>();
            //_container.RegisterType<IPriceSystemPresenter<ISection>, PriceSystemPresenter<ISection>>();
            _container.RegisterType<ISectionPresenter, SectionPresenter>();
            _container.RegisterType<IGenericSectionPresenter, GenericSectionPresenter<ISection,ISectionManagerApplicationService>>();
            _container.RegisterType<IGenericSectionPresenter, GenericSectionPresenter<IPriceSystem, IPriceSystemManagerApplicationService>>();
            //_container.RegisterType<IPlannedActivityPresenter<ISection>, BudgetComponentActivityPresenter<ISection, IBudgetComponentActivityManagerApplicationServices<IPlannedActivity, ISection>>>();
            //_container.RegisterType<IPlannedActivityPresenter<IVariantLinesHolder>, BudgetComponentActivityPresenter<IVariantLinesHolder, IBudgetComponentActivityManagerApplicationServices<IPlannedActivity, IVariantLinesHolder>>>();
            _container.RegisterType<IUnderGroupActivityPresenter, UnderGroupActivityPresenter>();
            _container.RegisterType<IUnderGroupResourcePresenter, UnderGroupResourcePresenter>();


            _container.RegisterType<IGenericSectionPresenter, GenericSectionPresenter<IPriceSystem, IPriceSystemManagerApplicationService>>();
            //Besides Registrations
           
            _container.RegisterType<IInvestmentElementPeriodPresenter, InvestmentElementPeriodPresenter>();
            //_container.RegisterType<IDossificatorPresenter, DossificatorPresenter>();
            //_container.RegisterType<IVariantLinesHolderPresenter, VariantLinesHolderPresenter>();

            _container.RegisterType<IPlannedResourceViewModel<IVariantLinesHolder>, PlannedResourceViewModel<IVariantLinesHolder>>();

            _container.RegisterType<IWorkCapitalCashFlowPresenter, WorkCapitalCashFlowPresenter>();
            _container.RegisterType<ICashMovementCategoryPresenter<ICashMovementCategory>, CashMovementCategoryPresenter<ICashMovementCategory>>();
            _container.RegisterType<ICashMovementCategoryPresenter<ICashEntry>, CashMovementCategoryPresenter<ICashEntry>>();
            _container.RegisterType<ICashMovementCategoryPresenter<ICashOutgoing>, CashMovementCategoryPresenter<ICashOutgoing>>();
            //_container.RegisterType<ICashMovementCategoryPresenter<IWorkCapitalCashFlow>, CashMovementCategoryPresenter<IWorkCapitalCashFlow>>();

            _container.RegisterType<ICashMovementPresenter<ICashMovementCategory>, CashMovementPresenter<ICashMovementCategory>>();
            _container.RegisterType<ICashMovementPresenter<ICashEntry>, CashMovementPresenter<ICashEntry>>();
            _container.RegisterType<ICashMovementPresenter<ICashOutgoing>, CashMovementPresenter<ICashOutgoing>>();

            _container.RegisterType<IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashEntry>, WorkCapitalCashFlowCashMovementCategoryPresenter<ICashEntry>>();
            _container.RegisterType<IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashOutgoing>, WorkCapitalCashFlowCashMovementCategoryPresenter<ICashOutgoing>>();

           
           
            _container.RegisterType<IExecutionPresenter, ExecutionPresenter>();
            //_container.RegisterType<IExecutionPresenter<IConstructionComponent>, ExecutionPresenter<IConstructionComponent>>();
            //_container.RegisterType<IExecutionPresenter<IOtherExpensesComponent>, ExecutionPresenter<IOtherExpensesComponent>>();
            //_container.RegisterType<IExecutionPresenter<IWorkCapitalComponent>, ExecutionPresenter<IWorkCapitalComponent>>();
            _container.RegisterType<IUnitConverterPresenter<IConvertibleEntity>, UnitConverterPresenter<IConvertibleEntity>>();
            _container.RegisterType<IUnitConverterPresenter<IMeasurementUnit>, UnitConverterPresenter<IMeasurementUnit>>();
            _container.RegisterType<IUnitConverterPresenter<ICurrency>, UnitConverterPresenter<ICurrency>>();
         
        //reporting

            _container.RegisterType<IMetroChartPresenter, MetroChartPresenter>();
            _container.RegisterType<IDataClass, DataClass>();
            _container.RegisterType<ISeriesData, SeriesData>();

            _container.RegisterType<IBudgetSummaryPresenter, BudgetSummaryPresenter>();
            _container.RegisterType<IBudgetSummaryConcept, BudgetSummaryConcept>();

            _container.RegisterType<IPeriodPresenter, PeriodPresenter>();
           // _container.RegisterType<IPeriodPresenter, PeriodPresenter>(new InjectionConstructor(typeof(IPresenter)));

            _container.RegisterType<IOverGroupPresenter, OverGroupPresenter>();
            _container.RegisterType<IRegularGroupPresenter, RegularGroupPresenter>();
            _container.RegisterType<IUnderGroupPresenter, UnderGroupPresenter>();

            _container.RegisterType<IInvestmentDocumentPresenter, InvestmentDocumentPresenter>();

            //last crap

            _container.RegisterType<IActivityExecutorPresenter, ActivityExecutorPresenter>();
            _container.RegisterType<IResourceSupplierPresenter, ResourceSupplierPresenter>();
            _container.RegisterType<IResourceProviderPresenter, ResourceProviderPresenter>();

            //some providers
            _container.RegisterType<IExpenseConceptProvider, ExpenseConceptViewModel>();
            _container.RegisterType<ICategoryProvider, CategoryViewModel>();
            _container.RegisterType<ISpecialityProvider, SpecialityViewModel>();
            _container.RegisterType<IWageScaleProvider, WageScaleViewModel>();
            _container.RegisterType<IInvestmentProvider, InvestmentViewModel>();
            _container.RegisterType<IActivityExecutorProvider, ActivityExecutorViewModel>();
            _container.RegisterType<IResourceSupplierProvider, ResourceSupplierViewModel>();
            _container.RegisterType<IResourceProviderProvider, ResourceProviderViewModel>();

            //reporting
            _container.RegisterType<IInvestmentMainCustomReportSettingsPresenter, InvestmentMainCustomReportSettingsPresenter>();
            _container.RegisterType<IInvestmentChildCustomReportSettingsPresenter, InvestmentChildCustomReportSettingsPresenter>();
            _container.RegisterType<IInvestmentChildMainCustomReportSettingsPresenter, InvestmentChildMainCustomReportSettingsPresenter>();

            

        }

        #endregion

        private void SetupNavigation()
        {
            // Sets up the standard navigation
            _navigationServices.SetupNavigation();

            /* Sets up the investment element 3rd navigation level and which selected investment element will be set as the suite
             * window content's data context. */
            string optionalNavControlUri = "/{0};component/Presentation/Views/InvestmentElementsView.xaml".EasyFormat(_assemblyName);
            _navigationServices.SetupOptionalNavigation(optionalNavControlUri, control => ((AtlasOptionalContent)control).ElementsTreeView);
          //////  string subSystemMenuUri = "/{0};component/Presentation/Views/InvestmentMenu.xaml".EasyFormat(_assemblyName);
           // _navigationServices.SetupSubSystemMenu(subSystemMenuUri, control => ((SubSystemMenu)control).SubSystemMenu);
           // _navigationServices.ShowOptionalNavigationContent();
        }

        private object GetInvestmentsMainViewModel()
        {
            var investmentViewModel = ServiceLocator.Current.GetInstance<IInvestmentViewModel>();
            investmentViewModel.Load();
            ////investmentViewModel.Raised -= OnInteractionRequested;
            ////investmentViewModel.Raised += OnInteractionRequested;
            _container.RegisterInstance(investmentViewModel);
            return investmentViewModel;
        }
        private object GetInvestmentsMainViewModel2()
        {
            return _container.Resolve(typeof(IInvestmentViewModel));
        }

        private object GetModuleAccessViewModel()
        {
          
            var viewModel = ServiceLocator.Current.GetInstance<IAtlasMainModuleAccessViewModel>();
            viewModel.AssemblyName = _assemblyName;
            viewModel.Collection = ServiceLocator.Current.GetInstance<IInvestmentProvider>().Investments;
            viewModel.Load();

            _container.RegisterInstance(viewModel);

            return viewModel;
        }
        private object GetAtlasModuleMainSubjectViewModel()
        {

            // var viewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            var viewModel = (IAtlasModuleMainSubjectViewModel)ServiceLocator.Current.GetInstance(typeof(AtlasModuleMainSubjectViewModel));
            viewModel.AssemblyName = _assemblyName;
           // viewModel.Collection = ServiceLocator.Current.GetInstance<IInvestmentProvider>().Investments;
            viewModel.Load();
            
            //viewModel.Raised -= OnInteractionRequested;
            //viewModel.Raised += OnInteractionRequested;

            _container.RegisterInstance(viewModel);

            return viewModel;
        }
        private object GetOaceViewModel()
        {
            var viewmodel = _container.Resolve<IOaceViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IOaceViewModel>();
                var provider = viewmodel as IOaceProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
                
                
            }

            return viewmodel;
        }

        private object GetOsdeViewModel()
        {
            //return CreateAndInitialize<IOsdeViewModel>();

            var viewmodel = _container.Resolve<IOsdeViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IOsdeViewModel>();
                var provider = viewmodel as IOsdeProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
               
            }

            return viewmodel;

         }

        private object GetPhaseViewModel()
        {
            var viewmodel = _container.Resolve<IPhaseViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IPhaseViewModel>();
                var provider = viewmodel as IPhaseProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
              }

            return viewmodel;

        }

       
        private object GetCategoryViewModel()
        {
            var viewmodel = _container.Resolve<ICategoryViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<ICategoryViewModel>();
                var provider = viewmodel as ICategoryProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
            }

            return viewmodel;

            
        }

        private object GetInvestmentTypeViewModel()
        {
     
            var viewmodel = _container.Resolve<IInvestmentTypeViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IInvestmentTypeViewModel>();
                var provider = viewmodel as IInvestmentTypeProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
            }

            return viewmodel;

        }

        private object GetExpenseConceptViewModel()
        {
            var viewmodel = _container.Resolve<IExpenseConceptViewModel>();
            if (!viewmodel.IsLoaded)
            {
                 viewmodel = CreateAndInitialize<IExpenseConceptViewModel>();
                var provider = viewmodel as IExpenseConceptProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
            }
            return viewmodel;

            
        }

        private object GetSpecialityViewModel()
        {
            var viewModel = CreateAndInitialize<ISpecialityViewModel>();
            var SpecialityViewModel = viewModel as ISpecialityProvider;
            if (SpecialityViewModel != null)
                _container.RegisterInstance(SpecialityViewModel);
            return viewModel;


        }
        private object GetWageScaleViewModel()
        {
            var viewModel = CreateAndInitialize<IWageScaleViewModel>();

            var wageScaleViewModel = viewModel as IWageScaleProvider;
            if (wageScaleViewModel != null)
                _container.RegisterInstance(wageScaleViewModel);

            return viewModel;
        }

        private object GetWorkForceViewModel()
        {
            return CreateAndInitialize<IWorkForceViewModel>();
        }

        private object GetInvestmentMainCustomReportSettingsViewModel()
        {

            var viewmodel = CreateAndInitialize<IInvestmentMainCustomReportSettingsViewModel>();
            _container.RegisterInstance(viewmodel);
            return viewmodel;
        }
        private object GetMeasurementUnitViewModel()
        {

            var viewmodel = _container.Resolve<IMeasurementUnitViewModel>();
            if (!viewmodel.IsLoaded)
            {

                 viewmodel = CreateAndInitialize<IMeasurementUnitViewModel>();
                var measurementUnitViewModel = viewmodel as IMeasurementUnitProvider;
                if (measurementUnitViewModel != null)
                    _container.RegisterInstance(measurementUnitViewModel);
                _container.RegisterInstance(viewmodel);
                return viewmodel;
            }

            return viewmodel;

        }
        private object GetCurrencyViewModel()
        {
            var viewmodel = _container.Resolve<ICurrencyViewModel>();
            if (!viewmodel.IsLoaded)
            {
                 viewmodel = CreateAndInitialize<ICurrencyViewModel>();
                var currencyViewModel = viewmodel as ICurrencyProvider;
                if (currencyViewModel != null)
                    _container.RegisterInstance(currencyViewModel);
                _container.RegisterInstance(viewmodel);
                return viewmodel;
            }

            return viewmodel;


        }
        private object GetPriceSystemViewModel()
        {
            var priceSystem = CreateAndInitialize<IPriceSystemViewModel>();

            priceSystem.Raised -= OnInteractionRequested;
            if (!Equals(priceSystem, null))
                _container.RegisterInstance(priceSystem);
            return priceSystem;
        }

        private object GetResourceSupplierViewModel()
        {
            var viewmodel = _container.Resolve<IResourceSupplierViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IResourceSupplierViewModel>();
                var provider = viewmodel as IResourceSupplierProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
            }

            return viewmodel;
        }

        private object GetResourceProviderViewModel()
        {
            var viewmodel = _container.Resolve<IResourceProviderViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IResourceProviderViewModel>();
                var provider = viewmodel as IResourceProviderProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
            }

            return viewmodel;
        }

        private object GetActivityExecutorViewModel()
        {
            var viewmodel = _container.Resolve<IActivityExecutorViewModel>();
            if (!viewmodel.IsLoaded)
            {
                viewmodel = CreateAndInitialize<IActivityExecutorViewModel>();
                var provider = viewmodel as IActivityExecutorProvider;
                if (provider != null)
                    _container.RegisterInstance(provider);
                _container.RegisterInstance(viewmodel);
            }

            return viewmodel;
        }



        //private object GetVariantLinesHolderViewModel()
        //{
        //    return CreateAndInitialize<IVariantLinesHolderViewModel>();
        //}
        //private object GetDossificatorViewModel()
        //{
        //    return CreateAndInitialize<IDossificatorViewModel>();
        //}

    }
}
