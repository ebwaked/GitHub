﻿using System.Web;
using System.Web.Mvc;

namespace C_Sharp_Exercises
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
