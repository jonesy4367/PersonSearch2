using System.Collections.Generic;
using DataAccess;
using DataAccess.Models;
using PersonSearchServices.Interfaces;
using System.Linq;
using PersonSearchServices.Dtos;

namespace PersonSearchServices
{
    public class PersonSearchService : IPersonSearchService
    {
        private readonly PeopleContext _peopleContext;

        public PersonSearchService(PeopleContext peopleContext)
        {
            _peopleContext = peopleContext;
        }

        public IReadOnlyCollection<PersonDto> GetPeopleByPartialName(string partialName)
        {
            return _peopleContext
                .People
                .Where(p => p.FirstName.ToLower().Contains(partialName))
                .Select(p => new PersonDto
                {
                    FullName = $"{p.FirstName} {p.LastName}",
                    Address =
                        $"{p.Address.StreetAddress} {p.Address.City.Name}, {p.Address.City.State.Abbreviation} {p.Address.ZipCode}",
                    Age = p.Age,
                    Interests = p.Interests
                        .Select(i => i.Name)
                        .ToList()
                })
                .ToList();
        }
    }
}
