﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Icarus.Models;

namespace Icarus.Controllers
{
    public class ResidentsController : Controller
    {
        private ICARUSDBEntities db = new ICARUSDBEntities();

        [Route("Residents/")]
        // GET: Residents
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                int res = db.tblResidents.Max(x => x.IDResident);
                tblResident resident = new tblResident();
                resident.IDResident = res + 1;
                ViewData["Resident"] = resident;
                return View(db.tblResidents.ToList().OrderByDescending(p => p.IDResident).ToList());
            }
            else {
                return RedirectToAction("Login", "Login");
            }
        }

        // GET: Residents/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Username"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblResident tblResident = db.tblResidents.Find(id);
                if (tblResident == null)
                {
                    return HttpNotFound();
                }
                return View(tblResident);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public PartialViewResult DetailsPartial(int id)
        {
            tblResident resident = db.tblResidents.Find(id);
            return PartialView("_DetailsPartial", resident);
        }

        // GET: Residents/Create
        public ActionResult Create()
        {
            if (Session["Username"] != null)
            {
                if (Session["isADG"].ToString() == "Y" || Session["isEDG"].ToString() == "Y" || Session["isAAG"].ToString() == "Y") {
                    return View();
                }
                return RedirectToAction("Index", "Residents");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        // POST: Residents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDResident,Lastname,Firstname,Middlename,Nickname,Birthdate,Age,Sex,Codep,Relationship,ContactNumber,EmailAddress")] tblResident tblResident)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.tblResidents.Add(tblResident);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(tblResident);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        // GET: Residents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Username"] != null)
            {
                if (Session["isADG"].ToString() == "Y" || Session["isEDG"].ToString() == "Y" || Session["isAAG"].ToString() == "Y")
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    tblResident tblResident = db.tblResidents.Find(id);
                    if (tblResident == null)
                    {
                        return HttpNotFound();
                    }
                    return View(tblResident);
                }
                return RedirectToAction("Index", "Residents");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        [HttpGet]
        public PartialViewResult EditPartial(int id)
        {
            tblResident resident = db.tblResidents.Find(id);
            return PartialView("_EditPartial", resident);
        }

        // POST: Residents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDResident,Lastname,Firstname,Middlename,Nickname,Birthdate,Age,Sex,Codep,Relationship,ContactNumber,EmailAddress")] tblResident tblResident)
        {
            if (Session["Username"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tblResident).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tblResident);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        // GET: Residents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Username"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tblResident tblResident = db.tblResidents.Find(id);
                if (tblResident == null)
                {
                    return HttpNotFound();
                }
                return View(tblResident);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
        }

        // POST: Residents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Username"] != null)
            {
                tblResident tblResident = db.tblResidents.Find(id);
                db.tblResidents.Remove(tblResident);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            
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
