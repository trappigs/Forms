using Forms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Forms.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            //�r�n listesi View sayfas�na g�nderiliyor
            return View(Repository.Products);
        }

        [HttpGet]
        public IActionResult Index(string searchString, string category)
        {
            // products de�i�kenine t�m �r�nler koyuluyor
            var products = Repository.Products;


            // searchstring null veya bo� de�ilse e�er bu ko�ul �al���yor
            if (!String.IsNullOrEmpty(searchString))
            {
                // �r�nlerin i�inde arama yap�l�yor
                // Mevcut �r�nler aras�ndan where ko�uluyla linq expression kullan�l�yor
                // p denilen de�i�ken products i�erisinde yer alan her bir �r�n� temsil ediyor
                // bu, products i�erisindeki �r�nlerin adlar� i�erisinde, bizim search stringimiz var m� kontrol ediliyor
                // varsa e�er, listeye �evirilerek, productsa kaydediliyor
                products = products.Where(p => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            // category null veya bo� de�ilse e�er bu ko�ul �al���yor
            // category 0 de�ilse e�er bu ko�ul �al���yor
            // category de�erine g�re products listesi filtreleniyor
            if (!String.IsNullOrEmpty(category) && category != "0")
            {
                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
            }

            // bundan sonra taray�c�da linkin sonuna ?searchString=iphone yazarak arama yapabiliriz

            // model ad�nda bir de�i�ken olu�turuyoruz
            // bu de�i�kenin i�erisine products ve categories listesini at�yoruz
            // ve bu de�i�keni view sayfas�na g�nderiyoruz
            var model = new ProductViewModel
            {
                Products = products,
                Categories = Repository.Categories,
                SelectedCategory = int.Parse(category)
            };

            return View(model);
        }
    }
}