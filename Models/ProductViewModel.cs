namespace Forms.Models
{
     // Tüm ürünleri ve kategorileri tek yerden yönetmek için bir class oluşturduk
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = null!;
        public List<Category> Categories { get; set; } = null!;

        public string? SelectedCategory { get; set; }
    }
}
