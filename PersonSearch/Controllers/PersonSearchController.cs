using System.Linq;
using System.Web.Mvc;
using PersonSearch.Models.PersonSearch;
using PersonSearchCore.Interfaces;

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
            var personData = _personSearchService.SearchPeople(partialName);

            return Json(personData
                .Select(p => new PersonModel
                {
                    FullName = $"{p.FirstName} {p.LastName}",
                    Age = p.Age,
                    Address =
                        $"{p.Address.StreetAddress} {p.Address.City.Name}, {p.Address.City.State.Abbreviation} {p.Address.ZipCode}",
                    Interests = p
                        .Interests
                        .Select(i => i.Name)
                        .ToList()
                })
                .ToList());
        }
    }
}