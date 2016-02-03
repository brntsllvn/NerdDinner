using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdDinner.Models;

namespace NerdDinner.Tests
{
    [TestClass]
    public class DinnerTest
    {
        [TestMethod]
        public void Dinner_Should_Not_Be_Valid_When_Some_Properties_Incorrect()
        {
            //Arrange
            Dinner dinner = new Dinner()
            {
                Title = "Test title",
                Country = ""
            };

            // Act
            bool isValid = dinner.IsValid();

            //Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Dinner_Should_Be_Valid_When_All_Properties_Correct()
        {

            //Arrange
            Dinner dinner = new Dinner
            {
                Title = "Test title",
                EventDate = DateTime.Now,
                HostedBy = "ScottGu",
                Address = "One Microsoft Way",
                Country = "USA",
                Latitude = 93,
                Longitude = -92,
            };

            // Act
            bool isValid = dinner.IsValid();

            //Assert
            Assert.IsTrue(isValid);
        }
    }
}
