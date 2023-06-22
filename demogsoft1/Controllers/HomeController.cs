using demogsoft1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demogsoft1.Controllers
{
    public class HomeController : Controller
    {
        gsoftDBEntities db=new gsoftDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult BrandList()
        {
            List<mstBrand> Brandlist = db.mstBrands.OrderByDescending(x => x.BrandId).ToList();
            return View(Brandlist);
        }
        [HttpGet]
        public ActionResult AddBrand()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBrand(mstBrand brand)
        {
            db.mstBrands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("BrandList");
        }
        [HttpGet]
        public ActionResult DeleteBrand(int Id)
        {
            mstBrand t = db.mstBrands.Where(x => x.BrandId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteBrand(int Id, mstBrand b)
        {
            db.mstBrands.Remove(db.mstBrands.Where(x => x.BrandId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("BrandList");
        }
            public ActionResult DepartmentList()
        {
            List<mstDepartment> Deplist = db.mstDepartments.OrderByDescending(x => x.DepartmentId).ToList();
            return View(Deplist);
        }
        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(mstDepartment dept)
        {
            db.mstDepartments.Add(dept);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }
        [HttpGet]
        public ActionResult DeleteDepartment(int Id)
        {
            mstDepartment t = db.mstDepartments.Where(x => x.DepartmentId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteDepartment(int Id, mstDepartment b)
        {
            db.mstDepartments.Remove(db.mstDepartments.Where(x => x.DepartmentId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }
            public ActionResult ProductList()
        {
            List<mstProduct> Prodlist = db.mstProducts.OrderByDescending(x => x.ProductId).ToList();
            return View(Prodlist);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(mstProduct product)
        {
            db.mstProducts.Add(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
        [HttpGet]
        public ActionResult DeleteProduct(int Id)
        {
            mstProduct t = db.mstProducts.Where(x => x.ProductId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int Id, mstProduct b)
        {
            db.mstProducts.Remove(db.mstProducts.Where(x => x.ProductId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
            public ActionResult TaxTypeList()
        {
            List<mstTax> Taxlist = db.mstTaxes.OrderByDescending(x => x.TaxId).ToList();
            return View(Taxlist);
        }
        [HttpGet]
        public ActionResult AddTaxType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTaxType(mstTax tax)
        {
            db.mstTaxes.Add(tax);
            var s = tax.TaxPrc / 2;
            tax.SGSTPrc = s;
            tax.CGSTPrc = s;
            tax.IGSTPrc = tax.TaxPrc;
            db.SaveChanges();
            return RedirectToAction("TaxTypeList");
        }
        [HttpGet]
        public ActionResult DeleteTax(int Id)
        {
            mstTax t = db.mstTaxes.Where(x => x.TaxId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteTax(int Id, mstTax b)
        {
            db.mstTaxes.Remove(db.mstTaxes.Where(x => x.TaxId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("TaxTypeList");
        }
        [HttpGet]
        public ActionResult EditTaxType(int Id)
        {
            mstTax b = db.mstTaxes.Where(x => x.TaxId == Id).SingleOrDefault();
          //  List<string> taxlist = db.mstTaxes.Select(x => x.TaxName).ToList();
           // ViewBag.BName = new SelectList(Blist);
            return View(b);
        }
        [HttpPost]
        public ActionResult EditTaxType(int Id, mstTax b)
        {
            mstTax t = db.mstTaxes.Where(x => x.TaxId == Id).SingleOrDefault();
            t.TaxName = b.TaxName;
            t.SGSTPrc = b.SGSTPrc;
            t.CGSTPrc = b.CGSTPrc;
            t.IGSTPrc = b.IGSTPrc;
            db.SaveChanges();
            return RedirectToAction("TaxTypeList");
        }
        public ActionResult StateList()
        {
            List<mstState> stlist = db.mstStates.OrderByDescending(x => x.StateId).ToList();
            return View(stlist);
        }
        [HttpGet]
        public ActionResult AddState()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddState(mstState st)
        {
            db.mstStates.Add(st);
            db.SaveChanges();
            return RedirectToAction("StateList");
        }
    
            public ActionResult BsgrGroupList()
        {
            List<mstBsgrGroup> gplist = db.mstBsgrGroups.OrderByDescending(x => x.BsgrId).ToList();
            return View(gplist);
        }
        [HttpGet]
        public ActionResult AddBsgrGroup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBsgrGroup(mstBsgrGroup gp)
        {
            db.mstBsgrGroups.Add(gp);
            db.SaveChanges();
            return RedirectToAction("BsgrGroupList");
        }
        [HttpGet]
        public ActionResult DeleteGroup(int Id)
        {
            mstBsgrGroup t = db.mstBsgrGroups.Where(x => x.BsgrId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteGroup(int Id, mstBsgrGroup b)
        {
            db.mstBsgrGroups.Remove(db.mstBsgrGroups.Where(x => x.BsgrId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("BsgrGroupList");
        }
            public ActionResult CompanyList()
        {
            List<mstCompany> gplist = db.mstCompanies.OrderByDescending(x => x.CompanyId).ToList();
            return View(gplist);
        }
        [HttpGet]
        public ActionResult AddCompany()
        {
            var statelist = db.mstStates.ToList();
            ViewBag.StateList = new SelectList(statelist, "StateId", "StateName");
            
            return View();
        }
        [HttpPost]
        public ActionResult AddCompany(mstCompany cp)
        {
            db.mstCompanies.Add(cp);
            db.SaveChanges();
            return RedirectToAction("CompanyList");
        }
    }
}