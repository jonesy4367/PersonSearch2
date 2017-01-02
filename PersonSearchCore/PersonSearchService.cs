using System;
using System.Collections.Generic;
using DataAccess;
using DataAccess.Models;
using PersonSearchServices.Interfaces;

namespace PersonSearchServices
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly PeopleContext _peopleContext;

        public PersonSearchService(PeopleContext peopleContext)
        {
            _peopleContext = peopleContext;
        }

        public IReadOnlyCollection<Person> SearchPeople(string partialName)
        {
            throw new NotImplementedException();
        }
    }
}
