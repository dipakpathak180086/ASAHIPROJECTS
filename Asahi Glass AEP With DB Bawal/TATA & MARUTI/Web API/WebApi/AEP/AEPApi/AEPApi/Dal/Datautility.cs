using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public class Datautility : IDisposable
{
    #region PrivateProperties&Methods

    public string MODE
    {
        get;
        set;
    }



    public SqlConnection _Con;
    public SqlCommand _Com;
    public SqlTransaction _Transaction;
 
    public void _OpenConnection()
    {
        if (_Con == null)
        {
            _Con = new SqlConnection((ConfigurationManager.ConnectionStrings["cnn"].ConnectionString));

            if (_Com == null)
            {
                _Com = new SqlCommand();
            }
            _Com.Parameters.Clear();
            _Com.Connection = _Con;
        }
        if (_Con.State == ConnectionState.Closed)
        {
            _Con.Open();
            // CommonHelper.mSatoLogger.LogMessage(SatoLib.EventNotice.EventTypes.evtData, "DNH" + "  ::  Connection Open ",System.DateTime.Now.ToString());
        }
    }
    public void _CloseConnection()
    {
        if (_Con != null)
        {
            if (_Con.State == ConnectionState.Open)
            {
                _Con.Close();
                // CommonHelper.mSatoLogger.LogMessage(SatoLib.EventNotice.EventTypes.evtData, "DNH" + "  ::  Connection Close ", System.DateTime.Now.ToString());
            }
        }
    }
    public void Dispose()
    {
        if (_Con != null)
        {
            if (_Con.State == ConnectionState.Closed)
            {
                _Con.Dispose();
                _Con = null;
                // CommonHelper.mSatoLogger.LogMessage(SatoLib.EventNotice.EventTypes.evtData, "DNH" + "  ::  Connection Dispose ", System.DateTime.Now.ToString());
            }
        }
    }

    #endregion PrivateProperties&Methods

    #region   Not In Transaction

    public object ExecuteScalar(string Squery)
    {
        object Objresult;
        _CloseConnection();
        Dispose();
        _OpenConnection();

        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        Objresult = _Com.ExecuteScalar();

        _CloseConnection();
        Dispose();

        return Objresult;
    }
    public object ExecuteScalar(string SqlProc, SqlParameter[] Arrp)
    {
        object Objresult;
        _CloseConnection();
        Dispose();
        _OpenConnection();
        _Com.CommandText = SqlProc;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }

        Objresult = _Com.ExecuteScalar();
        _Com.Parameters.Clear();
        _CloseConnection();
        Dispose();

        return Objresult;
    }
    public bool isexist(string strsql)
    {
        _OpenConnection();
        _Com.CommandType = CommandType.Text;
        _Com.CommandTimeout = 0;
        _Com.CommandText = strsql;
        int result = (int)_Com.ExecuteScalar();
        _CloseConnection();
        Dispose();

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    public int ExecuteSQL(string Squery)
    {
        int result = 0;
        _CloseConnection();
        Dispose();
        _OpenConnection();

        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        result = _Com.ExecuteNonQuery();

        _CloseConnection();
        Dispose();

        return result;
    }

    public int ExecuteProcedure(string ProcName, SqlParameter[] Arrp)
    {
        int result = 0;
        _CloseConnection();
        Dispose();
        _OpenConnection();
        _Com.CommandType = CommandType.StoredProcedure;
        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;

        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }
        result = _Com.ExecuteNonQuery();
        _Com.Parameters.Clear();
        _CloseConnection();
        Dispose();

        return result;
    }
    public int ExecuteProcedure(string ProcName)
    {
        int result = 0;
        _CloseConnection();
        Dispose();
        _OpenConnection();
        _Com.CommandType = CommandType.StoredProcedure;
        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;

        result = _Com.ExecuteNonQuery();

        _CloseConnection();
        Dispose();

        return result;
    }

    public DataTable GetDataTable(string Squery)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();
        DataTable DT = new DataTable();


        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DT);

        _CloseConnection();
        Dispose();

        return DT;
    }
    public DataSet GetDataSet(string Squery)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();
        DataSet DS = new DataSet();


        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DS);

        _CloseConnection();
        Dispose();

        return DS;
    }
    public DataSet GetDataSet(string ProcName, SqlParameter[] Arrp)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();
        DataSet DS = new DataSet();

        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;

        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DS);
        _Com.Parameters.Clear();
        _CloseConnection();
        Dispose();

        return DS;
    }
    public DataTable GetDataUsingProcedure(string ProcName)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();

        DataTable DT = new DataTable();


        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DT);
        _CloseConnection();
        Dispose();

        return DT;
    }
    public DataTable GetDataUsingProcedure(string ProcName, SqlParameter[] Arrp)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();

        DataTable DT = new DataTable();


        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;

        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }

        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DT);
        _Com.Parameters.Clear();
        _CloseConnection();
        Dispose();

        return DT;
    }

    public DataSet GetDataSetUsingProcedure(string ProcName, SqlParameter[] Arrp)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();

        DataSet DS = new DataSet();


        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;

        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }

        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DS);
        _Com.Parameters.Clear();
        _CloseConnection();
        Dispose();

        return DS;
    }
    public DataSet GetDataSetUsingProcedure(string ProcName)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();

        DataSet DS = new DataSet();


        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DS);
        _CloseConnection();
        Dispose();

        return DS;
    }

    public SqlDataReader GetDataReader(string Squery)
    {
        _CloseConnection();
        Dispose();
        _OpenConnection();

        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        SqlDataReader dr = _Com.ExecuteReader(CommandBehavior.CloseConnection);

        _CloseConnection();
        Dispose();

        return dr;
    }

    #endregion End Not In Transaction

    #region Start with Transaction

    public void TranStartTransaction()
    {
        _OpenConnection();
        _Transaction = _Con.BeginTransaction();
    }
    public void TranCommitTransaction()
    {
        if (_Transaction != null)
        {
            _Transaction.Commit();
            _Transaction.Dispose();
        }
        _CloseConnection();
        Dispose();
    }
    public void TranRollBackTransaction()
    {
        try
        {
            if (_Transaction != null)
            {
                _Transaction.Rollback();
                _Transaction.Dispose();
            }
            _CloseConnection();
            Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void TranStopTransaction()
    {
        _CloseConnection();
        Dispose();
        _Transaction = null;
    }

    #region Transaction Methods

    public object TranExecuteScalar(string Squery)
    {
        object Objresult;
        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        _Com.Transaction = _Transaction;
        Objresult = _Com.ExecuteScalar();

        return Objresult;
    }
    public int TranExecuteSQL(string Squery)
    {
        int result = 0;

        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        _Com.Transaction = _Transaction;
        result = _Com.ExecuteNonQuery();

        return result;
    }
    public bool Tranisexist(string strsql)
    {
        _OpenConnection();
        _Com.CommandType = CommandType.Text;
        _Com.CommandTimeout = 0;
        _Com.Transaction = _Transaction;
        _Com.CommandText = strsql;
        int result = (int)_Com.ExecuteScalar();
        _CloseConnection();
        Dispose();

        if (result > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int TranExecuteProcedure(string ProcName, SqlParameter[] Arrp, SqlTransaction tran)
    {
        int result = 0;

        _Com.CommandType = CommandType.StoredProcedure;
        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.Transaction = tran;


        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }
        result = _Com.ExecuteNonQuery();
        _Com.Parameters.Clear();
        return result;
    }
    public int TranExecuteProcedure(string ProcName, SqlParameter[] Arrp)
    {
        int result = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.Transaction = _Transaction;

        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }
        result = _Com.ExecuteNonQuery();
        _Com.Parameters.Clear();
        return result;
    }

    public DataTable TranGetDataTable(string Squery)
    {
        DataTable DT = new DataTable();

        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        _Com.Transaction = _Transaction;
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DT);

        return DT;
    }
    public DataSet TranGetDataSet(string Squery)
    {
        DataSet DS = new DataSet();


        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.Text;
        _Com.Transaction = _Transaction;
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DS);

        return DS;
    }
    public DataTable TranGetDataUsingProcedure(string ProcName)
    {

        DataTable DT = new DataTable();


        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        _Com.Transaction = _Transaction;
        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DT);

        return DT;
    }
    public DataTable TranGetDataUsingProcedure(string ProcName, SqlParameter[] Arrp)
    {

        DataTable DT = new DataTable();


        _Com.CommandText = ProcName;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        _Com.Transaction = _Transaction;

        if (Arrp != null)
        {
            _Com.Parameters.Clear();
            foreach (SqlParameter pr in Arrp)
            {
                _Com.Parameters.Add(pr);
            }
        }

        SqlDataAdapter da = new SqlDataAdapter(_Com);
        da.Fill(DT);
        _Com.Parameters.Clear();
        return DT;
    }
    public SqlDataReader TranGetDataReader(string Squery)
    {

        _Com.CommandText = Squery;
        _Com.CommandTimeout = 0;
        _Com.CommandType = CommandType.StoredProcedure;
        _Com.Transaction = _Transaction;
        SqlDataReader dr = _Com.ExecuteReader(CommandBehavior.CloseConnection);

        return dr;
    }


    #endregion End Transaction Methods



    #endregion End with Transaction

}

