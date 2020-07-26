using System;
using Xunit;
using PZ3;
using static PZ3.FunctionConstr;
using System.Collections.Generic;

namespace XUnit_PZ_Test
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(Math.PI / 4)]
        [InlineData(Math.PI / 6)]
        [InlineData(Math.PI / 8)]
        [InlineData(Math.PI / 10)]
        [InlineData(Math.PI / 12)]
        public void Test1(double value)
        {
            var y = new Variable("y");
            var TGY = TemplateFunctions(y, "Tg");
            var SINY = TemplateFunctions(y, "Sin");
            var COSY = TemplateFunctions(y, "Cos");
            var COSY_POW_2 = TemplateFunctions(COSY, "Pow", 2);
            var exex = (TGY + SINY - COSY) / COSY_POW_2;
            var endPow = TemplateFunctions(exex, "Pow", 2);
            var pow2In4 = TemplateFunctions(2, "Pow", 4);
            var end = endPow * pow2In4;
            Assert.Equal(end.Compute(new Dictionary<string, double> { ["y"] = value }), Math.Pow((Math.Tan(value) + Math.Sin(value) - Math.Cos(value)) / Math.Pow(Math.Cos(value), 2), 2) * 16);
        }
    }
}