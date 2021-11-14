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
        public LabCalculatorVisitor(Spreadsheet spreadsheet)
        {
            this.spreadsheet = spreadsheet;
        }

        Spreadsheet spreadsheet;

        public override double VisitCompileUnit([NotNull] LabCalculatorParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitNumberExpr([NotNull] LabCalculatorParser.NumberExprContext context)
        {
            var result = double.Parse(context.GetText());
            return result;
        }

        public override double VisitIdentifierExpr([NotNull] LabCalculatorParser.IdentifierExprContext context)
        {
            return spreadsheet.CalculateCell(context.GetText());
        }

        public override double VisitParenthesizedExpr([NotNull] LabCalculatorParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitExponentialExpr([NotNull] LabCalculatorParser.ExponentialExprContext context)
        {
            var right = WalkRight(context);
            var left = WalkLeft(context);
            return System.Math.Pow(left, right);
        }

        public override double VisitBinaryAdditiveExpr([NotNull] LabCalculatorParser.BinaryAdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == LabCalculatorLexer.ADD)
            {
                return left + right;
            }
        
            return left - right;
        }

        public override double VisitUnaryAdditiveExpr([NotNull] LabCalculatorParser.UnaryAdditiveExprContext context)
        {
            var expression = Visit(context.expression());
            if (context.operatorToken.Type == LabCalculatorLexer.ADD)
            {
                return expression;
            }

            return -expression;
        }

        public override double VisitMultiplicativeExpr([NotNull] LabCalculatorParser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);

            if (context.operatorToken.Type == LabCalculatorLexer.MULTIPLY)
            {
                return left * right;
            }
            return left / right;
        }

        public override double VisitIncrementalExpr([NotNull] LabCalculatorParser.IncrementalExprContext context)
        {
            var expression = Visit(context.expression());
            if (context.operatorToken.Type == LabCalculatorLexer.INC)
            {
                return expression + 1;
            }
            return expression - 1;
        }

        private double WalkLeft(LabCalculatorParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(0));
        }

        private double WalkRight(LabCalculatorParser.ExpressionContext context)
        {
            return Visit(context.GetRuleContext<LabCalculatorParser.ExpressionContext>(1));
        }
    }
}
