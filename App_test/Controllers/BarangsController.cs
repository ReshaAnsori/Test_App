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

namespace App_test.Controllers
{
    [Authorize]
    public class BarangsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Barangs
        public async Task<ActionResult> Index()
        {
            return View(await db.Barangs.ToListAsync());
        }

        // GET: Barangs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barang barang = await db.Barangs.FindAsync(id);
            if (barang == null)
            {
                return HttpNotFound();
            }
            return View(barang);
        }

        // GET: Barangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Barangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Brg_nama,Brg_harga")] Barang barang)
        {
            if (ModelState.IsValid)
            {
                db.Barangs.Add(barang);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(barang);
        }

        // GET: Barangs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barang barang = await db.Barangs.FindAsync(id);
            if (barang == null)
            {
                return HttpNotFound();
            }
            return View(barang);
        }

        // POST: Barangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Brg_nama,Brg_harga")] Barang barang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barang).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(barang);
        }

        // GET: Barangs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barang barang = await db.Barangs.FindAsync(id);
            if (barang == null)
            {
                return HttpNotFound();
            }
            return View(barang);
        }

        // POST: Barangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Barang barang = await db.Barangs.FindAsync(id);
            db.Barangs.Remove(barang);
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
