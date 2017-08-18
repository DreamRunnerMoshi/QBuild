using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenzeneSoft.QBuild.Builders;

namespace BenzeneSoft.QBuild.Clauses
{
    public class MutableClause : IClause
    {
        private readonly StringBuilder _textBuilder;
        private readonly List<Parameter> _parameters;

        public MutableClause(string text, params Parameter[] parameters)
        {
            _textBuilder = new StringBuilder(text);
            _parameters = new List<Parameter>(parameters);
        }

        public MutableClause() : this(string.Empty) { }
        public MutableClause(IClause clause) : this(clause.Text, clause.Parameters.ToArray()) { }
        public MutableClause(IClauseBuilder builder) : this(builder.Build()) { }

        public string Text => _textBuilder.ToString();
        public IEnumerable<Parameter> Parameters => _parameters;
        public bool IsEmpty => _textBuilder.Length == 0 && _parameters.Count == 0;

        public MutableClause Append(IClause clause)
        {
            if (clause == null) return this;

            var text = clause.Text;

            _textBuilder.Append(text);
            _parameters.AddRange(clause.Parameters);

            return this;
        }
        
        public MutableClause Append(string text)
        {
            _textBuilder.Append(text);
            return this;
        }

        public MutableClause Prepend(string text)
        {
            _textBuilder.Insert(0, text);
            return this;
        }

        public MutableClause Line()
        {
            _textBuilder.Append("\n");
            return this;
        }

        public MutableClause WrapParentheses()
        {
            _textBuilder.Insert(0, "(").Append(")");
            return this;
        }
    }
}