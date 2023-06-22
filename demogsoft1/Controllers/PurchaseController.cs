using demogsoft1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace demogsoft1.Controllers
{
    public class PurchaseController : Controller
    {
        gsoftDBEntities db=new gsoftDBEntities();
        // GET: Purchase
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PurchaseAccount()
        {
            List<trnPurchase> gplist = db.trnPurchases.OrderByDescending(x => x.PurchaseId).ToList();
            return View(gplist);
            
        }
        [HttpGet]
        public ActionResult AddPurchaseAccount() {
            var acclist = db.mstAccounts.ToList(); ;
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
            return View(); 
        }
        [HttpPost]
        public ActionResult AddPurchaseAccount(trnPurchase pur)
        {
            db.trnPurchases.Add(pur);
            var pid = pur.PurchaseId;
            
            db.SaveChanges();
            return RedirectToAction("AddPurchaseItem");
        }
        [HttpGet]
        public ActionResult DeletePurchaseAccount(int Id)
        {
            trnPurchase t = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeletePurchaseAccount(int Id, trnPurchase b)
        {

            //db.trnPurchases.ToList().ForEach(s => db.Entry(s.PurchaseId).State = EntityState.Deleted);
            var rem = (db.trnPurchases.Where(x => x.PurchaseId == Id).Include(f => f.trnPurchaseItems).FirstOrDefault());
            db.trnPurchases.Remove(rem);
         //  db.SaveChanges();
            return RedirectToAction("PurchaseAccount");
        }
        [HttpGet]
        public ActionResult DetailsPurchaseaccount(int Id)
        {
            var id = db.trnPurchaseItems.Where(x => x.PurchaseItemId == Id).SingleOrDefault();
            var pid = id.PurchaseId;
            var model1 = db.trnPurchaseItems.Include(m => m.trnPurchase).FirstOrDefault(m => m.PurchaseId == pid);

            return View(model1);
        }
        [HttpGet]
        public ActionResult DetailsPurchaseItem(int Id)
        {
        //    var id = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
        //  var pid = id.PurchaseId;
         //  var model1 = db.trnPurchases.Include(m => m.trnPurchaseItems).FirstOrDefault(m => m.PurchaseId == pid);
        var model2 = db.trnPurchases.Include(m => m.trnPurchaseItems).FirstOrDefault(m => m.PurchaseId == Id);

            return View(model2);
        }
        [HttpGet]
        public ActionResult EditPurchaseAccount(int Id)
        {
            trnPurchase b = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
            //  List<string> taxlist = db.mstTaxes.Select(x => x.TaxName).ToList();
            // ViewBag.BName = new SelectList(Blist);
            var acclist = db.mstAccounts.ToList(); 
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
           
            return View(b);
        }
        [HttpPost]
        public ActionResult EditPurchaseAccount(int Id, trnPurchase b)
        {
            trnPurchase t = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
            t.PurchaseType = b.PurchaseType;
            t.VoucherNo = b.VoucherNo;
            t.VoucherDate = b.VoucherDate;
            t.InvoiceType = b.InvoiceType;
            t.TotalAmount = b.TotalAmount;
            t.AccountId = b.AccountId;
            

           

            
            db.SaveChanges();
            return RedirectToAction("PurchaseAccount");
        }
        public ActionResult PurchaseItem()
        {
           List<trnPurchaseItem> gplist = db.trnPurchaseItems.OrderByDescending(x => x.PurchaseItemId).ToList();
     
           // List<trnPurchaseItem> purchaseList = GetPurchaseData(startDate, endDate);
            return View(gplist);
        }
        [HttpGet]
        public ActionResult AddPurchaseItem()
        {
            var purlist = db.trnPurchases.ToList(); 
            ViewBag.PurList = new SelectList(purlist, "PurchaseId", "VoucherNo");
            var itemlist = db.mstItems.ToList(); 
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            var taxlist = db.mstTaxes.ToList(); 
            ViewBag.TaxList = new SelectList(taxlist, "TaxId", "TaxName");
     
            return View();
        }
        [HttpPost]
        public ActionResult AddPurchaseItem(trnPurchaseItem pur)
        {
            db.trnPurchaseItems.Add(pur);
            var tid = pur.TaxId;
            mstTax b = db.mstTaxes.Where(x => x.TaxId == tid).SingleOrDefault();
            var pid = pur.PurchaseId;
            trnPurchase t=db.trnPurchases.Where(x=>x.PurchaseId== pid).SingleOrDefault();
            
       
            if (t.InvoiceType == "GST")
            {
                pur.IGSTPrc = 0;
                pur.SGSTPrc = b.SGSTPrc;
                pur.CGSTPrc = b.CGSTPrc;
                
               
            }
            else
            {
                pur.IGSTPrc = b.IGSTPrc;
                pur.SGSTPrc = 0;
                pur.CGSTPrc = 0;
               
               
            }
            pur.Amount = pur.Qty * pur.PurcRate;
            pur.IGSTAmount = pur.Amount * (pur.IGSTPrc / 100);
            pur.CGSTAmount = pur.Amount * (pur.CGSTPrc / 100);
            pur.SGSTAmount = pur.Amount * (pur.SGSTPrc / 100);
            pur.TotalAmt = ( pur.Amount + pur.IGSTAmount + pur.CGSTAmount + pur.SGSTAmount);
            pur.MRP = (pur.TotalAmt +(pur.Qty*200))/pur.Qty;
            pur.SalesRate = (pur.TotalAmt + (pur.Qty * 200))/ pur.Qty;
            pur.VoucherDate = t.VoucherDate;
            t.TotalAmount += pur.TotalAmt;
            db.trnPurchases.AddOrUpdate(t);
            //string tidIGST=db.mstTaxes.Find(tid.GetValueOrDefault(tid.IGSTPrc));
            // pur.IGSTPrc=db.mstTaxes.Where(x=>x.IGSTPrc==tid).FirstOrDefault();

            db.SaveChanges();
            return RedirectToAction("AddPurchaseItem");
        }
        [HttpGet]
        public ActionResult DeletePurchaseItem(int Id)
        {
            trnPurchaseItem t = db.trnPurchaseItems.Where(x => x.PurchaseItemId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeletePurchaseItem(int Id, trnPurchaseItem b)
        {
            db.trnPurchaseItems.Remove(db.trnPurchaseItems.Where(x => x.PurchaseItemId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("PurchaseItem");
        }
        [HttpGet]
        public ActionResult EditPurchaseItem(int Id)
        {
            trnPurchaseItem b = db.trnPurchaseItems.Where(x => x.PurchaseItemId == Id).SingleOrDefault();
            //  List<string> taxlist = db.mstTaxes.Select(x => x.TaxName).ToList();
            // ViewBag.BName = new SelectList(Blist);
            var purlist = db.trnPurchases.ToList(); ;
            ViewBag.PurList = new SelectList(purlist, "PurchaseId", " PurchaseType");
            var itemlist = db.mstItems.ToList(); ;
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            var taxlist = db.mstTaxes.ToList(); ;
            ViewBag.TaxList = new SelectList(taxlist, "TaxId", "TaxName");
            return View(b);
        }
        [HttpPost]
        public ActionResult EditPurchaseItem(int Id, trnPurchaseItem b)
        {
            trnPurchaseItem t = db.trnPurchaseItems.Where(x => x.PurchaseItemId == Id).SingleOrDefault();
            t.Qty = b.Qty;
            t.PurcRate = b.PurcRate;
            t.PurchaseId = b.PurchaseId;
            t.ItemId = b.ItemId;
            t.TaxId = b.TaxId;
            var tid = t.TaxId;
            mstTax s = db.mstTaxes.Where(x => x.TaxId == tid).SingleOrDefault();
            var pid = t.PurchaseId;
            trnPurchase p = db.trnPurchases.Where(x => x.PurchaseId == pid).SingleOrDefault();

            if (p.InvoiceType == "GST")
            {
                t.IGSTPrc = 0;
                t.SGSTPrc = s.SGSTPrc;
                t.CGSTPrc = s.CGSTPrc;


            }
            else
            {
                t.IGSTPrc = s.IGSTPrc;
                t.SGSTPrc = 0;
                t.CGSTPrc = 0;


            }
            t.Amount = t.Qty * t.PurcRate;
            t.IGSTAmount = t.Amount * (t.IGSTPrc / 100);
            t.CGSTAmount = t.Amount * (t.CGSTPrc / 100);
            t.SGSTAmount = t.Amount * (t.SGSTPrc / 100);
            t.TotalAmt = (t.Amount + t.IGSTAmount + t.CGSTAmount + t.SGSTAmount);
            t.MRP = (t.TotalAmt + (t.Qty * 200)) / t.Qty;
            t.SalesRate = (t.TotalAmt + (t.Qty * 200)) / t.Qty;
            db.SaveChanges();
            return RedirectToAction("PurchaseItem");
        }
        [HttpGet]
        public ActionResult GetPurchaseDate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetPurchaseDate(DateTime startdate,DateTime enddate)
        {
            
            List<trnPurchaseItem> purchaseList = GetPurchasePeriod(startdate,enddate);
            return View("PurchaseItem",purchaseList);
        }
        
        public List<trnPurchaseItem> GetPurchasePeriod(DateTime startdate,DateTime enddate)
        { 
            
            List<trnPurchaseItem> purchaseList = db.trnPurchaseItems
           .Where(p => p.VoucherDate >= startdate && p.VoucherDate <= enddate)
           .ToList();
            return purchaseList;
        }
        [HttpGet]
        public ActionResult GetMonth()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetMonth(DateTime month)
        {
            List<trnPurchaseItem> purList = GetSaleMonth(month);
            return View("PurchaseItem", purList);
        }
        public List<trnPurchaseItem> GetSaleMonth(DateTime month)
        {
            var purData = db.trnPurchaseItems
           .Where(pur => pur.VoucherDate.Month == month.Month)
           .ToList();
            return purData;
        }
        [HttpGet]
        public ActionResult GetYear()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetYear(DateTime year)
        {
            List<trnPurchaseItem> purList = GetYearSale(year);
            return View("PurchaseList", purList);
        }
        public List<trnPurchaseItem> GetYearSale(DateTime year)
        {
            var purData = db.trnPurchaseItems
           .Where(pur => pur.VoucherDate.Year == year.Year)
           .ToList();
            return purData;
        }
        [HttpGet]
        public ActionResult PurchaseTypeReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PurchaseTypereport(string type)
        {
            //List<trnPurchase> purlist = db.trnPurchases.OrderByDescending(x => x.PurchaseId).ToList();
           // var type=db.trnPurchases.Where(p=>p.PurchaseType=="Pur")
            List<trnPurchase> typelist= reportList(type);
            
             return View("PurchaseAccount",typelist);
        }
        public List<trnPurchase> reportList(string type)
        {
            List<trnPurchase> typelist;
            if (type == "Pur")
            {
                typelist = db.trnPurchases.Where(p => p.PurchaseType == "Pur").ToList();
            }
            else if (type == "PurRet")
            {
                typelist = db.trnPurchases.Where(p => p.PurchaseType == "PurRet").ToList();
            }
            else
            {
                typelist = db.trnPurchases.ToList();
            }
            return typelist;
        }
        public ActionResult StockReport()
        {
            List<trnPurchaseItem> gplist = db.trnPurchaseItems.OrderByDescending(x => x.PurchaseItemId).ToList();
            return View(gplist);
        }
        public ActionResult combpur()
        {
            var puracc = db.trnPurchases.FirstOrDefault();
            var puritems = db.trnPurchaseItems.FirstOrDefault();
            var com = new Combinedpurchase
            {
                trnPurchase = puracc,
                item = puritems
            };
            foreach (trnPurchaseItem puritem in db.trnPurchaseItems) {
                var tid = puritem.TaxId;
                mstTax t = db.mstTaxes.Where(x => x.TaxId == tid).SingleOrDefault();

                puritem.SGSTPrc = t.SGSTPrc;
                puritem.CGSTPrc = t.CGSTPrc;
                puritem.IGSTPrc = t.IGSTPrc;
                puritem.Amount = puritem.Qty * puritem.PurcRate;
                puritem.IGSTAmount = puritem.Amount * (puritem.IGSTPrc / 100);
                puritem.CGSTAmount = puritem.Amount * (puritem.CGSTPrc / 100);
                puritem.SGSTAmount = puritem.Amount * (puritem.SGSTPrc / 100);
                puritem.TotalAmt = (puritem.Amount + puritem.IGSTAmount + puritem.CGSTAmount + puritem.SGSTAmount);
                puritem.MRP = (puritem.TotalAmt + (puritem.Qty * 200)) / puritem.Qty;
                puritem.SalesRate = (puritem.TotalAmt + (puritem.Qty * 200)) / puritem.Qty;
                db.trnPurchaseItems.Add(puritem); }
            db.trnPurchases.Add(puracc);
            db.SaveChanges();
            return View(com);
        }
        [HttpGet]
        public ActionResult AddPurchaseInvoice()
        {
            var acclist = db.mstAccounts.ToList(); 
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
            var purlist = db.trnPurchases.ToList();
            ViewBag.PurList = new SelectList(purlist, "PurchaseId", "VoucherNo");
            var itemlist = db.mstItems.ToList();
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            var taxlist = db.mstTaxes.ToList();
            ViewBag.TaxList = new SelectList(taxlist, "TaxId", "TaxName");
            var model = new demogsoft1.Models.trnPurchase();
            model.trnPurchaseItems.Add(new demogsoft1.Models.trnPurchaseItem());
            return View(model);
            
        }
        [HttpPost]
        public ActionResult AddPurchaseInvoice(trnPurchase pur)
        {
            db.trnPurchases.Add(pur);
            // db.trnPurchaseItems.Add(pur);
            // var model2 = db.trnPurchases.Include(m => m.trnPurchaseItems).FirstOrDefault(m => m.PurchaseId == pur.PurchaseId);

            foreach (var item in pur.trnPurchaseItems)
            {
                item.PurchaseId = pur.PurchaseId;
                item.TaxId = item.TaxId;
                item.Qty = item.Qty;
                item.PurcRate = item.PurcRate;
                item.ItemId = item.ItemId;
                item.MRP = item.MRP;
                item.SalesRate = item.SalesRate;
                mstTax b = db.mstTaxes.Where(x => x.TaxId == item.TaxId).SingleOrDefault();
 
                if (pur.InvoiceType == "GST")
               {
                    item.IGSTPrc = 0;
                    item.SGSTPrc = b.SGSTPrc;
                    item.CGSTPrc = b.CGSTPrc;


                }
           else
                {
                    item.IGSTPrc = b.IGSTPrc;
                    item.SGSTPrc = 0;
                    item.CGSTPrc = 0;


                }
             item.Amount = item.Qty * item.PurcRate;
                item.IGSTAmount = item.Amount * (item.IGSTPrc / 100);
                item.CGSTAmount = item.Amount * (item.CGSTPrc / 100);
                item.SGSTAmount = item.Amount * (item.SGSTPrc / 100);
                item.TotalAmt = (item.Amount + item.IGSTAmount + item.CGSTAmount + item.SGSTAmount);
               // item.MRP = (item.TotalAmt + (item.Qty * 200)) / item.Qty;
               // item.SalesRate = (item.TotalAmt + (item.Qty * 200)) / item.Qty;
                item.VoucherDate = pur.VoucherDate;
                pur.TotalAmount += item.TotalAmt;
                 db.trnPurchaseItems.Add(item);
                db.trnPurchases.AddOrUpdate(pur);
                //db.trnPurchaseItems.AddOrUpdate(item);
          
        }
            
        
            
            db.SaveChanges();
            return RedirectToAction("PurchaseAccount");
        }
        [HttpGet]
        public ActionResult EditInvoice(int Id)
        {
            trnPurchase b = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
            //  List<string> taxlist = db.mstTaxes.Select(x => x.TaxName).ToList();
            // ViewBag.BName = new SelectList(Blist);
            var acclist = db.mstAccounts.ToList();
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
            var purlist = db.trnPurchases.ToList();
            ViewBag.PurList = new SelectList(purlist, "PurchaseId", "VoucherNo");
            var itemlist = db.mstItems.ToList();
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            var taxlist = db.mstTaxes.ToList();
            ViewBag.TaxList = new SelectList(taxlist, "TaxId", "TaxName");
            // var model = new demogsoft1.Models.trnPurchase();
            // model.trnPurchaseItems.(new demogsoft1.Models.trnPurchaseItem());
            //var model2 = db.trnPurchases.Include(m => m.trnPurchaseItems).FirstOrDefault(m => m.PurchaseId == Id);
            //ViewBag.ItemList = new SelectList(db.trnPurchaseItems, "PurchaseItemId", "TaxId");


            return View(b);
           
        }
        [HttpPost]
        public ActionResult EditInvoice(int Id,trnPurchase pur)
        {
            trnPurchase t = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
            t.PurchaseType = pur.PurchaseType;
            t.VoucherNo = pur.VoucherNo;
            t.VoucherDate = pur.VoucherDate;
            t.InvoiceType = pur.InvoiceType;
            t.TotalAmount = pur.TotalAmount;
            t.AccountId = pur.AccountId;
            
            foreach (var item in pur.trnPurchaseItems)
            {
               item.Qty = item.Qty;
                item.PurcRate = item.PurcRate;
                item.PurchaseId = item.PurchaseId;
                item.ItemId = item.ItemId;
                item.TaxId = item.TaxId;
                var tid = item.TaxId;
                mstTax b = db.mstTaxes.Where(x => x.TaxId == tid).SingleOrDefault();
                if (pur.InvoiceType == "GST")
                {
                    item.IGSTPrc = 0;
                    item.SGSTPrc = b.SGSTPrc;
                    item.CGSTPrc = b.CGSTPrc;


                }
                else
                {
                    item.IGSTPrc = b.IGSTPrc;
                    item.SGSTPrc = 0;
                    item.CGSTPrc = 0;


                }
                item.Amount = item.Qty * item.PurcRate;
                item.IGSTAmount = item.Amount * (item.IGSTPrc / 100);
                item.CGSTAmount = item.Amount * (item.CGSTPrc / 100);
                item.SGSTAmount = item.Amount * (item.SGSTPrc / 100);
                item.TotalAmt = (item.Amount + item.IGSTAmount + item.CGSTAmount + item.SGSTAmount);
                // item.MRP = (item.TotalAmt + (item.Qty * 200)) / item.Qty;
                // item.SalesRate = (item.TotalAmt + (item.Qty * 200)) / item.Qty;
                item.VoucherDate = pur.VoucherDate;
                pur.TotalAmount += item.TotalAmt;
                db.trnPurchaseItems.Add(item);
                db.trnPurchases.AddOrUpdate(pur);
            }
            db.SaveChanges();
            return RedirectToAction("PurchaseAccount");
        }
    }
}
