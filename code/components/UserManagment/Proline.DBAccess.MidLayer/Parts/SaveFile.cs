using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Proline.DBAccess.Data;
using System.Configuration;
using Proline.DBAccess.DBApi;

namespace Proline.DBAccess.MidLayer
{
    public partial class DBAccessApi
    {

        public InsertSaveResponse SaveFile(InsertSaveRequest inParameters)
        {
            InsertSaveFileAPI api = InsertSaveFileAPI.Execute(inParameters);
            var response = api.response;
            return response;

        }

        public GetSaveResponse GetSave(GetSaveRequest inParameter)
        {
            var api =  GetSaveFileAPI.Execute(inParameter);
            GetSaveResponse response = api.response; 
            return response; 
        }
    }
}
