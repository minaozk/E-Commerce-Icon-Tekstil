using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IconTekstil.DataAccess.Data;
using IconTekstil.DataAccess.Repository.IRepository;
using IconTekstil.Models;

namespace IconTekstil.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private ApplicationDbContext _db;
		public ICategoryRepository Category { get; private set; }
		public IProductRepository Product { get; }
		public ICompanyRepository Company { get; }
		public IShoppingCartRepository ShoppingCart { get; }
		public IApplicationUserRepository ApplicationUser { get; }
		public IOrderDetailRepository OrderDetail { get; }
		public IOrderHeaderRepository OrderHeader { get; }
		public IProductImageRepository ProductImage { get; }

		public UnitOfWork(ApplicationDbContext db /*ICompanyRepository company*/, IOrderHeaderRepository orderHeader, IOrderDetailRepository orderDetail, IProductImageRepository productImage)
		{
			_db = db;
            OrderHeader = orderHeader;
            OrderDetail = orderDetail;
            ProductImage = new ProductImageRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
			ShoppingCart = new ShoppingCartRepository(_db);
			Company = new CompanyRepository(_db);
			Category = new CategoryRepository(_db);
			Product = new ProductRepository(_db);

		}

		public ICategoryRepository CategoryRepository { get; private set; }
		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
