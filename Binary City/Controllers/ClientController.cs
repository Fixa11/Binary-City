using Binary_City.Models;
using Binary_City.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Binary_City.Controllers
{
    public class ClientController : BaseController
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCleints()
        {
           var clients= (from db in  _BinaryCityModel.Clients
                          select new ClientVm
                          { 
                              Id = db.Id,
                              Name = db.Name,
                              ClientCode = db.ClientCode,
                              No_Of_Contacts = db.ClientsContacts.Count()
                          }).ToList();


           return Json(clients, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _BinaryCityModel.Clients.Add(client);
                _BinaryCityModel.SaveChanges();

                client.ClientCode = client.Name.Length == 1 ? client.Name + "AA" + client.Id.ToString("D3") :
                                    client.Name.Length == 2 ? client.Name + "A" + client.Id.ToString("D3") :
                                    client.Name.Substring(0, 3) + client.Id.ToString("D3") ;

                _BinaryCityModel.Entry(client).State = EntityState.Modified;
                _BinaryCityModel.SaveChanges();

                return View("Display", client );
            }
            return View(client);
        }


        public ActionResult linking(int Id)
        {
            var allContacts = _BinaryCityModel.Contacts.ToList();
            LinkingVm linkingVm = new LinkingVm();
            linkingVm.Id = Id;
            linkingVm.Client = _BinaryCityModel.Clients.Find(Id);

            linkingVm.ContactList = (allContacts.OrderBy(a => a.Name).ToList().Select(x => new SelectListItem()
            {
                Selected = linkingVm.Client.ClientsContacts.FirstOrDefault(a => a.Contactid == x.Id) != null,
                Text = x.Email,
                Value = x.Id + ""
            }));

            var linkedConctacts = linkingVm.Client.ClientsContacts.ToArray();
            ViewBag.SelectedContacts = null;

            for (int i = 0; i < linkedConctacts.Length; i++)
            {
                ViewBag.SelectedContacts = i == linkedConctacts.Length - 1 ? ViewBag.SelectedContacts + "" + linkedConctacts[i].Contactid : ViewBag.SelectedContacts + "" + linkedConctacts[i].Contactid + ",";
            }

            return View("_Linking", linkingVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult linking(LinkingVm linkingVm)
        {

            int Id = Request.Form["id"] == "" ? 0 : Convert.ToInt32(Request.Form["id"]);

            var contacts = Request.Form["multiple_value"].Split(',');
            contacts = contacts.Where(x => ((x != null) && (x != ""))).ToArray();


            var currentClient= _BinaryCityModel.Clients.Find(Id);
            _BinaryCityModel.ClientsContacts.RemoveRange(currentClient.ClientsContacts);
            _BinaryCityModel.SaveChanges();

            List<ClientsContact> clientsContactList = new List<ClientsContact>();

            foreach (var item in contacts)
            {
                clientsContactList.Add(new ClientsContact
                {
                    ClientId = Id,
                    Contactid = int.Parse(item)
                });
            }

            _BinaryCityModel.ClientsContacts.AddRange(clientsContactList);
            _BinaryCityModel.SaveChanges();


            return RedirectToAction("Index");
        }


        public ActionResult View(int Id)
        {
            var client = _BinaryCityModel.Clients.Find(Id);
            return View("Display", client);
        }

        public ActionResult GetContactCleints(int Id)
        {
            var clients = (from db in _BinaryCityModel.ClientsContacts
                           where db.Contactid == Id
                           select new ClientVm
                           {
                               Id = db.Clients.Id,
                               Name = db.Clients.Name,
                               ClientCode = db.Clients.ClientCode
                           }).ToList();


            return Json(clients, JsonRequestBehavior.AllowGet);
        }

        

    }
}