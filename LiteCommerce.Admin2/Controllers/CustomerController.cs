﻿using Litecommerce.DomainModels;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.ACCOUNTANT)]
    public class CustomerController : Controller
    {
        [Authorize]
        public ActionResult Index(string country = "",int page = 1, string searchValue = "")
        {

            int pageSize = 10;
            int rowCount = 0;
            List<Customer> listOfCustomers = CatalogBLL.ListOfCustomers(country,page, pageSize, searchValue, out rowCount);

            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCustomers,
                Country = country

            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Input(string id = "")
        {

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Customer";
                    Customer newCustomer = new Customer()
                    {
                        CustomerID = ""
                    };
                    ViewBag.Mode = "create";
                    return View(newCustomer);
                }
                else
                {
                    ViewBag.Title = "Edit a Customer";
                    Customer editCustomer = CatalogBLL.GetCustomer(id);
                    if (editCustomer == null)
                        return RedirectToAction("Index");
                    ViewBag.Mode = "edit";
                    return View(editCustomer);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Input(Customer model, string mode)
        {
            try
            {
                //TODO: Kiểm tra tính hợp lleej của dự liệu được nhập
                if (string.IsNullOrEmpty(model.CustomerID))
                    ModelState.AddModelError("CustomerID", "CustomerID expected");
                else
                {
                    if(model.CustomerID.Length != 5)
                    {
                        ModelState.AddModelError("CustomerID", "CustomerID length must be 5");
                    }
                }
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", "CompanyName expected");
                if (string.IsNullOrEmpty(model.ContactName))
                    ModelState.AddModelError("ContactName", "ContactName expected");
                if (string.IsNullOrEmpty(model.ContactTitle))
                    ModelState.AddModelError("ContactTitle", "ContactTitle expected");
                if (string.IsNullOrEmpty(model.Address))
                    ModelState.AddModelError("Address", "Address expected");
                if (string.IsNullOrEmpty(model.City))
                    model.City = "";
                if (string.IsNullOrEmpty(model.Country))
                    model.Country = "";
                if (string.IsNullOrEmpty(model.Phone))
                    model.Phone = "";
                if (string.IsNullOrEmpty(model.Fax))
                    model.Fax = "";

                if (!ModelState.IsValid)
                {
                    ViewBag.Title = model.CustomerID == "" ? "Add new Customer" : "Edit a Customer";
                    return View(model);
                }
                //TODO: Lưu dữ liệu vào DB
                Customer existCustomer = CatalogBLL.GetCustomer(model.CustomerID);
                if (existCustomer == null)
                {
                    CatalogBLL.AddCustomer(model);
                }
                else
                {
                    CatalogBLL.UpdateCustomer(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                ViewBag.Mode = mode;
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string[] CustomerIDs = null)
        {
            if (CustomerIDs != null)
                CatalogBLL.DeleteCustomers(CustomerIDs);
            return RedirectToAction("Index");
        }
    }
}