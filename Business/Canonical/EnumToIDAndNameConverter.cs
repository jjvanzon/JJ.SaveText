using System;
using System.Collections.Generic;
using System.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Canonical
{
	public static class EnumToIDAndNameConverter
	{
		public static IList<IDAndName> Convert<TEnum>(ResourceManager resourceManager, bool mustIncludeUndefined)
			where TEnum : struct
		{
			if (resourceManager == null) throw new NullException(() => resourceManager);

			var enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

			var idAndNames = new List<IDAndName>(enumValues.Length);

			// Add Undefined separately, so it is shown as a null string.
			if (mustIncludeUndefined)
			{
				idAndNames.Add(new IDAndName { ID = 0, Name = null });
			}

			foreach (TEnum enumValue in enumValues)
			{
				int enumValueInt = (int)(object)enumValue;
				bool isUndefined = enumValueInt == 0;
				if (isUndefined)
				{
					continue;
				}

				string resourceName = enumValue.ToString();
				string displayName = resourceManager.GetString(resourceName);
				if (string.IsNullOrEmpty(displayName))
				{
					displayName = resourceName;
				}

				var idAndName = new IDAndName
				{
					ID = enumValueInt,
					Name = displayName
				};


				idAndNames.Add(idAndName);
			}

			return idAndNames;
		}

	}
}
