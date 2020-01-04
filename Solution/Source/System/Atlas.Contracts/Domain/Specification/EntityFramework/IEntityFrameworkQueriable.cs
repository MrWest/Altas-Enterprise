using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using Db4objects.Db4o.Query;

namespace CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework
{
    public interface IEntityFrameworkQuerable<T>
        where T: class , IEntity
    {
        IQueryable<T> Query { get; set; }
        string Parameter { get; set; }
        string SQL { get; set; }
    }
}