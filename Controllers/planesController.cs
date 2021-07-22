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
    public class planesController : Controller
    {
        private FunerariaEntities db = new FunerariaEntities();

        // GET: planes
        public ActionResult Index()
        {
            var planes = db.planes.Include(p => p.tipoplan);
            return View(planes.ToList());
        }

        // GET: planes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planes planes = db.planes.Find(id);
            if (planes == null)
            {
                return HttpNotFound();
            }
            return View(planes);
        }

        // GET: planes/Create
        public ActionResult Create()
        {
            ViewBag.tipoplan_id = new SelectList(db.tipoplan, "idtipoplan", "nombre");
            return View();
        }

        // POST: planes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idplanes,nombre,primer_contenido,segundo_contenido,tercer_contenido,cuarto_contenido,quinto_contenido,precio,tipoplan_id")] planes planes)
        {
            if (ModelState.IsValid)
            {
                db.planes.Add(planes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tipoplan_id = new SelectList(db.tipoplan, "idtipoplan", "nombre", planes.tipoplan_id);
            return View(planes);
        }

        // GET: planes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planes planes = db.planes.Find(id);
            if (planes == null)
            {
                return HttpNotFound();
            }
            ViewBag.tipoplan_id = new SelectList(db.tipoplan, "idtipoplan", "nombre", planes.tipoplan_id);
            return View(planes);
        }

        // POST: planes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idplanes,nombre,primer_contenido,segundo_contenido,tercer_contenido,cuarto_contenido,quinto_contenido,precio,tipoplan_id")] planes planes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tipoplan_id = new SelectList(db.tipoplan, "idtipoplan", "nombre", planes.tipoplan_id);
            return View(planes);
        }

        // GET: planes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planes planes = db.planes.Find(id);
            if (planes == null)
            {
                return HttpNotFound();
            }
            return View(planes);
        }

        // POST: planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            planes planes = db.planes.Find(id);
            db.planes.Remove(planes);
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
