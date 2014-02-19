using System.Collections.Generic;
using System.Linq;
using org.pescuma.dotnetdependencychecker.model;
using org.pescuma.dotnetdependencychecker.rules;

namespace org.pescuma.dotnetdependencychecker.output
{
	public class DependencyRuleMatch : BaseOutputEntry
	{
		public readonly bool Allowed;
		public readonly Rule Rule;

		public DependencyRuleMatch(bool allowed, Severity severity, OutputMessage messsage, Rule rule, IEnumerable<Dependency> deps)
			: base(severity, messsage, deps.ToList())
		{
			Allowed = allowed;
			Rule = rule;
		}
	}
}
