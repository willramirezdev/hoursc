using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hours.Core.AggregateRoots.BusinessUnitAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hours.Tests.Core.AggregateRoots.BusinessUnitAggregateTests
{
    [TestClass]
    [TestCategory("Unit")]
    public class BusinessUnitTests
    {
        [TestMethod]
        public void Create_NetNew_NoBusinessContact_HappyPath()
        {
            var name = Guid.NewGuid().ToString();
            var department = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();

            var result = BusinessUnit.Create(name, department, description);

            Assert.IsTrue(result.IsSuccess);

            var sut = result.ReturnValue;

            Assert.AreEqual(name, sut.Name);
            Assert.AreEqual(department, sut.Department);
            Assert.AreEqual(description, sut.Description);
            Assert.AreEqual(0, sut.BusinessContacts.Count);
            Assert.AreEqual(0, sut.AggregateVersion);
        }

        [TestMethod]
        public void Create_NetNew_BusinessContact_HappyPath()
        {
            var name = Guid.NewGuid().ToString();
            var department = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();

            var contact = 
                BusinessContact.Create("contact", "555-555-5555", "test@test.com")
                .ReturnValue;
            var contacts = new List<BusinessContact> { contact };

            var result = BusinessUnit.Create(name, department, description, contacts);

            Assert.IsTrue(result.IsSuccess);

            var sut = result.ReturnValue;

            Assert.AreEqual(name, sut.Name);
            Assert.AreEqual(department, sut.Department);
            Assert.AreEqual(description, sut.Description);
            Assert.AreEqual(0, sut.AggregateVersion);
            Assert.AreEqual(1, sut.BusinessContacts.Count);
            Assert.AreEqual(contact, sut.BusinessContacts.FirstOrDefault());
        }

        [TestMethod]
        public void Create_WithAggregateVersion_HappyPath()
        {
            var name = Guid.NewGuid().ToString();
            var department = Guid.NewGuid().ToString();
            var description = Guid.NewGuid().ToString();

            var result = BusinessUnit.Create(name, department, description, aggregateVersion: 97);

            Assert.IsTrue(result.IsSuccess);

            var sut = result.ReturnValue;

            Assert.AreEqual(name, sut.Name);
            Assert.AreEqual(department, sut.Department);
            Assert.AreEqual(description, sut.Description);
            Assert.AreEqual(0, sut.BusinessContacts.Count);
            Assert.AreEqual(97, sut.AggregateVersion);
        }

        [TestMethod]
        public void Create_AllowsNullDepartmentAndDescription()
        {
            var name = Guid.NewGuid().ToString();
            string department = null;
            string description = null;

            var result = BusinessUnit.Create(name, department, description);

            Assert.IsTrue(result.IsSuccess);

            var sut = result.ReturnValue;

            Assert.AreEqual(name, sut.Name);
            Assert.AreEqual(string.Empty, sut.Department);
            Assert.AreEqual(string.Empty, sut.Description);
        }

        [TestMethod]
        public void Create_AllowsEmptyDepartmentAndDescription()
        {
            var name = Guid.NewGuid().ToString();
            string department = string.Empty;
            string description = string.Empty;

            var result = BusinessUnit.Create(name, department, description);

            Assert.IsTrue(result.IsSuccess);

            var sut = result.ReturnValue;

            Assert.AreEqual(name, sut.Name);
            Assert.AreEqual(department, sut.Department);
            Assert.AreEqual(description, sut.Description);
        }
    }
}
