﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using org.pescuma.dependencychecker.architecture;
using org.pescuma.dependencychecker.utils;

namespace org.pescuma.dependencychecker.output.architeture
{
	public class DotArchitectureOutputer : ArchitectureOutputer
	{
		private const string INDENT = "    ";

		private readonly string file;
		private readonly Dictionary<string, int> ids = new Dictionary<string, int>();

		public DotArchitectureOutputer(string file)
		{
			this.file = file;
		}

		public void Output(ArchitectureGraph architecture, List<OutputEntry> warnings)
		{
			architecture.Vertices.ForEach((proj, i) => ids.Add(proj, i + 1));

			var result = new DotStringBuilder("Architecture");

			architecture.Vertices.ForEach(v => result.AppendNode(v, "shape", "box"));

			result.AppendSpace();

			var deps = new HashSet<GroupDependency>(architecture.Edges);
			var inversible = new HashSet<GroupDependency>(deps.Where(d => deps.Contains(new GroupDependency(d.Target, d.Source))));

			architecture.Edges.ForEach(
				d =>
					result.AppendEdge(d.Source, d.Target, "style", d.Implicit ? "dashed" : null, "color",
						d.Conflicted ? "yellow" : inversible.Contains(d) ? "red" : null));

			File.WriteAllText(file, result.ToString());
		}
	}
}
