// --------------------------------------------------------------------------
// <copyright file="DigitalLinkConvert.cs" company="Solidsoft Reply Ltd.">
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
// A toolkit for converting GS1 Digital Link data between different representations.
// </summary>
// --------------------------------------------------------------------------
// The code in this file is derived in part from the GS1 Digital Link
// Compression Prototype which is licenced under the Apache License,
// Version 2.0.
//
// Copyright 2019 GS1 AISBL
//
// See https://github.com/gs1/GS1DigitalLinkCompressionPrototype for
// further details.
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;
using Solidsoft.Reply.Parsers.HighCapacityAidc;
using Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

/// <summary>
/// A toolkit for converting GS1 Digital Link data between different representations.
/// </summary>
public static partial class DigitalLinkConvert {

    /// <summary>
    /// A dictionary of GS1 Application Identifier information.
    /// </summary>
    private static readonly AiTable _aiTable = AiTable.Create();

    /// <summary>
    /// A dictionary of prefix lengths.
    /// </summary>
    /// <remarks>
    /// Indicates for any initial two digits, what is the total length of the numeric
    /// AI key, e.g. "80":4 --> 80xx. All AI analyticsKeys beginning with 80 are four
    /// digit AI analyticsKeys.
    /// </remarks>
    private static readonly PrefixLengthTable _prefixLengthTable = PrefixLengthTable.Create();

    /// <summary>
    /// A dictionary of formats.  Indicates the expected format for the requiredAi of each AI.
    /// </summary>
    private static readonly FormatTable _formatTable = FormatTable.Create();

    /// <summary>
    /// A dictionary of optimisations.
    /// </summary>
    /// <remarks>
    /// Contains optimisations for pre-defined sequences of GS1 Application Identifiers.
    /// We'll initially use 0A-0F through 9A-9F to keep Ah - Eh unallocated and reserve
    /// Fh for support for non-GS1 analyticsKeys from the URI query string.
    /// </remarks>
    private static readonly OptimisationsTable _optimisationsTable = OptimisationsTable.Create();

    /// <summary>
    /// A dictionary of semantics.
    /// </summary>
    /// <remarks>
    /// Used for the semantic interpretation and expresses which simple analyticsKeys
    /// or compound analyticsKeys are instance identifiers (uniquely identifying only
    /// one thing globally).
    /// </remarks>
    private static readonly SemanticsTable _semanticsTable = SemanticsTable.Create();

    /// <summary>
    /// A dictionary of path sequence constraints.
    /// </summary>
    /// <remarks>Used to ensure that for those primary identification analyticsKeys in
    /// which multiple key qualifiers may appear in the URI path information, they SHALL
    /// appear in the expected order. Note that currently only GTIN (01) and ITIP (8006)
    /// have more than one permitted key qualifier.
    /// </remarks>
    private static readonly PathConstraints _pathSequenceConstraints = PathConstraints.Create();

    /// <summary>
    /// A dictionary of string semantics.
    /// </summary>
    private static readonly StringSemantics _stringSemantics = StringSemantics.Create();

    /// <summary>
    /// A dictionary of class semantics.
    /// </summary>
    private static readonly ClassSemantics _classSemantics = ClassSemantics.Create();

    /// <summary>
    /// A dictionary of date semantics.
    /// </summary>
    private static readonly DateSemantics _dateSemantics = DateSemantics.Create();

    /// <summary>
    /// A dictionary of date-time hour semantics.
    /// </summary>
    private static readonly DateTimeHoursSemantics _dateTimeHoursSemantics = DateTimeHoursSemantics.Create();

    /// <summary>
    /// A dictionary of date-time minutes semantics.
    /// </summary>
    private static readonly DateTimeMinutesSemantics _dateTimeMinutesSemantics = DateTimeMinutesSemantics.Create();

    /// <summary>
    /// A dictionary of date-time seconds semantics.
    /// </summary>
    private static readonly DateTimeSecondsSemantics _dateTimeSecondsSemantics = DateTimeSecondsSemantics.Create();

    /// <summary>
    /// A dictionary of date range semantics.
    /// </summary>
    private static readonly DateRangeSemantics _dateRangeSemantics = DateRangeSemantics.Create();

    /// <summary>
    /// A list of quantitative value semantics.
    /// </summary>
    private static readonly QuantitativeValueSemantics _quantitativeValueSemantics = QuantitativeValueSemantics.Create();

    /// <summary>
    /// A dictionary of AI validation regular expressions.
    /// </summary>
    private static readonly Dictionary<string, Regex> _aiRegex;

    /// <summary>
    /// A dictionary of AI short names. This is a legacy feature.
    /// </summary>
    private static readonly Dictionary<string, string> _aiShortNames;

    /// <summary>
    /// A dictionary of AI qualifiers.
    /// </summary>
    private static readonly Dictionary<string, string[]> _aiQualifiers;

    /// <summary>
    /// A dictionary of AI check digit positions.
    /// </summary>
    private static readonly Dictionary<string, CheckDigitPosition?> _aiCheckDigitPosition;

    /// <summary>
    /// A regular expression to match values that contain only digits.
    /// </summary>
    private static readonly Regex _regexAllNum;

    /// <summary>
    /// A regular expression to match a string that contains only upper-case
    /// characters in hexadecimal format.
    /// </summary>
    private static readonly Regex _regexHexLower;

    /// <summary>
    /// A regular expression to match a string that contains only lower-case
    /// characters in hexadecimal format.
    /// </summary>
    private static readonly Regex _regexHexUpper;

    /// <summary>
    /// A regular expression to match a string that contains only characters
    /// drawn from the 'safe 64' character set.
    /// </summary>
    private static readonly Regex _regexSafe64;

    /// <summary>
    /// A dictionary of AIs grouped by length.
    /// </summary>
    private static readonly Dictionary<int, List<string>> _aisByLength;

    /// <summary>
    /// A dictionary of AI maps.
    /// </summary>
    private static readonly Maps _aiMaps;

    /// <summary>
    /// A dictionary to obtain short names (legacy feature) from numeric
    /// GS1 Application Identifiers.
    /// </summary>
    private static readonly Dictionary<string, string> _shortNameToNumeric;

    /// <summary>
    /// The Group Separator character (ASCII 29).
    /// </summary>
    private static readonly string _groupSeparator = ((char)29).ToString();

    /// <summary>
    /// A dictionary to reverse-lookup AIs from serialised optimisation values.
    /// </summary>
    private static readonly Dictionary<string, string> _reverseOptimisationsTable;

    /// <summary>
    /// Initializes static members of the <see cref="DigitalLinkConvert"/> class.
    /// </summary>
    static DigitalLinkConvert() {

        // _reverseOptimisationsTable is computed in the JS code:
        _reverseOptimisationsTable = [];
        var tableOptKeys = _optimisationsTable.Keys.ToList();

        foreach (var key in tableOptKeys) {
            var sorted = _optimisationsTable[key].OrderBy(opt => opt).ToArray();
            var sortedJson = JsonSerializer.Serialize(sorted);
            _reverseOptimisationsTable[sortedJson] = key;
        }

        _aiRegex = [];
        _aiShortNames = [];
        _aiQualifiers = [];
        _aiCheckDigitPosition = [];

        _regexAllNum = ExtensionMethods.RegexAllNumbers();
        _regexHexLower = ExtensionMethods.RegexHexLower();
        _regexHexUpper = ExtensionMethods.RegexHexUpper();
        _regexSafe64 = ExtensionMethods.RegexSafe64();

        foreach (var a in _aiTable) {
            if (a != null) {
                _aiRegex[a.Ai] = new Regex("^" + a.Regex + "$");
                if (a.ShortName != null) {
                    _aiShortNames[a.Ai] = a.ShortName;
                }

                if (a.Qualifiers != null) {
                    _aiQualifiers[a.Ai] = [.. a.Qualifiers];
                }

                if (a.CheckDigitPosition != null) {
                    _aiCheckDigitPosition[a.Ai] = a.CheckDigitPosition;
                }
            }
        }

        bool GetIdentifiers(AiTableEntry ai) => ai.Type == AiTypes.Identifier;
        bool GetQualifiers(AiTableEntry ai) => ai.Type == AiTypes.Qualifier;
        bool GetDataAttributes(AiTableEntry ai) => ai.Type == AiTypes.DataAttribute;
        bool GetIsFnc1Element(AiTableEntry ai) => ai.PredefinedLength == false;
        bool GetIsNotFnc1Element(AiTableEntry ai) => ai.PredefinedLength == true;
        Func<AiTableEntry, bool> ByLength(int length) => element => element.Ai.Length == length;

        List<string> GetAIs(List<AiTableEntry> list) {
            var returnValue = new List<string>();
            foreach (var entry in list) {
                returnValue.Add(entry.Ai);
            }

            return returnValue;
        }

        // Define _aisByLength as a dictionary instead of an array
        _aisByLength = [];

        for (var idx = 2; idx <= 4; idx++) {
            // Use the GetAIs and ByLength functions
            _aisByLength[idx] = GetAIs(_aiTable.Where(ByLength(idx)).ToList());
        }

        var identifiers = _aiTable.Where(GetIdentifiers).ToList();
        var qualifiersEntries = _aiTable.Where(GetQualifiers).ToList();
        var dataAttributes = _aiTable.Where(GetDataAttributes).ToList();
        var fnc1ELements = _aiTable.Where(GetIsFnc1Element).ToList();
        var nonFnc1Elements = _aiTable.Where(GetIsNotFnc1Element).ToList();

        Dictionary<string, AiTableEntry> identifierMap = [];
        foreach (var i in identifiers) {
            identifierMap[i.Ai] = i;
        }

        Dictionary<string, AiTableEntry> qualifierMap = [];
        foreach (var q in qualifiersEntries) {
            qualifierMap[q.Ai] = q;
        }

        Dictionary<string, AiTableEntry> attributeMap = [];
        foreach (var da in dataAttributes) {
            attributeMap[da.Ai] = da;
        }

        Dictionary<string, AiTableEntry> fnc1ElementMap = [];
        foreach (var f in fnc1ELements) {
            fnc1ElementMap[f.Ai] = f;
        }

        Dictionary<string, AiTableEntry> nonFnc1ElementMap = [];
        foreach (var v in nonFnc1Elements) {
            nonFnc1ElementMap[v.Ai] = v;
        }

        _aiMaps = new Maps(
            [.. identifierMap.Keys],
            [.. qualifierMap.Keys],
            [.. attributeMap.Keys],
            [.. fnc1ElementMap.Keys],
            [.. nonFnc1ElementMap.Keys]);

        /* TODO - not yet making checks on invalid and mandatory associations of GS1 Application Identifiers */

        _shortNameToNumeric = [];
        foreach (var key in _aiShortNames.Keys) {
            _shortNameToNumeric[_aiShortNames[key]] = key;
        }
    }

    /// <summary>
    /// Gets the safe Base64 alphabet. This is a modified URI-safe Base64 alphabet used in the
    /// compression methods for converting the binary string to/from an alphanumeric representation that contains no
    /// characters that are restricted in URIs.
    /// </summary>
    internal static string SafeBase64Alphabet => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";

    /// <summary>
    /// Gets the hexadecimal alphabet.
    /// </summary>
    internal static string HexAlphabet => "0123456789ABCDEF";

    /// <summary>
    /// Gets a dictionary of AI validation regular expressions.
    /// </summary>
    internal static Dictionary<string, Regex> AiRegex => _aiRegex;

    /// <summary>
    /// Gets a dictionary of GS1 Application Identifier check digit positions.
    /// </summary>
    internal static Dictionary<string, CheckDigitPosition?> AiCheckDigitPositions => _aiCheckDigitPosition;

    /// <summary>
    /// Gets a dictionary of GS1 qualifier Application Identifiers.
    /// </summary>
    internal static Dictionary<string, string[]> AiQualifiers => _aiQualifiers;

    /// <summary>
    /// Gets categorisations of GS1 Application Identifiers.
    /// </summary>
    internal static Maps AiMaps => _aiMaps;

    /// <summary>
    /// Gets a dictionary of short names ('convenience alphas').
    /// </summary>
    [Obsolete("This property supports the use of short names ('convenience alphas') which are obsolete. This property is retained for legacy purposes, only.")]
    internal static Dictionary<string, string> AiShortNames => _aiShortNames;

    /// <summary>
    /// Gets a dictionary of GS1 Application Identifiers, referenced by short name ('convenience alphas').
    /// </summary>
    [Obsolete("This property supports the use of short names ('convenience alphas') which are obsolete. This property is retained for legacy purposes, only.")]
    internal static Dictionary<string, string> AiShortNamesToAIs => _shortNameToNumeric;

    /// <summary>
    /// Gets the table of GS1 Application Identifiers with predefined lengths.
    /// </summary>
    internal static PredefinedLengthTable PredefinedLengthTable => PredefinedLengthTable.Create();

    /// <summary>
    /// Returns an dictionary of Application Identifiers and their values for a given element string.
    /// </summary>
    /// <param name="elementString">The element string.</param>
    /// <param name="noValidation">
    /// If true, the element string is not validated. The GS1 AI dictionary may contain invalid AIs and AI values.
    /// </param>
    /// <returns>A dictionary of AIs and AI values.</returns>
    /// <exception cref="ArgumentException">The element string is invalid.</exception>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    public static Gs1Data FromGs1ElementStringToData(Gs1ElementString elementString, bool noValidation = false) =>
        FromGs1ElementStringToData(elementString.Value, noValidation);

    /// <summary>
    /// Returns an dictionary of Application Identifiers and their values for a given element string.
    /// </summary>
    /// <param name="elementString">The element string.</param>
    /// <param name="noValidation">
    /// If true, the element string is not validated. The GS1 AI dictionary may contain invalid AIs and AI values.
    /// </param>
    /// <returns>A dictionary of AIs and AI values.</returns>
    /// <exception cref="ArgumentException">The element string is invalid.</exception>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    public static Gs1Data FromGs1ElementStringToData(string elementString, bool noValidation = false) {

        // Remove AIM symbology identifier if present
        elementString = RegexAimIdentifierDetector().Match(elementString).Groups["input"].Value;
        var location = string.Format(Resources.Errors.ErrorMsgPart0Param1, nameof(FromGs1ElementStringToData), nameof(elementString));

        // Check if the initial AI is enclosed within parentheses
        var initialParenthesisedAiDetector = RegexInitialParenthesisedAiDetector();

        if (initialParenthesisedAiDetector.IsMatch(elementString)) {
            // Assume that the input is key bracketed element string and convert
            // it to FNC1 format
            elementString = elementString.ConvertParenthesesAIsToFnc1(nameof(FromGs1ElementStringToData), nameof(elementString), noValidation);
        }

        // Parse the element strings
        var parsedData = Parser.Parse(elementString, out _);

        if (!noValidation && !parsedData.IsRecognised) {
            var message = string.Format(Resources.Errors.ErrorMsgTheFormatOfTheElementString0IsNotRecognised, elementString);
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidElementString, location, message);
        }

        // Determine if the parsed data represents GS1 data
        if (!noValidation && parsedData.DataElements.FirstOrDefault()?.Format != FormatIndicator.Gs1Ai) {
            var message = string.Format(Resources.Errors.ErrorMsgTheElementString0DoesNotRepresentGs1Data, elementString);
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidElementString, location, message);
        }

        // Build the exceptions list
        if (!noValidation && parsedData.Exceptions.Any()) {
            var exceptionsMessage = new StringBuilder(Resources.Errors.ErrorMsgPartTheFollowingIssuesWereDetected);

            foreach (var exception in parsedData.Exceptions) {
                var fatalSpecifier = exception.IsFatal
                    ? Resources.Errors.ErrorMsgPartFatal
                    : string.Empty;
                exceptionsMessage.Append($"\r\n{exception.ErrorNumber}{fatalSpecifier}: {exception.Message}");
            }

            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidElementString, location, exceptionsMessage.ToString());
        }

        // Extract the AI data
        Dictionary<string, string> aiDictionary = [];
        foreach (var (ai, value) in from DataElement dataElement in parsedData.DataElements
                                    let ai = dataElement.Identifier
                                    let value = dataElement.Data
                                    select (ai, value)) {
            aiDictionary[ai] = value;
        }

        return new (aiDictionary);
    }

    /// <summary>
    /// Converts GS1 data into a GS1 Digital Link URI using short names.
    /// </summary>
    /// <param name="gs1Data">GS1 data.</param>
    /// <param name="uriStem">The URI stem for the Digital Link. If omitted, the library will use https://id.gs1.org.</param>
    /// <param name="digitalLinkForm">The level of compression to apply to the Digital Link URI.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A GS1 Digital Link.</returns>
    /// <exception cref="ArgumentException">Invalid data passed to method.</exception>
    [Obsolete("This method supports the use of short names ('convenience alphas') which are obsolete. This method is retained for legacy purposes, only.")]
    public static Gs1DigitalLink FromGs1DataToDigitalLinkWithShortNames(
        Gs1Data gs1Data,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        bool compressNonGs1KeyValuePairs = false) =>
           new (DoBuildGs1DigitalLink(
                    gs1Data.Gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    gs1Data.NonGs1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    gs1Data.OtherQueryStringContent,
                    gs1Data.FragmentSpecifier,
                    nameof(FromGs1DataToDigitalLinkWithShortNames),
                    nameof(gs1Data),
                    true));

    /// <summary>
    /// Converts a dictionary of GS1 Application Identifiers and their values into a GS1 Digital Link URI using short names.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 Application Identifiers.</param>
    /// <param name="uriStem">The URI stem for the Digital Link. If omitted, the library will use https://id.gs1.org.</param>
    /// <param name="digitalLinkForm">The level of compression to apply to the Digital Link URI.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGS1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional non-key=value content to be included in the query string.</param>
    /// <param name="fragment">Any additional fragment specifier to be included in the URI.</param>
    /// <returns>A GS1 Digital Link.</returns>
    /// <exception cref="ArgumentException">Invalid data passed to method.</exception>
    [Obsolete("This method supports the use of short names ('convenience alphas') which are obsolete. This method is retained for legacy purposes, only.")]
    public static Gs1DigitalLink FromGs1DataToDigitalLinkWithShortNames(
        Dictionary<string, string> gs1AIs,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        Dictionary<string, string>? nonGS1KeyValuePairs = null,
        bool compressNonGs1KeyValuePairs = false,
        string? otherQueryContent = null,
        string? fragment = null) =>
           new (DoBuildGs1DigitalLink(
                    gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    nonGS1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    otherQueryContent,
                    fragment,
                    nameof(FromGs1DataToDigitalLinkWithShortNames),
                    nameof(gs1AIs),
                    true));

    /// <summary>
    /// Converts GS1 data into a GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1Data">GS1 data.</param>
    /// <param name="uriStem">The URI stem for the Digital Link. If omitted, the library will use https://id.gs1.org.</param>
    /// <param name="digitalLinkForm">The level of compression to apply to the Digital Link URI.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A GS1 Digital Link.</returns>
    /// <exception cref="ArgumentException">Invalid data passed to method.</exception>
    public static Gs1DigitalLink FromGs1DataToDigitalLink(
        Gs1Data gs1Data,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        bool compressNonGs1KeyValuePairs = false) =>
            new (DoBuildGs1DigitalLink(
                    gs1Data.Gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    gs1Data.NonGs1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    gs1Data.OtherQueryStringContent,
                    gs1Data.FragmentSpecifier,
                    nameof(FromGs1DataToDigitalLink),
                    nameof(gs1Data)));

    /// <summary>
    /// Converts a dictionary of GS1 Application Identifiers and other values into a GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 Application Identifiers.</param>
    /// <param name="uriStem">The URI stem for the Digital Link. If omitted, the library will use https://id.gs1.org.</param>
    /// <param name="digitalLinkForm">The level of compression to apply to the Digital Link URI.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGS1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional non-key=value content to be included in the query string.</param>
    /// <param name="fragment">Any additional fragment specifier to be included in the URI.</param>
    /// <returns>A GS1 Digital Link.</returns>
    /// <exception cref="ArgumentException">Invalid data passed to method.</exception>
    public static Gs1DigitalLink FromGs1DataToDigitalLink(
        Dictionary<string, string> gs1AIs,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        Dictionary<string, string>? nonGS1KeyValuePairs = null,
        bool compressNonGs1KeyValuePairs = false,
        string? otherQueryContent = null,
        string? fragment = null) =>
            new (DoBuildGs1DigitalLink(
                    gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    nonGS1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    otherQueryContent,
                    fragment,
                    nameof(FromGs1DataToDigitalLink),
                    nameof(gs1AIs)));

    /// <summary>
    /// Extracts GS1 Application Identifiers and their values from key GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1DigitalLink">The Digital Link.</param>
    /// <returns>A dictionary of GS1 Application Identifiers (AIs).</returns>
    /// <exception cref="ArgumentException">Invalid GS1 Digital Link.</exception>
    public static Gs1Data FromGs1DigitalLinkToData(this Gs1DigitalLink gs1DigitalLink) =>
        new (gs1DigitalLink.Value.DoExtractAIsFromGs1DigitalLink(nameof(FromGs1DigitalLinkToData), nameof(gs1DigitalLink)));

    /// <summary>
    /// Extracts GS1 Application Identifiers and their values from key GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">The Digital Link URI.</param>
    /// <returns>A dictionary of GS1 Application Identifiers (AIs).</returns>
    /// <exception cref="ArgumentException">Invalid GS1 Digital Link.</exception>
    public static Gs1Data FromGs1DigitalLinkToData(this string gs1DigitalLinkUri) =>
        new (gs1DigitalLinkUri.DoExtractAIsFromGs1DigitalLink(nameof(FromGs1DigitalLinkToData), nameof(gs1DigitalLinkUri)));

    /// <summary>
    /// Build an element string from a dictionary of GS1 Application Identifiers and their values in GS1 data.
    /// </summary>
    /// <param name="gs1Data">GS1 data.</param>
    /// <param name="brackets">If true, the method returns an element string using bracket notation.</param>
    /// <returns>An element string.</returns>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    public static Gs1ElementString FromGs1DataToElementString(Gs1Data gs1Data, bool brackets = false) =>
        FromGs1DataToElementString(gs1Data.Gs1AIs, brackets);

    /// <summary>
    /// Build an element string from a dictionary of GS1 Application Identifiers and their values.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 Application Identifiers.</param>
    /// <param name="brackets">If true, the method returns an element string using bracket notation.</param>
    /// <returns>An element string.</returns>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    public static Gs1ElementString FromGs1DataToElementString(IReadOnlyDictionary<string, string> gs1AIs, bool brackets = false) {

        var methodName = nameof(FromGs1DataToElementString);
        var paramName = nameof(gs1AIs);

        // return an empty string if the input dictionary is empty.
        if (gs1AIs.Count == 0) {
            Trace.TraceWarning($"{methodName}: The dictionary of AIs ({nameof(gs1AIs)} is empty.");
            return new (string.Empty);
        }

        List<string> identifiers = [];
        List<string> qualifiers = [];
        List<string> attributes = [];
        List<string> fnc1Elements = [];
        List<string> nonFnc1Elements = [];
        List<string> otherKeys = [];
        List<string> elementStrings = [];

        gs1AIs.PopulateClassificationLists(
            identifiers,
            qualifiers,
            attributes,
            fnc1Elements,
            nonFnc1Elements,
            otherKeys,
            methodName,
            paramName);

        // if brackets=true, use GS1 Digital Link ordering - identifier, Qualifiers then data attributes in numeric order
        if (brackets == true) {
            identifiers[0].VerifySyntax(gs1AIs[identifiers[0]], methodName, paramName);
            identifiers[0].VerifyCheckDigit(gs1AIs[identifiers[0]], methodName, paramName);

            elementStrings = ElementStringsPush(elementStrings, "(" + identifiers[0] + ")", gs1AIs[identifiers[0]], string.Empty);

            // append any valid found Qualifiers for that primary identifier to the elementString
            if (_aiQualifiers.TryGetValue(identifiers[0], out string[]? qualifiersForPrimary)) {
                foreach (var q in qualifiersForPrimary) {
                    if (qualifiers.Contains(q)) {
                        elementStrings = ElementStringsPush(elementStrings, "(" + q + ")", gs1AIs[q], string.Empty);
                    }
                }
            }

            // sort attributes and append any found attributes to the elementString array
            attributes.Sort();
            foreach (var att in attributes) {
                elementStrings = ElementStringsPush(elementStrings, "(" + att + ")", gs1AIs[att], string.Empty);
            }
        }
        else {
            // if brackets=false, concatenate defined-length AIs first, then variable-length AIs
            // identify which AIs in gs1Data are defined fixed length
            List<string> nonfnc1PrimaryIdentifier = [];
            List<string> nonfnc1ValuesOther = new (nonFnc1Elements);

            for (var idx = 0; idx < nonfnc1ValuesOther.Count; idx++) {
                if (identifiers.Contains(nonfnc1ValuesOther[idx])) {
                    nonfnc1PrimaryIdentifier.Add(nonfnc1ValuesOther[idx]);
                    nonfnc1ValuesOther.RemoveAt(idx);
                    idx--;
                }
            }

            foreach (var fpi in nonfnc1PrimaryIdentifier) {
                elementStrings = ElementStringsPush(elementStrings, fpi, gs1AIs[fpi], string.Empty);
            }

            foreach (var flv in nonfnc1ValuesOther) {
                elementStrings = ElementStringsPush(elementStrings, flv, gs1AIs[flv], string.Empty);
            }

            for (var fnc1Idx = 0; fnc1Idx < fnc1Elements.Count; fnc1Idx++) {
                string gs = string.Empty;
                if (fnc1Idx < (fnc1Elements.Count - 1)) {
                    gs = _groupSeparator.ToString();
                }

                elementStrings = ElementStringsPush(
                                    elementStrings,
                                    fnc1Elements[fnc1Idx],
                                    gs1AIs[fnc1Elements[fnc1Idx]],
                                    gs);
            }
        }

        return new (string.Join(string.Empty, elementStrings));

        static List<string> ElementStringsPush(List<string> elementStrings, string ai, string value, string gs) {
            string newvalue = value;

            // always pad the requiredAi of any GTIN [ AI (01) or (02) ] to 14 digits in element string representation
            if ((ai == "01") || (ai == "(01)") || (ai == "02") || (ai == "(02)")) {
                if (value.Length == 8) {
                        newvalue = "000000" + value;
                    }

                if (value.Length == 12) {
                        newvalue = "00" + value;
                }

                if (value.Length == 13) {
                    newvalue = "0" + value;
                }
            }

            elementStrings.Add(ai + newvalue + gs);
            return elementStrings;
        }
    }

    /// <summary>
    /// Translates a GS1 element strings into key GS1 Digital Link URI.
    /// </summary>
    /// <param name="elementString">The element string.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="digitalLinkForm">The form of the GS1 digital Link (compressed, partially compressed or uncompressed).</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGS1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional query string content.</param>
    /// <param name="fragment">An additional fragment.</param>
    /// <returns>A GS1 Digitial Link.</returns>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    [Obsolete("This method supports the use of short names ('convenience alphas') which are obsolete. This method is retained for legacy purposes, only.")]
    public static Gs1DigitalLink FromGs1ElementStringToDigitalLinkWithShortNames(
        Gs1ElementString elementString,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        Dictionary<string, string>? nonGS1KeyValuePairs = null,
        bool compressNonGs1KeyValuePairs = false,
        string? otherQueryContent = null,
        string? fragment = null) =>
            new (DoBuildGs1DigitalLink(
                    FromGs1ElementStringToData(elementString).Gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    nonGS1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    otherQueryContent,
                    fragment,
                    nameof(FromGs1ElementStringToDigitalLinkWithShortNames),
                    nameof(elementString),
                    true));

    /// <summary>
    /// Translates a GS1 element strings into key GS1 Digital Link URI.
    /// </summary>
    /// <param name="elementString">The element string.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="digitalLinkForm">The form of the GS1 digital Link (compressed, partially compressed or uncompressed).</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGS1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional query string content.</param>
    /// <param name="fragment">An additional fragment.</param>
    /// <returns>A GS1 Digital Link..</returns>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    [Obsolete("This method supports the use of short names ('convenience alphas') which are obsolete. This method is retained for legacy purposes, only.")]
    public static Gs1DigitalLink FromGs1ElementStringToDigitalLinkWithShortNames(
        string elementString,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        Dictionary<string, string>? nonGS1KeyValuePairs = null,
        bool compressNonGs1KeyValuePairs = false,
        string? otherQueryContent = null,
        string? fragment = null) =>
            new (DoBuildGs1DigitalLink(
                    FromGs1ElementStringToData(elementString).Gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    nonGS1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    otherQueryContent,
                    fragment,
                    nameof(FromGs1ElementStringToDigitalLinkWithShortNames),
                    nameof(elementString),
                    true));

    /// <summary>
    /// Translates a GS1 element strings into key GS1 Digital Link URI.
    /// </summary>
    /// <param name="elementString">The element string.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="digitalLinkForm">The form of the GS1 digital Link (compressed, partially compressed or uncompressed).</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGS1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional query string content.</param>
    /// <param name="fragment">An additional fragment.</param>
    /// <returns>A GS1 Digital Link.</returns>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    public static Gs1DigitalLink FromGs1ElementStringToDigitalLink(
        Gs1ElementString elementString,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        Dictionary<string, string>? nonGS1KeyValuePairs = null,
        bool compressNonGs1KeyValuePairs = false,
        string? otherQueryContent = null,
        string? fragment = null) =>
            FromGs1ElementStringToDigitalLink(
                elementString.Value,
                uriStem,
                digitalLinkForm,
                useOptimisations,
                nonGS1KeyValuePairs,
                compressNonGs1KeyValuePairs,
                otherQueryContent,
                fragment);

    /// <summary>
    /// Translates a GS1 element strings into key GS1 Digital Link URI.
    /// </summary>
    /// <param name="elementString">The element string.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="digitalLinkForm">The form of the GS1 digital Link (compressed, partially compressed or uncompressed).</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGS1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional query string content.</param>
    /// <param name="fragment">An additional fragment.</param>
    /// <returns>A GS! Digital Link.</returns>
    /// <remarks>The element string may be in one of two forms.
    /// <para>
    /// <list type="number">
    ///     <item>
    ///         <term>Bracketed</term>
    ///         <description>An element string that delimits AIs using parentheses; e.g. "(01)05412345000013(3103)000189(3923)2172(10)ABC123".</description>
    ///     </item>
    ///     <item>
    ///         <term>Unbracketed</term>
    ///         <description>An element string, as it would be reported by key bar-code scanner; e.g. "3103000189010541234500001339232172&#x241D;10ABC123".</description>
    ///     </item>
    /// </list>
    /// <para>Unbracketed input may represent data directly read by key bar-code scanner, in which case this code assumes the following:</para>
    /// <list type="bullet">
    ///     <item>
    ///         <term>Precision</term>
    ///         <description>The data is precisely what is represented in the bar-code. No attempt is made to correct for scanner or
    ///                      computer misconfiguration or incompatibilities.</description>
    ///     </item>
    ///     <item>
    ///         <term>AIM Identifiers</term>
    ///         <description>If the bar-code data is prefixed, the prefix is one of key set of expected AIM identifiers (those that represent
    ///                      bar-codes that may be correctly used to represent GS1 data in accordance with GS1 standards - NB., there are
    ///                      other non-standard ways in which GS1 data could be represented in key bar-code.</description>
    ///     </item>
    ///     <item>
    ///         <term>Prefixes and Suffixes</term>
    ///         <description>No other prefix or suffix data is generated by the bar-code scanner.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>If any of these assumptions does not hold, you may need to perform additional processing before invoking this method.</para>
    /// </remarks>
    public static Gs1DigitalLink FromGs1ElementStringToDigitalLink(
        string elementString,
        string? uriStem = null,
        DigitalLinkForm digitalLinkForm = DigitalLinkForm.Uncompressed,
        bool useOptimisations = false,
        Dictionary<string, string>? nonGS1KeyValuePairs = null,
        bool compressNonGs1KeyValuePairs = false,
        string? otherQueryContent = null,
        string? fragment = null) =>
            new (DoBuildGs1DigitalLink(
                    FromGs1ElementStringToData(elementString).Gs1AIs,
                    uriStem,
                    digitalLinkForm,
                    useOptimisations,
                    nonGS1KeyValuePairs,
                    compressNonGs1KeyValuePairs,
                    otherQueryContent,
                    fragment,
                    nameof(FromGs1ElementStringToDigitalLink),
                    nameof(elementString)));

    /// <summary>
    /// Converts a GS1 Digital Link to an element string.
    /// </summary>
    /// <param name="digitalLink">The GS1 Digital Link.</param>
    /// <param name="brackets">If true, the method returns an element string using bracket notation.</param>
    /// <returns>A GS1 Digital Link.</returns>
    public static Gs1ElementString FromGs1DigitalLinkToElementString(Gs1DigitalLink digitalLink, bool brackets = false) {
        var extractedData = DoExtractAIsFromGs1DigitalLink(
                                digitalLink.Value,
                                nameof(FromGs1DigitalLinkToElementString),
                                nameof(digitalLink));
        var gs1AIs = extractedData.Gs1AIs;
        return FromGs1DataToElementString(gs1AIs ?? [], brackets);
    }

    /// <summary>
    /// Converts a GS1 Digital Link to an element string.
    /// </summary>
    /// <param name="digitalLinkUri">The GS1 Digital Link URI.</param>
    /// <param name="brackets">If true, the method returns an element string using bracket notation.</param>
    /// <returns>A GS1 Digital Link.</returns>
    public static Gs1ElementString FromGs1DigitalLinkToElementString(string digitalLinkUri, bool brackets = false) {
        var extractedData = DoExtractAIsFromGs1DigitalLink(
                                digitalLinkUri,
                                nameof(FromGs1DigitalLinkToElementString),
                                nameof(digitalLinkUri));
        var gs1AIs = extractedData.Gs1AIs;
        return FromGs1DataToElementString(gs1AIs ?? [], brackets);
    }

    /// <summary>
    /// Changes the compression level of a GS1 Digital Link, using short names for partially
    /// compressed or uncompressed URIs.
    /// </summary>
    /// <param name="digitalLink">The GS1 Digital Link.</param>
    /// <param name="compressionLevel">The required compression level.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A GS1 Digital Link URI.</returns>
    [Obsolete("This method supports the use of short names ('convenience alphas') which are obsolete. This method is retained for legacy purposes, only.")]
    public static Gs1DigitalLink Gs1DigitalLinkCompressionLevelWithShortNames(
    Gs1DigitalLink digitalLink,
    CompressionLevel compressionLevel,
    bool useOptimisations = false,
    bool compressNonGs1KeyValuePairs = false) =>
        new (digitalLink.Value.DoChangeGs1DigitalLinkCompression(
                compressionLevel,
                nameof(DoChangeGs1DigitalLinkCompression),
                nameof(digitalLink),
                useOptimisations,
                compressNonGs1KeyValuePairs,
                true));

    /// <summary>
    /// Changes the compression level of a GS1 Digital Link URI, using short names for partially
    /// compressed or uncompressed URIs.
    /// </summary>
    /// <param name="digitalLinkUri">The GS1 Digital Link URI.</param>
    /// <param name="compressionLevel">The required compression level.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A GS1 Digital Link URI.</returns>
    [Obsolete("This method supports the use of short names ('convenience alphas') which are obsolete. This method is retained for legacy purposes, only.")]
    public static Gs1DigitalLink Gs1DigitalLinkCompressionLevelWithShortNames(
    string digitalLinkUri,
    CompressionLevel compressionLevel,
    bool useOptimisations = false,
    bool compressNonGs1KeyValuePairs = false) =>
        new (digitalLinkUri.DoChangeGs1DigitalLinkCompression(
                compressionLevel,
                nameof(DoChangeGs1DigitalLinkCompression),
                nameof(digitalLinkUri),
                useOptimisations,
                compressNonGs1KeyValuePairs,
                true));

    /// <summary>
    /// Changes the compression level of a GS1 Digital Link.
    /// </summary>
    /// <param name="digitalLink">The GS1 Digital Link.</param>
    /// <param name="compressionLevel">The required compression level.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A GS1 Digital Link URI.</returns>
    public static Gs1DigitalLink Gs1DigitalLinkCompressionLevel(
        Gs1DigitalLink digitalLink,
        CompressionLevel compressionLevel,
        bool useOptimisations = false,
        bool compressNonGs1KeyValuePairs = false) =>
            new (digitalLink.Value.DoChangeGs1DigitalLinkCompression(
                    compressionLevel,
                    nameof(Gs1DigitalLinkCompressionLevel),
                    nameof(digitalLink),
                    useOptimisations,
                    compressNonGs1KeyValuePairs));

    /// <summary>
    /// Changes the compression level of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="digitalLinkUri">The GS1 Digital Link URI.</param>
    /// <param name="compressionLevel">The required compression level.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A GS1 Digital Link URI.</returns>
    public static Gs1DigitalLink Gs1DigitalLinkCompressionLevel(
        string digitalLinkUri,
        CompressionLevel compressionLevel,
        bool useOptimisations = false,
        bool compressNonGs1KeyValuePairs = false) =>
            new (digitalLinkUri.DoChangeGs1DigitalLinkCompression(
                    compressionLevel,
                    nameof(Gs1DigitalLinkCompressionLevel),
                    nameof(digitalLinkUri),
                    useOptimisations,
                    compressNonGs1KeyValuePairs));

    /// <summary>
    /// Analyse key GS1 Digital Link URI and return key structured output.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">The Digital Link URI.</param>
    /// <param name="extended">If true, the analyser returns additional structured data and an element string.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>The analysis results.</returns>
    internal static UriAnalytics AnalyseUri(
        Uri gs1DigitalLinkUri,
        bool extended,
        string methodName,
        string paramName) =>
            AnalyseUri(gs1DigitalLinkUri.ToString(), extended, methodName, paramName);

    /// <summary>
    /// Analyse key GS1 Digital Link URI and return key structured output.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">The Digital Link URI.</param>
    /// <param name="extended">If true, the analyser returns additional structured data and an element string.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>The analysis results.</returns>
    internal static UriAnalytics AnalyseUri(
        string gs1DigitalLinkUri,
        bool extended,
        string methodName,
        string paramName) {
        var analysis = new Dictionary<string, object> {
            ["fragment"] = string.Empty
        };

        var fragmentIndex = gs1DigitalLinkUri.IndexOf('#');
        var beforeFragment = gs1DigitalLinkUri;

        if (fragmentIndex > -1) {
            analysis["fragment"] = gs1DigitalLinkUri[(fragmentIndex + 1) ..];
            beforeFragment = gs1DigitalLinkUri[..fragmentIndex];
        }

        analysis["queryString"] = string.Empty;
        var beforeQueryString = beforeFragment;
        var queryStringIndex = beforeFragment.IndexOf('?');

        if (queryStringIndex > -1) {
            analysis["queryString"] = beforeFragment[(queryStringIndex + 1) ..];
            beforeQueryString = beforeFragment[..queryStringIndex];
        }

        // disregard any trailing forward slash
        var lengthBeforeQueryString = beforeQueryString.Length;

        if (lengthBeforeQueryString > 0 && beforeQueryString.Substring(lengthBeforeQueryString - 1, 1) == "/") {
            beforeQueryString = beforeQueryString[.. (lengthBeforeQueryString - 1)];
        }

        var cursor = 0;
        if (beforeQueryString.StartsWith("http://")) {
            cursor = 7;
        }

        if (beforeQueryString.StartsWith("https://")) {
            cursor = 8;
        }

        var protocol = beforeQueryString[..cursor];
        var afterProtocol = beforeQueryString[cursor..];

        var firstSlashOfAllPath = afterProtocol.IndexOf('/');

        string pathInfo;
        string domain;

        if (firstSlashOfAllPath > -1 && firstSlashOfAllPath < afterProtocol.Length) {
            pathInfo = afterProtocol[(firstSlashOfAllPath + 1) ..];
            domain = afterProtocol[..firstSlashOfAllPath];
        }
        else {
            // If no slash found, handle accordingly
            domain = afterProtocol;
            pathInfo = string.Empty;
        }

        analysis["uriPathInfo"] = "/" + pathInfo;
        analysis["uriStem"] = protocol + domain;

        var pathComponents = pathInfo.Split(['/'], StringSplitOptions.RemoveEmptyEntries);

        // iterate through pathComponents to find the path component corresponding to a primary GS1 ID key
        var reversedPathComponents = pathComponents.Reverse().ToList();
        var searching = true;
        var numericPrimaryIdentifier = string.Empty;
        var relevantPathComponents = new List<string>();
        var uriStemPathComponents = new List<string>();

        for (var idx = 0; idx < reversedPathComponents.Count; idx++) {
            var numkey = string.Empty;
            var pathComponent = reversedPathComponents[idx];

            if (_regexAllNum.IsMatch(pathComponent)) {
                numkey = pathComponent;
            }
            else {
                if (_shortNameToNumeric.TryGetValue(pathComponent, out string? value)) {
                    numkey = value;
                }
            }

            if ((numkey != string.Empty)
                && searching
                && _aiMaps.Identifiers.Contains(numkey)) {
                    searching = false;
                    numericPrimaryIdentifier = numkey;
                    relevantPathComponents = reversedPathComponents.Take(idx + 1).Reverse().ToList();
                    uriStemPathComponents = reversedPathComponents.Skip(idx + 1).Reverse().ToList();
            }
        }

        if (relevantPathComponents.Count > 0) {
            analysis["pathComponents"] = "/" + string.Join("/", relevantPathComponents);
        }
        else {
            analysis["pathComponents"] = string.Empty;
        }

        if (uriStemPathComponents.Count > 0) {
            analysis["uriStem"] = protocol + domain + "/" + string.Join("/", uriStemPathComponents);
        }
        else {
            analysis["uriStem"] = protocol + domain;
        }

        // If semicolon was used as delimiter between key=requiredAi parameters, replace with ampersand as delimiter
        var queryString = (string)analysis["queryString"];
        queryString = queryString.Replace(";", "&");
        analysis["queryString"] = queryString;

        // process URI path information
        Dictionary<string, string> pathCandidates = [];
        var pathElements = relevantPathComponents;
        var numberOfPathElements = pathElements.Count;
        var pathElementIndex = numberOfPathElements - 2;

        while (pathElementIndex >= 0) {
            pathCandidates[pathElements[pathElementIndex]] = pathElements[pathElementIndex + 1].PercentDecode();
            pathElementIndex -= 2;
        }

        Dictionary<string, string> gs1QueryStringPairs = [];
        Dictionary<string, string> nonGs1QueryStringPairs = [];
        var queryStringOther = new StringBuilder();

        if (!string.IsNullOrEmpty(queryString)) {
            var parameters = queryString.Split('&');

            foreach (var param in parameters) {
                var splitPair = param.Split('=');

                if (splitPair.Length == 2 &&
                    splitPair[0] != null &&
                    splitPair[1] != null) {
                    if (_shortNameToNumeric.TryGetValue(splitPair[0], out string? value)) {
                        // a short name is provided in the query string
                        gs1QueryStringPairs[value] = splitPair[1].PercentDecode();
                    }
                    else if (!_regexAllNum.IsMatch(splitPair[0])) {
                        // The key is not all numeric
                        nonGs1QueryStringPairs[splitPair[0]] = splitPair[1].PercentDecode();
                    }
                    else {
                        // Assume the key is an AI
                        gs1QueryStringPairs[splitPair[0]] = splitPair[1].PercentDecode();
                    }
                }
                else if (splitPair.Length == 1) {
                    // The query string parameter is not in key=value format.
                    queryStringOther.Append($"&{splitPair[0].PercentDecode()}");
                }
            }
        }

        if (queryStringOther.Length > 0) {
            queryStringOther = queryStringOther.Remove(0, 1);
        }

        analysis["pathCandidates"] = pathCandidates;
        analysis["queryStringGs1Pairs"] = gs1QueryStringPairs;
        analysis["queryStringNonGs1Pairs"] = nonGs1QueryStringPairs;
        analysis["queryStringOther"] = queryStringOther.ToString();
        analysis["detected"] = DigitalLinkForm.Unknown;
        analysis["uncompressedPath"] = string.Empty;
        analysis["compressedPath"] = string.Empty;
        analysis["structuredOutput"] = new Dictionary<string, ReadOnlyCollection<ReadOnlyDictionary<string, string>>>();

        // Check conditions
        if ((relevantPathComponents.Count > 0) && ((relevantPathComponents.Count % 2) == 0)) {
            if (_aiRegex[numericPrimaryIdentifier].IsMatch(Uri.UnescapeDataString(relevantPathComponents[1]))) {
                analysis["detected"] = DigitalLinkForm.Uncompressed;
                analysis["uncompressedPath"] = "/" + string.Join("/", relevantPathComponents);

                if (extended) {
                    var extractedData = gs1DigitalLinkUri.DoExtractAIsFromGs1DigitalLink(methodName, paramName);
                    var gs1AIs = extractedData.Gs1AIs;
                    var otherArray = extractedData.NonGs1KeyValuePairs;
                    var structuredArray = BuildStructuredArray(gs1AIs ?? [], otherArray ?? [], methodName, paramName);
                    analysis["structuredOutput"] = structuredArray;
                    analysis["elementStringOutput"] = FromGs1DigitalLinkToElementString(gs1DigitalLinkUri, true).Value;
                }
            }
        }

        if ((relevantPathComponents.Count == 3) && _regexSafe64.IsMatch(relevantPathComponents[2])) {
            if (_aiRegex[numericPrimaryIdentifier].IsMatch(Uri.UnescapeDataString(relevantPathComponents[1]))) {
                analysis["detected"] = DigitalLinkForm.PartiallyCompressed;
                analysis["uncompressedPath"] = "/" + string.Join("/", relevantPathComponents.Take(2));
                analysis["compressedPath"] = relevantPathComponents[2];

                if (extended) {
                    var extractedData = ExtractFromCompressedGs1DigitalLink(gs1DigitalLinkUri, null, methodName, paramName);
                    var gs1AIs = extractedData.Gs1AIs;
                    var otherArray = extractedData.NonGs1KeyValuePairs;
                    var structuredArray = BuildStructuredArray(gs1AIs ?? [], otherArray ?? [], methodName, paramName);
                    analysis["structuredOutput"] = structuredArray;
                    analysis["elementStringOutput"] = FromGs1DigitalLinkToElementString(gs1DigitalLinkUri, true).Value;
                }
            }
        }

        if ((DigitalLinkForm)analysis["detected"] == DigitalLinkForm.Unknown && reversedPathComponents.Count > 0 && _regexSafe64.IsMatch(reversedPathComponents[0]) && (protocol != string.Empty)) {
            analysis["detected"] = DigitalLinkForm.Compressed;
            analysis["uncompressedPath"] = string.Empty;
            analysis["compressedPath"] = reversedPathComponents[0];

            if (extended) {
                var extractedData = ExtractFromCompressedGs1DigitalLink(gs1DigitalLinkUri, null, methodName, paramName);
                var gs1AIs = extractedData.Gs1AIs;
                var otherArray = extractedData.NonGs1KeyValuePairs;
                var structuredArray = BuildStructuredArray(gs1AIs ?? [], otherArray ?? [], methodName, paramName);
                analysis["structuredOutput"] = structuredArray;
                analysis["elementStringOutput"] = FromGs1DigitalLinkToElementString(gs1DigitalLinkUri, true).Value;
            }
        }

        return new UriAnalytics {
            Fragment = analysis["fragment"] as string ?? string.Empty,
            QueryString = analysis["queryString"] as string ?? string.Empty,
            UriPathInfo = analysis["uriPathInfo"] as string ?? string.Empty,
            UriStem = analysis["uriStem"] as string ?? string.Empty,
            PathComponents = analysis["pathComponents"] as string ?? string.Empty,
            PathCandidates = analysis["pathCandidates"] as Dictionary<string, string> ?? [],
            QueryStringGs1Pairs = analysis["queryStringGs1Pairs"] as Dictionary<string, string> ?? [],
            QueryStringNonGs1Pairs = analysis["queryStringNonGs1Pairs"] as Dictionary<string, string> ?? [],
            OtherQueryContent = analysis["queryStringOther"] as string ?? string.Empty,
            DetectedForm = (DigitalLinkForm)analysis["detected"],
            UncompressedPath = analysis["uncompressedPath"] as string ?? string.Empty,
            CompressedPath = analysis["compressedPath"] as string ?? string.Empty,
            StructuredOutput = analysis.TryGetValue("structuredOutput", out var structuredOutput)
                ? ConvertArrayToStructuredOutput((Dictionary<string, ReadOnlyCollection<ReadOnlyDictionary<string, string>>>)structuredOutput) ?? new StructuredOutput()
                : new StructuredOutput(),
            ElementStringOutput = analysis.TryGetValue("elementStringOutput", out var elementStringOutput) ? elementStringOutput as string ?? string.Empty : string.Empty,
        };

        static StructuredOutput ConvertArrayToStructuredOutput(Dictionary<string, ReadOnlyCollection<ReadOnlyDictionary<string, string>>> structuredArray) =>
            new () {
                Identifiers = structuredArray.TryGetValue("identifiers", out var identifiers) ? identifiers : [],
                Qualifiers = structuredArray.TryGetValue("qualifiers", out var qualifiers) ? qualifiers : [],
                DataAttributes = structuredArray.TryGetValue("dataAttributes", out var dataAttributes) ? dataAttributes : [],
                Other = structuredArray.TryGetValue("other", out var other) ? other : [],
            };
    }

    /// <summary>
    /// Analyse the semantics of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">The GS1 Digital Link URI.</param>
    /// <returns>A dictionary of analytical results.</returns>
    internal static UriSemantics AnalyseUriSemantics(string gs1DigitalLinkUri) {
        // Analyze the URI and get the structured output
        var returnValue = AnalyseUri(
                            gs1DigitalLinkUri,
                            true,
                            nameof(AnalyseUriSemantics),
                            nameof(gs1DigitalLinkUri));
        var uncompressedDL = gs1DigitalLinkUri;
        var methodName = nameof(AnalyseUriSemantics);
        var paramName = nameof(gs1DigitalLinkUri);

        if (returnValue.DetectedForm != DigitalLinkForm.Unknown && returnValue.UriStem != string.Empty) {
            if (returnValue.DetectedForm == DigitalLinkForm.Compressed || returnValue.DetectedForm == DigitalLinkForm.PartiallyCompressed) {
                uncompressedDL = DoDecompressGs1DigitalLink(
                                    gs1DigitalLinkUri,
                                    returnValue,
                                    returnValue.UriStem,
                                    CompressionLevel.Uncompressed,
                                    false,
                                    false,
                                    nameof(AnalyseUriSemantics),
                                    nameof(gs1DigitalLinkUri));
            }
        }

        var excludeQueryString = uncompressedDL;
        var qpos = uncompressedDL.IndexOf('?');

        if (qpos > -1) {
            excludeQueryString = uncompressedDL[..qpos];
        }

        var returnValue2 = AnalyseUri(
                            excludeQueryString,
                            false,
                            nameof(AnalyseUriSemantics),
                            nameof(gs1DigitalLinkUri));

        var nonID = new Dictionary<string, string>();
        var elementStrings = new Dictionary<string, string>();

        // Process qualifiers
        foreach (var qualifier in returnValue.StructuredOutput.Qualifiers) {
            foreach (var key in qualifier.Keys) {
                nonID[key] = qualifier[key];
                elementStrings[key] = qualifier[key];
            }
        }

        // Process dataAttributes
        foreach (var attribute in returnValue.StructuredOutput.DataAttributes) {
            foreach (var key in attribute.Keys) {
                nonID[key] = attribute[key];
                elementStrings[key] = attribute[key];
            }
        }

        // Process identifiers
        foreach (var identifier in returnValue.StructuredOutput.Identifiers) {
            foreach (var key in identifier.Keys) {
                elementStrings[key] = identifier[key];
            }
        }

        returnValue = returnValue with { NonIdMap = nonID };

        var nonIDKeys = new List<string>(nonID.Keys);
        var aiKeys = new List<string>(elementStrings.Keys);

        returnValue = returnValue with { IdentifierMap = returnValue.StructuredOutput.Identifiers.FirstOrDefault() ?? new Dictionary<string, string>() };

        var primaryIdentifierMap = returnValue.StructuredOutput.Identifiers.FirstOrDefault();
        var primaryIdentifierMapKeys = new List<string>(primaryIdentifierMap?.Keys ?? []);

        returnValue = returnValue with { PrimaryIdentifier = primaryIdentifierMapKeys[0] };
        var primaryIdentifier = primaryIdentifierMapKeys[0];

        var instanceIdentifierKeys = new List<string>(_semanticsTable.Keys);
        bool isInstanceIdentifier = false;
        var outputObject = new Dictionary<string, object>();

        var context = new Dictionary<string, object>()
        {
            { "schema", "http://schema.org/" },
            { "gs1", "https://gs1.org/voc/" },
            { "rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#" },
            { "rdfs", "http://www.w3.org/2000/01/rdf-schema#" },
            { "owl", "http://www.w3.org/2002/07/owl#" },
            { "dcterms", "http://purl.org/dc/terms/" },
            { "xsd", "http://www.w3.org/2001/XMLSchema#" },
            { "skos", "http://www.w3.org/2004/02/skos/core#" },
            { "gs1:value", new Dictionary<string, object> { { "@type", "xsd:float" } } }
        };

        if (instanceIdentifierKeys.Contains(primaryIdentifier)) {
            if (_semanticsTable.TryGetValue(primaryIdentifier, out var instanceIdentifierSemantics)) {
                int? minLength = instanceIdentifierSemantics is SemanticsMinLength length ? length.Value : null;
                var requires = instanceIdentifierSemantics is SemanticsRequired required ? required : null;

                isInstanceIdentifier =
                    requires is null ||
                    requires.Any(nonIDKeys.Contains) ||
                    (minLength != null && primaryIdentifier.Length >= minLength);
            }
        }

        if (isInstanceIdentifier) {
            outputObject["@id"] = excludeQueryString;
            var osa = new List<string> { uncompressedDL };
            if (gs1DigitalLinkUri != uncompressedDL) {
                osa.Add(gs1DigitalLinkUri);
            }

            outputObject["owl:sameAs"] = osa;
        }
        else {
            outputObject["@id"] = "_:1";
        }

        var classSemanticsKeys = new List<string>(_classSemantics.Keys);
        var stringSemanticsKeys = new List<string>(_stringSemantics.Keys);
        var quantitativeValueSemanticsKeys = new List<string>(_quantitativeValueSemantics.AiKeys);
        var dateSemanticsKeys = new List<string>(_dateSemantics.Keys);
        var dateTimeSecondsSemanticsKeys = new List<string>(_dateTimeSecondsSemantics.Keys);
        var dateTimeMinutesSemanticsKeys = new List<string>(_dateTimeMinutesSemantics.Keys);
        var dateRangeSemanticsKeys = new List<string>(_dateRangeSemantics.Keys);

        // Handle class semantics
        var otype = new List<string> { "rdfs:Class", "owl:Class" };

        foreach (var ck in classSemanticsKeys) {
            if (aiKeys.Contains(ck)) {
                if (_classSemantics.TryGetValue(ck, out var cList)) {
                    otype.AddRange(cList);
                }
            }
        }

        outputObject["@type"] = otype;

        // Path analysis
        var uriStem = returnValue2.UriStem;
        var pathComponents = returnValue2.PathComponents?[1..].Split('/');

        if (pathComponents != null && pathComponents.Length > 2 && (pathComponents.Length % 2) == 0) {
            int l = pathComponents.Length - 2;
            var superClasses = new List<Dictionary<string, string>>();

            while (l >= 2) {
                var sl = pathComponents.Take(l).ToArray();
                var superClass = uriStem + "/" + string.Join("/", sl);
                superClasses.Add(new Dictionary<string, string> { { "@id", superClass } });
                l -= 2;
            }

            if (superClasses.Count > 0) {
                outputObject["dcterms:isPartOf"] = superClasses;
                outputObject["rdfs:subClassOf"] = superClasses;
            }
        }

        // Handle string semantics
        foreach (var sk in stringSemanticsKeys) {
            if (aiKeys.Contains(sk)) {
                if (_stringSemantics.TryGetValue(sk, out var predicates)) {
                    foreach (var predicate in predicates) {
                        outputObject[predicate] = elementStrings[sk].ToString();
                    }
                }
            }
        }

        // Handle quantitative requiredAi semantics
        foreach (var qk in quantitativeValueSemanticsKeys) {
            if (aiKeys.Contains(qk)) {
                var qvItem = _quantitativeValueSemantics[qk];  // There is something quite funky going on here.
                var predicate = qvItem.Predicates[0];
                var rec20 = qvItem.Rec20;
                var bareValueStr = elementStrings[qk].ToString();

                if (double.TryParse(bareValueStr, out var bareValue)) {
                    var fourthDigit = qk[3]; // Assuming AI code is at least 4 characters
                    int decimalPlaces = fourthDigit - '0';
                    double value = bareValue;

                    for (int idx = 0; idx < decimalPlaces; idx++) {
                        value /= 10.0;
                    }

                    var qvDict = new Dictionary<string, object>
                    {
                        { "@type", "gs1:QuantitativeValue" },
                        { "gs1:unitCode", rec20 },
                        { "gs1:value", value.ToString() }
                    };

                    outputObject[predicate] = qvDict;
                }
            }
        }

        // Handle date semantics (xsd:date)
        foreach (var dv in dateSemanticsKeys) {
            if (aiKeys.Contains(dv)) {
                if (_dateSemantics.TryGetValue(dv, out var predicates)) {
                    foreach (var predicate in predicates) {
                        context[predicate] = new Dictionary<string, string> { { "@type", "xsd:date" } };
                        var bareValue = elementStrings[dv].ToString();
                        var xsdDateValue = bareValue.SixDigitToXsdDate(methodName, paramName);
                        outputObject[predicate] = xsdDateValue;
                    }
                }
            }
        }

        // Handle dateTimeSecondsSemantics (xsd:dateTime)
        foreach (var dtsv in dateTimeSecondsSemanticsKeys) {
            if (aiKeys.Contains(dtsv)) {
                if (_dateTimeSecondsSemantics.TryGetValue(dtsv, out var predicates)) {
                    foreach (var predicate in predicates) {
                        context[predicate] = new Dictionary<string, string> { { "@type", "xsd:dateTime" } };
                        var bareValue = elementStrings[dtsv].ToString();
                        var xsdDateTimeValue = bareValue.MaxTwelveDigitToXsdDateTime(methodName, paramName);
                        outputObject[predicate] = xsdDateTimeValue;
                    }
                }
            }
        }

        // Handle dateTimeMinutesSemantics
        foreach (var dtmv in dateTimeMinutesSemanticsKeys) {
            if (aiKeys.Contains(dtmv)) {
                if (_dateTimeMinutesSemantics.TryGetValue(dtmv, out var predicates)) {
                    foreach (var predicate in predicates) {
                        context[predicate] = new Dictionary<string, string> { { "@type", "xsd:dateTime" } };
                        var bareValue = elementStrings[dtmv].ToString();
                        var xsdDateTimeValue = bareValue.TenDigitToXsdDateTime(methodName, paramName);
                        outputObject[predicate] = xsdDateTimeValue;
                    }
                }
            }
        }

        // Handle dateRangeSemantics
        foreach (var dr in dateRangeSemanticsKeys) {
            if (aiKeys.Contains(dr)) {
                if (_dateRangeSemantics.TryGetValue(dr, out var predicates)) {
                    foreach (var predicate in predicates) {
                        var bareValue = elementStrings[dr].ToString();

                        if (bareValue.Length == 6) {
                            var xsdDateValue = bareValue.SixDigitToXsdDate(methodName, paramName);
                            context[predicate] = new Dictionary<string, string> { { "@type", "xsd:dateTime" } };
                            outputObject[predicate] = xsdDateValue;
                        }
                        else if (bareValue.Length == 12) {
                            var xsdStartDateValue = bareValue[..6].SixDigitToXsdDate(methodName, paramName);
                            var xsdEndDateValue = bareValue.Substring(6, 6).SixDigitToXsdDate(methodName, paramName);
                            context[predicate + "Start"] = new Dictionary<string, string> { { "@type", "xsd:dateTime" } };
                            context[predicate + "End"] = new Dictionary<string, string> { { "@type", "xsd:dateTime" } };
                            outputObject[predicate + "Start"] = xsdStartDateValue;
                            outputObject[predicate + "End"] = xsdEndDateValue;
                        }
                    }
                }
            }
        }

        outputObject["gs1:elementStrings"] = returnValue.ElementStringOutput;

        var semantics = new Dictionary<string, object>
        {
            { "@context", context }
        };

        if (semantics is not null) {
            foreach (var kvp in outputObject) {
                semantics[kvp.Key] = kvp.Value;
            }
        }

        return new UriSemantics(semantics ?? []);
    }

    //////////private Dictionary<string, object> Canonical(Dictionary<string, object> aiDictionary) {
    //////////    Dictionary<string, object> analysis = [];
    //////////    var sortedKeys = aiDictionary.Keys.OrderBy(aiKey => aiKey).ToList();
    //////////    foreach (var key in sortedKeys) {
    //////////        analysis[key] = aiDictionary[key];
    //////////    }
    //////////    return analysis;
    //////////}

    /// <summary>
    /// Extracts GS1 Application Identifiers and their values from key GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">The Digital Link URI.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <param name="uriAnalysis">The URI analysis.</param>
    /// <returns>A dictionary of GS1 Application Identifiers (AIs).</returns>
    /// <exception cref="ArgumentException">Invalid GS1 Digital Link.</exception>
    private static ExtractedData DoExtractAIsFromGs1DigitalLink(
        this string gs1DigitalLinkUri,
        string methodName,
        string paramName,
        UriAnalytics? uriAnalysis = null) {

        var gs1AIs = new Dictionary<string, string>();

        uriAnalysis ??= AnalyseUri(gs1DigitalLinkUri, false, methodName, paramName);

        if (uriAnalysis.DetectedForm == DigitalLinkForm.Unknown) {
            var message = Resources.Errors.ErrorMsgUnableToDetermineTheFormOfTheDigitalLink;
            var location = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, location, message);
        }
        else if (uriAnalysis.DetectedForm != DigitalLinkForm.Uncompressed) {
            return ExtractFromCompressedGs1DigitalLink(
                        gs1DigitalLinkUri,
                        uriAnalysis,
                        methodName,
                        paramName);
        }

        var uriPathInfo = uriAnalysis.UriPathInfo;
        var pathCandidates = uriAnalysis.PathCandidates;

        // Split path and skip the leading empty element if URI starts with "/"
        var splitPath = uriPathInfo.Split('/').Skip(1).ToArray();

        var aiSeq = new List<string>();
        var splitPathLength = splitPath.Length;

        for (var idx = splitPathLength - 1; idx >= 0; idx--) {

            if (idx % 2 == 0) {
                var key = splitPath[idx];

                if (!_regexAllNum.IsMatch(key)) {
                    key = _shortNameToNumeric[key];
                }

                aiSeq.Add(key);
            }
        }

        aiSeq.Reverse();

        // Check the membership of the sequence.
        aiSeq.ValidateSequenceMembership(methodName, paramName);

        // Check that the URI path components appear in the correct sequence
        aiSeq.ValidateSequenceOrder(methodName, paramName);

        foreach (var kvp in pathCandidates) {
            var key = kvp.Key;

            if (!_regexAllNum.IsMatch(key)) {
                var numkey = _shortNameToNumeric[key];

                // Do something with numkey if needed
            }
        }

        var gs1Pairs = uriAnalysis.QueryStringGs1Pairs.ToDictionary();
        var nonGS1queryStringPairs = uriAnalysis.QueryStringNonGs1Pairs.ToDictionary();

        // Validate the non-GS1 key-value pairs.
        nonGS1queryStringPairs.ValidateNonGs1KeyValuePairs(methodName, paramName);
        uriAnalysis.OtherQueryContent.ValidateOtherQueryStringContent(methodName, paramName);
        uriAnalysis.Fragment.ValidateFragmentSpecifier(methodName, paramName);

        // merge pathCandidates and queryStringGs1Pairs into gs1Pairs
        foreach (var kvp in pathCandidates) {
            gs1Pairs[kvp.Key] = kvp.Value;
        }

        // process gs1Pairs
        var keysToRemove = new List<string>();

        foreach (var key in gs1Pairs.Keys.ToList()) {

            if (!_regexAllNum.IsMatch(key)) {

                if (_shortNameToNumeric.TryGetValue(key, out string? numkey)) {
                    gs1Pairs[numkey] = gs1Pairs[key];
                    keysToRemove.Add(key);
                }
                else {
                    keysToRemove.Add(key);
                }
            }
        }

        foreach (var key in keysToRemove) {
            gs1Pairs.Remove(key);
        }

        // check that each entry in the associative array has correct syntax and correct digit (where appropriate)
        foreach (var key in gs1Pairs.Keys) {
            key.VerifySyntax(gs1Pairs[key], methodName, paramName);
            key.VerifyCheckDigit(gs1Pairs[key], methodName, paramName);
            gs1AIs[key] = PadGTIN(key, gs1Pairs[key]);
        }

        return new ExtractedData(
            gs1AIs,
            nonGS1queryStringPairs,
            uriAnalysis.OtherQueryContent,
            uriAnalysis.Fragment);

        static string PadGTIN(string ai, string value) {

            // always pad the requiredAi of any GTIN [ AI (01) or (02) ] to 14 digits
            string newvalue = value;

            if ((ai == "01") || (ai == "(01)") || (ai == "02") || (ai == "(02)")) {
                if (value.Length == 8) {
                    newvalue = "000000" + value;
                }

                if (value.Length == 12) {
                    newvalue = "00" + value;
                }

                if (value.Length == 13) {
                    newvalue = "0" + value;
                }
            }

            return newvalue;
        }
    }

    /// <summary>
    /// Change the compression level of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="digitalLinkUri">The GS1 Digital Link URI.</param>
    /// <param name="compressionLevel">The required level of compression.  Should be uncompressed or partially compressed.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <param name="useOptimisations">If true, the compression will take advantage of any available optimisations.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key-value pairs will be compressed.</param>
    /// <param name="useShortNames">
    /// Use short names ('convenience alphas') instead of AIs. This is a legacy feature.
    /// </param>
    /// <returns>A GS1 Digital Link URI.</returns>
    private static string DoChangeGs1DigitalLinkCompression(
        this string digitalLinkUri,
        CompressionLevel compressionLevel,
        string methodName,
        string paramName,
        bool useOptimisations = false,
        bool compressNonGs1KeyValuePairs = false,
        bool useShortNames = false) {

        var analysis = AnalyseUri(digitalLinkUri, false, methodName, paramName);

        // If the URI is uncompressed, then validate it
        if (analysis.DetectedForm == DigitalLinkForm.Uncompressed) {
            var aiSeq = new List<string>();
            aiSeq = analysis.PathCandidates.Keys.ToList();
            aiSeq.Reverse();

            // Check the membership of the sequence.
            aiSeq.ValidateSequenceMembership(methodName, paramName);

            // Check that the URI path components appear in the correct sequence
            aiSeq.ValidateSequenceOrder(methodName, paramName);
        }

        // If no change is required, return the original Digital Link.
        if ((int)compressionLevel == (int)analysis.DetectedForm &&
            compressionLevel == CompressionLevel.Compressed) {
            return digitalLinkUri;
        }

        var uriStem = analysis.UriStem;

        if ((int)compressionLevel > (int)analysis.DetectedForm) {
            return DoCompressGs1DigitalLink(
                    digitalLinkUri,
                    uriStem,
                    compressionLevel,
                    useOptimisations,
                    compressNonGs1KeyValuePairs,
                    methodName,
                    paramName,
                    useShortNames);
        }

        return DoDecompressGs1DigitalLink(
                    digitalLinkUri,
                    analysis,
                    uriStem,
                    compressionLevel,
                    useOptimisations,
                    compressNonGs1KeyValuePairs,
                    nameof(DoChangeGs1DigitalLinkCompression),
                    nameof(digitalLinkUri),
                    useShortNames);
    }

    /// <summary>
    /// Decompresses a compressed GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">The compressed GS1 Digital Link URI.</param>
    /// <param name="uriAnalysis">URI analytics.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="compressionLevel">The required level of compression.  Should be uncompressed or partially compressed.</param>
    /// <param name="useOptimisations">If true, the compression will take advantage of any available optimisations.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key-value pairs will be compressed.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <param name="useShortNames">
    /// Use short names rather than AIs (e.g., gtin, rather than 01. This applies only when creating partially
    /// compressed Digital Links using the uncompressedPrimary parameter.  This is a legacy feature.
    /// </param>
    /// <returns>A GS1 Digital Link URI.</returns>
    private static string DoDecompressGs1DigitalLink(
        string gs1DigitalLinkUri,
        UriAnalytics uriAnalysis,
        string? uriStem,
        CompressionLevel compressionLevel,
        bool useOptimisations,
        bool compressNonGs1KeyValuePairs,
        string methodName,
        string paramName,
        bool useShortNames = false) {

        // Extract a dictionary of AIs and AI values from the compressed Digital Link
        var extractedData = ExtractFromCompressedGs1DigitalLink(gs1DigitalLinkUri, uriAnalysis, methodName, paramName);

        // Build the uncompressed Digital Link
        var gs1AIs = extractedData.Gs1AIs;
        var nonGs1KeyValuePairs = extractedData.NonGs1KeyValuePairs;

        if (compressionLevel == CompressionLevel.Uncompressed) {
            if (useShortNames) {
#pragma warning disable CS0618 // Type or member is obsolete
                return FromGs1DataToDigitalLinkWithShortNames(
                            gs1AIs ?? [],
                            uriStem,
                            DigitalLinkForm.Uncompressed,
                            false,
                            nonGs1KeyValuePairs,
                            false,
                            extractedData.OtherQueryStringContent,
                            extractedData.FragmentSpecifier).Value;
#pragma warning restore CS0618 // Type or member is obsolete
            }

            return FromGs1DataToDigitalLink(
                        gs1AIs ?? [],
                        uriStem,
                        DigitalLinkForm.Uncompressed,
                        false,
                        nonGs1KeyValuePairs,
                        false,
                        extractedData.OtherQueryStringContent,
                        extractedData.FragmentSpecifier).Value;
        }
        else {
            return DoCompressGs1DigitalLink(
                        gs1DigitalLinkUri,
                        uriStem,
                        compressionLevel,
                        useOptimisations,
                        compressNonGs1KeyValuePairs,
                        methodName,
                        paramName,
                        useShortNames);
        }
    }

    /// <summary>
    /// Compress a GS1 Digital Link URI.
    /// </summary>
    /// <param name="digitalLinkUri">The GS1 Digital Link URI.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="compressionLevel">The desired level of compression (partial or full).</param>
    /// <param name="useOptimisations">If true, the compression will take advantage of any available optimisations.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key-value pairs will be compressed.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <param name="useShortNames">
    /// Use short names rather than AIs (e.g., gtin, rather than 01. This applies only when creating partially
    /// compressed Digital Links using the uncompressedPrimary parameter. This is a legacy feature.
    /// </param>
    /// <returns>The compressed GS1 Digital Link URI.</returns>
    private static string DoCompressGs1DigitalLink(
        string digitalLinkUri,
        string? uriStem,
        CompressionLevel compressionLevel,
        bool useOptimisations,
        bool compressNonGs1KeyValuePairs,
        string methodName,
        string paramName,
        bool useShortNames = false) {

        // extract query string
        var firstQuestionMark = digitalLinkUri.IndexOf('?');
        var queryString = string.Empty;
        Dictionary<string, string> nonGs1KeyValuePairs = [];
        var queryStringOther = new StringBuilder();
        var fragment = string.Empty;

        if (firstQuestionMark > -1) {
            queryString = digitalLinkUri[(firstQuestionMark + 1) ..];
        }

        if (!string.IsNullOrEmpty(queryString)) {
            queryString = queryString.Replace(';', '&');

            // if semicolon was used as delimiter between key=requiredAi parameters, replace with ampersand as delimiter
            var firstFragment = queryString.IndexOf('#');

            if (firstFragment > -1) {
                fragment = queryString[(firstFragment + 1) ..];
                queryString = queryString[..firstFragment];
            }

            var parameters = queryString.Split('&');

            foreach (var param in parameters) {
                var splitPair = param.Split('=');

                // if the key is not numeric AND is not a Short Name such as exp or expdt, then add to the nonGS1keyvalueePairs
                if (splitPair.Length == 2 &&
                    splitPair[0] != null &&
                    splitPair[1] != null &&
                    (!_regexAllNum.IsMatch(splitPair[0]) &&
                    (!_shortNameToNumeric.ContainsKey(splitPair[0])))) {
                    nonGs1KeyValuePairs[splitPair[0]] = splitPair[1].PercentDecode();
                }
                else if (splitPair.Length == 1) {
                    // The query string parameter is not in key=value format.
                    queryStringOther.Append($"&{splitPair[0].PercentDecode()}");
                }
            }
        }
        else {
            var firstFragment = digitalLinkUri.IndexOf('#');
            fragment = firstFragment > -1 ? digitalLinkUri[(firstFragment + 1) ..] : string.Empty;
        }

        var extractedData = digitalLinkUri.DoExtractAIsFromGs1DigitalLink(
                                            nameof(DoCompressGs1DigitalLink),
                                            nameof(digitalLinkUri));
        var gs1AIs = extractedData.Gs1AIs;
        var other = extractedData.NonGs1KeyValuePairs;

        foreach (var kvp in other ?? []) {
            nonGs1KeyValuePairs[kvp.Key] = kvp.Value;
        }

        var compressedDL = compressionLevel == CompressionLevel.PartiallyCompressed
            ? BuildPartiallyCompressedGs1DigitalLink(nonGs1KeyValuePairs)
            : BuildCompressedGS1digitalLink(gs1AIs ?? [], uriStem, useOptimisations, nonGs1KeyValuePairs, compressNonGs1KeyValuePairs);

        var returnedQueryString = queryStringOther.ToString();
        var qsDelim = compressedDL.Contains('?') ? "&" : "?";

        if (returnedQueryString.Length > 0) {
            returnedQueryString = qsDelim + returnedQueryString[1..];
        }

        return compressedDL + returnedQueryString + (fragment.Length > 0 ? $"#{fragment}" : string.Empty);

        string BuildPartiallyCompressedGs1DigitalLink(Dictionary<string, string> nonGs1KeyValuePairs) {
            var separated = SeparateIdNonId(gs1AIs ?? []);
#pragma warning disable CS0618 // Type or member is obsolete
            var keyPart = useShortNames
                ? FromGs1DataToDigitalLinkWithShortNames(separated["ID"], uriStem, DigitalLinkForm.Uncompressed).Value
                : FromGs1DataToDigitalLink(separated["ID"], uriStem, DigitalLinkForm.Uncompressed).Value;
#pragma warning restore CS0618 // Type or member is obsolete

            var compressedPart = CompressGs1AIsToBinary(
                separated["nonID"],
                useOptimisations,
                compressNonGs1KeyValuePairs ? nonGs1KeyValuePairs : []).BinaryToSafe64();

            var returnValue = keyPart + (compressedPart.Length > 0 ? "/" + compressedPart : string.Empty);
            var initialQueryStringCharacter = keyPart.Contains('?') ? "&" : "?";
            var otherQueryContent = new StringBuilder();

            if (!compressNonGs1KeyValuePairs) {
                foreach (var kvp in nonGs1KeyValuePairs) {
                    otherQueryContent.Append($"{initialQueryStringCharacter}{kvp.Key.PercentEncode()}={kvp.Value.PercentEncode()}");
                    initialQueryStringCharacter = "&";
                }
            }

            return returnValue + otherQueryContent.ToString();
        }
    }

    /// <summary>
    /// Populates classification lists with AIs from a dictionary of GS1 Application Identifiers.  Each AI and value
    /// is validated.
    /// </summary>
    /// <param name="gs1AIs">The dictionary of GS1 Application Identifiers.</param>
    /// <param name="identifiers">The AIs representing Digital Link key identifiers.</param>
    /// <param name="qualifiers">The AIs representing Digital Link qualifiers.</param>
    /// <param name="attributes">The AIs representing Digital Link data attributes.</param>
    /// <param name="fnc1Elements">The AIs that require FNC1 (ASCII 29) delimiters.</param>
    /// <param name="nonFnc1Elements">The AIs that have a predefined length.</param>
    /// <param name="otherKeys">Any other keys provided in the dictionary.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <exception cref="ArgumentException">Invalid AI detected.</exception>
    private static void PopulateClassificationLists(
        this IReadOnlyDictionary<string, string> gs1AIs,
        List<string> identifiers,
        List<string> qualifiers,
        List<string> attributes,
        List<string> fnc1Elements,
        List<string> nonFnc1Elements,
        List<string> otherKeys,
        string methodName,
        string paramName) {

        var exceptionsMessage = new StringBuilder(Resources.Errors.ErrorMsgPartTheFollowingIssuesWereDetected);
        var exceptions = false;

        // Classify AIs according to _aiMaps
        foreach (var ai in gs1AIs.Keys) {
            var other = true;
            var parseResults = Parser.Parse($"{ai}{gs1AIs[ai]}");

            if (!parseResults.IsRecognised) {
                var errorMessage = string.Format(Resources.Errors.ErrorMsg0IsNotARecognisedGs1Ai, ai);
                Trace.TraceError($"{methodName}: {errorMessage}");

                // No exception is thrown here. If the dictionary includes unrecognised AIs,
                // they will be added to the 'other' list.
            }
            else if (parseResults.Exceptions.Any()) {
                exceptions = true;

                foreach (var exception in parseResults.Exceptions) {
                    var fatalSpecifier = exception.IsFatal ? Resources.Errors.ErrorMsgPartFatal : string.Empty;
                    exceptionsMessage.Append($"\r\n{exception.ErrorNumber}{fatalSpecifier}: {exception.Message}");
                }
            }

            if (exceptions) {
                continue;
            }

            if (_aiMaps.Identifiers.Contains(ai)) {
                identifiers.Add(ai);
                other = false;
            }

            if (_aiMaps.Qualifiers.Contains(ai)) {
                qualifiers.Add(ai);
                other = false;
            }

            if (_aiMaps.DataAttributes.Contains(ai)) {
                attributes.Add(ai);
                other = false;
            }

            if (_aiMaps.Fnc1Elements.Contains(ai)) {
                fnc1Elements.Add(ai);
                other = false;
            }

            if (_aiMaps.PredefinedLengthElements.Contains(ai)) {
                nonFnc1Elements.Add(ai);
                other = false;
            }

            if (other) {
                otherKeys.Add(ai);
            }
        }

        if (exceptions) {
            var location = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidGs1ApplicationIdentifier, location, exceptionsMessage.ToString());
        }
    }

    /// <summary>
    /// Converts a dictionary of GS1 Application Identifiers and their values into a GS1 Digital Link URI.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 Application Identifiers.</param>
    /// <param name="uriStem">The URI stem for the Digital Link. If omitted, the library will use https://id.gs1.org.</param>
    /// <param name="digitalLinkForm">The level of compression to apply to the Digital Link URI.</param>
    /// <param name="useOptimisations">If true, compression optimisations are used, if any apply..</param>
    /// <param name="nonGs1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <param name="otherQueryContent">Any additional non-key=value content to be included in the query string.</param>
    /// <param name="fragment">Any additional fragment specifier to be included in the URI.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <param name="useShortNames">Use short names ('convenience alphas') instead of AIs. This is a legacy feature.</param>
    /// <returns>A GS1 Digital Link.</returns>
    /// <exception cref="ArgumentException">Invalid data passed to method.</exception>
    private static string DoBuildGs1DigitalLink(
        this Dictionary<string, string> gs1AIs,
        string? uriStem,
        DigitalLinkForm digitalLinkForm,
        bool useOptimisations,
        Dictionary<string, string>? nonGs1KeyValuePairs,
        bool compressNonGs1KeyValuePairs,
        string? otherQueryContent,
        string? fragment,
        string methodName,
        string paramName,
        bool useShortNames = false) {

        // return an empty string if the input dictionary is empty.
        if (gs1AIs.Count == 0) {
            Trace.TraceWarning($"{methodName}: The dictionary of AIs ({nameof(gs1AIs)} is empty.");
            return string.Empty;
        }

        var otherQueryStringContent = new StringBuilder();
        var otherFragment = string.Empty;

        // The 'otherQueryContent' parameter may be used to pass additional key-pair values, fragments and other content. This could include
        // GS1 as well as non-GS1 key-value pairs
        if (!string.IsNullOrEmpty(otherQueryContent)) {
            var otherQueryString = otherQueryContent.Replace(';', '&');

            // if semicolon was used as delimiter between key=requiredAi parameters, replace with ampersand as delimiter
            var firstFragment = otherQueryString.IndexOf('#');

            if (firstFragment > -1) {
                otherFragment = otherQueryString[(firstFragment + 1) ..];
                otherQueryString = otherQueryString[..firstFragment];
            }

            var parameters = otherQueryString.Split('&');

            foreach (var param in parameters) {
                var splitPair = param.Split('=');

                // if the key is not numeric AND is not a Short Name such as exp or expdt, then add to the nonGS1keyvalueePairs
                if (splitPair.Length == 2 &&
                    splitPair[0] != null &&
                    splitPair[1] != null) {
                    var isAllDigits = _regexAllNum.IsMatch(splitPair[0]);

                    if (nonGs1KeyValuePairs is not null
                        && !isAllDigits
                        && (!useShortNames
                            || (useShortNames
                                && !_shortNameToNumeric.ContainsKey(splitPair[0])))) {
                                    nonGs1KeyValuePairs[splitPair[0]] = splitPair[1].PercentDecode();
                    }
                    else {
                        var ai = !isAllDigits && useShortNames ? _shortNameToNumeric[splitPair[0]] : splitPair[0];

                        // Because the standard specifies that all-numeric keys are disallowed for non-GS1 key=value pairs,
                        // we will assume that the key is a GS1 key and add it to the GS1 AI dictionary if it does not already exist.
                        if (!gs1AIs.ContainsKey(ai)) {
                            gs1AIs.Add(ai, splitPair[1].PercentDecode());
                        }
                    }
                }
                else if (splitPair.Length == 1) {
                    // The query string parameter is not in key=value format.
                    otherQueryStringContent.Append($"&{splitPair[0].PercentDecode()}");
                }
            }
        }

        otherQueryContent = otherQueryStringContent.ToString();

        if (!string.IsNullOrWhiteSpace(fragment) && !string.IsNullOrWhiteSpace(otherFragment)) {
            var message = string.Format(Resources.Errors.ErrorMsgThe0ParameterContains1AsAFragmentButAFragmentHasBeenProvidedUsingThe2Parameter, nameof(otherQueryContent), otherFragment, nameof(fragment));
            var location = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, nameof(otherQueryContent));
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidFragmentSpecifier, location, message);
        }

        fragment = string.IsNullOrWhiteSpace(fragment) ? otherFragment : fragment;

        // Validate non-GS1 key-value pairs.
        nonGs1KeyValuePairs?.ValidateNonGs1KeyValuePairs(methodName, paramName);
        otherQueryContent.ValidateOtherQueryStringContent(methodName, paramName);
        fragment.ValidateFragmentSpecifier(methodName, paramName);

        List<string> identifiers = [];
        List<string> qualifiers = [];
        List<string> attributes = [];
        List<string> fnc1Elements = [];
        List<string> nonFnc1Elements = [];
        List<string> otherKeys = [];

        gs1AIs.PopulateClassificationLists(
            identifiers,
            qualifiers,
            attributes,
            fnc1Elements,
            nonFnc1Elements,
            otherKeys,
            methodName,
            paramName);

        string path = string.Empty;
        var queryStringArray = new List<string>();
        string queryString = string.Empty;
        string webUri = string.Empty;

        // Get the normalised URI stem or the default (canonical) URI stem, if no stem is provided.
        uriStem = uriStem.GetNormalisedUriStem();

        if (identifiers.Count <= 0) {
            var message = Resources.Errors.ErrorMsgNoKeyIdentifierFoundInTheGsDigitallinkUriPathInformation;
            var location = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, location, message);
        }

        identifiers[0].VerifyCheckDigit(gs1AIs[identifiers[0]], methodName, paramName);

        var aiSeq = new List<string>() { identifiers[0] };
        aiSeq.AddRange(from qualifier in qualifiers
                       select qualifier);

        // Check that the URI path components appear to be in a correct sequence
        // There is no constraint on the order in which items appear in the GS1 AI
        // dictionary, and the correct order is imposed at the point that the
        // Digital Link is created, so the only check here is to make sure that
        // all qualifiers are for a single sequence.
        aiSeq.ValidateSequenceMembership(methodName, paramName);

        if (useShortNames) {
            // Using short names
            if (_aiShortNames.TryGetValue(identifiers[0], out string? value)) {
                path = "/" + value + "/" + gs1AIs[identifiers[0]].PercentEncode();
            }
            else {
                path = "/" + identifiers[0] + "/" + gs1AIs[identifiers[0]].PercentEncode();
            }
        }
        else {
            // Using numeric AIs
            path = "/" + identifiers[0] + "/" + gs1AIs[identifiers[0]].PercentEncode();
        }

        // append any data Qualifiers in the expected order
        if (_aiQualifiers.TryGetValue(identifiers[0], out string[]? qualifierValues)) {
            foreach (var qualifier in qualifierValues) {
                if (qualifiers.Contains(qualifier)) {
                    if (useShortNames) {
                        // Using short names
                        if (_aiShortNames.TryGetValue(qualifier, out string? shortName)) {
                            path += "/" + shortName + "/" + gs1AIs[qualifier].PercentEncode();
                        }
                        else {
                            path += "/" + qualifier + "/" + gs1AIs[qualifier].PercentEncode();
                        }
                    }
                    else {
                        // Using numeric AIs
                        path += "/" + qualifier + "/" + gs1AIs[qualifier].PercentEncode();
                    }
                }
            }
        }

        // if there are any data attributes, we need to add these to the query string
        if (attributes.Count > 0) {
            foreach (var a in attributes) {
                if (useShortNames) {
                    if (_aiShortNames.TryGetValue(a, out string? shortName)) {
                        queryStringArray.Add(shortName + "=" + gs1AIs[a].PercentEncode());
                    }
                    else {
                        queryStringArray.Add(a + "=" + gs1AIs[a].PercentEncode());
                    }
                }
                else {
                    // Using numeric AIs
                    queryStringArray.Add(a + "=" + gs1AIs[a].PercentEncode());
                }
            }

            queryString = "?" + string.Join("&", queryStringArray);
        }

        webUri = uriStem + path + queryString;

        if (otherKeys.Count > 0) {
            webUri += (queryString == string.Empty ? "?" : "&") +
                      string.Join("&", (from key in otherKeys
                                        select key + "=" + gs1AIs[key]).ToList());
        }

        if (nonGs1KeyValuePairs != null && nonGs1KeyValuePairs.Count > 0) {
            webUri += (queryString == string.Empty ? "?" : "&") +
                      string.Join("&", (from key in nonGs1KeyValuePairs.Keys
                                        select key + "=" + nonGs1KeyValuePairs[key]).ToList());
        }

        // Compress as required
        webUri = digitalLinkForm switch {
            DigitalLinkForm.Compressed => webUri.DoChangeGs1DigitalLinkCompression(
                                            CompressionLevel.Compressed,
                                            methodName,
                                            paramName,
                                            useOptimisations,
                                            compressNonGs1KeyValuePairs,
                                            useShortNames),
            DigitalLinkForm.PartiallyCompressed => webUri.DoChangeGs1DigitalLinkCompression(
                                                            CompressionLevel.PartiallyCompressed,
                                                            methodName,
                                                            paramName,
                                                            useOptimisations,
                                                            compressNonGs1KeyValuePairs,
                                                            useShortNames),
                                                        _ => webUri
        };

        // Additional content is never compressed.  Append to the URI.
        var qsDelim = webUri.Contains('?') ? "&" : "?";

        if (otherQueryContent.Length > 0) {
            otherQueryContent = qsDelim + otherQueryContent[1..];
        }

        var additionalQueryContent = !string.IsNullOrWhiteSpace(otherQueryContent)
            ? otherQueryContent
            : string.Empty;

        var additionalFragment = !string.IsNullOrWhiteSpace(fragment)
            ? $"#{fragment}"
            : string.Empty;

        return webUri + additionalQueryContent + additionalFragment;
    }

    /// <summary>
    /// Converts a flat dictionary of GS1 Application Identifiers and their values into a
    /// more structured object in which the primary identification key, key Qualifiers,
    /// data attributes and other key=requiredAi parameters from the URI string are clearly
    /// identified as such.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 AIs.</param>
    /// <param name="otherArray">A dictionary of non-GS1 key-value pairs.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>A categorised dictionary of lists of dictionaries of AIs and non-GS1 key-value pairs.</returns>
    private static Dictionary<string, ReadOnlyCollection<ReadOnlyDictionary<string, string>>> BuildStructuredArray(
            Dictionary<string, string> gs1AIs,
            Dictionary<string, string> otherArray,
            string methodName,
            string paramName) {
        var analyticsKeys = new List<string> { "identifiers", "qualifiers", "dataAttributes" };
        var map = new Dictionary<string, List<ReadOnlyDictionary<string, string>>> {
            ["identifiers"] = [],
            ["qualifiers"] = [],
            ["dataAttributes"] = [],
            ["other"] = []
        };

        foreach (var key in gs1AIs.Keys) {
            var aiValues = new Dictionary<string, string> {
                [key] = gs1AIs[key]
            };
            bool other = true;

            foreach (var analyticsKey in analyticsKeys) {
                switch (analyticsKey) {
                    case "identifiers":
                        if (_aiMaps.Identifiers?.Contains(key) ?? false) {
                            map["identifiers"].Add(new ReadOnlyDictionary<string, string>(aiValues));
                            other = false;
                        }

                        break;
                    case "qualifiers":
                        if (_aiMaps.Qualifiers?.Contains(key) ?? false) {
                            map["qualifiers"].Add(new ReadOnlyDictionary<string, string>(aiValues));
                            other = false;
                        }

                        break;
                    case "dataAttributes":
                        if (_aiMaps.DataAttributes?.Contains(key) ?? false) {
                            map["dataAttributes"].Add(new ReadOnlyDictionary<string, string>(aiValues));
                            other = false;
                        }

                        break;
                }

                if (!other) {
                    break;
                }
            }

            if (other) {
                map["other"].Add(new ReadOnlyDictionary<string, string>(aiValues));
            }
        }

        foreach (var key in otherArray.Keys) {
            var otherArrayValue = new Dictionary<string, string> {
                [key] = otherArray[key]
            };
            map["other"].Add(new ReadOnlyDictionary<string, string>(otherArrayValue));
        }

        var identifierDict = map["identifiers"][0];

        // There'uriAnalysis exactly one key in identifierDict:
        var identifierKey = identifierDict.Keys.First();
        identifierKey.VerifySyntax(gs1AIs[identifierKey], methodName, paramName);
        identifierKey.VerifyCheckDigit(gs1AIs[identifierKey], methodName, paramName);

        Console.WriteLine("map = " + JsonSerializer.Serialize(map));

        return new Dictionary<string, ReadOnlyCollection<ReadOnlyDictionary<string, string>>> {
            ["identifiers"] = new ReadOnlyCollection<ReadOnlyDictionary<string, string>>(map["identifiers"]),
            ["qualifiers"] = new ReadOnlyCollection<ReadOnlyDictionary<string, string>>(map["qualifiers"]),
            ["dataAttributes"] = new ReadOnlyCollection<ReadOnlyDictionary<string, string>>(map["dataAttributes"]),
            ["other"] = new ReadOnlyCollection<ReadOnlyDictionary<string, string>>(map["other"])
        };
    }

    /// <summary>
    /// Converts a GS1 Digital Link URI into a dictionary of dictionaries containing
    /// GS1 Application Identifiers and their values together with non-GS1
    /// key=value pairs.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">A GS1 Digital Link URI.</param>
    /// <param name="uriAnalysis">Results of URI analysis.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <returns>The extracted data.</returns>
    /// <exception cref="ArgumentException">Invalid Digital Link.</exception>
    private static ExtractedData ExtractFromCompressedGs1DigitalLink(
        string gs1DigitalLinkUri,
        UriAnalytics? uriAnalysis,
        string methodName,
        string paramName) {

        if (string.IsNullOrWhiteSpace(gs1DigitalLinkUri)) {
            return new ExtractedData([], [], string.Empty, string.Empty);
        }

        // Need to remove unwanted trailing slash from gs1DigitalLinkUri if present
        if (gs1DigitalLinkUri.EndsWith('/')) {
            gs1DigitalLinkUri = gs1DigitalLinkUri[..^1];
        }

        (Dictionary<string, string> gs1AIs, Dictionary<string, string> nonGs1KeyValuePairs) objGs1;
        var nonGS1queryStringCandidates = new Dictionary<string, string>();
        var queryStringOther = new StringBuilder();

        uriAnalysis ??= AnalyseUri(gs1DigitalLinkUri, false, methodName, paramName);

        if (uriAnalysis.DetectedForm == DigitalLinkForm.Unknown) {
            var message = Resources.Errors.ErrorMsgUnableToDetermineTheFormOfTheDigitalLink;
            var location = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            ExtensionMethods.ThrowArgumentException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, location, message);
        }
        else if (uriAnalysis.DetectedForm == DigitalLinkForm.Uncompressed) {
            return DoExtractAIsFromGs1DigitalLink(gs1DigitalLinkUri, methodName, paramName, uriAnalysis);
        }

        var queryString = uriAnalysis.QueryString;
        var fragmentSpecifier = uriAnalysis.Fragment;
        var uriPathInfo = uriAnalysis.UriPathInfo;

        if (!string.IsNullOrEmpty(queryString)) {
            // replace semicolon with ampersand
            queryString = queryString.Replace(";", "&").PercentDecode();
            var firstFragment = queryString.IndexOf('#');

            if (firstFragment > -1) {
                queryString = queryString[..firstFragment];
                fragmentSpecifier = queryString[(firstFragment + 1) ..];
            }

            var parameters = queryString.Split('&');

            foreach (var param in parameters) {
                var splitPair = param.Split('=');
                if (splitPair.Length == 2 &&
                    splitPair[0] != null &&
                    splitPair[1] != null &&
                    !_regexAllNum.IsMatch(splitPair[0]) &&
                    !_shortNameToNumeric.ContainsKey(splitPair[0])) {

                    nonGS1queryStringCandidates[splitPair[0]] = splitPair[1].PercentDecode();
                }
                else if (splitPair.Length == 1) {
                    // The query string parameter is not in key=value format.
                    queryStringOther.Append($"&{splitPair[0].PercentDecode()}");
                }
            }
        }

        // remove initial forward slash
        if (uriPathInfo.StartsWith('/')) {
            uriPathInfo = uriPathInfo[1..];
        }

        uriPathInfo = uriPathInfo.PercentDecode();

        if (uriAnalysis.DetectedForm == DigitalLinkForm.Compressed) {
            objGs1 = DecompressBinaryToGs1AIs(uriPathInfo.Safe64ToBinary());
        }
        else {
            // handle partially compressed URI
            var index1 = uriPathInfo.IndexOf('/');
            var index2 = uriPathInfo.LastIndexOf('/');

            string gs1primaryKeyValue;
            var gs1primaryKey = uriPathInfo[..index1];
            var base64segment = uriPathInfo[(index2 + 1) ..];

            gs1primaryKeyValue = uriPathInfo[(index1 + 1) ..index2];
            objGs1 = DecompressBinaryToGs1AIs(base64segment.Safe64ToBinary());

            if (_regexAllNum.IsMatch(gs1primaryKey)) {
                objGs1.gs1AIs[gs1primaryKey] = gs1primaryKeyValue;
            }
            else {
                if (_shortNameToNumeric.TryGetValue(gs1primaryKey, out string? value)) {
                    objGs1.gs1AIs[value] = gs1primaryKeyValue;
                }
            }
        }

        // filter non-GS1 analyticsKeys
        var keysToRemove = new List<string>();

        foreach (var key in objGs1.gs1AIs.Keys.ToList()) {
            // if key is not all-numeric and not in _shortNameToNumeric, move it to nonGS1
            if (!_regexAllNum.IsMatch(key) && !_shortNameToNumeric.ContainsKey(key)) {
                nonGS1queryStringCandidates[key] = objGs1.gs1AIs[key];
                keysToRemove.Add(key);
            }
        }

        foreach (var keyToRemove in keysToRemove) {
            objGs1.gs1AIs.Remove(keyToRemove);
        }

        foreach (var key in objGs1.nonGs1KeyValuePairs.Keys) {
            nonGS1queryStringCandidates[key] = objGs1.nonGs1KeyValuePairs[key];
        }

        var returnedQueryString = queryStringOther.ToString();

        if (returnedQueryString.Length > 0) {
            returnedQueryString = returnedQueryString[1..];
        }

        return new ExtractedData(
            objGs1.gs1AIs,
            nonGS1queryStringCandidates,
            returnedQueryString,
            fragmentSpecifier);
    }

    /// <summary>
    /// Decompresses the binary string to GS1 Application Identifiers.
    /// </summary>
    /// <param name="binstr">The binary string.</param>
    /// <returns>A dictionary of GS1 AIs.</returns>
    /// <exception cref="Exception">Binary string processing errors.</exception>
    private static (Dictionary<string, string> gsAIs, Dictionary<string, string> nonGs1KeyValuePairs) DecompressBinaryToGs1AIs(string binstr) {
        var totallength = binstr.Length;
        var cursor = 0;
        Dictionary<string, string> gs1AIs = [];
        Dictionary<string, string> nonGs1KeyValuePairs = [];

        while ((totallength - cursor) > 8) {
            var firstHexChar = binstr.Substring(cursor, 4).BinStrToHex();
            var secondHexChar = binstr.Substring(cursor + 4, 4).BinStrToHex();
            var thirdHexChar = string.Empty;
            string fourthHexChar;
            var ai = string.Empty;

            var hexValue = firstHexChar + secondHexChar;
            cursor += 8;

            var firstDigit = Convert.ToInt32(firstHexChar, 16);
            var secondDigit = Convert.ToInt32(secondHexChar, 16);

            // Check if hexValue is within the range 00-99
            if ((firstDigit >= 0) && (secondDigit >= 0) && (firstDigit <= 9) && (secondDigit <= 9)) {
                // Check if hexValue has entry in table P
                if (_prefixLengthTable.TryGetValue(hexValue, out int prefixLength)) {
                    if (prefixLength == 2) {
                        ai = hexValue;
                    }

                    if ((prefixLength == 3) || (prefixLength == 4)) {
                        thirdHexChar = binstr.Substring(cursor, 4).BinStrToHex();
                        cursor += 4;
                        var thirdDigit = Convert.ToInt32(thirdHexChar, 16);

                        if (thirdDigit > 9) {
                            throw new Exception("GS1 Application Identifier keys should be all-numeric; " + hexValue + thirdHexChar + " is not all-numeric.");
                        }

                        ai = hexValue + thirdHexChar;
                    }

                    if (prefixLength == 4) {
                        fourthHexChar = binstr.Substring(cursor, 4).BinStrToHex();
                        cursor += 4;
                        ai = hexValue + thirdHexChar + fourthHexChar;
                        var fourthDigit = Convert.ToInt32(fourthHexChar, 16);

                        if (fourthDigit > 9) {
                            throw new Exception("GS1 Application Identifier keys should be all-numeric; " + hexValue + thirdHexChar + fourthHexChar + " is not all-numeric.");
                        }
                    }

                    var temporaryDecoding = DecodeBinaryValue(ai, gs1AIs ?? [], nonGs1KeyValuePairs ?? [], binstr, cursor);
                    gs1AIs = temporaryDecoding.Gs1AIs ?? [];
                    nonGs1KeyValuePairs = temporaryDecoding.NonGs1KeyValuePairs ?? [];
                    cursor = (int)temporaryDecoding.Cursor;
                }
                else {
                    throw new Exception("Fail: Unsupported AI (reserved range) - no entry in tableP; h1h2=" + hexValue);
                }
            }
            else {
                // HexValue is outside 00-99, using some hex characters
                if (_optimisationsTable.ContainsKey(hexValue)) {
                    string[] optSequence = [.. _optimisationsTable[hexValue]];

                    foreach (var aiKey in optSequence) {
                        var temporaryDecoding = DecodeBinaryValue(aiKey, gs1AIs ?? [], nonGs1KeyValuePairs ?? [], binstr, cursor);
                        gs1AIs = temporaryDecoding.Gs1AIs ?? [];
                        nonGs1KeyValuePairs = temporaryDecoding.NonGs1KeyValuePairs ?? [];
                        cursor = (int)temporaryDecoding.Cursor;
                    }
                }
                else {
                    if (firstHexChar == "F") {
                        // Decompression of non-GS1 key=requiredAi parameters
                        // following logic is as in JS code
                        var keyLength = Convert.ToInt32(binstr.Substring(cursor - 4, 7), 2);
                        cursor += 3;
                        var keyBits = binstr.Substring(cursor, 6 * keyLength);
                        cursor += 6 * keyLength;
                        var key = string.Empty;

                        for (var idx = 0; idx < keyLength; idx++) {
                            var index = Convert.ToInt32(keyBits.Substring(6 * idx, 6), 2);
                            key += SafeBase64Alphabet[index];
                        }

                        var encodingBits = binstr.Substring(cursor, 3);
                        cursor += 3;
                        var encoding = Convert.ToInt32(encodingBits, 2);
                        var numberOfChars = Convert.ToInt32(binstr.Substring(cursor, 7), 2);
                        cursor += 7;
                        var decodings = encoding.HandleDecodings(
                                                    binstr,
                                                    cursor,
                                                    gs1AIs ?? [],
                                                    nonGs1KeyValuePairs,
                                                    key,
                                                    numberOfChars);
                        gs1AIs = decodings.Gs1AIs ?? [];

                        nonGs1KeyValuePairs = decodings.NonGs1KeyValuePairs ?? nonGs1KeyValuePairs;

                        cursor = decodings.Cursor;
                    }
                    else {
                        throw new Exception("No optimisation defined for hex code hh=" + hexValue);
                    }
                }
            }
        }

        return (gs1AIs, nonGs1KeyValuePairs);
    }

    /// <summary>
    /// Decodes a binary value.
    /// </summary>
    /// <param name="key">A GS1 AI.</param>
    /// <param name="gs1AIs">A dictionary of GS1 AIs.</param>
    /// <param name="nonGs1KeyValuePairs">A dictionary of non-GS1 key-value pairs.</param>
    /// <param name="binstr">The binary value to be decoded.</param>
    /// <param name="cursor">The current cursor position in the binary value.</param>
    /// <returns>A dictionary of decoded values.</returns>
    private static ExtractedData DecodeBinaryValue(
        string key,
        Dictionary<string, string> gs1AIs,
        Dictionary<string, string> nonGs1KeyValuePairs,
        string binstr,
        int cursor) {

        gs1AIs[key] = string.Empty;

        if (_formatTable.ContainsKey(key)) {
            foreach (var format in _formatTable[key]) {
                if (format is ExpectedLength
                    && format.DataType == ExpectedTypes.Numeric
                    && cursor < binstr.Length) {
                        // handle fixed-length numeric component
                        var numberOfValueBits = format.Value.NumberOfValueBits();
                        numberOfValueBits = cursor + numberOfValueBits > binstr.Length
                            ? binstr.Length - cursor
                            : numberOfValueBits;
                        var binaryValue = binstr.Substring(cursor, numberOfValueBits);
                        var binValue = binaryValue.BinaryLiteralToBigInteger().ToString();
                        binValue = binValue.PadStringToLength(format.Value);
                        cursor += numberOfValueBits;
                        if (gs1AIs is not null) {
                            gs1AIs[key] += binValue;
                        }
                }

                if (format is ExpectedMaxLength
                    && format.DataType == ExpectedTypes.Numeric
                    && cursor < binstr.Length) {
                        // handle variable-length numeric component
                        var valueLengthBits = format.Value.NumberOfLengthBits();
                        valueLengthBits = cursor + valueLengthBits > binstr.Length
                            ? binstr.Length - cursor
                            : valueLengthBits;
                        var lengthBits = binstr.Substring(cursor, valueLengthBits);
                        cursor += valueLengthBits;
                        var numDigits = Convert.ToInt32(lengthBits, 2);
                        if (numDigits > 0) {
                            var numBitsForValue = numDigits.NumberOfValueBits();
                            var binaryValue = binstr.Substring(cursor, numBitsForValue);
                            var binValue = binaryValue.BinaryLiteralToBigInteger().ToString();
                            cursor += numBitsForValue;
                            if (gs1AIs is not null) {
                                gs1AIs[key] += binValue;
                            }
                        }
                }

                if (format is ExpectedLength
                    && format.DataType == ExpectedTypes.Alphanumeric
                    && cursor < binstr.Length) {
                        // handle fixed-length alphanumeric component
                        var encoding = Convert.ToInt32(binstr.Substring(cursor, 3), 2);
                        cursor += 3;
                        var decodings = encoding.HandleDecodings(
                                                    binstr,
                                                    cursor,
                                                    gs1AIs ?? [],
                                                    nonGs1KeyValuePairs,
                                                    key,
                                                    format.Value);
                        cursor = decodings.Cursor;
                        gs1AIs = decodings.Gs1AIs ?? [];
                }

                if (format is ExpectedMaxLength
                    && format.DataType == ExpectedTypes.Alphanumeric
                    && cursor < binstr.Length) {
                        // handle variable-length alphanumeric component
                        var encBits = binstr.Substring(cursor, 3);
                        cursor += 3;
                        var valueLengthBitsWidth = format.Value.NumberOfLengthBits();
                        string lengthBits = binstr.Substring(cursor, valueLengthBitsWidth);
                        cursor += valueLengthBitsWidth;
                        var decodings = Convert.ToInt32(encBits, 2)
                                               .HandleDecodings(
                                                    binstr,
                                                    cursor,
                                                    gs1AIs ?? [],
                                                    nonGs1KeyValuePairs,
                                                    key,
                                                    Convert.ToInt32(lengthBits, 2));
                        cursor = decodings.Cursor;
                        gs1AIs = decodings.Gs1AIs ?? [];
                }
            }
        }

        return new ExtractedData(gs1AIs, null, Cursor: cursor);
    }

    /// <summary>
    /// Compresses the GS1 AI array to a binary representation.
    /// </summary>
    /// <param name="gs1AIs">The dictionary of GS1 Application Identifiers.</param>
    /// <param name="useOptimisations">Indicates that optimisations should be used.</param>
    /// <param name="nonGS1keyvaluePairs">Additional non-GS1 key=value parameters.</param>
    /// <returns>A binary representation of a compressed GS1 Digital LInk URI.</returns>
    private static string CompressGs1AIsToBinary(
        Dictionary<string, string> gs1AIs,
        bool useOptimisations,
        IReadOnlyDictionary<string, string> nonGS1keyvaluePairs) {

        var binstr = string.Empty;

        // Identify candidate optimisations from _optimisationsTable
        var orderedKeys = gs1AIs.Keys.OrderBy(k => k).ToList();
        List<string> optimisations = [];

        if (useOptimisations) {
            Dictionary<string, int> candidatesFromTableOpt;

            do {
                candidatesFromTableOpt = FindCandidatesFromTableOpt(orderedKeys, _optimisationsTable);

                // Pick candidatesFromTableOpt that can save the highest number of AI key characters
                var bestCandidate = FindBestOptimisationCandidate(candidatesFromTableOpt);

                if (bestCandidate != string.Empty) {
                    orderedKeys = RemoveOptimisedKeysFromAIlist(orderedKeys, _optimisationsTable[bestCandidate]);
                    optimisations.Add(bestCandidate);
                }

                candidatesFromTableOpt = FindCandidatesFromTableOpt(orderedKeys, _optimisationsTable);
            } while (candidatesFromTableOpt.Count > 0);
        }

        // Encode binary string for any optimised values from _optimisationsTable first
        foreach (var key in optimisations) {
            binstr += key.BinaryEncodingOfGS1AIKey();
            string[] optArray = [.. _optimisationsTable[key]];

            foreach (var optKey in optArray) {
                binstr += BinaryEncodingOfValue(gs1AIs, optKey);
            }
        }

        // Then append this further by encoding binary string values for any other
        // AI key=requiredAi parameters for which no optimisations were found.
        foreach (var key in orderedKeys) {
            if (gs1AIs.ContainsKey(key)) {
                binstr += key.BinaryEncodingOfGS1AIKey();
                binstr += BinaryEncodingOfValue(gs1AIs, key);
            }
        }

        // Then if any non-GS1 key=requiredAi parameters were also to be compressed, also
        // compress those and append to the binary string. Note that hex requiredAi F ('1111')
        // is used as a flag (as a reserved requiredAi of firstHexChar) to indicate that
        // what follows is a compressed binary representation of a non-GS1
        // key=requiredAi param. We permit key lengths up to 128 characters only from the
        // URI-safe base64 alphabet (A-Z a-z 0-9 hyphen and underscore).
        if (nonGS1keyvaluePairs.Count > 0) {
            foreach (var kvp in nonGS1keyvaluePairs) {
                var key = kvp.Key;
                var lengthBits = Convert.ToString(key.Length, 2);
                lengthBits = lengthBits.PadStringToLength(7);
                binstr += "1111"; // 'F' flag for foreign analyticsKeys
                binstr += lengthBits;

                StringBuilder binKey = new ();

                for (var idx = 0; idx < key.Length; idx++) {
                    var binChar = Convert.ToString(SafeBase64Alphabet.IndexOf(key[idx]), 2)
                                         .PadStringToLength(6);
                    binKey.Append(binChar);
                }

                binstr += binKey.ToString(); // Key encoded in binary using 6-bit per character

                // After encoding the binary key, encode the corresponding requiredAi,
                // using optimisations where possible.
                binstr += BinaryEncodingOfNonGS1Value(kvp.Value);
            }
        }

        return binstr;

        Dictionary<string, int> FindCandidatesFromTableOpt(List<string> orderedKeys, OptimisationsTable tableOpt) {
            Dictionary<string, int> candidatesFromTableOpt = [];

            foreach (var idx in tableOpt.Keys) {
                string[] optArray = [.. tableOpt[idx]];
                var optFound = true;

                foreach (var opt in optArray) {
                    if (!orderedKeys.Contains(opt)) {
                        optFound = false;
                        break;
                    }
                }

                if (optFound) {
                    // Sum of lengths of all analyticsKeys joined
                    var length = string.Join(string.Empty, tableOpt[idx]).Length;
                    candidatesFromTableOpt[idx] = length;
                }
            }

            return candidatesFromTableOpt;
        }

        string FindBestOptimisationCandidate(Dictionary<string, int> candidatesFromTableOpt) {
            var maxkey = string.Empty;
            var max = 0;

            foreach (var kvp in candidatesFromTableOpt) {
                if (kvp.Value > max) {
                    maxkey = kvp.Key;
                    max = kvp.Value;
                }
            }

            return maxkey;
        }

        List<string> RemoveOptimisedKeysFromAIlist(List<string> akeysa, IList<string> v) {
            foreach (var val in v) {
                var ind = akeysa.IndexOf(val);

                if (ind > -1) {
                    akeysa.RemoveAt(ind);
                }
            }

            return akeysa;
        }
    }

    /// <summary>
    /// Returns the binary encoding of a GS1 AI value.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 AIs.</param>
    /// <param name="key">An AI.</param>
    /// <returns>The binary encoding of a GS1 AI value.</returns>
    private static string BinaryEncodingOfValue(Dictionary<string, string> gs1AIs, string key) {
        var binstr = string.Empty;

        if (_formatTable.ContainsKey(key)) {
            var cursor = 0;
            var value = gs1AIs[key];

            foreach (var format in _formatTable[key]) {

                if (format is ExpectedLength
                    && format.DataType == ExpectedTypes.Numeric) {
                    // Handle fixed-length numeric component
                    var fixedLength = Convert.ToInt32(format.Value);
                    var charstr = value.Substring(cursor, fixedLength);
                    var binValue = charstr.DecimalLiteralToBinaryLiteral()
                                          .PadStringToLength(fixedLength.NumberOfValueBits());
                    cursor += fixedLength;
                    binstr += binValue;
                }

                if (format is ExpectedMaxLength
                    && format.DataType == ExpectedTypes.Numeric) {
                    // Handle variable-length numeric component
                    var charStr = value[cursor..];
                    var lengthBits = Convert.ToString(charStr.Length, 2)
                                            .PadStringToLength(Convert.ToInt32(format.Value)
                                            .NumberOfLengthBits());
                    var binValue = charStr.DecimalLiteralToBinaryLiteral()
                                          .PadStringToLength(charStr.Length.NumberOfValueBits());
                    cursor += charStr.Length;
                    binstr += lengthBits + binValue;
                }

                if (format is ExpectedLength
                    && format.DataType == ExpectedTypes.Alphanumeric) {
                    // Handle fixed-length alphanumeric component
                    var length = Convert.ToInt32(format.Value);
                    var charstr = value.Substring(cursor, length);
                    cursor += length;
                    binstr = charstr.DetermineEncoding()
                                    .HandleEncodings(string.Empty, charstr, binstr);
                }

                if (format is ExpectedMaxLength
                    && format.DataType == ExpectedTypes.Alphanumeric) {
                    // Handle variable-length alphanumeric component
                    var charstr = value[cursor..];
                    var lengthBits = Convert.ToString(charstr.Length, 2)
                                            .PadStringToLength(Convert.ToInt32(format.Value)
                                            .NumberOfLengthBits());
                    cursor += charstr.Length;
                    binstr = charstr.DetermineEncoding()
                                    .HandleEncodings(lengthBits, charstr, binstr);
                }
            }
        }

        return binstr;
    }

    /// <summary>
    /// Encodes a non-GS1 parameter value.
    /// </summary>
    /// <param name="charstr">The non-GS1 parameter value.</param>
    /// <returns>A binary encoding of the parameter value.</returns>
    private static string BinaryEncodingOfNonGS1Value(string charstr) {
        var lengthBits = Convert.ToString(charstr.Length, 2);
        lengthBits = lengthBits.PadStringToLength(7);
        var enc = charstr.DetermineEncoding();
        var binstr = enc.HandleEncodings(lengthBits, charstr, string.Empty);
        return binstr;
    }

    /// <summary>
    /// Builds key compressed Digital Link.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 Application Identifiers.</param>
    /// <param name="uriStem">The URI stem for the Digital Link. If omitted, the library will use https://id.gs1.org.</param>
    /// <param name="useOptimisations">If true, the GS1 Digital Link is built with additional optimisations, if available.</param>
    /// <param name="nonGs1KeyValuePairs">Any additional non-GS1 name/value parameters to be included in the query string.</param>
    /// <param name="compressNonGs1KeyValuePairs">If true, non-GS1 key=value pairs are compressed.</param>
    /// <returns>A compressed representation of a GS1 Digital Link.</returns>
    private static string BuildCompressedGS1digitalLink(
        Dictionary<string, string> gs1AIs,
        string? uriStem,
        bool useOptimisations,
        IReadOnlyDictionary<string, string> nonGs1KeyValuePairs,
        bool compressNonGs1KeyValuePairs) {

        // Minimal translation of logic
        var queryString = string.Empty;

        if (!compressNonGs1KeyValuePairs) {
            // Pass-through of query string params
            List<string> additionalKvps = [];

            foreach (var kvp in nonGs1KeyValuePairs) {
                additionalKvps.Add(kvp.Key + "=" + kvp.Value);
            }

            if (additionalKvps.Count > 0) {
                queryString = "?" + string.Join("&", additionalKvps);
            }
        }

        // Get the normalised URI stem or the default (canonical) URI stem, if no stem is provided.
        uriStem = uriStem.GetNormalisedUriStem();

        var path = "/" + CompressGs1AIsToBinary(
            gs1AIs,
            useOptimisations,
            compressNonGs1KeyValuePairs ? nonGs1KeyValuePairs : new Dictionary<string, string>()).BinaryToSafe64();

        return uriStem + path + queryString;
    }

    /// <summary>
    /// Separates GS1 AI identifiers and non-identifiers.
    /// </summary>
    /// <param name="gs1AIs">A dictionary of GS1 AIs.</param>
    /// <returns>A dictionary of two AI dictionaries.</returns>
    private static Dictionary<string, Dictionary<string, string>> SeparateIdNonId(Dictionary<string, string> gs1AIs) {
        Dictionary<string, Dictionary<string, string>> returnValue = [];
        Dictionary<string, string> idArray = [];
        Dictionary<string, string> nonIDarray = [];

        foreach (var key in gs1AIs.Keys) {
            if (_aiMaps.Identifiers.Contains(key)) {
                idArray[key] = gs1AIs[key];
            }
            else {
                nonIDarray[key] = gs1AIs[key];
            }
        }

        returnValue["ID"] = idArray;
        returnValue["nonID"] = nonIDarray;
        return returnValue;
    }

    /// <summary>
    /// Regex that detects the presence of an initial parenthesised AI in a URI.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\((\d{2,4}?)\)")]
    private static partial Regex RegexInitialParenthesisedAiDetector();

    /// <summary>
    /// Regex that detects the presence of an AIM identifier in a URI.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^(?<aim>][A-Za-z][0-9A-Za-z])?(?<input>.*)$")]
    private static partial Regex RegexAimIdentifierDetector();
}