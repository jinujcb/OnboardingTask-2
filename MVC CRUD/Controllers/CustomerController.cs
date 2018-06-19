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
    public class CustomerController : Controller
    {
        // GET: Customer
        StoreEntities1 db = new StoreEntities1();
        // GET: Store
        public JsonResult GetAllCustomers()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Customer> customerlist = db.Customers.ToList();
            return Json(customerlist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCustomer(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Customer customernew = db.Customers.Find(id);
            return Json(customernew, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Index()
        {
            return View("Index");
        }


        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)  // Update PartialView  
        {
            if (ModelState.IsValid)
            {
                Customer customernew = db.Customers.Where(X => X.ID == customer.ID).FirstOrDefault();
                if (customernew != null)
                {
                    customernew.ID = customer.ID;
                    customernew.Name = customer.Name;
                    customernew.Address = customer.Address;
                }
                db.SaveChanges();
                return View("Index");
            }
            else
            {
                return View(customer);
            }
        }


        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)  // Update PartialView  
        {
            if (ModelState.IsValid)
            {
                Customer customernew = db.Customers.Where(X => X.ID == id).FirstOrDefault();
                if (customernew != null)
                {
                    db.Customers.Remove(customernew);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
    }
}