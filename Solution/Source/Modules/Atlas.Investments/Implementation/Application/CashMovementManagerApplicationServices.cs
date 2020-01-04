using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// some same crap
    /// </summary>
    public class CashMovementManagerApplicationServices : ItemManagerApplicationServicesBase<ICashMovement, ICashMovementRepository, ICashMovementDomainService>, ICashMovementManagerApplicationServices
    {
        private ICashMovementCategory _category;
        public ICashMovementCategory Category { get { return _category; } set { _category = value; } }
        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override ICashMovementRepository Repository
        {
            get
            {
                ICashMovementRepository repository = base.Repository;
                repository.Category = Category;

                return repository;
            }
        }

      
        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        //protected override ICashMovementDomainServices DomainServices
        //{
        //    get
        //    {
        //        ICashMovementDomainService domainServices = base.DomainServices;
        //        domainServices.Category = Category;

        //        return domainServices;
        //    }
        //}
    }
}
