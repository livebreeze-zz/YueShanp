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
        private ICustomerRepository customerRepository;
        private IProductRepository productRepository;
        private ICostItemRepository costItemRepository;

        public ProductsController()
        {
            this.customerRepository = new CustomerRepository();
            this.productRepository = new ProductRepository();
            this.costItemRepository = new CostItemRepository();
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
                Customer = this.customerRepository.Get((int)customerId),
                Products = this.productRepository.GetAll().Where(w =>
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

            Product product = this.productRepository.Get((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create(int customerId)
        {
            var customer = this.customerRepository.Get(customerId);
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
                product.Customer = this.customerRepository.Get(product.Customer.Id);

                EntityHelper<Product>.CreateBaseEntity(product, User.Identity.Name);

                this.productRepository.CreateProductQuoted(product);
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

            Product product = this.productRepository.Get((int)id);
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
                EntityHelper<Product>.EditBaseEntity(product, User.Identity.Name);
                this.productRepository.Update(product);
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

            Product product = this.productRepository.Get((int)id);
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
            Product product = this.productRepository.Get(id);

            EntityHelper<Product>.EditBaseEntity(product, User.Identity.Name, EntityStatus.Deleted);
            this.productRepository.Update(product);
            //this.ProductRepository.Delete(product);

            return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
        }

        public ActionResult ProductCostItems(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = this.productRepository.Get((int)productId);
            var customer = this.customerRepository.Get(product.Customer.Id);

            return View(new ProductCostViewModel()
            {
                Product = product,
                Customer = customer
            });
        }

        public ActionResult ProductCostItemCreate(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costItem = new CostItem()
            {
                Product = this.productRepository.Get((int)productId)
            };

            return View(costItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCostItemCreate([Bind(Include = "Name,UnitPrice,ItemQty,ItemType,Product")]CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<CostItem>.CreateBaseEntity(costItem, User.Identity.Name);
                this.costItemRepository.CreateProductCostItem(costItem);

                return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
            }

            return View(costItem);
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
