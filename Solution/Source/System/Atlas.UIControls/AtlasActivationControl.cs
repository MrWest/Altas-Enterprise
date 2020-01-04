using System;
using System.Cryptography;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Features;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using Sharpen.IO;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasActivationControl: TextBox
    {
        
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ActivationStatusProperty = DependencyProperty.Register("ActivationStatus", typeof(ActivationStatus), typeof(AtlasActivationControl), new PropertyMetadata(ActivationStatus.Unaviable));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty IsActivatedProperty = DependencyProperty.Register("IsActivated", typeof(bool), typeof(AtlasActivationControl), new PropertyMetadata(false));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ActivationTextProperty = DependencyProperty.Register("ActivationText", typeof(string), typeof(AtlasActivationControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ActivationCodeProperty = DependencyProperty.Register("ActivationCode", typeof(string), typeof(AtlasActivationControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty ActivateCommandProperty = DependencyProperty.Register("ActivateCommand", typeof(ICommand), typeof(AtlasActivationControl), new PropertyMetadata(null));

        private  void ExecuteMethod()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "easy";
            openFileDialog.Filter = "Archivos Key (*.key)|*.key|Todos los Archivos (*.*)|*.*";
            openFileDialog.ShowDialog();
            if (System.IO.File.Exists(openFileDialog.FileName))
            {
                FileInfo fi = new FileInfo(openFileDialog.FileName);

                fi.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                  "\\Atlas Enterprise\\Atlas Suite\\Activation\\easy.key", true);

                ActivationStatus = Activation();

            }


        }

       public  string MacAddress { get; set; }
        public AtlasActivationControl()
        {
            DefaultStyleKey = typeof(AtlasActivationControl);
           // MacAddress = ShowNetworkInterfaces();

            ActivationStatus = Activation();
            //ActivationText = GetActivationText();
            ToolTip = "Click para Activar Atlas...";
            ActivateCommand = new DelegateCommand(ExecuteMethod);
           // Tag = ActivateCommand;
         //   MouseDoubleClick+=OnMouseDoubleClick;
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            ExecuteMethod();
        }

        private string GetActivationCode()
        {
            RC2Crypt rc = new RC2Crypt();
            MacAddress = ShowNetworkInterfaces();
            return rc.Encrypt(System.Convert.ToString(System.DateTime.Now) + " | - | " + MacAddress);

        }
        private string  ShowNetworkInterfaces()
        {
            string rslt = System.Environment.MachineName;
            IOrderedEnumerable<NetworkInterface> nics = NetworkInterface.GetAllNetworkInterfaces().OrderBy(x=>x.Name);
           
            if (nics == null || nics.Count() < 1)
            {

                return rslt;
            }

           

            foreach (NetworkInterface adapter in nics)
            {
                rslt = "";
                PhysicalAddress address = adapter.GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();
                for (int i = 0; i < bytes.Length; i++)
                { 
                   
                    // Display the physical address in hexadecimal.
                    rslt+= bytes[i].ToString("X2");
                    // Insert a hyphen after each byte, unless we are at the end of the  
                    // address. 
                    if (i != bytes.Length - 1)
                    {
                        rslt +="-";
                    }
                }

                if(MacAddress!=rslt)
                break;
              
            }


            return rslt;
        }


        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand ActivateCommand
        {
            get { return (ICommand)GetValue(ActivateCommandProperty); }
            set { SetValue(ActivateCommandProperty, value); }
        }

        private ActivationStatus Activation()
        {
            try
            {
                ActivationCode = GetActivationCode();

                int secs = -1;
                String fulltextdata = "";

                System.DateTime expdate = System.DateTime.Today;
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      "\\Atlas Enterprise\\Atlas Suite\\Activation"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                     "\\Atlas Enterprise\\Atlas Suite\\Activation");
                if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      "\\Atlas Enterprise\\Atlas Suite\\Activation\\easy.key"))
                {
                    Encripter enc = new Encripter();
                    int count = 0;
                    do
                    {
                       
                        fulltextdata = enc.DecryptFile2(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                      "\\Atlas Enterprise\\Atlas Suite\\Activation\\easy.key", MacAddress);
                        if (fulltextdata == "error")
                        {
                            MacAddress = ShowNetworkInterfaces();
                            count++;
                        }
                      
                    }
                    while (fulltextdata == "error" && NetworkInterface.GetAllNetworkInterfaces().Length > count);


                    String date = fulltextdata.Substring(fulltextdata.LastIndexOf(" (w) ") + 5, (fulltextdata.Length) - (fulltextdata.LastIndexOf(" (w) ") + 5));

                    expdate = System.Convert.ToDateTime(date,new CultureInfo("en-US"));

                    secs = System.DateTime.Compare(expdate, System.DateTime.Today);
                }
                ActivationText = "Periodo de Activacion No Disponible.";
                var rslt = ActivationStatus.Unaviable;
                if (fulltextdata.Contains("WildWest Forever"))
                {

                    ActivationText = Properties.Resources.ActivationNeverEnds;
                    rslt = ActivationStatus.Activated;
                }

                if (secs > 0)
                {
                    ActivationText = Properties.Resources.ActivationEndsOn + " " + MonthConverter(expdate.Month) + " "+expdate.Day.ToString()+", " + expdate.Year.ToString();//".\n Cuenta con "+System.Convert.ToString(secs)+" para Reactivar el Software.";

                    rslt = ActivationStatus.Activated;
                }
                else if (secs == 0)
                {
                    ActivationText = Properties.Resources.ActivationEndsToday;
                    rslt = ActivationStatus.Activated;
                }

                else if (fulltextdata != "")
                {

                    ActivationText = Properties.Resources.ActivationEnded;
                    rslt = ActivationStatus.NotActivated;
                }


                IsActivated = rslt == ActivationStatus.Activated;

               return rslt;
               // return ActivationStatus.Activated;
            }
            catch(Exception e)
            {
                ActivationText = "Periodo de Activacion No Disponible.";
               // MessageBox.Show(e.Message);
                
                return ActivationStatus.Unaviable;
                
            }
          
        }

        private string GetActivationText()
        {
            int secs = -1;
            String fulltextdata = "";

            System.DateTime expdate = System.DateTime.Today;
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                  "\\Atlas Enterprise\\Atlas Suite\\Activation"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                 "\\Atlas Enterprise\\Atlas Suite\\Activation");
            if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                  "\\Atlas Enterprise\\Atlas Suite\\Activation\\easy.key"))
            {
                Encripter enc = new Encripter();

                fulltextdata = enc.DecryptFile2(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                                  "\\Atlas Enterprise\\Atlas Suite\\Activation\\easy.key", Text);

                String date = fulltextdata.Substring(fulltextdata.LastIndexOf(" (w) ") + 5, (fulltextdata.Length) - (fulltextdata.LastIndexOf(" (w) ") + 5));

                expdate = System.Convert.ToDateTime(date);

                secs = System.DateTime.Compare(expdate, System.DateTime.Today);
            }

            if (fulltextdata.Contains("WildWest Forever"))
            {

               
                return "Su periodo de Activacion no termina.";
            }

            if (secs > 0)
            {
              

                return "Su periodo de Activacion culmina el dia " + expdate.Day.ToString() + " de " + MonthConverter(expdate.Month) + " del " + expdate.Year.ToString();//".\n Cuenta con "+System.Convert.ToString(secs)+" para Reactivar el Software.";
            }
            else if (secs == 0)
            {
                
                return "Su periodo de Activacion termina hoy.";
            }

            else if (fulltextdata != "")
            {

               
                return "Su periodo de Activacion ha terminado.";
            }

            return "Periodo de Activacion No Disponible.";
        }

        private String MonthConverter(int month)
        {
            if (month == 1)
            {
                return "Enero";
            }
            if (month == 2)
            {
                return "Febrero";
            }
            if (month == 3)
            {
                return "Marzo";
            }
            if (month == 4)
            {
                return "Abril";
            }
            if (month == 5)
            {
                return "Mayo";
            }
            if (month == 6)
            {
                return "Junio";
            }
            if (month == 7)
            {
                return "Julio";
            }
            if (month == 8)
            {
                return "Agosto";
            }
            if (month == 9)
            {
                return "Septiembre";
            }
            if (month == 10)
            {
                return "Octubre";
            }
            if (month == 11)
            {
                return "Noviembre";
            }

            return "Diciembre";

        }


        /// <summary>
        /// Gets of sets whether the root navigation bar is collapsed or not.
        /// </summary>
        public bool IsActivated
        {
            get { return (bool)GetValue(IsActivatedProperty);  }
            set { SetValue(IsActivatedProperty,value); }
        }
        /// <summary>
        /// Gets of sets whether the root navigation bar is collapsed or not.
        /// </summary>
        public ActivationStatus ActivationStatus
        {
            get { return (ActivationStatus) GetValue(ActivationStatusProperty); }
            set { SetValue(ActivationStatusProperty, value); }
        }

        /// <summary>
        /// Gets of sets whether the root navigation bar is collapsed or not.
        /// </summary>
        public string ActivationText
        {
            get { return (string)GetValue(ActivationTextProperty); }
            set { SetValue(ActivationTextProperty, value); }
        }

        public string ActivationCode
        {
            get { return (string)GetValue(ActivationCodeProperty); }
            set { SetValue(ActivationCodeProperty, value); }
        }

    }
}