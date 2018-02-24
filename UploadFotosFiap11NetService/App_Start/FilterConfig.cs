using System.Web;
using System.Web.Mvc;

namespace UploadFotosFiap11NetService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
