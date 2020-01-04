using System;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    public  class NomenclatorPresenter<TNomenclator> : EntityPresenterBase<TNomenclator,INomenclatorManagerApplicationServices<TNomenclator>>, INomenclatorPresenter<TNomenclator>
          where TNomenclator : class, ICodedNomenclator
    {
        public string Name
        {
            get { return Object.Name; }
        }
        public string Description
        {
            get { return Object.Description; }
        }
        public String Code
        {
            get { return Object.Code; }
        }
    }
}