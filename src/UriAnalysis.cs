// --------------------------------------------------------------------------
// <copyright file="UriAnalysis.cs" company="Solidsoft Reply Ltd.">
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
// The result of analysis of a Digital Link URI.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Newtonsoft.Json;

using System.Text.Json.Serialization;

/// <summary>
/// The result of analysis of a Digital Link URI.
/// </summary>
public record UriAnalysis {

    /// <summary>
    /// Gets the URI stem.
    /// </summary>
    [JsonProperty("uriStem")]
    [JsonPropertyName("uriStem")]
    public string UriStem { get; init; } = string.Empty;

    /// <summary>
    /// Gets the URI scheme.
    /// </summary>
    [JsonProperty("scheme")]
    [JsonPropertyName("scheme")]
    public string Scheme { get; init; } = string.Empty;

    /// <summary>
    /// Gets the URI hostname.
    /// </summary>
    [JsonProperty("hostname")]
    [JsonPropertyName("hostname")]
#pragma warning disable VSSpell001 // Spell Check
    public string Hostname { get; init; } = string.Empty;
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Gets the URI path components.
    /// </summary>
    [JsonProperty("pathComponents")]
    [JsonPropertyName("pathComponents")]
    public string PathComponents { get; init; } = string.Empty;

    /// <summary>
    /// Gets the URI path info.
    /// </summary>
    [JsonProperty("uriPathInfo")]
    [JsonPropertyName("uriPathInfo")]
    public string UriPathInfo { get; init; } = string.Empty;

    /// <summary>
    /// Gets a dictionary of path candidates.
    /// </summary>
    [JsonProperty("pathCandidates")]
    [JsonPropertyName("pathCandidates")]
    public IReadOnlyDictionary<string, string> PathCandidates { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets the URI query string, if present.
    /// </summary>
    [JsonProperty("queryString")]
    [JsonPropertyName("queryString")]
    public string QueryString { get; init; } = string.Empty;

    /// <summary>
    /// Gets a dictionary of query string key=value pairs.
    /// </summary>
    [JsonProperty("queryStringGs1Pairs")]
    [JsonPropertyName("queryStringGs1Pairs")]
    public IReadOnlyDictionary<string, string> QueryStringGs1Pairs { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets a dictionary of non-GS1 query string key=value pairs.
    /// </summary>
    [JsonProperty("queryStringNonGs1Pairs")]
    [JsonPropertyName("queryStringNonGs1Pairs")]
    public IReadOnlyDictionary<string, string> QueryStringNonGs1Pairs { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets additional non-key=value pair query string parameters.
    /// </summary>
    [JsonProperty("otherQueryContent")]
    [JsonPropertyName("otherQueryContent")]
    public string OtherQueryContent { get; init; } = string.Empty;

    /// <summary>
    /// Gets the URI fragment specifier, if present.
    /// </summary>
    [JsonProperty("fragment")]
    [JsonPropertyName("fragment")]
    public string Fragment { get; init; } = string.Empty;

    /// <summary>
    /// Gets the detected form of the Digital Link URI (uncompressed, partially compressed, compressed).
    /// </summary>
    [JsonProperty("detectedForm")]
    [JsonPropertyName("detectedForm")]
    public DigitalLinkForm DetectedForm { get; init; } = DigitalLinkForm.Unknown;

    /// <summary>
    /// Gets the uncompressed path, if present.
    /// </summary>
    [JsonProperty("uncompressedPath")]
    [JsonPropertyName("uncompressedPath")]
    public string UncompressedPath { get; init; } = string.Empty;

    /// <summary>
    /// Gets the compressed path, if present.
    /// </summary>
    [JsonProperty("compressedPath")]
    [JsonPropertyName("compressedPath")]
    public string CompressedPath { get; init; } = string.Empty;

   /// <summary>
    /// Gets the dictionary of identifier AI keys and values contained in the Digital Link..
    /// </summary>
    [JsonProperty("identifierMap")]
    [JsonPropertyName("identifierMap")]
    public IReadOnlyDictionary<string, string> IdentifierMap { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets the primary identifier of the Digital Link.
    /// </summary>
    [JsonProperty("primaryIdentifier")]
    [JsonPropertyName("primaryIdentifier")]
    public string PrimaryIdentifier { get; init; } = string.Empty;

    /// <summary>
    /// Gets the dictionary of non-identifier AI keys and values contained in the Digital Link.
    /// </summary>
    [JsonProperty("nonIdMap")]
    [JsonPropertyName("nonIdMap")]
    public IReadOnlyDictionary<string, string> NonIdMap { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets a structured representation of the GS1 elements in the Digital Link.
    /// </summary>
    [JsonProperty("structuredData")]
    [JsonPropertyName("structuredData")]
    public StructuredData StructuredData { get; init; } = new StructuredData();

    /// <summary>
    /// Gets an element string representing the Digital Link.
    /// </summary>
    /// <remarks>
    /// Element strings are presented using parenthesis to delimit each GS1 AI. These strings may be useful when
    /// generating barcodes using common barcode creation libraries such as ZXing or Zint.
    /// </remarks>
    [JsonProperty("elementStringOutput")]
    [JsonPropertyName("elementStringOutput")]
    public string ElementStringOutput { get; init; } = string.Empty;

    /// <summary>
    /// Returns the analytics data as JSON.
    /// </summary>
    /// <returns>The analytics data as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(this);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the analytics data as JSON.
    /// </summary>
    /// <returns>The analytics data as JSON.</returns>
    public override string ToString() => ToJson();
}