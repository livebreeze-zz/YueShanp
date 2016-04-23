using System.Linq;

namespace YueShanp.Models.Interface
{
    interface IDeliveryRepository
    {
        void CreateDeliveryOrder(DeliveryOrder instance);

        void Update(DeliveryOrder instance);

        void Delete(DeliveryOrder instance);

        DeliveryOrder Get(int DeliveryOrderId);

        IQueryable<DeliveryOrder> GetAll(int CustomerId);

        void SaveChanges();
    }
}
