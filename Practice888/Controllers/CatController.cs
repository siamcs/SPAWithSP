using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice888.Models;

namespace Practice888.Controllers
{
    public class CatController : Controller
    {
        private readonly MasterDB db;
      
        public CatController(MasterDB _db)
        {
            db = _db;
           
        }
        public IActionResult Index()
        {
            var c = db.Categories.FromSqlRaw("exec GC").ToList();
            return View(c);
        }

        public IActionResult Create( )
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category c)
        {
            db.Database.ExecuteSqlInterpolated($"IC {c.CID},{c.CatName}");
            return RedirectToAction("Index");   
        }

        public IActionResult Edit(int id)
        {
            Category? c = db.Categories.FirstOrDefault(c => c.CID == id);
            return View(c);
        }

        [HttpPost]
        public IActionResult Edit(Category c)
        {
            db.Database.ExecuteSqlInterpolated($"UC {c.CID},{c.CatName}");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            db.Database.ExecuteSqlInterpolated($"DC {id}");
            return RedirectToAction("Index");
        }
    }
}
