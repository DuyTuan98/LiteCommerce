using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Litecommerce.DomainModels;
using System.Data.SqlClient;
using System.Data;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CountryDAL : ICountryDAL
    {
        private string connectionString;

        public CountryDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Country> List()
        {
            List<Country> data = new List<Country>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //tao lenh thuc thi truy van du lieu
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Countries";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                // thuc thi truy van tra ve bien dbReader
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Country()
                        {
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
