using Forms.Models;
using Microsoft.AspNetCore.Mvc;
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

    }
}
