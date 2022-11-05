using Microsoft.AspNetCore.Mvc;

namespace OrnekNetCoreUyg2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult GetProducts()
        {
            //Bir controllera ait viewların hepsi ilgili controller ismi altında bulunması gerekiyor. Views/Product
            //Default olarak ilgili actiona ait bir view oluşturulacaksa, action ile aynı isimde olması gerekmektedir. 
            //View fonksiyonu bu actiona ait cshtml dosyasını tetikleyecek olan fonksiyondur. 

            //return View(); //->ilgili action ismiyle birebir aynı olan viewı tetikler


            // belirtilen view ismindeki view dosyasını render eder. 
            ViewResult result = new ViewResult();
            result = View("other");
            return result;






        }
    }
}
