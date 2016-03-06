using System;
using System.Linq;
using YueShanp.Models.Interface;

namespace YueShanp.Models.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Create(Customer instance)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer instance)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Customer instance)
        {
            throw new NotImplementedException();
        }
    }
}