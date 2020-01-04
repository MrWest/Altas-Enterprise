
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.UIControls.Annotations;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Implements a datagrid with the functionalities for managing the cash flow view
    /// </summary>
    public class AtlasCashFlowDataGrid:DataGrid,INotifyPropertyChanged
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty DataGridColumnsProperty = DependencyProperty.Register("DataGridColumns", typeof(IList<DataGridTextColumn>), typeof(AtlasCashFlowDataGrid), new PropertyMetadata(OnDataGridColumnsChanged));

        private static void OnDataGridColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          ((AtlasCashFlowDataGrid)d).ReColumn();
        }

        /// <summary>
        /// Gets or sets wheher the total summary is shown.
        /// </summary>
        public IList<DataGridTextColumn> DataGridColumns
        {
            get { return (IList<DataGridTextColumn>)GetValue(DataGridColumnsProperty); }
            set
            {
                SetValue(DataGridColumnsProperty, value);
                OnPropertyChanged("Columns");
            }
        }

      

        private void ReColumn()
        {
            Columns.Clear();
            if(DataGridColumns!=null)
            foreach (DataGridTextColumn gridColumn in DataGridColumns)
                Columns.Add(gridColumn);
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
