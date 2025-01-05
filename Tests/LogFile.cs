namespace Gs1DigitalLinkToolkitTests;

internal static class LogFile {
    public static object LockObject { get; } = new object();

    public static bool OutputLog { get; set; } = false;
}
