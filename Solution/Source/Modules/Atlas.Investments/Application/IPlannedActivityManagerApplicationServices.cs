using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    /// Describes the main interface for an activity manager service
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TComponent"></typeparam>
    public interface IPlannedActivityManagerApplicationServices : IActivityManagerApplicationServices<IPlannedActivity>
        //where TItem : class, IBudgetComponentItem
        //where TComponent : class, IBudgetComponent
    {
        IPlannedActivity AdquireUnderProperties(IPlannedActivity onAdquiring, IUnderGroupActivity toAdquire);

        DateTime StartDate(IPlannedActivity plannedActivity);
        DateTime FinishDate(IPlannedActivity plannedActivity);
    }


}
