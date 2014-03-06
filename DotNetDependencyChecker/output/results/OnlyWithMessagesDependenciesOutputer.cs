﻿using System.Collections.Generic;
using System.Linq;
using org.pescuma.dotnetdependencychecker.model;
using org.pescuma.dotnetdependencychecker.output.dependencies;

namespace org.pescuma.dotnetdependencychecker.output.results
{
	public class OnlyWithMessagesDependenciesOutputer : DependenciesOutputer
	{
		private readonly DependenciesOutputer next;

		public OnlyWithMessagesDependenciesOutputer(DependenciesOutputer next)
		{
			this.next = next;
		}

		public void Output(DependencyGraph graph, List<OutputEntry> warnings)
		{
			var filtered = Filter(graph, warnings);

			next.Output(filtered, warnings);
		}

		public static DependencyGraph Filter(DependencyGraph graph, List<OutputEntry> warnings)
		{
			var projs = new HashSet<Library>(warnings.SelectMany(w => w.Projects)
				.Concat(warnings.SelectMany(w => w.Dependencies.SelectMany(d => new[] { d.Source, d.Target }))));

			var deps = new HashSet<Dependency>(warnings.SelectMany(w => w.Dependencies));

			var filtered = new DependencyGraph();
			filtered.AddVertexRange(projs);
			//filtered.AddEdgeRange(graph.Edges.Where(d => projs.Contains(d.Source) && projs.Contains(d.Target)));
			filtered.AddEdgeRange(deps);
			return filtered;
		}
	}
}
