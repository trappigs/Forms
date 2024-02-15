namespace Forms.Models
{
    // kategori sınıfı
    // kategorilerin id ve adını tutuyor
    public class Category
    {
        public int CategoryId { get; set; }

        public string? Name { get; set; } = string.Empty;
    }
}