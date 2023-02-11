using Binary_City.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Binary_City.ViewModel
{
    public class LinkingVm
    {
        public int Id = 0;
        public Client Client = new Client();
        public Contact Contact = new Contact();
        public IEnumerable<SelectListItem> ContactList { get; set; }
        public IEnumerable<SelectListItem> ClientList { get; set; }
    }
}