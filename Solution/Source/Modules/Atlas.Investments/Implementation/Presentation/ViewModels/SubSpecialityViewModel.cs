using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class SubSpecialityViewModel : CrudViewModelBase<ISubSpeciality, ISubSpecialityPresenter, ISubSpecialityManagerApplicationServices>, ISubSpecialityViewModel
    {
        public ISpecialityPresenter Speciality { get; set; }
        protected override ISubSpecialityPresenter CreatePresenterFor(ISubSpeciality item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.Speciality = Speciality;
            return presenter;
        }
        protected override ISubSpecialityManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.Speciality = Speciality.Object;
            return service;
        }
    }
}
