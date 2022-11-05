using Microsoft.AspNetCore.Mvc;
using OrnekNetCoreUyg2.Models;

namespace OrnekNetCoreUyg2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult GetProducts()
        {
            #region Controller & View
            //Controllerın temel amacı gelen requestleri karşılamak ve requestin gereği olan servisleri tetiklemelidir.İş yapmaz.İşler başka kısımda yapılmaktadır.
            //Controller işi yapacak yönlendirmede bulunur.
            //Controller içerisindeki actionlar gerkeli noktalara yönlendirme yapar fakat iş yapmaz. Actionlar iş yapan servisleri çağırır,iş yapmaz.
            //Bir controllera ait viewların hepsi ilgili controller ismi altında bulunması gerekiyor. Views/Product
            //Default olarak ilgili actiona ait bir view oluşturulacaksa, action ile aynı isimde olması gerekmektedir. 
            //View fonksiyonu bu actiona ait cshtml dosyasını tetikleyecek olan fonksiyondur. 

            //return View(); //->ilgili action ismiyle birebir aynı olan viewı tetikler


            // belirtilen view ismindeki view dosyasını render eder. 
            //ViewResult result = new ViewResult();
            //result = View("other");
            //return result;
            #endregion

            #region Model
            //Model katamındaki bir nesneyi kullanmak controllerdan modela gitmek anlamına gelmektedir. 
            Product product = new Product();

            return View();
            #endregion

        }

        #region Action Turler

        #region ViewResult
        //Response olarak bir View dosyasını (.cshtml) render etmemizi sağlayan action türüdür.
        public ViewResult GetProductViewResult()
        {
            ViewResult result= View();
            return result;
        }
        #endregion

        #region PartialViewResult
        //Bir view dosyasını(.cshtml) render etmemizi sağlayan bir action türüdür.
        //ViewResulttan temel farkı, client tabanlı yapılan Ajax isteklerinde PartialViewResult kullanılmaktadır.. 
        //Teknik fark: ViewResult _ViewStar.cshtml dosyasını baz alır. Lakin PartialViewResult ise ilgili dosyayı baz almadan render edilir. 
        public PartialViewResult GetProductPartialViewResult()
        {
            PartialViewResult partialViewResult = PartialView();
            return partialViewResult;
        }
        #endregion

        #region JsonResult
        //Üretilen datayı JSON türüne dönüştürüp döndüren bir action türüdür.
        //client tabanlı tercih edilir.Ajax işlemelrinde
        public JsonResult GetProductJsonResult()
        {
            JsonResult result= Json(new Product
            {
                Id = 5,
                ProductName = "Çanta",
                Quantity = 10
            });
            return result;
        }
        #endregion

        #region EmptyResult
        //Bazen gelen istekler neticesinde herhangi bir şey döndürmek istemeyebiliriz. 
        //Böyle bir durumda EmptyResult action türü kullanılabilir. 
        //Response var fakat result olmamış oluyor
        public EmptyResult GetProductEmptyResult()
        {
            return new EmptyResult();   
        }

        //void ile de boş result döndürülebilir
        public void GetProductsVoid()
        {

        }
        #endregion

        #region ContentResult
        //İstek neticesinde cevap olarak metinsel bir değer döndürmemizi sağlayan action türüdür. 
        public ContentResult GetProductContentResult()
        {
            ContentResult result = Content("Content içeriğidir");
            return result;
            //Sonucu text olarak gönderirir, sayfa html değil text formatında olarak davranır. 
            //client tabanlı tercih edilir..Ajax işlemelrinde
        }
        #endregion

        #region ActionResult
        //Gelen bir istek neticesinde geriye döndürülecek action türleri değişkenlik gösterebildiği durumlarda kullanılır.
        //Bütün result türlerinin base clasıdır / atasıdır. 
        public ActionResult GetProductActionResult()
        {
            if (true)
            {
                return Json(new object());
            }
            else if (false)
            {
                return Content("Content");
            }
            return View();
        }
        #endregion

        #region IActionResult
        //Action result bir interface'idir.

        #endregion

        #endregion


        #region NonAction ve NonController
        //Bir controller içerisidne oluşturulan metod iş mantığı yürüten bir metot ise request karşılamaktan ziyade iş odaklı çalışıyorsa ona gelen requestleri engellemek gerekir. 
        public IActionResult Index()
        {
            X(); //-> request karşılamaz, iş mantığı amacıyla kullanılır. 
            //Metodu sadece iş amacıyla kullanıcaksak ilgil metodun action metod olmadığını belirtmemiz gerekir. 
            return View();
        }

        [NonAction] //-> Dışarıdan request almazlar. İş amaçlı operasyonel işlemlerde kullanılırlar.   
        public void X()
        {

        }

        //Controllerın dışarıdan istek alması istenmiyor ise [NonController] attribute kullanılabilir. 
        #endregion
    }
}
