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
            //View->controllerda üretilen datanın js, css gibi ui tabanlı teknolojilerin kullanılarak görselleştirildiği,render edildiği katmandır. 
            //.cshtml , html gibi evrensel değildir sadece .netcore üzerinde çalışır,render edilebilir. ,
            //html içerisine c# kodu yazabildiğimiz teknoloji razor teknolojisidir. 
            //view dosyaları genellikle Views klasörü altında bulunurlar. 
            //View klasörü içerisinde bir view dosyası tutulacaksa ilgili actiona karşılık olarak tutulması gerekmektedir. 
            //Bu sebeple controolera karşılık bir klasör ve o klasör altında actiona karşılık action isminde bir .cshtml uzantılı dosya bulunmalıdır. 
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return View();
        }

    }
}
