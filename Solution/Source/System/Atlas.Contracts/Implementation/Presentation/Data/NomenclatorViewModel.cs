using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    public abstract class NomenclatorViewModel<TNomenclator,TInstanciable>: CrudViewModelBase<TNomenclator,INomenclatorPresenter<TNomenclator>,INomenclatorManagerApplicationServices<TNomenclator>>, INomenclatorViewModel<TNomenclator>
        where TNomenclator : class, ICodedNomenclator
         where TInstanciable : class, INomenclatorViewModel
    {
        private string _text;
        private int _maxNumber=10;

        public string Text
        {
            get
            {
                if (_text == null)
                    return "";
                return _text;
            }
            set
            {
                 _text = value;
                //Load();
             }
        }

        public int MaxNumber
        {
            get { return _maxNumber; }
            set
            {
                _maxNumber = value;
               // Load();
            }
        }

        protected override INomenclatorManagerApplicationServices<TNomenclator> CreateServices()
        {
            var service = base.CreateServices();
            service.Text = Text;
            service.MaxNumber = MaxNumber;
            service.AddedExpression = AddedExpression;
            return service;
           
        }


        public static INomenclatorViewModel NomenclatorProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TInstanciable>().NomenclatorProvider;
            }
        }

        INomenclatorViewModel INomenclatorViewModel.NomenclatorProvider
        {
            get { return this; }
        }

        private Tuple<Func<TNomenclator, bool>, string> AddedExpression
        {
            get { return GetAddedExpression(); }
        }

        protected virtual Tuple<Func<TNomenclator, bool>, string> GetAddedExpression()
        {
            return null;
        }

        public void FillMe(IList<INomenclatorPresenter> fillList)
        {
            

            fillList.Clear();

            ExecuteUsingServices(services =>
            {
                var itemList = GetItems(services);
                foreach (TNomenclator item in itemList)
                {
                    INomenclatorPresenter presenter = CreatePresenterFor(item);


                    //if(_items.All(x=>x.Object.Id.ToString()!=item.Id.ToString()))
                    fillList.Add(presenter);
                }
            });

           
        }
    }
}