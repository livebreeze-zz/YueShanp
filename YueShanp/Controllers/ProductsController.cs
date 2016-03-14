using System.Linq;
using System.Net;
using System.Web.Mvc;

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

            var customer = this.customerRepository.Get((int)customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProductsMasterViewModel()
            {
                Customer = customer,
                Products = this.productRepository.GetAll((int)customerId)
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
        public ActionResult Edit([Bind(Include = "Id,Name,QuotedPrice,Note,Creator,CreateTime,Customer")] Product product)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<Product>.EditBaseEntity(product, User.Identity.Name);
                this.productRepository.Update(product);
                return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
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

            var product = this.productRepository.Get((int)id);
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
            var product = this.productRepository.Get(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            EntityHelper<Product>.EditBaseEntity(product, User.Identity.Name, EntityStatus.Deleted);
            this.productRepository.Update(product);
            //this.ProductRepository.Delete(product);

            return RedirectToAction("ProductsMaster", new { CustomerId = product.Customer.Id });
        }

        #region Product cost item
        public ActionResult ProductCostItems(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = this.productRepository.Get((int)productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            var costItems = this.costItemRepository.GetAll((int)productId);

            return View(new ProductCostViewModel()
            {
                ProductId = (int)productId,                
                ProductName = product.Name,
                CostItems = costItems
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

            if (costItem.Product == null)
            {
                return HttpNotFound();
            }

            return View(costItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCostItemCreate([Bind(Include = "Name,UnitPrice,ItemQty,Product")]CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<CostItem>.CreateBaseEntity(costItem, User.Identity.Name);
                costItem.ItemType = ItemType.Normal;
                this.costItemRepository.CreateProductCostItem(costItem);

                return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
            }

            return View(costItem);
        }        

        public ActionResult ProductCostItemEdit(int? costItemId)
        {
            if (costItemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costItem = this.costItemRepository.Get((int)costItemId);

            return View(costItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductCostItemEdit([Bind(Include = "Id,Name,UnitPrice,ItemQty,ItemType,Product,Creator,CreateTime")]CostItem costItem)
        {
            if (ModelState.IsValid)
            {
                EntityHelper<CostItem>.EditBaseEntity(costItem, User.Identity.Name);
                this.costItemRepository.Update(costItem);

                return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
            }

            return View(costItem);
        }        

        public ActionResult ProductCostItemDelete(int? costItemId)
        {
            if (costItemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costItem = this.costItemRepository.Get((int)costItemId);
            if (costItemId == null)
            {
                return HttpNotFound();
            }

            return View(costItem);
        }

        [HttpPost, ActionName("ProductCostItemDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCostItemConfirmed(int costItemId)
        {
            var costItem = this.costItemRepository.Get(costItemId);

            EntityHelper<CostItem>.EditBaseEntity(costItem, User.Identity.Name, EntityStatus.Deleted);
            this.costItemRepository.Update(costItem);

            return RedirectToAction("ProductCostItems", new { ProductId = costItem.Product.Id });
        }
        #endregion
    }
}
