using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Effects;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Contracts.Presentation.Visuals;
using CompanyName.Atlas.Contracts.Presentation.Visuals.Structures;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Implementation of the base contract <see cref="IBudgetComponentItemManagerApplicationServices{TItem, TComponent}"/> representing
    /// the application services to handle the coming CRUD-operation request from upper layers regarding to budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of budget component item to manage.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which belong the items.</typeparam>
    /// <typeparam name="TRepository">The type of the repository required to make the data oeprations.</typeparam>
    /// <typeparam name="TDomainServices">The type of the domain services used to ensure the business rules for the items.</typeparam>
    public abstract class BudgetComponentItemManagerApplicationServicesBase<TItem, TRepository, TDomainServices> :
        ItemManagerApplicationServicesBase<TItem, TRepository, TDomainServices>,
        IBudgetComponentItemManagerApplicationServices<TItem>
        where TItem : class, IBudgetComponentItem
        //where TComponent : class, IEntity
        where TRepository : IBudgetComponentItemRepository<TItem>
        where TDomainServices : IBudgetComponentItemDomainServices<TItem>
    {


      
       

       
        /// <summary>
        /// Gets the key to cache the result for the given method, using it and its arguments.
        /// </summary>
        /// <param name="method">The <see cref="MethodBase"/> to generate a key for.</param>
        /// <param name="arguments">The arguments currently being passed to the method.</param>
        /// <returns>The key for the method's result.</returns>
        public override string GetKeyFor(MethodBase method, params object[] arguments)
        {
            string baseKey = base.GetKeyFor(method, arguments);

            return "{0}".EasyFormat(baseKey);
        }

        /// <summary>
        /// Determines whether there can be narrowed the set of the budget component items by leaving only the ones with the given
        /// specification in their names.
        /// </summary>
        /// <param name="nameSpecification">
        /// A <see cref="string"/> being the criteria that must match the name of the budget component items in order to be
        /// returned.
        /// </param>
        /// <returns>True.</returns>
        [CachesResult]
        public bool CanFilter(string nameSpecification)
        {
            return true;
        }

        /// <summary>
        /// Gets the budget component items which names match the given specification.
        /// </summary>
        /// <param name="nameSpecification">The criteria to be matched by the budget component items in order to be returned.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> matching <paramref name="nameSpecification"/>.</returns>
        [CachesResult]
        public IEnumerable<TItem> Filter(string nameSpecification)
        {
            return Repository.FilterByName(nameSpecification);
        }

        
        /// <summary>
        /// adquiere properties from another IBudgetComponentItem
        /// </summary>
        /// <param name="onAdquiring"></param>
        /// <param name="toAdquire"></param>
        /// <returns></returns>
        public virtual TItem AdquireProperties(TItem onAdquiring, TItem toAdquire)
            //where TOther: class,IBudgetComponentItem
        {


            if (Equals(onAdquiring, null) || Equals(toAdquire, null))
                return null;
           // var resourceDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<TItem>>();

           

         

            var resourceRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<TItem>>();
            resourceRepoSevice.Component =  onAdquiring;
            resourceRepoSevice.DeleteAll();

            //var resourceRepoSeviceOther = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<TItem>>();

            resourceRepoSevice.Component = toAdquire;
                foreach (IPlannedResource plannedResource in resourceRepoSevice.Entities)
                {
                    var recursiveDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<TItem>>();
                     recursiveDomainSevice.Component = onAdquiring;
                      var recursiveApplicationSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<TItem>>();
                    recursiveApplicationSevice.Component = onAdquiring;
                    var recursiveRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<TItem>>();
                    recursiveRepoSevice.Component =  onAdquiring;
                    var newAquiredResource = recursiveApplicationSevice.AdquireProperties(
                       recursiveRepoSevice.Add(recursiveDomainSevice.Create()), plannedResource);
                    
                 //   recursiveRepoSevice.Update(newAquiredResource);
                }


            //}




            onAdquiring = SetAdquiring(onAdquiring, toAdquire);

           
            return onAdquiring;
       

            
        }

        protected virtual TItem SetAdquiring(TItem onAdquiring, TItem toAdquire)
        {

            onAdquiring.Name = toAdquire.Name;
            onAdquiring.Description = toAdquire.Description;
            onAdquiring.Code = toAdquire.Code;
            onAdquiring.MeasurementUnit = toAdquire.MeasurementUnit;
            onAdquiring.Currency = toAdquire.Currency;
           // onAdquiring.Quantity = toAdquire.Quantity;
            onAdquiring.SubExpenseConcept = toAdquire.SubExpenseConcept;
            onAdquiring.Category = toAdquire.Category;
            onAdquiring.UnitaryCost = toAdquire.UnitaryCost;
            onAdquiring.PriceSystem = toAdquire.PriceSystem;

            return onAdquiring;
        }

        /// <summary>
        /// convert the cost given <see cref="IBudgetComponentItem"/> to match the actual one based on the currency convert factor
        /// </summary>
        /// <param name="budgetComponentItem"></param>
        /// <returns></returns>
        public decimal CurrencyConvert(ICurrency currency, ICurrenciable currenciable, decimal Cost = Decimal.MinValue)
        {
            // var provider = ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<ICurrency>>();
            var foreingCurrency = currenciable.Currency;
            var localCurrency = currency;

            // if there is a lack of values, dont do a thing
            if (foreingCurrency == null || localCurrency == null)
                return currenciable.Cost;

            //get convertion service
            var converionService = ServiceLocator.Current.GetInstance<IUnitConverterManagerApplicationServices>();
            converionService.ConversionForEntity = currency;

            // find the convertion
            var convertion =
                converionService.Items.SingleOrDefault(
                    x =>
                        x.ConversionForEntityId != null &&
                        x.ConversionForEntityId.ToString() == foreingCurrency.ToString() &&
                        x.ConversionUnit != null &&
                        x.Id.ToString() == localCurrency.Id.ToString());


            if (convertion == null)
            {

                //may be its defined for the localCurrency
                converionService.ConversionForEntity = localCurrency;
                convertion =
                converionService.Items.SingleOrDefault(
                    x =>
                        x.ConversionForEntityId != null &&
                        x.ConversionForEntityId.ToString() == localCurrency.ToString() &&
                        x.ConversionUnit != null &&
                        x.ConversionUnit.ToString() == foreingCurrency.ToString());

                if (convertion == null && Cost==0)
                    return currenciable.Cost;

                if (convertion == null)
                    return Cost;
            }

            return Cost==0? currenciable.Cost * convertion.Factor: Cost * convertion.Factor;

        }

        public TItem ExportRelated(IDatabaseContext exportDatabaseContext, TItem item)
        {

            var measurementService =
                         ServiceLocator.Current.GetInstance<IMeasurementUnitManagerApplicationServices>();
            item.MeasurementUnit = measurementService.Export(exportDatabaseContext,
                measurementService.Find(item.MeasurementUnit))?.Id;
            var currencyService =
               ServiceLocator.Current.GetInstance<ICurrencyManagerApplicationServices>();
            item.Currency = currencyService.Export(exportDatabaseContext,
                currencyService.Find(item.Currency))?.Id;

            var subExpenseConceptService =
                        ServiceLocator.Current.GetInstance<ISubExpenseConceptManagerApplicationServices>();
            item.SubExpenseConcept = subExpenseConceptService.Export(exportDatabaseContext,
                subExpenseConceptService.Find(item.SubExpenseConcept))?.Id;

            var categoriesService =
               ServiceLocator.Current.GetInstance<ICategoryManagerApplicationServices>();
            item.Category = categoriesService.Export(exportDatabaseContext,
                categoriesService.Find(item.Category))?.Id;

            return item;
        }

        public abstract TItem Export(IDatabaseContext exportDatabaseContext, TItem item);

      
        public override void Update(TItem entity)
        {
            var repoEntity = Repository.Find(entity.Id);
            //var code = entity.Code;
            //var quantity = entity.Quantity;

            base.Update(entity);


            CommonCheck(entity, repoEntity);
           
        }

        private void CommonCheck(TItem entity, TItem repoEntity)
        {
            if (!Equals(repoEntity, null) && repoEntity.Code == entity.Code && repoEntity.Quantity == entity.Quantity)
            {
                if (!Equals(entity.SubExpenseConcept, repoEntity.SubExpenseConcept)) // if SubExpenseConcept Changed
                    return;
                if (!Equals(entity.Category, repoEntity.Category)) // if Category Changed
                    return;
                


                Check4Spread(entity,repoEntity);
            }
        }
        protected virtual void Check4Spread(TItem entity, TItem repoEntity)
        {
            
                AtlasModuleView navigationServices =
                    ServiceLocator.Current.GetInstance<INavigationServices>() as AtlasModuleView;
                if (navigationServices != null)
                    navigationServices.StatusBar = new StatusBarConfirmationView()
                    {
                        Command = new DelegateCommand<TItem>(ExecuteMethod, CanExecuteMethod),
                        DataContext = entity
                    };
            
        }

        protected bool CanExecuteMethod(IBudgetComponentItem item)
        {
            return true;
        }

        protected void ExecuteMethod(IBudgetComponentItem item)
        {
          
               
             Spread(item);

        }

        protected void Spread( IBudgetComponentItem item)//string path
        {
            //var dataContext =  ServiceLocator.Current.GetInstance<IDatabaseContext>();
            //dataContext = new Db4ODatabaseContext(path);

            var mainWindow = (Window)ServiceLocator.Current.GetInstance(typeof(Window));
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.Effect = blurEffect;
            // var crap = mainWindow.Name;
            // var response = new InteractionStructure() { Text = "Export Content", Title = "Export" };
            var spreadDialog = new SpreadChangesPlattform() { Owner = mainWindow};
            spreadDialog.ExportableViewModels = new List<NamedCrudViewModel>();
            spreadDialog.ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.Projects, ViewModel = ServiceLocator.Current.GetInstance<IInvestmentViewModel>() });
            spreadDialog.ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.PriceSystem, ViewModel = ServiceLocator.Current.GetInstance<IPriceSystemViewModel>() });
            spreadDialog.DataContext = item;
            //   exportDialog.ExportebleViewModels = ((AtlasOptionalContent) DataContext).ExportableViewModels;
            spreadDialog.ShowDialog();
            // Display the Message Box to the user with information gathered
            // var response = MessageBox.Show(text, title, MessageBoxButton.YesNo, icon);

            // remove blur effect
            mainWindow.Effect = null;
        }

        public decimal GetMyCost(TItem item, ICurrency currency)
        {
            var resourceAppService = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<TItem>>();
            resourceAppService.Component = item;
            var items = resourceAppService.Items;
            if (items.Any())
            {
                var check  = items.Sum(x => CurrencyConvert(currency, x, GetMyCostInResources(x, currency)));
                return check;
            }
               
            return item.Cost;
        }

        public decimal GetMyCostInResources(IPlannedResource resource, ICurrency currency)
        {
            var resourceAppService = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedResource>>();
            resourceAppService.Component = resource;
            var items = resourceAppService.Items;
            if (resourceAppService.Items.Any())
            {
                var check = resourceAppService.Items.Sum(x => CurrencyConvert(currency, x, GetMyCostInResources(x, currency)));
                return check;
            }
                
            return resource.Cost;
        }

        public void FreeUpdate(TItem budItem) 
        {
            Repository.Update(budItem);
        }
    }
}
