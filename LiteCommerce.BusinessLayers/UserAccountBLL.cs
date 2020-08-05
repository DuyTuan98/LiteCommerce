using Litecommerce.DomainModels;
using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng tác nghiệp liên quan đến tài khoản
    /// </summary>
    public static class UserAccountBLL
    {
        private static string _connectionString;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }
        public static UserAccount Authorize(string userName, string passWord, UserAccountTypes userTypes)
        {
            IUserAccountDAL userAccountDB;
            switch (userTypes)
            {
                case UserAccountTypes.Employee:
                    userAccountDB = new EmployeeUserAccountDAL(_connectionString);
                    break;

                case UserAccountTypes.Customer:
                    userAccountDB = new CustomerUserAccountDAL(_connectionString);
                    break;
                default:
                    return null;
            }
            return userAccountDB.Authorize(userName, passWord);
        }
        public static int UpdateProfile(Object data, UserAccountTypes userType)
        {
            IUserAccountDAL userAccountDAL = null;
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    userAccountDAL = new LiteCommerce.DataLayers.SqlServer.EmployeeUserAccountDAL(_connectionString);
                    break;
                case UserAccountTypes.Customer:
                    userAccountDAL = new LiteCommerce.DataLayers.SqlServer.CustomerUserAccountDAL(_connectionString);
                    break;
                default:
                    throw new Exception("invalid type");
            }
            return userAccountDAL.UpdateProfile(data);
        }

        public static int ChangePassword(string accountId, string newPAssword, UserAccountTypes userType)
        {
            IUserAccountDAL userAccountDAL = null;
            switch (userType)
            {
                case UserAccountTypes.Employee:
                    userAccountDAL = new LiteCommerce.DataLayers.SqlServer.EmployeeUserAccountDAL(_connectionString);
                    break;
                case UserAccountTypes.Customer:
                    userAccountDAL = new LiteCommerce.DataLayers.SqlServer.CustomerUserAccountDAL(_connectionString);
                    break;
                default:
                    throw new Exception("invalid type");
            }
            return userAccountDAL.ChangePassword(accountId, newPAssword);
        }

    }
}
