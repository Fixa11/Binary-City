using Binary_City.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Binary_City.Controllers
{
    public class BaseController : Controller
    {

         public BinaryCityModel _BinaryCityModel = new BinaryCityModel();
        // GET: Base
        public BaseController()
        {
        }
    }
}