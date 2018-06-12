using MVC_CRUD.Models;
using System;
using System.Data.Entity.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Data.Entity;
using Newtonsoft.Json;

namespace MVC_CRUD.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        StoreEntities1 db = new StoreEntities1();

        // GET: Store

        JavaScriptSerializer ser = new JavaScriptSerializer();
        public JsonResult GetAllSales()
        {
            db.Configuration.ProxyCreationEnabled = false;

            List<ProductSold> salelist = db.ProductSolds.Include(c => c.Customer).Include(c => c.Product).Include(c => c.Store).ToList();
            //var deserializedProduct = JsonConvert.DeserializeObject<IEnumerable<ProductSold>>();

            var product = JsonConvert.SerializeObject(salelist, Formatting.Indented,
     new JsonSerializerSettings()
     {
         ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     }
 );
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSales(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            ProductSold productnew = db.ProductSolds.Find(id);
            return Json(productnew, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
            
            ViewBag.Customers = new SelectList(db.Customers.ToList(), "ID", "Name");
            ViewBag.Products = new SelectList(db.Products.ToList(), "ID", "Name");
            ViewBag.Stores = new SelectList(db.Stores.ToList(), "ID", "Name");
            ProductSold productnew = new ProductSold();
            if (productnew == null)
            {
                return HttpNotFound();
            }
            return View(productnew);
   
        }

        [HttpPost]
        public ActionResult Index(ProductSold product)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");


        }

        public ActionResult Create()
        {
            ViewBag.Customers = new SelectList(db.Customers.ToList(), "ID", "Name");
            ViewBag.Products = new SelectList(db.Products.ToList(), "ID", "Name");
            ViewBag.Stores = new SelectList(db.Stores.ToList(), "ID", "Name");
            ProductSold productnew = new ProductSold();
            if (productnew == null)
            {
                return HttpNotFound();
            }
            return View(productnew);
        }

        [HttpPost]

        public ActionResult Create(ProductSold productsold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolds.Add(productsold);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }



        public ActionResult Edit(int id)
        {
            ProductSold productnew = db.ProductSolds.Find(id);
            if (productnew == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customers = new SelectList(db.Customers, "ID", "Name");
            ViewBag.Products = new SelectList(db.Products, "ID", "Name");
            ViewBag.Stores = new SelectList(db.Stores, "ID", "Name");
            return View(productnew);
        }



        [HttpPost]

        public ActionResult Edit(ProductSold product)  // Update PartialView  
        {
            if (ModelState.IsValid)
            {
                ProductSold productnew = db.ProductSolds.Where(X => X.ID == product.ID).FirstOrDefault();
                if (productnew != null)
                {
                    productnew.CustomerID = product.CustomerID;
                    productnew.ProductID = product.ProductID;
                    productnew.StoreID = product.StoreID;
                    productnew.Datesold = product.Datesold;
                    db.SaveChanges();
                    // status = true;
                }
            }

            return RedirectToAction("Index");


        }


        public ActionResult Delete(int id)
        {
            ProductSold productnew = db.ProductSolds.Where(X => X.ID == id).FirstOrDefault();
            if (productnew == null)
            {
                return HttpNotFound();
            }

            return View(productnew);
        }



        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)  // Update PartialView  
        {
            // bool status = false;
            if (ModelState.IsValid)
            {
                ProductSold productnew = db.ProductSolds.Where(X => X.ID == id).FirstOrDefault();
                if (productnew != null)
                {
                    db.ProductSolds.Remove(productnew);
                    db.SaveChanges();
                    // status = true;
                }
            }
            return RedirectToAction("Index");

        }
    }
}