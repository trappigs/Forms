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

            if (!String.IsNullOrEmpty(category) && category != "0")
            {
                products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
            }

            // bundan sonra taray�c�da linkin sonuna ?searchString=iphone yazarak arama yapabiliriz

            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");



            return View(products);
        }
    }
}