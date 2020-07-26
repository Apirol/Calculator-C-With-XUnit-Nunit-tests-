using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace PZ3
{
    using static FunctionConstr;


    public abstract class Expr
    {
        public abstract double Compute(IReadOnlyDictionary<string, double> variableValues);
        public abstract bool IsConstant { get; }

        public abstract bool IsPolynom { get; }
        public abstract IEnumerable<string> Variables { get; }

        public static Expr operator -(Expr Operand) => new Minus(Operand);
        public static Expr operator +(Expr Operand1, Expr Operand2) => new Sum(Operand1, Operand2);
        public static Expr operator -(Expr Operand1, Expr Operand2) => new Sub(Operand1, Operand2);
        public static Expr operator *(Expr Operand1, Expr Operand2) => new Mult(Operand1, Operand2);
        public static Expr operator /(Expr Operand1, Expr Operand2) => new Div(Operand1, Operand2);
        public static implicit operator Expr(double var) => new Constant(var);
    }

    public abstract class Function : Expr
    {
        protected double pow;
        protected Expr A;
        protected string nameFunc;
        public Function(Expr a, string name)
        {
            nameFunc = name;
            A = a;
        }
        public Function(Expr a, string name, double pow)
        {
            this.nameFunc = name;
            A = a;
            this.pow = pow;
        }
        public override abstract double Compute(IReadOnlyDictionary<string, double> variableValues);
        public override IEnumerable<string> Variables => A.Variables;
        public override bool IsConstant => A.IsConstant;
        public override bool IsPolynom => false;
    }

    public abstract class UnaryOperation : Expr
    {
        protected Expr A;
        public UnaryOperation(Expr a)
        {
            A = a;
        }
        public override IEnumerable<string> Variables => A.Variables;

    }

    public abstract class BinaryOperation : Expr
    {
        protected Expr A, B;
        public BinaryOperation(Expr a, Expr b)
        {
            A = a;
            B = b;
        }
        public override IEnumerable<string> Variables => A.Variables.Concat(B.Variables).Distinct();
    }

    public class Variable : Expr
    {
        string var;
        public Variable(string Var) => var = Var;
        public override bool IsConstant { get => true; }
        public override bool IsPolynom { get => true; }
        public override IEnumerable<string> Variables => new string[] { var };
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => variableValues[var];
    }

    public class Constant : Expr
    {
        double constant;
        public Constant(double constant) => this.constant = constant;
        public override bool IsConstant => true;
        public override bool IsPolynom => true;
        public override IEnumerable<string> Variables => new string[0];
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => constant;
    }

    class Sum : BinaryOperation
    {
        public Sum(Expr a, Expr b) : base(a, b) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => A.Compute(variableValues) + B.Compute(variableValues);
        public override bool IsConstant => A.IsConstant && B.IsConstant;
        public override bool IsPolynom => (A.IsPolynom && B.IsPolynom) || (A.IsPolynom && B.IsConstant) || (A.IsConstant && B.IsPolynom);
    }

    class Minus : UnaryOperation
    {
        public Minus(Expr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => -A.Compute(variableValues);

        public override bool IsConstant => false;
        public override bool IsPolynom => false;
    }
    class Sub : BinaryOperation
    {
        public Sub(Expr operand1, Expr operand2) : base(operand1, operand2) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => A.Compute(variableValues) - B.Compute(variableValues);
        public override bool IsConstant => A.IsConstant && B.IsConstant;
        public override bool IsPolynom => (A.IsPolynom && B.IsPolynom) || (A.IsPolynom && B.IsConstant) || (A.IsConstant && B.IsPolynom);
    }

    class Mult : BinaryOperation
    {
        public Mult(Expr operand1, Expr operand2) : base(operand1, operand2) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => A.Compute(variableValues) * B.Compute(variableValues);
        public override bool IsConstant => A.IsConstant && B.IsConstant;
        public override bool IsPolynom => (A.IsPolynom && B.IsPolynom) || (A.IsPolynom && B.IsConstant) || (A.IsConstant && B.IsPolynom);
    }

    class Div : BinaryOperation
    {
        public Div(Expr operand1, Expr operand2) : base(operand1, operand2) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => A.Compute(variableValues) / B.Compute(variableValues);
        public override bool IsConstant => A.IsConstant && B.IsConstant;
        public override bool IsPolynom => (A.IsPolynom && B.IsPolynom) || (A.IsPolynom && B.IsConstant) || (A.IsConstant && B.IsPolynom);
    }

    public class TemplateFunctions : Function
    {
        public TemplateFunctions(Expr a, string name) : base(a, name) { }

        public double pow;

        public TemplateFunctions(Expr a, string name, double pow) : base(a, name) { this.pow = pow; }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            if (nameFunc == "Sin")
                return Math.Sin(A.Compute(variableValues));
            if (nameFunc == "Cos")
                return Math.Cos(A.Compute(variableValues));
            if (nameFunc == "Tg")
                return Math.Tan(A.Compute(variableValues));
            if (nameFunc == "Ctg")
                return 1 / Math.Tan(A.Compute(variableValues));
            if (nameFunc.Equals("ArcSin"))
                return Math.Asin(A.Compute(variableValues));
            if (nameFunc == "ArcCos")
                return Math.Acos(A.Compute(variableValues));
            if (nameFunc == "ArcTg")
                return Math.Atan(A.Compute(variableValues));
            if (nameFunc == "ArcCtg")
                return Math.PI - Math.Atan(A.Compute(variableValues));
            if (nameFunc == "Ch")
                return Math.Cosh(A.Compute(variableValues));
            if (nameFunc == "Th")
                return Math.Tanh(A.Compute(variableValues));
            if (nameFunc == "Sh")
                return Math.Sinh(A.Compute(variableValues));
            if (nameFunc == "Cth")
                return 1 / Math.Tanh(A.Compute(variableValues));
            if (nameFunc == "Sch")
                return 1 / Math.Cosh(A.Compute(variableValues));
            if (nameFunc == "Csch")
                return 1 / Math.Sinh(A.Compute(variableValues));
            if (nameFunc == "Pow")
                return Math.Pow(A.Compute(variableValues), pow);
            return 0;
        }
    }
    public static class FunctionConstr
    {
        public static TemplateFunctions TemplateFunctions(Expr Arg, string nameFunc) => new TemplateFunctions(Arg, nameFunc);
        public static TemplateFunctions TemplateFunctions(Expr Arg, string nameFunc, double pow) => new TemplateFunctions(Arg, nameFunc, pow);
    }


    public class Program
    {
        public static int Main()
        {
            var x = new Variable("x");
            var ex = -x;
            return (int)ex.Compute(new Dictionary<string, double> { ["x"] = 4 });
        }
    }
}