namespace UltimateRemote.Constants;

internal static class JsFunctions
{
    public const string NumericOnly =
        "return (['1','2','3','4','5','6','7','8','9','0','.','Backspace','Delete'].includes(event.key));";

    public const string IpAndPort =
        "return (['1','2','3','4','5','6','7','8','9','0','.',':','Backspace','Delete'].includes(event.key));";

    public const string HexOnly =
        "return (['1','2','3','4','5','6','7','8','9','0','A','B','C','D','E','F','a','b','c','d','e','f','Backspace','Delete'].includes(event.key));";
}

internal static class InputPatterns
{
    //public const string Numeric = "\\d*";
    public const string Numeric = "[0-9]*";
}

internal static class RegexPatterns
{
    public const string ValidateIp = @"^((([0-1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\.){3}([0-1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]))$";
    public const string ValidateIpPort = @"^((([0-1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\.){3}([0-1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]))(?::\d{0,4})?";
}

internal static class Validators
{
    public static Func<string?, bool> IpValidator = (ipAddress) => !string.IsNullOrWhiteSpace(ipAddress) && 
                                                                   System.Text.RegularExpressions.Regex.IsMatch(input: ipAddress, pattern: RegexPatterns.ValidateIp);

    public static Func<string?, bool> IpPortValidator = (ipPort) => !string.IsNullOrWhiteSpace(ipPort) &&
                                                                        System.Text.RegularExpressions.Regex.IsMatch(input: ipPort, pattern: RegexPatterns.ValidateIpPort);

    public static Func<string?, bool> PortValidator = (port) => !string.IsNullOrWhiteSpace(port) &&
                                                                int.TryParse(port, out var portNum) && portNum is >= 0 and < 65536;

    // not used ...
    public static Func<int, int, int, bool> RangeValidator = (value, min, max) => value >= min && value <= max;

}

internal static class FilePathHelper
{
    public static string LocationPath(string? location, string? path)
        => $"{location}{(!string.IsNullOrWhiteSpace(path) && path.StartsWith('/') ? path : $"/{path}")}";
}