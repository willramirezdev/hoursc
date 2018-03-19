using Hours.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hours.Tests.Unit
{   
    [TestClass]
    [TestCategory("Unit")]
    public class ResultTests
    {        
        [TestMethod]
        public void IsFailure_ReturnsTrue_WhenIsSuccessFalse()
        {
            var sut = new Result(false);

            Assert.IsFalse(sut.IsSuccess);
            Assert.IsTrue(sut.IsFailure);
        }

        [TestMethod]
        public void IsFailure_ReturnsFalse_WhenIsSuccessTrue()
        {
            var sut = new Result(true);

            Assert.IsTrue(sut.IsSuccess);
            Assert.IsFalse(sut.IsFailure);
        }

        [TestMethod]
        public void Combine_IsSuccessIsTrue_WhenAllResultsSucceed()
        {
            var result1 = new Result(true);
            var result2 = new Result(true);

            var sut = Result.Combine(result1, result2);

            Assert.IsTrue(sut.IsSuccess);
        }

        [TestMethod]
        public void Combine_IsSuccessIsFalse_WhenAnyResultFails()
        {
            var result1 = new Result(true);
            var result2 = new Result(false);

            var sut = Result.Combine(result1, result2);

            Assert.IsFalse(sut.IsSuccess);
        }

        [TestMethod]
        public void Combine_AggregatesErrorMessages()
        {
            var error1 = Guid.NewGuid().ToString();
            var result1 = new Result(false, new List<string> { error1 });

            var error2 = Guid.NewGuid().ToString();
            var result2 = new Result(false, new List<string> { error2 });

            var sut = Result.Combine(result1, result2);

            Assert.IsFalse(sut.IsSuccess);
            Assert.AreEqual(2, sut.ErrorMessages.Count);
            Assert.IsTrue(sut.ErrorMessages.Contains(error1));
            Assert.IsTrue(sut.ErrorMessages.Contains(error2));
        }
    }
}
