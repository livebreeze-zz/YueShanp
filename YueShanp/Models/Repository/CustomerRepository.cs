using System;
using System.Data.Entity;
using System.Linq;
using YueShanp.Models.Interface;

namespace YueShanp.Models
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private const string ARGUMENTNULLEXCEPTIONPARAM = "Customer";

        protected YueShanpContext db
        {
            get;
            private set;
        }

        public CustomerRepository()
        {
            this.db = new YueShanpContext();
        }

        public void Create(Customer instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(ARGUMENTNULLEXCEPTIONPARAM);
            }
            else
            {
                this.db.Customers.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(Customer instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(ARGUMENTNULLEXCEPTIONPARAM);
            }
            else
            {
                this.db.Entry(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(Customer instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(ARGUMENTNULLEXCEPTIONPARAM);
            }
            else
            {
                this.db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Customer Get(int customerId)
        {
            return this.db.Customers.Find(customerId);
        }

        public IQueryable<Customer> GetAll()
        {
            return this.db.Customers.OrderByDescending(x => x.Id);
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