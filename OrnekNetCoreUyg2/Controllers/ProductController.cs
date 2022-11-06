using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrnekNetCoreUyg2.Models;
using OrnekNetCoreUyg2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using static System.Net.WebRequestMethods;

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


        #region Model Binding
        //Model Binding -> http request ile gelen verilerin ayrıştırılarak ilgili controllerda bulunan action metotlarında uygun bir türe dönüştürülmesi işlemidir.
        //Bir model oluşturup model propertyleri formdaki input alanlarından doluyorsa verimiz oluşturulan model türüne dönüştürülecektir bu dönüştürülmeye binding denmektedir.
        //Binding kullanılıyorsa ise data artık ilgili model olarak karşılanır.Bunun içindeki propertyleri yönetmek yeterli olacaktır. 
        //Kullanıcının form üzerinden girmiş olduğu dataları biz controllerlarda kendimize ait türlerde yakalamak istediğimizde model binding kullanılıyoruz.
        public IActionResult CreateProduct()
        {
            //Burası sayfanın açılmasını sağlayacak, get işlemi yapacak
            //Açılan sayfa üzerinden yapılacak post işlemi sonucunda gönderilen dataları farklı bir action metodunda yakalanacak
            //BinaryReader action metodu varsayılan olarak get türündedir.get isteklerini karşılar. bu fonk post put delete isteklerini karşılamaz

            var product = new Product();
            //View üzerinde yapılan değişiklikler bu nesne üzerine işlenecek
            //form tetiklendiği zaman üzerine işlenen nesne tetiklenmiş olacak ->post actionuna
            //post actionuna gelen parametredeki nesne bu nesnenin dolu halidir
            return View(product);
        }

        [HttpPost] //metodu post olarak işaretleme, işaretlenmez ise varsayılan get'tir.
        public IActionResult CreateProduct(Product product)
        {
            //public IActionResult CreateProduct(string id,string txtProductName,string txtQuantity) ->model binding olmadan
            //Web mimarilerinde bir post/form tetikleniyorsa, işlendiği endpointe içerisindeki inputların değerlerini döndürür
            //Request neticesinde gelen dataların hepsi action metotlarda parametrelerden yakalanmaktadır. 
            return View();
        }
        #endregion


        #region Kullanıcıdan Veri Alma Yöntemleri - Form Üzerinden Veri Alma
        //form üzerinden post ve get türünde veri alabiliyoruz
        //kullanıcıdan veri alınacaksa post kullanılması / verinin post edilmesi gerekir
        public IActionResult GetProduct2()
        {
            return View();
        }

        #region IFormCollection Kullanımı
        //[HttpPost]
        //public IActionResult VeriAl(IFormCollection datas)
        //{
        //    //IFormCollection ->Microsoft.AspNetCore.Http ile gelen bir arayüzdür, bunun sayesinde post edilen formun içerisindeki input nesnelerinin dataları yakalanabilmektedir
        //    var value1 = datas["txtValue1"].ToString(); //name değerlerine göre input deperlerini ele alma
        //    var value2 = datas["txtValue2"].ToString();
        //    var value3 = datas["txtValue3"].ToString();
        //    return View();
        //}
        #endregion

        #region Parametreler ile veri alma 
        //[HttpPost]
        //public IActionResult VeriAl(string txtValue1, string txtValue2, string txtValue3)
        //{
        //    var value1 = txtValue1; //name değerlerine göre input deperlerini ele alma
        //    var value2 = txtValue2;
        //    var value3 = txtValue3;
        //    return View();
        //} 
        #endregion

        #region Model tanımlayarak değerleri alma
        //formdan gelecek olan input propertlerina karşılık bir class tanımlarsak 
        public class Data
        {
            public string txtValue1 { get; set; }
            public string txtValue2 { get; set; }
            public string txtValue3 { get; set; }
        }

        //parametreleri gelecek olan post ile yakalamak isteniyorsa Model kullanılabilir 
        [HttpPost]
        //public IActionResult VeriAl(Data data)
        //{
        //    var value1 = data.txtValue1;
        //    var value2 = data.txtValue2;
        //    var value3 = data.txtValue3;
        //    return View();
        //}
        #endregion

        #region Model Binding ile veri alma
        //view'e model belirlemek gerekmektedir. ->Product belirlenmiş
        //TagHelpers kullanarak asp-for ile belirleme yapılır 
        [HttpPost]
        public IActionResult VeriAl(Product data)
        {
            var value1 = data.Id;
            var value2 = data.ProductName;
            var value3 = data.Quantity;
            return View();
        }

        #endregion
        #endregion

        #region Kullanıcıdan Veri Alma Yöntemleri - QueryString Üzerinden Veri Alma
        //QueryString-> webte belirli verilerde gizli formatta taşıma tercih edilir, bazı veriler güvenlik gerektirmez ve url üzerinde hızlı şekilde taşınır,amaç hızlı şekilde servise eriştirmek
        //url/url?id=2 -> ? kısmı querystring değeridir
        //querystring kullanıcıdan veri almaktan ziyade ilgili uygulamanın istek yapacağı servise bu isteklerde hızlı bir şekilde veri taşır, bunu yazılım kullanır, kullanıcıdan da veri alınabilir
        //query string yapılan requestin türü her ne olursa olsun, querystring değerleri taşınabilir. 


        public IActionResult GetUserProduct()
        {
            return View();
        }

        public IActionResult CreateUserProduct()
        {
            return View();
        }

        #region Parametre ile değerleri alma
        //public IActionResult VeriAlUserProduct(string deger,string olcu)
        //{
        //    //query string değerlerini yakalamak için parametre üzerinden ilgli querystringe denk gelen bir parametre tanımlanabilir .
        //    //girilen parametreler birden fazla olaiblir bunun için & operatörü kullanılabilir ve parametre olarak ifade de tanımlanmalıdır
        //    return View();
        //}
        #endregion

        #region Request içerisinden querystring değerlerini okuma
        //Gelen değerleri parametre tanımlayarak değil requestin içerisine girerek querystring değerlerini okuyabiliriz
        //public IActionResult VeriAlUserProduct()
        //{
        //    var queryString = Request.QueryString; //request yapılan endpointte query styring parametresi eklenmiş mi eklenmemiş mi bununla ilgil bilgi verir
        //    var a = Request.Query["a"].ToString(); //query üzerinden querystring değerlerini yakalayabiliriz
        //    var b = Request.Query["b"].ToString();
        //    return View();
        //}
        //}
        #endregion

        #region Model üzerinden eşleştirme
        //Querystring parametrelerine karşılık gelen birer property isimleri tanımlayarak da değerleri ele alabliriz.
        public IActionResult VeriAlUserProduct(Data data)
        {
            //prop isimlerine göre querystrign göndererek değerler data üzerinden yakalanabilir.
            return View();
        }
        #endregion
        #endregion
    }
}
