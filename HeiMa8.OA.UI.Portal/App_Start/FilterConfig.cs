using System.Web;
using System.Web.Mvc;
using HeiMa8.OA.UI.Portal.Models;

namespace HeiMa8.OA.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionFilterAttribute());

            //filters.Add(new LoginCheckFilterAttribute() { isCheck = true });
        }
    }
}