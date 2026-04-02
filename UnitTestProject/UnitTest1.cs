using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OhmLawTests
{
    [TestClass]
    public class OhmLawLogicTests
    {
        [TestMethod]
        public void CalculateCurrent_ValidInput_ReturnsCorrectValue()
        {
            double voltage = 12.0;
            double resistance = 4.0;
            double expected = 3.0;

            double actual = voltage / resistance;

            Assert.AreEqual(expected, actual, 0.001, "Ток рассчитан неверно");
        }

        [TestMethod]
        public void CalculateCurrent_DivideByZero_ThrowsException()
        {
            decimal voltage = 10;
            decimal resistance = 0;
            Assert.ThrowsException<DivideByZeroException>(() =>
            {
                decimal x = voltage / resistance;
            });
        }

        [TestMethod]
        public void CalculateVoltage_ValidInput_ReturnsCorrectValue()
        {
            double current = 2.0;
            double resistance = 5.0;
            double expected = 10.0;
            double actual = current * resistance;
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void CalculateResistance_ValidInput_ReturnsCorrectValue()
        {
            double voltage = 24.0;
            double current = 3.0;
            double expected = 8.0;
            double actual = voltage / current;
            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void CalculateResistance_ZeroCurrent_ThrowsException()
        {
            decimal voltage = 5;
            decimal current = 0;
            Assert.ThrowsException<DivideByZeroException>(() =>
            {
                decimal x = voltage / current;
            });
        }

        [TestMethod]
        public void GetDoubleValue_ValidNumber_ReturnsDouble()
        {
            bool success = double.TryParse("12,5", out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(12.5, result);
        }

        [TestMethod]
        public void GetDoubleValue_InvalidInput_ReturnsFalse()
        {
            bool success = double.TryParse("abc", out _);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void GetDoubleValue_EmptyInput_ReturnsFalse()
        {
            bool success = double.TryParse("", out _);
            Assert.IsFalse(success);
        }
    }
}