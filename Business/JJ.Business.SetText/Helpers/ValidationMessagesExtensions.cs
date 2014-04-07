using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.SetText.Helpers
{
    internal static class ValidationMessagesExtensions
    {
        public static List<JJ.Models.Canonical.ValidationMessage> ToCanonical(this IEnumerable<JJ.Framework.Validation.ValidationMessage> sourceList)
        {
            var destList = new List<JJ.Models.Canonical.ValidationMessage>();

            foreach (JJ.Framework.Validation.ValidationMessage sourceItem in sourceList)
            {
                JJ.Models.Canonical.ValidationMessage destItem = sourceItem.ToCanonical();
                destList.Add(destItem);
            }

            return destList;
        }
    }
}
