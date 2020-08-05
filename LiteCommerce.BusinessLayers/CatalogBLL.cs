using Litecommerce.DomainModels;
using LiteCommerce.DataLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng xử lý nghiệp vụ liên quan đến
    /// quản lý dữ liệu chung của hệ thống như: nhằ cung cấp, khách hàng, mặt hàng,...
    /// </summary>
    public static class CatalogBLL
    {
        /// <summary>
        /// Hàm phải được gọi để khởi tạo chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new DataLayers.SqlServer.SupplierDAL(connectionString);
            CustomerDB = new DataLayer.SqlServer.CustomerDAL(connectionString);
            ShipperDB = new DataLayers.SqlServer.ShipperDAL(connectionString);
            CategoryDB = new DataLayers.SqlServer.CategoryDAL(connectionString);
            EmployeeDB = new DataLayers.SqlServer.EmployeeDAL(connectionString);
            CountryDB = new DataLayers.SqlServer.CountryDAL(connectionString);
            ProductDB = new DataLayers.SqlServer.ProductDAL(connectionString);
            AttributeDB = new DataLayers.SqlServer.AttributeDAL(connectionString);
            ProductAttributeDB = new DataLayers.SqlServer.ProductAttributeDAL(connectionString);
        }
        private static ICountryDAL CountryDB { get; set; }
        public static List<Country> ListOfCountries()
        {
            return CountryDB.List();
        }

        #region Khai báo các thuộc tính giao tiếp với DAL
        /// <summary>
        /// 
        /// </summary>
        private static ISupplierDAL SupplierDB { get; set; }
        #endregion

        #region Khai báo các chức naqwng xử lý nghiệp vụ
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }
        public static List<Supplier> ListOfSuppliers()
        {
            return SupplierDB.List(1, -1, "");
        }
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }
        /// <summary>
        /// bổ sung 1 supplier. Hàm trả về ID của supplier được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin của một supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        /// <summary>
        /// xóa (các) supplier dựa vào ID
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public static int DeleteSuppliers(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }
        #endregion

        #region Khai báo các chức naqwng xử lý nghiệp vụ
        private static ICustomerDAL CustomerDB { get; set; }
        public static List<Customer> ListOfCustomers(string country, int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = CustomerDB.Count(searchValue, country);
            return CustomerDB.List(country, page, pageSize, searchValue);
        }
        public static Customer GetCustomer(string customerID)
        {
            return CustomerDB.Get(customerID);
        }
        /// <summary>
        /// bổ sung 1 supplier. Hàm trả về ID của supplier được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin của một supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }
        
        public static int DeleteCustomers(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }

        #endregion

        #region shipper
        private static IShipperDAL ShipperDB { get; set; }
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }
        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }
        /// <summary>
        /// bổ sung 1 supplier. Hàm trả về ID của supplier được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin của một supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        /// <summary>
        /// xóa (các) supplier dựa vào ID
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public static int DeleteShippers(int[] shipperIDs)
        {
            return ShipperDB.Delete(shipperIDs);
        }
        #endregion

        #region category
        private static ICategoryDAL CategoryDB { get; set; }
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue);
        }
        public static List<Category> ListOfCategories()
        {
            return CategoryDB.List(1, -1, "");
        }
        public static Category GetCategory(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }
        /// <summary>
        /// bổ sung 1 supplier. Hàm trả về ID của supplier được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// Cập nhật thông tin của một supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return CategoryDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        public static int DeleteCategories(int[] categoryIDs)
        {
            return CategoryDB.Delete(categoryIDs);
        }
        #endregion

        #region employee
        private static IEmployeeDAL EmployeeDB { get; set; }

        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }

        public static Employee GetEmployee(int EmployeeID)
        {
            return EmployeeDB.Get(EmployeeID);
        }
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        public static int DeleteEmployees(int[] EmployeeIDs)
        {
            return EmployeeDB.Delete(EmployeeIDs);
        }
        public static Employee Employee_GetByEmail(string email, int exceptID = 0)
        {
            return EmployeeDB.GetByEmail(email, exceptID);
        }
        #endregion

        #region product
        private static IProductDAL ProductDB { get; set; }

        public static List<Product> ListOfProducts(int page, int pageSize, string searchValue, int category, int supplier, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = ProductDB.Count(searchValue, category, supplier);
            return ProductDB.List(page, pageSize, searchValue, category, supplier);
        }
        public static Product GetProduct(int productID)
        {
            return ProductDB.Get(productID);
        }
        public static int AddProduct(Product data)
        {
            return ProductDB.Add(data);
        }
        public static bool UpdateProduct(Product data)
        {
            return ProductDB.Update(data);
        }
        public static int DeleteProducts(int[] productIDs)
        {
            return ProductDB.Delete(productIDs);
        }
        #endregion

        #region productAttribute
        private static IProductAttributeDAL ProductAttributeDB { get; set; }
        private static IAttributeDAL AttributeDB { get; set; }
        public static List<Attributes> ListOfAttributes()
        {
            return AttributeDB.List();
        }
        public static List<ProductAttribute> ListOfProductAttribute(int productID)
        {
            return ProductAttributeDB.List(productID);
        }
        public static int AddProductAttribute(ProductAttribute data)
        {
            return ProductAttributeDB.Add(data);
        }
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return ProductAttributeDB.Update(data);
        }
        public static int DeleteProductAttributes(int[] attributeIDs)
        {
            return ProductAttributeDB.Delete(attributeIDs);
        }
        public static ProductAttribute GetProductAttribute(int attributeID)
        {
            return ProductAttributeDB.Get(attributeID);
        }
        #endregion
    }


}
