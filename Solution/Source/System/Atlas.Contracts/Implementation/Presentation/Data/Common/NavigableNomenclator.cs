using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Mvvm;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.Windows;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public abstract class NavigableNomenclator<T, TServices> : NomenclatorPresenterBase<T, TServices>, INavigable, IView
        where T : class, INomenclator
        where TServices : IItemManagerApplicationServices<T>

    {
       
        public virtual INavigable Parent { get; private set; }
        public virtual ICrudViewModel Items { get; private set; }

        public virtual ICommand DeleteMySelfCommand
        {
            get
            {
                if (Parent != null && Items != null)
                    return Parent.Items.DeleteCommand;
                return null;

            }
        }

        protected bool _isAwaiting = false;
        private bool _isExpanded;

        public virtual  bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                _isExpanded = value;


             
                    InternalSignalText();




            }
        }

     
        private async void InternalSignalText()
        {

           
                await AsyncSignalText().ContinueWith(Continues);
         

          
        }
        private async void Continues(Task task)
        {
            OnPropertyChanged(() => IsExpanded);
            OnPropertyChanged(() => HasItems);
            OnPropertyChanged(() => IconData);

            var navigable = GetSelected();
            navigable.ResetFakeIsExpanded();
        }
        protected virtual async  Task AsyncSignalText()
        {

            if (_isExpanded)
                if (StatusBarServices.StatusText == Resources.Ready)
                {
                    StatusBarServices.ForceSignalLoading();

                }


           
        }

        

        public INavigable GetSelected()
        {
           // var navigable = this;
            if (!IsSelected&&Parent!=null)
                return Parent.GetSelected();

            return this;

        }

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public virtual string IconData
        {
            get
            {
                if (HasItems)
                {
                    if (IsExpanded)
                        return "F1 M 0,12 0,8 20,8 20,12 Z ";

                    return "F1 M 0,12 0,8 8,8 8,0 12,0 12,8 20,8 20,12 12,12 12,20 8,20 8,12 Z ";
                }

                return "";
            }

        }
        ///// <summary>
        /////     Gets the data representing the geometry specification of the icon corresponding to the current
        /////     <see cref="InvestmentElementPresenter" /> according to its depth.
        ///// </summary>
        //[ExcludeFromCodeCoverage]
        //public virtual string IconData
        //{
        //    get
        //    {
        //        switch (Depth)
        //        {
        //            case 0:
        //                return
        //                    "F1 M 25,27L 46,19L 46,22.25L 28.5,29L 31.75,31.25L 51,23.75L 51,48.5L 31.75,57L 25,52L 25,27 Z ";
        //            case 1:
        //                return "F1 M 24,19L 40,19L 40,31L 52,31L 52,57L 24,57L 24,19 Z M 41,30L 41,19L 52,30L 41,30 Z ";
        //            case 2:
        //                return "F1 M 15.0021,35.0049L 15.0021,33.0046L 15.0021,24.2034L 14.002,25.0035L 12.0017,22.0031L 24.0033,13.0018L 29.004,16.7523L 29.004,14.002L 31.0043,13.502L 31.0043,18.2525L 36.005,22.0031L 34.0047,25.0035L 33.0046,24.2034L 33.0046,33.0046L 33.0046,35.0049L 15.0021,35.0049 Z M 24.0033,17.0024L 17.0024,22.6032L 17.0024,33.0046L 21.0029,33.0046L 21.0029,27.0038L 27.0038,27.0038L 27.0038,33.0046L 31.0043,33.0046L 31.0043,22.6032L 24.0033,17.0024 Z";
        //            case 3:
        //                return "F1 M 24,19L 40,19L 40,31L 52,31L 52,57L 24,57L 24,19 Z M 41,30L 41,19L 52,30L 41,30 Z ";
        //            case 4:
        //                return "F1 M 17,19L 20,19L 20,54L 59,54L 59,57L 17,57L 17,19 Z M 22,52L 22,47L 31.6667,36.4167L 45.9167,42.75L 53.1866,30.0277L 47.9986,31.5153L 45.6038,28.9077L 57.7798,25.4163L 61.2712,37.5923L 57.7908,36.9432L 56.1901,31.3612L 47.5,47.5L 32.4167,40.6667L 22,52 Z ";
        //            default:
        //                return "F1 M 38,15.8334L 58.5833,23.75L 58.5833,30.0833L 38,38L 17.4167,30.0833L 17.4166,23.75L 38,15.8334 Z M 58.5833,44.3333L 58.5833,52.25L 38,60.1667L 17.4167,52.25L 17.4167,44.3333L 21.5333,45.9167L 38,52.25L 54.4667,45.9167L 58.5833,44.3333 Z M 58.5833,33.25L 58.5833,41.1667L 38,49.0833L 17.4167,41.1667L 17.4167,33.25L 21.5333,34.8333L 38,41.1667L 54.4666,34.8333L 58.5833,33.25 Z ";



        //        }
        //    }
        //}
        public virtual int Depth => Parent?.Depth+1 ?? 0;

        public virtual bool HasItems => Items?.Items.Count > 0;

        public virtual string Kind { get; private set; }

        public virtual string NewText
        {
            get { return "Nuevo"; }
        }

        public virtual string DeleteText  {
            get { return "Eliminar"; }
        } 

        public virtual void Filter(ref IEnumerable<INavigable> list, string filter)
        {
            var local = new List<INavigable>();
            foreach (INavigable navigable in list)
            {
                local.Add(navigable);
            }
            foreach (PropertyInfo propertyInfo in this.GetType().GetProperties())
            {
                if (propertyInfo.GetValue(this) != null && propertyInfo.GetValue(this) != Parent && propertyInfo.GetValue(this).ToString().ToLower().Contains(filter.ToLower()))
                {
                    if (!local.Exists(x => x == this))
                    {
                        local.Add(this);
                        list = local.AsEnumerable();
                    }
                   
                    break;
                }

            }

            if(Items!=null)
            foreach (INavigable navigable in Items.Items)
            {
                navigable.Filter(ref list, filter);
            }
        }
        private ICurrencyPresenter _currency;
        private bool _isChecked;
        private NavigableState _state = NavigableState.UnSelected;
        private bool finish = true;

        public virtual ICurrencyPresenter Currency
        {
            get
            {
                if (Object.GetType().Implements<ICurrenciable>() && ((ICurrenciable)Object).Currency != null &&
                    (_currency == null || _currency.Object.Id.ToString() != ((ICurrenciable)Object).Currency.ToString()))
                {
                    var viewModel = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();
                    viewModel.Load();
                    _currency = viewModel.Find(((ICurrenciable)Object).Currency);
                }


                return _currency;

            }
            set
            {
                if (value != null)
                    SetProperty(v => ((ICurrenciable)Object).Currency = v.Object.Id, value);
                OnPropertyChanged(() => Currency);
                OnPropertyChanged(() => Cost);
                DoNotify();
                if (Parent != null) Parent.DoNotify();
            }
        }

        public virtual decimal Cost{
                    get
                    {
                        if (!IsCostCalculated)
                        {
                            decimal cost = 0;
                            if (Currency != null)
                            foreach (INavigable val in Items)
                            {
                                if (val.Currency != null)
                                    if (val.Currency.Id.ToString() == Currency.Id.ToString())
                                    {
                                        cost += val.Cost;
                                    }
                                    else
                                    {
                                        var curren = val.Currency.ConvertionFactorFor(Currency.Object);

                                        cost += val.Cost * curren;

                                    }

                            }
                           _calculatedCost = Math.Round(cost, 2);
                            IsCostCalculated = true;
                        }
                      
                //if (Currency!=null)
                    return _calculatedCost;
                }
            }

        private decimal _calculatedCost;

        protected bool __isCostCalculated;
        public virtual bool IsCostCalculated
        {
            get { return __isCostCalculated; }
            set
            {
                __isCostCalculated = value;
                if (!__isCostCalculated && Parent != null)
                {
                    OnPropertyChanged(() => Cost);
                    Parent.IsCostCalculated = false;
                }
               
            }
        }
        public virtual Thickness DeepThickness
        {
            get
            {
                if (Parent != null)
                    return new Thickness(Parent.DeepThickness.Left + 13, 0, 0, 0);

                return new Thickness(0, 0, 0, 0);
            }

        }
        public virtual void DoNotify()
        {
            OnPropertyChanged(()=>HasItems);
            OnPropertyChanged(() => IsExpanded);
            OnPropertyChanged(() => IconData);
            OnPropertyChanged(() => Cost);
            if (Parent != null)
                Parent.DoNotify();
        }

        public void ExpandAll()
        {
            IsExpanded = true;
            foreach (INavigable navigable in Items)
            {
                navigable.ExpandAll();
            }
        }

        public void CollapseAll()
        {
            IsExpanded = false;
            foreach (INavigable navigable in Items)
            {
                navigable.CollapseAll();
            }
        }

        public virtual void SetToPrint()
        {
            foreach (INavigable navigable in Items)
            {
                navigable.SetToPrint();
            }
        }
        public object DataContext { get; set; }
        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }




        public NavigableState State
        {
            get { return _state; }
            set
            {
                //if (_state != value)
                //{
                if (finish)
                {
                    _state = value;

                    if (!Equals(Items, null) && (_state == NavigableState.Selected || _state == NavigableState.UnSelected))
                    {
                        finish = false;
                        foreach (INavigable navigable in Items.Items)
                        {
                            navigable.State = _state;
                        }
                        finish = true;
                    }


                    if (!Equals(Items, null) && _state == NavigableState.Marked && Items.Cast<INavigable>().All(x => x.State == NavigableState.UnSelected))
                        _state = NavigableState.UnSelected;

                    if (!Equals(Items, null) && _state == NavigableState.Marked && Items.Cast<INavigable>().All(x => x.State == NavigableState.Selected))
                        _state = NavigableState.Selected;

                    if (!Equals(Parent, null) )
                        Parent.State = NavigableState.Marked;


                }


                //}



                OnPropertyChanged(() => State);
            }
        }

        public virtual void Refresh()
        {
            
        }

        private bool _fakeIsExpanded = true;
        private Task _callbackTask;

        public bool FakeIsExpanded
        {
            get
            {
                return _fakeIsExpanded;
            }
            set
            {
                _fakeIsExpanded = value;
                OnPropertyChanged(() => FakeIsExpanded);
                if (Items!=null)
                    foreach (INavigable navigables in Items.Items)
                        navigables.FakeIsExpanded = _fakeIsExpanded;

               ResetFakeIsExpanded();
            }
        }
        public bool AnyIsExpanded
        {
            get
            {

                
                if (Parent != null && Parent.IsExpanded )
                    return Parent.AnyIsExpanded;
                if (Parent != null && ( Parent.IsSelected))
                    return true;


                return false;
            }

        }


        public bool IsSelected { get; set; }
        public void ResetFakeIsExpanded()
        {
            //if (_fakeIsExpanded)
            //{
            //    _fakeIsExpanded = false;
            //    OnPropertyChanged(() => FakeIsExpanded);
            //    _fakeIsExpanded = true;
            //    OnPropertyChanged(() => FakeIsExpanded);
            //    if (Items != null)
            //        foreach (INavigable itemsItem in Items.Items)
            //            itemsItem.ResetFakeIsExpanded();
            //}
            
           
        }

      
    }
   



}
