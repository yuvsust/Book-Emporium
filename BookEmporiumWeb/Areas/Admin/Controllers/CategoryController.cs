using BookEmporium.DataAccess.Data;
using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookEmporiumWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAllAsync();
            return View(categoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }
        
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Category.AddAsync(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category is created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryObj = _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
            if (categoryObj == null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category is updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryObj = _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
            if (categoryObj == null)
            {
                return NotFound();
            }
            return View(categoryObj);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            var categoryObj = _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == obj.Id);
            if(categoryObj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(categoryObj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
