using Forms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Reflection;

namespace Forms.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(string searchString, string category)
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

            // category null veya boþ deðilse eðer bu koþul çalýþýyor
            // category 0 deðilse eðer bu koþul çalýþýyor
            // category deðerine göre products listesi filtreleniyor
            if (!String.IsNullOrEmpty(category) && category != "0")
            {
                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
            }

            // bundan sonra tarayýcýda linkin sonuna ?searchString=iphone yazarak arama yapabiliriz

            // model adýnda bir deðiþken oluþturuyoruz
            // bu deðiþkenin içerisine products ve categories listesini atýyoruz
            // ve bu deðiþkeni view sayfasýna gönderiyoruz

            var model = new ProductViewModel
            {
                Products = products,
                Categories = Repository.Categories,
                SelectedCategory = category
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            return View();
        }

        // bind kullanarak, viewden sadece Name ve Price alanlarını alıyoruz
        [HttpPost]
        public IActionResult Create(Product model)
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

            model.ProductId = Repository.GetProductId();
            Repository.CreateProduct(model);



            return RedirectToAction("Index");
        }
    }
}