function ltrim(str) {
     for (var k = 0; k < str.length && isWhitespace(str.charAt(k)) ; k++);
     return str.substring(k, str.length);
}
function rtrim(str) {
     for (var j = str.length - 1; j >= 0 && isWhitespace(str.charAt(j)) ; j--);
     return str.substring(0, j + 1);
}
function trim(str) {
     return ltrim(rtrim(str));
}
function isWhitespace(charToCheck) {
     var whitespaceChars = " \t\n\r\f";
     return (whitespaceChars.indexOf(charToCheck) != -1);
}
