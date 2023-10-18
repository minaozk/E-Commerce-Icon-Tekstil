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
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		private ApplicationDbContext _db;
		
		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		
		}

	

	}
}
