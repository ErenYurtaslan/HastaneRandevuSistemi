using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace HastaneRandevuSistemi.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int HastaId { get; set; }


        [ValidateNever]
        public int? DoktorId { get; set; }


        [ForeignKey(("DoktorId"))]
        [ValidateNever]
        public Doktor? Doktor { get; set; }
    }
}
