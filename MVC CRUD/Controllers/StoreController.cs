using MVC_CRUD.Models;
using System;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;

namespace MVC_CRUD.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        StoreEntities1 db = new StoreEntities1();
        // GET: Store
        public JsonResult GetAllStores()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Store> storelist = db.Stores.ToList();
            return Json(storelist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStore(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Store storenew = db.Stores.Find(id);
            return Json(storenew, JsonRequestBehavior.AllowGet);

        }
      
        public ActionResult Index()
        {
            return View(db.Stores.ToList());
        }

        [HttpPost]
        public ActionResult Index(Store store)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Stores.Add(store);
                    db.SaveChanges();

                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            return View(db.Stores.ToList());
        }




        public ActionResult Edit(int id)
        {
            Store store = db.Stores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }



        [HttpPost]
        public ActionResult Edit(Store store)  // Update PartialView  
        {
            // bool status = false;
            if (ModelState.IsValid)
            {
                Store storenew = db.Stores.Where(X => X.ID == store.ID).FirstOrDefault();
                if (storenew != null)
                {
                    storenew.ID = store.ID;
                    storenew.Name = store.Name;
                    storenew.Address = store.Address;
                }


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            Store storenew = db.Stores.Where(X => X.ID == id).FirstOrDefault();
            if (storenew != null)
            {
                return View(storenew);
            }
            else
                return HttpNotFound();
        }



        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)  // Update PartialView  
        {
            // bool status = false;
            if (ModelState.IsValid)
            {
                Store storenew = db.Stores.Where(X => X.ID == id).FirstOrDefault();
                if (storenew != null)
                {
                    db.Stores.Remove(storenew);
                    db.SaveChanges();
                    // status = true;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
           
        }

    }
}