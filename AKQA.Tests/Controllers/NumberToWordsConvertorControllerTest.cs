using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKQA;
using AKQA.Controllers;
using System.Threading;
using System.Web.Http.Results;

namespace AKQA.Tests.Controllers
{
    [TestClass]
    public class NumberToWordsConvertorControllerTest
    {
        
        [TestMethod]
        [DataRow("123.45", "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("100123.45", "ONE HUNDRED THOUSAND AND ONE HUNDRED AND TWENTY-THREE DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("100.45", "ONE HUNDRED DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("4.45", "FOUR DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("0.45", "FOURTY-FIVE CENTS")]
        public void Get(string value, string expectedOutput)
        {
            // Arrange
            NumberToWordConvertorController controller = new NumberToWordConvertorController();

            // Act
            string result = controller.Get(value);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        [DataRow("123.45", "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("100123.45", "ONE HUNDRED THOUSAND AND ONE HUNDRED AND TWENTY-THREE DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("100.45", "ONE HUNDRED DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("4.45", "FOUR DOLLARS AND FOURTY-FIVE CENTS")]
        [DataRow("0.45", "FOURTY-FIVE CENTS")]
        public void Post(string value, string expectedOutput)
        {
            // Arrange
            NumberToWordConvertorController controller = new NumberToWordConvertorController();

            // Act
            var result = controller.Post(new Request() { value = value }) as OkNegotiatedContentResult<string>;

            // Assert
            Assert.AreEqual(expectedOutput, result.Content);
        }
    }
}
