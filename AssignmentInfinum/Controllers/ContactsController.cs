using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentInfinum.Controllers
{
    public class ContactsController : Controller
    {
        List<contacts> contacts2;
        // GET: Contacts
        public ActionResult Index(int? page)
        {
            ContactsEntities1 contactsEntities = new ContactsEntities1();



            int pageSize = 3;
            int pageNumber = (page ?? 1);

            contacts2 = new List<contacts>();
            contacts2.Add(new contacts() { name = "asd", birthdate = new DateTime(1900, 1, 1), address = "lda" });
            contacts2.Add(new contacts() { name = "sad", birthdate = new DateTime(1900, 1, 1), address = "da" });
            contacts2.Add(new contacts() { name = "as", birthdate = new DateTime(1900, 1, 1), address = "la" });
            contacts2.Add(new contacts() { name = "sd", birthdate = new DateTime(1900, 1, 1), address = "lds" });
            contacts2.Add(new contacts() { name = "a", birthdate = new DateTime(1900, 1, 1), address = "l" });
            contacts2.Add(new contacts() { name = "d", birthdate = new DateTime(1900, 1, 1), address = "a" });
            contacts2.Add(new contacts() { name = "s", birthdate = new DateTime(1900, 1, 1), address = "ld" });
            PagedList<contacts> contacts1 = new PagedList<contacts>(contacts2, pageNumber, pageSize);

            return View(contacts2.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public JsonResult InsertContact(contacts contact)
        {
            using (ContactsEntities1 contactsModel = new ContactsEntities1())
            {
                contactsModel.contacts.Add(contact);
                contactsModel.SaveChanges();
            }

            return Json(contact);
        }

        [HttpPost]
        public ActionResult UpdateContact(contacts contact)
        {
            using (ContactsEntities1 contactsModel = new ContactsEntities1())
            {
                contactsModel.contacts.Add(contact);
                contactsModel.SaveChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteContact(contacts contacts)
        {
            using (ContactsEntities1 contactsModel = new ContactsEntities1())
            {
                //contacts deleteContact = (from c in contactsModel.contacts
                //                                where c.name == name && c.address == address
                //                                select c).FirstOrDefault();

                //contactsModel.contacts.Remove(deleteContact);
                contactsModel.SaveChanges();
            }

            return new EmptyResult();
        }

        [Route("Contacts/Test")]
        [HttpPost]
        public ActionResult Test(contacts contacts)
        {
            Console.WriteLine("gkgkkggk");
            return new EmptyResult();
        }
    }
}