using Hours.Core.AggregateRoots.BusinessUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Tests.Core.AggregateRoots.BusinessUnit
{
    [TestClass]
    [TestCategory("Unit")]
    public class BusinessContactTests
    {
        [TestMethod]
        public void Create_HappyPath()
        {
            var name = Guid.NewGuid().ToString();
            var email = "test@test.com";
            var phone = "555-555-5555";

            var result = BusinessContact.Create(name, phone, email);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.ReturnValue);

            var sut = result.ReturnValue;
            Assert.AreEqual(name, sut.Name);
            Assert.AreEqual(phone, sut.Phone);
            Assert.AreEqual(email, sut.Email);
        }

        [TestMethod]
        public void Create_ValidationErrors()
        {
            var name = string.Empty;
            var email = "test";
            var phone = null as string;

            var result = BusinessContact.Create(name, phone, email);

            Assert.IsTrue(result.IsFailure);
            Assert.IsNull(result.ReturnValue);

            Assert.AreEqual(3, result.ErrorMessages.Count);
        }
    }
}
