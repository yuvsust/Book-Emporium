using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models;
using BookEmporium.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookEmporiumWeb.Areas.Admin.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWOrk;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWOrk, IWebHostEnvironment webHostEnvironment)
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
            Company companyObj = new Company();
            if (id == null || id == 0)
            {
                //create
                return View(companyObj);
            }
            else
            {
                //update
                companyObj = _unitOfWOrk.Company.GetFirstOrDefaultAsync(x => x.Id == id).Result;
                return View(companyObj);
            }
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company companyObj)
        {
            if(ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    _unitOfWOrk.Company.AddAsync(companyObj);
                    TempData["success"] = "Company is created successfully";
                }
                else
                {
                    _unitOfWOrk.Company.Update(companyObj);
                    TempData["success"] = "Company is updated successfully";
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
            var companyList = _unitOfWOrk.Company.GetAllAsync().Result;
            return Json(new
            {
                data = companyList
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWOrk.Company.GetFirstOrDefaultAsync(x => x.Id == id).Result;
            if(obj is null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _unitOfWOrk.Company.Remove(obj);
            _unitOfWOrk.Save();
            return Json(new { success = true, message = "Delete successfully" });
        }
        #endregion
    }
}
