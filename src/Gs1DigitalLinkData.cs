// --------------------------------------------------------------------------
// <copyright file="Gs1DigitalLinkData.cs" company="Solidsoft Reply Ltd.">
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
// Represents the data extracted from a GS1 Digital Link URI.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the data extracted from a GS1 Digital Link URI.
/// </summary>
public class Gs1DigitalLinkData {

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    public Gs1DigitalLinkData() {
        Gs1AIs = new Dictionary<string, string>();
        NonGs1KeyValuePairs = new Dictionary<string, string>();
        OtherQueryStringContent = string.Empty;
        FragmentSpecifier = string.Empty;
        UriStem = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    /// <param name="gsAIs">A dictionary of GS1 AIs and values.</param>
    /// <param name="nonGs1KeyValuePairs">A dictionary of non-GS1 key-value parameters for the query string.</param>
    /// <param name="otherQueryStringContent">Other non key-value pair content for the query string.</param>
    /// <param name="fragmentSpecifier">A fragment specifier.</param>
    /// <param name="uriStem">The URI stem.</param>
    /// <param name="structuredData">A structured representation of the GS1 elements in the Digital Link.</param>
    public Gs1DigitalLinkData(
        IReadOnlyDictionary<string, string> gsAIs,
        IReadOnlyDictionary<string, string>? nonGs1KeyValuePairs = null,
        string? otherQueryStringContent = null,
        string? fragmentSpecifier = null,
        string? uriStem = null,
        StructuredData? structuredData = null) {
            Gs1AIs = gsAIs;
            NonGs1KeyValuePairs = nonGs1KeyValuePairs ?? new Dictionary<string, string>();
            OtherQueryStringContent = otherQueryStringContent ?? string.Empty;
            FragmentSpecifier = fragmentSpecifier ?? string.Empty;
            UriStem = uriStem ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkUriOrElementString">A GS1 Digital Link URI or element string.</param>
    /// <param name="noValidation">
    /// If true, and the provided data is a GS1 Digital Link element string, the element string is not validated.
    /// The data values may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 Digital Link URI or element string is invalid.</exception>
    public Gs1DigitalLinkData(string gs1DigitalLinkUriOrElementString, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null) {
        Gs1DigitalLinkData innerData;

        try {
            innerData = Gs1DigitalLinkConvert.FromGs1DigitalLinkToData(gs1DigitalLinkUriOrElementString);
        }
        catch (Gs1DigitalLinkException gs1DlEx) {
            try {
                innerData = Gs1DigitalLinkConvert.FromGs1ElementStringToDigitalLinkData(new Gs1ElementString(gs1DigitalLinkUriOrElementString), noValidation);
            }
            catch {
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, nameof(Gs1DigitalLinkData), nameof(gs1DigitalLinkUriOrElementString));
                throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, gs1DlEx.Message, logger: logger);
            }
        }

        Gs1AIs = innerData.Gs1AIs;
        NonGs1KeyValuePairs = innerData.NonGs1KeyValuePairs;
        OtherQueryStringContent = innerData.OtherQueryStringContent;
        FragmentSpecifier = innerData.FragmentSpecifier;
        UriStem = innerData.UriStem;
        StructuredData = innerData.StructuredData;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">A GS1 Digital Link URI.</param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 Digital Link URI is invalid.</exception>
    public Gs1DigitalLinkData(Uri gs1DigitalLinkUri, ILogger<Gs1DigitalLink>? logger = null)
                : this(gs1DigitalLinkUri.ToString(), logger: logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    /// <param name="gs1DigitalLink">A GS1 Digital Link.</param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 Digital Link is invalid.</exception>
    public Gs1DigitalLinkData(Gs1DigitalLink gs1DigitalLink, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLink.Value, logger: logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    /// <param name="gs1DigitalLink">A GS1 Digital Link.</param>
    /// <param name="noValidation">
    /// If true, and the provided data is a GS1 Digital Link element string, the element string is not validated.
    /// The data values may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 element string is invalid.</exception>
    public Gs1DigitalLinkData(Gs1ElementString gs1DigitalLink, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLink.Value, noValidation, logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkData"/> class.
    /// </summary>
    /// <param name="extractedData">Internal extracted data.</param>
    internal Gs1DigitalLinkData(ExtractedData extractedData) {
        // We clone the dictionaries as a safety/future-proofing measure.
        Gs1AIs = extractedData.gs1DigitalLinkData != null ? new Dictionary<string, string>(extractedData.gs1DigitalLinkData) : [];
        NonGs1KeyValuePairs = extractedData.NonGs1KeyValuePairs != null ? new Dictionary<string, string>(extractedData.NonGs1KeyValuePairs) : [];
        OtherQueryStringContent = extractedData.OtherQueryStringContent;
        FragmentSpecifier = extractedData.FragmentSpecifier;
        UriStem = extractedData.UriStem;
        StructuredData = extractedData.StructuredData ?? new StructuredData();
    }

    /// <summary>
    /// Gets the GS1 AIs and values.
    /// </summary>
    [JsonProperty("gs1DigitalLinkData")]
    [JsonPropertyName("gs1DigitalLinkData")]
    public IReadOnlyDictionary<string, string> Gs1AIs { get; init; }

    /// <summary>
    /// Gets the non-GS1 key-value parameters included in the query string.
    /// </summary>
    [JsonProperty("nonGs1KeyValuePairs")]
    [JsonPropertyName("nonGs1KeyValuePairs")]
    public IReadOnlyDictionary<string, string> NonGs1KeyValuePairs { get; init; }

    /// <summary>
    /// Gets other non key-value content for the query string.
    /// </summary>
    [JsonProperty("otherQueryStringContent")]
    [JsonPropertyName("otherQueryStringContent")]
    public string OtherQueryStringContent { get; init; }

    /// <summary>
    /// Gets the fragment specifier.
    /// </summary>
    [JsonProperty("fragmentSpecifier")]
    [JsonPropertyName("fragmentSpecifier")]
    public string FragmentSpecifier { get; init; }

    /// <summary>
    /// Gets the URI stem.
    /// </summary>
    [JsonProperty("uriStem")]
    [JsonPropertyName("uriStem")]
    public string UriStem { get; init; }

    /// <summary>
    /// Gets a structured representation of the GS1 elements in the Digital Link.
    /// </summary>
    [JsonProperty("structuredData")]
    [JsonPropertyName("structuredData")]
    public StructuredData StructuredData { get; init; } = new StructuredData();

    /// <summary>
    /// Returns the semantics data as JSON.
    /// </summary>
    /// <returns>The semantics data as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(this);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the semantics data as JSON.
    /// </summary>
    /// <returns>The semantics data as JSON.</returns>
    public override string ToString() => ToJson();
}