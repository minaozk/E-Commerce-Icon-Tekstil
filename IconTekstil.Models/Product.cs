using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IconTekstil.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IconTekstil.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[DisplayName("Ürün Adı")]
		public string? Title { get; set; }
        [DisplayName("Açıklama")]
        public string Description{ get; set; }
        [DisplayName("Barkod Kodu")]
        public string ISBN { get; set; }
        [DisplayName("Fiyat")]
        public double Price { get; set; }

        [DisplayName("Kategori")]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		[ValidateNever]
		public Category Category { get; set; }
		[ValidateNever]
		public List<ProductImage> ProductImages { get; set; }
		
	}
}
