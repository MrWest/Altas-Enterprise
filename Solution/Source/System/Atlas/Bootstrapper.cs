using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Validation;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Implementation.Application.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Contracts.Implementation.Domain.Validation.EnterpriseLibrary;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Modularity;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Properties;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Views;
using CompanyName.Atlas.ViewModels;
using CompanyName.Atlas.Views;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using MyLogger = CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Logging.Logger;
using ViewModelLocationProvider = Microsoft.Practices.Prism.Mvvm.ViewModelLocationProvider;
using IView = Microsoft.Practices.Prism.Mvvm.IView;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas
{
    /// <summary>
    /// Represents the Atlas initialization bootstrapper.
    /// </summary>
    internal class Bootstrapper : CommonBootstrapper, IView
    {
        private Shell _shell;
        private readonly ShellViewModel _shellViewModel = new ShellViewModel();
        private readonly IDb4ODatabaseContext _databaseContext = new Db4ODatabaseContext();


        /// <summary>
        /// Gets the database context to use to manage the raw data operations of the system.
        /// </summary>
        public IDatabaseContext DatabaseContext
        {
            get { return _databaseContext; }
        }

        public object DataContext  { get; set; }


        /// <summary>
        /// Creates the logger the system will use.
        /// </summary>
        /// <returns>A new instance of <see cref="ILoggerFacade"/>.</returns>
        protected override ILoggerFacade CreateLogger()
        {
            var logger = MyLogger.Instance;

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                logger.Log("Unhandled exception. Below are the details:", Category.Warn, Priority.High);
                logger.Log(e.ExceptionObject.ToString(), Category.Exception, Priority.High);
            };

            return logger;
        }

        /// <summary>
        /// Configures the Microsoft.Practices.Unity.IUnityContainer.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

          

            Container.AddNewExtension<Interception>();

            // Register the cache members
            Container.RegisterInstance(CacheFactory.GetCacheManager("Cache"));
            Container.RegisterInstance<ICache>(new CachingBridge());

            // The dependencies container
            Container.RegisterInstance(Container);

            // Register the visual resources services
            Container.RegisterInstance<IVisualResourcesServices>((App)Application.Current);

            // The status bar services
            Container.RegisterInstance<IStatusBarServices>(_shellViewModel);

            // The database management members
            Container.RegisterType<IUnitOfWork, Db4OUnitOfWork>();
            //Container.RegisterType<IDb4ODatabaseContext, Db4ODatabaseContext>();
            //Container.RegisterType<IDb4ODatabaseContext, Db4ODatabaseContext>("SomeData");
            Container.RegisterInstance(DatabaseContext);
            Container.RegisterInstance((IDb4ODatabaseContext)DatabaseContext);

            //for DataBasePathContainer
            Container.RegisterInstance(new DataBasePathContainer() { DatabaseContext = (IDb4ODatabaseContext)DatabaseContext });

            // The Validation Block members
            Container.RegisterInstance<IValidatorFactory>(new EntLibValidatorFactory());

            // The Exception Handling Block members
            Container.RegisterType<ILogExceptionHandler, LogExceptionHandler>();
            Container.RegisterType<IReplaceHandler, ReplaceHandlerBridge>();
            Container.RegisterType<IEmailExceptionHandler, NullEmailExceptionHandler>();

            // The Atlas Configuration members
            //AtlasUser
            Container.RegisterType<IAtlasUser, AtlasUser>();
            Container.RegisterType<IAtlasUserDomainService, AtlasUserDomainService>();
            Container.RegisterType<IAtlasUserMangerApplicationService, AtlasUserMangerApplicationService>();
            Container.RegisterType<IAtlasUserRepository, AtlasUserRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<AtlasUser>, EntityFrameworkDbContext<AtlasUser>>();
            Container.RegisterType<IAtlasUserViewModel, AtlasUserViewModel>();
            Container.RegisterType<IAtlasUserPresenter, AtlasUserPresenter>();
            //AtlasModuleInfo
            Container.RegisterType<IAtlasModuleInfo, AtlasModuleInfo>();
            Container.RegisterType<IAtlasModuleInfoDomainService, AtlasModuleInfoDomainService>();
            Container.RegisterType<IAtlasModuleInfoMangerApplicationService, AtlasModuleInfoMangerApplicationService>();
            Container.RegisterType<IAtlasModuleInfoRepository, AtlasModuleInfoRepository>();
            Container.RegisterType<IEntityFrameworkDbContext<AtlasModuleInfo>, EntityFrameworkDbContext<AtlasModuleInfo>>();
            Container.RegisterType<IAtlasModuleInfoViewModel, AtlasModuleInfoViewModel>();
            Container.RegisterType<IAtlasModuleInfoPresenter, AtlasModuleInfoPresenter>();


            //Modules User Config
            Container.RegisterType<IAtlasModuleAccess, AtlasModuleAccess>();
            Container.RegisterType<IDomainServices<IAtlasModuleAccess>, CommonDomainService<IAtlasModuleAccess>>();
            Container.RegisterType<IAtlasModuleAccessManagerApplicationServices, AtlasModuleAccessManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleAccessRepository, AtlasModuleAccessRepository>();
            Container.RegisterType<IAtlasGenericModuleAccessPresenter<IAtlasModuleAccess>, AtlasModuleAccessPresenter>();
            Container.RegisterType<IAtlasModuleAccessViewModel, AtlasModuleAccessViewModel>();
            Container.RegisterType<IAtlasModuleAccessPresenter, AtlasModuleAccessPresenter>();
            //Container.RegisterType< IAtlasGenericModuleAccessViewModel<IAtlasModuleAccess>, AtlasModuleAccessViewModel>();


            Container.RegisterType<IAtlasMainModuleAccess, AtlasMainModuleAccess>();
            Container.RegisterType<IDomainServices<IAtlasMainModuleAccess>, CommonDomainService<IAtlasMainModuleAccess>>();
            Container
                .RegisterType<IAtlasMainModuleAccessManagerApplicationServices, AtlasMainModuleAccessManagerApplicationServices>();
            Container.RegisterType<IAtlasMainModuleAccessRepository, AtlasMainModuleAccessRepository>();
            Container.RegisterType<IAtlasMainModuleAccessViewModel, AtlasMainModuleAccessViewModel>();
            Container.RegisterType<IAtlasMainModuleAccessPresenter, AtlasMainModuleAccessPresenter>();
            //Container.RegisterType<IAtlasGenericModuleAccessViewModel<IAtlasMainModuleAccess>, AtlasGenericModuleAccessViewModel<IAtlasMainModuleAccess>>();
            //Container.RegisterType<IAtlasForMainModuleAccessViewModel, AtlasForMainModuleAccessViewModel>();
            //Container.RegisterType<IAtlasGenericModuleAccessPresenter<IAtlasMainModuleAccess>, AtlasGenericModuleAccessPresenter<IAtlasMainModuleAccess>>();
            //Container.RegisterType<IAtlasForMainModuleAccessPresenter, AtlasForMainModuleAccessPresenter>();

            Container
                .RegisterType
                <IAtlasGenericModuleAccessDomainServices<IAtlasMainModuleAccess>,
                    AtlasGenericModuleAccessDomainServices<IAtlasMainModuleAccess>>();
            Container
                .RegisterType
                <IAtlasGenericModuleAccessDomainServices<IAtlasModuleAccess>,
                    AtlasGenericModuleAccessDomainServices<IAtlasModuleAccess>>();

            Container.RegisterType<IAtlasModuleRole, AtlasModuleRole>();
            Container.RegisterType<IDomainServices<IAtlasModuleRole>, CommonDomainService<IAtlasModuleRole>>();
            Container.RegisterType<IAtlasModuleRoleManagerApplicationServices, AtlasModuleRoleManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleRoleRepository, AtlasModuleRoleRepository>();
            Container.RegisterType<IAtlasModuleRoleViewModel, AtlasModuleRoleViewModel>();
            Container.RegisterType<IAtlasModuleRolePresenter, AtlasModuleRolePresenter>();
            //Container.RegisterType<IAtlasModuleRoleViewModel<IAtlasMainModuleAccess>, AtlasModuleRoleViewModel<IAtlasMainModuleAccess>>();
            //Container.RegisterType<IAtlasModuleRolePresenter<IAtlasMainModuleAccess>, AtlasModuleRolePresenter<IAtlasMainModuleAccess>>();


            // for Genesis
            Container.RegisterType<IAtlasModuleMainSubject, AtlasModuleMainSubject>();
            Container.RegisterType<IAtlasModuleMainSubjectDomainServices, AtlasModuleMainSubjectDomainServices>();
            Container.RegisterType<IAtlasModuleMainSubjectManagerApplicationServices, AtlasModuleMainSubjectManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleMainSubjectRepository, AtlasModuleMainSubjectRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<AtlasModuleMainSubject>, EntityFrameworkDbContext<AtlasModuleMainSubject>>();
            Container.RegisterType<IAtlasModuleMainSubjectPresenter, AtlasModuleMainSubjectPresenter>();
            Container.RegisterType<IAtlasModuleMainSubjectViewModel, AtlasModuleMainSubjectViewModel>();

            Container.RegisterType<IAtlasModuleSubject, AtlasModuleSubject>();
            Container.RegisterType<IAtlasModuleSubjectDomainServices, AtlasModuleSubjectDomainServices>();
            Container.RegisterType<IAtlasModuleSubjectManagerApplicationServices, AtlasModuleSubjectManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleSubjectRepository, AtlasModuleSubjectRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<AtlasModuleSubject>, EntityFrameworkDbContext<AtlasModuleSubject>>();
            Container.RegisterType<IAtlasModuleSubjectPresenter, AtlasModuleSubjectPresenter>();
            Container.RegisterType<IAtlasModuleSubjectViewModel, AtlasModuleSubjectViewModel>();

            Container.RegisterType<IReferenceDocument, ReferenceDocument>();
            Container.RegisterType<IReferenceDocumentDomainService, ReferenceDocumentDomainService>();
            Container.RegisterType<IReferenceDocumentManagerApplicationServices, ReferenceDocumentManagerApplicationServices>();
            Container.RegisterType<IReferenceDocumentRepository, ReferenceDocumentRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<ReferenceDocument>, EntityFrameworkDbContext<ReferenceDocument>>();
            Container.RegisterType<IReferenceDocumentPresenter, ReferenceDocumentPresenter>();
            Container.RegisterType<IReferenceDocumentViewModel, ReferenceDocumentViewModel>();

            Container.RegisterType<ISubjectConcept, SubjectConcept>();
            Container.RegisterType<ISubjectConceptDomainServices, SubjectConceptDomainServices>();
            Container.RegisterType<ISubjectConceptRepository, SubjectConceptRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<SubjectConcept>, EntityFrameworkDbContext<SubjectConcept>>();
            Container.RegisterType<ISubjectConceptManagerApplicationServices, SubjectConceptManagerApplicationServices>();
            Container.RegisterType<ISubjectConceptViewModel, SubjectConceptViewModel>();
            Container.RegisterType<ISubjectConceptPresenter, SubjectConceptPresenter>();
            Container.RegisterType<ICommonRepository<ISubjectConcept>, CommonRepositoryEF<ISubjectConcept, SubjectConcept>>();
            Container.RegisterType<ISubjectConceptForNomenclatorViewModel, SubjectConceptForNomenclatorViewModel>();
            Container.RegisterType<INomenclatorPresenter<ISubjectConcept>, NomenclatorPresenter<ISubjectConcept>>();
            Container.RegisterType<INomenclatorRepository<ISubjectConcept>, NomenclatorRepositoryEF<ISubjectConcept, SubjectConcept>>();
            //Container.RegisterType<INomenclatorRepository<ISubjectConcept>, NomenclatorRepository<ISubjectConcept>>();
            Container.RegisterType<INomenclatorManagerApplicationServices<ISubjectConcept>, NomenclatorManagerApplicationServices<ISubjectConcept>>();
            Container.RegisterType<ICommonDomainService<ISubjectConcept>, CommonDomainService<ISubjectConcept>>();

            Container.RegisterType<ISubjectConceptDefinition, SubjectConceptDefinition>();
            Container.RegisterType<ISubjectConceptDefinitionDomainServices, SubjectConceptDefinitionDomainServices>();
            Container.RegisterType<ISubjectConceptDefinitionRepository, SubjectConceptDefinitionRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<SubjectConceptDefinition>, EntityFrameworkDbContext<SubjectConceptDefinition>>();
            Container.RegisterType<ISubjectConceptDefinitionManagerApplicationServices, SubjectConceptDefinitionManagerApplicationServices>();
            Container.RegisterType<ISubjectConceptDefinitionViewModel, SubjectConceptDefinitionViewModel>();
            Container.RegisterType<ISubjectConceptDefinitionPresenter, SubjectConceptDefinitionPresenter>();

            Container.RegisterType<ISubjectConceptExample, SubjectConceptExample>();
            Container.RegisterType<ISubjectConceptExampleDomainServices, SubjectConceptExampleDomainServices>();
            Container.RegisterType<ISubjectConceptExampleRepository, SubjectConceptExampleRepositoryEF>();

            Container.RegisterType<ISubjectConceptExampleManagerApplicationServices, SubjectConceptExampleManagerApplicationServices>();
            Container.RegisterType<ISubjectConceptExampleViewModel, SubjectConceptExampleViewModel>();
            Container.RegisterType<ISubjectConceptExamplePresenter, SubjectConceptExamplePresenter>();
            Container.RegisterType<IEntityFrameworkDbContext<SubjectConceptExample>, EntityFrameworkDbContext<SubjectConceptExample>>();
            Container.RegisterType<ISubjectConceptContent, SubjectConceptContent>();
            Container.RegisterType<IRelatedConcept, RelatedConcept>();
            Container.RegisterType<IRelatedConceptDomainServices, RelatedConceptDomainServices>();
            Container.RegisterType<IRelatedConceptRepository, RelatedConceptRepositoryEF>();
            Container.RegisterType<IEntityFrameworkDbContext<RelatedConcept>, EntityFrameworkDbContext<RelatedConcept>>();
            Container.RegisterType<IRelatedConceptManagerApplicationServices, RelatedConceptManagerApplicationServices>();
            Container.RegisterType<IRelatedConceptViewModel, RelatedConceptViewModel>();
            Container.RegisterType<IRelatedConceptPresenter, RelatedConceptPresenter>();

            // var atlasModuleSubjectsviewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            Container.RegisterInstance(new AtlasModuleMainSubjectViewModel());


            //Security
            Container.RegisterInstance(new AtlasSecurity());
            // DataAccess
            Container.RegisterInstance(new AtlasDataAccess());

            //Generals
            Container.RegisterType<IConvertibleEntityProvider<ICurrency>, ConvertibleEntityProvider<ICurrency>>();
            Container.RegisterType<IConvertibleEntityProvider<IMeasurementUnit>, ConvertibleEntityProvider<IMeasurementUnit>>();
            Container.RegisterType<IMeasurementUnitProvider, MeasurementUnitViewModel>();
            Container.RegisterType<ICurrencyProvider, CurrencyViewModel>();
            Container.RegisterType<IMeasurementUnitViewModel, MeasurementUnitViewModel>();
            Container.RegisterType<ICurrencyViewModel, CurrencyViewModel>();
            Container.RegisterType<IRepository<INomenclator>, StandarDb4ORepository<INomenclator>>();

            // And lastly the view model for the shell
            ViewModelLocationProvider.Register(typeof(Shell).FullName, () => _shellViewModel);
            //var _measurementUnitviewModel = GetMeasurementUnitViewModel();
            //var _currencyviewModel = GetCurrencyViewModel();

            //ViewModelLocationProvider.Register(typeof(MeasurementUnitEditor).FullName, () => _measurementUnitviewModel);
            //ViewModelLocationProvider.Register(typeof(CurrencyEditor).FullName, () => _currencyviewModel);

            //var userViewModel = CreateAndInitialize<IAtlasUserViewModel>();
            //ViewModelLocationProvider.Register(typeof(AtlasUserManagePage).FullName, () => userViewModel);

            Logger.Log("Configured the container", Category.Debug, Priority.Low);
        }

        private void Init()
        {
            Container.AddNewExtension<Interception>();

            // Register the cache members
            Container.RegisterInstance(CacheFactory.GetCacheManager("Cache"));
            Container.RegisterInstance<ICache>(new CachingBridge());

            // The dependencies container
            Container.RegisterInstance(Container);

            // Register the visual resources services
            Container.RegisterInstance<IVisualResourcesServices>((App) Application.Current);

            // The status bar services
            Container.RegisterInstance<IStatusBarServices>(_shellViewModel);

            // The database management members
            Container.RegisterType<IUnitOfWork, Db4OUnitOfWork>();
            //Container.RegisterType<IDb4ODatabaseContext, Db4ODatabaseContext>();
            //Container.RegisterType<IDb4ODatabaseContext, Db4ODatabaseContext>("SomeData");
            Container.RegisterInstance(DatabaseContext);
            Container.RegisterInstance((IDb4ODatabaseContext) DatabaseContext);

            //for DataBasePathContainer
            Container.RegisterInstance(new DataBasePathContainer() {DatabaseContext = (IDb4ODatabaseContext) DatabaseContext});

            // The Validation Block members
            Container.RegisterInstance<IValidatorFactory>(new EntLibValidatorFactory());

            // The Exception Handling Block members
            Container.RegisterType<ILogExceptionHandler, LogExceptionHandler>();
            Container.RegisterType<IReplaceHandler, ReplaceHandlerBridge>();
            Container.RegisterType<IEmailExceptionHandler, NullEmailExceptionHandler>();

            // The Atlas Configuration members
            //AtlasUser
            Container.RegisterType<IAtlasUser, AtlasUser>();
            Container.RegisterType<IAtlasUserDomainService, AtlasUserDomainService>();
            Container.RegisterType<IAtlasUserMangerApplicationService, AtlasUserMangerApplicationService>();
            Container.RegisterType<IAtlasUserRepository, AtlasUserRepository>();
            Container.RegisterType<IAtlasUserViewModel, AtlasUserViewModel>();
            Container.RegisterType<IAtlasUserPresenter, AtlasUserPresenter>();

            //Modules User Config
            Container.RegisterType<IAtlasModuleAccess, AtlasModuleAccess>();
            Container.RegisterType<IDomainServices<IAtlasModuleAccess>, CommonDomainService<IAtlasModuleAccess>>();
            Container.RegisterType<IAtlasModuleAccessManagerApplicationServices, AtlasModuleAccessManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleAccessRepository, AtlasModuleAccessRepository>();
            Container.RegisterType<IAtlasGenericModuleAccessPresenter<IAtlasModuleAccess>, AtlasModuleAccessPresenter>();
            Container.RegisterType<IAtlasModuleAccessViewModel, AtlasModuleAccessViewModel>();
            Container.RegisterType<IAtlasModuleAccessPresenter, AtlasModuleAccessPresenter>();
            //Container.RegisterType< IAtlasGenericModuleAccessViewModel<IAtlasModuleAccess>, AtlasModuleAccessViewModel>();


            Container.RegisterType<IAtlasMainModuleAccess, AtlasMainModuleAccess>();
            Container.RegisterType<IDomainServices<IAtlasMainModuleAccess>, CommonDomainService<IAtlasMainModuleAccess>>();
            Container
                .RegisterType<IAtlasMainModuleAccessManagerApplicationServices, AtlasMainModuleAccessManagerApplicationServices>
                ();
            Container.RegisterType<IAtlasMainModuleAccessRepository, AtlasMainModuleAccessRepository>();
            Container.RegisterType<IAtlasMainModuleAccessViewModel, AtlasMainModuleAccessViewModel>();
            Container.RegisterType<IAtlasMainModuleAccessPresenter, AtlasMainModuleAccessPresenter>();
            //Container.RegisterType<IAtlasGenericModuleAccessViewModel<IAtlasMainModuleAccess>, AtlasGenericModuleAccessViewModel<IAtlasMainModuleAccess>>();
            //Container.RegisterType<IAtlasForMainModuleAccessViewModel, AtlasForMainModuleAccessViewModel>();
            //Container.RegisterType<IAtlasGenericModuleAccessPresenter<IAtlasMainModuleAccess>, AtlasGenericModuleAccessPresenter<IAtlasMainModuleAccess>>();
            //Container.RegisterType<IAtlasForMainModuleAccessPresenter, AtlasForMainModuleAccessPresenter>();

            Container
                .RegisterType
                <IAtlasGenericModuleAccessDomainServices<IAtlasMainModuleAccess>,
                    AtlasGenericModuleAccessDomainServices<IAtlasMainModuleAccess>>();
            Container
                .RegisterType
                <IAtlasGenericModuleAccessDomainServices<IAtlasModuleAccess>,
                    AtlasGenericModuleAccessDomainServices<IAtlasModuleAccess>>();

            Container.RegisterType<IAtlasModuleRole, AtlasModuleRole>();
            Container.RegisterType<IDomainServices<IAtlasModuleRole>, CommonDomainService<IAtlasModuleRole>>();
            Container.RegisterType<IAtlasModuleRoleManagerApplicationServices, AtlasModuleRoleManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleRoleRepository, AtlasModuleRoleRepository>();
            Container.RegisterType<IAtlasModuleRoleViewModel, AtlasModuleRoleViewModel>();
            Container.RegisterType<IAtlasModuleRolePresenter, AtlasModuleRolePresenter>();
            //Container.RegisterType<IAtlasModuleRoleViewModel<IAtlasMainModuleAccess>, AtlasModuleRoleViewModel<IAtlasMainModuleAccess>>();
            //Container.RegisterType<IAtlasModuleRolePresenter<IAtlasMainModuleAccess>, AtlasModuleRolePresenter<IAtlasMainModuleAccess>>();


            // for Genesis
            Container.RegisterType<IAtlasModuleMainSubject, AtlasModuleMainSubject>();
            Container.RegisterType<IAtlasModuleMainSubjectDomainServices, AtlasModuleMainSubjectDomainServices>();
            Container
                .RegisterType
                <IAtlasModuleMainSubjectManagerApplicationServices, AtlasModuleMainSubjectManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleMainSubjectRepository, AtlasModuleMainSubjectRepository>();
            Container.RegisterType<IAtlasModuleMainSubjectPresenter, AtlasModuleMainSubjectPresenter>();
            Container.RegisterType<IAtlasModuleMainSubjectViewModel, AtlasModuleMainSubjectViewModel>();

            Container.RegisterType<IAtlasModuleSubject, AtlasModuleSubject>();
            Container.RegisterType<IAtlasModuleSubjectDomainServices, AtlasModuleSubjectDomainServices>();
            Container.RegisterType<IAtlasModuleSubjectManagerApplicationServices, AtlasModuleSubjectManagerApplicationServices>();
            Container.RegisterType<IAtlasModuleSubjectRepository, AtlasModuleSubjectRepository>();
            Container.RegisterType<IAtlasModuleSubjectPresenter, AtlasModuleSubjectPresenter>();
            Container.RegisterType<IAtlasModuleSubjectViewModel, AtlasModuleSubjectViewModel>();

            Container.RegisterType<IReferenceDocument, ReferenceDocument>();
            Container.RegisterType<IReferenceDocumentDomainService, ReferenceDocumentDomainService>();
            Container.RegisterType<IReferenceDocumentManagerApplicationServices, ReferenceDocumentManagerApplicationServices>();
            Container.RegisterType<IReferenceDocumentRepository, ReferenceDocumentRepository>();
            Container.RegisterType<IReferenceDocumentPresenter, ReferenceDocumentPresenter>();
            Container.RegisterType<IReferenceDocumentViewModel, ReferenceDocumentViewModel>();

            Container.RegisterType<ISubjectConcept, SubjectConcept>();
            Container.RegisterType<ISubjectConceptDomainServices, SubjectConceptDomainServices>();
            Container.RegisterType<ISubjectConceptRepository, SubjectConceptRepository>();
            Container.RegisterType<ISubjectConceptManagerApplicationServices, SubjectConceptManagerApplicationServices>();
            Container.RegisterType<ISubjectConceptViewModel, SubjectConceptViewModel>();
            Container.RegisterType<ISubjectConceptPresenter, SubjectConceptPresenter>();
            Container.RegisterType<ICommonRepository<ISubjectConcept>, CommonRepositoryEF<ISubjectConcept, SubjectConcept>>();
            Container.RegisterType<ISubjectConceptForNomenclatorViewModel, SubjectConceptForNomenclatorViewModel>();
            Container.RegisterType<INomenclatorPresenter<ISubjectConcept>, NomenclatorPresenter<ISubjectConcept>>();
            Container.RegisterType<INomenclatorRepository<ISubjectConcept>, NomenclatorRepositoryEF<ISubjectConcept, SubjectConcept>>();
            Container
                .RegisterType
                <INomenclatorManagerApplicationServices<ISubjectConcept>, NomenclatorManagerApplicationServices<ISubjectConcept>
                >();
            Container.RegisterType<ICommonDomainService<ISubjectConcept>, CommonDomainService<ISubjectConcept>>();

            Container.RegisterType<ISubjectConceptDefinition, SubjectConceptDefinition>();
            Container.RegisterType<ISubjectConceptDefinitionDomainServices, SubjectConceptDefinitionDomainServices>();
            Container.RegisterType<ISubjectConceptDefinitionRepository, SubjectConceptDefinitionRepository>();
            Container
                .RegisterType
                <ISubjectConceptDefinitionManagerApplicationServices, SubjectConceptDefinitionManagerApplicationServices>();
            Container.RegisterType<ISubjectConceptDefinitionViewModel, SubjectConceptDefinitionViewModel>();
            Container.RegisterType<ISubjectConceptDefinitionPresenter, SubjectConceptDefinitionPresenter>();

            Container.RegisterType<ISubjectConceptExample, SubjectConceptExample>();
            Container.RegisterType<ISubjectConceptExampleDomainServices, SubjectConceptExampleDomainServices>();
            Container.RegisterType<ISubjectConceptExampleRepository, SubjectConceptExampleRepository>();
            Container
                .RegisterType<ISubjectConceptExampleManagerApplicationServices, SubjectConceptExampleManagerApplicationServices>
                ();
            Container.RegisterType<ISubjectConceptExampleViewModel, SubjectConceptExampleViewModel>();
            Container.RegisterType<ISubjectConceptExamplePresenter, SubjectConceptExamplePresenter>();

            Container.RegisterType<ISubjectConceptContent, SubjectConceptContent>();
            Container.RegisterType<IRelatedConcept, RelatedConcept>();
            Container.RegisterType<IRelatedConceptDomainServices, RelatedConceptDomainServices>();
            Container.RegisterType<IRelatedConceptRepository, RelatedConceptRepository>();
            Container.RegisterType<IRelatedConceptManagerApplicationServices, RelatedConceptManagerApplicationServices>();
            Container.RegisterType<IRelatedConceptViewModel, RelatedConceptViewModel>();
            Container.RegisterType<IRelatedConceptPresenter, RelatedConceptPresenter>();

            // var atlasModuleSubjectsviewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            Container.RegisterInstance(new AtlasModuleMainSubjectViewModel());


            //Security
            Container.RegisterInstance(new AtlasSecurity());

            //Generals
            Container.RegisterType<IConvertibleEntityProvider<ICurrency>, ConvertibleEntityProvider<ICurrency>>();
            Container.RegisterType<IConvertibleEntityProvider<IMeasurementUnit>, ConvertibleEntityProvider<IMeasurementUnit>>();
            Container.RegisterType<IMeasurementUnitProvider, MeasurementUnitViewModel>();
            Container.RegisterType<ICurrencyProvider, CurrencyViewModel>();
            Container.RegisterType<IMeasurementUnitViewModel, MeasurementUnitViewModel>();
            Container.RegisterType<ICurrencyViewModel, CurrencyViewModel>();
            Container.RegisterType<IRepository<INomenclator>, StandarDb4ORepository<INomenclator>>();

            // And lastly the view model for the shell
            ViewModelLocationProvider.Register(typeof(Shell).FullName, () => _shellViewModel);
            //var _measurementUnitviewModel = GetMeasurementUnitViewModel();
            //var _currencyviewModel = GetCurrencyViewModel();

            //ViewModelLocationProvider.Register(typeof(MeasurementUnitEditor).FullName, () => _measurementUnitviewModel);
            //ViewModelLocationProvider.Register(typeof(CurrencyEditor).FullName, () => _currencyviewModel);

            //var userViewModel = CreateAndInitialize<IAtlasUserViewModel>();
            //ViewModelLocationProvider.Register(typeof(AtlasUserManagePage).FullName, () => userViewModel);

            Logger.Log("Configured the container", Category.Debug, Priority.Low);
        }


        /// <summary>
        /// Gets a new instance of a crud view model and loads it.
        /// </summary>
        /// <returns>A new and initialized instance of <typeparamref name="TViewModel"/>.</returns>
        protected TViewModel CreateAndInitialize<TViewModel>()
            where TViewModel : ICrudViewModel
        {
            var viewModel = Container.Resolve<TViewModel>();

            viewModel.Raised += OnInteractionRequested;
             viewModel.Load();
          //  viewModel.Raised -= OnInteractionRequested;

            return viewModel;
        }
        protected internal virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            InteractionRequestHelpers.Notify(e);
        }
        //protected internal virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        //{
        //    ((IView)sender).Notify(e);
        //}

        /// <summary>
        /// Returns the created shell window.
        /// </summary>
        /// <returns>A new instance of a Shell window.</returns>
        protected override DependencyObject CreateShell()
        {
            _shell = Container.Resolve<Shell>();

            // Register the navigation services
            Container.RegisterInstance<INavigationServices>(_shell.AtlasModuleView);

            Logger.Log("Created shell", Category.Debug, Priority.Low);

            return _shell;
        }

        /// <summary>
        /// Initializes the created shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            try
            {
                InitializeModules();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, Category.Debug, Priority.Low);
            }
              InitializeModules();
           
           _shell.ModuleCatalog = ModuleCatalog;
            _shell.UnityBootstrapper = this;
            _shell.LoginMediaPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                Path.Combine(Settings.Default.VideoMediaRelativePath,Settings.Default.VideoMediaFile));
            _shell.LoginImgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(Settings.Default.ImgMediaRelativePath, Settings.Default.ImgMediaFile));
            _shell.ModulePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.ModulePath);

            _shell.Bootstrapper = this;
            //_shell.ModuleCatalog.AddModule(new ModuleInfo("Investment",null));
            //for Interaction
            _shell.Name = "AtlasWindows";
            Container.RegisterInstance(_shell as Window);
            // for anyone who needs
            Container.RegisterInstance(ModuleCatalog as ModuleCatalog);
            Application.Current.MainWindow = _shell;
            Application.Current.MainWindow.Show();

            Logger.Log("Initialized shell", Category.Debug, Priority.Low);
        }

        /// <summary>
        /// Creates the module catalog used by Prism to locate the modules.
        /// </summary>
        /// <returns>A new module catalog.</returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = new DirectoryModuleCatalog { ModulePath = GetFullPath(Settings.Default.ModulePath) };
         
            Logger.Log("Created module catalog", Category.Debug, Priority.Low);
          
            return moduleCatalog;
        }

        private static string GetFullPath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

       }
}
