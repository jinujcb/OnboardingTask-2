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
            return View(db.Customers.ToList());
        }

        [HttpPost]
        public ActionResult Index(Customer customer)
        {

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            

            return View(db.Customers.ToList());
        }




        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }



        [HttpPost]
        public ActionResult Edit(Customer customer)  // Update PartialView  
        {
            // bool status = false;
            if (ModelState.IsValid)
            {
                Customer customernew = db.Customers.Where(X => X.ID == customer.ID).FirstOrDefault();
                if (customernew != null)
                {
                    customernew.ID = customer.ID;
                    customernew.Name = customer.Name;
                    customernew.Address = customer.Address;
                }

            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            Customer customernew = db.Customers.Where(X => X.ID == id).FirstOrDefault();
            if (customernew != null)
            {
                return View(customernew);
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
                Customer customer = db.Customers.Where(X => X.ID == id).FirstOrDefault();
                if (customer != null)
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                    // status = true;
                }
                // db.Customers.Remove(customer);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}