using System;

namespace org.pescuma.dotnetdependencychecker
{
	public class ConfigException : Exception
	{
		public ConfigException(string message)
			: base(message)
		{
		}
	}
}
