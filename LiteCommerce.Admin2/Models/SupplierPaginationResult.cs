﻿using Litecommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class SupplierPaginationResult : PaginationResult
    {
        /// <summary>
        /// Danh sách supplier
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}