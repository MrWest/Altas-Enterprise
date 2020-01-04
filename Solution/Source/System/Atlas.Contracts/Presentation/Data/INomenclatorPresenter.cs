using System;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    public interface INomenclatorPresenter<TNomenclator>:IPresenter<TNomenclator>, INomenclatorPresenter
          where TNomenclator : class, ICodedNomenclator
    {
        //String Name { get;  }
        //object Code { get; }
    }
    public interface INomenclatorPresenter 
    {
        String Name { get; }
        String Description { get; }
        String Code { get; }
    }
}