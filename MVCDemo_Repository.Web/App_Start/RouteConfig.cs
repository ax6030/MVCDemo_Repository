using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCDemo_Repository.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            );

            routes.MapRoute(
                name: "ProductList_Category_Page",                                                // 路由名稱
                url: "Product/Category/{Category}",  // URL 及參數
                new { controller = "Product", action = "List", Category = "all" } // 參數預設值
            );
        }
    }
}
