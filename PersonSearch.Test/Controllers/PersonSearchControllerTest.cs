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
            // Arrange

            // Act

            // Assert
        }

        #endregion
    }
}