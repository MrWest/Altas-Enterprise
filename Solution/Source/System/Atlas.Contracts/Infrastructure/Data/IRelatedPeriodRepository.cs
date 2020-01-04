using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;


namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    /// Repository for a period related with some other entity
    /// / <typeparam name="TPeriod">The budget component items which data operations will be handled here.</typeparam>
    /// <typeparam name="TEntity">The budget component to which they belong.</typeparam>
    /// </summary>
    interface IRelatedPeriodRepository<TPeriod, TEntity> : IRelatedRepository<TPeriod, TEntity>
        where TPeriod:class, IPeriod
        where TEntity:class, IEntity
    {
    }

   
}
