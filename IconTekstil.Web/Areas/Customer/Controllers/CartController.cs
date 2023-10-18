using System.Security.Claims;
using IconTekstil.DataAccess.Repository.IRepository;
using IconTekstil.Models;
using IconTekstil.Models.ViewModels;
using IconTekstil.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;



namespace IconTekstil.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVm { get; set; }


        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }



        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVm = new()
            {
                ShoppingCartList =
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new ()
            };

            IEnumerable<ProductImage> productImages = _unitOfWork.ProductImage.GetAll();

            foreach (var cart in ShoppingCartVm.ShoppingCartList)
            {
	            cart.Product.ProductImages = productImages.Where(u => u.ProductId == cart.Product.Id).ToList();
                cart.Price = GetPrice(cart);
                ShoppingCartVm.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVm);
        }

        public IActionResult Summary(ShoppingCartVM shoppingCartVm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVm = new()
            {
                ShoppingCartList =
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            ShoppingCartVm.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);


            ShoppingCartVm.OrderHeader.Name = ShoppingCartVm.OrderHeader.ApplicationUser.Name;
            ShoppingCartVm.OrderHeader.PhoneNumber = ShoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVm.OrderHeader.StreetAddress = ShoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVm.OrderHeader.City = ShoppingCartVm.OrderHeader.ApplicationUser.City;
            ShoppingCartVm.OrderHeader.State = ShoppingCartVm.OrderHeader.ApplicationUser.State;
            ShoppingCartVm.OrderHeader.PostalCode = ShoppingCartVm.OrderHeader.ApplicationUser.PostalCode;




            foreach (var cart in ShoppingCartVm.ShoppingCartList)
            {
                cart.Price = GetPrice(cart);
                ShoppingCartVm.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVm);
        }

        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVm.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties:"Product");

            ShoppingCartVm.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVm.OrderHeader.ApplicationUserId = userId;

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

            foreach (var cart in ShoppingCartVm.ShoppingCartList)
            {
                cart.Price = GetPrice(cart);
                ShoppingCartVm.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            //if (ShoppingCartVm.OrderHeader.ApplicationUser.CompanyId.GetValueOrDefault() == 0)
            //{
            //    ShoppingCartVm.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            //    ShoppingCartVm.OrderHeader.OrderStatus = SD.StatusPending;
            //}
            //else
            
                ShoppingCartVm.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCartVm.OrderHeader.OrderStatus = SD.StatusApproved;
            

            _unitOfWork.OrderHeader.Add(ShoppingCartVm.OrderHeader);
            _unitOfWork.Save();

          




            return RedirectToAction(nameof(OrderConfirmation), new {id=ShoppingCartVm.OrderHeader.Id});
        }

        public IActionResult OrderConfirmation(int id)
        {

           HttpContext.Session.Clear();
            return View(id);            
          
        }

        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked:true);
            if (cartFromDb.Count <= 1)
            {
	            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
				_unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId, tracked:true);

            HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
            _unitOfWork.Save();
			_unitOfWork.ShoppingCart.Remove(cartFromDb);
            
            
            return RedirectToAction("Index");
        }

        private double GetPrice(ShoppingCart shoppingCart)
        {
            return shoppingCart.Product.Price;
        }
    }
}
