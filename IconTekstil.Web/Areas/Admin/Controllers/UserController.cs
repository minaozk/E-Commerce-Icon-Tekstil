﻿using IconTekstil.DataAccess.Data;
using IconTekstil.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IconTekstil.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly ApplicationDbContext _db;

		public UserController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}


		#region API CALLS

		[HttpGet]
		public IActionResult GetAll()
		{
			List<ApplicationUser> objUserList = _db.ApplicationUsers.ToList();
			return Json(new { data = objUserList });
		}


		[HttpDelete]
		public IActionResult Delete(int? id)
		{
		

			return Json(new { success = true, message = "Delete Successful" });
		}

		#endregion
	}
}
