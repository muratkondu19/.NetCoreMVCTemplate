using Microsoft.AspNetCore.Mvc;

namespace OrnekNetCoreUyg2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult UrunleriGetir()
        {
            return View();
        }
    }
}
