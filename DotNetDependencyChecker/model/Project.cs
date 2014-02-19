﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.pescuma.dotnetdependencychecker.utils;

namespace org.pescuma.dotnetdependencychecker.model
{
	public class Project : Assembly, Dependable
	{
		public readonly string Name;
		public readonly Guid Guid;

		public string CsprojPath
		{
			get { return Paths.First(); }
		}

		IEnumerable<string> Dependable.Names
		{
			get
			{
				var result = new HashSet<string>();
				result.Add(Name);
				if (AssemblyName != null)
					result.Add(AssemblyName);
				return result;
			}
		}

		public Project(string name, string assemblyName, Guid guid, string csprojPath)
			: base(assemblyName)
		{
			Argument.ThrowIfNull(name);
			Argument.ThrowIfNull(csprojPath);

			Name = name;
			Guid = guid;

			Paths.Add(csprojPath);
		}

		protected bool Equals(Project other)
		{
			return base.Equals(other) && string.Equals(Name, other.Name) && string.Equals(CsprojPath, other.CsprojPath);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != GetType())
				return false;
			return Equals((Project) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = base.GetHashCode();
				hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (CsprojPath != null ? CsprojPath.GetHashCode() : 0);
				return hashCode;
			}
		}

		public override string ToString()
		{
			var result = new StringBuilder();

			result.Append(Name)
				.Append("[");

			if (AssemblyName != null)
				result.Append(AssemblyName)
					.Append(", ");

			result.Append(Guid)
				.Append(", ");

			if (CsprojPath != null)
				result.Append(CsprojPath)
					.Append(", ");

			result.Append("Paths: ")
				.Append(string.Join(", ", Paths));

			result.Append("]");

			return result.ToString();
		}
	}
}