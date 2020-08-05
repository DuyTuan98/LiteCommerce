using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Litecommerce.DomainModels;

namespace LiteCommerce.DataLayers.SqlServer
{
    public class CustomerUserAccountDAL : IUserAccountDAL
    {
        private string connectionString;
        public CustomerUserAccountDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public UserAccount Authorize(string userName, string password)
        {
            //TODO: kiểm tra thông tin đăng nhập dựa vào bảng Customer
            return new UserAccount()
            {
                UserID = userName,
                FullName = "Võ Duy Tuấn",
                Photo = "tuan.jpg"
            };
        }
        public int ChangePassword(string accountId, string newPassword)
        {
            return 0;
        }

        public int UpdateProfile(Object data)
        {
            return 0;
        }
    }
}
