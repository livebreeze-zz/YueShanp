using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    public class CustomersController : Controller
    {
        private ICustomerRepository customerRepository;

        public CustomersController()
        {
            this.customerRepository = new CustomerRepository();
        }        

        // GET: Customers
        public ActionResult Index()
        {
            var customers = this.customerRepository.GetAll().Where(w => w.EntityStatus == EntityStatus.Enabled);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = this.customerRepository.Get((int)id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Address,Email,Purchaser")] Customer customer)
        {
            customer.Creator = "admin";
            customer.CreateTime = DateTime.Now;
            customer.LastEditor = "admin";
            customer.LastEditTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                this.customerRepository.Create(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = this.customerRepository.Get((int)id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Address,Email,Purchaser,Creator,CreateTime")] Customer customer)
        {
            customer.LastEditor = "admin";
            customer.LastEditTime = DateTime.Now;

            if (ModelState.IsValid)
            {
                this.customerRepository.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = this.customerRepository.Get((int)id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = this.customerRepository.Get(id);

            customer.LastEditor = "admin";
            customer.LastEditTime = DateTime.Now;
            customer.EntityStatus = EntityStatus.Deleted;
            this.customerRepository.Update(customer);

            //this.customerRepository.Delete(customer);

            return RedirectToAction("Index");
        }       
    }
}
