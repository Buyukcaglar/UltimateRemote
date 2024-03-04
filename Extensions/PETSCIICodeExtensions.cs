using UltimateRemote.Models;

namespace UltimateRemote.Extensions;
// ReSharper disable once InconsistentNaming

public static class PETSCIICodeExtensions
{
    //public static string GetHex(this PETSCIICode[] petsciiCodes, char charStr)
    //    => petsciiCodes.FirstOrDefault(code => code.Char == charStr)?.HexCode ?? "";

    public static string GetHex(this PETSCIICode[] petsciiCodes, char chr)
    {
        if (petsciiCodes.Any(code => code.DecimalCode == chr))
            return petsciiCodes.First(code => code.DecimalCode == chr).HexCode;

        if (petsciiCodes.Any(code => code.ShiftDecimalCode == chr))
            return petsciiCodes.First(code => code.ShiftDecimalCode == chr).ShiftHexCode;

        if (petsciiCodes.Any(code => code.CmdrDecimalCode == chr))
            return petsciiCodes.First(code => code.CmdrDecimalCode == chr).CmdrHexCode;

        if (petsciiCodes.Any(code => code.ReverseDecimalCode == chr))
            return petsciiCodes.First(code => code.ReverseDecimalCode == chr).ReverseHexCode;

        if (petsciiCodes.Any(code => code.ReverseShiftDecimalCode == chr))
            return petsciiCodes.First(code => code.ReverseShiftDecimalCode == chr).ReverseShiftHexCode;

        return petsciiCodes.Any(code => code.ReverseCmdrDecimalCode == chr)
            ? petsciiCodes.First(code => code.ReverseCmdrDecimalCode == chr).ReverseCmdrHexCode
            : string.Empty;
    }

    public static string GetHex(this PETSCIICode[] petsciiCodes, int decimalCode)
    {
        if (petsciiCodes.Any(code => code.DecimalCode == decimalCode))
            return petsciiCodes.First(code => code.DecimalCode == decimalCode).HexCode;

        if (petsciiCodes.Any(code => code.ShiftDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.ShiftDecimalCode == decimalCode).ShiftHexCode;

        if (petsciiCodes.Any(code => code.CmdrDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.CmdrDecimalCode == decimalCode).CmdrHexCode;

        if (petsciiCodes.Any(code => code.ReverseDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.ReverseDecimalCode == decimalCode).ReverseHexCode;

        if (petsciiCodes.Any(code => code.ReverseShiftDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.ReverseShiftDecimalCode == decimalCode).ReverseShiftHexCode;

        return petsciiCodes.Any(code => code.ReverseCmdrDecimalCode == decimalCode)
            ? petsciiCodes.First(code => code.ReverseCmdrDecimalCode == decimalCode).ReverseCmdrHexCode
            : string.Empty;
    }

    //public static string GetShiftHex(this PETSCIICode[] petsciiCodes, char charStr)
    //    => petsciiCodes.FirstOrDefault(code => code.Char == charStr)?.ShiftHexCode ?? "";

    public static string GetShiftHex(this PETSCIICode[] petsciiCodes, char chr)
        => petsciiCodes.FirstOrDefault(code => code.DecimalCode == chr)?.ShiftHexCode ?? "";

    public static string GetHtmlCode(this PETSCIICode[] petsciiCodes, int decimalCode)
    {
        if (petsciiCodes.Any(code => code.DecimalCode == decimalCode))
            return petsciiCodes.First(code => code.DecimalCode == decimalCode).HtmlCode;

        if (petsciiCodes.Any(code => code.ShiftDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.ShiftDecimalCode == decimalCode).ShiftHtmlCode;

        if (petsciiCodes.Any(code => code.CmdrDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.CmdrDecimalCode == decimalCode).CmdrHtmlCode;

        if (petsciiCodes.Any(code => code.ReverseDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.ReverseDecimalCode == decimalCode).ReverseHtmlCode;

        if (petsciiCodes.Any(code => code.ReverseShiftDecimalCode == decimalCode))
            return petsciiCodes.First(code => code.ReverseShiftDecimalCode == decimalCode).ReverseShiftHtmlCode;

        return petsciiCodes.Any(code => code.ReverseCmdrDecimalCode == decimalCode)
            ? petsciiCodes.First(code => code.ReverseCmdrDecimalCode == decimalCode).ReverseCmdrHtmlCode
            : string.Empty;
    }

    public static string GetShiftHtml(this PETSCIICode[] petsciiCodes, char chr)
        => petsciiCodes.FirstOrDefault(code => code.DecimalCode == chr)?.ShiftHtmlCode ?? "";

    public static string GetCmdrHtml(this PETSCIICode[] petsciiCodes, char chr)
        => petsciiCodes.FirstOrDefault(code => code.DecimalCode == chr)?.CmdrHtmlCode ?? "";

}
