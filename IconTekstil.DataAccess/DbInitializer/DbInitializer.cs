using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IconTekstil.DataAccess.Data;
using IconTekstil.DataAccess.DbIniitalizer;
using IconTekstil.Models;
using IconTekstil.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IconTekstil.DataAccess.DbInitalizer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationDbContext _db;

		public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_db = db;
		}

		public void Initialize()
		{
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				
			}

			if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "admin@icontekstil.com",
					Email = "admin@icontekstil.com",
					Name = "Admin",
					PhoneNumber = "82329129818",
					StreetAddress = "iRFAN BAŞTUĞ PAŞA",
					State = "Eyüp",
					PostalCode = "34394",
					City = "İstanbul"
				}, "Admin.123").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@icontekstil.com");
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}

			return;
			
		}
	}
}
