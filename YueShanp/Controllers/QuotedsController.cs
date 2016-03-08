using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    public class QuotedsController : Controller
    {
        private ICustomerRepository CustomerRepository;
        private IQuotedRepository QuotedRepository;

        public QuotedsController()
        {
            this.CustomerRepository = new CustomerRepository();
            this.QuotedRepository = new QuotedRepository();
        }

        // GET: Quoteds
        public ActionResult Index(int? customerId)
        {
            if (customerId == null)
            {
                return RedirectToAction("Index", "Customers");
            }

            var customer = this.CustomerRepository.Get((int)customerId);
            var quoteds = this.QuotedRepository.GetAll().Where(w => w.Customer.Id == (int)customerId);
            ViewData["Customer"] = customer;

            return View(quoteds.ToList());
        }

        // GET: Quoteds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quoted quoted = this.QuotedRepository.Get((int)id);
            if (quoted == null)
            {
                return HttpNotFound();
            }

            return View(quoted);
        }

        // GET: Quoteds/Create
        public ActionResult Create(int customerId)
        {
            var customer = this.CustomerRepository.Get(customerId);
            var quoted = new Quoted()
            {
                Customer = customer
            };

            return View(quoted);
        }

        // POST: Quoteds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuotedPrice,Remark,Product.Name,Customer.Id")] Quoted quoted)
        {
            if (ModelState.IsValid)
            {
                quoted.Customer = this.CustomerRepository.Get(quoted.Customer.Id);
                quoted.Creator = "admin";
                quoted.CreateTime = DateTime.Now;
                quoted.LastEditor = "admin";
                quoted.LastEditTime = DateTime.Now;
                quoted.EntityStatus = EntityStatus.Enabled;

                this.QuotedRepository.Create(quoted);
                return RedirectToAction("Index");
            }

            return View(quoted);
        }

        // GET: Quoteds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quoted quoted = this.QuotedRepository.Get((int)id);
            if (quoted == null)
            {
                return HttpNotFound();
            }

            return View(quoted);
        }

        // POST: Quoteds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuotedPrice,Remark,Product.Name")] Quoted quoted)
        {
            if (ModelState.IsValid)
            {
                quoted.LastEditor = "admin";
                quoted.LastEditTime = DateTime.Now;
                quoted.EntityStatus = EntityStatus.Enabled;

                this.QuotedRepository.Update(quoted);
                return RedirectToAction("Index");
            }

            return View(quoted);
        }

        // GET: Quoteds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quoted quoted = this.QuotedRepository.Get((int)id);
            if (quoted == null)
            {
                return HttpNotFound();
            }

            return View(quoted);
        }

        // POST: Quoteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quoted quoted = this.QuotedRepository.Get(id);

            quoted.LastEditor = "admin";
            quoted.LastEditTime = DateTime.Now;
            quoted.EntityStatus = EntityStatus.Deleted;

            //this.QuotedRepository.Delete(quoted);
            return RedirectToAction("Index");
        }
    }
}
