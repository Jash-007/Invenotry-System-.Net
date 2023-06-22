using demogsoft1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace demogsoft1.Controllers
{
    public class ItemController : Controller
    {
        gsoftDBEntities db = new gsoftDBEntities();
        // GET: Item
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DisplayItem()
        {
            List<mstItem> Ilist = db.mstItems.OrderByDescending(x=>x.ItemId).ToList();
            return View(Ilist);
        }
        [HttpGet]
        public ActionResult AddItem()
        {
            var prodlist = db.mstProducts.ToList();
            ViewBag.ProdList = new SelectList(prodlist, "ProductId", "ProductName");
            var deptlist = db.mstDepartments.ToList();
            ViewBag.DeptList = new SelectList(deptlist, "DepartmentId", "DepartmentName");
            var brdlist = db.mstBrands.ToList();
            ViewBag.BrdList = new SelectList(brdlist, "BrandId", "BrandName");
            var taxlist = db.mstTaxes.ToList();
            ViewBag.TaxList = new SelectList(taxlist, "TaxId", "TaxName");
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(mstItem item)
        {
            
           // mstProduct p = db.mstProducts.Find(db.mstProducts.Where(x => x.ProductName.Equals(Prodname, System.StringComparison.OrdinalIgnoreCase)));
           // item.ProductID = p.ProductId;
           // mstDepartment d = db.mstDepartments.Find(db.mstDepartments.Where(x => x.DepartmentName.Equals(Deptname, System.StringComparison.OrdinalIgnoreCase)));
           // item.DepartmentID = d.DepartmentId;
            //mstBrand b = db.mstBrands.Find(db.mstBrands.Where(x => x.BrandName.Equals(brname, System.StringComparison.OrdinalIgnoreCase)));
            //item.BrandId = b.BrandId;
            //mstTax t = db.mstTaxes.Find(db.mstTaxes.Where(x => x.TaxName.Equals(tname, System.StringComparison.OrdinalIgnoreCase)));
           // item.TaxId = t.TaxId;
            db.mstItems.Add(item);
            db.SaveChanges();
            return RedirectToAction("DisplayItem");
        }
        [HttpGet]
        public ActionResult DeleteItem(int Id)
        {
            mstItem t = db.mstItems.Where(x => x.ItemId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteItem(int Id, mstItem b)
        {
            db.mstItems.Remove(db.mstItems.Where(x => x.ItemId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("DisplayItem");
        }
        [HttpGet]
        public ActionResult EditItem(int Id)
        {
            mstItem b = db.mstItems.Where(x => x.ItemId == Id).SingleOrDefault();
            var prodlist = db.mstProducts.ToList();
            ViewBag.ProdList = new SelectList(prodlist, "ProductId", "ProductName");
            var deptlist = db.mstDepartments.ToList();
            ViewBag.DeptList = new SelectList(deptlist, "DepartmentId", "DepartmentName");
            var brdlist = db.mstBrands.ToList();
            ViewBag.BrdList = new SelectList(brdlist, "BrandId", "BrandName");
            var taxlist = db.mstTaxes.ToList();
            ViewBag.TaxList = new SelectList(taxlist, "TaxId", "TaxName");
            return View(b);
        }
        [HttpPost]
        public ActionResult EditTaxType(int Id, mstItem b)
        {
            mstItem t = db.mstItems.Where(x => x.ItemId == Id).SingleOrDefault();
            t.ItemName = b.ItemName;
            t.ShortName = b.ShortName;
            t.DiscountPrc = b.DiscountPrc;
            t.DiscountFromDate = b.DiscountFromDate;
            t.DiscountToDate = b.DiscountToDate;
            t.DefaultQty = b.DefaultQty;
            t.IsActive = b.IsActive;
            t.ProductID = b.ProductID;
            t.BrandId = b.BrandId;
            t.DepartmentID = b.DepartmentID;
            t.TaxId = b.TaxId;
            
            db.SaveChanges();
            return RedirectToAction("DisplayItem");
        }
        [HttpGet]
        public ActionResult DetailsItem(int Id)
        {

            mstItem bu = db.mstItems.Where(x => x.ItemId == Id).SingleOrDefault();
            return View(bu);
        }
    }
}