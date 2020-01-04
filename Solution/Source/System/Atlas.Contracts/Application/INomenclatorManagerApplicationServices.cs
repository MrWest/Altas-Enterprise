using System;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application
{
    public interface INomenclatorManagerApplicationServices<TNomenclator> : IItemManagerApplicationServices<TNomenclator>
        where TNomenclator : class , ICodedNomenclator
    {
        String Text { get; set; }
        int MaxNumber { get; set; }
        Tuple<Func<TNomenclator, bool>, string> AddedExpression { get; set; }
    }
}