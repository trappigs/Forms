using System.ComponentModel.DataAnnotations;

namespace Forms.Models
{
    public class Product
    {
        // Product Id'nin namenin çağırılmasıyla tabloda nasıl görünmesi gerektiğini belirtiyoruz
        [Display(Name = "Ürün Id")]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        public string? Name { get; set; } = string.Empty;

        [Display(Name = "Fiyat")]
        public int Price { get; set; }

        [Display(Name = "Resim")]
        public string Image { get; set; } = string.Empty;

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
    }
}
