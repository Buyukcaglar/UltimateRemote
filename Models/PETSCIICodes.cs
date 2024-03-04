using UltimateRemote.Extensions;
// ReSharper disable InconsistentNaming

namespace UltimateRemote.Models;

// https://sta.c64.org/cbm64petkey.html
public static class PETSCIICodes
{
    // ShiftVariant: ~, C=Variant: |, CtrlVariant: ¨
    public static PETSCIICode[] Codes = [
        new PETSCIICode(Char: '@', HexCode: "40", ShiftHexCode: "BA", CmdrHexCode: "A4", CtrlHexCode: "00", DecimalCode: 064, ShiftDecimalCode: 186, CmdrDecimalCode: 164, CtrlDecimalCode: 000, HtmlCode: "e00", CmdrHtmlCode: "e64"),
        
        new PETSCIICode(Char: 'A', HexCode: "41", ShiftHexCode: "C1", CmdrHexCode: "B0", CtrlHexCode: "01", DecimalCode: 065, ShiftDecimalCode: 193, CmdrDecimalCode: 176, CtrlDecimalCode: 001, HtmlCode: "e01", CmdrHtmlCode: "e70"),
        new PETSCIICode(Char: 'B', HexCode: "42", ShiftHexCode: "C2", CmdrHexCode: "BF", CtrlHexCode: "02", DecimalCode: 066, ShiftDecimalCode: 194, CmdrDecimalCode: 191, CtrlDecimalCode: 002, HtmlCode: "e02", CmdrHtmlCode: "e7f"),
        new PETSCIICode(Char: 'C', HexCode: "43", ShiftHexCode: "C3", CmdrHexCode: "BC", CtrlHexCode: "03", DecimalCode: 067, ShiftDecimalCode: 195, CmdrDecimalCode: 188, CtrlDecimalCode: 003, HtmlCode: "e03", CmdrHtmlCode: "e7c"),
        new PETSCIICode(Char: 'D', HexCode: "44", ShiftHexCode: "C4", CmdrHexCode: "AC", CtrlHexCode: "04", DecimalCode: 068, ShiftDecimalCode: 196, CmdrDecimalCode: 172, CtrlDecimalCode: 004, HtmlCode: "e04", CmdrHtmlCode: "e6c"),
        new PETSCIICode(Char: 'E', HexCode: "45", ShiftHexCode: "C5", CmdrHexCode: "B1", CtrlHexCode: "05", DecimalCode: 069, ShiftDecimalCode: 197, CmdrDecimalCode: 177, CtrlDecimalCode: 005, HtmlCode: "e05", CmdrHtmlCode: "e71"),
        new PETSCIICode(Char: 'F', HexCode: "46", ShiftHexCode: "C6", CmdrHexCode: "BB", CtrlHexCode: "06", DecimalCode: 070, ShiftDecimalCode: 198, CmdrDecimalCode: 187, CtrlDecimalCode: 006, HtmlCode: "e06", CmdrHtmlCode: "e7b"),
        new PETSCIICode(Char: 'G', HexCode: "47", ShiftHexCode: "C7", CmdrHexCode: "A5", CtrlHexCode: "07", DecimalCode: 071, ShiftDecimalCode: 199, CmdrDecimalCode: 165, CtrlDecimalCode: 007, HtmlCode: "e07", CmdrHtmlCode: "e65"),
        new PETSCIICode(Char: 'H', HexCode: "48", ShiftHexCode: "C8", CmdrHexCode: "B4", CtrlHexCode: "08", DecimalCode: 072, ShiftDecimalCode: 200, CmdrDecimalCode: 180, CtrlDecimalCode: 008, HtmlCode: "e08", CmdrHtmlCode: "e74"),
        new PETSCIICode(Char: 'I', HexCode: "49", ShiftHexCode: "C9", CmdrHexCode: "A2", CtrlHexCode: "09", DecimalCode: 073, ShiftDecimalCode: 201, CmdrDecimalCode: 162, CtrlDecimalCode: 009, HtmlCode: "e09", CmdrHtmlCode: "e62"),
        new PETSCIICode(Char: 'J', HexCode: "4A", ShiftHexCode: "CA", CmdrHexCode: "B5", CtrlHexCode: "0A", DecimalCode: 074, ShiftDecimalCode: 202, CmdrDecimalCode: 181, CtrlDecimalCode: 010, HtmlCode: "e0a", CmdrHtmlCode: "e75"),
        new PETSCIICode(Char: 'K', HexCode: "4B", ShiftHexCode: "CB", CmdrHexCode: "A1", CtrlHexCode: "0B", DecimalCode: 075, ShiftDecimalCode: 203, CmdrDecimalCode: 161, CtrlDecimalCode: 011, HtmlCode: "e0b", CmdrHtmlCode: "e61"),
        new PETSCIICode(Char: 'L', HexCode: "4C", ShiftHexCode: "CC", CmdrHexCode: "B6", CtrlHexCode: "0C", DecimalCode: 076, ShiftDecimalCode: 204, CmdrDecimalCode: 182, CtrlDecimalCode: 012, HtmlCode: "e0c", CmdrHtmlCode: "e76"),
        new PETSCIICode(Char: 'M', HexCode: "4D", ShiftHexCode: "CD", CmdrHexCode: "A7", CtrlHexCode: "0D", DecimalCode: 077, ShiftDecimalCode: 205, CmdrDecimalCode: 167, CtrlDecimalCode: 013, HtmlCode: "e0d", CmdrHtmlCode: "e67"),
        new PETSCIICode(Char: 'N', HexCode: "4E", ShiftHexCode: "CE", CmdrHexCode: "A9", CtrlHexCode: "0E", DecimalCode: 078, ShiftDecimalCode: 206, CmdrDecimalCode: 169, CtrlDecimalCode: 014, HtmlCode: "e0e", CmdrHtmlCode: "e6a"),
        new PETSCIICode(Char: 'O', HexCode: "4F", ShiftHexCode: "CF", CmdrHexCode: "B9", CtrlHexCode: "0F", DecimalCode: 079, ShiftDecimalCode: 207, CmdrDecimalCode: 185, CtrlDecimalCode: 015, HtmlCode: "e0f", CmdrHtmlCode: "e79"),
        new PETSCIICode(Char: 'P', HexCode: "50", ShiftHexCode: "D0", CmdrHexCode: "AF", CtrlHexCode: "10", DecimalCode: 080, ShiftDecimalCode: 208, CmdrDecimalCode: 175, CtrlDecimalCode: 016, HtmlCode: "e10", CmdrHtmlCode: "e6f"),
        new PETSCIICode(Char: 'Q', HexCode: "51", ShiftHexCode: "D1", CmdrHexCode: "AB", CtrlHexCode: "11", DecimalCode: 081, ShiftDecimalCode: 209, CmdrDecimalCode: 171, CtrlDecimalCode: 017, HtmlCode: "e11", CmdrHtmlCode: "e6b"),
        new PETSCIICode(Char: 'R', HexCode: "52", ShiftHexCode: "D2", CmdrHexCode: "B2", CtrlHexCode: "12", DecimalCode: 082, ShiftDecimalCode: 210, CmdrDecimalCode: 178, CtrlDecimalCode: 018, HtmlCode: "e12", CmdrHtmlCode: "e72"),
        new PETSCIICode(Char: 'S', HexCode: "53", ShiftHexCode: "D3", CmdrHexCode: "AE", CtrlHexCode: "13", DecimalCode: 083, ShiftDecimalCode: 211, CmdrDecimalCode: 174, CtrlDecimalCode: 019, HtmlCode: "e13", CmdrHtmlCode: "e6e"),
        new PETSCIICode(Char: 'T', HexCode: "54", ShiftHexCode: "D4", CmdrHexCode: "A3", CtrlHexCode: "14", DecimalCode: 084, ShiftDecimalCode: 212, CmdrDecimalCode: 163, CtrlDecimalCode: 020, HtmlCode: "e14", CmdrHtmlCode: "e63"),
        new PETSCIICode(Char: 'U', HexCode: "55", ShiftHexCode: "D5", CmdrHexCode: "B8", CtrlHexCode: "15", DecimalCode: 085, ShiftDecimalCode: 213, CmdrDecimalCode: 184, CtrlDecimalCode: 021, HtmlCode: "e15", CmdrHtmlCode: "e78"),
        new PETSCIICode(Char: 'V', HexCode: "56", ShiftHexCode: "D6", CmdrHexCode: "BE", CtrlHexCode: "16", DecimalCode: 086, ShiftDecimalCode: 214, CmdrDecimalCode: 190, CtrlDecimalCode: 022, HtmlCode: "e16", CmdrHtmlCode: "e7e"),
        new PETSCIICode(Char: 'W', HexCode: "57", ShiftHexCode: "D7", CmdrHexCode: "B3", CtrlHexCode: "17", DecimalCode: 087, ShiftDecimalCode: 215, CmdrDecimalCode: 179, CtrlDecimalCode: 023, HtmlCode: "e17", CmdrHtmlCode: "e73"),
        new PETSCIICode(Char: 'X', HexCode: "58", ShiftHexCode: "D8", CmdrHexCode: "BD", CtrlHexCode: "18", DecimalCode: 088, ShiftDecimalCode: 216, CmdrDecimalCode: 189, CtrlDecimalCode: 024, HtmlCode: "e18", CmdrHtmlCode: "e7d"),
        new PETSCIICode(Char: 'Y', HexCode: "59", ShiftHexCode: "D9", CmdrHexCode: "B7", CtrlHexCode: "19", DecimalCode: 089, ShiftDecimalCode: 217, CmdrDecimalCode: 183, CtrlDecimalCode: 025, HtmlCode: "e19", CmdrHtmlCode: "e77"),
        new PETSCIICode(Char: 'Z', HexCode: "5A", ShiftHexCode: "DA", CmdrHexCode: "AD", CtrlHexCode: "1A", DecimalCode: 090, ShiftDecimalCode: 218, CmdrDecimalCode: 173, CtrlDecimalCode: 026, HtmlCode: "e1a", CmdrHtmlCode: "e6d"),
        
        new PETSCIICode(Char: '[', HexCode: "5B", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 091, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e1b", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '£', HexCode: "5C", ShiftHexCode: "A9", CmdrHexCode: "A8", CtrlHexCode: "1C", DecimalCode: 092, ShiftDecimalCode: 169, CmdrDecimalCode: 168, CtrlDecimalCode: 028, HtmlCode: "e1c", CmdrHtmlCode: "e68"),
        new PETSCIICode(Char: ']', HexCode: "5D", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 093, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e1d", CmdrHtmlCode: ""),
        
        // Code $FF is the BASIC token of the π (pi) symbol. It is converted internally to code $DE when printed and, vice versa, code $DE is converted to $FF when fetched from the screen.
        // However, when reading the keyboard buffer, you will find code $DE for Shift-↑ (up arrow) as no conversion takes place there yet.
        new PETSCIICode(Char: '↑', HexCode: "5E", ShiftHexCode: "DE", CmdrHexCode: "", CtrlHexCode: "1E", DecimalCode: 094, ShiftDecimalCode: 222, CmdrDecimalCode: -001, CtrlDecimalCode: 030, HtmlCode: "e1e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '←', HexCode: "5F", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 095, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e1f", CmdrHtmlCode: ""),
        
        new PETSCIICode(Char: ' ', HexCode: "20", ShiftHexCode: "A0", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 032, ShiftDecimalCode: 160, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e20", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '!', HexCode: "21", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 033, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e21", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '"', HexCode: "22", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 034, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e22", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '#', HexCode: "23", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 035, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e23", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '$', HexCode: "24", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 036, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e24", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '%', HexCode: "25", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 037, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e25", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '&', HexCode: "26", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 038, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e26", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\'', HexCode: "27", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 039, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e27", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '(', HexCode: "28", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: -001, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e28", CmdrHtmlCode: ""),
        new PETSCIICode(Char: ')', HexCode: "29", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: -001, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e29", CmdrHtmlCode: ""),
        
        new PETSCIICode(Char: '*', HexCode: "2A", ShiftHexCode: "C0", CmdrHexCode: "DF", CtrlHexCode: "", DecimalCode: 042, ShiftDecimalCode: 192, CmdrDecimalCode: 223, CtrlDecimalCode: -001, HtmlCode: "e2a", CmdrHtmlCode: "e5f"),
        new PETSCIICode(Char: '+', HexCode: "2B", ShiftHexCode: "DB", CmdrHexCode: "A6", CtrlHexCode: "", DecimalCode: 043, ShiftDecimalCode: 219, CmdrDecimalCode: 166, CtrlDecimalCode: -001, HtmlCode: "e2b", CmdrHtmlCode: "e66"),
        new PETSCIICode(Char: ',', HexCode: "2C", ShiftHexCode: "3C", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 044, ShiftDecimalCode: 060, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e2c", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '-', HexCode: "2D", ShiftHexCode: "DD", CmdrHexCode: "DC", CtrlHexCode: "", DecimalCode: 045, ShiftDecimalCode: 221, CmdrDecimalCode: 220, CtrlDecimalCode: -001, HtmlCode: "e2d", CmdrHtmlCode: "e5c"),
        new PETSCIICode(Char: '.', HexCode: "2E", ShiftHexCode: "3E", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 046, ShiftDecimalCode: 062, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e2e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '/', HexCode: "2F", ShiftHexCode: "3F", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 047, ShiftDecimalCode: 063, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e2f", CmdrHtmlCode: ""),
        
        // No printables for Ctrl & Cmdr variations are colors and control values for these chars
        new PETSCIICode(Char: '0', HexCode: "30", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "92", DecimalCode: 048, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: 146, HtmlCode: "e30", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '1', HexCode: "31", ShiftHexCode: "21", CmdrHexCode: "81", CtrlHexCode: "90", DecimalCode: 049, ShiftDecimalCode: 033, CmdrDecimalCode: 129, CtrlDecimalCode: 144, HtmlCode: "e31", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '2', HexCode: "32", ShiftHexCode: "22", CmdrHexCode: "95", CtrlHexCode: "05", DecimalCode: 050, ShiftDecimalCode: 034, CmdrDecimalCode: 149, CtrlDecimalCode: 005, HtmlCode: "e32", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '3', HexCode: "33", ShiftHexCode: "23", CmdrHexCode: "96", CtrlHexCode: "1C", DecimalCode: 051, ShiftDecimalCode: 035, CmdrDecimalCode: 150, CtrlDecimalCode: 028, HtmlCode: "e33", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '4', HexCode: "34", ShiftHexCode: "24", CmdrHexCode: "97", CtrlHexCode: "9F", DecimalCode: 052, ShiftDecimalCode: 036, CmdrDecimalCode: 151, CtrlDecimalCode: 159, HtmlCode: "e34", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '5', HexCode: "35", ShiftHexCode: "25", CmdrHexCode: "98", CtrlHexCode: "9C", DecimalCode: 053, ShiftDecimalCode: 037, CmdrDecimalCode: 152, CtrlDecimalCode: 156, HtmlCode: "e35", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '6', HexCode: "36", ShiftHexCode: "26", CmdrHexCode: "99", CtrlHexCode: "1E", DecimalCode: 054, ShiftDecimalCode: 038, CmdrDecimalCode: 153, CtrlDecimalCode: 030, HtmlCode: "e36", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '7', HexCode: "37", ShiftHexCode: "27", CmdrHexCode: "9A", CtrlHexCode: "1F", DecimalCode: 055, ShiftDecimalCode: 039, CmdrDecimalCode: 154, CtrlDecimalCode: 031, HtmlCode: "e37", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '8', HexCode: "38", ShiftHexCode: "28", CmdrHexCode: "9B", CtrlHexCode: "9E", DecimalCode: 056, ShiftDecimalCode: 040, CmdrDecimalCode: 155, CtrlDecimalCode: 158, HtmlCode: "e38", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '9', HexCode: "39", ShiftHexCode: "29", CmdrHexCode: "", CtrlHexCode: "12", DecimalCode: 057, ShiftDecimalCode: 041, CmdrDecimalCode: -001, CtrlDecimalCode: 018, HtmlCode: "e39", CmdrHtmlCode: ""),
        
        new PETSCIICode(Char: ':', HexCode: "3A", ShiftHexCode: "5B", CmdrHexCode: "", CtrlHexCode: "1B", DecimalCode: 058, ShiftDecimalCode: 091, CmdrDecimalCode: -001, CtrlDecimalCode: 027, HtmlCode: "e3a", CmdrHtmlCode: ""),
        new PETSCIICode(Char: ';', HexCode: "3B", ShiftHexCode: "5D", CmdrHexCode: "", CtrlHexCode: "1D", DecimalCode: 059, ShiftDecimalCode: 093, CmdrDecimalCode: -001, CtrlDecimalCode: 029, HtmlCode: "e3b", CmdrHtmlCode: ""),
        
        new PETSCIICode(Char: '<', HexCode: "3C", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 060, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e3c", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '=', HexCode: "3D", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "1F", DecimalCode: 061, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: 031, HtmlCode: "e3d", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '>', HexCode: "3E", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 062, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e3e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '?', HexCode: "3F", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 063, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e3f", CmdrHtmlCode: ""),
        
        new PETSCIICode(Char: '\r', HexCode: "0D", ShiftHexCode: "8D", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 013, ShiftDecimalCode: 141, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "<br>", CmdrHtmlCode: ""),
        
        // Codes $60-$7F and $E0-$FE are not used. Although you can print them, these are, actually, copies of codes $C0-$DF and $A0-$BE.
        // This is not true, disks having directory art (especially demo disks) seems to be using these codes.
        new PETSCIICode(Char: '\0', HexCode: "E0", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 224, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e60", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E1", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 225, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e61", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E2", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 226, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e62", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E3", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 227, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e63", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E4", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 228, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e64", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E5", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 229, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e65", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E6", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 230, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e66", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E7", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 231, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e67", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E8", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 232, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e68", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "E9", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 233, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e69", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "EA", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 234, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6a", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "EB", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 235, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6b", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "EC", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 236, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6c", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "ED", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 237, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6d", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "EE", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 238, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "EF", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 239, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6f", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F0", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 240, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e70", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F1", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 241, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e71", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F2", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 242, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e72", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F3", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 243, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e73", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F4", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 244, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e74", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F5", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 245, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e75", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F6", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 246, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e76", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F7", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 247, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e77", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F8", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 248, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e78", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "F9", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 249, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e79", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "FA", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 250, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e7a", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "FB", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 251, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e7b", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "FC", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 252, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e7c", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "FD", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 253, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e7d", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "FE", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 254, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e7e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: 'π', HexCode: "FF", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 255, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5e", CmdrHtmlCode: ""),

        new PETSCIICode(Char: '\0', HexCode: "60", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 096, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e6a", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "61", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 097, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e41", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "62", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 098, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e42", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "63", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 099, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e43", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "64", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 100, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e44", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "65", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 101, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e45", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "66", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 102, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e46", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "67", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 103, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e47", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "68", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 104, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e48", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "69", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 105, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e49", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "6A", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 106, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e4a", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "6B", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 107, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e4b", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "6C", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 108, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e4c", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "6D", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 109, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e4d", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "6E", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 110, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e4e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "6F", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 111, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e4f", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "70", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 112, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e50", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "71", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 113, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e51", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "72", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 114, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e52", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "73", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 115, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e53", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "74", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 116, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e54", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "75", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 117, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e55", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "76", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 118, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e56", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "77", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 119, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e57", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "78", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 120, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e58", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "79", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 121, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e59", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "7A", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 122, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5a", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "7B", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 123, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5b", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "7C", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 124, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5c", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "7D", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 125, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5d", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "7E", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 126, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5e", CmdrHtmlCode: ""),
        new PETSCIICode(Char: '\0', HexCode: "7F", ShiftHexCode: "", CmdrHexCode: "", CtrlHexCode: "", DecimalCode: 127, ShiftDecimalCode: -001, CmdrDecimalCode: -001, CtrlDecimalCode: -001, HtmlCode: "e5f", CmdrHtmlCode: ""),

    ];

    public static string GetHexValue(string str)
    {
        var retVal = string.Empty;

        for (int i = 0; i < str.Length; i++)
        {
            if(str[i] == '~') continue;
            retVal += i + 1 < str.Length && str[i + 1] == '~' ? Codes.GetShiftHex(str[i]) : Codes.GetHex(str[i]);
        }

        return retVal;
    }

    public static string GetHexValue(int[] decimalCodes)
        => decimalCodes.Aggregate(string.Empty, (current, decimalCode) => current + Codes.GetHex(decimalCode));

    public static string GetHtmlCodesString(int[] decimalCodes)
    {
        var retVal = "";
        foreach (var decimalCode in decimalCodes)
        {
            var htmlCode = Codes.GetHtmlCode(decimalCode);
            if (!string.IsNullOrWhiteSpace(htmlCode))
                retVal += htmlCode.Length == 3 ? $"&#x0e{htmlCode};" : htmlCode;
        }
        return retVal;
    }

    // // ShiftVariant: ~, C=Variant: |, CtrlVariant: ¨
    public static string GetHtmlCodesString(string str)
    {
        var retVal = string.Empty;
        var controlChars = "~|\u00a8";

        for (int i = 0; i < str.Length; i++)
        {
            if(controlChars.Contains(str[i])) continue;
            
            var htmlCode = "";
            
            if (i + 1 < str.Length && str[i + 1] == '~')
                htmlCode = Codes.GetShiftHtml(str[i]);

            if (i + 1 < str.Length && str[i + 1] == '|')
                htmlCode = Codes.GetCmdrHtml(str[i]);
            
            if(string.IsNullOrWhiteSpace(htmlCode))
                htmlCode = Codes.GetHtmlCode(str[i]);
            
            if(!string.IsNullOrWhiteSpace(htmlCode))
                retVal += htmlCode.Length == 3 ? $"&#x0e{htmlCode};" : htmlCode;
        }

        return retVal;
    }

}

public record PETSCIICode(
    char Char,
    string HexCode,
    string ShiftHexCode,
    string CmdrHexCode,
    string CtrlHexCode,
    int DecimalCode,
    int ShiftDecimalCode,
    int CmdrDecimalCode,
    int CtrlDecimalCode,
    string HtmlCode,
    string CmdrHtmlCode)
{
    public string ReverseHexCode => ReverseDecimalCode > 0 ? ReverseDecimalCode.ToString("X2") : "";

    public string ReverseShiftHexCode => ReverseShiftDecimalCode > 0 ? ReverseShiftDecimalCode.ToString("X2") : "";

    public string ReverseCmdrHexCode => ReverseCmdrDecimalCode > 0 ? ReverseCmdrDecimalCode.ToString("X2") : "";

    public int ReverseDecimalCode => GetReverseDecimal(DecimalCode);

    public int ReverseShiftDecimalCode => GetReverseDecimal(ShiftDecimalCode);

    public int ReverseCmdrDecimalCode => GetReverseDecimal(CmdrDecimalCode);

    public string ShiftHtmlCode => GetShiftHtml(HtmlCode);

    public string ReverseHtmlCode => GetReverseHtml(HtmlCode);
    
    public string ReverseShiftHtmlCode => GetReverseHtml(ShiftHtmlCode);

    public string ReverseCmdrHtmlCode => GetReverseHtml(CmdrHtmlCode);

    private int GetReverseDecimal(int decimalValue)
        => decimalValue < 0 ? decimalValue : decimalValue > 63 ? decimalValue - 64 : decimalValue;

    private string GetReverseHtml(string htmlCode)
        => !string.IsNullOrWhiteSpace(htmlCode) ? (Convert.ToInt32(htmlCode, 16) + 128).ToString("x2") : "";

    private string GetShiftHtml(string htmlCode)
        => !string.IsNullOrWhiteSpace(htmlCode) ? (Convert.ToInt32(htmlCode, 16) + 64).ToString("x2") : "";

}


