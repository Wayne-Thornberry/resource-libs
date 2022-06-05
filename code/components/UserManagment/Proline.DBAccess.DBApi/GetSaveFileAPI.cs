using Proline.DBAccess.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Proline.DBAccess.DBApi
{
    public class GetSaveFileAPI : DBApi
    {

        public GetSaveResponse response;

        private GetSaveFileAPI() : base("dbo.GetSaveFile")
        {
        }

        public static GetSaveFileAPI Execute(GetSaveRequest inParameter)
        {
            var api = new GetSaveFileAPI();
            api.AddInputParameter("@id", SqlDbType.BigInt, inParameter.Id);
            api.ExecuteReader();
            return api;
        }

        public override void OnSqlReader(SqlDataReader reader)
        {
            response = new GetSaveResponse();
            while (reader.Read())
            {
                response.Data = reader.GetString(reader.GetOrdinal("Value"));
            }
        }
    }
}
