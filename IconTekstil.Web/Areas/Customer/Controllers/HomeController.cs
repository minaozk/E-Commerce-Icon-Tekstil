﻿using System.Collections;
using System.Security.Claims;
using IconTekstil.DataAccess.Repository.IRepository;
using IconTekstil.Models;
using IconTekstil.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;



namespace IconTekstil.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{


			IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,ProductImages");
            return View(productList);
        }

		public IActionResult Details(int id)
		{
			ShoppingCart cart = new()
			{
				Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category,ProductImages"),
				Count = 1,
				ProductId = id
			};
			return View(cart);
		}
		[HttpPost]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			shoppingCart.Id = 0;
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			shoppingCart.ApplicationUserId = userId;

			ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId && u.ProductId == shoppingCart.ProductId);

			if (cartFromDb != null)
			{
				//shopping cart exists
				cartFromDb.Count += shoppingCart.Count;
				_unitOfWork.ShoppingCart.Update(cartFromDb);
				_unitOfWork.Save();
			}
			else
			{
				//add cart record
				_unitOfWork.ShoppingCart.Add(shoppingCart);
				_unitOfWork.Save();
				HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
				

				
			}
			TempData["success"] = "Sepete eklendi.";




			return RedirectToAction(nameof(Index));
		}

		public IActionResult Privacy()
		{
			return View();
		}

	}
}
