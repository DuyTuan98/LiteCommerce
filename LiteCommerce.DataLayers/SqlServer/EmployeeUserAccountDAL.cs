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

    public class EmployeeUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;
        public EmployeeUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string passWord)
        {
            UserAccount data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT EmployeeId, LastName, FirstName, Title, PhotoPath, Roles
                                    FROM Employees WHERE Email = @email AND Password = @password";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@email", userName);
                cmd.Parameters.AddWithValue("@passWord", passWord);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserID = dbReader["EmployeeId"].ToString(),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Title = dbReader["Title"].ToString(),
                            Photo = dbReader["PhotoPath"].ToString(),
                            Roles = dbReader["Roles"].ToString()
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
        //TODO: kiểm tra thông tin tài khoản từ bằng Employee
        //    return new UserAccount()
        //    {
        //        UserID = userName,
        //        FullName = "Nguyễn Thị Hiếu Giang",
        //        Photo = "637274190476205208.jpg"
        //    };
        //}
        public int ChangePassword(string accountId, string newPassword)
        {
            int dem;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"UPDATE Employees SET Password=@Password WHERE(EmployeeID = @EmployeeID)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(accountId));
                    cmd.Parameters.AddWithValue("@Password", newPassword);

                    dem = Convert.ToInt32(cmd.ExecuteScalar());
                }
                connection.Close();
            }
            return dem;
        }

        public int UpdateProfile(Object data)
        {
            Employee empl = (Employee)data;

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees
                    SET 
                        LastName=@LastName,
	                    FirstName=@FirstName,
	                    Title=@Title,
	                    BirthDate=@BirthDate,
	                    Address=@Address,
	                    City=@City,
	                    Country=@Country,
	                    HomePhone=@HomePhone,
	                    PhotoPath=@PhotoPath
                    WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@LastName", empl.LastName);
                cmd.Parameters.AddWithValue("@FirstName", empl.FirstName);
                cmd.Parameters.AddWithValue("@Title", empl.Title);
                cmd.Parameters.AddWithValue("@BirthDate", empl.BirthDate);
                cmd.Parameters.AddWithValue("@Address", empl.Address);
                cmd.Parameters.AddWithValue("@City", empl.City);
                cmd.Parameters.AddWithValue("@Country", empl.Country);
                cmd.Parameters.AddWithValue("@HomePhone", empl.HomePhone);
                cmd.Parameters.AddWithValue("@PhotoPath", empl.PhotoPath);
                cmd.Parameters.AddWithValue("@EmployeeID", empl.EmployeeID);

                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());

                connection.Close();
            }

            return rowsAffected; ;
        }
    }
}
