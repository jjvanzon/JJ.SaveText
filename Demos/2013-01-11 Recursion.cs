using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Demos.Recursion
{
	internal enum CircularityHandling
	{
		Exception,
		Continue
	}

	internal class CircularityException : Exception
	{
		public CircularityException(string message)
			: base(message)
		{ }

		public CircularityException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}

	internal static class Recursion
	{
		public static List<T> GetRecursive<T>(T parent, Func<T, List<T>> getChildren, CircularityHandling circularityHandling = CircularityHandling.Exception)
		{
			if (parent == null) throw new ArgumentNullException("parent");
			if (getChildren == null) throw new ArgumentNullException("getChildren");

			List<T> list = new List<T>();

			foreach (T child in getChildren(parent))
			{
				// Handle circularity.
				if (list.Contains(child))
				{
					switch (circularityHandling)
					{
						case CircularityHandling.Continue:
							continue;

						case CircularityHandling.Exception:
							throw new CircularityException("Circularity detected.");
					}
				}

				list.Add(child);

				list.AddRange(Recursion.GetRecursive(child, getChildren));
			}

			return list;
		}
	}
}
