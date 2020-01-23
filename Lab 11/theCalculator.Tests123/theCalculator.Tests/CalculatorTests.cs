using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using theCalculator;

namespace theCalculator.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        [TestCategory("Add")]
        [Ignore]
        public void AddSimple()
        {
            Calculator calculator = new Calculator();
            int sum = calculator.Add(1, 2);

            Assert.AreEqual(3, sum);
        }

        [TestMethod]
        [TestCategory("Divide")]
        public void DivideSimple()
        {
            Calculator calculator = new Calculator();
            int quotient = calculator.Divide(10, 5);
            Assert.AreEqual(2, quotient);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideByZero()
        {
            Calculator calculator = new Calculator();
            calculator.Divide(10, 0);
        }
        /* [TestMethod]
         public void AddWithException()
         {

         }
         [TestMethod]
         public void AddNegative()
         {

         }*/
    }
}
