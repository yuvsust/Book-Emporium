using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookEmporiumWeb.Controllers
{
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();
            return View(coverTypes);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type is created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var coverTypeObj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (coverTypeObj == null)
            {
                return NotFound();
            }
            return View(coverTypeObj);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type is updated successfully";
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
            var coverTypeObj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (coverTypeObj == null)
            {
                return NotFound();
            }
            return View(coverTypeObj);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType obj)
        {
            var coverTypeObj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == obj.Id);
            if (coverTypeObj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(coverTypeObj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
