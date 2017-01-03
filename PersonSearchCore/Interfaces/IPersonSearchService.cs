using System.Collections.Generic;
using DataAccess.Models;
using PersonSearchServices.Dtos;

namespace PersonSearchServices.Interfaces
{
    public interface IPersonSearchService
    {
        IReadOnlyCollection<PersonDto> GetPeopleByPartialName(string partialName);
    }
}