using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HastaneRandevuSistemi.Models
{
    public class DoktorBrans
    {
        [Key] //primary key  
        public int Id { get; set; }

        [Required(ErrorMessage = "Branş adı boş bırakılamaz.")] //not null
        [MaxLength(25)]
        [DisplayName("Doktorun Branşı:")]
        public string Ad { get; set; }
    }
}
