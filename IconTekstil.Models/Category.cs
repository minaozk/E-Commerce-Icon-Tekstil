using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace IconTekstil.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Kategori Adı 30 karakterden büyük olamaz")]
        [DisplayName("Kategori Adı")]
        public string Name { get; set; }
        [DisplayName("Sıra Numarası")]
        [Range(1,100, ErrorMessage = "Sıra numarası 1-100 arasında olmalıdır.")]
        public int DisplayOrder { get; set; }
    }
}
