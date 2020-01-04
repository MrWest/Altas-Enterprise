using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
    /// <summary>
    /// Repository interface for a period entity, in this architecture a period will be
    /// always related to some other entity.
    /// </summary>
    public interface IPeriodRepository : IRepository<IPeriod>
    {
        IEntity Holder { get; set; }
    }
    //public interface IPeriodRepository<TPeriod, TEntity> : IRelatedRepository<TPeriod, TEntity>
    //    where TPeriod : class,IPeriod
    //    where TEntity : class,IEntity
    //{

    //}
}
