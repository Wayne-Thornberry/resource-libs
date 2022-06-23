using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace Proline.DBAccess.DBApi
{
    public abstract class DBApi
    {
        private string _sproc;
        private int _apiReturnCode;
        private List<SqlParameter> _parameters;
        private string _conString;

        public int APIReturnCode => _apiReturnCode;

        public DBApi(string procName)
        {
            this._sproc = procName;
            var x = ConfigurationManager.AppSettings;
            var settings = ConfigurationManager.ConnectionStrings;
            _conString = settings["OnlineGameDB"].ConnectionString;     
            _parameters = new List<SqlParameter>();
        }

        private void Execute()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                ConnectionString = _conString
            };

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(_sproc, connection) { CommandType = CommandType.StoredProcedure })
                {  
                    command.Parameters.AddRange(_parameters.ToArray()); 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        OnSqlReader(reader);
                    } 
                    _apiReturnCode = int.Parse(_parameters[_parameters.Count - 1].Value.ToString());
                } 
            }

             
        }

        public void ExecuteReader()
        { 
            AddDefaultParameters();
            Execute();
        } 

        private void AddDefaultParameters()
        {
            AddParameter("@ReturnVal", SqlDbType.Int, ParameterDirection.ReturnValue);
        }

        public void AddInputParameter(string parameterName, SqlDbType type, object value)
        {
            AddParameter(parameterName, type, ParameterDirection.Input, value);
        }

        public void AddOutputParameter(string parameterName, SqlDbType type, object value)
        {
            AddParameter(parameterName, type, ParameterDirection.Output, value);
        }

        private void AddParameter(string parameterName, SqlDbType type, ParameterDirection direction, object value = null)
        {
            var param = new SqlParameter(parameterName, type);
            param.Direction = direction;
            param.Value = value;
            _parameters.Add(param);
        }

        public virtual void OnSqlReader(SqlDataReader reader) { }
    }
}
