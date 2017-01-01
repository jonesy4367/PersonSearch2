using System.Collections.Generic;
using System.Web.Mvc;
using Models.PersonSearch;

namespace PersonSearch.Controllers
{
    public class PersonSearchController : Controller
    {
        // GET: PersonSearch
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchPeople(string partialName, string blah)
        {
            var people = new List<Person>
            {
                new Person
                {
                    FullName = "Billy McBilly"
                }
            };

            return Json(people, JsonRequestBehavior.AllowGet);
        }
    }
}