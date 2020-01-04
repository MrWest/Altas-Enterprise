using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital
{
    public class WorkCapitalCashFlowItemManagerApplicationServices : ItemManagerApplicationServicesBase<IWorkCapitalCashFlow, IWorkCapitalCashFlowRepository, IWorkCapitalCashFlowDomainServices>, IWorkCapitalCashFlowItemManagerApplicationServices
    {
        protected override IWorkCapitalCashFlowRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.WorkCapitalComponent = WorkCapitalComponent;
                return repo;
            }
        }

        protected override IWorkCapitalCashFlowDomainServices DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.WorkCapitalComponent = WorkCapitalComponent;
                return domain;
            }
        }

        public IList<ICashMovement> GeRemainingCashHistory(IPeriod period, ICashEntry cashEntries, ICashOutgoing cashOutgoings)
        {
            IList<ICashMovement> rtnList = new Collection<ICashMovement>();

            var movementEntries = ServiceLocator.Current.GetInstance<ICashMovementRepository>();
            var movementOutgoings = ServiceLocator.Current.GetInstance<ICashMovementRepository>();

            foreach (IPeriod p in period.Periods)
             {
                 var movement = ServiceLocator.Current.GetInstance<ICashMovement>();
                 movement.Date = p.Starts;

              
                 movementEntries.Category = cashEntries;


                movementOutgoings.Category = cashOutgoings;

                movement.Amount = GetRemainingCash(GetAllMovementOnDeep(cashEntries), GetAllMovementOnDeep(cashOutgoings), p,period.Periods);


                rtnList.Add(movement);
               
            }
            return rtnList;
        }

     
        public decimal GetWorkCapital(IPeriod period, ICashEntry cashEntries, ICashOutgoing cashOutgoings)
        {
           
           
            return IsAnyRemainingLow(period.Periods,  period, cashEntries, cashOutgoings)*-1;
        }

        private IList<ICashMovement> GetAllMovementOnDeep(ICashEntry cashMovementCategory)
        {
            IList<ICashMovement> movementsList = new List<ICashMovement>();

           

            var subCategoriesRepo = ServiceLocator.Current.GetInstance<ICashMovementCategoryRepository<ICashEntry>>();
            subCategoriesRepo.SuperiorCategory = cashMovementCategory;
            if (subCategoriesRepo.Entities.Count() == 0)
            {
                var movementRepo = ServiceLocator.Current.GetInstance<ICashMovementRepository>();
                movementRepo.Category = cashMovementCategory;
                return movementRepo.Entities.ToList();
            }
            else
            {
                foreach (ICashEntry cashEntry in subCategoriesRepo.Entities)
                {
                    if(cashEntry.Name=="Liquidez")
                        subCategoriesRepo.Delete(cashEntry);
                    else
                  movementsList = AppendTwoLists(movementsList, GetAllMovementOnDeep(cashEntry));
                }
            }
            return movementsList;
        }

        private IList<ICashMovement> GetAllMovementOnDeep(ICashOutgoing cashMovementCategory)
        {
            IList<ICashMovement> movementsList = new List<ICashMovement>();



            var subCategoriesRepo = ServiceLocator.Current.GetInstance<ICashMovementCategoryRepository<ICashOutgoing>>();
            subCategoriesRepo.SuperiorCategory = cashMovementCategory;
            if (subCategoriesRepo.Entities.Count() == 0)
            {
                var movementRepo = ServiceLocator.Current.GetInstance<ICashMovementRepository>();
                movementRepo.Category = cashMovementCategory;
                return movementRepo.Entities.ToList();
            }
            else
            {
                foreach (ICashOutgoing cashOutgoing in subCategoriesRepo.Entities)
                {
                    movementsList = AppendTwoLists(movementsList, GetAllMovementOnDeep(cashOutgoing));
                }
            }
            return movementsList;
        }
        private IList<ICashMovement> AppendTwoLists(IList<ICashMovement> list, IList<ICashMovement> list2)
        {
            if (list != null && list2 != null)
            {
                foreach (ICashMovement movement in list2)
                {
                    list.Add(movement);
                }

                return list;
            }

            return new List<ICashMovement>();
        }
      

        private decimal GetRemainingCash(IList<ICashMovement> entriesValues, IList<ICashMovement> outgoingValues, IPeriod period, IList<IPeriod> periods)
        {




            int index = periods.IndexOf(periods.SingleOrDefault(x=>x.Name==period.Name && x.Starts.Year == period.Starts.Year));
            decimal auxrslt = 0;

            foreach (ICashMovement overTimeValue in entriesValues)
            {
                if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
                    (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
                    auxrslt += overTimeValue.Amount;
            }


            foreach (ICashMovement overTimeValue in outgoingValues)
            {
                if ((overTimeValue.Date.CompareTo(period.Starts)) >= 0 &&
                    (overTimeValue.Date.CompareTo(period.Ends)) <= 0)
                    auxrslt -= overTimeValue.Amount;
            }

            if (index > 0)
            {
                auxrslt += GetRemainingCash(entriesValues, outgoingValues, periods[index - 1], periods);
            }
            return auxrslt;
        }

        private decimal IsAnyRemainingLow(IList<IPeriod> periods, IPeriod p, ICashEntry cashEntries,
            ICashOutgoing cashOutgoings)
        {

            //ArrayList rsltList = new ArrayList();
            //rsltList.Add(0);
            //rsltList.Add(p);
       
            decimal lowRemain = 0;
            foreach (IPeriod period in periods)
            {




                decimal remain = GetRemainingCash(GetAllMovementOnDeep(cashEntries), GetAllMovementOnDeep(cashOutgoings),
                    period, periods); //+ (lowRemain*-1);

                if (remain + lowRemain * -1 < 0)
                {
                    lowRemain += (remain + lowRemain * -1);
                    //rsltList[0] = remain;
                    //rsltList[1] = period;
                }

                //if (period.Starts.CompareTo(p.Starts) >=0)
                //    break;

            }

            return lowRemain;
        }

        public IWorkCapitalComponent WorkCapitalComponent { get; set; }
    }
}
