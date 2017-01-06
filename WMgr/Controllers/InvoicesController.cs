using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMgr.Models;

namespace WMgr.Controllers
{
    public class InvoicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Invoices
        public ActionResult Index()
        {
            return View(db.Invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            int maxInvoiceNumber = 0;
            foreach(var inv in db.Invoices)
            {
                if (inv.Number > maxInvoiceNumber)
                {
                    maxInvoiceNumber = inv.Number;
                }
            }

            var invoice = new Invoice() { Number = ++maxInvoiceNumber, Date = DateTime.Now, Validated = false };
            db.Invoices.Add(invoice);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Products(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return PartialView("Partial/InvoiceDetail", invoice.Details);
        }

        [HttpPost]
        public ActionResult AddProduct(int? invoiceId, int? productId)
        {
            if (invoiceId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.Invoices.Find(invoiceId);
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (invoice.Validated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.Products.Find(productId);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detail = invoice.Details.AsEnumerable().Where(d => d.Product.ProductId == productId).FirstOrDefault();
            if (detail == null)
            {
                detail = new InvoiceDetail() { InvoiceId = invoice.InvoiceId, Product = product, SellPrice = product.Price, Quantity = 1 };
                invoice.Details.Add(detail);
            }
            else
            {
                detail.Quantity++;
            }
            detail.SellPrice = product.Price;
            
            db.SaveChanges();

            return Json(detail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveProduct(int? invoiceId, int? productId)
        {
            if (invoiceId == null || productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.Invoices.Find(invoiceId);
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (invoice.Validated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var detail = invoice.Details.Where(d => d.Product.ProductId == productId).FirstOrDefault();
            if (detail == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            invoice.Details.Remove(detail);
            db.Entry(detail).State = EntityState.Deleted;
            db.Entry(invoice).State = EntityState.Modified;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Validate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (invoice.Validated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            invoice.Validated = true;

            db.Entry(invoice).State = EntityState.Modified;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult Total(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return Json(new { invoiceTotal = invoice.Total}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Invalidate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!invoice.Validated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            invoice.Validated = false;

            db.Entry(invoice).State = EntityState.Modified;
            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
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
