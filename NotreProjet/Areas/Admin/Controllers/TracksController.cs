using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Nwd.BackOffice.Model;

namespace NotreProjet.Areas.Admin.Controllers
{
    public class TracksController : Controller
    {
        private NwdMusikEntities db = new NwdMusikEntities();

        // GET: Admin/Tracks
        public ActionResult Index()
        {
            var tracks = db.Tracks.Include(t => t.Album).Include(t => t.Song);
            return View(tracks.ToList());
        }

        // GET: Admin/Tracks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // GET: Admin/Tracks/Create
        public ActionResult Create()
        {
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "Title");
            ViewBag.Song_Id = new SelectList(db.Songs, "Id", "Name");
            return View();
        }

        // POST: Admin/Tracks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,Number,Duration,FileRelativePath,Song_Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                db.Tracks.Add(track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "Title", track.AlbumId);
            ViewBag.Song_Id = new SelectList(db.Songs, "Id", "Name", track.Song_Id);
            return View(track);
        }

        // GET: Admin/Tracks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "Title", track.AlbumId);
            ViewBag.Song_Id = new SelectList(db.Songs, "Id", "Name", track.Song_Id);
            return View(track);
        }

        // POST: Admin/Tracks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Number,Duration,FileRelativePath,Song_Id")] Track track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumId = new SelectList(db.Albums, "Id", "Title", track.AlbumId);
            ViewBag.Song_Id = new SelectList(db.Songs, "Id", "Name", track.Song_Id);
            return View(track);
        }

        // GET: Admin/Tracks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        // POST: Admin/Tracks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Track track = db.Tracks.Find(id);
            db.Tracks.Remove(track);
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
