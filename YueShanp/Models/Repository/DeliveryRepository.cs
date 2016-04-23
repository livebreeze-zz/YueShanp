using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YueShanp.Models.Interface;

namespace YueShanp.Models
{
    public class DeliveryRepository : IDeliveryRepository
    {
        protected YueShanpContext db
        {
            get;
            private set;
        }

        public DeliveryRepository()
        {
            this.db = new YueShanpContext();
        }

        public void CreateDeliveryOrder(DeliveryOrder instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(DeliveryOrder instance)
        {
            throw new NotImplementedException();
        }

        public DeliveryOrder Get(int DeliveryOrderId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DeliveryOrder> GetAll(int CustomerId)
        {
            return db.DeliveryOrders.OrderByDescending(o => o.Id).Where(w => w.EntityStatus == EntityStatus.Enabled);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(DeliveryOrder instance)
        {
            throw new NotImplementedException();
        }
    }
}