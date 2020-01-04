using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;


namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
  
    public class Period: NomenclatorBase,IPeriod
    {


        //public Period()
        //{
        //    PeriodKind = DateTimeScale.Month;
        //    Starts = DateTime.Today;
        //    Ends = DateTime.Today;
           
        //}
        //public Period(DateTime start, DateTime end)
        //{
        //    PeriodKind = DateTimeScale.Month;
        //    Starts = start;
        //    Ends = end;

        //}
        private IEntity _holder;

        [ForeignKey("HolderId")]
        public IEntity Holder {
            get { return _holder; } 
            set { _holder = value; }
        }
        public DateTimeScale PeriodKind { get; set; }

        private DateTimeScale _periodKind = DateTimeScale.Monthly;
        private DateTime _starts = DateTime.Parse(DateTime.Today.ToUniversalTime().ToString());
        private DateTime _ends = DateTime.Parse(DateTime.Today.ToUniversalTime().ToString());

        [Column(TypeName = "datetime2")]
        public DateTime Starts
        {
            get
            {
                ////get the finish date by asking to the period holder
                //if (Holder != null && Holder.GetType().Implements<IPeriodCalculator>())
                //    return ((IPeriodCalculator)Holder).StartDate();
                return _starts;
            }
            set
            {
                _starts = value;
                if (_starts.CompareTo(_ends) > 0)
                    Ends = Starts;
              
            }
        }

        [Column(TypeName = "datetime2")]
        public DateTime Ends
        {
            get
            {
                //get the finish date by asking to the period holder
                ////if (Holder != null && Holder.GetType().Implements<IPeriodCalculator>())
                ////    return ((IPeriodCalculator) Holder).FinishDate();
                return _ends;
            }
            set
            {
                _ends = value;
                if (_ends.CompareTo(_starts) < 0)
                    Starts = Ends;
            }
        }

        public int Days { get { return GetDays(); }}

        private int GetDays()
        {
            int days = 0;
            DateTime aux = Starts;
            while (aux.CompareTo(Ends) <= 0)
            {
                aux = aux.AddDays(1);
                days++;
            }

            return days;
        }

        public bool IsContained(IPeriod period)
        {
            return Starts.CompareTo(period.Starts) <= 0 && Ends.CompareTo(period.Starts) >= 0;
        }

        public DateTime OriStart()
        {
            return _starts;
        }

        public DateTime OriEnd()
        {
            return _ends;
        }

        public String ShortEnds { get { return Ends.ToShortDateString(); } }
        public String ShortStarts { get { return Starts.ToShortDateString(); } }


        /// <summary>
        /// To obtains a collection of period, defined by the kind of time scale from the current (<see cref="InvestmentElementPeriodPresenter"/>
        /// </summary>
        public IList<IPeriod> Periods { get { return GetPeriods(); } }

        public string HolderId { get; set; }

        private IList<IPeriod> GetPeriods()
        {
            DateTime auxStart = Starts;
            DateTime markStart = Starts;
            DateTime auxEnds = Ends;

            IList<IPeriod> returnPeriods = new List<IPeriod>();

            if (PeriodKind != DateTimeScale.Weekly)
                while (auxStart.CompareTo(auxEnds) <= 0)
                {
                    if (PeriodKind == DateTimeScale.Monthly)
                    {
                        if (markStart.Month != auxStart.Month)
                        {

                            returnPeriods.Add(new Period() { Name = ConvertMonths(markStart.Month), Starts = markStart, Ends = auxStart.AddDays(-1) });
                            markStart = auxStart;
                        }
                        if (markStart.Month == auxEnds.Month && markStart.Year == auxEnds.Year)
                        {
                            returnPeriods.Add(new Period() { Name = ConvertMonths(markStart.Month), Starts = markStart, Ends = auxEnds });
                            break;
                        }
                    }

                    if (PeriodKind == DateTimeScale.Yearly)
                    {
                        if (markStart.Year != auxStart.Year)
                        {
                            returnPeriods.Add(new Period() { Name = markStart.Year.ToString(), Starts = markStart, Ends = auxStart.AddDays(-1) });
                            markStart = auxStart;
                        }
                        if (markStart.Year == auxEnds.Year)
                        {
                            returnPeriods.Add(new Period() { Name = markStart.Year.ToString(), Starts = markStart, Ends = auxEnds });
                            break;
                        }
                    }
                    if (PeriodKind == DateTimeScale.Daily)
                    {
                        if (markStart.Day != auxStart.Day)
                        {
                            returnPeriods.Add(new Period() { Name = markStart.Day.ToString(), Starts = markStart, Ends = auxStart.AddDays(-1) });
                            markStart = auxStart;
                        }
                        if (markStart.Day == auxEnds.Day && markStart.Month == auxEnds.Month && markStart.Year == auxEnds.Year)
                        {
                            returnPeriods.Add(new Period() { Name = markStart.Day.ToString(), Starts = markStart, Ends = auxEnds });
                            break;
                        }
                    }


                    auxStart = auxStart.AddDays(1);
                }
            else
            {
                int counter = 1;

                DateTime auxWeek = auxStart;

                while (auxWeek.DayOfWeek != DayOfWeek.Monday)
                {
                    auxWeek = auxWeek.AddDays(-1);
                }

                while (auxStart.CompareTo(auxEnds) <= 0)
                {

                    if (PeriodKind == DateTimeScale.Weekly)
                    {
                        if (auxStart.AddDays(7).CompareTo(auxEnds) <= 0)
                        {
                            returnPeriods.Add(new Period() { Name = "Semana: " + counter, Starts = auxStart, Ends = auxStart.AddDays(6) });

                        }
                        else
                        {
                            returnPeriods.Add(new Period() { Name = "Semana: " + counter, Starts = auxStart, Ends = auxStart.AddDays(6) });
                            break;
                        }

                    }

                    counter++;
                    auxStart = auxStart.AddDays(7);
                }


            }

            return returnPeriods;
        }
        private String ConvertMonths(int month)
        {
            if (month == 1)
                return Resources.January;
            if (month == 2)
                return Resources.February;
            if (month == 3)
                return Resources.March;
            if (month == 4)
                return Resources.April;
            if (month == 5)
                return Resources.May;
            if (month == 6)
                return Resources.June;
            if (month == 7)
                return Resources.July;
            if (month == 8)
                return Resources.August;
            if (month == 9)
                return Resources.September;
            if (month == 10)
                return Resources.October;
            if (month == 11)
                return Resources.November;

            return Resources.December;
        }
    }
}
