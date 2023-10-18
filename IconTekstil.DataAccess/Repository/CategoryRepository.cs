using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IconTekstil.DataAccess.Data;
using IconTekstil.DataAccess.Repository.IRepository;
using IconTekstil.Models;
using Microsoft.EntityFrameworkCore;

namespace IconTekstil.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private ApplicationDbContext _db;
		
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
			_db.Products.Include(u => u.Category).Include(u => u.CategoryId);
		}

		public void Update(Category obj)
		{
			_db.Categories.Update(obj);
		}


	}
}
