using Proline.DBAccess.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Proline.DBAccess.DBApi
{
    public class InsertSaveFileAPI : DBApi
    {
        public InsertSaveResponse response;
        public InsertSaveFileAPI() : base("dbo.InsertSaveFile")
        {
        }

        public static InsertSaveFileAPI Execute(InsertSaveRequest inParameter)
        {
            var api = new InsertSaveFileAPI();

            api.AddInputParameter("@id", SqlDbType.NVarChar, inParameter.Identity);
            api.AddInputParameter("@data", SqlDbType.NVarChar, inParameter.Data); 
            api.AddInputParameter("@playerId", SqlDbType.BigInt, inParameter.PlayerId);

            api.ExecuteReader();

            return api;
        }

        public override void OnSqlReader(SqlDataReader reader)
        {
            response = new InsertSaveResponse();
            while (reader.Read())
            {
                response.Identity = reader.GetString(reader.GetOrdinal("Identity"));
                response.SaveId = reader.GetInt64(reader.GetOrdinal("SaveId"));
            }
        }
    }
}
