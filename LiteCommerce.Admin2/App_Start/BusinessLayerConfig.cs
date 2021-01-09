﻿using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.App_Start
{
    public class BusinessLayerConfig
    {
        public static void Init()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCOmmerce"].ConnectionString;
            //TODO: Khởi tạo các BLL  khi cần sử dụng đến
            CatalogBLL.Initialize(connectionString);
            UserAccountBLL.Initialize(connectionString);
        }
    }
}