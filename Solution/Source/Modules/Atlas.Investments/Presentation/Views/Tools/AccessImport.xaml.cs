using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Annotations;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyName.Atlas.Investments.Presentation.Views.Tools
{
    /// <summary>
    /// Interaction logic for AccessImport.xaml
    /// </summary>
    public partial class AccessImport : UserControl, INotifyPropertyChanged
    {
        private List<object[]> tableresult;
        private BackgroundWorker _backgroundWorker;
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TransferStateProperty = DependencyProperty.Register("TransferState", typeof(AtlasTransferWizardState), typeof(AccessImport));


        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty OverGroupCodeMatchColumnProperty = DependencyProperty.Register("OverGroupCodeMatchColumn", typeof(string), typeof(AccessImport),new PropertyMetadata("-", OGCPropertyChangedCallback));

        private static void OGCPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport) dependencyObject;
            thisone.SetOGC(thisone.OverGroupCodeMatchColumn);
        }

        private void SetOGC(string thisoneOverGroupCodeMatchColumn)
        {
            _ogcodeIndex = thisoneOverGroupCodeMatchColumn;
        }

       
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty OverGroupNameMatchColumnProperty = DependencyProperty.Register("OverGroupNameMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", OGNPropertyChangedCallback));

        private static void OGNPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetOGN(thisone.OverGroupNameMatchColumn);
        }

        private void SetOGN(string thisoneOverGroupCodeMatchColumn)
        {
            _ognameIndex = thisoneOverGroupCodeMatchColumn;
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty RegularGroupCodeMatchColumnProperty = DependencyProperty.Register("RegularGroupCodeMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", RGCPropertyChangedCallback));

        private static void RGCPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetRGC(thisone.RegularGroupCodeMatchColumn);
        }

        private void SetRGC(string thisoneOverGroupCodeMatchColumn)
        {
            _rgcodeIndex = thisoneOverGroupCodeMatchColumn;
        }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty RegularGroupNameMatchColumnProperty = DependencyProperty.Register("RegularGroupNameMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", RGNPropertyChangedCallback));

        private static void RGNPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetRGN(thisone.RegularGroupNameMatchColumn);
        }

        private void SetRGN(string thisoneOverGroupCodeMatchColumn)
        {
            _rgnameIndex = thisoneOverGroupCodeMatchColumn;
        }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty UnderGroupCodeMatchColumnProperty = DependencyProperty.Register("UnderGroupCodeMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", UGCPropertyChangedCallback));
        private static void UGCPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetUGC(thisone.UnderGroupCodeMatchColumn);
        }

        private void SetUGC(string thisoneOverGroupCodeMatchColumn)
        {
            _ugcodeIndex = thisoneOverGroupCodeMatchColumn;
        }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty UnderGroupNameMatchColumnProperty = DependencyProperty.Register("UnderGroupNameMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", UGNPropertyChangedCallback));
        private static void UGNPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetUGN(thisone.UnderGroupNameMatchColumn);
        }

        private void SetUGN(string thisoneOverGroupCodeMatchColumn)
        {
            _ugnameIndex = thisoneOverGroupCodeMatchColumn;
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActivityNameMatchColumnProperty = DependencyProperty.Register("ActivityNameMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", VLNPropertyChangedCallback));
        private static void VLNPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetVLN(thisone.ActivityNameMatchColumn);
        }

        private void SetVLN(string thisoneOverGroupCodeMatchColumn)
        {
            _vlnameIndex = thisoneOverGroupCodeMatchColumn;
        }

        private string _vlnameIndex;
        public string ActivityNameMatchColumn
        {
            get { return (string)GetValue(ActivityNameMatchColumnProperty); }
            set
            {
                SetValue(ActivityNameMatchColumnProperty, value);
              //  _ugnameIndex = ActivityNameMatchColumn;
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActivityCodeMatchColumnProperty = DependencyProperty.Register("ActivityCodeMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", VLCPropertyChangedCallback));
        private static void VLCPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetVLC(thisone.ActivityCodeMatchColumn);
        }

        private void SetVLC(string thisoneOverGroupCodeMatchColumn)
        {
            _vlCodeIndex = thisoneOverGroupCodeMatchColumn;
        }

        private string _vlCodeIndex;
        public string ActivityCodeMatchColumn
        {
            get { return (string)GetValue(ActivityCodeMatchColumnProperty); }
            set
            {
                SetValue(ActivityCodeMatchColumnProperty, value);
                _vlCodeIndex = ActivityCodeMatchColumn;
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActivityPriceMatchColumnProperty = DependencyProperty.Register("ActivityPriceMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", VLPPropertyChangedCallback));
        private static void VLPPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetVLP(thisone.ActivityPriceMatchColumn);
        }

        private void SetVLP(string thisoneOverGroupPriceMatchColumn)
        {
            _VLPriceIndex = thisoneOverGroupPriceMatchColumn;
        }

        private string _VLPriceIndex;
        public string ActivityPriceMatchColumn
        {
            get { return (string)GetValue(ActivityPriceMatchColumnProperty); }
            set
            {
                SetValue(ActivityPriceMatchColumnProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActivityMUMatchColumnProperty = DependencyProperty.Register("ActivityMUMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", VLMUPropertyChangedCallback));
        private static void VLMUPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetVLMU(thisone.ActivityMUMatchColumn);
        }

        private void SetVLMU(string thisoneOverGroupMUMatchColumn)
        {
            _vlMUIndex = thisoneOverGroupMUMatchColumn;
        }

        private string _vlMUIndex;
        public string ActivityMUMatchColumn
        {
            get { return (string)GetValue(ActivityMUMatchColumnProperty); }
            set
            {
                SetValue(ActivityMUMatchColumnProperty, value);
                _vlMUIndex = ActivityMUMatchColumn;
            }
        }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActivityDescriptionMatchColumnProperty = DependencyProperty.Register("ActivityDescriptionMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", VLDPropertyChangedCallback));
        private static void VLDPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetVLD(thisone.ActivityDescriptionMatchColumn);
        }

        private void SetVLD(string thisoneOverGroupDescriptionMatchColumn)
        {
            _VLDescIndex = thisoneOverGroupDescriptionMatchColumn;
        }

        private string _VLDescIndex;
        public string ActivityDescriptionMatchColumn
        {
            get { return (string)GetValue(ActivityDescriptionMatchColumnProperty); }
            set
            {
                SetValue(ActivityDescriptionMatchColumnProperty, value);
               
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ActivityCurrencyMatchColumnProperty = DependencyProperty.Register("ActivityCurrencyMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", VLCuPropertyChangedCallback));
        private static void VLCuPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetVLCu(thisone.ActivityCurrencyMatchColumn);
        }

        private void SetVLCu(string thisoneOverGroupCurrencyMatchColumn)
        {
            _VLCuIndex = thisoneOverGroupCurrencyMatchColumn;
        }

        private string _VLCuIndex;
        public string ActivityCurrencyMatchColumn
        {
            get { return (string)GetValue(ActivityCurrencyMatchColumnProperty); }
            set
            {
                SetValue(ActivityCurrencyMatchColumnProperty, value);

            }
        }


        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceNameMatchColumnProperty = DependencyProperty.Register("ResourceNameMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResNPropertyChangedCallback));
        private static void ResNPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResN(thisone.ResourceNameMatchColumn);
        }

        private void SetResN(string thisoneOverGroupCodeMatchColumn)
        {
            _ResnameIndex = thisoneOverGroupCodeMatchColumn;
        }

        private string _ResnameIndex;
        public string ResourceNameMatchColumn
        {
            get { return (string)GetValue(ResourceNameMatchColumnProperty); }
            set
            {
                SetValue(ResourceNameMatchColumnProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceCodeMatchColumnProperty = DependencyProperty.Register("ResourceCodeMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResCPropertyChangedCallback));
        private static void ResCPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResC(thisone.ResourceCodeMatchColumn);
        }

        private void SetResC(string thisoneOverGroupCodeMatchColumn)
        {
            _ResCodeIndex = thisoneOverGroupCodeMatchColumn;
        }

        private string _ResCodeIndex;
        public string ResourceCodeMatchColumn
        {
            get { return (string)GetValue(ResourceCodeMatchColumnProperty); }
            set
            {
                SetValue(ResourceCodeMatchColumnProperty, value);
                _ResCodeIndex = ResourceCodeMatchColumn;
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourcePriceMatchColumnProperty = DependencyProperty.Register("ResourcePriceMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResPPropertyChangedCallback));
        private static void ResPPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResP(thisone.ResourcePriceMatchColumn);
        }

        private void SetResP(string thisoneOverGroupPriceMatchColumn)
        {
            _ResPriceIndex = thisoneOverGroupPriceMatchColumn;
        }

        private string _ResPriceIndex;
        public string ResourcePriceMatchColumn
        {
            get { return (string)GetValue(ResourcePriceMatchColumnProperty); }
            set
            {
                SetValue(ResourcePriceMatchColumnProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceMUMatchColumnProperty = DependencyProperty.Register("ResourceMUMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResMUPropertyChangedCallback));
        private static void ResMUPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResMU(thisone.ResourceMUMatchColumn);
        }

        private void SetResMU(string thisoneOverGroupMUMatchColumn)
        {
            _ResMUIndex = thisoneOverGroupMUMatchColumn;
        }

        private string _ResMUIndex;
        public string ResourceMUMatchColumn
        {
            get { return (string)GetValue(ResourceMUMatchColumnProperty); }
            set
            {
                SetValue(ResourceMUMatchColumnProperty, value);
             
            }
        }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceDescriptionMatchColumnProperty = DependencyProperty.Register("ResourceDescriptionMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResDPropertyChangedCallback));
        private static void ResDPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResD(thisone.ResourceDescriptionMatchColumn);
        }

        private void SetResD(string thisoneOverGroupDescriptionMatchColumn)
        {
            _ResDescIndex = thisoneOverGroupDescriptionMatchColumn;
        }

        private string _ResDescIndex;
        public string ResourceDescriptionMatchColumn
        {
            get { return (string)GetValue(ResourceDescriptionMatchColumnProperty); }
            set
            {
                SetValue(ResourceDescriptionMatchColumnProperty, value);

            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceCurrencyMatchColumnProperty = DependencyProperty.Register("ResourceCurrencyMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResCuPropertyChangedCallback));
        private static void ResCuPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResCu(thisone.ResourceCurrencyMatchColumn);
        }

        private void SetResCu(string thisoneOverGroupCurrencyMatchColumn)
        {
            _ResCuIndex = thisoneOverGroupCurrencyMatchColumn;
        }

        private string _ResCuIndex;
        public string ResourceCurrencyMatchColumn
        {
            get { return (string)GetValue(ResourceCurrencyMatchColumnProperty); }
            set
            {
                SetValue(ResourceCurrencyMatchColumnProperty, value);

            }
        }


        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceKindMatchColumnProperty = DependencyProperty.Register("ResourceKindMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResKindPropertyChangedCallback));
        private static void ResKindPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResKind(thisone.ResourceKindMatchColumn);
        }

        private void SetResKind(string thisoneOverGroupKindMatchColumn)
        {
            _ResKindIndex = thisoneOverGroupKindMatchColumn;
        }

        private string _ResKindIndex;
        public string ResourceKindMatchColumn
        {
            get { return (string)GetValue(ResourceKindMatchColumnProperty); }
            set
            {
                SetValue(ResourceKindMatchColumnProperty, value);

            }
        }
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceNormMatchColumnProperty = DependencyProperty.Register("ResourceNormMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResnormPropertyChangedCallback));
        private static void ResnormPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResnorm(thisone.ResourceNormMatchColumn);
        }

        private void SetResnorm(string thisoneOverGroupNormMatchColumn)
        {
            _ResnormIndex = thisoneOverGroupNormMatchColumn;
        }

        private string _ResnormIndex;
        public string ResourceNormMatchColumn
        {
            get { return (string)GetValue(ResourceNormMatchColumnProperty); }
            set
            {
                SetValue(ResourceNormMatchColumnProperty, value);

            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceWVMatchColumnProperty = DependencyProperty.Register("ResourceWVMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResWVPropertyChangedCallback));
        private static void ResWVPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResWV(thisone.ResourceWVMatchColumn);
        }

        private void SetResWV(string thisoneOverGroupWVMatchColumn)
        {
            _ResWVIndex = thisoneOverGroupWVMatchColumn;
        }

        private string _ResWVIndex;
        public string ResourceWVMatchColumn
        {
            get { return (string)GetValue(ResourceWVMatchColumnProperty); }
            set
            {
                SetValue(ResourceWVMatchColumnProperty, value);

            }
        }

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="UIControls.AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ResourceWMUMatchColumnProperty = DependencyProperty.Register("ResourceWMUMatchColumn", typeof(string), typeof(AccessImport), new PropertyMetadata("-", ResWUMVPropertyChangedCallback));
        private static void ResWUMVPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var thisone = (AccessImport)dependencyObject;
            thisone.SetResWUMV(thisone.ResourceWMUMatchColumn);
        }

        private void SetResWUMV(string thisoneOverGroupWUMVMatchColumn)
        {
            _ResWUMVIndex = thisoneOverGroupWUMVMatchColumn;
        }

        private string _ResWUMVIndex;
        public string ResourceWMUMatchColumn
        {
            get { return (string)GetValue(ResourceWMUMatchColumnProperty); }
            set
            {
                SetValue(ResourceWMUMatchColumnProperty, value);

            }
        }

        public AccessImport()
        {
            InitializeComponent();

            TransferState = AtlasTransferWizardState.Welcome;

            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

            OverGroupCodeMatchColumn = "0";
            OverGroupNameMatchColumn = "1";

            RegularGroupCodeMatchColumn = "2";
            RegularGroupNameMatchColumn = "3";

            UnderGroupCodeMatchColumn = "4";
            UnderGroupNameMatchColumn = "5";

            ActivityCodeMatchColumn = "6";
            ActivityNameMatchColumn = "7";
            ActivityDescriptionMatchColumn = "7";
            ActivityMUMatchColumn = "8";
            ActivityCurrencyMatchColumn = "9";
            ActivityPriceMatchColumn = "10";

            ResourceCodeMatchColumn = "11";
            ResourceNameMatchColumn = "12";
            ResourceDescriptionMatchColumn = "12";
            ResourceNormMatchColumn = "13";
            ResourceMUMatchColumn = "14";
            ResourceCurrencyMatchColumn = "15";
            ResourceKindMatchColumn = "16";
            ResourceWVMatchColumn = "17";
            ResourceWMUMatchColumn = "18";
            ResourcePriceMatchColumn = "19";




        }

        private int commandsCount = 0;
        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            var commands = SQLCommandTextBox.Text.Split('\n');

            FinishText.Text = finishMessage += "Command #"+(commandsCount+1)+". Started at: " + startDateTime + ". Finished at: " + DateTime.Now + ".\n";
            commandsCount++;


            if (commands.Length > 1 && commands.Length > commandsCount)
            {
                
                    var queryresult = dbControl.SelectQuerryFixed(commands[commandsCount]);


                    // get all the menlabor resources
                    tableresult = queryresult.Tables[0].Rows.Cast<DataRow>().Aggregate(new List<object[]>(), (list, pr) =>
                    {

                        list.Add(pr.ItemArray);
                        return list;
                    });

                    _backgroundWorker.RunWorkerAsync();
                
            }
            
            if(commands.Length == commandsCount)
            TransferState = AtlasTransferWizardState.Finish;
        }

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;

            ProgressTextBlock.Text = "Command #"+(commandsCount+1)+": Importing progress "+e.ProgressPercentage+"%: " + count.ToString() + " records, out of " + totalRecords.ToString() +
                                     "...";
        }

        private string GetRealCode(string code, int choice)
        {

            if (code.Length == 2)
                return code;
            if (code.Length == 6)
            {
                if (choice == 0)
                {
                    return code.Substring(0, 2);
                }
                if (choice == 1)
                {
                    return code.Substring(2, 2);
                }
                if (choice == 2)
                {
                    return code.Substring(4, 2);
                }
            }
               

            return code;
        }

        private bool cancel;

        private String finishMessage = "Import transfer Completed Successfully.";

        private DateTime startDateTime;

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                startDateTime = DateTime.Now;
                IPriceSystemPresenter PsPresenter = priceSystemPresenter;

                totalRecords = tableresult.Count;

                count = 0;

                foreach (object[] dataRow in tableresult)
                {

                    string code = GetRealCode(dataRow[Convert.ToInt32(_ogcodeIndex)].ToString(), 0);

                    if (PsPresenter.ExistOverGroup(code))
                    {
                        var ogroup = PsPresenter.GetOverGroup(code);

                        if (_overwrite)
                        {
                            

                            ogroup.Name = dataRow[Convert.ToInt32(_ognameIndex)].ToString();
                        }
                        

                        ImportThroughGroups(dataRow, ogroup);
                    }
                    else
                    {

                        PsPresenter.AddFromScratch(code, dataRow[Convert.ToInt32(_ognameIndex)].ToString());

                        var ogroup = PsPresenter.GetOverGroup(code);

                        ImportThroughGroups(dataRow, ogroup);
                    }

                    count++;

                    int percent = Convert.ToInt32((count * 100) / totalRecords);
                    _backgroundWorker.ReportProgress(percent);

                    if (cancel)
                        break;
                }
            }
            catch (Exception ex)
            {
                finishMessage = "Started at: "+ startDateTime.ToLongDateString()+". Finished by error: " + ex.Message + "\n" + ex.InnerException?.Message+". at:  "+DateTime.Now+".";
               // ErrorsList.Items.Add("Error: " + finishMessage);
                // TransferState = AtlasTransferWizardState.Finish;
            }


        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
          OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = @"Access files (*.mdb)|*.mdb|All files (*.*)|*.*";
            openFile.FileOk+=OpenFileOnFileOk;
            openFile.ShowDialog();
        }

        private void OpenFileOnFileOk(object sender, CancelEventArgs cancelEventArgs)
        {
            DataUriTextBox.Text = ((OpenFileDialog) sender).FileName;
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            //DBControl dbControl = new DBControl();

            //dbControl.DataUri = DataUriTextBox.Text;

            //dbControl.Load();

            //var queryresult = dbControl.SelectQuerryFixed("Select * From OK");
            //IntroGrid.Visibility = Visibility.Collapsed;
            //SQLGrid.Visibility = Visibility.Visible;

           TransferState = AtlasTransferWizardState.Select;
           
            //DoubleAnimation shrinkAnimation = new DoubleAnimation(IntroGrid.ActualWidth, 0, new Duration(TimeSpan.FromMilliseconds(250)));

            //IntroGrid.BeginAnimation(WidthProperty, shrinkAnimation);

            //DoubleAnimation expandAnimation = new DoubleAnimation(0, this.ActualWidth - 40, new Duration(TimeSpan.FromMilliseconds(250)));

            //expandAnimation.Completed += ExpandAnimationOnCompleted;

            //SQLGrid.BeginAnimation(WidthProperty, expandAnimation);
           
           
        }

        public AtlasTransferWizardState TransferState
        {
            get { return (AtlasTransferWizardState)GetValue(TransferStateProperty); }
            set
            {
                SetValue(TransferStateProperty, value);
                OnPropertyChanged(nameof(TransferState));
            }
        }

        private string _ogcodeIndex;
        public string OverGroupCodeMatchColumn
        {
            get { return (string)GetValue(OverGroupCodeMatchColumnProperty); }
            set
            {


                SetValue(OverGroupCodeMatchColumnProperty, value);

                _ogcodeIndex = OverGroupCodeMatchColumn;
            }
        }

        private string _ognameIndex;
        public string OverGroupNameMatchColumn
        {
            get { return (string)GetValue(OverGroupNameMatchColumnProperty); }
            set
            {
                SetValue(OverGroupNameMatchColumnProperty, value);
                _ognameIndex = OverGroupNameMatchColumn;
            }
        }
        private string _rgcodeIndex;
        public string RegularGroupCodeMatchColumn
        {
            get { return (string)GetValue(RegularGroupCodeMatchColumnProperty); }
            set
            {
                SetValue(RegularGroupCodeMatchColumnProperty, value);
                _rgcodeIndex = RegularGroupCodeMatchColumn;
            }
        }

        private string _rgnameIndex;
        public string RegularGroupNameMatchColumn
        {
            get { return (string)GetValue(RegularGroupNameMatchColumnProperty); }
            set
            {
                SetValue(RegularGroupNameMatchColumnProperty, value);
                _rgnameIndex = RegularGroupNameMatchColumn;
            }
        }

        private string _ugcodeIndex;
        public string UnderGroupCodeMatchColumn
        {
            get { return (string)GetValue(UnderGroupCodeMatchColumnProperty); }
            set
            {
                SetValue(UnderGroupCodeMatchColumnProperty, value);
                _ugcodeIndex = UnderGroupCodeMatchColumn;
            }
        }
        private string _ugnameIndex;
        public string UnderGroupNameMatchColumn
        {
            get { return (string)GetValue(UnderGroupNameMatchColumnProperty); }
            set
            {
                SetValue(UnderGroupNameMatchColumnProperty, value);
               // _ugnameIndex = UnderGroupNameMatchColumn;
            }
        }

        private DBControlOdbc dbControl;

        private void ButtonBase_OnClick4(object sender, RoutedEventArgs e)
        {
            dbControl = new DBControlOdbc();

            dbControl.DataUri = DataUriTextBox.Text;

            dbControl.Load();

            var commands = SQLCommandTextBox.Text.Split('\n');
            var queryresult = dbControl.SelectQuerryFixed(commands[commandsCount]);


            // get all the menlabor resources
            tableresult = queryresult.Tables[0].Rows.Cast<DataRow>().Aggregate(new List<object[]>(), (list, pr) =>
            {

                list.Add(pr.ItemArray);
                return list;
            });

            int index = 0;
            AtlasDataGrid.Columns.Clear();
            foreach (DataColumn dataColumn in queryresult.Tables[0].Columns)
            {
                AtlasDataGrid.Columns.Add( new DataGridTextColumn() { Header = dataColumn.ColumnName, Binding = new Binding() {  Converter = new DataStructureConverter(), ConverterParameter = index} });
                index++;
            }
           
            AtlasDataGrid.Visibility = Visibility.Visible;
            AtlasDataGrid.ItemsSource = tableresult.Take(20);

            PSStackPanel.DataContext = ServiceLocator.Current.GetInstance<IPriceSystemViewModel>();
        }
        private void ButtonBase_OnClick5(object sender, RoutedEventArgs e)
        {
           
           if(this.Parent != null  && this.Parent.GetType() == typeof(ToolsWindow))
                ((ToolsWindow)this.Parent).Close();
        }


        private void Import_OnClick(object sender, RoutedEventArgs e)
        {
            if (PSComboBox.SelectedItem != null)
            {
                TransferState = AtlasTransferWizardState.Path;

               _backgroundWorker.RunWorkerAsync();
                

            }
        }

        private void ImportThroughGroups(object[] dataRow, IOverGroupPresenter overGroup)
        {

            string code = GetRealCode(dataRow[Convert.ToInt32(_rgcodeIndex)].ToString(), 1);

            if (String.IsNullOrEmpty(code))
                return;

            if (overGroup.ExistGroup(code))
            {
                var rgroup = overGroup.GetRegularGroup(code);

                if(_overwrite)
                rgroup.Name = dataRow[Convert.ToInt32(_rgnameIndex)].ToString();

                ImportThroughUnderGroups(dataRow, rgroup);
            }
            else
            {

                overGroup.AddFromScratch(code, dataRow[Convert.ToInt32(_rgnameIndex)].ToString());

                var rgroup = overGroup.GetRegularGroup(code);

                ImportThroughUnderGroups(dataRow, rgroup);
            }
        }

        private void ImportThroughUnderGroups(object[] dataRow, IRegularGroupPresenter regularGroup)
        {
            string code = GetRealCode(dataRow[Convert.ToInt32(_ugcodeIndex)].ToString(), 2);

            if (String.IsNullOrEmpty(code))
                return;

            if (regularGroup.ExistUnderGroup(code))
            {
                var ugroup = regularGroup.GetUnderGroup(code);

                if(_overwrite)
                ugroup.Name = dataRow[Convert.ToInt32(_ugnameIndex)].ToString();

                ImportThroughActivities(dataRow, ugroup);
            }
            else
            {

                regularGroup.AddFromScratch(code, dataRow[Convert.ToInt32(_ugnameIndex)].ToString());

                var ugroup = regularGroup.GetUnderGroup(code);

                ImportThroughActivities(dataRow,ugroup);
            }
        }

        private String GetResCode(string code)
        {
            if (!_check4Code)
                return code;

            string restult = code;
            for (int i = 0; i < code.Length - 8; i++)
            {
                restult = "0" + restult;
            }

            return restult;
        }
        private void ImportThroughActivities(object[] dataRow, IUnderGroupPresenter underGroup)
        {
            string code = dataRow[Convert.ToInt32(_vlCodeIndex)].ToString();

            if (String.IsNullOrEmpty(code))
                return;

            if (underGroup.ExistActivity(code))
            {
                var activity = underGroup.GetActivity(code);

                if (_overwrite)
                {

                    var activityName = dataRow[Convert.ToInt32(_vlnameIndex)].ToString();


                    var activityDescription = dataRow[Convert.ToInt32(_VLDescIndex)].ToString();


                    var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                    var muname = dataRow[Convert.ToInt32(_vlMUIndex)].ToString();

                    if (! measurementsUnits.ExistsConvertible(muname))
                    {
                      
                        measurementsUnits.AddFromScratch(muname, muname);
                       
                    }

                    var mu = measurementsUnits.GetConvertible(muname);

                    var activityMeasurementUnit = mu.Id;



                    var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();
                   

                    var curname = dataRow[Convert.ToInt32(_VLCuIndex)].ToString();

                    if (!currencies.ExistsConvertible(curname))
                    {

                       
                        currencies.AddFromScratch(curname, curname);


                       
                    }

                    var currency = currencies.GetConvertible(curname);

                    var activityCurrency = currency.Id;

                    decimal price = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_VLPriceIndex)].ToString())? 0 :Convert.ToDecimal(dataRow[Convert.ToInt32(_VLPriceIndex)].ToString());

                    underGroup.Activities.EditFromScratch(activity.Id, activityName, activityDescription, activityMeasurementUnit, activityCurrency, price);


                }



                ImportThroughResources(dataRow, activity);
               
                    
            }
            else
            {

                var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                var muname = dataRow[Convert.ToInt32(_vlMUIndex)].ToString();

                if (!measurementsUnits.ExistsConvertible(muname))
                {

                    measurementsUnits.AddFromScratch(muname, muname);

                }

                var mu = measurementsUnits.GetConvertible(muname);

                var activityMeasurementUnit = mu.Id;



                var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                var curname = dataRow[Convert.ToInt32(_VLCuIndex)].ToString();

                if (!currencies.ExistsConvertible(curname))
                {


                    currencies.AddFromScratch(curname, curname);



                }

                var currency = currencies.GetConvertible(curname);

                var activityCurrency = currency.Id;


                decimal price = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_VLPriceIndex)].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_VLPriceIndex)].ToString());

                underGroup.AddFromScratch(code, dataRow[Convert.ToInt32(_vlnameIndex)].ToString(), 
                    dataRow[Convert.ToInt32(_VLDescIndex)].ToString(), activityMeasurementUnit, activityCurrency, price) ;

                var activity = underGroup.GetActivity(code);

                ImportThroughResources(dataRow, activity);

            }
        }

        private void ImportThroughResources(object[] dataRow, IUnderGroupActivityPresenter activityPresenter)
        {
            string code = GetResCode(dataRow[Convert.ToInt32(_ResCodeIndex)].ToString());

            if(String.IsNullOrEmpty(code))
                return;

            if (activityPresenter.ExistPlannedResource(code))
            {
                var resource = activityPresenter.GetPlannedResource(code);

                if (_overwrite)
                {

                    var resourceName = dataRow[Convert.ToInt32(_ResnameIndex)].ToString();


                    var resourceDescription = dataRow[Convert.ToInt32(_ResDescIndex)].ToString();


                    var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                    var muname = dataRow[Convert.ToInt32(_ResMUIndex)].ToString();

                    if (!measurementsUnits.ExistsConvertible(muname))
                    {

                        measurementsUnits.AddFromScratch(muname, muname);

                    }

                    var mu = measurementsUnits.GetConvertible(muname);

                    var activityMeasurementUnit = mu.Id;



                    var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                    var curname = dataRow[Convert.ToInt32(_ResCuIndex)].ToString();

                    if (!currencies.ExistsConvertible(curname))
                    {


                        currencies.AddFromScratch(curname, curname);



                    }

                    var currency = currencies.GetConvertible(curname);

                    var activityCurrency = currency.Id;


                    decimal resourcePrice = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResPriceIndex)].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex)].ToString());

                    //  decimal resourcePrice = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex)].ToString());
                    decimal resourceNorm = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResnormIndex)].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex)].ToString());

                    // decimal resourceNorm = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex)].ToString());

                    int kind = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResKindIndex)].ToString()) ? 0 : Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex)].ToString());

                   // int kind = Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex)].ToString());

                    decimal wv =  String.IsNullOrEmpty( dataRow[Convert.ToInt32(_ResWVIndex)].ToString())? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResWVIndex)].ToString());

                    //wEIGTH

                    // Weight

                    IConvertibleEntityPresenter<IMeasurementUnit> muw = null;
                    if (_getWeight)
                    {

                        var munamew = dataRow[Convert.ToInt32(_ResWUMVIndex)].ToString();
                      

                        if (!measurementsUnits.ExistsConvertible(munamew))
                        {
                            


                            measurementsUnits.AddFromScratch(muname, muname);

                           
                        }

                         muw = measurementsUnits.GetConvertible(munamew);
                    }
                   

                    //TODO
                    activityPresenter.PlannedResources.EditFromScratch(resource.Id, dataRow[Convert.ToInt32(_ResnameIndex)].ToString(),
                        dataRow[Convert.ToInt32(_ResDescIndex)].ToString(), mu.Id, currency.Id, resourceNorm, resourcePrice, kind, muw?.Id, wv);

                }





                if (dataRow.Length - 1 > Convert.ToInt32(_ResPriceIndex))
                    ImportThroughResources(dataRow, resource, Convert.ToInt32(_ResPriceIndex) - Convert.ToInt32(_ResCodeIndex) + 1, 1);

            }
            else
            {

                var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                var muname = dataRow[Convert.ToInt32(_ResMUIndex)].ToString();

                if (!measurementsUnits.ExistsConvertible(muname))
                {

                    measurementsUnits.AddFromScratch(muname, muname);

                }

                var mu = measurementsUnits.GetConvertible(muname);

                var activityMeasurementUnit = mu.Id;



                var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                var curname = dataRow[Convert.ToInt32(_ResCuIndex)].ToString();

                if (!currencies.ExistsConvertible(curname))
                {


                    currencies.AddFromScratch(curname, curname);



                }

                var currency = currencies.GetConvertible(curname);

                var activityCurrency = currency.Id;



                //   decimal price = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex)].ToString());
                decimal price = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResPriceIndex)].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex)].ToString());

                // decimal norm = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex)].ToString());
                decimal norm = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResnormIndex)].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex)].ToString());

                int kind = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResKindIndex)].ToString()) ? 0 : Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex)].ToString());


                decimal wv =  String.IsNullOrEmpty( dataRow[Convert.ToInt32(_ResWVIndex)].ToString())? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResWVIndex)].ToString());


                //wEIGTH

                // Weight

                IConvertibleEntityPresenter<IMeasurementUnit> muw = null;
                if (_getWeight)
                {

                    var munamew = dataRow[Convert.ToInt32(_ResWUMVIndex)].ToString();


                    if (!measurementsUnits.ExistsConvertible(munamew))
                    {



                        measurementsUnits.AddFromScratch(munamew, munamew);


                    }

                    muw = measurementsUnits.GetConvertible(munamew);
                }


                activityPresenter.AddFromScratch(code, dataRow[Convert.ToInt32(_ResnameIndex)].ToString(),
                    dataRow[Convert.ToInt32(_ResDescIndex)].ToString(), mu.Id, currency.Id, norm, price, kind, muw?.Id, wv);



                var resource = activityPresenter.GetPlannedResource(code);

                if (dataRow.Length - 1 > Convert.ToInt32(_ResPriceIndex))
                    ImportThroughResources(dataRow, resource, Convert.ToInt32(_ResPriceIndex) - Convert.ToInt32(_ResCodeIndex) + 1, 1);


            }
        }

        private void ImportThroughResources(object[] dataRow, IPlannedResourcePresenter<IUnderGroupActivity> resourcePresenter, int increment, int depth)
        {
            string code = GetResCode(dataRow[Convert.ToInt32(_ResCodeIndex) + increment].ToString());

            if (String.IsNullOrEmpty(code))
                return;

            if (resourcePresenter.ExistPlannedResource(code))
            {
                var resource = resourcePresenter.GetPlannedResource(code);


                if (_overwrite)
                {

                    var resourceName = dataRow[Convert.ToInt32(_ResnameIndex) + increment].ToString();


                    var resourceDescription = dataRow[Convert.ToInt32(_ResDescIndex) + increment].ToString();


                    var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                    var muname = dataRow[Convert.ToInt32(_ResMUIndex) + increment].ToString();

                    if (!measurementsUnits.ExistsConvertible(muname))
                    {

                        measurementsUnits.AddFromScratch(muname, muname);

                    }

                    var mu = measurementsUnits.GetConvertible(muname);

                    var activityMeasurementUnit = mu.Id;



                    var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                    var curname = dataRow[Convert.ToInt32(_ResCuIndex) + increment].ToString();

                    if (!currencies.ExistsConvertible(curname))
                    {


                        currencies.AddFromScratch(curname, curname);



                    }

                    var currency = currencies.GetConvertible(curname);

                    var activityCurrency = currency.Id;

                    decimal resourcePrice = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                  //  decimal resourcePrice = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                    decimal resourceNorm = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                 //   decimal resourceNorm = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                   // int kind = Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());
                    int kind = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString()) ? 0 : Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());



                    decimal wv = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString());

                    //wEIGTH

                    // Weight

                    IConvertibleEntityPresenter<IMeasurementUnit> muw = null;
                    if (_getWeight)
                    {

                        var munamew = dataRow[Convert.ToInt32(_ResWUMVIndex) + increment].ToString();


                        if (!measurementsUnits.ExistsConvertible(munamew))
                        {



                            measurementsUnits.AddFromScratch(munamew, munamew);


                        }

                        muw = measurementsUnits.GetConvertible(munamew);
                    }


                    resourcePresenter.PlannedResources.EditFromScratch(resource.Id, dataRow[Convert.ToInt32(_ResnameIndex) + increment].ToString(),
                        dataRow[Convert.ToInt32(_ResDescIndex) + increment].ToString(), mu.Id, currency.Id, resourceNorm, resourcePrice, kind, muw?.Id, wv);


                }




                if (dataRow.Length - 1 > Convert.ToInt32(_ResPriceIndex) + increment)
                    ImportThroughResources(dataRow, resource, increment * (depth+1), (depth + 1));

            }
            else
            {

                var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                var muname = dataRow[Convert.ToInt32(_ResMUIndex) + increment].ToString();

                if (!measurementsUnits.ExistsConvertible(muname))
                {

                    measurementsUnits.AddFromScratch(muname, muname);

                }

                var mu = measurementsUnits.GetConvertible(muname);

                var activityMeasurementUnit = mu.Id;



                var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                var curname = dataRow[Convert.ToInt32(_ResCuIndex) + increment].ToString();

                if (!currencies.ExistsConvertible(curname))
                {


                    currencies.AddFromScratch(curname, curname);



                }

                var currency = currencies.GetConvertible(curname);

                var activityCurrency = currency.Id;


                decimal price = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                //  decimal resourcePrice = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                decimal norm = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                //   decimal resourceNorm = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                // int kind = Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());
                int kind = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString()) ? 0 : Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());


                decimal wv = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString());

                //wEIGTH

                // Weight

                IConvertibleEntityPresenter<IMeasurementUnit> muw = null;
                if (_getWeight)
                {

                    var munamew = dataRow[Convert.ToInt32(_ResWUMVIndex) + increment].ToString();


                    if (!measurementsUnits.ExistsConvertible(munamew))
                    {



                        measurementsUnits.AddFromScratch(munamew, munamew);


                    }

                    muw = measurementsUnits.GetConvertible(munamew);
                }


                resourcePresenter.AddFromScratch(code, dataRow[Convert.ToInt32(_ResnameIndex) + increment].ToString(),
                    dataRow[Convert.ToInt32(_ResDescIndex) + increment].ToString(), mu.Id, currency.Id, norm, price, kind, muw?.Id, wv);



              
                var resource = resourcePresenter.GetPlannedResource(code);

                if (dataRow.Length - 1 > Convert.ToInt32(_ResPriceIndex) + increment)
                    ImportThroughResources(dataRow, resource, increment * (depth + 1), (depth + 1));

            }
        }

        private void ImportThroughResources(object[] dataRow, IPlannedResourcePresenter<IPlannedResource> resourcePresenter, int increment, int depth)
        {
            string code = GetResCode(dataRow[Convert.ToInt32(_ResCodeIndex) + increment].ToString());

            if (String.IsNullOrEmpty(code))
                return;

            if (resourcePresenter.ExistPlannedResource(code))
            {
                var resource = resourcePresenter.GetPlannedResource(code);

                if (_overwrite)
                {
                    var resourceName = dataRow[Convert.ToInt32(_ResnameIndex) + increment].ToString();


                    var resourceDescription = dataRow[Convert.ToInt32(_ResDescIndex) + increment].ToString();


                    var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                    var muname = dataRow[Convert.ToInt32(_ResMUIndex) + increment].ToString();

                    if (!measurementsUnits.ExistsConvertible(muname))
                    {

                        measurementsUnits.AddFromScratch(muname, muname);

                    }

                    var mu = measurementsUnits.GetConvertible(muname);

                    var activityMeasurementUnit = mu.Id;



                    var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                    var curname = dataRow[Convert.ToInt32(_ResCuIndex) + increment].ToString();

                    if (!currencies.ExistsConvertible(curname))
                    {


                        currencies.AddFromScratch(curname, curname);



                    }

                    var currency = currencies.GetConvertible(curname);

                    var activityCurrency = currency.Id;


                    decimal resourcePrice = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                    //  decimal resourcePrice = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                    decimal resourceNorm = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                    //   decimal resourceNorm = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                    // int kind = Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());
                    int kind = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString()) ? 0 : Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());


                    decimal wv = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString());

                    //wEIGTH

                    // Weight

                    IConvertibleEntityPresenter<IMeasurementUnit> muw = null;
                    if (_getWeight)
                    {

                        var munamew = dataRow[Convert.ToInt32(_ResWUMVIndex) + increment].ToString();


                        if (!measurementsUnits.ExistsConvertible(munamew))
                        {



                            measurementsUnits.AddFromScratch(munamew, munamew);


                        }

                        muw = measurementsUnits.GetConvertible(munamew);
                    }


                    //TODO
                    resourcePresenter.PlannedResources.EditFromScratch(resource.Id, dataRow[Convert.ToInt32(_ResnameIndex) + increment].ToString(),
                        dataRow[Convert.ToInt32(_ResDescIndex) + increment].ToString(), mu.Id, currency.Id, resourceNorm, resourcePrice, kind, muw?.Id, wv);


                }




                if (dataRow.Length - 1 > Convert.ToInt32(_ResPriceIndex)+ increment)
                    ImportThroughResources(dataRow, resource, increment * (depth + 1), (depth + 1));

            }
            else
            {
                var measurementsUnits = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();

                var muname = dataRow[Convert.ToInt32(_ResMUIndex) + increment].ToString();

                if (!measurementsUnits.ExistsConvertible(muname))
                {

                    measurementsUnits.AddFromScratch(muname, muname);

                }

                var mu = measurementsUnits.GetConvertible(muname);

                var activityMeasurementUnit = mu.Id;



                var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();


                var curname = dataRow[Convert.ToInt32(_ResCuIndex) + increment].ToString();

                if (!currencies.ExistsConvertible(curname))
                {


                    currencies.AddFromScratch(curname, curname);



                }

                var currency = currencies.GetConvertible(curname);

                var activityCurrency = currency.Id;



                decimal price = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                //  decimal resourcePrice = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResPriceIndex) + increment].ToString());
                decimal norm = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                //   decimal resourceNorm = Convert.ToDecimal(dataRow[Convert.ToInt32(_ResnormIndex) + increment].ToString());
                // int kind = Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());
                int kind = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString()) ? 0 : Convert.ToInt32(dataRow[Convert.ToInt32(_ResKindIndex) + increment].ToString());

                decimal wv = String.IsNullOrEmpty(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString()) ? 0 : Convert.ToDecimal(dataRow[Convert.ToInt32(_ResWVIndex) + increment].ToString());

                //wEIGTH

                // Weight

                IConvertibleEntityPresenter<IMeasurementUnit> muw = null;
                if (_getWeight)
                {

                    var munamew = dataRow[Convert.ToInt32(_ResWUMVIndex) + increment].ToString();


                    if (!measurementsUnits.ExistsConvertible(munamew))
                    {



                        measurementsUnits.AddFromScratch(munamew, munamew);


                    }

                    muw = measurementsUnits.GetConvertible(munamew);
                }


                resourcePresenter.AddFromScratch(code, dataRow[Convert.ToInt32(_ResnameIndex) + increment].ToString(),
                    dataRow[Convert.ToInt32(_ResDescIndex) + increment].ToString(), mu.Id, currency.Id, norm, price, kind, muw?.Id, wv);

                var resource = resourcePresenter.GetPlannedResource(code);

                if (dataRow.Length - 1 > Convert.ToInt32(_ResPriceIndex)+ increment)
                    ImportThroughResources(dataRow, resource, increment * (depth + 1), (depth + 1));


            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private IPriceSystemPresenter priceSystemPresenter;
        private int totalRecords;
        private int count;
        private bool _check4Code = true;
        private bool _getWeight = true;
        private bool _overwrite;

        private void PSComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            priceSystemPresenter = PSComboBox.SelectedItem as IPriceSystemPresenter;
        }

        private void ButtonBase_OnClick33(object sender, RoutedEventArgs e)
        {
            if (!cancel)
            {
                cancel = true;

               
            }
        }

        private void Check4Code_OnChecked(object sender, RoutedEventArgs e)
        {
            _check4Code = true;
        }

        private void Check4Code_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _check4Code = false;
        }

        private void GetWeight_OnChecked(object sender, RoutedEventArgs e)
        {
            _getWeight = true;
        }

        private void GetWeight_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _getWeight = false;
        }

        private void Overwrite_OnChecked(object sender, RoutedEventArgs e)
        {
            _overwrite = true;
        }

        private void Overwrite_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _overwrite = false;
        }
    }


    public class DataStructureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rowItem = value as object[];
            int index = (int) parameter;

            return rowItem[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
