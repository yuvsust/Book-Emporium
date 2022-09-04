using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models;
using BookEmporium.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookEmporiumWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWOrk;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWOrk, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWOrk = unitOfWOrk;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWOrk.Product.GetAll();
            return View(objProductList);
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWOrk.Category.GetAll().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = _unitOfWOrk.CoverType.GetAll().Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                //create
                return View(productViewModel);
            }
            else
            {
                //update
                return View();
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                if (file != null)
                {
                    var wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productViewModel.Product.ImageUrl = @"images\products\" + fileName + extension;
                }
                _unitOfWOrk.Product.Add(productViewModel.Product);
                _unitOfWOrk.Save();
                TempData["success"] = "Product is created successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWOrk.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new
            {
                data = productList
            });
        }
        #endregion
    }
}
