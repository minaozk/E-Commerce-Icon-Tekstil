using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IconTekstil.DataAccess.Data;
using IconTekstil.DataAccess.Repository.IRepository;
using IconTekstil.Models;
using IconTekstil.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IconTekstil.DataAccess.Repository
{
	public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
	{
		private ApplicationDbContext _db;
		
		public ProductImageRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
			
		}

		public void Update(ProductImage obj)
		{
			_db.ProductImages.Update(obj);
		}


	}
}
