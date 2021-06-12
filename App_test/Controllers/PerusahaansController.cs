using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App_test.Models;
using TestApp.Models;
using System.Xml.Linq;

namespace App_test.Controllers
{
    [Authorize]
    public class PerusahaansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Perusahans
        public async Task<ActionResult> Index()
        {
            return View(await db.Perusahaans.ToListAsync());
        }

        // GET: Perusahans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perusahaan perusahan = await db.Perusahaans.FindAsync(id);
            if (perusahan == null)
            {
                return HttpNotFound();
            }
            return View(perusahan);
        }

        // GET: Perusahans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Perusahans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Comp_nama")] Perusahaan perusahaan)
        {
            if (ModelState.IsValid)
            {
                db.Perusahaans.Add(perusahaan);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(perusahaan);
        }

        // GET: Perusahans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perusahaan perusahan = await db.Perusahaans.FindAsync(id);
            if (perusahan == null)
            {
                return HttpNotFound();
            }
            return View(perusahan);
        }

        // POST: Perusahans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Comp_nama")] Perusahaan perusahan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perusahan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(perusahan);
        }

        // GET: Perusahans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perusahaan perusahan = await db.Perusahaans.FindAsync(id);
            if (perusahan == null)
            {
                return HttpNotFound();
            }
            return View(perusahan);
        }

        // POST: Perusahans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Perusahaan perusahaan = await db.Perusahaans.FindAsync(id);
            db.Perusahaans.Remove(perusahaan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
