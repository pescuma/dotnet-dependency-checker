﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using org.pescuma.dependencychecker.presenter.architecture;
using org.pescuma.dependencychecker.utils;

namespace org.pescuma.dependencychecker.presenter.output.architeture
{
	public class TextArchitectureOutputer : ArchitectureOutputer
	{
		private readonly string file;

		public TextArchitectureOutputer(string file)
		{
			this.file = file;
		}

		public void Output(ArchitectureGraph architecture, List<OutputEntry> warnings)
		{
			var result = new StringBuilder();

			result.Append("Groups:\n");
			architecture.Vertices.OrderBy(v => v, StringComparer.CurrentCultureIgnoreCase)
				.ForEach(v => result.Append("  - ")
					.Append(v)
					.Append("\n"));

			result.Append("\n");
			result.Append("Dependencies:\n");
			architecture.Edges.SortBy(GroupDependency.NaturalOrdering)
				.ForEach(v => result.Append("  - ")
					.Append(v.Source)
					.Append(" -> ")
					.Append(v.Target)
					.Append(v.Type == GroupDependency.Types.Conflicted ? " (this reference is both allowed and not allowed)" : "")
					.Append(v.Type == GroupDependency.Types.Implicit ? " (this reference is not explicit allowed, but is also not not allowed)" : "")
					.Append("\n"));

			File.WriteAllText(file, result.ToString());
		}
	}
}
