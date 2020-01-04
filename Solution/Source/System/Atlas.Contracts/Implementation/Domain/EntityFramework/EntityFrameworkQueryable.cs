using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework
{
    public  class EntityFrameworkQueryable<T>: IEntityFrameworkQuerable<T>
        where T: EntityBase
        //   where TContext: class , IEntityFrameworkDbContext<T>
    {
        public EntityFrameworkQueryable(IEntityFrameworkDbContext<T> context)
        {



            Query = (from e in context.Entities select e);
            Parameter = "";
            SQL = "";
           
        }
        public IQueryable<T> Query { get; set; }
        public string Parameter { get; set; }
        public string SQL { get; set; }
    }
}