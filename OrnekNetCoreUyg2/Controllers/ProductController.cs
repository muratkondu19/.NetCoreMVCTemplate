using Microsoft.AspNetCore.Mvc;
using OrnekNetCoreUyg2.Models;
using OrnekNetCoreUyg2.Models.ViewModels;
using System.Collections.Generic;
using System.Text.Json;

namespace OrnekNetCoreUyg2.Controllers
{
    public class ProductController : Controller
    {
        #region Controller & View & Model
        public IActionResult GetProductsAction()
        {

            //Controllerın temel amacı gelen requestleri karşılamak ve requestin gereği olan servisleri tetiklemelidir.İş yapmaz.İşler başka kısımda yapılmaktadır.
            //Controller işi yapacak yönlendirmede bulunur.
            //Controller içerisindeki actionlar gerkeli noktalara yönlendirme yapar fakat iş yapmaz. Actionlar iş yapan servisleri çağırır,iş yapmaz.
            //Bir controllera ait viewların hepsi ilgili controller ismi altında bulunması gerekiyor. Views/Product
            //Default olarak ilgili actiona ait bir view oluşturulacaksa, action ile aynı isimde olması gerekmektedir. 
            //View fonksiyonu bu actiona ait cshtml dosyasını tetikleyecek olan fonksiyondur. 

            //return View(); //->ilgili action ismiyle birebir aynı olan viewı tetikler


            // belirtilen view ismindeki view dosyasını render eder. 
            ViewResult result = new ViewResult();
            result = View("other");
            return result;


            #region Model
            //Model katamındaki bir nesneyi kullanmak controllerdan modela gitmek anlamına gelmektedir. 
            //Product product = new Product();

            //return View();
            #endregion

        }
        #endregion

        #region Action Turler

        #region ViewResult
        //Response olarak bir View dosyasını (.cshtml) render etmemizi sağlayan action türüdür.
        public ViewResult GetProductViewResult()
        {
            ViewResult result = View();
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
            JsonResult result = Json(new Product
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
        public IActionResult IndexNonAction()
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



        #region View Yapılanması

        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product{Id=1,ProductName="A Product",Quantity=10},
                new Product{Id=2,ProductName="B Product",Quantity=15},
                new Product{Id=3,ProductName="C Product",Quantity=20},
            };

            //Controllerdan viewa 4 farklı şekilde veri gönderilebilir. 
            //Biri model bazlı veri gönderme ve diğerleri veri taşıma kontrolleriyle veri göndermedir. 
            //Model bazlı göndermede kullanıcıdan veri alabilirken, veri taşıma kontrolleriyle sadeece controllerdan viewa veri gönderme operasyonu gerçekleştirebiliriz. 

            #region Model Bazlı Veri Gönderme
            //return View(products); //Controllera model bazlı veri gönderme için viewı return etmek ve datayı bildirmek yeterlidir. 
            #endregion

            #region Veri Taşıma Kontroller

            #region ViewBag
            //Viewa taşınacak datayı dynamic şekilde tanımlanan bir değişkenle taşımamızı sağlayan veri taşıma kontrolüdür. 
            ViewBag.products = products;
            #endregion

            #region ViewData
            //Actiondaki datayı viewbag gibi viewa taşımaktadır. 
            //Viewbagten farkı : viewbag datayı dynmaic taşır ve runtimeda türü belli olurken,viewdata boxing yapmaktadır.Viewde unboxing yapıp kullanılır
            ViewData["produtcs"] = products; //products boxing edilmiş oldu
            #endregion

            #region TempData
            //Actiondaki datayı viewbag gibi viewa taşımaktadır. 
            //Viewdata gibi boxinge tabi tutar ve view üzerinde unboxing yapılmasını bekler. 

            //TempData["products"]=products;

            //Actionlara kendi aralarında yönlendirme yapabiliyoruz,index.cshtml operasyonu bittikten sonra kullanıcıya veri göndermeden başka bir actiona redirct işlemi gerçekleştirilebiliyor
            //O actiondaki operasyonun da gerçekleştirilmesi sağlanabilmektedir. 
            //Farklı bir actiona yönlendirme söz konusu olduğunda viewbag ya da viewdata ile veri taşınamazken tempdata ile veriler taşınabilmektedir. 
            //Çünkü tempdata aslında cookie kullanmaktadır.
            //Cookie üzerinde veri taşıdığı için verinin serilize edilmesi gerekmektedir. 

            string data = JsonSerializer.Serialize(products);
            TempData["products"] = data;

            #endregion

            #endregion

            return View();
            //return RedirectToAction("Index2"); //Mevut controllerda Index2 actionuna yönlendirme yapılmaktadır. Controller adı da yazılabilir.

        }

        public IActionResult Index2()
        {
            //TempData ile verinin farklı actiona taşınabildiği gözlemlenecektir.
            //var v1 = ViewBag.products;
            //var v2 = ViewData["products"];
            var data = TempData["products"].ToString();
            List<Product> products = JsonSerializer.Deserialize<List<Product>>("data");
            return View();
        }

        #endregion


        #region View'e Tuple Nesne Gönderimi
        //Tuple-> içerisinde birden fazla değeri, veriyi, nesneyi referans yapabilen bir nesnedir.
        //Bir product ve user nesnesini bütün olarak tasarlamak istiyorsan viewmodel oluşturulur ve nesnelerin referansları eklenir. 
        //Bir viewmodel üzerinde birden fazla nesneyi referans yaparak tek bir nesne üzerinden de kullanılaibilir.


        public IActionResult GetProducts()
        {
            Product product = new Product
            {
                Id = 1,
                ProductName = "ABC Product",
                Quantity = 5
            };
            User user = new User
            {
                Id = 1,
                Name = "Murat",
                LastName = "Konu"
            };

            //Viewmodel kullanımı ile birden fazla nesneyi ele alma
            //UserProduct userProduct = new UserProduct
            //{
            //    User = user,
            //    Product = product,
            //};

            //Tuple kullanımı
            var userProduct = (product, user); //Derleyici tuple olarak algılar ve view tarafında karşılanması gerekir.
            return View(userProduct);
        }
        #endregion
    }
}
