using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Test
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void EvaluateTestTrivial()
        {
            Assert.AreEqual(3, Calculator.Evaluate("1+2", null));
            Assert.AreEqual(6, Calculator.Evaluate("2*3", null));
            Assert.AreEqual(1, Calculator.Evaluate("-2+3", null));
            Assert.AreEqual(8, Calculator.Evaluate("2^3", null));
            Assert.AreEqual(2, Calculator.Evaluate("inc(1)", null));
        }

        [TestMethod()]
        public void EvaluateTestSpaces()
        {
            Assert.AreEqual(3, Calculator.Evaluate("1   +2", null));
            Assert.AreEqual(3, Calculator.Evaluate("\t1\t+\t2\t", null));
        }

        [TestMethod()]
        public void EvaluateTestAssociative()
        {
            Assert.AreEqual(19683, Calculator.Evaluate("3^3^2", null));
            Assert.AreEqual(0.5, Calculator.Evaluate("2/2/2", null));
        }
    }
}