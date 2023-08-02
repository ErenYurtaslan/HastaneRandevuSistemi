using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HastaneRandevuSistemi.Models
{
    public class Kitap
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DisplayName("Kitap Adı:")]
        public required string KitapAdi { get; set; }


        [DisplayName("Tanımı:")]
        public string? Tanim { get; set;}



        [Required]
        [DisplayName("Yazarı:")]
        public required string Yazar { get; set;}

        [Required]
        [Range(0, 10000)]
        [DisplayName("Fiyatı:")]
        public double Fiyat { get; set;}




        [DisplayName("Türü:")]
        [ValidateNever]
        public int? KitapTuruId { get; set; }




        [ForeignKey(("KitapTuruId"))]
        [ValidateNever]
        public KitapTuru? KitapTuru { get; set;}



        [ValidateNever]
        [DisplayName("Resim Yükle:")]
        public string? ResimUrl  { get; set; }
    }

}
