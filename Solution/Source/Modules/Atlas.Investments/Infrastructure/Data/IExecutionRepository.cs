using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    public interface IExecutionRepository:IRelatedRepository<IExecution, IExecutedActivity>
    {
        IExecutedActivity ExecutedActivity { get; set; }
    }
}
