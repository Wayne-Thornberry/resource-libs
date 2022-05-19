using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using Proline.DBAccess.Data;

namespace Proline.DBAccess.MidLayer
{
    public partial class DBAccessApi
    {

        public PlacePlayerDataResponse SaveFile(PlacePlayerDataRequest inParameters)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.ConnectionString = "Server=DESKTOP-RODUPL7;Database=OnlineGame;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                connection.Open();


                using (SqlCommand command = new SqlCommand("dbo.InsertSaveFile", connection) { CommandType = CommandType.StoredProcedure })
                {
                    var parm1 = new SqlParameter("@data", SqlDbType.NVarChar);
                    parm1.Value = inParameters.Data;

                    command.Parameters.Add(parm1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}", reader.GetInt64(reader.GetOrdinal("Id")));
                        }
                    }
                }

             
            }

            return null;

        }

        public void LoadFile(int id)
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.ConnectionString = "Server=DESKTOP-RODUPL7;Database=OnlineGame;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("\nQuery data example:");
                Console.WriteLine("=========================================\n");

                connection.Open();


                using (SqlCommand command = new SqlCommand("dbo.InsertSaveFile", connection) { CommandType = CommandType.StoredProcedure })
                { 
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                        }
                    }
                }  
            }

        }
    }
}
