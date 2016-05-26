using System;
using System.Diagnostics;

using VSCode.JsonRpc;

namespace VSCode.Utils
{
    internal static class DebugLogger
    {
        internal static void Debug(string message)
        {
            #if DEBUG
            Trace.WriteLine(string.Concat("[ VSCode.NET ] :: ", message));
            #endif
        }

        internal static void Debug(string message, object obj)
        {
            #if DEBUG
            string json = JsonSerializer.Serialize(obj);
            Debug($"{message} | {json}");
            #endif
        }

        internal static void Debug(Exception ex)
        {
            #if DEBUG
            string json = JsonSerializer.Serialize(ex);
            Debug($"EXCEPTION ({ex.Message}): {json}");
            #endif
        }
    }
}
