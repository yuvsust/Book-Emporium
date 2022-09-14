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
            return View();
        }

        // GET
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategoryList = _unitOfWOrk.Category.GetAllAsync().Result.Select(u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = _unitOfWOrk.CoverType.GetAllAsync().Result.Select(u => new SelectListItem()
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
                productViewModel.Product = _unitOfWOrk.Product.GetFirstOrDefaultAsync(x => x.Id == id).Result;
                return View(productViewModel);
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

                    if(productViewModel.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    productViewModel.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (productViewModel.Product.Id == 0)
                {
                    _unitOfWOrk.Product.AddAsync(productViewModel.Product);
                    TempData["success"] = "Product is created successfully";
                }
                else
                {
                    _unitOfWOrk.Product.Update(productViewModel.Product);
                    TempData["success"] = "Product is updated successfully";
                }
                _unitOfWOrk.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWOrk.Product.GetAllAsync(includeProperties: "Category,CoverType");
            return Json(new
            {
                data = productList
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWOrk.Product.GetFirstOrDefaultAsync(x => x.Id == id).Result;
            if(obj is null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWOrk.Product.Remove(obj);
            _unitOfWOrk.Save();
            return Json(new { success = true, message = "Delete successfully" });
        }
        #endregion
    }
}
