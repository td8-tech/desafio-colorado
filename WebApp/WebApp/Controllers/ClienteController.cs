using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClienteController : BaseController
    {
        public ClienteController(IConfiguration configuration) : base(configuration)
        {
        }

        public IActionResult Index(long? id)
        {
            ViewBag.idCliente = id != null ? id : 0;
            return View();
        }
    }
}
