using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security
{
    /// <summary>
    /// Manages Security options for Atlas System
    /// </summary>
    public class AtlasSecurity
    {

        private IAtlasUserPresenter _atlasUser;

        public IAtlasUserPresenter CurrentUser
        {
            get { return _atlasUser; }
            set { _atlasUser = value; }
        }

    }

    /// <summary>
    /// Manages Security options for Atlas System
    /// </summary>
    public class AtlasDataAccess
    {

        private  string _connection = GetConnectionString();

        public string ConnectionString
        {
            get { return _connection; }
            set { _connection = value; }
        }

        //private DbConnection _dBConnection = GetDbConnection();

        //private static DbConnection GetDbConnection()
        //{
        //    var crap = new SecureString();
        //    crap.AppendChar('k');
        //    crap.AppendChar('i');
        //    crap.AppendChar('d');
        //    crap.AppendChar('w');
        //    crap.AppendChar('e');
        //    crap.AppendChar('s');
        //    crap.AppendChar('t');
        //    crap.AppendChar('1');
        //    crap.AppendChar('2');
        //    crap.AppendChar('!');

        //    if (!crap.IsReadOnly())
        //        crap.MakeReadOnly();
        //    return  new SqlConnection() {ConnectionString = GetConnectionString(), Credential = new SqlCredential("sa", crap)};
        //}

        //public DbConnection DbConnection
        //{
        //    get { return _dBConnection; }
        //    set { _dBConnection = value; }
        //}
        private static string GetConnectionString()
        {

             string cnx = "AtlasDb";
            try
            {
                StreamReader sr = File.OpenText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) + "\\fileAddress.cfg");
                sr.BaseStream.Position = 0;
                //bool can=sr.BaseStream.CanRead;
                if (sr.ReadToEnd() != "")
                {
                    sr.BaseStream.Position = 0;
                    cnx = sr.ReadToEnd();

                }
                sr.Close();
            }
            catch (System.Exception ex)
            {

                cnx = "AtlasDb";

            }




            return cnx;
        }

        

    }
}
