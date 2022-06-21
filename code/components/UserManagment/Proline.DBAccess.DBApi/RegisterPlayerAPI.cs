using Proline.DBAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Proline.DBAccess.DBApi
{
    public class RegisterPlayerAPI : DBApi
    {
        public RegisterPlayerResponse response;

        private RegisterPlayerAPI() : base ("dbo.RegisterPlayer")
        {

        }

        public static RegisterPlayerAPI Execute(RegisterPlayerRequest request)
        {
            RegisterPlayerAPI api = new RegisterPlayerAPI();

            api.AddInputParameter("@username",  System.Data.SqlDbType.NVarChar, request.Name);

            api.ExecuteReader();
            return api;
        }

        public override void OnSqlReader(SqlDataReader reader)
        {
            response = new RegisterPlayerResponse();
            while (reader.Read())
            {
                response.Id = reader.GetInt64(reader.GetOrdinal("Id"));
            }
        }
    }
}
