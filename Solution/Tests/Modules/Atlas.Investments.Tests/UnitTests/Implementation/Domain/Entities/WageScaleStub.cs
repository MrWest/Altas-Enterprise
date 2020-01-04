using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class WageScaleStub : IWageScale
    {
        public object Id { get; set; }

        public string FullName { get; private set; }

        public string Name { get; set; }

        public decimal Retribution { get; set; }
    }
}