using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentType" /> for the domain entity: "Investment Type".
    /// </summary>
    public class InvestmentType : CodedNomenclatorBase, IInvestmentType
    {
    }
}