using Binary_City.Models;
using Binary_City.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Binary_City.Controllers
{
    public class ContactController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetContacts()
        {
            var Contacts = (from db in _BinaryCityModel.Contacts
                           select new ContactVm
                           {
                               Id = db.Id,
                               Email= db.Email,
                               FullName = db.Surname + " " + db.Name,
                               No_Of_Clients = db.ClientsContacts.Count()
                           }).ToList();


            return Json(Contacts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact Contact)
        {
            if (ModelState.IsValid)
            {
                _BinaryCityModel.Contacts.Add(Contact);
                _BinaryCityModel.SaveChanges();

                return RedirectToAction("View", "Contact", new { Id = Contact.Id });

            }
            return View(Contact);
        }

        public ActionResult linking(int Id)
        {
            var allclients = _BinaryCityModel.Clients.ToList();
            LinkingVm linkingVm = new LinkingVm();
            linkingVm.Id = Id;
            linkingVm.Contact = _BinaryCityModel.Contacts.Find(Id);

            linkingVm.ClientList = (allclients.OrderBy(a => a.Name).ToList().Select(x => new SelectListItem()
            {
                Selected = linkingVm.Contact.ClientsContacts.FirstOrDefault(a => a.ClientId == x.Id) != null,
                Text = x.Name,
                Value = x.Id + ""

            }));

            var linkedClints = linkingVm.Contact.ClientsContacts.ToArray();
            ViewBag.SelectedClients = null;

            for (int i = 0; i < linkedClints.Length; i++)
            {
                ViewBag.SelectedClients = i == linkedClints.Length - 1 ? ViewBag.SelectedClients + "" + linkedClints[i].ClientId : ViewBag.SelectedClients + "" + linkedClints[i].ClientId + ",";
            }

            return View("_ContactLinking", linkingVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult linking(LinkingVm linkingVm)
        {

            int Id = Request.Form["id"] == "" ? 0 : Convert.ToInt32(Request.Form["id"]);

            var clients = Request.Form["multiple_value"].Split(',');
            clients = clients.Where(x => ((x != null)&&(x != ""))).ToArray();
           

            var currentcontact=   _BinaryCityModel.Contacts.Find(Id);
            _BinaryCityModel.ClientsContacts.RemoveRange(currentcontact.ClientsContacts);
            _BinaryCityModel.SaveChanges();

            List<ClientsContact> clientsContactList = new List<ClientsContact>();

            foreach (var item in clients)
            {
                clientsContactList.Add(new ClientsContact
                {
                    ClientId = int.Parse(item),
                    Contactid = Id
                });
            }

            _BinaryCityModel.ClientsContacts.AddRange(clientsContactList);
            _BinaryCityModel.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult View(int Id)
        {
            var contact = _BinaryCityModel.Contacts.Find(Id);
            return View(contact);
        }


        public ActionResult GetClientContacts(int Id)
        {
            var Contacts = (from db in _BinaryCityModel.ClientsContacts
                            where db.ClientId == Id
                            select new ContactVm
                            {
                                Id = db.Contacts.Id,
                                Email = db.Contacts.Email,
                                FullName = db.Contacts.Surname + " " + db.Contacts.Name
                            }).ToList();


            return Json(Contacts, JsonRequestBehavior.AllowGet);
        }


    }
}