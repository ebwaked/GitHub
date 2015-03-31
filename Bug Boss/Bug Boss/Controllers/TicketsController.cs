﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bug_Boss.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace Bug_Boss.Controllers
{
    [Authorize(Roles="Submitter,Developer,Administrator,Project Manager")]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);
            return View(tickets.ToList());
        }
        
        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName", "LastName");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", "LastName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                ticket.Created = System.DateTimeOffset.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "LastName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "LastName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            TempData["old_ticket"] = ticket;
            if (ticket == null)
            {
                return HttpNotFound();
            }
            TempData["old_ticket"] = ticket;
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                Ticket old_ticket = (Ticket)TempData["old_ticket"];
                var userId = db.Users.Single(u => u.UserName == User.Identity.Name).Id;

                if (old_ticket.Description != ticket.Description)
                {
                    db.TicketHistories.Add(new TicketHistory
                        {
                            Property = "Description",
                            Changed = DateTimeOffset.Now,
                            OldValue = old_ticket.Description,
                            NewValue = ticket.Description,
                            TicketId = ticket.Id,
                            UserId = userId
                        });
                }

                if (old_ticket.Description != ticket.Title)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "Title",
                        Changed = DateTimeOffset.Now,
                        OldValue = old_ticket.Title,
                        NewValue = ticket.Title,
                        TicketId = ticket.Id,
                        UserId = userId
                    });
                }

                if (old_ticket.TicketTypeId != ticket.TicketTypeId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "TicketTypeId",
                        Changed = DateTimeOffset.Now,
                        OldValue = old_ticket.TicketType.Name,
                        NewValue = db.TicketTypes.FirstOrDefault(p=>p.Id == ticket.TicketTypeId).Name,
                        TicketId = ticket.Id,
                        UserId = userId
                    });
                }

                if (old_ticket.TicketPriorityId != ticket.TicketPriorityId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "TicketPriority",
                        Changed = DateTimeOffset.Now,
                        OldValue = old_ticket.TicketPriority.Name,
                        NewValue = db.TicketPriorities.FirstOrDefault(p => p.Id == ticket.TicketPriorityId).Name,
                        TicketId = ticket.Id,
                        UserId = userId
                    });
                }

                if (old_ticket.TicketStatusId != ticket.TicketStatusId)
                {
                    db.TicketHistories.Add(new TicketHistory
                    {
                        Property = "TicketStatusId",
                        Changed = DateTimeOffset.Now,
                        OldValue = old_ticket.TicketStatus.Name,
                        NewValue = db.TicketStatuses.FirstOrDefault(p => p.Id == ticket.TicketStatusId).Name,
                        TicketId = ticket.Id,
                        UserId = userId
                    });
                }


                if (old_ticket.AssignedUserId != ticket.AssignedUserId)
                {
                    
                        var assignedUser = db.Users.Find(ticket.AssignedUserId);
                        var projectName = db.Projects.Find(ticket.ProjectId).Name;
                        var mailer = new EmailService();
                        mailer.SendAsync(new IdentityMessage {Destination = assignedUser.Email, Subject = "New Ticket", Body = "You have been added to a ticket titled" + ticket.Title + "on project" + projectName + "."});

                        db.TicketHistories.Add(new TicketHistory
                        {

                        Property = "AssignedUser",
                        Changed = DateTimeOffset.Now,
                        OldValue = old_ticket.AssignedUser.Email,
                        NewValue = ticket.AssignedUser.Email,
                        TicketId = ticket.Id,
                        UserId = userId
                    });
                
                }


                ticket.Updated = DateTimeOffset.Now;
                db.Entry(ticket).State = EntityState.Modified;
                db.Entry(ticket).Property(p => p.Created).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new {id = ticket.Id});
            }

            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "LastName", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "LastName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
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

        public ActionResult AddAttachment([Bind(Include = "TicketId,Attachment,Description")] TicketAttachment post, HttpPostedFileBase image)
        {
            if (ModelState.IsValid) 
            {
                if (image != null)
                {
                    if (image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        if ((fileExtension == ".jpg") || (fileExtension == ".gif") || (fileExtension == ".png") || (fileExtension == ".pdf") || (fileExtension == ".jpeg"))
                        {
                            var path = Server.MapPath("~/Uploads/Attachments/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            var filePath = Path.Combine(path, fileName);
                            image.SaveAs(filePath);

                            post.FilePath = filePath;
                            post.FileUrl = "/Uploads/Attachments/" + fileName;
                            post.UserId = User.Identity.GetUserId();
                            post.Created = System.DateTimeOffset.Now;

                        }
                        else
                        {
                            TempData["AttachError"] = "Invalid image extension. Only .jpg, .jpeg, .pdf, .gif, or .png";
                            return RedirectToAction("Details", new { Id = post.TicketId });
                        }
                    }
               }
                if (ModelState.IsValid)
                {
                    post.Created = System.DateTimeOffset.Now;
                    db.TicketAttachments.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { Id = post.TicketId });
                }
                
           }
            return RedirectToAction("Details", new { Id = post.TicketId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment([Bind(Include = "TicketId,Comment")] TicketComment addition)
        {
            if (ModelState.IsValid) 
            {
                addition.UserId = User.Identity.GetUserId();
                addition.Created = System.DateTimeOffset.Now;
                db.TicketComments.Add(addition);
                db.SaveChanges();

                return RedirectToAction("Details", new { Id = addition.TicketId });
            }

            return RedirectToAction("Details", new { Id = addition.TicketId });
        }
    }
}
