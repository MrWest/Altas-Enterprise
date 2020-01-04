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
    public class SubSpecialityManagerApplicationServices : ItemManagerApplicationServicesBase<ISubSpeciality, ISubSpecialityRepository, ISubSpecialityDomainService>, ISubSpecialityManagerApplicationServices
    {
        public ISpeciality Speciality { get; set; }

        protected override ISubSpecialityRepository Repository
        {
            get
            { 
                var repo = base.Repository;
                repo.Speciality = Speciality;
                return repo;
            }
        }

        protected override ISubSpecialityDomainService DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.Speciality = Speciality;
                return domain;
            }
        }

        public ISubSpeciality Export(IDatabaseContext databaseContext, ISubSpeciality subSpeciality)
        {
            if (Equals(subSpeciality, null))
                return null;

            var expConvert = Repository.GetClone(subSpeciality);
            //if in the context is nothing found
            if (!databaseContext.GetAll<ISubSpeciality>().Any(x => x.Code == expConvert.Code))
            {
                var specialityService = ServiceLocator.Current.GetInstance<ISpecialityManagerApplicationServices>();
                expConvert.Speciality = specialityService.Export(databaseContext, subSpeciality.Speciality);
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
