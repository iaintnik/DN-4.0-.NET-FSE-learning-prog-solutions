using NUnit.Framework;
using CalcLibrary;
using System;

namespace CalcLibraryTests
{
    [TestFixture]
    public class CalculatorTests
    {
        SimpleCalculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new SimpleCalculator();
        }

        [TearDown]
        public void TearDown()
        {
            calculator.AllClear();
        }

        [Test]
        public void Addition_WithTwoPositiveNumbers_ReturnsCorrectResult()
        {
            double result = calculator.Addition(10, 20);
            Assert.That(result, Is.EqualTo(30));
        }

        [TestCase(2, 3, 5)]
        [TestCase(-4, -6, -10)]
        [TestCase(10.5, 4.5, 15)]
        [TestCase(0, 0, 0)]
        public void Addition_ParameterizedTests(double a, double b, double expected)
        {
            double result = calculator.Addition(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(10, 5, 5)]
        [TestCase(0, 0, 0)]
        [TestCase(-3, -2, -1)]
        public void Subtraction_ParameterizedTests(double a, double b, double expected)
        {
            double result = calculator.Subtraction(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(2, 3, 6)]
        [TestCase(-4, -5, 20)]
        [TestCase(3.5, 2, 7)]
        [TestCase(0, 10, 0)]
        public void Multiplication_ParameterizedTests(double a, double b, double expected)
        {
            double result = calculator.Multiplication(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(10, 2, 5)]
        [TestCase(7.5, 2.5, 3)]
        [TestCase(-8, -2, 4)]
        public void Division_ParameterizedTests(double a, double b, double expected)
        {
            double result = calculator.Division(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Division_ByZero_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => calculator.Division(10, 0));
            Assert.That(ex.Message, Is.EqualTo("Second Parameter Can't be Zero"));
        }

        [Test]
        public void AllClear_ShouldResetResult()
        {
            calculator.Addition(10, 20);
            calculator.AllClear();
            Assert.That(calculator.GetResult, Is.EqualTo(0));
        }

        [Test, Ignore("This test is temporarily disabled")]
        public void IgnoredTestExample()
        {
            Assert.Fail("This test is ignored");
        }
    }
}
