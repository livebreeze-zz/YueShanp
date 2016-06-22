using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using YueShanp.Models.Interface;

namespace YueShanp.Models
{
    public class DeliveryRepository : IDeliveryRepository, IDisposable
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
            if (instance == null)
            {
                throw new ArgumentException(nameof(DeliveryOrder));
            }
            else
            {
                this.db.DeliveryOrders.Add(instance);
                db.Entry(instance.Customer).State = EntityState.Unchanged;
                instance.DeliveryOrderDetailList.ForEach(deliveryOrderDetail =>
                    {
                        deliveryOrderDetail.DeliveryUnitPrice = deliveryOrderDetail.Product.UnitPrice;
                        db.Entry(deliveryOrderDetail.Product).State = EntityState.Unchanged;
                    }
                );

                this.SaveChanges();
            }
        }

        public void Delete(DeliveryOrder instance)
        {
            throw new NotImplementedException();
        }

        public DeliveryOrder Get(int deliveryOrderId)
        {
            return db.DeliveryOrders.Find(deliveryOrderId);
        }

        public IQueryable<DeliveryOrder> GetAll(int customerId)
        {
            return
                db.DeliveryOrders
                    .OrderByDescending(o => o.Id)
                    .Where(w => w.Customer.Id == customerId && w.EntityStatus == EntityStatus.Enabled);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Update(DeliveryOrder instance)
        {
            throw new NotImplementedException();
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