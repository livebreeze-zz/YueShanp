using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private ICustomerRepository CustomerRepository;
        private IProductRepository ProductRepository;

        public ProductsController()
        {
            this.CustomerRepository = new CustomerRepository();
            this.ProductRepository = new ProductRepository();
        }

        // GET: ProductsMaster
        [AllowAnonymous]
        public ActionResult ProductsMaster(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //return RedirectToAction("Index", "Customers");
            }

            var viewModel = new ProductsMasterViewModel()
            {
                Customer = this.CustomerRepository.Get((int)customerId),
                Products = this.ProductRepository.GetAll().Where(w =>
                                                        w.Customer.Id == (int)customerId
                                                        && w.EntityStatus == EntityStatus.Enabled).ToList()
            };

            return View(viewModel);
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.ProductRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create(int customerId)
        {
            var customer = this.CustomerRepository.Get(customerId);
            var product = new Product()
            {
                Customer = customer
            };

            return View(product);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,QuotedPrice,Note,Customer")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Customer = this.CustomerRepository.Get(product.Customer.Id);
                product.Creator = User.Identity.GetUserName();
                product.CreateTime = DateTime.Now;
                product.LastEditor = User.Identity.GetUserName();
                product.LastEditTime = DateTime.Now;
                product.EntityStatus = EntityStatus.Enabled;

                this.ProductRepository.CreateProductQuoted(product);
                return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.ProductRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,QuotedPrice,Note")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.LastEditor = User.Identity.GetUserName();
                product.LastEditTime = DateTime.Now;
                product.EntityStatus = EntityStatus.Enabled;

                this.ProductRepository.Update(product);
                return RedirectToAction("ProductsMaster");
            }

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.ProductRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = this.ProductRepository.Get(id);

            product.LastEditor = User.Identity.GetUserName();
            product.LastEditTime = DateTime.Now;
            product.EntityStatus = EntityStatus.Deleted;

            this.ProductRepository.Update(product);
            //this.ProductRepository.Delete(product);

            return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
        }

        public ActionResult ProductCost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = this.ProductRepository.Get((int)id);
            var customer = this.CustomerRepository.Get(product.Customer.Id);

            return View(new ProductCostViewModel()
            {
                Product = product,
                Customer = customer
            });
        }

        public ActionResult ProductCostCreate()
        {
            return View();
        }

        public ActionResult ProductCostEdit()
        {
            return View();
        }

        public ActionResult ProductCostDetails()
        {
            return View();
        }        

        public ActionResult ProductCostDelete()
        {
            return View();
        }
    }
}
