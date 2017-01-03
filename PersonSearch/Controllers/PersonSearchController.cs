﻿using System.Web.Mvc;
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
            var people = _personSearchService.GetPeopleByPartialName(partialName);
            return Json(people);
        }
    }
}