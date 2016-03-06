using System.Linq;

namespace YueShanp.Models.Interface
{
    interface ICustomerRepository
    {
        void Create(Customer instance);

        void Update(Customer instance);

        void Delete(Customer instance);

        Customer Get(int CustomerId);

        IQueryable<Customer> GetAll();

        void SaveChanges();
    }
}
