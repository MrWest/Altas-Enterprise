using System;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace CompanyName.Atlas.Investments.Infrastructure
{
    public abstract class DBControlBase
    {
        protected DbConnection _connection;
        protected DbCommand _command;
        protected DbDataAdapter _adapter;
        //private System.String fileName;
        public String ConnectionString { get; set; }

      

        public DBControlBase()
        {

            //this.odbcConnection = new System.Data.Odbc.OdbcConnection();
            //this.odbcCommand = new System.Data.Odbc.OdbcCommand();

            ////Conex = "Data Source=ENRIKE\\SQLEXPRESS;Initial Catalog=MLB;Integrated Security=False; User Id=killer; Password=killer";


            ////Conex = LoadOnInit();

            //this.odbcConnection.ConnectionString = Conex;


            //this.odbcCommand.Connection = this.odbcConnection;
            //odbcCommand.Parameters.Clear();
            //odbcCommand.CommandType = System.Data.CommandType.Text;

            //odbcConnection.Close();
            // alow=true;
        }

        public virtual void Load()
        {
            //this.odbcConnection = new System.Data.Odbc.OdbcConnection();
            //this.odbcCommand = new System.Data.Odbc.OdbcCommand();

            //Conex = "Data Source=ENRIKE\\SQLEXPRESS;Initial Catalog=MLB;Integrated Security=False; User Id=killer; Password=killer";


            //Conex = LoadOnInit();

            _connection.ConnectionString = ConnectionString;


            _command.Connection = _connection;
            _command.Parameters.Clear();
            _command.CommandType = System.Data.CommandType.Text;

            _connection.Close();
        }

        public void Load(String connectionString)
        {
            this.ConnectionString = connectionString;

           Load();
        }

        public System.Data.DataSet SelectQuerryFixed(String sel)
        {
            
            System.Data.DataSet dts = new System.Data.DataSet();
            try
            {

                _command.Parameters.Clear();

                _command.CommandText = sel;

                _connection.Open();

                _command.Prepare();




                _adapter.SelectCommand = _command;
                _adapter.Fill(dts);

                _connection.Close();

            }
            catch (System.Exception ex)
            {
                throw new Exception("Atlas " + ex.Message);
            }
            return dts;
        }
        public bool ExistQuerry(System.String sel)
        {
            //System.Data.Odbc.OdbcDataAdapter adapter = new System.Data.Odbc.OdbcDataAdapter();
            System.Data.DataSet dts = new System.Data.DataSet();
            try
            {
                _command.Parameters.Clear();

                _command.CommandText = sel;

                _command.Prepare();

                _connection.Open();




                _adapter.SelectCommand = _command;
                _adapter.Fill(dts);

                _connection.Close();

                if (dts.Tables[0].Rows.Count > 0)
                {
                    return true;
                }

            }
            catch (System.Exception ex)
            {
                throw new Exception("Atlas " + ex.Message);
            }

            return false;
        }


        public void SimplePlan(System.String querry)
        {
            try
            {
                //System.Data.Odbc.OdbcDataAdapter adapter = new System.Data.Odbc.OdbcDataAdapter();

                _command.Parameters.Clear();

                _command.CommandText = "SELECT * FROM Clase";
                _adapter.SelectCommand = _command;

                _command.CommandText = querry;

                _command.Prepare();

                _connection.Open();

                //sqlCommand1.ExecuteReader();

                System.Data.Common.DataTableMapping tableMapping = new System.Data.Common.DataTableMapping();
                tableMapping.SourceTable = "Table";
                tableMapping.DataSetTable = "myTable";


                _adapter.UpdateCommand = _command;
                _adapter.UpdateCommand.Connection = _connection;
                _adapter.UpdateCommand.CommandType = System.Data.CommandType.Text;

                System.Data.DataSet dts = new System.Data.DataSet();

                _adapter.InsertCommand = _command;


                _adapter.Fill(dts);

                _adapter.TableMappings.Add(tableMapping);

                //int result = adapter.Update(dts.Tables[0]);

                _connection.Close();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Atlas " + ex.Message);
            }


        }
        public DbConnection GetConnection()
        {
            return _connection;
        }
        public void CloseConnection()
        {
            _connection.Close();
        }
        public System.String MaxQuerry(System.String table)
        {

            try
            {
                _command.Parameters.Clear();

                _command.CommandText = "SELECT MAX(Id) FROM [MLB].[dbo].[" + table + "]";

                _command.Prepare();

                _connection.Open();


                //System.Data.Odbc.OdbcDataAdapter adapter = new System.Data.Odbc.OdbcDataAdapter();
                System.Data.DataSet dts = new System.Data.DataSet();

                _adapter.SelectCommand = _command;
                _adapter.Fill(dts);
                System.String number = dts.Tables[0].Rows[0].ItemArray[0].ToString();

                _connection.Close();

                if (number == "")
                {
                    int n = 1;
                    return n.ToString();
                }
                int num = System.Convert.ToInt32(number) + 1;

                return num.ToString();
            }
            catch (System.Exception ex)
            {
                throw new Exception("Atlas " + ex.Message);
            }

            return "0";


        }



    }
    public class DBControlOdbc : DBControlBase
    {
       
      
        private string _dataUri;

        public String DataUri
        {
            get { return _dataUri; }
            set
            {
                _dataUri = value;

                ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ="+_dataUri;
            }
        }


        public DBControlOdbc(): base()
        {
            _connection = new System.Data.Odbc.OdbcConnection();
            _command = new System.Data.Odbc.OdbcCommand();
            _adapter = new OdbcDataAdapter();
            //this.odbcConnection = new System.Data.Odbc.OdbcConnection();
            //this.odbcCommand = new System.Data.Odbc.OdbcCommand();

            ////Conex = "Data Source=ENRIKE\\SQLEXPRESS;Initial Catalog=MLB;Integrated Security=False; User Id=killer; Password=killer";


            ////Conex = LoadOnInit();

            //this.odbcConnection.ConnectionString = Conex;


            //this.odbcCommand.Connection = this.odbcConnection;
            //odbcCommand.Parameters.Clear();
            //odbcCommand.CommandType = System.Data.CommandType.Text;

            //odbcConnection.Close();
            // alow=true;
        }

    
       

    }


    public class DBControlSQL : DBControlBase
    {


       


        public DBControlSQL() : base()
        {
            _connection = new SqlConnection();
            _command = new SqlCommand();
            _adapter = new SqlDataAdapter();
            //this.odbcConnection = new System.Data.Odbc.OdbcConnection();
            //this.odbcCommand = new System.Data.Odbc.OdbcCommand();

            ////Conex = "Data Source=ENRIKE\\SQLEXPRESS;Initial Catalog=MLB;Integrated Security=False; User Id=killer; Password=killer";


            ////Conex = LoadOnInit();

            //this.odbcConnection.ConnectionString = Conex;


            //this.odbcCommand.Connection = this.odbcConnection;
            //odbcCommand.Parameters.Clear();
            //odbcCommand.CommandType = System.Data.CommandType.Text;

            //odbcConnection.Close();
            // alow=true;
        }




    }
}
