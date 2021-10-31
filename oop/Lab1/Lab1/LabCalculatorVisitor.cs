using Antlr4.Runtime.Misc;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class LabCalculatorVisitor : LabCalculatorBaseVisitor<double>
    {
        public LabCalculatorVisitor(Form1 form)
        {
            this.form = form;
        }

        Form1 form;

        public override double VisitCompileUnit([NotNull] LabCalculatorParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitNumberExpr([NotNull] LabCalculatorParser.NumberExprContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);

            return result;
        }

        public override double VisitIdentifierExpr([NotNull] LabCalculatorParser.IdentifierExprContext context)
        {
            return form.CalculateCell(context.GetText());
        }

        public override double VisitParenthesizedExpr([NotNull] LabCalculatorParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponentialExpr([NotNull] LabCalculatorParser.ExponentialExprContext context)
        {
            var right = WalkRight(context);
            var left = WalkLeft(context);

            Debug.WriteLine("{0}^{1}", left, right);
            return System.Math.Pow(left, right);
        }

        

        private double WalkLeft(LabCalculatorParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(0));
        }

        private double WalkRight(LabCalculatorParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(1));
        }

        public override double VisitBinAdditiveExpr([NotNull] LabCalculatorParser.BinAdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == LabCalculatorLexer.ADD)
            {
                Debug.WriteLine("{0} + {1}", left, right);
                return left + right;
            }
            else
            {
                Debug.WriteLine("{0} - {1}", left, right);
                return left - right;
            }
        }

        public override double VisitUnAdditiveExpr([NotNull] LabCalculatorParser.UnAdditiveExprContext context)
        {
            var expression = Visit(context.expression());
            if (context.operatorToken.Type == LabCalculatorLexer.ADD)
            {
                Debug.WriteLine("+{0}", expression);
                return expression;
            }
            else
            {
                Debug.WriteLine("-{0}", expression);
                return -expression;
            }
        }

        public override double VisitMultiplicativeExpr([NotNull] LabCalculatorParser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == LabCalculatorLexer.MULTIPLY)
            {
                Debug.WriteLine("{0} * {1}", left, right);
                return left * right;
            }
            else
            {
                Debug.WriteLine("{0} / {1}", left, right);
                return left / right;
            }
        }

        public override double VisitIncrementalExpr([NotNull] LabCalculatorParser.IncrementalExprContext context)
        {
            var expression = Visit(context.expression());
            if (context.operatorToken.Type == LabCalculatorLexer.INC)
            {
                Debug.WriteLine("inc({0})", expression);
                return expression + 1;
            }
            else
            {
                Debug.WriteLine("dec({0})", expression);
                return expression - 1;
            }
        }
    }
}
