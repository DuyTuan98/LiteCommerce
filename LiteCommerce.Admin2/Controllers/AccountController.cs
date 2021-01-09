using Litecommerce.DomainModels;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DataLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            string ActionRequest = "Default";
            ViewBag.ActionRequest = ActionRequest;
            Employee account = this.getCurrentUser();

            return View(account);
        }

        [HttpPost]
        public ActionResult Index(Employee fakeEmployee = null, HttpPostedFileBase PhotoPathFile = null, string actionRequest = "Default")
        {
            ViewBag.ActionRequest = actionRequest;

            switch (actionRequest)
            {
                case "UpdateInfos":
                    Employee account = this.getCurrentUser();
                    try
                    {

                        if (string.IsNullOrEmpty(fakeEmployee.FirstName))
                        {
                            ModelState.AddModelError("UpdateInfos_FirstName", "First name is required");
                        }
                        else if (fakeEmployee.FirstName.Length > 10)
                        {
                            ModelState.AddModelError("UpdateInfos_FirstName", "Length of first name field is 10");
                        }

                        if (string.IsNullOrEmpty(fakeEmployee.LastName))
                        {
                            ModelState.AddModelError("UpdateInfos_LastName", "Last name is required");
                        }
                        else if (fakeEmployee.LastName.Length > 20)
                        {
                            ModelState.AddModelError("UpdateInfos_LastName", "Length of last name field is 20");
                        }

                        if (string.IsNullOrEmpty(fakeEmployee.Title))
                        {
                            ModelState.AddModelError("UpdateInfos_Title", "Title is required");
                        }
                        else if (fakeEmployee.Title.Length > 30)
                        {
                            ModelState.AddModelError("UpdateInfos_Title", "Length of title field is 30");
                        }

                        if (fakeEmployee.BirthDate == null)
                        {
                            ModelState.AddModelError("UpdateInfos_BirthDate", "Birth date is required");
                        }

                        if (string.IsNullOrEmpty(fakeEmployee.Address))
                        {
                            ModelState.AddModelError("UpdateInfos_Address", "Address is required");
                        }

                        if (string.IsNullOrEmpty(fakeEmployee.City))
                        {
                            ModelState.AddModelError("UpdateInfos_City", "City is required");
                        }

                        if (string.IsNullOrEmpty(fakeEmployee.Country))
                        {
                            ModelState.AddModelError("UpdateInfos_Country", "Country is required");
                        }

                        if (string.IsNullOrEmpty(fakeEmployee.HomePhone))
                        {
                            ModelState.AddModelError("UpdateInfos_HomePhone", "Home phone is required");
                        }

                        if (PhotoPathFile == null)
                        {
                            fakeEmployee.PhotoPath = account.PhotoPath;
                        }
                        else
                        {
                            string pic = System.IO.Path.GetFileName(PhotoPathFile.FileName);
                            string path = System.IO.Path.Combine(Server.MapPath("~/Images/"), pic);
                            PhotoPathFile.SaveAs(path);
                            fakeEmployee.PhotoPath = PhotoPathFile.FileName;
                        }

                        if (ModelState.IsValid)
                        {
                            int rs = UserAccountBLL.UpdateProfile(fakeEmployee, UserAccountTypes.Employee);
                            return View(this.getCurrentUser());
                        }
                        else
                        {
                            ViewBag.FakeEmployee = fakeEmployee;
                            return View(this.getCurrentUser());
                        }

                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError(string.Empty, "Error: " + e.Message);
                        return View(this.getCurrentUser());
                    }
                case "Default":
                default:
                    return RedirectToAction("Index");
            }

        }

        
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string email = "", string password = "")
        {
            UserAccount user = UserAccountBLL.Authorize(email, Codes.EncodeMD5.EncodesMD5(password), UserAccountTypes.Employee);

            if (user != null)
            {
                //ghi nhận thông tin của phiên đăng nhập
                WebUserData userData = new WebUserData()
                {
                    UserID = user.UserID,
                    FullName = user.FullName,
                    GroupName = user.Roles, //TODO: cần thay đổi cho đúng
                    LoginTime = DateTime.Now,
                    SessionID = Session.SessionID,
                    ClientIP = Request.UserHostAddress,
                    Photo = user.Photo,
                    AditionalData = email
                };
                FormsAuthentication.SetAuthCookie(userData.ToCookieString(), false);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("LoginError", "LOGIN thất bại");
                ViewBag.Email = email;
                return View();
            }
            //TODO: check tài khoản thông qua CSDL
            //if (email == "sstga1998@gmail.com" && password == "123")
            //{
            //    //ghi nhận phiên đăng nhập của tài khoản
            //    FormsAuthentication.SetAuthCookie(email, false);
            //    //chuyển trang
            //    return RedirectToAction("Index", "Dashboard");
            //}
            //else
            //{
            //    //tạo thông báo lỗi: modelState("key","value")key có thể rỗng
            //    ModelState.AddModelError("", "LOGIN thất bại");
            //    ViewBag.Email = email;
            //    return View();
            //}
        }
        [HttpGet]
        public ActionResult ChangePwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePwd(string email = "", string password = "", string newpassword1 = "", string newpassword2 = "")
        {
            try
            {
                Employee acc = this.getCurrentUser();
                if (email == "")
                {
                    ModelState.AddModelError("Email", "Email is required");
                }
                else if (email != acc.Email)
                {
                    ModelState.AddModelError("Email", "Email isn't correct");
                }
                if (acc.Password != LiteCommerce.Admin.Codes.EncodeMD5.EncodesMD5(password))
                {
                    ModelState.AddModelError("Password", "Password isn't correct");
                }
                if (newpassword1.Length < 8)
                {
                    ModelState.AddModelError("NewPassword1", "New password length must be greater or equal 8");
                }
                else if (newpassword1 != newpassword2)
                {
                    ModelState.AddModelError("NewPassword1", "New password are not same");
                    ModelState.AddModelError("NewPassword2", "New password are not same");
                }
                if (ModelState.IsValid)
                {
                    int rs = UserAccountBLL.ChangePassword(acc.EmployeeID + "", LiteCommerce.Admin.Codes.EncodeMD5.EncodesMD5(newpassword1), UserAccountTypes.Employee);
                    ViewBag.MessageSuccess = "Password changed";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Error: " + e.Message);
            }
            return View();
        }
        private Employee getCurrentUser()
        {
            WebUserData data = System.Web.HttpContext.Current.User.GetUserData();



            Employee account = CatalogBLL.Employee_GetByEmail(data.AditionalData);
            return account;
        }
    }
}