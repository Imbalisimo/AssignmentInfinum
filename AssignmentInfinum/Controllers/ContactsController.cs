﻿using Devart.Data.PostgreSql;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentInfinum.Controllers
{
    [RoutePrefix("Contacts")]
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(ContactsDatabase.Instance.GetAllContacts().ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult InsertContact(contacts contact)
        {
            ContactsDatabase.Instance.InsertNewContact(contact);
            return Json(contact);
        }

        [HttpPost]
        public ActionResult DeleteContact(contacts contact)
        {
            ContactsDatabase.Instance.DeleteContact(contact);

            return new EmptyResult();
        }
    }
}