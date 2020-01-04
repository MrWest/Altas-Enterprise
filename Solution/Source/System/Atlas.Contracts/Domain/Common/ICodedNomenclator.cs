namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    ///     Base contract of the domain entities being nomenclators with a code.
    /// </summary>
    public interface ICodedNomenclator : INomenclator
    {
        /// <summary>
        ///     Gets or sets the code of the current nomenclator.
        /// </summary>
        string Code { get; set; }
    }
}