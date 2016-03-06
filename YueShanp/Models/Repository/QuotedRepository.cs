using System;
using System.Data.Entity;
using System.Linq;
using YueShanp.Models.Interface;

namespace YueShanp.Models.Repository
{
    public class QuotedRepository : IQuotedRepository, IDisposable
    {
        private const string ARGUMENTNULLEXCEPTIONPARAM = "instance";

        protected YueShanpContext db
        {
            get;
            private set;
        }

        public QuotedRepository()
        {
            this.db = new YueShanpContext();
        }

        public void Create(Quoted instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(ARGUMENTNULLEXCEPTIONPARAM);
            }
            else
            {
                db.Quoteds.Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(Quoted instance)
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

        public void Delete(Quoted instance)
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

        public Quoted Get(int QuotedId)
        {
            return db.Quoteds.FirstOrDefault(x => x.Id == QuotedId);
        }

        public IQueryable<Quoted> GetAll()
        {
            return db.Quoteds.OrderByDescending(x => x.Id);
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