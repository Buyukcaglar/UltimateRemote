namespace UltimateRemote.Constants;

public static class MemoryAddresses
{
    // Hex: 277, Dec: 631
    public const string KeyboardBuffer = "277";

    // Hex: C6, Dec: 198
    public const string ExecKeyBuffer = "C6";
}

public static class MachineCommands
{
    public const string List = $"LI~\r";
    
    public const string Run = $"RU~\r";

    public static string LoadFirstFile(int busId) => $"LO~\"*\",{busId},1\r";
    
    public static string LoadFirstFileAndRun(int busId) => $"{LoadFirstFile(busId)}{Run}";

    public static string LoadDirectory(int busId) => $"LO~\"$\",{busId}\r";

    public static string ListDirectoryAndList(int busId) => $"{LoadDirectory(busId)}{List}";

    public static string LoadFile(int busId, string fileName) => $"LO~\"{fileName}\",{busId},1\r";

    public static string LoadFileAndRun(int busId, string fileName) => $"{LoadFile(busId, fileName)}{Run}";

}
