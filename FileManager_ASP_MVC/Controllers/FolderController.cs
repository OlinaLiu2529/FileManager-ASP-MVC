using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FileManager_EF.EF;
using FileManager_EF.Repos;
using FileManager_EF_Models.Models;
using System.Data.Entity.Infrastructure;

namespace FileManager_ASP_MVC.Controllers
{
    public class FolderController : Controller
    {
        private BaseRepo<Folder> _repo = new BaseRepo<Folder>();

        // GET: Folder
        //public ActionResult Index()
        //{
        //    return View(_repo.GetAll());
        //}


        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                ViewBag.Folder = id;
                TempData["item"] = id;
            }
            return View(_repo.GetAll());
        }

        // GET: Folder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Folder folder = _repo.GetOne(id);
            if (folder == null)
            {
                return HttpNotFound();
            }
            return View(folder);
        }

        // GET: Folder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Folder/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FolderName,ParentFolder")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(folder);
                return RedirectToAction("Index");
            }

            return View(folder);
        }

        // GET: Folder/Edit/5
        public ActionResult Edit()
        {
            Folder folder = _repo.GetOne((int)TempData["item"]);
            if (folder == null)
            {
                return HttpNotFound();
            }
            return View(folder);
        }

        // POST: Folder/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FolderName, ParentFolder, Timestamp")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                _repo.Save(folder);
                return RedirectToAction("Index");
            }
            return View(folder);
        }

        // GET: Folder/Delete/5
        public ActionResult Delete()
        {
            Folder folder = _repo.GetOne((int)TempData["item"]);
            if (folder == null)
            {
                return HttpNotFound();
            }
            return View(folder);
        }

        // POST: Folder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, Timestamp")] Folder folder)
        {
            if (ModelState.IsValid)
            {
                 _repo.Delete(folder);
               // _repo.Delete(_repo.GetOne(1));
                 return RedirectToAction("Index");
            }

            return View(folder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
