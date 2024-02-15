using System.ComponentModel.DataAnnotations;

namespace Forms.Models
{
    // kategori sınıfı
    // kategorilerin id ve adını tutuyor
    public class Category
    {
        [Display(Name="")]
        public int? CategoryId { get; set; }

        [Display(Name = "")]
        public string? Name { get; set; } = string.Empty;
    }
}