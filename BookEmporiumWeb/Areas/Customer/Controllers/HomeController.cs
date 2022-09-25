using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models.ViewModels;
using BookEmporiumWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookEmporiumWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.Product.GetAllAsync().Result;
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int id)
        {
            //var productDetails = _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Category,CoverType").Result;
            var shoppingCart = new ShoppingCartViewModel()
            {
                Product = _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id, includeProperties: "Category,CoverType").Result,
                Count = 1
            };
            return View(shoppingCart);
        }
    }
}