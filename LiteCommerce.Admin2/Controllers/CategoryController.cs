using Litecommerce.DomainModels;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.ACCOUNTANT)]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Category> listOfCategory = CatalogBLL.ListOfCategories(page, pageSize, searchValue, out rowCount);
            var model = new Models.CategoryPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCategory

            };
            return View(model);
            //int pageSize = 3;
            //int rowCount = 0;
            //List<Supplier> model = CatalogBLL.ListOfSuppliers(page, pageSize, searchValue, out rowCount);
            //ViewBag.RowCount = rowCount;
            //return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new category";
                    Category newCategory = new Category()
                    {
                        CategoryID = 0
                    };
                    
                    return View(newCategory);
                }
                else
                {
                    ViewBag.Title = "Edit a category";
                    Category editCategory = CatalogBLL.GetCategory(Convert.ToInt32(id));
                    if (editCategory == null)
                        return RedirectToAction("Index");
                    
                    return View(editCategory);
                }
            }
            catch (Exception ex)
            {
                //Log.Write(ex);
                return Content(ex.Message + ": " + ex.StackTrace);
            }

        }

        [HttpPost]
        public ActionResult Input(Category model)
        {
            try
            {
                //TODO: kiểm tra tính hợp lệ của dữ liệu được nhập
                if (string.IsNullOrEmpty(model.CategoryName))
                    ModelState.AddModelError("CategoryName", "CategoryName expected");
                if (string.IsNullOrEmpty(model.Description))
                    ModelState.AddModelError("Description", "Description expected");
                if (!ModelState.IsValid)
                {
                    //ViewBag.Title
                    return View(model);
                }
                //TODO: lưu dữ liệu vào DB
                if (model.CategoryID == 0)
                {
                    CatalogBLL.AddCategory(model);

                }
                else
                {
                    CatalogBLL.UpdateCategory(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ": " + ex.StackTrace);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Delete(int[] categoryIDs = null)
        {
            if (categoryIDs != null)
                CatalogBLL.DeleteCategories(categoryIDs);
            return RedirectToAction("Index");
        }
    }
}