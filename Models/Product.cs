using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Forms.Models
{
    // bind edilecek alanları burada da tanımlayabiliyoruz
    // bind edilmesini istemediğimiz alanlara giderek BindNever yazarsak da bind edilmelerini engellemiş oluyoruz
    //[Bind("Name", "Price")]
    public class Product
    {
        // Product Id'nin namenin çağırılmasıyla tabloda nasıl görünmesi gerektiğini belirtiyoruz
        [Display(Name = "Ürün Id")]
        // BindNever attribute'u ile bu alanın bind edilmemesini sağlıyabiliyoruz
        //[BindNever]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required]
        // null değer alamasaydı, required özelliğinin anlamı olmazdı. Çünkü null değer alamazsa, zaten null değer alamazdı
        public string? Name { get; set; } = string.Empty;

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat bilgisi girilmelidir.")]
        public int? Price { get; set; }

        [Display(Name = "Resim")]
        [Required]
        // string length attribute'u ile bu alanın maksimum kaç karakter alabileceğini belirleyebiliyoruz
        [StringLength(25, ErrorMessage = "8 karakterden fazla girilemez")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name = "Durum")]
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public int? CategoryId { get; set; }
    }
}
