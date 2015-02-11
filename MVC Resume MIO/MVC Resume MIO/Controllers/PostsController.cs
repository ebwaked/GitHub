using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Resume_MIO.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;



namespace MVC_Resume_MIO.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Comment([Bind(Include = "PostId, Message")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.AuthorID = User.Identity.Name; 
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = comment.PostID});
            }

            comment.Created = DateTimeOffset.Now.Date;
            comment.Updated = DateTimeOffset.Now.Date;
            return View(comment);
        }


        
        // GET: Posts
        [Authorize(Roles="Admin")]
        public ActionResult AdminIndex()
        {
            List<Post> model = db.Posts.ToList();
            return View(db.Posts.ToList());  // or return View(model);
        }

        public ActionResult Index(int? page, string query)
        {
            var posts = db.Posts.AsQueryable();
            if (!String.IsNullOrWhiteSpace(query))
            {
                posts = posts.Where(p => p.Title.Contains(query) || p.Body.Contains(query));
                ViewBag.Query = query;
            }
            posts = posts.OrderByDescending(p => p.Created);
            return View(posts.ToPagedList(page ?? 1, 3));
            
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }


        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Title,Body,MediaURL")] Post post, HttpPostedFileBase image )
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    if ((fileExtension == ".jpg") || (fileExtension == ".gif") || (fileExtension == ".png"))
                    {
                        var path = Server.MapPath("~/img/Blog/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        post.MediaURL = "/img/Blog/" + fileName;

                        image.SaveAs(Path.Combine(path, fileName));
                    }
                    else
                    {
                        ModelState.AddModelError("image", "Invalid image extension. Only .gif, .jpeg, and .png are allowed.");
                            return View(post);
                    }
                }
            }
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            post.Created = DateTimeOffset.Now.Date;
            post.Updated = DateTimeOffset.Now.Date;
            return View(post);
        }


        // GET: Posts/Edit/5
        [Authorize(Roles = "Moderator,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Created,Updated,Title,Body,MediaURL")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
