using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Abstractions;
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
        private Mock<PersonContext> _personContextMock;

        private Mock<DbSet<Person>> _personDbSetMock;
        private Mock<DbSet<Address>> _addressDbSetMock;
        private Mock<DbSet<City>> _cityDbSetMock;
        private Mock<DbSet<State>> _stateDbSetMock;
        private Mock<DbSet<Interest>> _interestDbSetMock;

        private Mock<IFileSystem> _fileSystemMock;

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

        private const string RootDirectory = "C:\\";

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

            _personContextMock = new Mock<PersonContext>();
            _personContextMock.Setup(p => p.People).Returns(_personDbSetMock.Object);
            _personContextMock.Setup(p => p.Addresses).Returns(_addressDbSetMock.Object);
            _personContextMock.Setup(p => p.Cities).Returns(_cityDbSetMock.Object);
            _personContextMock.Setup(p => p.States).Returns(_stateDbSetMock.Object);
            _personContextMock.Setup(p => p.Interests).Returns(_interestDbSetMock.Object);

            _fileSystemMock = new Mock<IFileSystem>();

            _personSearchService = new PersonSearchServices.PersonSearchService(
                _personContextMock.Object,
                _fileSystemMock.Object,
                RootDirectory);
        }

        #region GetPeopleByPartialName() Tests

        [Test]
        [TestCase("bo")]
        [TestCase("BO")]
        public void GetPeopleByPartialName_PartialNameIsBo_ReturnsBobLawblaw(string partialName)
        {
            var expectedFullName = $"{_person1.FirstName} {_person1.LastName}";
            var expectedAddress = $"{_address1.StreetAddress} {_city1.Name}, {_state1.Abbreviation} {_address1.ZipCode}";
            var expectedAge = _person1.Age;
            var expectedInterest = _interest1.Name;

            var expectedPhoto = new byte[] {0xA4, 0x54, 0x83};

            // Arrange
            _fileSystemMock
                .Setup(f => f.File.ReadAllBytes(RootDirectory + "//Images//" + _person1.ImageFileName))
                .Returns(expectedPhoto);
            
            // Act
            var people = _personSearchService.GetPeopleByPartialName(partialName);

            // Assert
            var actualPerson = people.Single();
            Assert.AreEqual(expectedFullName, actualPerson.FullName);
            Assert.AreEqual(expectedAddress, actualPerson.Address);
            Assert.AreEqual(expectedAge, actualPerson.Age);
            Assert.AreSame(expectedPhoto, actualPerson.Photo);

            var actualInterest = actualPerson.Interests.Single();
            Assert.AreEqual(expectedInterest, actualInterest);
        }

        [Test]
        [TestCase("sch")]
        [TestCase("SCH")]
        public void GetPeopleByPartialName_PartialNameIsSch_ReturnsSteveSchmeve(string partialName)
        {
            var expectedFullName = $"{_person2.FirstName} {_person2.LastName}";

            // Act
            var people = _personSearchService.GetPeopleByPartialName(partialName);

            // Assert
            var actualPerson = people.Single();
            Assert.AreEqual(expectedFullName, actualPerson.FullName);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void GetPeopleByPartialName_PartialNameIsNullOrEmpty_ReturnsAllPeople(string partialName)
        {
            var expectedFullName1 = $"{_person1.FirstName} {_person1.LastName}";
            var expectedFullName2 = $"{_person2.FirstName} {_person2.LastName}";

            // Arrange
            _fileSystemMock.Setup(f => f.File.ReadAllBytes(It.IsAny<string>())).Returns(new byte[] {});

            // Act
            var people = _personSearchService.GetPeopleByPartialName(partialName);

            // Assert
            Assert.AreEqual(2, people.Count);
            Assert.IsTrue(people.Any(p => p.FullName == expectedFullName1));
            Assert.IsTrue(people.Any(p => p.FullName == expectedFullName2));
        }

        #endregion

        private static void SetupMock<T>(Mock dbSetMock, IQueryable<T> data) where T : class
        {
            var dbSetQueryable = dbSetMock.As<IQueryable<T>>();
            dbSetQueryable.Setup(p => p.Provider).Returns(data.Provider);
            dbSetQueryable.Setup(p => p.Expression).Returns(data.Expression);
            dbSetQueryable.Setup(p => p.ElementType).Returns(data.ElementType);
            dbSetQueryable.Setup(p => p.GetEnumerator()).Returns(data.GetEnumerator);
        }
        
        private void GenerateData()
        {
            _personData = BuildPeople();
            _addressData = BuildAddresses();
            _cityData = BuildCities();
            _stateData = BuildStates();
            _interestData = BuildInterests();

            _person1.Address = _address1;
            _person1.Interests.Add(_interest1);

            _person2.Address = _address2;
            _person2.Interests.Add(_interest1);
            _person2.Interests.Add(_interest2);

            _address1.City = _city1;
            _address1.People.Add(_person1);

            _address2.City = _city2;
            _address2.People.Add(_person2);

            _city1.State = _state1;
            _city1.Addresses.Add(_address1);

            _city2.State = _state2;
            _city2.Addresses.Add(_address2);

            _state1.Cities.Add(_city1);
            _state2.Cities.Add(_city2);

            _interest1.People.Add(_person1);
            _interest1.People.Add(_person2);

            _interest2.People.Add(_person2);
        }

        private List<Person> BuildPeople()
        {
            _person1 = new Person
            {
                PersonId = 1,
                FirstName = "Bob",
                LastName = "Lawblaw",
                Age = 43,
                ImageFileName = "apictureofme",
                Interests = new List<Interest>()
            };

            _person2 = new Person
            {
                PersonId = 2,
                FirstName = "Steve",
                LastName = "Schmeve",
                Age = 36,
                Interests = new List<Interest>()
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
                AddressId = 1,
                StreetAddress = "123 Street St",
                ZipCode = "83483",
                People = new List<Person>()
            };

            _address2 = new Address
            {
                AddressId = 2,
                StreetAddress = "456 Avenue Ave",
                ZipCode = "90298",
                People = new List<Person>()
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
                Addresses = new List<Address>()
            };

            _city2 = new City
            {
                Name = "Nashville",
                Addresses = new List<Address>()
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
                Cities = new List<City>()
            };

            _state2 = new State
            {
                Name = "Tennessee",
                Abbreviation = "TN",
                Cities = new List<City>()
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
                People = new List<Person>()
            };

            _interest2 = new Interest
            {
                Name = "Cooking",
                People = new List<Person>()
            };

            return new List<Interest>
            {
                _interest1,
                _interest2
            };
        }
    }
}
