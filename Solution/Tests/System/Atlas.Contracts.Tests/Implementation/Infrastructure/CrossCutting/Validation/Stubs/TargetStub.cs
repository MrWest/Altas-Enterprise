using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Validation.Stubs
{
    public interface ITargetStub : IValidationServices<IEntity>
    {
        void Add(IEntity item);

        void Do();
    }


    public class TargetStub : ITargetStub
    {
        public IDomainServices<IEntity> DomainServices
        {
            get { return ServiceLocator.Current.GetInstance<IDomainServices<IEntity>>(); }
        }
        

        [Validate]
        public void Add(IEntity item)
        {
            item.Id = 100.ToString();
        }

        [Validate]
        public void Do()
        {
            // This method must be ignored
        }

        public IEnumerable<string> Validate(IEntity item)
        {
            return DomainServices.Validate(item);
        }

        IEnumerable<string> IValidationServices.Validate(object item)
        {
            return Validate((IEntity)item);
        }
    }
}
