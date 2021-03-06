using System.Collections.Generic;
using System.Linq;
using org.pescuma.dependencychecker.model;
using org.pescuma.dependencychecker.presenter.rules;

namespace org.pescuma.dependencychecker.presenter.output
{
	public class DependencyRuleMatch : RuleOutputEntry
	{
		public readonly bool Allowed;

		public DependencyRuleMatch(bool allowed, string type, Severity severity, OutputMessage messsage, Rule rule, IEnumerable<Dependency> deps,
			IEnumerable<ProcessedField> processedFields)
			: base(type, severity, messsage, rule, null, deps.ToList(), processedFields)
		{
			Allowed = allowed;
		}
	}
}
