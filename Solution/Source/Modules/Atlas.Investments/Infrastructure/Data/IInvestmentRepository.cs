using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    ///     Contract defining the interface of the repository with the responsibility of managing the data operations of the
    ///     investments.
    /// </summary>
    public interface IInvestmentRepository : IInvestmentElementRepository2<IInvestment>
    {
    }
}