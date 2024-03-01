using UltimateRemote.Extensions;
// ReSharper disable InconsistentNaming

namespace UltimateRemote.Models;

// https://sta.c64.org/cbm64petkey.html
public static class PETSCIICodes
{
    public static PETSCIICode[] Codes = [
        new PETSCIICode(Char: 'A', HexCode: "41", ReverseHexCode: "C1"),
        new PETSCIICode(Char: 'B', HexCode: "42", ReverseHexCode: "C2"),
        new PETSCIICode(Char: 'C', HexCode: "43", ReverseHexCode: "C3"),
        new PETSCIICode(Char: 'D', HexCode: "44", ReverseHexCode: "C4"),
        new PETSCIICode(Char: 'E', HexCode: "45", ReverseHexCode: "C5"),
        new PETSCIICode(Char: 'F', HexCode: "46", ReverseHexCode: "C6"),
        new PETSCIICode(Char: 'G', HexCode: "47", ReverseHexCode: "C7"),
        new PETSCIICode(Char: 'H', HexCode: "48", ReverseHexCode: "C8"),
        new PETSCIICode(Char: 'I', HexCode: "49", ReverseHexCode: "C9"),
        new PETSCIICode(Char: 'J', HexCode: "4A", ReverseHexCode: "CA"),
        new PETSCIICode(Char: 'K', HexCode: "4B", ReverseHexCode: "CB"),
        new PETSCIICode(Char: 'L', HexCode: "4C", ReverseHexCode: "CC"),
        new PETSCIICode(Char: 'M', HexCode: "4D", ReverseHexCode: "CD"),
        new PETSCIICode(Char: 'N', HexCode: "4E", ReverseHexCode: "CE"),
        new PETSCIICode(Char: 'O', HexCode: "4F", ReverseHexCode: "CF"),
        new PETSCIICode(Char: 'P', HexCode: "50", ReverseHexCode: "D0"),
        new PETSCIICode(Char: 'Q', HexCode: "51", ReverseHexCode: "D1"),
        new PETSCIICode(Char: 'R', HexCode: "52", ReverseHexCode: "D2"),
        new PETSCIICode(Char: 'S', HexCode: "53", ReverseHexCode: "D3"),
        new PETSCIICode(Char: 'T', HexCode: "54", ReverseHexCode: "D4"),
        new PETSCIICode(Char: 'U', HexCode: "55", ReverseHexCode: "D5"),
        new PETSCIICode(Char: 'V', HexCode: "56", ReverseHexCode: "D6"),
        new PETSCIICode(Char: 'W', HexCode: "57", ReverseHexCode: "D7"),
        new PETSCIICode(Char: 'X', HexCode: "58", ReverseHexCode: "D8"),
        new PETSCIICode(Char: 'Y', HexCode: "59", ReverseHexCode: "D9"),
        new PETSCIICode(Char: 'Z', HexCode: "5A", ReverseHexCode: "DA"),
        
        new PETSCIICode(Char: '[', HexCode: "1B", ReverseHexCode: "9B"),
        new PETSCIICode(Char: '£', HexCode: "1C", ReverseHexCode: "9C"),
        new PETSCIICode(Char: ']', HexCode: "1D", ReverseHexCode: "9D"),
        new PETSCIICode(Char: '↑', HexCode: "1E", ReverseHexCode: "9E"),
        new PETSCIICode(Char: '←', HexCode: "1F", ReverseHexCode: "9F"),
        new PETSCIICode(Char: ' ', HexCode: "20", ReverseHexCode: "A0"),
        new PETSCIICode(Char: '!', HexCode: "21", ReverseHexCode: "A1"),
        new PETSCIICode(Char: '"', HexCode: "22", ReverseHexCode: "A2"),
        new PETSCIICode(Char: '#', HexCode: "23", ReverseHexCode: "A3"),
        new PETSCIICode(Char: '$', HexCode: "24", ReverseHexCode: "A4"),
        new PETSCIICode(Char: '%', HexCode: "25", ReverseHexCode: "A5"),
        new PETSCIICode(Char: '&', HexCode: "26", ReverseHexCode: "A6"),
        new PETSCIICode(Char: '\'', HexCode: "27", ReverseHexCode: "A7"),
        new PETSCIICode(Char: '(', HexCode: "28", ReverseHexCode: "A8"),
        new PETSCIICode(Char: ')', HexCode: "29", ReverseHexCode: "A9"),
        new PETSCIICode(Char: '*', HexCode: "2A", ReverseHexCode: "AA"),
        new PETSCIICode(Char: '+', HexCode: "2B", ReverseHexCode: "AB"),
        new PETSCIICode(Char: ',', HexCode: "2C", ReverseHexCode: "AC"),
        new PETSCIICode(Char: '-', HexCode: "2D", ReverseHexCode: "AD"),
        new PETSCIICode(Char: '.', HexCode: "2E", ReverseHexCode: "AE"),
        new PETSCIICode(Char: '/', HexCode: "2F", ReverseHexCode: "AF"),
        new PETSCIICode(Char: '0', HexCode: "30", ReverseHexCode: "B0"),
        new PETSCIICode(Char: '1', HexCode: "31", ReverseHexCode: "B1"),
        new PETSCIICode(Char: '2', HexCode: "32", ReverseHexCode: "B2"),
        new PETSCIICode(Char: '3', HexCode: "33", ReverseHexCode: "B3"),
        new PETSCIICode(Char: '4', HexCode: "34", ReverseHexCode: "B4"),
        new PETSCIICode(Char: '5', HexCode: "35", ReverseHexCode: "B5"),
        new PETSCIICode(Char: '6', HexCode: "36", ReverseHexCode: "B6"),
        new PETSCIICode(Char: '7', HexCode: "37", ReverseHexCode: "B7"),
        new PETSCIICode(Char: '8', HexCode: "38", ReverseHexCode: "B8"),
        new PETSCIICode(Char: '9', HexCode: "39", ReverseHexCode: "B9"),
        new PETSCIICode(Char: ':', HexCode: "3A", ReverseHexCode: "BA"),
        new PETSCIICode(Char: ';', HexCode: "3B", ReverseHexCode: "BB"),
        new PETSCIICode(Char: '<', HexCode: "3C", ReverseHexCode: "BC"),
        new PETSCIICode(Char: '=', HexCode: "3D", ReverseHexCode: "BD"),
        new PETSCIICode(Char: '>', HexCode: "3E", ReverseHexCode: "BE"),
        new PETSCIICode(Char: '?', HexCode: "3F", ReverseHexCode: "BF"),

        new PETSCIICode(Char: '@', HexCode: "40", ReverseHexCode: "C0"),
        
        new PETSCIICode(Char: '\r', HexCode: "0D", ReverseHexCode: "0D"),

    ];

    public static string GetHexValue(string str)
    {
        var retVal = string.Empty;

        for (int i = 0; i < str.Length; i++)
        {
            if(str[i] == '~') continue;
            retVal += i + 1 < str.Length && str[i + 1] == '~' ? Codes.GetReverseHex(str[i]) : Codes.GetHex(str[i]);
        }

        return retVal;
    }

}

public record PETSCIICode(char Char, string HexCode, string ReverseHexCode);

