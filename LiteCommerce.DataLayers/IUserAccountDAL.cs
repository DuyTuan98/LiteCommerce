using Litecommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra username và password có hợp lệ hay không?
        /// nếu hợp lệ thì trả về thông tin của user
        /// ngược lại thì trả về giá trị null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        int UpdateProfile(Object data);

        int ChangePassword(string accountId, string newPassword);
    }
}
