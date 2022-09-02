﻿using BookEmporium.DataAccess.Data;
using BookEmporium.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookEmporiumWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
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
                _db.Categories.Add(obj);
                _db.SaveChangesAsync();
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
            var categoryObj = _db.Categories.FirstOrDefault(x => x.Id == id);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            var categoryObj = _db.Categories.FirstOrDefault(x => x.Id == id);
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
            var categoryObj = _db.Categories.FirstOrDefault(x => x.Id == obj.Id);
            if(categoryObj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(categoryObj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
