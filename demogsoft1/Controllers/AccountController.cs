using demogsoft1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demogsoft1.Controllers
{
    public class AccountController : Controller
    {
        gsoftDBEntities db= new gsoftDBEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountList()
        {
            List<mstAccount> acclist = db.mstAccounts.OrderByDescending(x => x.StateId).ToList();
            return View(acclist);
        }
        [HttpGet]
        public ActionResult AddAccount()
        {
            var gplist = db.mstBsgrGroups.ToList();
            ViewBag.GpList = new SelectList(gplist, "BsgrId", "GroupName");
            var stlist = db.mstStates.ToList();
            ViewBag.StList = new SelectList(stlist, "StateId", "StateName"); 
            return View();
        }
        [HttpPost]
        public ActionResult AddAccount(mstAccount acc)
        {
            db.mstAccounts.Add(acc);
            db.SaveChanges();
            return RedirectToAction("AccountList");
        }
        [HttpGet]
        public ActionResult DeleteAccount(int Id)
        {
            mstAccount t = db.mstAccounts.Where(x => x.AccountId == Id).SingleOrDefault();
            return View(t);
        }
        [HttpPost]
        public ActionResult DeleteAccount(int Id, mstAccount b)
        {
            db.mstAccounts.Remove(db.mstAccounts.Where(x => x.AccountId == Id).SingleOrDefault());
            db.SaveChanges();
            return RedirectToAction("AccountList");
        }
        [HttpGet]
        public ActionResult DetailsAccount(int Id)
        {

            mstAccount bu = db.mstAccounts.Where(x => x.AccountId == Id).SingleOrDefault();
            return View(bu);
        }
        [HttpGet]
        public ActionResult EditAccount(int Id)
        {
            mstAccount b = db.mstAccounts.Where(x => x.AccountId == Id).SingleOrDefault();
            var gplist = db.mstBsgrGroups.ToList();
            ViewBag.GpList = new SelectList(gplist, "BsgrId", "GroupName");
            var stlist = db.mstStates.ToList();
            ViewBag.StList = new SelectList(stlist, "StateId", "StateName");
            return View(b);
        }
        [HttpPost]
        public ActionResult EditAccount(int Id,mstAccount acc)
        {
            mstAccount t = db.mstAccounts.Where(x => x.AccountId == Id).SingleOrDefault();
            t.AccountName = acc.AccountName;
            t.ShortName = acc.ShortName;
            t.Address_ = acc.Address_;
            t.CityName = acc.CityName;
            t.PinCode = acc.PinCode;
            t.Mobile = acc.Mobile;
            t.Email = acc.Email;
            t.GSTNo = acc.GSTNo;
            t.BsgrId = acc.BsgrId;
            t.PANNo = acc.PANNo;
            t.StateId = acc.StateId;
            db.SaveChanges();
            return RedirectToAction("AccountList");
        }
    }
}