using System.Linq;

namespace YueShanp.Models.Interface
{
    interface IProductRepository
    {
        void CreateProductQuoted(Product instance);

        void Update(Product instance);

        void Delete(Product instance);

        Product Get(int productId);

        Product Get(string productName);

        IQueryable<Product> GetAll(int customerId);

        void SaveChanges();
    }
}
