﻿using System;
using System.Linq;
using org.pescuma.dotnetdependencychecker.utils;
using QuickGraph;

namespace org.pescuma.dotnetdependencychecker.model
{
	public class Dependency : Edge<Dependable>
	{
		public static Comparison<Dependency> NaturalOrdering = (d1, d2) =>
		{
			var comp = string.Compare(d1.Source.Names.First(), d2.Source.Names.First(), StringComparison.CurrentCultureIgnoreCase);
			if (comp != 0)
				return comp;

			return string.Compare(d1.Target.Names.First(), d2.Target.Names.First(), StringComparison.CurrentCultureIgnoreCase);
		};

		public readonly Types Type;
		public readonly Location Location;
		public readonly string DLLHintPath;

		public enum Types
		{
			ProjectReference,
			DllReference
		}

		public static Dependency WithProject(Dependable source, Dependable target, Location location)
		{
			return new Dependency(source, target, Types.ProjectReference, location, null);
		}

		public static Dependency WithAssembly(Dependable source, Dependable target, Location location, string dllPath)
		{
			return new Dependency(source, target, Types.DllReference, location, dllPath);
		}

		private Dependency(Dependable source, Dependable target, Types type, Location location, string dllHintPath)
			: base(source, target)
		{
			Argument.ThrowIfNull(source);
			Argument.ThrowIfNull(location);

			Type = type;
			Location = location;
			DLLHintPath = dllHintPath;
		}

		public Dependency WithTarget(Dependable otherTarget)
		{
			if (otherTarget == Target)
				return this;

			return new Dependency(Source, otherTarget, Type, Location, DLLHintPath);
		}

		public Dependency WithSource(Dependable otherSource)
		{
			if (otherSource == Source)
				return this;

			return new Dependency(otherSource, Target, Type, Location, DLLHintPath);
		}

		protected bool Equals(Dependency other)
		{
			return Equals(Source, other.Source) && Equals(Target, other.Target) && Type == other.Type;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != GetType())
				return false;
			return Equals((Dependency) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (Source != null ? Source.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (Target != null ? Target.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (int) Type;
				return hashCode;
			}
		}

		public override string ToString()
		{
			return string.Format("{0} -> {1} ({2})", Source.Names.First(), Target.Names.First(), Type);
		}
	}
}
