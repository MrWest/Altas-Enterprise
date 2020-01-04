using System;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Application
{
    public class NomenclatorManagerApplicationServices<TNomenclator> : ItemManagerApplicationServicesBase<TNomenclator, INomenclatorRepository<TNomenclator>, ICommonDomainService<TNomenclator>>, INomenclatorManagerApplicationServices<TNomenclator>
        where TNomenclator : class, ICodedNomenclator
    
    {
        public string Text { get; set; }

        public int MaxNumber { get; set; }

        protected override INomenclatorRepository<TNomenclator> Repository
        {
            get
            {
                var repo = base.Repository;
                repo.Text = Text;
                repo.MaxNumber = MaxNumber;
                repo.AddedExpression = AddedExpression;
                return repo;
            }
        }

        public Tuple<Func<TNomenclator, bool>, string> AddedExpression { get; set; }
    }
}