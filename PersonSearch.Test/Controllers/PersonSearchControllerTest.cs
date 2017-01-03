using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PersonSearch.Controllers;
using PersonSearchServices.Dtos;
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
            const string partialName = "blah";

            var expectedPersonDto = new PersonDto();

            // Arrange
            _personSearchServiceMock
                .Setup(p => p.GetPeopleByPartialName(partialName))
                .Returns(new List<PersonDto>
                {
                    expectedPersonDto
                });

            // Act
            var people = (List<PersonDto>) _personSearchController.SearchPeople(partialName).Data;

            // Assert
            var actualPersonDto = people.Single();
            Assert.AreSame(expectedPersonDto, actualPersonDto);
        }

        #endregion
    }
}