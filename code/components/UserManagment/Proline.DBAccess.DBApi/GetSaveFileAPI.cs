using Proline.DBAccess.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            api.AddInputParameter("@identity", SqlDbType.NVarChar, inParameter.Identity);
            api.AddInputParameter("@username", SqlDbType.NVarChar, inParameter.Username);
            api.ExecuteReader();
            return api;
        }

        public override void OnSqlReader(SqlDataReader reader)
        {
            response = new GetSaveResponse();
            response.SaveFiles = new SaveFile[16];
            int i = 0;
            while (reader.Read())
            {
                var saveFile = new SaveFile
                {
                    Identity = reader.GetString(reader.GetOrdinal("Identity")),
                    Data = reader.GetString(reader.GetOrdinal("Value"))
                };
                response.SaveFiles[i] = saveFile;
                i++;
            }
            response.SaveFiles = response.SaveFiles.Where(e => e != null).ToArray();
        }
    }
}
