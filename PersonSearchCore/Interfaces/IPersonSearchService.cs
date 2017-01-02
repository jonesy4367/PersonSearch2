using System.Collections.Generic;
using DataModels;

namespace PersonSearchCore.Interfaces
{
    public interface IPersonSearchService
    {
        IReadOnlyCollection<Person> SearchPeople(string partialName);
    }
}