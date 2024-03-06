using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practice888.Models;
using Practice888.Vm;

namespace Practice888.Controllers
{
    public class SaleController : Controller
    {
        private readonly MasterDB db;
        private readonly IWebHostEnvironment h;
        public SaleController(MasterDB _db, IWebHostEnvironment _h)
        {
           db = _db;
            h = _h;
        }

        [HttpGet]
        public IActionResult Index(int ? id)
        {
            ViewData["CID"] = new SelectList(db.Categories, "CID", "CatName");
            VmSale osale = new VmSale();
            var osm=db.SaleMasters.Where(x=>x.SaleId==id).FirstOrDefault();
            if (osm != null) 
            { 
                osale.SaleId = osm.SaleId;
                osale.CustomerName=osm.CustomerName;
                osale.Gender= osm.Gender;
                var list=new List<VmSale.VmSaleDetail>();
                var listSD= db.SaleDetails.Where(x => x.SaleId == id).ToList();
                foreach(var oSD in listSD)
                {
                    var sd=new VmSale.VmSaleDetail();
                    sd.SaleDetailId = oSD.SaleDetailId;
                    sd.ProductName = oSD.ProductName;
                    sd.Date = oSD.Date;
                    sd.Photo = oSD.Photo;
                    sd.Price = oSD.Price;
                    list.Add(sd);
                }
                osale.SaleDetails= list;
            }
            osale=osale==null? new VmSale() :osale;
            ViewData["List"] = db.SaleMasters.ToList();
            ViewData["DList"] = db.SaleDetails.ToList();

            //var list = db.SaleMasters.ToList();
           // var liste = db.SaleMasters.Include(x=>x.SaleDetails).ToList();
            //foreach (var e in liste)
            //{
            //    db.Entry(e).Collection(x => x.SaleDetails).Load();
            //}
       
            if (db.SaleMasters.Count()>0)
            {
                ViewData["s"] = db.SaleDetails.Sum(x => x.Price);
                ViewData["a"] = db.SaleDetails.Average(x => x.Price);
                ViewData["mx"] = db.SaleDetails.Max(x => x.Price);
                ViewData["mn"] = db.SaleDetails.Min(x => x.Price);
                ViewData["c"] = db.SaleDetails.Count();
                //ViewData["G"] = db.SaleDetails.GroupBy(x => new { x.Price, x.ProductName })
                //    .OrderBy(c => c.Key.Price).ThenBy(c => c.Key.ProductName);
            }
            return View(osale);
        }
        [HttpPost]
        public async Task <IActionResult> Index(VmSale model, IFormFile[] img)
        {
            string[] fn = new string[model.ProductName.Length];
            for(var i=0;i<model.ProductName.Length;i++)
            {
                if (img[i] != null)
                {
                    fn[i] = img[i].FileName;
                    var fp = Path.Combine(h.WebRootPath, "Pic", fn[i]);
                    using(var s=new FileStream(fp,FileMode.Create))
                    {
                        await img[i].CopyToAsync(s);
                    }
                }
            }
            var osalmas = db.SaleMasters.Find(model.SaleId);
            if (osalmas == null)
            {
                osalmas = new SaleMaster();
                osalmas.SaleId=model.SaleId;
                osalmas.CID=model.CID;
                osalmas.CustomerName= model.CustomerName;
                osalmas.Gender= model.Gender;
                db.SaleMasters.Add(osalmas);
            }
            else
            {
                osalmas.SaleId = model.SaleId;
                osalmas.CID = model.CID;
                osalmas.CustomerName = model.CustomerName;
                osalmas.Gender = model.Gender;
                var listSD = db.SaleDetails.Where(x => x.SaleId == model.SaleId).ToList();
                db.SaleDetails.RemoveRange(listSD);
            }

            db.SaveChanges();
            var listDet=new List<SaleDetail>();
            for (var i = 0; i < model.ProductName.Length; i++)
            {
                if (!string.IsNullOrEmpty(model.ProductName[i]))
                {
                    var sd=new SaleDetail();
                    sd.SaleId = osalmas.SaleId;
                    sd.ProductName= model.ProductName[i];
                    sd.Price = model.Price[i];
                    sd.Photo = (fn[i]!=null)? "/Pic/"+fn[i]:sd.Photo;
                    sd.Date = model.Date[i];
                    listDet.Add(sd);
                }
            }
            ViewData["CID"] = new SelectList(db.Categories, "CID", "CatName",osalmas.CID);
            db.SaleDetails.AddRange(listDet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var sm=(from o in db.SaleMasters where o.SaleId==id select o).FirstOrDefault();
            var sd = (from o in db.SaleDetails where o.SaleId == id select o).ToList();
            if(sm!=null && sd!=null)
            {
                db.SaleDetails.RemoveRange(sd);
                db.SaleMasters.Remove(sm);
                db.SaveChanges();   
            }
            return RedirectToAction("Index");
        }
    }
}
