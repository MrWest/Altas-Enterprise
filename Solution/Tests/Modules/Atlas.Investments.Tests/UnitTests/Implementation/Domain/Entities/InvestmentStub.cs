using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public class InvestmentStub : InvestmentElementBaseStub, IInvestment
    {
        public string Capacity { get; set; }
        
        public string InducedDoings { get; set; }

        public string AuthorOrEmitter { get; set; }

        public string Entity { get; set; }

        public string RelatedPrograms { get; set; }
    }
}