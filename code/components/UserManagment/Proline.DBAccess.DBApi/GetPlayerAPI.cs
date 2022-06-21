using Proline.DBAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proline.DBAccess.DBApi
{
    public class GetPlayerAPI : DBApi
    {
        public GetPlayerResponse response;

        private GetPlayerAPI() : base("dbo.GetPlayer")
        {
        }

        public static GetPlayerAPI Execute(GetPlayerRequest request)
        {
            var api = new GetPlayerAPI();

            api.AddInputParameter("@username", System.Data.SqlDbType.NVarChar, request.Username);

            api.ExecuteReader();

            return api;
        }

        public override void OnSqlReader(SqlDataReader reader)
        {
            response = new GetPlayerResponse();
            while (reader.Read())
            {
                response.PlayerId = reader.GetInt64(reader.GetOrdinal("Id"));
            }
        }
    }
}
