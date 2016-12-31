using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonSearch.Controllers
{
    public class PersonSearchController : Controller
    {
        // GET: PersonSearch
        public ActionResult Index()
        {
            return View();
        }
    }
}