using System;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    /// <summary>
    ///     Base class of the presenter view models to decorate and impersonate coded nomenclators.
    /// </summary>
    /// <typeparam name="TNomenclator">The type of the coded nomenclator the presenter will impersonate.</typeparam>
    /// <typeparam name="TServices">The manager application services handling the CRUD operations for the coded nomenclator.</typeparam>
    public abstract class CodedNomenclatorPresenterBase<TNomenclator, TServices> :
        NomenclatorPresenterBase<TNomenclator, TServices>,
        ICodedNomenclator
        where TNomenclator : class, ICodedNomenclator
        where TServices : IItemManagerApplicationServices<TNomenclator>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="CodedNomenclatorPresenterBase{TNomenclator,TServices}" /> deriver with a
        ///     coded nomenclator.
        /// </summary>
        /// <param name="nomenclator">The coded nomenclator to be decorated by the current presenter view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="nomenclator"/> is null.</exception>
        protected CodedNomenclatorPresenterBase(TNomenclator nomenclator)
            : base(nomenclator)
        {
        }


        /// <summary>
        ///     Gets or sets the code of the underlying nomenclator.
        /// </summary>
        public string Code
        {
            get { return Object.Code; }
            set
            {
                SetProperty(v => Object.Code = v, value);
                OnPropertyChanged(() => Code);
            }
        }
    }
}