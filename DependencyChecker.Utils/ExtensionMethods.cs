﻿using System.Collections.Generic;
using System.Linq;

namespace org.pescuma.dependencychecker.utils
{
	public static class ExtensionMethods
	{
		public static TV Get<TK, TV>(this IDictionary<TK, TV> dict, TK key) where TV : class
		{
			TV result;
			if (dict.TryGetValue(key, out result))
				return result;
			else
				return null;
		}

		public static void AddRange<T>(this ISet<T> set, IEnumerable<T> toAdd)
		{
			foreach (var e in toAdd)
				set.Add(e);
		}

		public static string NullIfEmpty(this string obj)
		{
			if (string.IsNullOrEmpty(obj))
				return null;
			else
				return obj;
		}

		public static string EmptyIfNull(this string obj)
		{
			return obj ?? "";
		}

		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> obj)
		{
			return obj ?? Enumerable.Empty<T>();
		}

		public static List<T> EmptyIfNull<T>(this List<T> obj)
		{
			return obj ?? new List<T>();
		}
	}
}
