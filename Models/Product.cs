using System.ComponentModel.DataAnnotations;

namespace Forms.Models
{
    public class Product
    {
        // Product Id'nin namenin çağırılmasıyla tabloda nasıl görünmesi gerektiğini belirtiyoruz
        [Display(Name = "Ürün Id")]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required]
        public string? Name { get; set; } = string.Empty;

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat bilgisi girilmelidir.")]
        public int Price { get; set; }

        [Display(Name = "Resim")]
        [Required]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "Durum")]
        [Required]
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public int CategoryId { get; set; }
    }
}
