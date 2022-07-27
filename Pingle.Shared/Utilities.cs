using System.Security.Principal;

namespace Pingle.Shared;

public static class Utilities
{
    public static bool IsAdministrator => !OperatingSystem.IsWindows() || new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
}
