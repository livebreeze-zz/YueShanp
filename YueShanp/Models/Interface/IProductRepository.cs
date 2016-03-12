using System.Linq;

namespace YueShanp.Models.Interface
{
    interface IProductRepository
    {
        void CreateProductQuoted(Product instance);

        void Update(Product instance);

        void Delete(Product instance);

        Product Get(int QuotedId);

        IQueryable<Product> GetAll();

        void SaveChanges();
    }
}
