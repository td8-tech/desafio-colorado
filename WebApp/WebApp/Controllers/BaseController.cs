using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    public class BaseController: Controller
    {
        public readonly string? ApiUrl;
        public BaseController(IConfiguration configuration)
        {
            ApiUrl = (string?)configuration.GetValue(typeof(string), "ApiUrl");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ApiUrl = ApiUrl;
        }

        //public ActionResult Index()
        //{
        //    return View("~/Views/Home/index.cshtml");
        //}

        //public ActionResult Cliente()
        //{
        //    return View("~/Views/Cliente/index.cshtml");
        //}
    }
}
