using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace URLShortener.Models
{
    public static class Repository
    {          
        public static string GetURL(int id, string connectionString)
        {
            string url = null;
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetURL";
                    command.Parameters.Add(new SqlParameter("@ID", id));

                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        url = reader.GetString(reader.GetOrdinal("URL"));
                    }
                    connection.Close();
                }
            }
            return url;
        }
        public static int SetURL(string url,string connectionString)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SetURL";
                    command.Parameters.Add(new SqlParameter("@url", url));

                    connection.Open();
                    var reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        id = reader.GetInt32(reader.GetOrdinal("ID"));
                    }
                    connection.Close();
                }
            }
            return id;
        }
    }
}
