using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentComponentRepository" /> representing the repository used to
    ///     handle the data operations for the investment components of a certain investment element.
    /// </summary>
    public class InvestmentComponentRepository :
        InvestmentElementRepositoryBase<IInvestmentComponent>,
        IInvestmentComponentRepository
    {
        private IInvestmentElement _parent;

        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentComponentRepository" /> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        ///     The <see cref="IDb4ODatabaseContext" /> representing the Db4O database context used to carry on with the row data
        ///     operations.
        /// </param>
        public InvestmentComponentRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the reference to the <see cref="IInvestmentElement" /> containing the investment components handled in the
        ///     current repository.
        /// </summary>
        public IInvestmentElement InvestmentElement
        {
            get
            {
                if (_parent == null)
                    throw new ArgumentException(Resources.InitializeInvestmentElementBeforeUsingIt);

                return _parent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _parent = value;
            }
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IInvestmentComponent> Entities
        {
            get
            {
                var specification = new InvestmentElementsOfSpecification(InvestmentElement);

                return Where(specification);
            }
        }


        /// <summary>
        ///     Adds the given investment component to the current repository.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentComponent" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentComponent" /> does not have an <see cref="IBudget" />.</exception>
        /// <returns>The added <see cref="IInvestmentComponent"/> that was just added.</returns>
        public override IInvestmentComponent Add(IInvestmentComponent investmentComponent)
        {
            IInvestmentComponent dbComponent = base.Add(investmentComponent);
            this.Relate(investmentComponent, InvestmentElement, DatabaseContext);

            return dbComponent;
        }

        /// <summary>
        /// Deletes the given investment component from the current repository.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentComponent" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentComponent" /> has a null budget.</exception>
        public override void Delete(IInvestmentComponent investmentComponent)
        {
            if (investmentComponent == null)
                throw new ArgumentNullException("investmentComponent");
            if (investmentComponent.Budget == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentComponent");

            var dbInvestment = DatabaseContext.Find<IInvestmentComponent>(investmentComponent.Id);
            
            this.Unrelate(investmentComponent, InvestmentElement, DatabaseContext);

            var investmentComponentRepository = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            investmentComponentRepository.InvestmentElement = investmentComponent;
            investmentComponentRepository.DeleteAll();

            base.Delete(investmentComponent);

            DatabaseContext.Delete((object)dbInvestment.Elements);
        }

        /// <summary>
        ///     Relates the given investment component with the given investment, making it its parent.
        /// </summary>
        /// <param name="investmentComponent">
        ///     The <see cref="IInvestmentComponent" /> to relate to its parent:
        ///     <paramref name="parent" />.
        /// </param>
        /// <param name="parent">The <see cref="IInvestment" /> to relate with its child: <paramref name="investmentComponent" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentComponent" /> or <paramref name="parent" /> is null.
        /// </exception>
        public void Relate(IInvestmentComponent investmentComponent, IInvestmentElement parent)
        {
            if (investmentComponent == null)
                throw new ArgumentNullException("investmentComponent");
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (parent.Elements.Any(x => Equals(investmentComponent.Id, x.Id)))
                return;

            parent.Elements.Add(investmentComponent);
            investmentComponent.Parent = parent;
        }

        /// <summary>
        ///     Breaks the relations between the given investment component and its parent investment.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent" /> to unrelate.</param>
        /// <param name="parent">The <see cref="IInvestmentElement" /> to unrelate.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentComponent" /> or <paramref name="parent" /> is null.
        /// </exception>
        public void Unrelate(IInvestmentComponent investmentComponent, IInvestmentElement parent)
        {
            if (investmentComponent == null)
                throw new ArgumentNullException("investmentComponent");
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (parent.Elements.All(x => !Equals(investmentComponent.Id, x.Id)))
                return;

            parent.Elements.Remove(investmentComponent);
            investmentComponent.Parent = null;
        }

        /// <summary>
        ///     Saves the references of the given investment component.
        /// </summary>
        /// <param name="parent">The <see cref="IInvestmentElement" /> to save references of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parent"/> is null.</exception>
        public void SaveReference(IInvestmentElement parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            DatabaseContext.Store(parent);
            DatabaseContext.Store(parent.Elements);
        }
    }


    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentComponentRepository" /> representing the repository used to
    ///     handle the data operations for the investment components of a certain investment element.
    /// </summary>
    public class InvestmentComponentRepositoryEF :
        InvestmentElementRepositoryBaseEF<IInvestmentComponent, InvestmentComponent>,
        IInvestmentComponentRepository
    {
        private IInvestmentElement _parent;

        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentComponentRepository" /> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        ///     The <see cref="IDb4ODatabaseContext" /> representing the Db4O database context used to carry on with the row data
        ///     operations.
        /// </param>
        public InvestmentComponentRepositoryEF(IEntityFrameworkDbContext<InvestmentComponent> databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets the properties of the investment elements to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.ParentId)
                }).ToArray();
            }
        }

        /// <summary>
        ///     Gets the reference to the <see cref="IInvestmentElement" /> containing the investment components handled in the
        ///     current repository.
        /// </summary>
        public IInvestmentElement InvestmentElement
        {
            get
            {
                if (_parent == null)
                    throw new ArgumentException(Resources.InitializeInvestmentElementBeforeUsingIt);

                return _parent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _parent = value;
            }
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IInvestmentComponent> Entities
        {
            get
            {
                var specification = new InvestmentElementsOfQueryable(InvestmentElement,DatabaseContext);

                return WhereSQL(specification);
            }
        }

        

        /// <summary>
        ///     Adds the given investment component to the current repository.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentComponent" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentComponent" /> does not have an <see cref="IBudget" />.</exception>
        /// <returns>The added <see cref="IInvestmentComponent"/> that was just added.</returns>
        public override IInvestmentComponent Add(IInvestmentComponent investmentComponent)
        {
            this.Relate(investmentComponent, InvestmentElement);
            IInvestmentComponent dbComponent = base.Add(investmentComponent);
           

            return dbComponent;
        }

        /// <summary>
        /// Deletes the given investment component from the current repository.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentComponent" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentComponent" /> has a null budget.</exception>
        public override void Delete(IInvestmentComponent investmentComponent)
        {
            if (investmentComponent == null)
                throw new ArgumentNullException("investmentComponent");
            if (investmentComponent.Budget == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentComponent");

           // var dbInvestment = DatabaseContext.Find<IInvestmentComponent>(investmentComponent.Id);

            this.Unrelate(investmentComponent, InvestmentElement);

         

            base.Delete(investmentComponent);

           // DatabaseContext.Delete((object)dbInvestment.Elements);
        }

        /// <summary>
        ///     Relates the given investment component with the given investment, making it its parent.
        /// </summary>
        /// <param name="investmentComponent">
        ///     The <see cref="IInvestmentComponent" /> to relate to its parent:
        ///     <paramref name="parent" />.
        /// </param>
        /// <param name="parent">The <see cref="IInvestment" /> to relate with its child: <paramref name="investmentComponent" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentComponent" /> or <paramref name="parent" /> is null.
        /// </exception>
        public void Relate(IInvestmentComponent investmentComponent, IInvestmentElement parent)
        {
            if (investmentComponent == null)
                throw new ArgumentNullException("investmentComponent");
            if (parent == null)
                throw new ArgumentNullException("parent");

           
            investmentComponent.Parent = parent;
            investmentComponent.ParentId = parent.Id;
        }

        /// <summary>
        ///     Breaks the relations between the given investment component and its parent investment.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent" /> to unrelate.</param>
        /// <param name="parent">The <see cref="IInvestmentElement" /> to unrelate.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentComponent" /> or <paramref name="parent" /> is null.
        /// </exception>
        public void Unrelate(IInvestmentComponent investmentComponent, IInvestmentElement parent)
        {
            if (investmentComponent == null)
                throw new ArgumentNullException("investmentComponent");
            if (parent == null)
                throw new ArgumentNullException("parent");

            if (parent.Elements.All(x => !Equals(investmentComponent.Id, x.Id)))
                return;

            parent.Elements.Remove(investmentComponent);
            investmentComponent.Parent = null;
        }

        /// <summary>
        ///     Saves the references of the given investment component.
        /// </summary>
        /// <param name="parent">The <see cref="IInvestmentElement" /> to save references of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parent"/> is null.</exception>
        public void SaveReference(IInvestmentElement parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            DatabaseContext.Save();
            DatabaseContext.Save();
        }
    }
}