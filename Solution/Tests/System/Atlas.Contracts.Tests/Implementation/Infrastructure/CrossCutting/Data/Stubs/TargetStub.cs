using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Data.Stubs
{
    public interface ITargetStub
    {
        int NotUsingUnitOfWork { get; }

        int UsingUnitOfWork { get; }


        void CommitUnitOfWork();

        void CommitUnitOfWork1();

        void DoNotCommitUnitOfWork();

        void DoNotUseUnitOfWork();

        void Throw();

        string ImplicitUseUnitOfWork();
    }


    public class TargetStub : ITargetStub
    {
        public int NotUsingUnitOfWork
        {
            [SkipUnitOfWork]
            get { return 0; }
        }

        public int UsingUnitOfWork { get { return 0; } }


        [Commit]
        public void CommitUnitOfWork()
        {
        }

        [Commit(true)]
        public void CommitUnitOfWork1()
        {
        }

        [Commit(false)]
        public void DoNotCommitUnitOfWork()
        {
        }

        [SkipUnitOfWork]
        public void DoNotUseUnitOfWork()
        {
            ServiceLocator.Current.GetInstance<IUnitOfWork>().Commit();
        }

        [Commit]
        public void Throw()
        {
            throw new DllNotFoundException();
        }

        public string ImplicitUseUnitOfWork()
        {
            return ServiceLocator.Current.GetInstance<IUnitOfWork>().ToString();
        }
    }
}
