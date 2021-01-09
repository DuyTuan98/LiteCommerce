﻿using Litecommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ProductAttributeResult
    {
        public Product Product { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}