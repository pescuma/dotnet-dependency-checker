﻿using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using org.pescuma.dependencychecker.model;
using org.pescuma.dependencychecker.model.xml;
using org.pescuma.dependencychecker.presenter.config;
using org.pescuma.dependencyconsole.commands;

namespace org.pescuma.dependencyconsole
{
	internal class Program
	{
		private static Command[] commands =
		{
			new LibsCommand(),
			new InfoCommand(),
			new DependentsOfCommand(),
			new ReferencedByCommand(),
			new CircularDependenciesCommand(),
			new PathBetweenCommand(),
			new RuleCommand(),
			new QuitCommand()
		};

		private static int Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Use: dependency-console <dependencies.xml>");
				Console.WriteLine();
				return -1;
			}

			var graph = LoadGraph(args[0]);

			Console.WriteLine("Graph loaded.");

			try
			{
				while (true)
				{
					Console.WriteLine();
					Console.Write("> ");
					var line = Console.ReadLine();
					if (line == null)
						break;
					line = line.Trim();

					if (string.IsNullOrEmpty(line))
						continue;

					RunCommand(line, graph);
				}
			}
			catch (QuitException)
			{
			}

			return 0;
		}

		private static void RunCommand(string line, DependencyGraph graph)
		{
			if (line == "help" || line == "?")
			{
				Console.WriteLine("Commands: " + string.Join(", ", commands.Select(c => c.Name)));
				Console.WriteLine("(you can also use only the first letter of the command)");
				return;
			}

			try
			{
				var handled = commands.Any(c => c.Handle(line, graph));

				if (!handled)
				{
					Console.WriteLine("Unknown command: " + line);
					Console.WriteLine("Type ? for help");
				}
			}
			catch (ConfigParserException e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine("Type ? for help");
			}
		}

		private static DependencyGraph LoadGraph(string filename)
		{
			var doc = XDocument.Load(filename);

			if (doc.Root == null || doc.Root.Name != "DependencyChecker-Depedencies")
				throw new IOException("Invalid dependencies XML file: " + filename);

			return XMLHelper.DependencyGraphFromXML(doc.Root);
		}
	}
}
