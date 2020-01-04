using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{   
    /// <summary>
    ///     Implements some handling data operations for
    ///    section  domain entities.
    /// </summary>
    public class SectionRepository : Db4ORepositoryBase<ISection>, ISectionRepository
    {
        private IPriceSystem _aboveSection;
       
        /// <summary>
        ///     Initializes a new instance of <see cref="ISection" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public SectionRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets the reference to the <see cref="IInvestmentElement" /> containing the investment components handled in the
        ///     current repository.
        /// </summary>
        public IPriceSystem AboveSection
        {
            get
            {
                if (_aboveSection == null)
                    throw new ArgumentException(Resources.InitializeInvestmentElementBeforeUsingIt);

                return _aboveSection;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _aboveSection = value;
            }
        }
        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description)
                }).ToArray();
            }
        }
        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<ISection> Entities
        {
            get
            {
                var specification = new SectionOfSpecification(AboveSection);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override ISection Add(ISection section)
        {
            ISection addedSection = base.Add(section);

            this.Relate(section, AboveSection, DatabaseContext);

            return section;
        }
        public void Delete(ISection section)
        {
            if (section == null)
                throw new ArgumentNullException("priceSystem");

            this.Unrelate(section,AboveSection,DatabaseContext);

            var dbSecions = DatabaseContext.Find<ISection>(section.Id);
            foreach (ISection sec in dbSecions.Sections.ToArray())
                Delete(sec);
            foreach (IPlannedActivity plannedActivity in dbSecions.PlannedActivities.ToArray())
                Delete(plannedActivity);

            DatabaseContext.Delete((object)dbSecions.Sections);
            DatabaseContext.Delete(dbSecions);
        }

         public void Delete(IPlannedActivity plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");

            var dbSecions = DatabaseContext.Find<IPlannedActivity>(plannedActivity.Id);
            foreach (IPlannedResource sec in dbSecions.PlannedResources.ToArray())
                Delete(sec);
           

            DatabaseContext.Delete((object)dbSecions.PlannedResources);
            DatabaseContext.Delete(dbSecions);
        }

         public void Delete(IPlannedResource plannedResource)
         {
             if (plannedResource == null)
                 throw new ArgumentNullException("plannedResource");

             var dbSecions = DatabaseContext.Find<IPlannedResource>(plannedResource.Id);
             foreach (IPlannedResource sec in dbSecions.PlannedResources.ToArray())
                 Delete(sec);


             DatabaseContext.Delete((object)dbSecions.PlannedResources);
             DatabaseContext.Delete(dbSecions);
         }

        /// <summary>
        /// When overridden in a deriver it returns the list of items of the budget component that the items managed here will added
        /// or removed from.
        /// </summary>
        //private Func<IPriceSystem, IList<ISection>> GetItemCollection
        //{
        //    get { return x => x.Sections; }
        //}
         /// <summary>
         /// Relates a item with its budget component.
         /// </summary>
         /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
         /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
         /// <exception cref="ArgumentNullException">
         /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
         /// </exception>
         public virtual void Relate(ISection section, IPriceSystem priceSystem)
         {
             if (section == null)
                 throw new ArgumentNullException("section");
             if (priceSystem == null)
                 throw new ArgumentNullException("priceSystem");

             section.AboveSection = priceSystem;
             //IList<ISection> itemCollection = GetItemCollection(priceSystem);
             //if (itemCollection.All(x => !Equals(x.Id, section.Id)))
             //    itemCollection.Add(section);
         }

         /// <summary>
         /// Breaks the relation of the given item with its budget component.
         /// </summary>
         /// <param name="budgetComponentItem">The item to break its relation to <paramref name="budgetComponent"/>.</param>
         /// <param name="budgetComponent">The budget component to break its relation to <paramref name="budgetComponentItem"/>.</param>
         /// <exception cref="ArgumentNullException">
         /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
         /// </exception>
         public virtual void Unrelate(ISection section, IPriceSystem priceSystem)
         {
             if (section == null)
                 throw new ArgumentNullException("section");
             if (priceSystem == null)
                 throw new ArgumentNullException("priceSystem");

             // budgetComponentItem.Component = null;
             //IList<ISection> itemCollection = GetItemCollection(priceSystem);
             //if (itemCollection.Any(x => Equals(x.Id, section.Id)))
             //    itemCollection.Remove(section);
         }

         /// <summary>
         /// Saves the changes made to the references of the given budget component item.
         /// </summary>
         /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
         /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
         public virtual void SaveReference(ISection section)
         {
             if (section == null)
                 throw new ArgumentNullException("section");

             DatabaseContext.Store(section);
         }

        public void SaveReference(IPriceSystem other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }
    }
}
