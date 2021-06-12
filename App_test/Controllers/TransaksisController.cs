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
using System.ComponentModel.DataAnnotations;

namespace App_test.Controllers
{
    [Authorize]
    public class TransaksisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transaksis
        public async Task<ActionResult> Index()
        {
            var query = await (from t in db.Transaksis
                        join b in db.Barangs
                            on t.Brg.ID equals b.ID
                        join c in db.Perusahaans
                            on t.Comp.ID equals c.ID
                        select new
                        {
                            ID = t.ID,
                            Brg_ID = b.ID,
                            Brg_nama = b.Brg_nama,
                            Comp_ID = c.ID,
                            Comp_nama = c.Comp_nama,
                            Qty = t.Qty,
                            total = t.total
                        }).ToListAsync();
            var newQ = query.Select(x => new Transaksi
            {
                ID = x.ID,
                Brg_ID = x.ID,
                Brg_nama = x.Brg_nama,
                Comp_ID = x.ID,
                Comp_nama = x.Comp_nama,
                Qty = x.Qty,
                total = x.total
            });

            return View(newQ);
        }

        // GET: Transaksis/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var query = (from t in db.Transaksis
                         join b in db.Barangs
                             on t.Brg.ID equals b.ID
                         join c in db.Perusahaans
                             on t.Comp.ID equals c.ID
                         select new
                         {
                             ID = t.ID,
                             Brg_ID = b.ID,
                             Brg_nama = b.Brg_nama,
                             Comp_ID = c.ID,
                             Comp_nama = c.Comp_nama,
                             Qty = t.Qty,
                             total = t.total
                         }).FirstOrDefault();

            var newQ = new Transaksi
            {
                ID = query.ID,
                Brg_ID = query.ID,
                Brg_nama = query.Brg_nama,
                Comp_ID = query.ID,
                Comp_nama = query.Comp_nama,
                Qty = query.Qty,
                total = query.total
            };

            if (newQ == null)
            {
                return HttpNotFound();
            }
            return View(newQ);
        }

        // GET: Transaksis/Create
        public ActionResult Create()
        {
            var brg = db.Barangs.ToList();
            var comp = db.Perusahaans.ToList();
            ViewBag.databrg = brg;
            ViewBag.dataComp = comp;

            return View();
        }

        // POST: Transaksis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Brg_id,Comp_id,Qty")] Transaksi transaksi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var barang = db.Barangs.Find(transaksi.Brg_ID);
                    var comp = db.Perusahaans.Find(transaksi.Comp_ID);

                    if(barang == null || comp == null)
                    {
                        ModelState.AddModelError("", "Wah error nih");
                    }
                    else
                    {

                        transaksi.Brg = barang;
                        transaksi.Comp = comp;
                        transaksi.total = barang.Brg_harga * transaksi.Qty;

                        db.Transaksis.Add(transaksi);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }

            var brg = db.Barangs.ToList();
            var Comp = db.Perusahaans.ToList();
            ViewBag.databrg = brg;
            ViewBag.dataComp = Comp;

            return View(transaksi);
        }

        // GET: Transaksis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = (from t in db.Transaksis
                        join b in db.Barangs
                            on t.Brg.ID equals b.ID
                        join c in db.Perusahaans
                            on t.Comp.ID equals c.ID
                        where t.ID == id
                        select new 
                        {
                            ID = t.ID,
                            Brg_ID = b.ID,
                            Brg_nama = b.Brg_nama,
                            Comp_ID = c.ID,
                            Comp_nama = c.Comp_nama,
                            Qty = t.Qty,
                            total = t.total
                        }).FirstOrDefault();

            var newQ = new Transaksi
            {
                ID = query.ID,
                Brg_ID = query.Brg_ID,
                Brg_nama = query.Brg_nama,
                Comp_ID = query.Comp_ID,
                Comp_nama = query.Comp_nama,
                Qty = query.Qty,
                total = query.total
            };

            if (newQ == null)
            {
                return HttpNotFound();
            }

            var brg = db.Barangs.ToList();
            var comp = db.Perusahaans.ToList();
            ViewBag.databrg = brg;
            ViewBag.dataComp = comp;

            return View(newQ);
        }

        // POST: Transaksis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Brg_id,comp_id,Qty")] Transaksi transaksi)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var barang = db.Barangs.Find(transaksi.Brg_ID);
                    var comp = db.Perusahaans.Find(transaksi.Comp_ID);

                    if (barang == null || comp == null)
                    {
                        ModelState.AddModelError("", "Wah error nih");
                    }
                    else
                    {

                        transaksi.Brg = barang;
                        transaksi.Comp = comp;
                        transaksi.total = barang.Brg_harga * transaksi.Qty;

                        db.Entry(transaksi).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            var brg = db.Barangs.ToList();
            var Comp = db.Perusahaans.ToList();
            ViewBag.databrg = brg;
            ViewBag.dataComp = Comp;
            return View(transaksi);
        }

        // GET: Transaksis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaksi transaksi = await db.Transaksis.FindAsync(id);
            if (transaksi == null)
            {
                return HttpNotFound();
            }
            return View(transaksi);
        }

        // POST: Transaksis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Transaksi transaksi = await db.Transaksis.FindAsync(id);
            db.Transaksis.Remove(transaksi);
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
    public class TransaksiDto
    {
        public int ID { get; set; }

        public int Brg_ID { get; set; }
        [Required]
        public int Comp_ID { get; set; }

        [Required]
        public string Brg_nama { get; set; }

        [Required]
        public string Comp_nama { get; set; }

        [Required]
        public int Qty { get; set; }
        [Required]
        public decimal total { get; set; }
       
    }
}


