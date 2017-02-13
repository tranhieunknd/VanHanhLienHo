using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Cwrs.Lib
{
    public class DataAccess
    {
        private SqlCommand cmd = new SqlCommand();
        public DataAccess(string connectionString)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            cmd.Connection = conn;
            cmd.CommandTimeout = 0;
        }
        private void Open()
        {
            cmd.Connection.Open();
        }
        private void Close()
        {
            cmd.Connection.Close();
        }
        public void Dispose()
        {
            cmd.Dispose();
        }
        #region ExcuteReader
        public SqlDataReader ExecuteReader()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = null;
            try
            {
                this.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // CommandBehavior.CloseConnection : gui CommanText vao Connection va tao ra doi tuong SqlDatareader su dung mot gia tri cua CommandBehavior 

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return reader;
        }
        public SqlDataReader ExecuteReader(string commandText)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = null;
            try
            {
                cmd.CommandText = commandText;
                reader = this.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return reader;
        }
        #endregion
        #region ExcuteScalar
        public object ExecuteScalar()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            object obj = null;
            try
            {
                this.Open();
                obj = cmd.ExecuteScalar();
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obj;
        }
        public object ExecuteScalar(string commandText)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            object obj = null;
            try
            {
                cmd.CommandText = commandText;
                obj = this.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obj;
        }
        #endregion
        #region ExcuteNonQuery
        public int ExcuteNonQuery()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            int i = -1;
            try
            {
                this.Open();
                i = cmd.ExecuteNonQuery();
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return i;
        }
        public int ExcuteNonQuery(string commandText)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            int i = -1;
            try
            {
                cmd.CommandText = commandText;
                i = this.ExcuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return i;
        }
        #endregion
        #region ExcuteDataSet
        public DataSet ExecuteDataset()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                da = new SqlDataAdapter();
                da.SelectCommand = (SqlCommand)cmd;
                ds = new DataSet();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }
        public DataTable ReadToDataTable(string commandText)
        {
            this.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            DataSet ds = null;
            try
            {
                cmd.CommandText = commandText;
                ds = this.ExecuteDataset();
                this.Close();
            }
            catch (Exception ex)
            {
                this.Close();
                throw new Exception(ex.Message);
            }
            return ds.Tables[0];
        }
        public DataTable GetDataTableByQuery(string _queryClause)
        {
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                this.Open();
                cmd.CommandText = _queryClause;
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                dt = ds.Tables[0];
                this.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        #endregion
        #region AddParam
        public SqlParameterCollection Param
        {
            get
            {
                return cmd.Parameters;
            }
        }
        public void AddParam(string ParamName, object ParamValue)
        {
            SqlParameter param = new SqlParameter(ParamName, ParamValue);
            cmd.Parameters.Add(param);
        }
        public void AddParam(SqlParameter param)
        {
            cmd.Parameters.Add(param);
        }
        public void ClearParam()
        {
            cmd.Parameters.Clear();
        }
        #endregion        
    }
}