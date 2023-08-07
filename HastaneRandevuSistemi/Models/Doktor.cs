using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HastaneRandevuSistemi.Models
{
    public class Doktor
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DisplayName("Doktor Adı-Soyadı:")]
        public required string DoktorAdi { get; set; }


        [DisplayName("Poliklinik:")]
        public string? Poliklinik { get; set; }



        [Required]
        [DisplayName("Çalışma Günleri-Saatleri:")]
        public required string Calisma { get; set; }



        [DisplayName("Branşı:")]
        [ValidateNever]
        public int? DoktorBransId { get; set; }




        [ForeignKey(("DoktorBransId"))]
        [ValidateNever]
        public DoktorBrans? DoktorBrans { get; set; }



        [ValidateNever]
        [DisplayName("Resim Yükle:")]
        public string? ResimUrl { get; set; }
    }
}
