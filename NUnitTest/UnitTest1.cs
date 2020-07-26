using NUnit.Framework;
using PZ3;
using static PZ3.FunctionConstr;
using System.Collections.Generic;
using System;

namespace PZ_NUnit_Test
{
    public class Tests
    {
        [Test]
        [TestCase(Math.PI / 2)]
        [TestCase((Math.PI))]
        [TestCase((Math.PI / 4))]
        public void Test1(double value)
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Tg");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = value }), Math.Tan(value));
        }

        [Test]
        [TestCase(Math.PI / 2)]
        [TestCase((Math.PI))]
        [TestCase((Math.PI / 4))]
        public void Test2(double value)
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Sin");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = value }), Math.Sin(value));
        }

        [Test]
        [TestCase(Math.PI / 2)]
        [TestCase((Math.PI))]
        [TestCase((Math.PI / 4))]
        public void Test3(double value)
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Cos");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = value }), Math.Cos(value));
        }
    }
}