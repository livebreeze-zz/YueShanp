using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YueShanp.Models.Interface;

namespace YueShanp.Models
{
    public class CostItemRepository : ICostItemRepository
    {
        protected YueShanpContext db
        {
            get;
            private set;
        }

        public CostItemRepository()
        {
            this.db = new YueShanpContext();
        }

        public void CreateProductCostItem(CostItem instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("CostItem");
            }
            else
            {
                db.Entry(instance.Product).State = EntityState.Unchanged;
                db.CostItems.Add(instance);
                this.SaveChanges();
            }
        }

        public CostItem Get(int costItemId)
        {
            return db.CostItems.FirstOrDefault(x => x.Id == costItemId);
        }

        public IQueryable<CostItem> GetAll(int productId)
        {
            return db.CostItems.Where(w => w.Product.Id == productId && w.EntityStatus == EntityStatus.Enabled).OrderByDescending(x => x.Id);
        }

        public void Update(CostItem instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("CostItem");
            }
            else
            {
                //db.Entry(instance.Customer).State = EntityState.Unchanged;
                db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }
        public void Delete(CostItem instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("CostItem");
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
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