using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class KitapTuru
    {
        [Key] //primary key  
        public int Id { get; set; }
         
        [Required(ErrorMessage ="Tür adı boş bırakılamaz.")] //not null
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı:")]
        public string Ad { get; set; }
    }
}
