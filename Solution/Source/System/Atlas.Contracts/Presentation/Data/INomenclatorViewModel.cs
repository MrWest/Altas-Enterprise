using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    public interface INomenclatorViewModel<TNomenclator> : ICrudViewModel<TNomenclator,INomenclatorPresenter<TNomenclator>>, INomenclatorViewModel
        where TNomenclator : class, ICodedNomenclator
    {
       // Expression<Predicate<TNomenclator>> AddedExpression { get; set; }
    }

    public interface INomenclatorViewModel:ICrudViewModel
    {
        String Text { get; set; }
        int MaxNumber { get; set; }
        INomenclatorViewModel NomenclatorProvider { get; }
        void FillMe(IList<INomenclatorPresenter> fillList);
    }
}