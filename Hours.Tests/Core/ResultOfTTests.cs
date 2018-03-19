using Hours.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hours.Tests.Core.Unit
{
    [TestClass]
    [TestCategory("Unit")]
    public class ResultOfTTests
    {
        [TestMethod]
        public void IsFailure_ReturnsTrue_WhenIsSuccessFalse()
        {
            var sut = new Result<string>(false, string.Empty);

            Assert.IsFalse(sut.IsSuccess);
            Assert.IsTrue(sut.IsFailure);
        }

        [TestMethod]
        public void IsFailure_ReturnsFalse_WhenIsSuccessTrue()
        {
            var sut = new Result<string>(true, string.Empty);

            Assert.IsTrue(sut.IsSuccess);
            Assert.IsFalse(sut.IsFailure);
        }

        [TestMethod]
        public void Combine_IsSuccessIsTrue_WhenAllResultsSucceed()
        {
            var result1 = new Result<string>(true, string.Empty);
            var result2 = new Result<string>(true, string.Empty);

            var sut = Result<string>.Combine(result1, result2);

            Assert.IsTrue(sut.IsSuccess);
        }

        [TestMethod]
        public void Combine_IsSuccessIsFalse_WhenAnyResultFails()
        {
            var result1 = new Result<string>(true, string.Empty);
            var result2 = new Result<string>(false, string.Empty);

            var sut = Result<string>.Combine(result1, result2);

            Assert.IsFalse(sut.IsSuccess);
        }

        [TestMethod]
        public void Combine_AggregatesErrorMessages()
        {
            var error1 = Guid.NewGuid().ToString();
            var result1 = new Result<string>(false, string.Empty, new List<string> { error1 });

            var error2 = Guid.NewGuid().ToString();
            var result2 = new Result<string>(false, string.Empty, new List<string> { error2 });

            var sut = Result<string>.Combine(result1, result2);

            Assert.IsFalse(sut.IsSuccess);
            Assert.AreEqual(2, sut.ErrorMessages.Count);
            Assert.IsTrue(sut.ErrorMessages.Contains(error1));
            Assert.IsTrue(sut.ErrorMessages.Contains(error2));
        }

        [TestMethod]
        public void Combine_Success_AggregatesResults()
        {
            var returnValue1 = Guid.NewGuid().ToString();
            var result1 = new Result<string>(true, returnValue1);

            var returnValue2 = Guid.NewGuid().ToString();
            var result2 = new Result<string>(true, returnValue2);

            var sut = Result<string>.Combine(result1, result2);

            Assert.IsTrue(sut.IsSuccess);

            Assert.IsInstanceOfType(sut, typeof(Result<List<string>>));
            Assert.IsInstanceOfType(sut.ReturnValue, typeof(List<string>));
            Assert.AreEqual(2, sut.ReturnValue.Count);
        }
    }
}
