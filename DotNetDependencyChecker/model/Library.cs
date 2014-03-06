﻿using System;
using System.Collections.Generic;
using System.Text;
using org.pescuma.dotnetdependencychecker.utils;

namespace org.pescuma.dotnetdependencychecker.model
{
	public class Library
	{
		public static Comparison<Library> NaturalOrdering =
			(p1, p2) => String.Compare(p1.Name, p2.Name, StringComparison.CurrentCultureIgnoreCase);

		public readonly string LibraryName;
		public readonly HashSet<string> Paths = new HashSet<string>();
		public GroupElement GroupElement;

		public Library(string libraryName)
		{
			Argument.ThrowIfNull(libraryName);

			LibraryName = libraryName;
		}

		public virtual string Name
		{
			get { return LibraryName; }
		}

		public virtual List<string> Names
		{
			get { return LibraryName.AsList(); }
		}

		protected bool Equals(Library other)
		{
			return string.Equals(LibraryName, other.LibraryName);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != GetType())
				return false;
			return Equals((Library) obj);
		}

		public override int GetHashCode()
		{
			return (LibraryName != null ? LibraryName.GetHashCode() : 0);
		}

		public override string ToString()
		{
			var result = new StringBuilder();

			result.Append(LibraryName)
				.Append("[")
				.Append("Paths: ")
				.Append(string.Join(", ", Paths))
				.Append("]");

			return result.ToString();
		}
	}
}