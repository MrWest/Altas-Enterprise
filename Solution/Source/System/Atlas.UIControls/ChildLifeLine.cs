using System;

namespace CompanyName.Atlas.UIControls
{
    // TODO: Comment
    public class ChildLifeLine : IChildLifeLine
    {
        private readonly DateTime _end;
        private readonly DateTime _fend;
        private readonly DateTime _fstart;
        private readonly string _name;
        private readonly DateTime _start;

        public ChildLifeLine(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;

            _name = "Some Child";
        }

        public ChildLifeLine(DateTime start, DateTime end, DateTime fstart, DateTime fend)
        {
            _start = start;
            _end = end;
            _fstart = fstart;
            _fend = fend;
            _name = "Some Child";
        }

        /// <summary>
        ///     Gets the name the currnet ChildLifeLine.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        public int Percent
        {
            get { return GetPercent(); }
        }

        public int StartPercent
        {
            get { return GetStartPercent(); }
        }

        /// <summary>
        ///     Gets the start date of the currnet ChildLifeLine.
        /// </summary>
        public DateTime Start
        {
            get { return _start; }
        }


        /// <summary>
        ///     Gets the end date of the currnet ChildLifeLine.
        /// </summary>
        public DateTime End
        {
            get { return _end; }
        }

        private int GetPercent()
        {
            int fatherdays = GetDays(_fstart, _fend);
            int mydays = GetDays(_start, _end);
            return fatherdays == 0 ? 0 : mydays * 100 / fatherdays;
        }

        private int GetStartPercent()
        {
            int fatherdays = GetDays(_fstart, _fend);
            int mydays = GetDays(_fstart, _start);
            return fatherdays == 0 ? 0 : mydays * 100 / fatherdays;
        }

        private int GetDays(DateTime starts, DateTime ends)
        {
            int days = 0;
            DateTime aux = starts;
            while (aux.CompareTo(ends) <= 0)
            {
                aux = aux.AddDays(1);
                days++;
            }

            return days;
        }
    }
}