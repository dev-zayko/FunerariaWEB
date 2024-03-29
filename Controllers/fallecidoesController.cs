﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FunerariaMuertoFeliz.Models;

namespace FunerariaMuertoFeliz.Controllers
{
    public class fallecidoesController : Controller
    {
        private FunerariaEntities db = new FunerariaEntities();

        // GET: fallecidoes
        public ActionResult Index()
        {
            return View(db.fallecido.ToList());
        }
        public ActionResult ObituarioYear()
        {
            var grouped = (from p in db.fallecido.ToList()
                           group p by new { month = p.fechaDefuncion.Month, year = p.fechaDefuncion.Year} into d
                           select new { dt = string.Format("{0}/{1}", d.Key.month, d.Key.year), count = d.Count() }).OrderByDescending(g => g.dt);
 
            return View(grouped);
        }
        // GET: fallecidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fallecido fallecido = db.fallecido.Find(id);
            if (fallecido == null)
            {
                return HttpNotFound();
            }
            return View(fallecido);
        }

        // GET: fallecidoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fallecidoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idfallecido,rut,nombre,apellidoP,apellidoM,fechaNacimiento,fechaDefuncion")] fallecido fallecido)
        {
            if (ModelState.IsValid)
            {
                db.fallecido.Add(fallecido);
                Session["fallecido"] = fallecido;
                db.SaveChanges();
                return Redirect("~/parques/Index");
            }

            return View(fallecido);
        }

        // GET: fallecidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fallecido fallecido = db.fallecido.Find(id);
            if (fallecido == null)
            {
                return HttpNotFound();
            }
            return View(fallecido);
        }

        // POST: fallecidoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idfallecido,rut,nombre,apellidoP,apellidoM,fechaNacimiento,fechaDefuncion")] fallecido fallecido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fallecido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fallecido);
        }

        // GET: fallecidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fallecido fallecido = db.fallecido.Find(id);
            if (fallecido == null)
            {
                return HttpNotFound();
            }
            return View(fallecido);
        }

        // POST: fallecidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fallecido fallecido = db.fallecido.Find(id);
            db.fallecido.Remove(fallecido);
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
