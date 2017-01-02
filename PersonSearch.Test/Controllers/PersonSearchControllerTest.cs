using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Models;
using Moq;
using NUnit.Framework;
using PersonSearch.Controllers;
using PersonSearch.Models.PersonSearch;
using PersonSearchServices.Interfaces;

namespace PersonSearch.Test.Controllers
{
    [TestFixture]
    public class PersonSearchControllerTest
    {
        private Mock<IPersonSearchService> _personSearchServiceMock;

        private PersonSearchController _personSearchController;

        [SetUp]
        public void Setup()
        {
            _personSearchServiceMock = new Mock<IPersonSearchService>();
            _personSearchController = new PersonSearchController(_personSearchServiceMock.Object);
        }

        #region Index() Tests

        [Test]
        public void Index_ReturnsIndexView()
        {
            const string expectedViewName = "";

            // Act
            var actualViewName = ((ViewResult) _personSearchController.Index()).ViewName;

            // Assert
            Assert.AreEqual(expectedViewName, actualViewName);
        }

        #endregion

        #region SearchPeople() Tests

        [Test]
        public void SearchPeople_ReturnsPeople()
        {
            const string searchString = "tes";

            const string firstName = "Test";
            const string lastName = "McTester";
            const string streetAddress = "123 Street";
            const string city = "Salt Lake City";
            const string stateAbbreviation = "UT";
            const string zipCode = "82922";

            const short expectedAge = 43;
            const string expectedInterest = "Skiing";
            string expectedFullName = $"{firstName} {lastName}";
            string expectedAddress = $"{streetAddress} {city}, {stateAbbreviation} {zipCode}";

            // Arrange
            var personData = new List<Person>
            {
                new Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = expectedAge,
                    Address = new Address
                    {
                        StreetAddress = streetAddress,
                        City = new City
                        {
                            Name = city,
                            State = new State
                            {
                                Abbreviation = stateAbbreviation
                            }
                        },
                        ZipCode = zipCode
                    },
                    Interests = new List<Interest>
                    {
                        new Interest
                        {
                            Name = expectedInterest
                        }
                    }
                }
            };

            _personSearchServiceMock
                .Setup(p => p.SearchPeople(searchString))
                .Returns(personData);

            // Act
            var person = ((List<PersonModel>) _personSearchController.SearchPeople(searchString).Data).Single();

            // Assert
            Assert.AreEqual(expectedFullName, person.FullName);
            Assert.AreEqual(expectedAge, person.Age);
            Assert.AreEqual(expectedAddress, person.Address);
            Assert.AreEqual(expectedInterest, person.Interests.Single());
        }

        #endregion
    }
}