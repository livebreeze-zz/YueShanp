using System;
using System.Data.Entity;
using System.Linq;
using YueShanp.Models.Interface;

namespace YueShanp.Models
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        protected YueShanpContext db
        {
            get;
            private set;
        }

        public ProductRepository()
        {
            this.db = new YueShanpContext();
        }

        public void CreateProductQuoted(Product instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Product");
            }
            else
            {
                db.Products.Add(instance);

                // Use this setting to not do add or update for Customer data.
                db.Entry(instance.Customer).State = EntityState.Unchanged;
                this.SaveChanges();
            }
        }        

        public void Update(Product instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Product");
            }
            else
            {
                db.Entry(instance.Customer).State = EntityState.Unchanged;
                db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(Product instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Product");
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Product Get(int productId)
        {
            return db.Products.Find(productId);
        }

        public Product Get(string productName)
        {
            return db.Products.ToList().Where(x => x.Name.EqualsIgnoreCase(productName)).FirstOrDefault();
        }

        public IQueryable<Product> GetAll(int customerId)
        {
            return db.Products.Where(w => w.Customer.Id == customerId && w.EntityStatus == EntityStatus.Enabled).OrderByDescending(x => x.Id);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }        
    }
}