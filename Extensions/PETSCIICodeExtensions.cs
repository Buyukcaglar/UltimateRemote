using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
// ReSharper disable once InconsistentNaming

public static class PETSCIICodeExtensions
{
    public static string GetHex(this PETSCIICode[] petsciiCodes, char charStr)
        => petsciiCodes.FirstOrDefault(code => code.Char == charStr)?.HexCode ?? "";

    public static string GetReverseHex(this PETSCIICode[] petsciiCodes, char charStr)
        => petsciiCodes.FirstOrDefault(code => code.Char == charStr)?.ReverseHexCode ?? "";
}
