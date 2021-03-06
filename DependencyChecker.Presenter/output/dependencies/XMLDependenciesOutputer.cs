﻿using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using org.pescuma.dependencychecker.model;
using org.pescuma.dependencychecker.model.xml;
using org.pescuma.dependencychecker.presenter.architecture;

namespace org.pescuma.dependencychecker.presenter.output.dependencies
{
	public class XMLDependenciesOutputer : DependenciesOutputer
	{
		private readonly string file;

		public XMLDependenciesOutputer(string file)
		{
			this.file = file;
		}

		public void Output(DependencyGraph graph, ArchitectureGraph architecture, List<OutputEntry> warnings)
		{
			var xdoc = new XDocument();
			var xroot = new XElement("DependencyChecker-Depedencies");
			xdoc.Add(xroot);

			XMLHelper.ToXML(xroot, graph);

			File.WriteAllText(file, xdoc.ToString());
		}
	}
}
