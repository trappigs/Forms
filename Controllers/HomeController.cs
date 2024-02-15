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

    }
}
