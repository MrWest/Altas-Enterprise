using System.ComponentModel;
using CompanyName.Atlas.Contracts.Presentation.Reporting;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting
{
    public class DataClass :IDataClass, INotifyPropertyChanged
    {
        public string Category { get; set; }

        private decimal _number = 0;
        public decimal Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Number"));
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
