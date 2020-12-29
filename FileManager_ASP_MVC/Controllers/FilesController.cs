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


namespace FileManager_ASP_MVC.Controllers
{
    public class FilesController : Controller
    {
        private BaseRepo<File> _repo = new BaseRepo<File>();
        private BaseRepo<Folder> _repoFolder = new BaseRepo<Folder>();
        

        // GET: Files
        //public ActionResult Index()
        //{
        //    var files = _repo.GetAll();
        //    return View(files.ToList());
        //}

        // GET: Files/Details/5
        public ActionResult Details()
        {
            File fileEmpty = new File() { FileName = "FileEmpty", Id = 0, Content = "Нет данных для вывода" };
            File file = _repo.GetOne((int)TempData["item"]);
            if (file == null)
            {
                View(fileEmpty);
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileName,DescriptionFile, Content")] File file)
        {
            if (ModelState.IsValid )
            {
                
                Folder folder = _repoFolder.GetOne((int)TempData["item"]);
                if (folder == null)
                {
                    return HttpNotFound();
                }
                file.Content = TempData["FileData"] as string;
               file.ExtensionId=1;
                file.FolderId = ((int)TempData["item"]);
                _repo.Add(file);
                return RedirectToAction("Index", "Folder");
            }

            return View(file);
        }

        public ActionResult LoadFile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadFile (HttpPostedFileBase uploadFile)
        {
            string content;
            using (var filereader = new System.IO.StreamReader(uploadFile.InputStream))
            {
                content = filereader.ReadToEnd();
            }
            TempData["FileData"] = content;
            // return PartialView();
            return RedirectToAction("Index", "Folder");
        }

        // GET: Files/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    File file = db.Files.Find(id);
        //    if (file == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ExtensionId = new SelectList(db.Extensions, "Id", "Type", file.ExtensionId);
        //    ViewBag.FolderId = new SelectList(db.Folders, "Id", "FolderName", file.FolderId);
        //    return View(file);
        //}

        // POST: Files/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FileName,DescriptionFile,FolderId,ExtensionId,Content,Timestamp")] File file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(file).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ExtensionId = new SelectList(db.Extensions, "Id", "Type", file.ExtensionId);
        //    ViewBag.FolderId = new SelectList(db.Folders, "Id", "FolderName", file.FolderId);
        //    return View(file);
        //}

        // GET: Files/Delete/5
        public ActionResult Delete()
        {
            File file = _repo.GetOne((int)TempData["item"]);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id, Timestamp")] File file)
        {
            _repo.Delete(file);
            return RedirectToAction("Index", "Folder");
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
