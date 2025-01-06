// --------------------------------------------------------------------------
// <copyright file="Gs1Data.cs" company="Solidsoft Reply Ltd.">
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

using Newtonsoft.Json;

using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents the data extracted from a GS1 Digital Link URI.
/// </summary>
public class Gs1Data {

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1Data"/> class.
    /// </summary>
    public Gs1Data() {
        Gs1AIs = [];
        NonGs1KeyValuePairs = [];
        OtherQueryStringContent = string.Empty;
        FragmentSpecifier = string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1Data"/> class.
    /// </summary>
    /// <param name="gsAIs">A dictionary of GS1 AIs and values.</param>
    /// <param name="nonGs1KeyValuePairs">A dictionary of non-GS1 key-value parameters for the query string.</param>
    /// <param name="otherQueryStringContent">Other non key-value pair content for the query string.</param>
    /// <param name="fragmentSpecifier">A fragment specifier.</param>
    public Gs1Data(
        Dictionary<string, string> gsAIs,
        Dictionary<string, string>? nonGs1KeyValuePairs = null,
        string? otherQueryStringContent = null,
        string? fragmentSpecifier = null) {
            Gs1AIs = gsAIs;
            NonGs1KeyValuePairs = nonGs1KeyValuePairs ?? [];
            OtherQueryStringContent = otherQueryStringContent ?? string.Empty;
            FragmentSpecifier = fragmentSpecifier ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1Data"/> class.
    /// </summary>
    /// <param name="extractedData">Internal extracted data.</param>
    internal Gs1Data(ExtractedData extractedData) {
        // We clone the dictionaries as a safety/future-proofing measure.
        Gs1AIs = extractedData.Gs1AIs != null ? new Dictionary<string, string>(extractedData.Gs1AIs) : [];
        NonGs1KeyValuePairs = extractedData.NonGs1KeyValuePairs != null ? new Dictionary<string, string>(extractedData.NonGs1KeyValuePairs) : [];
        OtherQueryStringContent = extractedData.OtherQueryStringContent;
        FragmentSpecifier = extractedData.FragmentSpecifier;
    }

    /// <summary>
    /// Gets the GS1 AIs and values.
    /// </summary>
    [JsonProperty("gs1AIs")]
    public Dictionary<string, string> Gs1AIs { get; init; }

    /// <summary>
    /// Gets the non-GS1 key-value parameters included in the query string.
    /// </summary>
    [JsonProperty("nonGs1KeyValuePairs")]
    public Dictionary<string, string> NonGs1KeyValuePairs { get; init; }

    /// <summary>
    /// Gets other non key-value content for the query string.
    /// </summary>
    [JsonProperty("otherQueryStringContent")]
    public string OtherQueryStringContent { get; init; }

    /// <summary>
    /// Gets the fragment specifier.
    /// </summary>
    [JsonProperty("fragmentSpecifier")]
    public string FragmentSpecifier { get; init; }
}