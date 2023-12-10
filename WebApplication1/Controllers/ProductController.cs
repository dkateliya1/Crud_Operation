using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;
using PagedList.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebDb _context;
        public ProductController(WebDb context)
        {
            _context = context;
        }

        public IActionResult Index(int? i)
        {
            var products = _context.Products.Include(navigationPropertyPath: "Category").ToPagedList(i ?? 1,3);
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }
        [NonAction]
        private void LoadCategories()
        {
            var cat = _context.Categories.ToList();
            ViewBag.cat = new SelectList(cat,"Id","Name");
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Update(int? id) 
        { 
            if(id != null)
            {
                NotFound();
            }
            LoadCategories();
            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product model) 
        {
            ModelState.Remove("Category");
            if(ModelState.IsValid)
            {
                _context.Products.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
             return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                NotFound();
            }
            LoadCategories();
            var product = _context.Products.Find(id);
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Product model)
        {
            _context.Products.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
