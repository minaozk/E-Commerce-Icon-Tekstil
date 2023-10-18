using IconTekstil.DataAccess.Data;
using IconTekstil.DataAccess.Repository.IRepository;
using IconTekstil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace IconTekstil.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext _db;

		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}



		public void Update(Product obj)
		{
			var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
			if (objFromDb != null)
			{
				objFromDb.Title = obj.Title;
				objFromDb.Description = obj.Description;
				objFromDb.ISBN = obj.ISBN;
				objFromDb.CategoryId = obj.CategoryId;
				objFromDb.Price = obj.Price;
				objFromDb.ProductImages = obj.ProductImages;
				//if (obj.ImageUrl != null)
				//{
				//	objFromDb.ImageUrl = obj.ImageUrl;
				//}
			}
		}
	}
}

