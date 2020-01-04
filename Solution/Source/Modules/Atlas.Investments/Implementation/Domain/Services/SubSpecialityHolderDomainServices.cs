using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public abstract class SubSpecialityHolderDomainServices<THolder>: DomainServicesBase<THolder>, ISubSpecialityHolderDomainServices<THolder>
        where THolder : class, ISubSpecialityHolder

    {
        public IBudgetComponent BudgetComponent{ get; set; }

        public override THolder Create()
        {
            var subs = base.Create();
            subs.SubSpeciality = Resources.NewSubSpeciality_Name;

            return subs;
        }
    }
}