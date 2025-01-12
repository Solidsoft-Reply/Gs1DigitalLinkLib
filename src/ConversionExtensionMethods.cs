// --------------------------------------------------------------------------
// <copyright file="ConversionExtensionMethods.cs" company="Solidsoft Reply Ltd.">
// Copyright © 2025 Solidsoft Reply Ltd. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <summary>
// Extension methods for data manipulation and conversion.
// </summary>
// --------------------------------------------------------------------------

#pragma warning disable SA1512 // Single-line comments should not be followed by blank line
#pragma warning disable SA1515 // Single-line comments should not be followed by blank line
// Ignore Spelling: Fnc
#pragma warning restore SA1512 // Single-line comments should not be followed by blank line
#pragma warning restore SA1515 // Single-line comments should not be followed by blank line

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Microsoft.Extensions.Logging;

using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Extension methods for data manipulation and conversion.
/// </summary>
internal static partial class ConversionExtensionMethods {

    /// <summary>
    /// Regex that checks that a string is composed only of digits.
    /// </summary>
    private static readonly Regex _regexAllNum = RegexAllNumbers();

    /// <summary>
    /// Regex that checks that a string is composed of lower-case hexadecimal characters.
    /// </summary>
    private static readonly Regex _regexHexLower = RegexHexLower();

    /// <summary>
    /// Regex that checks that a string is composed of upper-case hexadecimal characters.
    /// </summary>
    private static readonly Regex _regexHexUpper = RegexHexUpper();

    /// <summary>
    /// Regex that checks that a string is composed only using a 64-character
    /// set that is safe for compressed data.
    /// </summary>
    private static readonly Regex _regexSafe64 = RegexSafe64();

    /// <summary>
    /// A regular expression to match a string that contains only characters
    /// allowed in keys for non-GS12 query string key-value values.
    /// </summary>
    private static readonly Regex _regexQueryStringKey = RegexQueryStringKey();

    /// <summary>
    /// A regular expression to match a string that contains only characters
    /// allowed in URI query strings.
    /// </summary>
    private static readonly Regex _regexQueryString = RegexQueryString();

    /// <summary>
    /// A regular expression to match a string that contains only characters
    /// allowed in URI fragments.
    /// </summary>
    private static readonly Regex _regexFragments = RegexFragments();

    /// <summary>
    /// A read-only list of AIs that require termination using the FNC1 character.
    /// </summary>
    private static readonly AiTable _aiTable = AiTable.Create();

    /// <summary>
    /// A read-only dictionary of Identifier and qualifier path constraints..
    /// </summary>
    private static readonly PathConstraints _pathSequenceConstraints = PathConstraints.Create();

    /// <summary>
    /// The months of the year with thirty days.
    /// </summary>
    private static readonly string[] _thirtyDayMonths = ["04", "06", "09", "11"];

    /// <summary>
    /// Characters that must be escaped when performing percent-encoding.
    /// </summary>
    private static readonly string _charsToEscape = "#/%&+,!()*':;<=>?";

    /// <summary>
    /// Pad a string with leading zeros to a required length.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="requiredLength">The required length of the padded string.</param>
    /// <returns>A padded string.</returns>
    public static string PadStringToLength(this string input, int requiredLength) {
        if (input.Length < requiredLength) {
            input = new string('0', requiredLength - input.Length) + input;
        }

        return input;
    }

    /// <summary>
    /// Percent-encode all reserved characters mentioned in the GS1 Digital Link standard.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>Percent-encoded string.</returns>
    public static string PercentEncode(this string input) {
        List<string> escaped = [];

        for (int idx = 0; idx < input.Length; idx++) {
            char testChar = input[idx];
            if (_charsToEscape.IndexOf(testChar) > -1) {
                escaped.Add("%" + ((int)testChar).ToString("X2"));
            }
            else {
                escaped.Add(testChar.ToString());
            }
        }

        return string.Join(string.Empty, escaped);
    }

    /// <summary>
    /// Percent-decode a string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>The percent-decoded string.</returns>
    public static string PercentDecode(this string input) {
        return Uri.UnescapeDataString(input);
    }

    /// <summary>
    /// Converts a binary string to hexadecimal.
    /// </summary>
    /// <param name="binStr">The binary string.</param>
    /// <returns>The hexadecimal representation of the binary string.</returns>
    public static string BinStrToHex(this string binStr) {
        int val = Convert.ToInt32(binStr, 2);
        return val.ToString("X").ToUpper();
    }

    /// <summary>
    /// Return the number of bits required to encode the length specifier for a value.
    /// </summary>
    /// <param name="maxLength">The maximum length of the value.</param>
    /// <returns>The number of bits required to encode a length specifier.</returns>
    public static int NumberOfLengthBits(this int maxLength) {
        return (int)Math.Ceiling((Math.Log(maxLength) / Math.Log(2)) + 0.01);
    }

    /// <summary>
    /// Return the number of bits required to encode a value of a given length.
    /// </summary>
    /// <param name="valueLength">The length of the value to be encoded.</param>
    /// <returns>The number of bits required to encode a value.</returns>
    public static int NumberOfValueBits(this int valueLength) {
        return (int)Math.Ceiling((valueLength * Math.Log(10) / Math.Log(2)) + 0.01);
    }

    /// <summary>
    /// Converts a ten-digit date (YYMMDDhhmm) to an XSD date.
    /// </summary>
    /// <param name="tenDigit">The six-digit date (YYMMDDhhmm).</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>An XSD date representation.</returns>
    /// <exception cref="Gs1DigitalLinkException">Invalid date representation.</exception>
    public static string TenDigitToXsdDateTime(this string tenDigit, string methodName, string paramName) {
        var dateRegex = RegexTenDigitDate();

        if (!dateRegex.IsMatch(tenDigit)) {
            var message = string.Format(Resources.Errors.ErrorMsgInput0ToDateConversionDidNotMatchValidYymmddhhmmPattern, tenDigit);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw LogAndReturnException(Resources.Errors.ErrorTypeSyntaxError, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
        }

        var year = tenDigit[..2];
        var month = tenDigit.Substring(2, 2);
        var day = tenDigit.Substring(4, 2);
        var hour = tenDigit.Substring(6, 2);
        var mins = tenDigit.Substring(8, 2);
        var intendedYear = DetermineFourDigitYear(year);

        var intendedDateTime = intendedYear + "-" + month + "-" + day + "T" + hour + ":" + mins + ":00";
        return intendedDateTime;
    }

    /// <summary>
    /// Converts a (max) twelve-digit date (YYMMDDhh[mm][ss]) to an XSD date.
    /// </summary>
    /// <param name="twelveDigit">The (max) twelve-digit date (YYMMDDhh[mm][ss]).</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>An XSD date representation.</returns>
    /// <exception cref="Gs1DigitalLinkException">Invalid date representation.</exception>
    public static string MaxTwelveDigitToXsdDateTime(this string twelveDigit, string methodName, string paramName) {
        var dateRegex = RegexMaxTwelveDigitDate();

        if (!dateRegex.IsMatch(twelveDigit)) {
            var message = string.Format(Resources.Errors.ErrorMsgInput0ToDateConversionDidNotMatchValidYymmddhhMmSsPattern, twelveDigit);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw LogAndReturnException(Resources.Errors.ErrorTypeSyntaxError, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
        }

        var year = twelveDigit[..2];
        var month = twelveDigit.Substring(2, 2);
        var day = twelveDigit.Substring(4, 2);
        var hour = twelveDigit.Substring(6, 2);
        string min;
        string sec;

        if (twelveDigit.Length > 8) {
            min = twelveDigit.Substring(8, 2);
        }
        else {
            min = "00";
        }

        if (twelveDigit.Length > 10) {
            sec = twelveDigit.Substring(10, 2);
        }
        else {
            sec = "00";
        }

        var intendedYear = DetermineFourDigitYear(year);
        var intendedDateTime = intendedYear + "-" + month + "-" + day + "T" + hour + ":" + min + ":" + sec;
        return intendedDateTime;
    }

    /// <summary>
    /// Converts a six-digit date (YYMMDD) to an XSD date.
    /// </summary>
    /// <param name="sixDigit">The six-digit date (YYMMDD).</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>An XSD date representation.</returns>
    /// <exception cref="Gs1DigitalLinkException">Invalid date representation.</exception>
    public static string SixDigitToXsdDate(this string sixDigit, string methodName, string paramName) {
        var dateRegex = RegexSixDigitDate();
        if (!dateRegex.IsMatch(sixDigit)) {
            var message = string.Format(Resources.Errors.ErrorMsgInput0ToDateConversionDidNotMatchValidYymmddPattern, sixDigit);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw LogAndReturnException(Resources.Errors.ErrorTypeSyntaxError, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
        }

        var year = sixDigit[..2];
        var month = sixDigit.Substring(2, 2);
        var day = sixDigit.Substring(4, 2);
        var intendedYear = int.Parse(DetermineFourDigitYear(year));

        var lastDay = 31;

        if (_thirtyDayMonths.Contains(month)) {
            lastDay = 30;
        }

        if (month == "02") {
            lastDay = 28;
            if ((intendedYear % 400 == 0) || ((intendedYear % 4 == 0) && ((intendedYear % 100) != 0))) {
                lastDay = 29;
            }
        }

        if (int.Parse(day) > lastDay) {
            var message = string.Format(Resources.Errors.ErrorMsgInputDate0InvalidDdTooLargeForMm, sixDigit);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw LogAndReturnException(Resources.Errors.ErrorTypeSyntaxError, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
        }

        string intendedDate;

        if (day == "00") {
            intendedDate = intendedYear + "-" + month + "-" + lastDay.ToString("00");
        }
        else {
            intendedDate = intendedYear + "-" + month + "-" + day;
        }

        return intendedDate;
    }

    /// <summary>
    /// Converts a binary string to a BigInteger.
    /// </summary>
    /// <param name="input">The binary string.</param>
    /// <returns>A BigIntegre value representing the binary string.</returns>
    /// <exception cref="FormatException">Invalid binary number.</exception>
    public static BigInteger BinaryLiteralToBigInteger(this string input) {
        BigInteger bigVal = 0;

        foreach (char inoutChar in input) {

            if (inoutChar != '0' && inoutChar != '1') {
                throw new FormatException("The input string is not a valid binary number.");
            }

            bigVal <<= 1;
            if (inoutChar == '1') {
                bigVal += 1;
            }
        }

        return bigVal;
    }

    /// <summary>
    /// Converts a decimal number to a binary string.
    /// </summary>
    /// <param name="input">The decimal number.</param>
    /// <returns>A binary string.</returns>
    /// <exception cref="FormatException">Invalid format for decimal number.</exception>
    public static string DecimalLiteralToBinaryLiteral(this string input) {
        if (input == string.Empty) {
            return "0";
        }

        if (BigInteger.TryParse(input, out BigInteger bigVal)) {
            return ConvertToBinaryString(bigVal);
        }
        else {
            throw new FormatException("The input string is not a valid decimal number.");
        }
    }

    /// <summary>
    /// Returns a binary encoding of a GS1 AI.
    /// </summary>
    /// <param name="key">The GS1 AI.</param>
    /// <returns>A binary encoding.</returns>
    public static string BinaryEncodingOfGS1AIKey(this string key) {
        // Construct binary encoding of AI key as sequence of 4-bit digits.
        // This also supports hex values of key.
        var binAi = new StringBuilder();

        for (var idx = 0; idx < key.Length; idx++) {
            var value = Convert.ToInt32(key[idx].ToString(), 16);
            var bits = Convert.ToString(value, 2);
            bits = bits.PadStringToLength(4);
            binAi.Append(bits);
        }

        return binAi.ToString();
    }

    /// <summary>
    /// Converts a binary string to a 'safe 64' string.
    /// </summary>
    /// <param name="binStr">The binary string.</param>
    /// <returns>A 'safe 64' string.</returns>
    public static string BinaryToSafe64(this string binStr) {
        var returnValue = string.Empty;

        if (binStr.Length % 6 > 0) {
            var numberRightPadZeros = 6 - (binStr.Length % 6);
            binStr += new string('0', numberRightPadZeros);
        }

        int numChar = binStr.Length / 6;

        for (var idx = 0; idx < numChar; idx++) {
            var binFrag = binStr.Substring(6 * idx, 6);
            var safeFrag = Convert.ToInt32(binFrag, 2);
            var base64char = Gs1DigitalLinkConvert.SafeBase64Alphabet.Substring(safeFrag, 1);
            returnValue += base64char;
        }

        return returnValue;
    }

    /// <summary>
    /// Converts a 'safe 64' string to a binary representation.
    /// </summary>
    /// <param name="safe64str">The 'safe 64' string.</param>
    /// <returns>A binary string.</returns>
    public static string Safe64ToBinary(this string safe64str) {
        var returnValue = string.Empty;

        for (var idx = 0; idx < safe64str.Length; idx++) {
            var decimalDigit = Gs1DigitalLinkConvert.SafeBase64Alphabet.IndexOf(safe64str.Substring(idx, 1));
            var binaryString = Convert.ToString(decimalDigit, 2);

            if (binaryString.Length < 6) {
                binaryString = new string('0', 6 - binaryString.Length) + binaryString;
            }

            returnValue += binaryString;
        }

        return returnValue;
    }

    /// <summary>
    /// Encodes a value and appends it to a binary string.
    /// </summary>
    /// <param name="encoding">The encoding specifier.</param>
    /// <param name="lengthBits">The length bits.</param>
    /// <param name="charStr">The value to be encoded.</param>
    /// <param name="binStr">The binary string.</param>
    /// <returns>Encoded binary string.</returns>
    public static string HandleEncodings(this int encoding, string lengthBits, string charStr, string binStr) {
        /* Creates a binary string fragment that starts with a 3-digit encoding indicator, any
         * lengthBits specified (empty string "" if not required for a fixed-length value) and
         * then the actual value of charStr in binaryEncodingOfGS1AIKey. Calls BuildBinaryValue()
         * when encoding the value in binary.
         * */

        switch (encoding) {
            case 0:
                // all-numeric
                int binLength = charStr.Length.NumberOfValueBits();

                var binValue = BigInteger.Parse(charStr)
                    .BigIntegerToBinaryString()
                    .PadStringToLength(binLength);
                binStr += "000" + lengthBits + binValue;
                break;

            case 1:
                binStr += "001" + lengthBits + BuildBinaryValue(charStr.ToUpper(), 4, Gs1DigitalLinkConvert.HexAlphabet);
                break;

            case 2:
                binStr += "010" + lengthBits + BuildBinaryValue(charStr.ToUpper(), 4, Gs1DigitalLinkConvert.HexAlphabet);
                break;

            case 3:
                binStr += "011" + lengthBits + BuildBinaryValue(charStr, 6, Gs1DigitalLinkConvert.SafeBase64Alphabet);
                break;

            case 4:
                binStr += "100" + lengthBits + BuildBinaryValue(charStr, 7, string.Empty);
                break;
        }

        return binStr;
    }

    /// <summary>
    /// Decodes a binary string and returns the decoded data.
    /// </summary>
    /// <param name="encoding">The encoding specifier.</param>
    /// <param name="binStr">A binary string.</param>
    /// <param name="cursor">The current cursor index.</param>
    /// <param name="gs1DigitalLinkData">A dictionary of GS1 AIs.</param>
    /// <param name="nonGs1KeyValuePairs">A dictionary on non-GS1 key-value pairs.</param>
    /// <param name="key">A GS1 AI.</param>
    /// <param name="numChars">The number of characters to extract.</param>
    /// <returns>The decoded data.</returns>
    public static ExtractedData HandleDecodings(
        this int encoding,
        string binStr,
        int cursor,
        Dictionary<string, string> gs1DigitalLinkData,
        Dictionary<string, string> nonGs1KeyValuePairs,
        string key,
        int numChars) {
            /* Starting with a specified encoding (in range 0-4), binary string binStr,
             * binary string cursor position, key, number of characters to extract and dictionary
             * Gs1AIs to extend. This method determines how many bits to extract (depending on the
             * encoding), extracts those bits, advances the cursor and converts the extracted bits
             * into a string value in the appropriate encoding, which is then inserted into the
             * specified dictionary. The updated dictionary and updated binary string
             * cursor position are returned.
             * */

            switch (encoding) {
                case 0:
                    // digits only at 3.32 bits per character
                    var numBitsForValue = numChars.NumberOfValueBits();
                    var rbv = binStr.Substring(cursor, numBitsForValue);
                    cursor += numBitsForValue;
                    AppendToDictionary(key, rbv
                        .BinaryLiteralToBigInteger()
                        .ToString());
                    break;

                case 1:
                    // lower case hexadecimal characters
                    var ret1 = BuildString(numChars, Gs1DigitalLinkConvert.HexAlphabet, cursor, 4, binStr, key);
                    cursor = (int)ret1.Cursor;
                    AppendToDictionary(key, (ret1?.gs1DigitalLinkData?[key] ?? string.Empty).ToLower());
                    break;

                case 2:
                    // upper case hexadecimal characters
                    var ret2 = BuildString(numChars, Gs1DigitalLinkConvert.HexAlphabet, cursor, 4, binStr, key);
                    cursor = (int)ret2.Cursor;
                    AppendToDictionary(key, (ret2?.gs1DigitalLinkData?[key] ?? string.Empty).ToUpper());
                    break;

                case 3:
                    // URI safe base64 alphabet at 6 bits per character
                    var ret3 = BuildString(numChars, Gs1DigitalLinkConvert.SafeBase64Alphabet, cursor, 6, binStr, key);
                    cursor = (int)ret3.Cursor;
                    AppendToDictionary(key, ret3?.gs1DigitalLinkData?[key] ?? string.Empty);
                    break;

                case 4:
                    // ASCII at 7 bits per character
                    var ret4 = BuildString(numChars, string.Empty, cursor, 7, binStr, key);
                    cursor = ret4.Cursor;
                    gs1DigitalLinkData[key] += ret4.gs1DigitalLinkData?[key] ?? string.Empty;
                    break;
            }

            return new ExtractedData(gs1DigitalLinkData, nonGs1KeyValuePairs, Cursor: cursor);

            void AppendToDictionary(string key, string value) {
                if (gs1DigitalLinkData.ContainsKey(key)) {
                    gs1DigitalLinkData[key] += value;
                }
                else if (!nonGs1KeyValuePairs.TryAdd(key, value)) {
                    nonGs1KeyValuePairs[key] += value;
                }
            }

            ExtractedData BuildString(int numChars, string alphabet, int cursor, int multiplier, string binstr, string key) {
                var sb = new StringBuilder();
                var numBitsForValue = multiplier * numChars;
                var rbv = binstr.Substring(cursor, numBitsForValue);
                cursor += numBitsForValue;
                for (var i = 0; i < numChars; i++) {
                    var index = Convert.ToInt32(rbv.Substring(multiplier * i, multiplier), 2);
                    if (multiplier == 7) {
                        sb.Append((char)index);
                    }
                    else {
                        sb.Append(alphabet[index]);
                    }
                }

                var ai = new Dictionary<string, string> { [key] = sb.ToString() };
                return new ExtractedData(ai, null, Cursor: cursor);
            }
    }

    /// <summary>
    /// Verifies the syntax of an AI value against a regular expression representing the
    /// expected format of the value.
    /// </summary>
    /// <param name="ai">A GS1 Application Identifier.</param>
    /// <param name="value">The Application Identifier value.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid syntax.</exception>
    public static void VerifySyntax(this string ai, string value, string methodName, string paramName) {
        try {
            if ((ai != null) && _regexAllNum.IsMatch(ai)) {
                if (!Gs1DigitalLinkConvert.AiRegex[ai].IsMatch(value)) {
                    var message = string.Format(Resources.Errors.ErrorMsgInvalidSyntaxForValue0Of1, value, ai);
                    var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                    throw LogAndReturnException(Resources.Errors.ErrorTypeSyntaxError, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
                }
            }
        }
        catch (KeyNotFoundException knfEx) {
            var message = string.Format(Resources.Errors.ErrorMsg01IsInvalidNonGs1KeyValuesMustNotBeAllNumeric, ai, value);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidQueryStringKeyValuePair, apiCall, message, knfEx, logger: Gs1DigitalLinkConvert.Logger);
        }
    }

    /// <summary>
    /// Verifies the syntax and grammar of an AI value.
    /// </summary>
    /// <param name="ai">A GS1 Application Identifier.</param>
    /// <param name="value">The Application Identifier value.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid syntax.</exception>
    public static void VerifyGs1KeyPair(this string ai, string value, string methodName, string paramName) {
        var parsedData = Parsers.HighCapacityAidc.Parser.Parse($"{ai}{value}");

        if (parsedData.Exceptions.Any()) {
            var exceptionsMessage = new StringBuilder(string.Format(Resources.Errors.ErrorMsg01IsInvalid, ai, value));
            exceptionsMessage.Append("\r\n");
            exceptionsMessage.Append(Resources.Errors.ErrorMsgPartTheFollowingIssuesWereDetected);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);

            foreach (var exception in parsedData.Exceptions) {
                var fatalSpecifier = exception.IsFatal
                    ? Resources.Errors.ErrorMsgPartFatal
                    : string.Empty;
                exceptionsMessage.Append($"\r\n{exception.ErrorNumber}{fatalSpecifier}: {exception.Message}");
            }

            throw ConversionExtensionMethods.LogAndReturnException(
                Resources.Errors.ErrorTypeInvalidGs1ApplicationIdentifier,
                apiCall,
                exceptionsMessage.ToString(),
                logger: Gs1DigitalLinkConvert.Logger);
        }
    }

    /// <summary>
    /// Converts a bracketed element string to a FNC1 string using ASCII 29.
    /// </summary>
    /// <param name="input">A bracketed element string.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <param name="noValidation">
    /// If true, the element string is not validated. The GS1 AI dictionary may contain invalid AIs and AI values.
    /// </param>
    /// <returns>A FNC1 element string.</returns>
    /// <exception cref="Gs1DigitalLinkException">Invalid syntax.</exception>
    public static string ConvertParenthesesAIsToFnc1(
        this string input,
        string? methodName,
        string? paramName,
        bool noValidation) {
            // This regex finds sequences like (01), (3103), (3923), (10), etc.
            var aiPattern = RegexBracketedAiParser();
            var matches = aiPattern.Matches(input);

            if (matches.Count == 0) {
                // No AIs found.  Assume that the data is already in FNC1
                // format and return input as-is.
                return input;
            }

            var output = string.Empty;

            foreach (Match match in matches) {
                var ai = match.Groups[1].Value;  // The AI without parentheses
                var aiStart = match.Index;
                var aiLength = match.Length;

                // Extract the data that follows this AI up to the next AI or the end of the string
                var dataStart = aiStart + aiLength;
                int dataEnd; // We'll determine where the data ends

                // Find the next AI start (if any)
                Match nextAI = match.NextMatch();
                if (nextAI.Success) {
                    dataEnd = nextAI.Index;
                }
                else {
                    dataEnd = input.Length;
                }

                var rawDataSection = input[dataStart..dataEnd];

                // Determine if AI is fixed or variable length
                string dataForThisAI;

                var fnc1Elements = _aiTable.Where(e => !e.PredefinedLength).ToList();
                var isFnc1 = fnc1Elements.Any(e => e.Ai == ai);

                if (isFnc1) {
                    // AI is FNC1
                    dataForThisAI = rawDataSection;
                }
                else {
                    var length = Gs1DigitalLinkConvert.PredefinedLengthTable.ContainsKey(ai) ? Gs1DigitalLinkConvert.PredefinedLengthTable[ai] - ai.Length : rawDataSection.Length;

                    if (!noValidation && rawDataSection.Length < length) {
                        var message = string.Format(Resources.Errors.ErrorMsgInvalidSyntaxForValueOf01Expected2Characters, ai, rawDataSection, length);
                        var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                        throw LogAndReturnException(Resources.Errors.ErrorTypeSyntaxError, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
                    }

                    dataForThisAI = rawDataSection[..length];
                }

                output += ai + dataForThisAI + (isFnc1 ? "\x1D" : string.Empty);
            }

            // Strip off any trailing GS separator
            output = output.TrimEnd('\x1D');

            return output;
    }

    /// <summary>
    /// Normalises a URI stem to a consistent format and returns a default (canonical) stem
    /// if none is provided.
    /// </summary>
    /// <param name="uriStem">The URI stem.</param>
    /// <returns>An absolute URI stem, favouring https: if no scheme is provided, or the
    /// default (canonical) stem if no URI stem is provided.</returns>
    /// <exception cref="UriFormatException">Thrown when the URI stem is not a valid relative or absolute URI.</exception>"
    public static string GetNormalisedUriStem(this string? uriStem) {
        uriStem = uriStem?.Trim() ?? null;
        const string canonicalStem = "https://id.gs1.org";

        // Use the canonical stem if no uriStem is provided.
        // Be forgiving regarding the format of the URI stem, if provided.
        try {
            uriStem = string.IsNullOrWhiteSpace(uriStem)
                ? canonicalStem
                : new Uri(uriStem, UriKind.RelativeOrAbsolute) switch {
                    var uri when uri.IsAbsoluteUri => uri.AbsoluteUri,
                    _ => new Uri($"https://{uriStem}", UriKind.Absolute).AbsoluteUri,
                };
        }
        catch (UriFormatException) {
            throw;
        }

        // Need to remove unwanted trailing slash from uriStem if present
        if (uriStem.EndsWith('/')) {
            uriStem = uriStem[..^1].Trim();
        }

        return uriStem;
    }

    /// <summary>
    /// Returns the encoding indicator of a string value.
    /// </summary>
    /// <param name="charStr">The string value.</param>
    /// <returns>An encoding indicator.</returns>
    /// <remarks>See section 8.4.4.1 of the GS1 Digital Link standard.</remarks>
    public static int DetermineEncoding(this string charStr) {
        var enc = 4;
        if (_regexSafe64.IsMatch(charStr)) {
            enc = 3;
        }

        if (_regexHexLower.IsMatch(charStr)) {
            enc = 1;
        }

        if (_regexHexUpper.IsMatch(charStr)) {
            enc = 2;
        }

        if (_regexAllNum.IsMatch(charStr)) {
            enc = 0;
        }

        return enc;
    }

    /// <summary>
    /// Validates the URI stem.
    /// </summary>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid URI stem.</exception>
    public static void ValidateUriStem(this string? uriStem, string methodName, string paramName) {
        // Validate the stem
        try {
            if (string.IsNullOrWhiteSpace(uriStem)) {
                var message = string.Format(Resources.Errors.ErrorMsgTheDigitalLinkUriCannotBeNullOrEmpty);
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
            }

            var uri = new Uri(uriStem);
            var scheme = uri.Scheme.ToLower();

            if (scheme != "https" && scheme != "http") {
                var message = string.Format(Resources.Errors.ErrorMsgTheDigitalLinkScheme0IsNotRecognised, scheme);
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
            }
        }
        catch (UriFormatException ufEx) {
            var message = string.Format(Resources.Errors.ErrorMsgTheDigitalLinkUriStem0IsInvalid, uriStem);
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, ufEx, Gs1DigitalLinkConvert.Logger);
        }
    }

    /// <summary>
    /// Validates the sequence membership of a list of qualifiers.  If the qualifiers
    /// belong to more than one sequence, the method throws an exception.
    /// </summary>
    /// <param name="aiSeq">A list containing an identifier and qualifiers.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid sequence membership.</exception>
    public static void ValidateSequenceMembership(this List<string> aiSeq, string methodName, string paramName) {
        if (_pathSequenceConstraints.ContainsKey(aiSeq[0])) {
            var sequences = _pathSequenceConstraints[aiSeq[0]].ToList();
            var sequenceCounts = new Dictionary<int, int>();

            for (var idx = 0; idx < sequences.Count; idx++) {
                var sequence = sequences[idx];
                foreach (var qualifier in sequence) {
                    if (aiSeq.Contains(qualifier)) {
                        sequenceCounts[idx] = sequenceCounts.TryGetValue(idx, out int value) ? value + 1 : 1;
                    }
                }
            }

            if ((from sc in sequenceCounts
                 where sc.Value > 0
                 select sc.Value).ToList().Count > 1) {
                var message = Resources.Errors.ErrorMsgInvalidSequenceOfKeyQualifiersFoundInTheGs1DigitallinkUriPathInformation;
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
            }
        }
    }

    /// <summary>
    /// Validates the non-GS1 key-value pairs.
    /// </summary>
    /// <param name="nonGs1KeyValuePairs">The non-GS1 key-value pairs.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid non-GS1 key-value pairs.</exception>
    public static void ValidateNonGs1KeyValuePairs(this IReadOnlyDictionary<string, string> nonGs1KeyValuePairs, string methodName, string paramName) {
        // Validate the non-GS1 key value parameters.
        if (nonGs1KeyValuePairs != null && nonGs1KeyValuePairs.Count > 0) {
            foreach (var key in nonGs1KeyValuePairs.Keys) {
                var value = nonGs1KeyValuePairs[key];
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);

                // The key in a non-GS1 key-value param must contain at least one non-digit character
                if (_regexAllNum.IsMatch(key)) {
                    var message = string.Format(Resources.Errors.ErrorMsg01IsInvalidNonGs1KeyValuesMustNotBeAllNumeric, key, value);
                    throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidQueryStringKeyValuePair, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
                }

                // The key-value param is limited to unencoded certain characters and %-encoded hexadecimal values.
                if (!_regexQueryStringKey.IsMatch(key) || !_regexQueryString.IsMatch(value)) {
                    var message = string.Format(Resources.Errors.ErrorMsg01IsInvalidTheParameterContainsIncorrectlyUnencodedCharacters, key, value);
                    throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidQueryStringKeyValuePair, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
                }
            }
        }
    }

    /// <summary>
    /// Validates the order of the sequence of a list of qualifiers. If the qualifiers
    /// belong to more than one sequence, the method throws an exception.
    /// </summary>
    /// <param name="aiSeq">A list containing an identifier and qualifiers.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid sequence order.</exception>
    public static void ValidateSequenceOrder(this List<string> aiSeq, string methodName, string paramName) {
        // Check that the URI path components appear in the correct sequence
        if (_pathSequenceConstraints.ContainsKey(aiSeq[0])) {
            var invalidSequence = 0;
            var sequences = _pathSequenceConstraints[aiSeq[0]];

            foreach (var sequence in sequences) {
                var lastIndex = -1;

                foreach (var item in aiSeq.Skip(1)) {
                    var itemIndex = sequence.ToList().IndexOf(item);

                    if (itemIndex <= lastIndex) {
                        invalidSequence++;
                    }

                    lastIndex = itemIndex;
                }

                if (invalidSequence == 0) {
                    break;
                }
            }

            if (invalidSequence == sequences.Count) {
                var message = Resources.Errors.ErrorMsgInvalidSequenceOfKeyQualifiersFoundInTheGs1DigitallinkUriPathInformation;
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
            }
        }
    }

    /// <summary>
    /// Validates non key-pair query string content.
    /// </summary>
    /// <param name="otherQueryContent">Non key-pair query string content.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid non key-pair query string content.</exception>
    public static void ValidateOtherQueryStringContent(this string otherQueryContent, string methodName, string paramName) {
        // Validate any other query string content.
        if (!string.IsNullOrWhiteSpace(otherQueryContent)) {
            // The query string content is limited to unencoded certain characters and %-encoded hexadecimal values.
            if (!_regexQueryString.IsMatch(otherQueryContent)) {
                var message = string.Format(Resources.Errors.ErrorMsg0IsInvalidTheValueContainsIncorrectlyUnencodedCharacters, otherQueryContent);
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidQueryStringContent, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
            }
        }
    }

    /// <summary>
    /// Validates the fragment specifier.
    /// </summary>
    /// <param name="fragment">The fragment specifier.</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="paramName">The parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">Invalid fragment specifier.</exception>
    public static void ValidateFragmentSpecifier(this string fragment, string methodName, string paramName) {
        // Validate any fragment specifier.
        if (!string.IsNullOrWhiteSpace(fragment)) {
            // The fragment specifier is limited to unencoded certain characters and %-encoded hexadecimal values.
            if (!_regexFragments.IsMatch(fragment)) {
                var message = string.Format(Resources.Errors.ErrorMsg0IsInvalidTheFragmentContainsIncorrectlyUnencodedCharacters, fragment);
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
                throw LogAndReturnException(Resources.Errors.ErrorTypeInvalidFragmentSpecifier, apiCall, message, logger: Gs1DigitalLinkConvert.Logger);
            }
        }
    }

    /// <summary>
    /// Returns a Gs1DigitalLinkException and traces the exception for diagnostics.
    /// </summary>
    /// <param name="title">The error title.</param>
    /// <param name="location">The public API location of the error.</param>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">An exception that will be added as an inner exception.</param>
    /// <param name="logger">A logger.</param>
    /// <param name="lineNumber">The source code line number.</param>
    /// <param name="memberName">The member name where the exception was raised.</param>
    /// <param name="filePath">The source code file name.</param>
    /// <returns>A requested argument exception..</returns>
    public static Gs1DigitalLinkException LogAndReturnException(
    string title,
    string location,
    string message,
    Exception? innerException = null,
    ILogger? logger = null,
    [CallerLineNumber] int lineNumber = 0,
    [CallerMemberName] string memberName = "",
    [CallerFilePath] string filePath = "") {
        var directory = "<unknown>";
        var file = "<unknown>";

        try {
            FileInfo fileInfo = new (filePath);
            directory = fileInfo.Directory?.Name ?? "<unknown>";
            file = fileInfo.Name;
        }
        catch {
        }

        const string sigil = "SSRGS1DLL: -> ";

        // For trace insert the sigil at each subsequent new line in the message.
        var traceMessage = message.Replace(Environment.NewLine, Environment.NewLine + sigil);

        var logMessage = $"{sigil}{location}: {traceMessage} [line {lineNumber} in ../{directory}/{file} ({memberName})]";
        Trace.TraceError(logMessage);
        logger?.LogError("{LogMessage}", logMessage);

        return new Gs1DigitalLinkException($"{title}: {message}", location, innerException);
    }

    /// <summary>
    /// Regex that checks that a string is composed only of digits.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^[0-9]+$")]
    public static partial Regex RegexAllNumbers();

    /// <summary>
    /// Regex that checks that a string is composed of lower-case hexadecimal characters.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^[0-9a-f]+$")]
    public static partial Regex RegexHexLower();

    /// <summary>
    /// Regex that checks that a string is composed of upper-case hexadecimal characters.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^[0-9A-F]+$")]
    public static partial Regex RegexHexUpper();

    /// <summary>
    /// Regex that checks that a string is composed only using a 64-character
    /// set that is safe for compressed data.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("^[A-Za-z0-9_-]+$")]
    public static partial Regex RegexSafe64();

    /// <summary>
    /// Determines the four-digit year value for a year expressed as two digits.
    /// </summary>
    /// <param name="yy">The year expressed as two digits.</param>
    /// <returns>The year, expressed as a four-digit level.</returns>
    private static string DetermineFourDigitYear(string yy) {
        int yearTwoDigits = int.Parse(yy);
        var currentDateTime = DateTime.Now;
        var currentYear = currentDateTime.Year;
        var currentCentury = int.Parse(currentYear.ToString()[..2]);
        var difference = yearTwoDigits - (currentYear % 100);
        int intendedYear;
        if ((difference >= 51) && (difference <= 99)) {
            intendedYear = ((currentCentury - 1) * 100) + yearTwoDigits;
        }
        else {
            if ((difference >= -99) && (difference <= -50)) {
                intendedYear = ((currentCentury + 1) * 100) + yearTwoDigits;
            }
            else {
                intendedYear = (currentCentury * 100) + yearTwoDigits;
            }
        }

        return intendedYear.ToString();
    }

    /// <summary>
    /// Converts an arbitrarily-sized integer to a binary string.
    /// </summary>
    /// <param name="value">The arbitrarily-sized integer.</param>
    /// <returns>A binary string.</returns>
    private static string BigIntegerToBinaryString(this BigInteger value) {
        if (value == 0) {
            return "0";
        }

        var returnValue = new StringBuilder();
        BigInteger intValue = BigInteger.Abs(value);

        while (intValue > 0) {
            returnValue.Insert(0, (intValue % 2 == 1) ? '1' : '0');
            intValue /= 2;
        }

        // If the original value was negative and you need a representation,
        // you would have to define how you represent negatives. For now, we
        // assume only non-negative values or a simple '-' prefix.
        if (value.Sign < 0) {
            returnValue.Insert(0, '-');
        }

        return returnValue.ToString();
    }

    /// <summary>
    /// Builds a binary value from a string.
    /// </summary>
    /// <param name="charStr">The string to be converted.</param>
    /// <param name="bitsPerCharactern">The bits per character.</param>
    /// <param name="alphabet">The alphabet used for decoding.</param>
    /// <returns>A binary value.</returns>
    private static string BuildBinaryValue(string charStr, int bitsPerCharactern, string alphabet) {
        // This method converts a string charStr into binary, using bitsPerCharactern bits per character
        // and decoding from the supplied alphabet or from ASCII when bitsPerCharactern=7.
        var binValue = new StringBuilder();

        for (var idx = 0; idx < charStr.Length; idx++) {
            int index;

            if (bitsPerCharactern == 7) {
                index = charStr[idx]; // ASCII code
            }
            else {
                index = alphabet.IndexOf(charStr[idx]);
            }

            var binChar = Convert.ToString(index, 2);
            binChar = binChar.PadStringToLength(bitsPerCharactern);
            binValue.Append(binChar);
        }

        return binValue.ToString();
    }

    /// <summary>
    /// Converts an arbitrarily-sized integer to a binary string.
    /// </summary>
    /// <param name="number">The arbitrarily-sized integer.</param>
    /// <returns>A binary string.</returns>
    private static string ConvertToBinaryString(BigInteger number) {
        if (number == 0) {
            return "0";
        }

        // Retrieve the byte array of the BigInteger
        var bytes = number.ToByteArray();

        // Initialize a StringBuilder to collect the binary digits
        var binaryString = new StringBuilder();

        // Iterate over the bytes in reverse order to process the most significant byte first
        for (var idx = bytes.Length - 1; idx >= 0; idx--) {
            // Convert each byte to its binary representation
            var binaryByte = Convert.ToString(bytes[idx], 2).PadLeft(8, '0');
            binaryString.Append(binaryByte);
        }

        // Remove leading zeros
        string result = binaryString.ToString().TrimStart('0');

        // If the number is negative, handle two's complement representation
        if (number.Sign < 0) {
            // Calculate the bit length
            int bitLength = bytes.Length * 8;

            // Create a mask with the same bit length
            BigInteger mask = (BigInteger.One << bitLength) - 1;

            // Apply the mask to get the positive equivalent
            BigInteger positiveNumber = number & mask;

            // Convert the positive number to binary
            result = ConvertToBinaryString(positiveNumber);

            // Invert the bits for two's complement
            result = new string(result.Select(bit => bit == '0' ? '1' : '0').ToArray());

            // Add one to complete two's complement
            positiveNumber = BigInteger.Parse(result, System.Globalization.NumberStyles.AllowHexSpecifier) + 1;
            result = ConvertToBinaryString(positiveNumber);
        }

        return result;
    }

    /// <summary>
    /// Calculate the expected GS1 Check Digit for a given AI.
    /// </summary>
    /// <param name="ai">The GS1 AI.</param>
    /// <param name="gs1AiValue">The AI value.</param>
    /// <returns>The expected check digit.</returns>
    private static int CalculateCheckDigit(string ai, string gs1AiValue) {
        var counter = 0;
        var total = 0;
        int valueLength;

        if (Gs1DigitalLinkConvert.AiCheckDigitPositions.TryGetValue(ai, out CheckDigitPosition? value) && value == CheckDigitPosition.Last) {
            valueLength = gs1AiValue.Length;
        }
        else {
            valueLength = (int)(Gs1DigitalLinkConvert.AiCheckDigitPositions[ai] ?? 0);
        }

        int multiplier;

        for (var idx = valueLength - 2; idx >= 0; idx--) {
            var digitStr = gs1AiValue.Substring(idx, 1);
            var digit = int.Parse(digitStr);

            if ((counter % 2) == 0) {
                multiplier = 3;
            }
            else {
                multiplier = 1;
            }

            total += digit * multiplier;
            counter++;
        }

        var expectedCheckDigit = (10 - (total % 10)) % 10;
        return expectedCheckDigit;
    }

    /// <summary>
    /// Regex that parses a bracketed element string.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"\((\d{2,4}?)\)", RegexOptions.Compiled)]
    private static partial Regex RegexBracketedAiParser();

    /// <summary>
    /// Regex that checks that a date specifier of up to twelve digits is valid.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("\\d{2}(?:12|11|0\\d)(?:31|30|2\\d|1\\d|0[1-9])(?:0\\d|1\\d|2[0-4])(?:[0-5]\\d)?(?:[0-5]\\d)?")]
    private static partial Regex RegexMaxTwelveDigitDate();

    /// <summary>
    /// Regex that checks that a date specifier of up to ten digits is valid.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("\\d{2}(?:12|11|0\\d)(?:31|30|2\\d|1\\d|0[1-9])(?:0\\d|1\\d|2[0-4])(?:[0-5]\\d)")]
    private static partial Regex RegexTenDigitDate();

    /// <summary>
    /// Regex that checks that a date specifier of up to six digits is valid.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex("\\d{2}(?:12|11|0\\d)(?:31|30|2\\d|1\\d|0\\d)")]
    private static partial Regex RegexSixDigitDate();

    /// <summary>
    /// Regex that checks that a string is composed of characters allowed in
    /// non-GS1 key-value pair query string keys.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^([A-Za-z0-9!$ '()*+,;:/\-._~?@]|%[0-9A-Fa-f]{2})*$")]
    private static partial Regex RegexQueryStringKey();

    /// <summary>
    /// Regex that checks that a string is composed of characters allowed in a query string.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^([A-Za-z0-9!$&'()*+,;=:/\-._~?@]|%[0-9A-Fa-f]{2})*$")]
    private static partial Regex RegexQueryString();

    /// <summary>
    /// Regex that checks that a string is composed of characters allowed in fragments.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^([A-Za-z0-9-._~:/?#[\]@!$&'()*+,;=]|%[0-9A-Fa-f]{2})*$")]
    private static partial Regex RegexFragments();
}