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
            Assert.AreEqual(3, Spreadsheet.Evaluate("1+2", null));
            Assert.AreEqual(6, Spreadsheet.Evaluate("2*3", null));
            Assert.AreEqual(-2, Spreadsheet.Evaluate("-2", null));
            Assert.AreEqual(8, Spreadsheet.Evaluate("2^3", null));
            Assert.AreEqual(2, Spreadsheet.Evaluate("inc(1)", null));
        }

        [TestMethod()]
        public void EvaluateTestSpaces()
        {
            Assert.AreEqual(3, Spreadsheet.Evaluate("1   +2", null));
            Assert.AreEqual(3, Spreadsheet.Evaluate("\t1\t+\t2\t", null));
        }

        [TestMethod()]
        public void EvaluateTestPrecedence()
        {
            Assert.AreEqual(6, Spreadsheet.Evaluate("2 + 2*2", null));
            Assert.AreEqual(8, Spreadsheet.Evaluate("(2 + 2)*2", null));
            Assert.AreEqual(1, Spreadsheet.Evaluate("-2 + 3", null));
            Assert.AreEqual(-5, Spreadsheet.Evaluate("-(2 + 3)", null));
            Assert.AreEqual(7, Spreadsheet.Evaluate("2^3 - 1", null));
            Assert.AreEqual(4, Spreadsheet.Evaluate("2^(3 - 1)", null));
        }

        [TestMethod()]
        public void EvaluateTestAssociative()
        {
            Assert.AreEqual(19683, Spreadsheet.Evaluate("3^3^2", null));
            Assert.AreEqual(0.5, Spreadsheet.Evaluate("2/2/2", null));
        }
        
    }

}