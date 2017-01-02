using System.Collections.Generic;
using DataAccess.Models;

namespace PersonSearchServices.Interfaces
{
    public interface IPersonSearchService
    {
        IReadOnlyCollection<Person> SearchPeople(string partialName);
    }
}