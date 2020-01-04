using System;
using System.Reflection;
using Atlas.Configuration.Presentation.Views;
using CompanyName.Atlas.Configuration.Presentation.Views;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Modularity;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace CompanyName.Atlas.Configuration
{
    /// <summary>
    /// This is the representant of the current subsystem: Investments. Through this class the Atlas suite will detect, load and initialize
    /// such subsystem, to them be able to use its logic.
    /// </summary>
    [Module(ModuleName = SubsystemName)]
    public class ConfigurationSubsystem : SubsystemBase
    {
        /// <summary>
        /// Public name of the Investments subsystem.
        /// </summary>
        public const string SubsystemName = "Atlas.Configuration";

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
        public ConfigurationSubsystem(IUnityContainer container, INavigationServices navigationServices, IVisualResourcesServices visualResourcesServices, ILoggerFacade logger)
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
            _logger.Log("Initializing module: Configuration Subsystem..", Microsoft.Practices.Prism.Logging.Category.Debug, Priority.High);

            MergeResourceDictionaries();
            SetupViewModelMappings();
            SetupDependencies();
            SetupNavigation();

            _logger.Log("Initialized module: Configuration Subsystem", Microsoft.Practices.Prism.Logging.Category.Debug, Priority.Low);
        }

        private void MergeResourceDictionaries()
        {
            _visualResourcesServices.Merge("/{0};component/Presentation/Views/Themes/Light.xaml".EasyFormat(_assemblyName));
        }

        private void SetupViewModelMappings()
        {
            ViewModelLocationProvider.Register(typeof(ModuleSubjectView).FullName, GetAtlasModuleMainSubjectViewModel);
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

            //_container.RegisterType<IEntityProviderManagerApplicationServices<INomenclator>, EntityProviderManagerApplicationServices<INomenclator>>();
            //_container.RegisterType<IDocumentManagerApplicationServices, DocumentManagerApplicationServices>();

        }

        private void RegisterRepositories()
        {
   
            _container.RegisterType<IConvertibleEntityRepository<IConvertibleEntity>, ConvertibleEntityRepository<IConvertibleEntity>>();
            //_container.RegisterType<IConvertibleEntityRepository<IMeasurementUnit>, ConvertibleEntityRepository<IMeasurementUnit>>();
            //_container.RegisterType<IConvertibleEntityRepository<ICurrency>, ConvertibleEntityRepository<ICurrency>>();
            _container.RegisterType<IConvertibleEntityRepository<IMeasurementUnit>, ConvertibleEntityRepositoryEF<IMeasurementUnit, MeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityRepository<ICurrency>, ConvertibleEntityRepositoryEF<ICurrency, Currency>>();


            _container.RegisterType<IPeriodRepository, PeriodRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<Period>, EntityFrameworkDbContext<Period>>();
            _container.RegisterType<IUnitConverterRepository, UnitConverterRepositoryEF>();
            _container.RegisterType<IEntityFrameworkDbContext<UnitConverter>, EntityFrameworkDbContext<UnitConverter>>();

            _container.RegisterType<IMeasurableUnitRepository<IWeight>, MeasurableUnitRepositoryEF<IWeight, Weight>>();
            _container.RegisterType<IEntityFrameworkDbContext<Weight>, EntityFrameworkDbContext<Weight>>();
            _container.RegisterType<IMeasurableUnitRepository<IVolume>, MeasurableUnitRepositoryEF<IVolume, Volume>>();
            _container.RegisterType<IEntityFrameworkDbContext<Volume>, EntityFrameworkDbContext<Volume>>();
            _container.RegisterType<ICommonRepository<IWeight>, CommonRepositoryEF<IWeight, Weight>>();
            _container.RegisterType<ICommonRepository<IVolume>, CommonRepositoryEF<IVolume, Volume>>();

        }

        private void RegisterDomainServices()
        {
           _container.RegisterType<IConvertibleEntityDomainService<IConvertibleEntity>, ConvertibleEntityDomainService<IConvertibleEntity>>();
            _container.RegisterType<IConvertibleEntityDomainService<IMeasurementUnit>, ConvertibleEntityDomainService<IMeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityDomainService<ICurrency>, ConvertibleEntityDomainService<ICurrency>>();
            _container.RegisterType<IMeasurementUnitDomainService, MeasurementUnitDomainService>();
            _container.RegisterType<ICurrencyDomainService, CurrencyDomainService>();

            _container.RegisterType<IPeriodDomainService, PeriodDomainService>();
           
         
            _container.RegisterType<IWeightDomainService, WeightDomainService>();
            _container.RegisterType<IVolumeDomainService, VolumeDomainService>();

            _container.RegisterType<IMeasurableUnitDomainService<IWeight>, MeasurableUnitDomainService<IWeight>>();
            _container.RegisterType<IMeasurableUnitDomainService<IVolume>, MeasurableUnitDomainService<IVolume>>();

           _container.RegisterType<IDomainServices<ICurrency>, StandarDomainService<ICurrency>>();
            _container.RegisterType<IDomainServices<IMeasurementUnit>, StandarDomainService<IMeasurementUnit>>();
           


        }

        private void RegisterDomainEntities()
        {
       
            _container.RegisterType<IConvertibleEntity, ConvertibleEntity>();
            _container.RegisterType<IMeasurementUnit, MeasurementUnit>();
            _container.RegisterType<ICurrency, Currency>();

          
           _container.RegisterType<IUnitConverter, UnitConverter>();
            _container.RegisterType<IWeight, Weight>();
            _container.RegisterType<IVolume, Volume>();

          

        }

        private void RegisterCrudViewModels()
        {
           
           
            _container.RegisterType<IConvertibleEntityViewModel<IConvertibleEntity>, ConvertibleEntityViewModel<IConvertibleEntity>>();
            _container.RegisterType<IConvertibleEntityViewModel<IMeasurementUnit>, ConvertibleEntityViewModel<IMeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityViewModel<ICurrency>, ConvertibleEntityViewModel<ICurrency>>();
            _container.RegisterType<IConvertibleEntityViewModel<ICurrency,ICurrencyPresenter>, ConvertibleEntityViewModel<ICurrency,ICurrencyPresenter,ICurrencyManagerApplicationServices>>();
            _container.RegisterType<IConvertibleEntityViewModel<IMeasurementUnit,IMeasurementUnitPresenter>, ConvertibleEntityViewModel<IMeasurementUnit,IMeasurementUnitPresenter,IMeasurementUnitManagerApplicationServices>>();
            _container.RegisterType<IMeasurementUnitViewModel, MeasurementUnitViewModel>();
            _container.RegisterType<ICurrencyViewModel, CurrencyViewModel>();

            _container.RegisterType<IUnitConverterViewModel<IConvertibleEntity>, UnitConverterViewModel<IConvertibleEntity>>();
            _container.RegisterType<IUnitConverterViewModel<IMeasurementUnit>, UnitConverterViewModel<IMeasurementUnit>>();
            _container.RegisterType<IUnitConverterViewModel<ICurrency>, UnitConverterViewModel<ICurrency>>();

            _container.RegisterType<IWeightPresenter, WeightPresenter>();
            _container.RegisterType<IVolumePresenter, VolumePresenter>();
              }

        private void RegisterPresenterViewModels()
        {
           
            _container.RegisterType<IConvertibleEntityPresenter<IConvertibleEntity>, ConvertibleEntityPresenter<IConvertibleEntity>>();
            _container.RegisterType<IConvertibleEntityPresenter<IMeasurementUnit>, ConvertibleEntityPresenter<IMeasurementUnit>>();
            _container.RegisterType<IConvertibleEntityPresenter<ICurrency>, ConvertibleEntityPresenter<ICurrency>>();
            _container.RegisterType<IMeasurementUnitPresenter, MeasuremetUnitPresenter>();
            _container.RegisterType<ICurrencyPresenter, CurrencyPresenter>();

            _container.RegisterType<IUnitConverterPresenter<IConvertibleEntity>, UnitConverterPresenter<IConvertibleEntity>>();
            _container.RegisterType<IUnitConverterPresenter<IMeasurementUnit>, UnitConverterPresenter<IMeasurementUnit>>();
            _container.RegisterType<IUnitConverterPresenter<ICurrency>, UnitConverterPresenter<ICurrency>>();
            _container.RegisterType<IDataClass, DataClass>();
           
            _container.RegisterType<IPeriodPresenter, PeriodPresenter>();
           

        }

        #endregion

        private void SetupNavigation()
        {
            // Sets up the standard navigation
            _navigationServices.SetupNavigation();

            /* Sets up the investment element 3rd navigation level and which selected investment element will be set as the suite
             * window content's data context. */
            //string optionalNavControlUri = "/{0};component/Presentation/Views/InvestmentElementsView.xaml".EasyFormat(_assemblyName);
            //_navigationServices.SetupOptionalNavigation(optionalNavControlUri, control => ((AtlasOptionalContent)control).ElementsTreeView);
           // string subSystemMenuUri = "/{0};component/Presentation/Views/ConfigurationMenu.xaml".EasyFormat(_assemblyName);
           // _navigationServices.SetupSubSystemMenu(subSystemMenuUri, control => ((SubSystemMenu)control).SubSystemMenu);
           // _navigationServices.HideOptionalNavigationContent();
        }

        private object GetAtlasModuleMainSubjectViewModel()
        {

            //  var viewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            var viewModel = (IAtlasModuleMainSubjectViewModel)ServiceLocator.Current.GetInstance(typeof(AtlasModuleMainSubjectViewModel));
            viewModel.AssemblyName = _assemblyName;
            // viewModel.Collection = ServiceLocator.Current.GetInstance<IInvestmentProvider>().Investments;
            viewModel.Load();
            viewModel.Raised += OnInteractionRequested;
            //  viewModel.Raised -= OnInteractionRequested;

            return viewModel;
        }

    }
}
