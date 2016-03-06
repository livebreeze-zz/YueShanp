using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using YueShanp.Models;
using YueShanp.Models.Interface;

namespace YueShanp.Controllers
{
    public class QuotedsController : Controller
    {
        private IQuotedRepository QuotedRepository;

        public QuotedsController()
        {
            this.QuotedRepository = new QuotedRepository();
        }

        // GET: Quoteds
        public ActionResult Index()
        {
            var Quoteds = this.QuotedRepository.GetAll();
            return View(Quoteds.ToList());
        }

        // GET: Quoteds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quoted quoted = this.QuotedRepository.Get((int)id);
            if (quoted == null)
            {
                return HttpNotFound();
            }

            return View(quoted);
        }

        // GET: Quoteds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quoteds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,QuotedPrice,Remark,Creator,CreateTime,LastEditor,LastEditTime,EntityStatus")] Quoted quoted)
        {
            if (ModelState.IsValid)
            {
                this.QuotedRepository.Create(quoted);
                //return RedirectToAction("Index");
            }

            return View(quoted);
        }

        // GET: Quoteds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quoted quoted = this.QuotedRepository.Get((int)id);
            if (quoted == null)
            {
                return HttpNotFound();
            }

            return View(quoted);
        }

        // POST: Quoteds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,QuotedPrice,Remark,Creator,CreateTime,LastEditor,LastEditTime,EntityStatus")] Quoted quoted)
        {
            if (ModelState.IsValid)
            {
                this.QuotedRepository.Update(quoted);
                //return RedirectToAction("Index");
            }

            return View(quoted);
        }

        // GET: Quoteds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quoted quoted = this.QuotedRepository.Get((int)id);
            if (quoted == null)
            {
                return HttpNotFound();
            }

            return View(quoted);
        }

        // POST: Quoteds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quoted quoted = this.QuotedRepository.Get(id);
            this.QuotedRepository.Delete(quoted);
            return RedirectToAction("Index");
        }
    }
}
