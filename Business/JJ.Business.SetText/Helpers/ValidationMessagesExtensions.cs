using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.SetText.Helpers
{
    internal static class ValidationMessagesExtensions
    {
        public static List<JJ.Business.CanonicalModel.Message> ToCanonical(this IEnumerable<JJ.Framework.Validation.ValidationMessage> sourceList)
        {
            var destList = new List<JJ.Business.CanonicalModel.Message>();

            foreach (JJ.Framework.Validation.ValidationMessage sourceItem in sourceList)
            {
                JJ.Business.CanonicalModel.Message destItem = sourceItem.ToCanonical();
                destList.Add(destItem);
            }

            return destList;
        }
    }
}
