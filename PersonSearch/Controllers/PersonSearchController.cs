using System.Linq;
using System.Web.Mvc;
using PersonSearch.Models.PersonSearch;
using PersonSearchServices.Interfaces;

namespace PersonSearch.Controllers
{
    public class PersonSearchController : Controller
    {
        private readonly IPersonSearchService _personSearchService;

        public PersonSearchController(IPersonSearchService personSearchService)
        {
            _personSearchService = personSearchService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SearchPeople(string partialName)
        {
            return Json("");
        }
    }
}