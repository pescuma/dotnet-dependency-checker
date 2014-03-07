using System;
using QuickGraph;

namespace org.pescuma.dependencychecker.architecture
{
	public class GroupDependency : Edge<string>
	{
		public static Comparison<GroupDependency> NaturalOrdering = (d1, d2) =>
		{
			var comp = string.Compare(d1.Source, d2.Source, StringComparison.CurrentCultureIgnoreCase);
			if (comp != 0)
				return comp;

			return string.Compare(d1.Target, d2.Target, StringComparison.CurrentCultureIgnoreCase);
		};

		public readonly bool Conflicted;
		public readonly bool Implicit;

		public GroupDependency(string source, string target, bool conflicted = false, bool @implicit = false)
			: base(source, target)
		{
			Conflicted = conflicted;
			Implicit = @implicit;
		}

		private bool Equals(GroupDependency other)
		{
			return string.Equals(Source, other.Source) && string.Equals(Target, other.Target);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != GetType())
				return false;
			return Equals((GroupDependency) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Source != null ? Source.GetHashCode() : 0) * 397) ^ (Target != null ? Target.GetHashCode() : 0);
			}
		}
	}
}
