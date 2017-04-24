using System;
using System.Linq;
using System.Linq.Expressions;

namespace BenzeneSoft.QBuild.Expressions
{
    public class ParserLookup : IParserLookup
    {
        private IExpressionParser[] _expressionParsers;
        private IOperationParser[] _operationParsers;

        public ParserLookup(INameResolver nameResolver)
        {
            var constantParser = new ConstantExpressionParser();
            var propertyParser = new PropertyExpressionParser(nameResolver);
            var binaryParser = new BinaryExpressionParser(this);
            var notExpressionParser = new NotExpressionParser(nameResolver);
            _expressionParsers = new IExpressionParser[]
            {
                constantParser, propertyParser, binaryParser, notExpressionParser
            };

            _operationParsers = new IOperationParser[]
            {
                new BinaryOperationParser()
            };
        }

        public IExpressionParser this[Expression expression]
        {
            get
            {
                var parser = _expressionParsers.FirstOrDefault(p => p.CanParse(expression));
                if (parser != null)
                {
                    return parser;
                }
                throw new ArgumentException($"Expression ({expression}) of type ({expression.Type}) is not supported.");
            }
        }

        public IOperationParser this[ExpressionType operation]
        {
            get
            {
                var parser = _operationParsers.FirstOrDefault(p => p.CanParse(operation));
                if (parser != null)
                {
                    return parser;
                }
                throw new ArgumentException($"Operation {operation} is not supported.");
            }
        }
    }
}