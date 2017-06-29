using System;

namespace JJ.Data.Canonical.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(IDAndName idAndName)
        {
            if (idAndName == null) throw new ArgumentNullException(nameof(idAndName));

            string debuggerDisplay = $"{{{nameof(IDAndName)}}} {idAndName.ID} {idAndName.Name}";
            return debuggerDisplay;
        }

        internal static string GetDebuggerDisplay(MessageDto message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            string debuggerDisplay = $"{{{nameof(MessageDto)}}} {message.Key} - '{message.Text}'";
            return debuggerDisplay;
        }
    }
}
