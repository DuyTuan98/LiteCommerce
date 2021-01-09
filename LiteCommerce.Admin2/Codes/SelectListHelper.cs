using Litecommerce.DomainModels;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public class SelectListHelper
    {
        public static List<SelectListItem> Countries(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "...All Countries..." });
            }
            foreach (var country in CatalogBLL.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = country.CountryName,
                    Text = country.CountryName
                });
            }
            //TODO: add dánh ách category lấy từ DB vào list
            return list;
            // List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem() { Value = "USA", Text = "United States" });
            //list.Add(new SelectListItem() { Value = "UK", Text = "England" });
            // list.Add(new SelectListItem() { Value = "VN", Text = "Vietnam" });
            // return list;
        }
        public static List<SelectListItem> Categories(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "...All Categories..." });
            }
            foreach (var category in CatalogBLL.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = category.CategoryID.ToString(),
                    Text = category.CategoryName
                });
            }
            //TODO: add dánh ách category lấy từ DB vào list
            return list;
        }
        public static List<SelectListItem> Suppliers(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "...All Supplier..." });
            }
            foreach (var supplier in CatalogBLL.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = supplier.SupplierID.ToString(),
                    Text = supplier.CompanyName
                });
            }
            return list;
        }
        public static List<SelectListItem> Attributes(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--Choose Attribute--" });
            }
            List<Attributes> data = CatalogBLL.ListOfAttributes();
            foreach (var attribute in data)
            {
                list.Add(new SelectListItem() { Value = attribute.AttributeName, Text = attribute.AttributeName });
            }
            return list;
        }
    }
}