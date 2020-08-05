using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litecommerce.DomainModels
{
    /// <summary>
    /// Lưu thông tin liên quan đến tài khoản đăng nhập hệ thống
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// ID cuar tài khoản đăng nhập vào hệ thống
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// tên đầy đủ của User (FirstName và LastName)
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Tên file ảnh của user
        /// </summary>
        public string Photo { get; set; }

        public string Title { get; set; }

        public string Roles { get; set; }
    }
}
