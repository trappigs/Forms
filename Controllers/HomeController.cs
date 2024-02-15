using Forms.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forms.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            //Ürün listesi View sayfasýna gönderiliyor
            return View(Repository.Products);
        }

        [HttpGet]
        public IActionResult Index(string searchString)
        {
            // products deðiþkenine tüm ürünler koyuluyor
            var products = Repository.Products;

            
            // searchstring null veya boþ deðilse eðer bu koþul çalýþýyor
            if (!String.IsNullOrEmpty(searchString))
            {
                // Ürünlerin içinde arama yapýlýyor
                // Mevcut ürünler arasýndan where koþuluyla linq expression kullanýlýyor
                // p denilen deðiþken products içerisinde yer alan her bir ürünü temsil ediyor
                // bu, products içerisindeki ürünlerin adlarý içerisinde, bizim search stringimiz var mý kontrol ediliyor
                // varsa eðer, listeye çevirilerek, productsa kaydediliyor
                products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
            }
            // bundan sonra tarayýcýda linkin sonuna ?searchString=iphone yazarak arama yapabiliriz

            return View(products);
        }
    }
}