using demogsoft1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace demogsoft1.Controllers
{
    public class SalesController : Controller
    {
        gsoftDBEntities db = new gsoftDBEntities();
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SalesList()
        {
            List<trnSale> gplist = db.trnSales.OrderByDescending(x => x.SalesId).ToList();
            return View(gplist);
        }
        [HttpGet]
        public ActionResult AddSaleAccount()
        {
            var acclist = db.mstAccounts.ToList();
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
            return View();
        }
        [HttpPost]
        public ActionResult AddSaleAccount(trnSale sa)
        {
            db.trnSales.Add(sa);
          
            db.SaveChanges();
            return RedirectToAction("AddSalesItem");
        }
        [HttpGet]
        public ActionResult DeleteSalesAccount(int Id)
        {
            trnSale t = db.trnSales.Where(x => x.SalesId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteSalesAccount(int Id, trnSale b)
        {
            db.trnSales.Remove(db.trnSales.Where(x => x.SalesId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("SalesList");
        }
        [HttpGet]
        public ActionResult EditSalesAccount(int Id)
        {
            trnSale b = db.trnSales.Where(x => x.SalesId == Id).SingleOrDefault();
            //  List<string> taxlist = db.mstTaxes.Select(x => x.TaxName).ToList();
            // ViewBag.BName = new SelectList(Blist);
            var acclist = db.mstAccounts.ToList();
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");

            return View(b);
        }
        [HttpPost]
        public ActionResult EditSalesAccount(int Id, trnSale b)
        {
            trnSale t = db.trnSales.Where(x => x.SalesId == Id).SingleOrDefault();
            t.SalesType = b.SalesType;
            t.VoucherNo = b.VoucherNo;
            t.VoucherDate = b.VoucherDate;
            t.InvoiceType = b.InvoiceType;
            t.NetAmt = b.NetAmt;
            t.Remarks = b.Remarks;
            t.SGSTAmt = b.SGSTAmt;
            t.CGSTAmt = b.CGSTAmt;
            t.IGSTAmt = b.IGSTAmt;
            t.AccountId = b.AccountId;
            db.SaveChanges();
            return RedirectToAction("SalesList");
        }
        [HttpGet]
        public ActionResult DetailsSalesAccount(int Id)
        {
            var id = db.trnSalesItems.Where(x => x.SalesItemId == Id).SingleOrDefault();
            var pid = id.SalesId;
            var model1 = db.trnSalesItems.Include(m => m.trnSale).FirstOrDefault(m => m.SalesId == pid);

            return View(model1);
        }
        [HttpGet]
        public ActionResult DetailsSalesItems(int Id)
        {
            var model2 = db.trnSales.Include(m => m.trnSalesItems).FirstOrDefault(m => m.SalesId == Id);

            return View(model2);
        }
        [HttpGet]
        public ActionResult SalesItemList()
        {
            List<trnSalesItem> gplist = db.trnSalesItems.OrderByDescending(x => x.SalesItemId).ToList();
            return View(gplist);
        }
        [HttpGet]
        public ActionResult AddSalesItem()
        {
           
            var salelist = db.trnSales.ToList();
            ViewBag.SaleList = new SelectList(salelist, "SalesId", "VoucherNo");
            var itemlist = db.mstItems.ToList();
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            return View();
        }
        [HttpPost]
        public ActionResult AddSalesitem(trnSalesItem si)
        {
            db.trnSalesItems.Add(si);
            var itId = si.ItemId;
            var sid = si.SalesId;
            trnSale s = db.trnSales.Where(x => x.SalesId == sid).FirstOrDefault();
            mstItem i= db.mstItems.Where(x=>x.ItemId== itId).FirstOrDefault();
            trnPurchaseItem t= db.trnPurchaseItems.Where(x=>x.ItemId ==itId).FirstOrDefault();
            if (s.SalesType == "Sales")
            {
                if (t.Qty >= si.Qty)
                {
                    si.MRP = t.MRP;
                    si.SalesRate = t.SalesRate;
                    si.Amount = si.Qty * si.SalesRate;
                    si.DiscAmt = si.Amount * (si.DIscPrc / 100);
                    si.NetAmt = si.Amount - si.DiscAmt;
                    si.TaxId = t.TaxId;
                    si.SGSTPrc = t.SGSTPrc;
                    si.CGSTPrc = t.CGSTPrc;
                    si.IGSTPrc = t.IGSTPrc;
                    si.IGSTAmount = si.Qty * t.IGSTAmount;
                    si.SGSTAmount = si.Qty * t.SGSTAmount;
                    si.CGSTAmount = si.Qty * t.CGSTAmount;
                    si.TotalAmt = si.NetAmt + si.IGSTAmount + si.CGSTAmount + si.SGSTAmount;
                    si.VoucherDate = s.VoucherDate;
                    t.Qty = t.Qty - si.Qty;
                    db.trnPurchaseItems.AddOrUpdate(t);
                    s.SGSTAmt += si.SGSTAmount;
                    s.CGSTAmt += si.CGSTAmount;
                    s.IGSTAmt += si.IGSTAmount;
                    s.NetAmt += si.NetAmt;
                    db.trnSales.AddOrUpdate(s);
                }
                else
                {
                    return RedirectToAction("SalesItemList");
                }

               
            }
            else if (s.SalesType == "SalesRet")
            {
                si.MRP = t.MRP;
                si.SalesRate = t.SalesRate;
                si.Amount = si.Qty * si.SalesRate;
                si.DiscAmt = si.Amount * (si.DIscPrc / 100);
                si.NetAmt = si.Amount - si.DiscAmt;
                si.TaxId = t.TaxId;
                si.SGSTPrc = t.SGSTPrc;
                si.CGSTPrc = t.CGSTPrc;
                si.IGSTPrc = t.IGSTPrc;
                si.IGSTAmount = si.Qty * t.IGSTAmount;
                si.SGSTAmount = si.Qty * t.SGSTAmount;
                si.CGSTAmount = si.Qty * t.CGSTAmount;
                si.TotalAmt = si.NetAmt + si.IGSTAmount + si.CGSTAmount + si.SGSTAmount;
                si.VoucherDate = s.VoucherDate;
                t.Qty = t.Qty + si.Qty;
                db.trnPurchaseItems.AddOrUpdate(t);
                s.SGSTAmt += si.SGSTAmount;
                s.CGSTAmt += si.CGSTAmount;
                s.IGSTAmt += si.IGSTAmount;
                s.NetAmt += si.NetAmt;
                db.trnSales.AddOrUpdate(s);
                
            }
            db.SaveChanges();
            return RedirectToAction("AddSalesItem");
        }
        [HttpGet]
        public ActionResult DeleteSalesItem(int Id)
        {
            trnSalesItem t = db.trnSalesItems.Where(x => x.SalesItemId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteSalesItem(int Id, trnSale b)
        {
            db.trnSalesItems.Remove(db.trnSalesItems.Where(x => x.SalesItemId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("SalesItemList");
        }
        [HttpGet]
        public ActionResult EditSalesItem(int Id)
        {
            trnSalesItem b = db.trnSalesItems.Where(x => x.SalesItemId == Id).SingleOrDefault();
         
            var salelist = db.trnSales.ToList();
            ViewBag.SaleList = new SelectList(salelist, "SalesId", "SalesType");
            var itemlist = db.mstItems.ToList();
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            return View(b);
        }
        [HttpPost]
        public ActionResult EditSalesItem(int Id, trnSalesItem b)
        {
            trnSalesItem t = db.trnSalesItems.Where(x => x.SalesItemId == Id).SingleOrDefault();
            t.SalesId=b.SalesId;
            t.ItemId = b.ItemId;
            var itId = b.ItemId;
            var sid = b.SalesId;
            trnSale s = db.trnSales.Where(x => x.SalesId == sid).FirstOrDefault();
            mstItem i = db.mstItems.Where(x => x.ItemId == itId).FirstOrDefault();
            trnPurchaseItem y = db.trnPurchaseItems.Where(x => x.ItemId == itId).FirstOrDefault();
            
            t.Qty = b.Qty;
            if (y.Qty >= t.Qty)
            {
                t.MRP = y.MRP;
                t.SalesRate = y.SalesRate;
                t.Amount = t.Qty * t.SalesRate;
                t.DiscAmt = t.Amount * (b.DIscPrc / 100);
                t.NetAmt = t.Amount - t.DiscAmt;
                t.TaxId = y.TaxId;
                t.SGSTPrc = y.SGSTPrc;
                t.CGSTPrc = y.CGSTPrc;
                t.IGSTPrc = y.IGSTPrc;
                t.IGSTAmount = t.Qty * y.IGSTAmount;
                t.SGSTAmount = t.Qty * y.SGSTAmount;
                t.CGSTAmount = t.Qty * y.CGSTAmount;
                t.TotalAmt = t.NetAmt + t.IGSTAmount + t.CGSTAmount + t.SGSTAmount;
                t.VoucherDate = s.VoucherDate;
                y.Qty = y.Qty - t.Qty;
                db.trnPurchaseItems.AddOrUpdate(y);
                s.SGSTAmt += t.SGSTAmount;
                s.CGSTAmt += t.CGSTAmount;
                s.IGSTAmt += t.IGSTAmount;
                s.NetAmt += t.NetAmt;
                db.trnSales.AddOrUpdate(s);
            }
            else
            {
                return RedirectToAction("SalesItemList");
            }

            db.SaveChanges();
            return RedirectToAction("SalesItemList");
        }
        [HttpGet]
        public ActionResult GetSalesDate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetSalesDate(DateTime startdate, DateTime enddate)
        {
            List<trnSalesItem> saleList = GetSalePeriod(startdate,enddate);
            return View("SalesItemList", saleList);
        }

        public List<trnSalesItem> GetSalePeriod(DateTime startdate, DateTime enddate)
        {
            List<trnSalesItem> saleList = db.trnSalesItems
           .Where(p => p.VoucherDate >= startdate && p.VoucherDate <= enddate)
           .ToList();
            return saleList;
        }
        
        [HttpGet]
        public ActionResult GetMonth()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetMonth(DateTime month)
        {
           List<trnSalesItem> saleList = GetSaleMonth(month);
            return View("SalesItemList",saleList);
        }
        public List<trnSalesItem> GetSaleMonth(DateTime month)
        {
            var salesData = db.trnSalesItems
           .Where(sale => sale.VoucherDate.Month == month.Month)
           .ToList();
            return salesData;
        }
        [HttpGet]
        public ActionResult GetYear()
        {

            return View();
        }
        [HttpPost]
        public ActionResult GetYear(DateTime year)
        {
            List<trnSalesItem> saleList = GetYearSale(year);
            return View("SalesItemList", saleList);
        }
        public List<trnSalesItem>  GetYearSale(DateTime year)
        {
            var salesData = db.trnSalesItems
           .Where(sale => sale.VoucherDate.Year == year.Year)
           .ToList();
            return salesData;
        }
        [HttpGet]
        public ActionResult SalesTypeReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SalesTypereport(string type)
        {
            //List<trnPurchase> purlist = db.trnPurchases.OrderByDescending(x => x.PurchaseId).ToList();
            // var type=db.trnPurchases.Where(p=>p.PurchaseType=="Pur")
            List<trnSale> typelist = reportList(type);

            return View("SalesList", typelist);
        }
        public List<trnSale> reportList(string type)
        {
            List<trnSale> typelist;
            if (type == "Sales")
            {
                typelist = db.trnSales.Where(p => p.SalesType == "Sales").ToList();
            }
            else if (type == "SalesRet")
            {
                typelist = db.trnSales.Where(p => p.SalesType == "SalesRet").ToList();
            }
            else
            {
                typelist = db.trnSales.ToList();
            }
            return typelist;
        }
        [HttpGet]
        public ActionResult AddSalesInvoice()
        {
            var acclist = db.mstAccounts.ToList();
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
            var salelist = db.trnSales.ToList();
            ViewBag.SaleList = new SelectList(salelist, "SalesId", "VoucherNo");
            var itemlist = db.mstItems.ToList();
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            var model = new demogsoft1.Models.trnSale();
            model.trnSalesItems.Add(new demogsoft1.Models.trnSalesItem());
            return View(model);
        }
        [HttpPost]
        public ActionResult AddSalesInvoice(trnSale sale)
        {
            sale.NetAmt = 0;
            sale.CGSTAmt = 0;
            sale.SGSTAmt = 0;
            sale.IGSTAmt = 0;
            db.trnSales.Add(sale);
            if (sale.SalesType == "Sales")
            {
                foreach (var si in sale.trnSalesItems)
                {
                    si.SalesId = sale.SalesId;
                    si.ItemId = si.ItemId;
                    var itId = si.ItemId;
                    si.Qty = si.Qty;
                    si.DIscPrc = si.DIscPrc;

                    mstItem i = db.mstItems.Where(x => x.ItemId == itId).FirstOrDefault();
                    trnPurchaseItem t = db.trnPurchaseItems.Where(x => x.ItemId == itId).FirstOrDefault();

                    if (t.Qty >= si.Qty)
                    {
                        si.MRP = t.MRP;
                        si.SalesRate = t.SalesRate;
                        si.Amount = si.Qty * si.SalesRate;
                        si.DiscAmt = si.Amount * (si.DIscPrc / 100);
                        si.NetAmt = si.Amount - si.DiscAmt;
                        si.TaxId = t.TaxId;
                        si.SGSTPrc = t.SGSTPrc;
                        si.CGSTPrc = t.CGSTPrc;
                        si.IGSTPrc = t.IGSTPrc;
                        si.IGSTAmount = si.Qty * t.IGSTAmount;
                        si.SGSTAmount = si.Qty * t.SGSTAmount;
                        si.CGSTAmount = si.Qty * t.CGSTAmount;
                        si.TotalAmt = si.NetAmt + si.IGSTAmount + si.CGSTAmount + si.SGSTAmount;
                        si.VoucherDate = sale.VoucherDate;
                        t.Qty = t.Qty - si.Qty;
                        db.trnPurchaseItems.AddOrUpdate(t);
                        sale.SGSTAmt += si.SGSTAmount;
                        sale.CGSTAmt += si.CGSTAmount;
                        sale.IGSTAmt += si.IGSTAmount;
                        sale.NetAmt += si.NetAmt;
                        db.trnSales.AddOrUpdate(sale);
                        db.trnSalesItems.Add(si);

                    }
                    else
                    {
                        return RedirectToAction("SalesItemList");
                    }


                }
            }
            else if (sale.SalesType == "SalesRet")
            {
                foreach (var si in sale.trnSalesItems)
                {
                    si.SalesId = sale.SalesId;
                    si.ItemId = si.ItemId;
                    var itId = si.ItemId;
                    si.Qty = si.Qty;
                    si.DIscPrc = si.DIscPrc;

                    mstItem i = db.mstItems.Where(x => x.ItemId == itId).FirstOrDefault();
                    trnPurchaseItem t = db.trnPurchaseItems.Where(x => x.ItemId == itId).FirstOrDefault();

                    si.MRP = t.MRP;
                si.SalesRate = t.SalesRate;
                si.Amount = si.Qty * si.SalesRate;
                si.DiscAmt = si.Amount * (si.DIscPrc / 100);
                si.NetAmt = si.Amount - si.DiscAmt;
                si.TaxId = t.TaxId;
                si.SGSTPrc = t.SGSTPrc;
                si.CGSTPrc = t.CGSTPrc;
                si.IGSTPrc = t.IGSTPrc;
                si.IGSTAmount = si.Qty * t.IGSTAmount;
                si.SGSTAmount = si.Qty * t.SGSTAmount;
                si.CGSTAmount = si.Qty * t.CGSTAmount;
                si.TotalAmt = si.NetAmt + si.IGSTAmount + si.CGSTAmount + si.SGSTAmount;
                si.VoucherDate = sale.VoucherDate;
                t.Qty = t.Qty + si.Qty;
                db.trnPurchaseItems.AddOrUpdate(t);
                sale.SGSTAmt += si.SGSTAmount;
                sale.CGSTAmt += si.CGSTAmount;
                sale.IGSTAmt += si.IGSTAmount;
                sale.NetAmt += si.NetAmt;
                db.trnSales.AddOrUpdate(sale);

            }
            }
            db.SaveChanges();
            return RedirectToAction("SalesList");
        }
        [HttpGet]
        public ActionResult EditInvoice(int Id)
        {
            trnPurchase b = db.trnPurchases.Where(x => x.PurchaseId == Id).SingleOrDefault();
            //  List<string> taxlist = db.mstTaxes.Select(x => x.TaxName).ToList();
            // ViewBag.BName = new SelectList(Blist);
            var acclist = db.mstAccounts.ToList();
            ViewBag.AccList = new SelectList(acclist, "AccountId", "AccountName");
            var salelist = db.trnSales.ToList();
            ViewBag.SaleList = new SelectList(salelist, "SalesId", "VoucherNo");
            var itemlist = db.mstItems.ToList();
            ViewBag.ItemList = new SelectList(itemlist, "ItemId", "ItemName");
            // var model = new demogsoft1.Models.trnPurchase();
            // model.trnPurchaseItems.(new demogsoft1.Models.trnPurchaseItem());
            //var model2 = db.trnPurchases.Include(m => m.trnPurchaseItems).FirstOrDefault(m => m.PurchaseId == Id);
            //ViewBag.ItemList = new SelectList(db.trnPurchaseItems, "PurchaseItemId", "TaxId");


            return View(b);

        }
        [HttpPost]
        public ActionResult EditInvoice(int Id, trnSale sale)
        {
            trnSale x = db.trnSales.Where(f => f.SalesId == Id).SingleOrDefault();
            x.SalesType = sale.SalesType;
            x.VoucherNo = sale.VoucherNo;
            x.VoucherDate = sale.VoucherDate;
            x.InvoiceType = sale.InvoiceType;
            x.NetAmt = sale.NetAmt;
            x.CGSTAmt = sale.CGSTAmt;
            x.IGSTAmt = sale.IGSTAmt;
            x.SGSTAmt = sale.SGSTAmt;
            x.AccountId = sale.AccountId;
            
                foreach (var si in sale.trnSalesItems)
                {
                  //  si.SalesId = sale.SalesId;
                    si.ItemId = si.ItemId;
                  
                    si.Qty = si.Qty;
                    si.DIscPrc = si.DIscPrc;
                var itId = si.ItemId;
                mstItem i = db.mstItems.Where(f => f.ItemId == itId).FirstOrDefault();
                    trnPurchaseItem t = db.trnPurchaseItems.Where(f => f.ItemId == itId).FirstOrDefault();

                    if (t.Qty >= si.Qty)
                    {
                        si.MRP = t.MRP;
                        si.SalesRate = t.SalesRate;
                        si.Amount = si.Qty * si.SalesRate;
                        si.DiscAmt = si.Amount * (si.DIscPrc / 100);
                        si.NetAmt = si.Amount - si.DiscAmt;
                        si.TaxId = t.TaxId;
                        si.SGSTPrc = t.SGSTPrc;
                        si.CGSTPrc = t.CGSTPrc;
                        si.IGSTPrc = t.IGSTPrc;
                        si.IGSTAmount = si.Qty * t.IGSTAmount;
                        si.SGSTAmount = si.Qty * t.SGSTAmount;
                        si.CGSTAmount = si.Qty * t.CGSTAmount;
                        si.TotalAmt = si.NetAmt + si.IGSTAmount + si.CGSTAmount + si.SGSTAmount;
                        si.VoucherDate = sale.VoucherDate;
                        t.Qty = t.Qty - si.Qty;
                        db.trnPurchaseItems.AddOrUpdate(t);
                        sale.SGSTAmt += si.SGSTAmount;
                        sale.CGSTAmt += si.CGSTAmount;
                        sale.IGSTAmt += si.IGSTAmount;
                        sale.NetAmt += si.NetAmt;
                        db.trnSales.AddOrUpdate(sale);
                        db.trnSalesItems.Add(si);

                    }
                    else
                    {
                        return RedirectToAction("SalesItemList");
                    }


                }
            
           

            db.SaveChanges();
            return RedirectToAction("SalesList");
        }
    }
   
}