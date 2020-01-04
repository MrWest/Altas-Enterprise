using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public  class SubSpecialityPresenter : CodedNomenclatorPresenterBase<ISubSpeciality,ISubSpecialityManagerApplicationServices>, ISubSpecialityPresenter
    {
        public SubSpecialityPresenter(ISubSpeciality nomenclator)
            : base(nomenclator)
        {
        }

        public ISpecialityPresenter Speciality { get; set; }
        protected override ISubSpecialityManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.Speciality = Speciality.Object;
            return service;
        }
    }
}
