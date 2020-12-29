using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentInfinum.Controllers
{
    [RoutePrefix("ContactView")]
    public class ContactViewController : Controller
    {
        // GET: ContactView
        public ActionResult Index(contacts contact)
        {

            return View(contact);
        }

        [HttpPost]
        public ActionResult UpdateContact(contacts contactOld, contacts contactOld2) // "Old2" -> "New" was not working
        {
            ContactsDatabase.Instance.UpdateContact(contactOld, contactOld2);

            return Json(contactOld2);
        }


        [Route("ViewSingleContact/{id}/{id2}")]
        public ActionResult ViewSingleContact(string id, string id2)
        {
            // check for malicious query
            if (Request.QueryString[id] == null && Request.QueryString[id2] == null)
            {
                contacts c = ContactsDatabase.Instance.GetContact(id, id2);
                return View(c);
            }
            return new EmptyResult();
        }
    }
}