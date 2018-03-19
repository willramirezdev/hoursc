using Hours.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hours.Tests.Core
{
    [TestClass]
    [TestCategory("Unit")]
    public class ValidationHelperTests
    {
        [TestMethod]
        public void ValidateStringLengthAndNullOrEmpty_ReturnsSuccess_WhenLessThanMaxLength()
        {
            var str = "test";

            Assert.IsTrue(ValidationHelper.ValidateStringLengthAndNullOrEmpty(str, nameof(str), 5).IsSuccess);
        }

        [TestMethod]
        public void ValidateStringLengthAndNullOrEmpty_ReturnsSuccess_WhenEqualMaxLength()
        {
            var str = "test";

            var result = ValidationHelper.ValidateStringLengthAndNullOrEmpty(str, nameof(str), 4);
            Assert.IsTrue(result.IsSuccess);            
        }

        [TestMethod]
        public void ValidateStringLengthAndNullOrEmpty_ReturnsError_WhenMaxLengthIsPassed()
        {
            var str = "test";
            var name = nameof(str);

            var result = ValidationHelper.ValidateStringLengthAndNullOrEmpty(str, name, 1);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.ErrorMessages.Count);
            // verify the name is in the error message but not the exact error
            Assert.IsTrue(result.ErrorMessages.Any(x => x.Contains(name)));
        }

        [TestMethod]
        public void ValidateStringLengthAndNullOrEmpty_ReturnsError_WhenNull()
        {
            var str = null as string;
            var name = nameof(str);

            var result = ValidationHelper.ValidateStringLengthAndNullOrEmpty(str, name, 1);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.ErrorMessages.Count);
            // verify the name is in the error message but not the exact error
            Assert.IsTrue(result.ErrorMessages.Any(x => x.Contains(name)));
        }

        [TestMethod]
        public void ValidateEmail_ReturnsSuccess()
        {
            var email = "test@test.com";

            var result = ValidationHelper.ValidateEmail(email);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ValidateEmail_ReturnsError_WhenEmailInvalid()
        {
            var email = "test@";

            var result = ValidationHelper.ValidateEmail(email);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.ErrorMessages.Count);
            Assert.IsTrue(result.ErrorMessages.Any(x => x.Contains("Invalid email format.")));
        }

        [TestMethod]
        public void ValidateEmail_ReturnsError_WhenEmailOver200Characters()
        {
            var email = "test@";

            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += Guid.NewGuid().ToString();
            email += ".com";

            var result = ValidationHelper.ValidateEmail(email);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.ErrorMessages.Count);
            Assert.IsTrue(result.ErrorMessages.Any(x => x.Contains("email is longer than 200 characters.")));
        }
    }
}
