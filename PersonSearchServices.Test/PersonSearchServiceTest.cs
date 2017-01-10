using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Abstractions;
using System.Linq;
using DataAccess;
using DataAccess.Models;
using Moq;
using NUnit.Framework;

namespace PersonSearchServices.Test
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

        private PersonSearchService _personSearchService;

        private List<Person> _personData;
        private Person _bobLawblaw;
        private Person _steveSchmeve;

        private List<Address> _addressData;
        private Address _123Street;
        private Address _456Avenue;

        private List<City> _cityData;
        private City _logan;
        private City _nashville;

        private List<State> _stateData;
        private State _utah;
        private State _tennessee;

        private List<Interest> _interestData;
        private Interest _pizza;
        private Interest _cooking;

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

            _personDbSetMock.Setup(p => p.Include(It.IsAny<string>())).Returns(_personDbSetMock.Object);

            _fileSystemMock = new Mock<IFileSystem>();

            _personSearchService = new PersonSearchService(
                _personContextMock.Object,
                _fileSystemMock.Object,
                RootDirectory);
        }

        #region GetPeopleIncludeRelatedDataByPartialName() Tests

        [Test]
        [TestCase("bo")]
        [TestCase("BO")]
        public void GetPeopleIncludeRelatedDataByPartialName_PartialNameIsBo_ReturnsBobLawblaw(string partialName)
        {
            var expectedFullName = $"{_bobLawblaw.FirstName} {_bobLawblaw.LastName}";
            var expectedAddress = $"{_123Street.StreetAddress} {_logan.Name}, {_utah.Abbreviation} {_123Street.ZipCode}";
            var expectedAge = _bobLawblaw.Age;
            var expectedInterest = _pizza.Name;

            var expectedPhoto = new byte[] {0xA4, 0x54, 0x83};

            // Arrange
            _fileSystemMock
                .Setup(f => f.File.ReadAllBytes(RootDirectory + "//PersonImages//" + _bobLawblaw.ImageFileName))
                .Returns(expectedPhoto);
            
            // Act
            var people = _personSearchService.GetPeopleIncludeRelatedDataByPartialName(partialName);

            // Assert
            var actualPerson = people.Single();
            Assert.AreEqual(expectedFullName, actualPerson.FullName);
            Assert.AreEqual(expectedAddress, actualPerson.Address);
            Assert.AreEqual(expectedAge, actualPerson.Age);
            Assert.AreSame(expectedPhoto, actualPerson.Photo);

            var actualInterest = actualPerson.Interests.Single();
            Assert.AreEqual(expectedInterest, actualInterest);

            // Make sure the context call includes related data
            _personDbSetMock.Verify(p => p.Include("Address"));
            _personDbSetMock.Verify(p => p.Include("Interests"));
            _personDbSetMock.Verify(p => p.Include("Address.City"));
            _personDbSetMock.Verify(p => p.Include("Address.City.State"));
        }

        [Test]
        [TestCase("sch")]
        [TestCase("SCH")]
        public void GetPeopleIncludeRelatedDataByPartialName_PartialNameIsSch_ReturnsSteveSchmeve(string partialName)
        {
            var expectedFullName = $"{_steveSchmeve.FirstName} {_steveSchmeve.LastName}";

            // Act
            var people = _personSearchService.GetPeopleIncludeRelatedDataByPartialName(partialName);

            // Assert
            var actualPerson = people.Single();
            Assert.AreEqual(expectedFullName, actualPerson.FullName);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void GetPeopleIncludeRelatedDataByPartialName_PartialNameIsNullOrEmpty_ReturnsAllPeople(string partialName)
        {
            var expectedFullName1 = $"{_bobLawblaw.FirstName} {_bobLawblaw.LastName}";
            var expectedFullName2 = $"{_steveSchmeve.FirstName} {_steveSchmeve.LastName}";

            // Arrange
            _fileSystemMock.Setup(f => f.File.ReadAllBytes(It.IsAny<string>())).Returns(new byte[] {});

            // Act
            var people = _personSearchService.GetPeopleIncludeRelatedDataByPartialName(partialName);

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

            _bobLawblaw.Address = _123Street;
            _bobLawblaw.Interests.Add(_pizza);

            _steveSchmeve.Address = _456Avenue;
            _steveSchmeve.Interests.Add(_pizza);
            _steveSchmeve.Interests.Add(_cooking);

            _123Street.City = _logan;
            _123Street.People.Add(_bobLawblaw);

            _456Avenue.City = _nashville;
            _456Avenue.People.Add(_steveSchmeve);

            _logan.State = _utah;
            _logan.Addresses.Add(_123Street);

            _nashville.State = _tennessee;
            _nashville.Addresses.Add(_456Avenue);

            _utah.Cities.Add(_logan);
            _tennessee.Cities.Add(_nashville);

            _pizza.People.Add(_bobLawblaw);
            _pizza.People.Add(_steveSchmeve);

            _cooking.People.Add(_steveSchmeve);
        }

        private List<Person> BuildPeople()
        {
            _bobLawblaw = new Person
            {
                PersonId = 1,
                FirstName = "Bob",
                LastName = "Lawblaw",
                Age = 43,
                ImageFileName = "apictureofme",
                Interests = new List<Interest>()
            };

            _steveSchmeve = new Person
            {
                PersonId = 2,
                FirstName = "Steve",
                LastName = "Schmeve",
                Age = 36,
                Interests = new List<Interest>()
            };

            return new List<Person>
            {
                _bobLawblaw,
                _steveSchmeve
            };
        }

        private List<Address> BuildAddresses()
        {
            _123Street = new Address
            {
                AddressId = 1,
                StreetAddress = "123 Street St",
                ZipCode = "83483",
                People = new List<Person>()
            };

            _456Avenue = new Address
            {
                AddressId = 2,
                StreetAddress = "456 Avenue Ave",
                ZipCode = "90298",
                People = new List<Person>()
            };

            return new List<Address>
            {
                _123Street,
                _456Avenue
            };
        }

        private List<City> BuildCities()
        {
            _logan = new City
            {
                Name = "Logan",
                Addresses = new List<Address>()
            };

            _nashville = new City
            {
                Name = "Nashville",
                Addresses = new List<Address>()
            };

            return new List<City>
            {
                _logan,
                _nashville
            };
        }

        private List<State> BuildStates()
        {
            _utah = new State
            {
                Name = "Utah",
                Abbreviation = "UT",
                Cities = new List<City>()
            };

            _tennessee = new State
            {
                Name = "Tennessee",
                Abbreviation = "TN",
                Cities = new List<City>()
            };

            return new List<State>
            {
                _utah,
                _tennessee
            };
        }

        private List<Interest> BuildInterests()
        {
            _pizza = new Interest
            {
                Name = "Pizza",
                People = new List<Person>()
            };

            _cooking = new Interest
            {
                Name = "Cooking",
                People = new List<Person>()
            };

            return new List<Interest>
            {
                _pizza,
                _cooking
            };
        }
    }
}
