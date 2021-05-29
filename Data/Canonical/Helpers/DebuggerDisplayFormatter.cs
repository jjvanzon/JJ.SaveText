using System;

namespace JJ.Data.Canonical.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(IDAndName idAndName)
        {
            if (idAndName == null) throw new ArgumentNullException(nameof(idAndName));

            string debuggerDisplay = $"{{{nameof(IDAndName)}}} {idAndName.ID} {idAndName.Name}";
            return debuggerDisplay;
        }
    }
}
