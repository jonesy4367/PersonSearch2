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
        private readonly PersonContext _personContext;

        public PersonSearchService(PersonContext personContext)
        {
            _personContext = personContext;
        }

        public IReadOnlyCollection<PersonDto> GetPeopleByPartialName(string partialName)
        {
            IQueryable<Person> peopleData = _personContext.People;

            if (!string.IsNullOrWhiteSpace(partialName))
            {
                peopleData = peopleData
                    .Where(p =>
                        p.FirstName.ToLower().Contains(partialName.ToLower()) ||
                        p.LastName.ToLower().Contains(partialName.ToLower()));
            }

            return peopleData
                .ToList()
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
