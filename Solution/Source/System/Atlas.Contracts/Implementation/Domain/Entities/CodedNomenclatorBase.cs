using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    /// <summary>
    ///     Base class of the nomenclators having a code.
    /// </summary>
    public abstract class CodedNomenclatorBase : NomenclatorBase, ICodedNomenclator
    {
        /// <summary>
        ///     Gets or sets the code of the current nomenclator.
        /// </summary>
        public string Code { get; set; }
    }
}