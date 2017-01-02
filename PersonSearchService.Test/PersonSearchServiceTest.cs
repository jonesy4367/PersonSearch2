using System.Collections.Generic;
using System.Data.Entity;
using DataAccess;
using DataAccess.Models;
using Moq;
using NUnit.Framework;

namespace PersonSearchService.Test
{
    [TestFixture]
    public class PersonSearchServiceTest
    {
        private Mock<PeopleContext> _peopleContextMock;

        private Mock<DbSet<Person>> _personDbSetMock;
        private Mock<DbSet<Address>> _addressDbSetMock;
        private Mock<DbSet<City>> _cityDbSetMock;
        private Mock<DbSet<State>> _stateDbSetMock;
        private Mock<DbSet<Interest>> _interestDbSetMock;

        private PersonSearchServices.PersonSearchService _personSearchService;

        private List<Person> _personData;
        private Person person1;
        private Person person2;

        private List<Address> _addressData;
        private Address address1;
        private Address address2;

        private List<City> _cityData;
        private City city1;
        private City city2;

        private List<State> _stateData;
        private State state1;
        private State state2;

        private List<Interest> _interestData;
        private Interest interest1;
        private Interest interest2;

        [SetUp]
        public void Setup()
        {
            GenerateData();

            // TODO: do we really need dbsets to be mocked???
            _personDbSetMock = new Mock<DbSet<Person>>();
            _personDbSetMock.Object.AddRange(_personData);

            _addressDbSetMock = new Mock<DbSet<Address>>();
            _addressDbSetMock.Object.AddRange(_addressData);

            _cityDbSetMock = new Mock<DbSet<City>>();
            _cityDbSetMock.Object.AddRange(_cityData);

            _stateDbSetMock = new Mock<DbSet<State>>();
            _stateDbSetMock.Object.AddRange(_stateData);

            _interestDbSetMock = new Mock<DbSet<Interest>>();
            _interestDbSetMock.Object.AddRange(_interestData);

            _peopleContextMock = new Mock<PeopleContext>();
            _peopleContextMock.Setup(p => p.People).Returns(_personDbSetMock.Object);
            _peopleContextMock.Setup(p => p.Addresses).Returns(_addressDbSetMock.Object);
            _peopleContextMock.Setup(p => p.Cities).Returns(_cityDbSetMock.Object);
            _peopleContextMock.Setup(p => p.States).Returns(_stateDbSetMock.Object);
            _peopleContextMock.Setup(p => p.Interests).Returns(_interestDbSetMock.Object);
        }

        private void GenerateData()
        {
            
        }
    }
}
