using Microsoft.AspNetCore.Mvc;

namespace OrnekNetCoreUyg2.Controllers
{
    public class HomeController : Controller
    {
        //Bir class'ın request karşılayabilmesi için Controller sınıfından kalıtım alması gerekmektedir. 
        //Controller -> bir sınıfı request alabilir ve response döndürebilir / controller yapabilmek için ilgili sınıfı bu class'tan türetmek gerekir. 

        //Controller içerisindeki istekleri karşılayan metotlara action metod denir. 
        //Controller sınıfı içerisindeki tüm metotlar action metotdur. 
        //Action metot->controllera gelen isteği karşılayan ve gerekli operasyonları gerçekleştiren metotlardır. 
        public IActionResult Index()
        {
            return View();
        }
   
    }
}
