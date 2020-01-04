using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class ChildCustomReportSettingsofSpecification<TSettings>: Specification<TSettings>
        where TSettings: class , IChildCustomReportSettings
    {

        public ChildCustomReportSettingsofSpecification(ICustomReportSettings parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            Predicate = settings => settings.Parent != null && (settings.Parent.Id.ToString() == parent.Id.ToString());
        }
    }


    public class ChildCustomReportSettingsOfQueryable<TSettings> : EntityFrameworkQueryable<TSettings>
       where TSettings : ChildCustomReportSettings
    {

        public ChildCustomReportSettingsOfQueryable(ICustomReportSettings parent, IEntityFrameworkDbContext<TSettings> context) : base(context)
        {
            
            if (parent == null)
                throw new ArgumentNullException("parent");
            Query = (from e in context.Entities orderby e.Id ascending where e.ParentId == parent.Id select e);
        }
    }
}