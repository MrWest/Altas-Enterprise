using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class SubExpenseConceptManagerApplicationServices : ItemManagerApplicationServicesBase<ISubExpenseConcept, ISubExpenseConceptRepository, ISubExpenseConceptDomainService>, ISubExpenseConceptManagerApplicationServices
    {
        public IExpenseConcept ExpenseConcept { get; set; }

        protected override ISubExpenseConceptRepository Repository
        {
            get
            { 
                var repo = base.Repository;
                repo.ExpenseConcept = ExpenseConcept;
                return repo;
            }
        }

        protected override ISubExpenseConceptDomainService DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.ExpenseConcept = ExpenseConcept;
                return domain;
            }
        }

        public ISubExpenseConcept Export(IDatabaseContext databaseContext, ISubExpenseConcept exportable)
        {
            if (Equals(exportable, null))
                return null;

            var expConvert = Repository.GetClone(exportable);
            //if in the context is nothing found
            if (!databaseContext.GetAll<ISubSpeciality>().Any(x => !Equals(expConvert, null) && x.Code == expConvert.Code))
            {
                var specialityService = ServiceLocator.Current.GetInstance<IExpenseConceptManagerApplicationServices>();
                expConvert.ExpenseConcept = specialityService.Export(databaseContext, exportable.ExpenseConcept);
                databaseContext.Add(expConvert);
            }
            else
            {
                expConvert.Id = databaseContext.GetAll<ISubSpeciality>().First(x => x.Code == expConvert.Code).Id;
            }

            return expConvert;
        }
    }
}
