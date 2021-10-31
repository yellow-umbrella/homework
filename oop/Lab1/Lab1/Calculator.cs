using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace Lab1
{
    public static class Calculator
    {
        public static double Evaluate(string expression, Form1 form)
        {
            var lexer = new LabCalculatorLexer(new AntlrInputStream(expression));
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new ThrowExceptionErrorListener());

            var tokens = new CommonTokenStream(lexer);
            var parser = new LabCalculatorParser(tokens);
            parser.ErrorHandler = new BailErrorStrategy();
            var tree = parser.compileUnit();

            var visitor = new LabCalculatorVisitor(form);

            return visitor.Visit(tree);
        }
    }
}
