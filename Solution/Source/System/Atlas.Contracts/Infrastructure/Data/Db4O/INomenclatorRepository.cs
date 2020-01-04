using System;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O
{
    public interface INomenclatorRepository<T> : IRepository<T>
        where T :class, ICodedNomenclator
    {
        String Text { get; set; }
        int MaxNumber { get; set; }
        Tuple<Func<T, bool>, string> AddedExpression { get; set; }
    }
}