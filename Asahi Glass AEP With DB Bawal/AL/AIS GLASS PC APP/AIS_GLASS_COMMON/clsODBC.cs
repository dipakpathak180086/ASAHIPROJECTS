using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Data.Odbc;

namespace AIS_GLASS_COMMON
{
    public class clsODBC
    {
        OdbcConnection m_Con;
        OdbcTransaction sqlTrans;
        OdbcCommand com;
        public static string strLicCountry = "";
        public static string strLicCompany = "";
        public static string strLicStore = "";
        public static string strLicMacId = "";
        //  string m_DataSource = "";

        #region PUBLIC PROPERTY

        private string m_DNS;

        public string DNS
        {
            get { return m_DNS; }
            set { m_DNS = value; }
        }
        private string _DatabaseName;

        public string DatabaseName
        {
            get { return _DatabaseName; }
            set { _DatabaseName = value; }
        }
        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        private string _dataSource;

        public string DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }


        private bool m_IsConnected;
        public bool IsConnected
        {
            get { return m_IsConnected; }
            set { m_IsConnected = value; }

        }
        #endregion


        public clsODBC()
        {

        }

        /// <summary>
        /// Connect to Database
        /// </summary>
        public bool Connect()
        {
            try
            {
                m_IsConnected = false;
                m_Con = new OdbcConnection();
                 m_Con.ConnectionString = "DSN=Excel Files;DBQ=" + DataSource + ";";//Uid=barcode;Pwd=barcode123;";
                m_Con.Open(); 
                 com = new OdbcCommand();
                com = m_Con.CreateCommand();
                m_IsConnected = true;
            }
            catch (Exception ex)
            {
                m_IsConnected = false;
                throw ex;
            }
            return m_IsConnected;
        }
        /// <summary>
        /// Begin Transection
        /// </summary>
        public void BeginTrans()
        {
            //sqlTrans = new SqlTransaction();
            try
            {
                sqlTrans = m_Con.BeginTransaction(IsolationLevel.ReadCommitted);
                com.Transaction = sqlTrans;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Commit Transection
        /// </summary>
        public void CommitTrans()
        {
            try
            {
                sqlTrans.Commit();
                sqlTrans.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Rollback trasection
        /// </summary>
        public void RollBack()
        {
            try
            {
                sqlTrans.Rollback();
                sqlTrans.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Disconnect database connection
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (m_Con != null && m_Con.State == ConnectionState.Open)
                {
                    com.Dispose();
                    m_Con.Close();
                    m_IsConnected = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Fetch data from data set 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataSet GetSQL(string strSql)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            OdbcDataAdapter da;
            try
            {
                if (m_Con.State == ConnectionState.Open)
                {
                    //ds = new DataSet();

                    da = new OdbcDataAdapter(strSql, m_Con);
                    int x = da.Fill(ds);
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataTable GetDataSetInTransaction(string strSql)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            OdbcDataAdapter da;
            try
            {
                if (m_Con.State == ConnectionState.Open)
                {
                    ds = new DataSet();
                    da = new OdbcDataAdapter(strSql, m_Con);
                    da.SelectCommand.Transaction = sqlTrans;
                    da.Fill(dt);
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public int executeQuery(string StrSql)
        {

            int result = 0;
            try
            {
                if (m_Con.State == ConnectionState.Open)
                {
                    com.CommandText = StrSql;
                    result = com.ExecuteNonQuery();

                    com.CommandText = "commit";
                    result = com.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public void ExecuteCommand(OdbcCommand cmd)
        {
            try
            {
                if (m_Con.State == ConnectionState.Open)
                {
                    cmd.Connection = m_Con;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Fetch data from data set
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string strSql)
        {
            DataSet ds = new DataSet();
            OdbcDataAdapter da;
            try
            {
                if (m_Con.State == ConnectionState.Closed)
                    Connect();
                ds = new DataSet();
                da = new OdbcDataAdapter(strSql, m_Con);
                da.Fill(ds);
                da.Dispose();

            }
            catch (Exception ex)
            { throw ex; }
            return ds;
        }

        public DataTable GetDataTable(string strSql)
        {
            DataTable dt = new DataTable();
            OdbcDataAdapter da;
            try
            {
                if (m_Con.State == ConnectionState.Open)
                {
                    dt = new DataTable();
                    da = new OdbcDataAdapter(strSql, m_Con);
                    da.Fill(dt);
                    da.Dispose();
                }

            }
            catch (Exception ex)
            { throw ex; }
            return dt;
        }

    }
}
