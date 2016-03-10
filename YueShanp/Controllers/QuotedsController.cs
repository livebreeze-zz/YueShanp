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
        private readonly string Creator = "admin";

        public QuotedsController()
        {
            this.CustomerRepository = new CustomerRepository();
            this.QuotedRepository = new QuotedRepository();
        }

        // GET: Quoteds
        public ActionResult QuotedsMaster(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return RedirectToAction("Index", "Customers");
            }

            var viewModel = new QuotedsMasterViewModel()
            {
                Customer = this.CustomerRepository.Get((int)customerId),
                QuotedList = this.QuotedRepository.GetAll().Where(w => w.Customer.Id == (int)customerId)
            };

            return View(viewModel);
        }

        // GET: Quoteds/Details/5
        public ActionResult QuotedsDetail(int? id)
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
        public ActionResult Create([Bind(Include = "Id,QuotedPrice,Remark,Product,Customer")] Quoted quoted)
        {
            if (ModelState.IsValid)
            {
                quoted.Customer = this.CustomerRepository.Get(quoted.Customer.Id);
                quoted.Creator = this.Creator;
                quoted.CreateTime = DateTime.Now;
                quoted.LastEditor = this.Creator;
                quoted.LastEditTime = DateTime.Now;
                quoted.EntityStatus = EntityStatus.Enabled;

                quoted.Product.Name = quoted.Product.Name;
                quoted.Product.Creator = this.Creator;
                quoted.Product.CreateTime = DateTime.Now;
                quoted.Product.LastEditor = this.Creator;
                quoted.Product.LastEditTime = DateTime.Now;

                this.QuotedRepository.CreateProductQuoted(quoted);
                return RedirectToAction("QuotedsMaster", new { CustomerId = quoted.Customer.Id });
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
                quoted.LastEditor = this.Creator;
                quoted.LastEditTime = DateTime.Now;
                quoted.EntityStatus = EntityStatus.Enabled;

                this.QuotedRepository.Update(quoted);
                return RedirectToAction("QuotedsMaster");
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

            quoted.LastEditor = this.Creator;
            quoted.LastEditTime = DateTime.Now;
            quoted.EntityStatus = EntityStatus.Deleted;

            //this.QuotedRepository.Delete(quoted);
            return RedirectToAction("QuotedsMaster");
        }
    }
}
