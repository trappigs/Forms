namespace Forms.Models
{
    // veri tabanı yerine geçiyor
    public class Repository
    {
        // ürünlerin ve kategorilerin listesi
        // ürünleri ve kategorileri buradan ekleyeceğiz, ve burayı geçici veri tabanıymış gibi kullanacağız
        public static List<Product> _products = new();
        public static List<Category> _categories = new();

        // ürünlerin ve kategorilerin eklenmesini bu kurucu metotta yapacağız
        static Repository()
        {
            _categories.Add(new Category { CategoryId = 1, Name = "Telefon" });
            _categories.Add(new Category { CategoryId = 2, Name = "Bilgisayar" });

            _products.Add(new Product { ProductId = 1, Name = "iPhone 14", Price = 2000, Image = "1.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 2, Name = "iPhone 15", Price = 3000, Image = "2.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 3, Name = "Samsung S7", Price = 4000, Image = "3.jpg", IsActive = true, CategoryId = 1 });
            _products.Add(new Product { ProductId = 4, Name = "Samsung S8", Price = 5000, Image = "4.jpg", IsActive = true, CategoryId = 1 });

            _products.Add(new Product { ProductId = 6, Name = "Macbook Air", Price = 7000, Image = "5.jpg", IsActive = true, CategoryId = 2 });
            _products.Add(new Product { ProductId = 7, Name = "Macbook Pro", Price = 8000, Image = "6.jpg", IsActive = true, CategoryId = 2 });
        }


        // ürünlerin çağırılması için oluşturduğumuz metot
        public static List<Product> Products
        {
            get { return _products; }
        }

        public static void CreateProduct(Product entity)
        {
            _products.Add(entity);
        }


        // ürünümüzü düzenlemek için Repository classındayken
        // bir EditProduct metodu yazıyoruz ve ürününün çeşitli özelliklerini gelen parametrelere
        // göre değiştiriyoruz
        public static void EditProduct(Product updatedProduct)
        {
            var entity = _products.FirstOrDefault(p => p.ProductId == updatedProduct.ProductId);

            if (entity != null)
            {
                entity.Name = updatedProduct.Name;
                entity.Price = updatedProduct.Price;
                entity.Image = updatedProduct.Image;
                entity.CategoryId = updatedProduct.CategoryId;
                entity.IsActive = updatedProduct.IsActive;
            }
        }

        public static int GetProductId()
        {
            if (_products.Count > 0)
            {
                return _products.Max(p => p.ProductId) + 1;
            }
            else { return 0; }
        }

        // kategorilerin çağırılması için oluşturduğumuz metot
        public static List<Category> Categories
        {
            get { return _categories; }
        }
    }
}
