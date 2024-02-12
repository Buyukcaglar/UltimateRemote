
var _dotNetref;
export function initEditor(dotNetRef) {
    
    _dotNetref = dotNetRef;
    
    $('#basiceditor').on('keypress', function (e) {
        var lines = $(this).val().split('\n');
        var cursorPosition = this.selectionStart;
        var currentLine = 0;
        var currentLineLength = 0;

        for (var i = 0; i < lines.length; i++) {
            currentLineLength += lines[i].length + 1; // +1 for the newline character
            if (cursorPosition <= currentLineLength) {
                currentLine = i;
                break;
            }
        }

        if (lines[currentLine].length >= 160 && e.which !== 8 && e.which !== 13) {
            e.preventDefault();
        }
    });    
   
}

export function parseBASIC() {
    var textarea = $("#basiceditor").val();

    // Split into lines, remove blank lines, convert to lowercase, and join back
    var temp = textarea.split('\n')
        .map(line => line.trim().toLowerCase())
        .filter(line => line.trim().length > 0)
        .join('\n');

    $("#basiceditor").val(temp);
    var lines = temp.split('\n');

    const tokenized_lines = [];
    let addr = 2049;

    // Process each line
    for (let x = 0; x < lines.length; x++) {

        const tokenizedLine = new TokenizedLine(lines[x].trim(), addr);
        addr += tokenizedLine.length();
        tokenized_lines.push(tokenizedLine)
    }
    
    let i = 1
    while (i < tokenized_lines.length) {
        tokenized_lines[i - 1].next_addr = tokenized_lines[i].addr
        i += 1;
    }

    tokenized_lines[i - 1].next_addr = tokenized_lines[i - 1].addr + tokenized_lines[i - 1].bytes.length + 5;
    
    var data = convertData(tokenized_lines);
    
    var retVal; // = {};
    if (basic_error !== "") {
        $("#basicmsg").text(basic_error);
        $("#basicmsg").show();
    }
    else {
        $("#basicmsg").hide();
        
        //let params = { "address": "0801" };
        //let [status_code, content] = make_post_request("http://" + deviceIp + "/v1/machine:writemem", params, data);

        let varptr = toHex16(tokenized_lines[i - 1].next_addr + 2);
        varptr = varptr.substring(2, 4) + varptr.substring(0, 2);

        //params = { "address": "002d", "data": varptr };
        //[status_code, content] = make_put_request("http://" + deviceIp + "/v1/machine:writemem", params);

        retVal = {
            "address1": "0801",
            "data1": data,
            "address2": "002d",
            "data2": varptr
        };        
    }

    basic_error = "";
    
    return retVal;
}


function getAsciiValues(str) {
    var asciiValues = [];
    for (var i = 0; i < str.length; i++) {
        asciiValues.push(str.charCodeAt(i));
    }
    return asciiValues;
}

var basic_error = "";

const TOKENS = [
    ['restore', 140],
    ['input#', 132],
    ['return', 142],
    ['verify', 149],
    ['print#', 152],
    ['right$', 201],
    ['input', 133],
    ['gosub', 141],
    ['print', 153],
    ['close', 160],
    ['left$', 200],
    ['next', 130],
    ['data', 131],
    ['read', 135],
    ['goto', 137],
    ['stop', 144],
    ['wait', 146],
    ['load', 147],
    ['save', 148],
    ['poke', 151],
    ['cont', 154],
    ['list', 155],
    ['open', 159],
    ['tab(', 163],
    ['spc(', 166],
    ['then', 167],
    ['step', 169],
    ['peek', 194],
    ['str$', 196],
    ['chr$', 199],
    ['mid$', 202],
    ['end', 128],
    ['for', 129],
    ['dim', 134],
    ['let', 136],
    ['run', 138],
    ['rem', 143],
    ['def', 150],
    ['clr', 156],
    ['cmd', 157],
    ['sys', 158],
    ['get', 161],
    ['new', 162],
    ['not', 168],
    ['and', 175],
    ['sgn', 180],
    ['int', 181],
    ['abs', 182],
    ['usr', 183],
    ['fre', 184],
    ['pos', 185],
    ['sqr', 186],
    ['rnd', 187],
    ['log', 188],
    ['exp', 189],
    ['cos', 190],
    ['sin', 191],
    ['tan', 192],
    ['atn', 193],
    ['len', 195],
    ['val', 197],
    ['asc', 198],
    ['if', 139],
    ['on', 145],
    ['to', 164],
    ['fn', 165],
    ['or', 176],
    ['go', 203],
    ['+', 170],
    ['-', 171],
    ['*', 172],
    ['/', 173],
    ['^', 174],
    ['>', 177],
    ['=', 178],
    ['<', 179]
];

const TOKENS_UPPERCASE = TOKENS.map(token => [token[0].toUpperCase(), token[1]]);

const SPECIAL = [
    ['{rvs off}', 0x92],
    ['{rvs on}', 0x12],
    ['{up}', 0x91],
    ['{down}', 0x11],
    ['{left}', 0x9d],
    ['{rght}', 0x1d],
    ['{clr}', 0x93],
    ['{clear}', 0x93],
    ['{home}', 0x13],

    ['{blk}', 0x90],
    ['{wht}', 0x05],
    ['{red}', 0x1c],
    ['{cyn}', 0x9f],
    ['{pur}', 0x9c],
    ['{grn}', 0x1e],
    ['{blu}', 0x1f],
    ['{yel}', 0x9e],
    ['{org}', 0x81],
    ['{brn}', 0x95],
    ['{lred}', 0x96],
    ['{dgry}', 0x97],
    ['{mgry}', 0x98],
    ['{lgrn}', 0x99],
    ['{lblu}', 0x9a],
    ['{lgry}', 0x9b]


];

function asciiToPetscii(o) {
    // Check if character code is less than or equal to '@' or is '[' or ']'
    if (o <= '@'.charCodeAt(0) || o === '['.charCodeAt(0) || o === ']'.charCodeAt(0)) {
        return o;
    }
    // Check if character code is between 'a' and 'z'
    if (o >= 'a'.charCodeAt(0) && o <= 'z'.charCodeAt(0)) {
        return o - 'a'.charCodeAt(0) + 0x41;
    }
    // Check if character code is between 'A' and 'Z'
    if (o >= 'A'.charCodeAt(0) && o <= 'Z'.charCodeAt(0)) {
        return o - 'A'.charCodeAt(0) + 0x61 + 0x60;
    }

    basic_error = "Error -> ..." + o + " \nUnable to convert to PETSCII value.";

}

function scan(s, tokenize = true) {
    if (tokenize) {
        for (let i = 0; i < TOKENS.length; i++) {
            let [token, value] = TOKENS[i];
            if (s.startsWith(token)) {
                return [value, s.substring(token.length)];
            }
        }
    }
    if (s[0] === '{') {
        for (let i = 0; i < SPECIAL.length; i++) {
            let [token, value] = SPECIAL[i];
            if (s.startsWith(token)) {
                return [value, s.substring(token.length)];
            }
        }
        basic_error = "Error -> ..." + s + " \nInvalid code.";
    }
    return [asciiToPetscii(s.charCodeAt(0)), s.substring(1)];
}

function scanLineNumber(s) {
    s = s.trimStart();
    let acc = [];
    while (s && s[0].match(/\d/)) {
        acc.push(s[0]);
        s = s.substring(1);
    }
    return [parseInt(acc.join(''), 10), s.trimStart()];
}

function tokenize(s) {
    let [lineNumber, remainingString] = scanLineNumber(s);
    let bytes = [];
    let inQuotes = false;
    let inRemark = false;

    while (remainingString) {
        let [byte, newString] = scan(remainingString, !(inQuotes || inRemark));
        bytes.push(byte);
        remainingString = newString;

        if (byte === '"'.charCodeAt(0)) {
            inQuotes = !inQuotes;
        }
        if (byte === 143) {
            inRemark = true;
        }
    }

    return [lineNumber, bytes];
}



class TokenizedLine {
    constructor(s, addr) {
        let [lineNumber, bytes] = tokenize(s); // Ensure tokenize function is defined
        this.lineNumber = lineNumber;
        this.bytes = bytes;
        this.addr = addr;
        //this.nextAddr = null;
    }

    toString() {
        return `${this.lineNumber} @${this.addr}: ${this.bytes}`;
    }

    length() {
        return this.bytes.length + 5;
    }
}


function convertData(dataArray) {
    let result = [];

    dataArray.forEach(item => {
        // Extract low and high bytes of the next address and line number
        let nextAddrLowByte = item.next_addr & 0xFF;
        let nextAddrHighByte = (item.next_addr >> 8) & 0xFF;
        let lineNumberLowByte = item.lineNumber & 0xFF;
        let lineNumberHighByte = (item.lineNumber >> 8) & 0xFF;

        // Append to the result
        result.push(nextAddrLowByte, nextAddrHighByte, lineNumberLowByte, lineNumberHighByte);
        result.push(...item.bytes);
        result.push(0); // Terminator
    });
    result.push(0);
    result.push(0);

    return new Uint8Array(result);
}

function toHex16(num) {
    return num.toString(16).padStart(4, '0').toUpperCase();
}


function make_post_request(url, params, body) {
    const queryString = Object.entries(params).map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`).join('&');
    const fullUrl = `${url}?${queryString}`;

    try {
        const response = $.ajax({
            url: fullUrl,
            method: 'POST',
            contentType: 'application/octet-stream',
            data: body,
            processData: false
        });

        return [200, response];
    } catch (error) {
        console.error("Error fetching data:", error);
        console.log('Error details:', {
            textStatus: error.statusText,
            status: error.status,
            responseText: error.responseText
        });
        return [error.status, error.responseText];
        //const statusCode = error && error.status ? error.status : 500;
        //return [statusCode, response];
    }
}

function make_put_request(url, params) {
    const queryString = Object.entries(params).map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`).join('&');
    const fullUrl = `${url}?${queryString}`;

    try {
        const response = $.ajax({
            url: fullUrl,
            method: 'PUT',
            contentType: 'application/octet-stream',
            processData: false
        });

        return [200, response];
    } catch (error) {
        console.error("Error fetching data:", error);
        console.log('Error details:', {
            textStatus: error.statusText,
            status: error.status,
            responseText: error.responseText
        });
        return [error.status, error.responseText];
        //const statusCode = error && error.status ? error.status : 500;
        //return [statusCode, response];
    }
}