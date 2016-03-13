using System;
using System.Collections.Generic;
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
                db.CostItems.Add(instance);
                this.SaveChanges();
            }
        }

        public CostItem Get(int CostItemId)
        {
            throw new NotImplementedException();
        }

        public void Update(CostItem instance)
        {
            throw new NotImplementedException();
        }
        public void Delete(CostItem instance)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }       
    }
}