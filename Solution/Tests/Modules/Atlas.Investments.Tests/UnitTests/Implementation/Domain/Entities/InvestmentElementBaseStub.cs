using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public abstract class InvestmentElementBaseStub : IInvestmentElement
    {
        protected InvestmentElementBaseStub()
        {
            Elements = new List<IInvestmentComponent>();
        }


        public IBudget Budget { get; set; }

        public IList<IInvestmentComponent> Elements { get; private set; }
        
        public string Code { get; set; }

        public string Location { get; set; }
        
        public string Constructor { get; set; }
        
        public string Objective { get; set; }
        
        public string Scope { get; set; }

        public string Description { get; set; }

        public string FullName { get; set; }

        public object Id { get; set; }

        public string Name { get; set; }

        public IInvestmentElement Parent { get; set; }
    }
}
