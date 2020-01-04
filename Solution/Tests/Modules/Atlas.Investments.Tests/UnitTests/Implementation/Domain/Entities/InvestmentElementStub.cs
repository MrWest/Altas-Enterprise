using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public class InvestmentElementStub : IInvestmentElement
    {
        public InvestmentElementStub()
        {
            Elements = new List<IInvestmentComponent>();
        }


        public object Id { get; set; }
        public string FullName { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IBudget Budget { get; set; }
        public IList<IInvestmentComponent> Elements { get; private set; }
        public string Code { get; set; }
        public string RelatedPrograms { get; set; }
        public string Location { get; set; }
        public string Constructor { get; set; }
        public string Objective { get; set; }
        public string Scope { get; set; }
    }
}