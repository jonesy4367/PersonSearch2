using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        private Person _person1;
        private Person _person2;

        private List<Address> _addressData;
        private Address _address1;
        private Address _address2;

        private List<City> _cityData;
        private City _city1;
        private City _city2;

        private List<State> _stateData;
        private State _state1;
        private State _state2;

        private List<Interest> _interestData;
        private Interest _interest1;
        private Interest _interest2;

        [SetUp]
        public void Setup()
        {
            GenerateData();

            _personDbSetMock = new Mock<DbSet<Person>>();
            SetupMock(_personDbSetMock, _personData.AsQueryable());

            _addressDbSetMock = new Mock<DbSet<Address>>();
            SetupMock(_addressDbSetMock, _addressData.AsQueryable());

            _cityDbSetMock = new Mock<DbSet<City>>();
            SetupMock(_cityDbSetMock, _cityData.AsQueryable());

            _stateDbSetMock = new Mock<DbSet<State>>();
            SetupMock(_stateDbSetMock, _stateData.AsQueryable());

            _interestDbSetMock = new Mock<DbSet<Interest>>();
            SetupMock(_interestDbSetMock, _interestData.AsQueryable());

            _peopleContextMock = new Mock<PeopleContext>();
            _peopleContextMock.Setup(p => p.People).Returns(_personDbSetMock.Object);
            _peopleContextMock.Setup(p => p.Addresses).Returns(_addressDbSetMock.Object);
            _peopleContextMock.Setup(p => p.Cities).Returns(_cityDbSetMock.Object);
            _peopleContextMock.Setup(p => p.States).Returns(_stateDbSetMock.Object);
            _peopleContextMock.Setup(p => p.Interests).Returns(_interestDbSetMock.Object);
        }

        private void GenerateData()
        {
            _personData = BuildPeople();
            _addressData = BuildAddresses();
            _cityData = BuildCities();
            _stateData = BuildStates();
            _interestData = BuildInterests();
        }

        private List<Person> BuildPeople()
        {
            _person1 = new Person
            {
                PersonId = 1,
                FirstName = "Bob",
                LastName = "LawBlob",
                Age = 43,
                Address = _address1,
                Interests = new List<Interest>
                {
                    _interest1
                }
            };

            _person2 = new Person
            {
                PersonId = 2,
                FirstName = "Steve",
                LastName = "Schmeve",
                Age = 36,
                Address = _address2,
                Interests = new List<Interest>
                {
                    _interest1,
                    _interest2
                }
            };

            return new List<Person>
            {
                _person1,
                _person2
            };
        }

        private List<Address> BuildAddresses()
        {
            _address1 = new Address
            {
                StreetAddress = "123 Street St",
                ZipCode = "83483",
                City = _city1,
                People = new List<Person>
                {
                    _person1
                }
            };

            _address2 = new Address
            {
                StreetAddress = "456 Avenue Ave",
                ZipCode = "90298",
                City = _city2,
                People = new List<Person>
                {
                    _person2
                }
            };

            return new List<Address>
            {
                _address1,
                _address2
            };
        }

        private List<City> BuildCities()
        {
            _city1 = new City
            {
                Name = "Logan",
                State = _state1,
                Addresses = new List<Address>
                {
                    _address1
                }
            };

            _city2 = new City
            {
                Name = "Nashville",
                State = _state2,
                Addresses = new List<Address>
                {
                    _address2
                }
            };

            return new List<City>
            {
                _city1,
                _city2
            };
        }

        private List<State> BuildStates()
        {
            _state1 = new State
            {
                Name = "Utah",
                Abbreviation = "UT",
                Cities = new List<City>
                {
                    _city1
                }
            };

            _state2 = new State
            {
                Name = "Tennessee",
                Abbreviation = "TN",
                Cities = new List<City>
                {
                    _city2
                }
            };

            return new List<State>
            {
                _state1,
                _state2
            };
        }

        private List<Interest> BuildInterests()
        {
            _interest1 = new Interest
            {
                Name = "Pizza",
                People = new List<Person>
                {
                    _person1,
                    _person2
                }
            };

            _interest2 = new Interest
            {
                Name = "Cooking",
                People = new List<Person>
                {
                    _person2
                }
            };

            return new List<Interest>
            {
                _interest1,
                _interest2
            };
        }

        private static void SetupMock<T>(Mock dbSetMock, IQueryable<T> data) where T : class
        {
            var dbSetQueryable = dbSetMock.As<IQueryable<T>>();
            dbSetQueryable.Setup(p => p.Provider).Returns(data.Provider);
            dbSetQueryable.Setup(p => p.Expression).Returns(data.Expression);
            dbSetQueryable.Setup(p => p.ElementType).Returns(data.ElementType);
            dbSetQueryable.Setup(p => p.GetEnumerator()).Returns(data.GetEnumerator);
        }
    }
}
