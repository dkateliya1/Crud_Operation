using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;
using PagedList.Mvc;


namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly WebDb _context;
        public CategoryController (WebDb context)
        {
            _context = context;
        }

        public IActionResult Index(int ? i)
        {
            var result = _context.Categories.ToPagedList(i ?? 1,3);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                var cat = new Category()
                {
                    Name = model.Name,
                };
                _context.Categories.Add(cat);   
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "empty fields cannot be submitted";
                return View();
            }
        }
        public IActionResult Delete(int id)
        {
            var cat = _context.Categories.SingleOrDefault(e => e.Id ==  id);
            _context.Categories.Remove(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var cat = _context.Categories.SingleOrDefault(e => e.Id == id);
            var result = new Category()
            {
                Name = cat.Name
            };
            return View(result);
        }
        [HttpPost]
        public IActionResult Update(Category model)
        {
            var cat = new Category()
            {
                Name = model.Name,
                Id = model.Id
            };
            _context.Categories.Update(cat);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
