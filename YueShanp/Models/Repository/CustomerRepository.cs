using System;
using System.Data.Entity;
using System.Linq;
using YueShanp.Models.Interface;

namespace YueShanp.Models.Repository
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private const string ARGUMENTNULLEXCEPTIONPARAM = "instance";

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
                db.Customers.Add(instance);
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
                db.Entry(instance).State = EntityState.Modified;
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
                db.Entry(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public Customer Get(int customerId)
        {
            return db.Customers.FirstOrDefault(x => x.Id == customerId);
        }

        public IQueryable<Customer> GetAll()
        {
            return db.Customers.OrderByDescending(x => x.Id);
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