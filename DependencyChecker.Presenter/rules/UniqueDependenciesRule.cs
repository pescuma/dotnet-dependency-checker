﻿using System.Collections.Generic;
using System.Linq;
using org.pescuma.dependencychecker.model;
using org.pescuma.dependencychecker.presenter.output;
using org.pescuma.dependencychecker.utils;

namespace org.pescuma.dependencychecker.presenter.rules
{
	public class UniqueDependenciesRule : BaseRule
	{
		private readonly DependencyMatcher filter;

		public UniqueDependenciesRule(Severity severity, DependencyMatcher filter, ConfigLocation location)
			: base(severity, location)
		{
			this.filter = filter ?? ((d, r) => true);
		}

		public override List<OutputEntry> Process(DependencyGraph graph, Library element)
		{
			var proj = element as Project;
			if (proj == null)
				return null;

			var result = new List<OutputEntry>();

			var same = graph.OutEdges(proj)
				.Where(d => filter(d, Matchers.NullReporter))
				.GroupBy(d => d.Target)
				.Where(g => g.Count() > 1);

			same.ForEach(g =>
			{
				var message = new OutputMessage();
				message.Append("The project ")
					.Append(proj, OutputMessage.ProjInfo.Name)
					.Append(" has multiple dependencies with ")
					.Append(g.Key, OutputMessage.ProjInfo.Name);

				result.Add(new UniqueDependencyOutputEntry(Severity, message, this, g));
			});

			return result;
		}
	}
}
