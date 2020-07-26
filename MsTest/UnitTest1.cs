using Microsoft.VisualStudio.TestTools.UnitTesting;
using PZ3;
using static PZ3.FunctionConstr;
using System.Collections.Generic;
using System;

namespace PZ_TEST
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Pow()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Pow", 4);
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 4 }), 256);
        }
        [TestMethod]
        public void Sin()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Sin");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = Math.PI / 2 }), 1);
        }
        [TestMethod]
        public void Cos()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Cos");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = Math.PI }), -1);
        }
        [TestMethod]
        public void Tg()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Tg");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 5 }), Math.Sin(5) / Math.Cos(5));
        }
        [TestMethod]
        public void Ctg()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Ctg");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = Math.PI / 4 }), 1 / Math.Tan(Math.PI / 4));
        }
        [TestMethod]
        public void Arcsin()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "ArcSin");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 0.5 }), Math.Asin(0.5));
        }
        [TestMethod]
        public void Arccos()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "ArcCos");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 0.5 }), Math.Acos(0.5));
        }
        [TestMethod]
        public void Arctg()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "ArcTg");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 1 }), Math.Atan(1));
        }
        [TestMethod]
        public void Arcctg()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "ArcCtg");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 0.5 }), Math.PI - Math.Atan(0.5));
        }
        [TestMethod]
        public void Ch()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Ch");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 10 }), Math.Cosh(10));
        }
        [TestMethod]
        public void Th()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Th");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 10 }), Math.Tanh(10));
        }
        [TestMethod]
        public void Sh()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Sh");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 10 }), Math.Sinh(10));
        }
        [TestMethod]
        public void Cth()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Cth");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 10 }), 1 / Math.Tanh(10));
        }
        [TestMethod]
        public void Sch()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Sch");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 5 }), 1 / Math.Cosh(5));
        }
        [TestMethod]
        public void Csch()
        {
            var y = new Variable("y");
            var exex = TemplateFunctions(y, "Csch");
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 5 }), 1 / Math.Sinh(5));
        }
        [TestMethod]
        public void IsConstant()
        {
            var y = new Variable("y");
            var x = new Variable("x");
            var exex = x + y;
            Assert.IsTrue(exex.IsConstant);


        }
        [TestMethod]
        public void IsPolynom()
        {
            var y = new Variable("y");
            var x = new Variable("x");
            var exex = x + y;
            Assert.IsTrue(exex.IsPolynom);
        }
        [TestMethod]
        public void Sub()
        {
            var y = new Variable("y");
            var exex = -y;
            Assert.AreEqual(exex.Compute(new Dictionary<string, double> { ["y"] = 5 }), -5);
        }

        [TestMethod]
        public void NewConstant()
        {
            var y = new Constant(2);
            Assert.IsTrue(y.IsConstant);
            Assert.IsTrue(y.IsPolynom);
        }

        [TestMethod]
         public void MainTest()
         {
             Assert.IsTrue(Program.Main() == -4);
         }
    }
}