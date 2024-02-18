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

        // Metodumuzu, async ve Task yapıyoruz. Bunun sebebi: async çalıştırmak istiyoruz ve async çalıştırabilmek için
        // View Metodlarının Task olması gerekiyor
        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile imageFile)
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
            var extension = "";

            // Gönderilen dosyanın null olup olmadığını kontrol ediyoruz
            if (imageFile != null)
            {
                // sadece izin verdiğimiz uzantılı dosyalar yüklenebilmeli
                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                // yüklenilen dosyanın uzantısını öğreniyoruz
                extension = Path.GetExtension(imageFile.FileName);
                // yüklenilen dosyanının uzantısına izin verip vermediğimizi kontrol ediyoruz.
                // izin vermiyorsak hata veriyoruz
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Geçerli bir resim seçiniz.");
                }
            }

            if (ModelState.IsValid)
            {
                // yüklenilen dosya null değilse işlemleri gerçekleştireceğiz
                if (imageFile != null)
                {
                    // yüklenecek dosya projeye klasörüne yükleneceği ve çakışma olabileceği için
                    // rastgele bir isim belirliyoruz
                    var randomFileName = Path.GetRandomFileName() + extension;
                    // resimlerin yükleneceği klasör yolunu belirliyoruz
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomFileName);

                    // resimleri yükleme işlemleri
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        // asenkron bir şekilde resmin projemize yüklenmesini sağlıyoruz
                        await imageFile.CopyToAsync(stream);
                    }

                    // resmin adının producta eklenmesini sağlıyoruz
                    // bunu yapmazsak, index.cshtml bunu görüntüleyemez
                    model.Image = randomFileName;
                    model.ProductId = Repository.GetProductId();
                    Repository.CreateProduct(model);
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


        // kullanıcıya editlemek istediği sayfayı gösterebilmek için
        // IActionResult metodu yazıyoruz
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");

            // id null ise, kullanıcıya sayfa bulunamadı hatası veriyoruz
            if (id == null)
            {
                return NotFound();
            }

            // kullanıcıya sadece editlemek istediği ürünü gösterebilmek için gönderilen id yi değişkene kaydediyoruz
            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);

            // id ye göre editlenmek istenen ürün null ise kullanıcıya sayfa bulunamadı hatası gönderiyoruz
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Product model, IFormFile? imageFile)
        {
            // gönderilen id, ürün id ile aynı mı kontrol ediyoruz
            if (id != model.ProductId)
            {
                return NotFound();
            }

            // formdan gönderilen değerler uygun mu diye kontrol ediyoruz
            if (ModelState.IsValid)
            {
                // resim yükleme işlemlerini gerçekleştiriyoruz
                var extension = "";
                if (imageFile != null)
                {
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg" };
                    extension = Path.GetExtension(imageFile.FileName);
                    var randomFileName = Path.GetRandomFileName() + extension;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    // Productın mevcut Image parametresine yeni bir değer atıyoruz
                    model.Image = randomFileName;
                }

                // Create metodundan farklı olarak, EditProduct metodunu kullanıyor
                // ve ürünün özelliklerini değiştiriyoruz
                Repository.EditProduct(model);
                // ürünü düzenledikten sonra Index sayfasına geri gönderiyoruz
                return RedirectToAction("Index");
            }

            // ModelState.IsValid false olduğu için model bilgileriyle birlikte Edit sayfasının
            // bir daha açılmasını sağlıyoruz
            return View(model);
        }


        public IActionResult Delete(int? id)
        {
            // gönderilen ürün id'ye göre, ürün modelini entity değişkenine aktarıyoruz
            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);


            if (id == null || entity == null)
            {
                return NotFound();
            }

            return View("DeleteConfirm", entity);
        }

        [HttpPost]
        public IActionResult Delete(int? id, int ProductId)
        {

            if (id != ProductId)
            {
                return NotFound();
            }

            var entity = Repository.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (entity == null)
            {
                return NotFound();
            }

            Repository.DeleteProduct(entity);

            return RedirectToAction("Index");
        }


    }
}