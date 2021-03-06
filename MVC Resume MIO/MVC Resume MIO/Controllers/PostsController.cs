﻿using System;
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
                return RedirectToAction("Details", new { Slug = comment.ParentPost.Slug});
            }

            comment.Created = DateTimeOffset.Now;
            comment.Updated = DateTimeOffset.Now;
            return View(comment);
        }


        
        // GET: Posts
        [Authorize(Roles="Admin")]
        public ActionResult AdminIndex()
        {
            var posts = db.Posts.ToList();
            posts.ForEach(p => p.Slug = StringUtilities.URLFriendly(p.Title));
            //List<Post> model = db.Posts.ToList();
            return View(db.Posts.ToList());  // or return View(model);
        }


        public ActionResult Index(int? page, string query)
        {
            var posts = db.Posts.AsQueryable();
            if (!String.IsNullOrWhiteSpace(query))
            {
                posts = posts.Where(p => p.Title.Contains(query) || p.Body.Contains(query))
                .Union(db.Posts.Where(p => p.Comment.Any(c => c.Body.Contains(query))))
                .Union(db.Posts.Where(p => p.Comment.Any(c => c.Author.DisplayName.Contains(query))))
                .Union(db.Posts.Where(p => p.Comment.Any(c => c.Author.FirstName.Contains(query))))
                .Union(db.Posts.Where(p => p.Comment.Any(c => c.UpdateReason.Contains(query))));

                ViewBag.Query = query;
            }
            posts = posts.OrderByDescending(p => p.Created);
            return View(posts.ToPagedList(page ?? 1, 3));
            
        }

        
       

        // GET: Posts/Details/5
        public ActionResult Details(string Slug)
        {
            if (String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.FirstOrDefault(p => p.Slug == Slug);
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
                var Slug = StringUtilities.URLFriendly(post.Title);

                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid Title.");
                    return View(post);
                }
                
                if( db.Posts.Any(p => p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(post);
                }

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
                ///create slug here
                post.Slug = Slug;
                db.Posts.Add(post);
                post.Created = System.DateTimeOffset.Now;
                post.Updated = System.DateTimeOffset.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
                db.Posts.Attach(post);
                db.Entry(post).Property(p => p.Title).IsModified = true;
                db.Entry(post).Property(p => p.ID).IsModified = true;
                db.Entry(post).Property(p => p.Updated).IsModified = true;
                db.Entry(post).Property(p => p.Body).IsModified = true;
                db.Entry(post).Property(p => p.MediaURL).IsModified = true;
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
