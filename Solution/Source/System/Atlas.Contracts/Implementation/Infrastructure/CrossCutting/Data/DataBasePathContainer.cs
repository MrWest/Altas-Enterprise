using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data
{
    public class DataBasePathContainer
    {
        private string _dataBasePath;

        public string DataBasePath
        {
            get { return _dataBasePath; }
            set { _dataBasePath = value; }
        }

        private string OldPath { get; set; }

        private IDatabaseContext _databaseContext;
        public IDatabaseContext DatabaseContext
        {
            get
            {
                //if (DataBasePath != OldPath)
                //{

                //    if (_databaseContext != null)
                //    {
                //        _databaseContext.Dispose();
                //        if (_databaseContext.GetType().Implements<IDb4ODatabaseContext>())
                //        {
                           
                //            ((IDb4ODatabaseContext) _databaseContext).Close();
                //           // ((IDb4ODatabaseContext)_databaseContext).Ext();
                //        }
                            
                //    }
                   
                //    _databaseContext = new Db4ODatabaseContext(DataBasePath);
                //    OldPath = DataBasePath;
                //}

                return _databaseContext;

            }
            set { _databaseContext = value; }
        }

        public DataBasePathContainer()
        {


            // DataBasePath = (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.adb"));
            if (!Directory.Exists(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o"))
            Directory.CreateDirectory(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o");

            DataBasePath = (Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o", "db.adb"));
            OldPath = DataBasePath;
           // _databaseContext = new Db4ODatabaseContext();
            if(ServiceLocator.IsLocationProviderSet)
            _databaseContext = ServiceLocator.Current.GetInstance<IDb4ODatabaseContext>(DataBasePath);
        
           
        }
    }
}
