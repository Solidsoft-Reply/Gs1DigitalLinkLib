// --------------------------------------------------------------------------
// <copyright file="StructuredData.cs" company="Solidsoft Reply Ltd.">
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
// Lists of GS1 AIs reflecting their role in Digital Links.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Text.Json.Serialization;

using Newtonsoft.Json;

/// <summary>
/// Lists of GS1 AIs reflecting their role in Digital Links.
/// </summary>
public record StructuredData {

    /// <summary>
    /// Gets a list of AIs recognised as Digital Link key identifiers.
    /// </summary>
    [JsonProperty("identifiers")]
    [JsonPropertyName("identifiers")]
    public IReadOnlyDictionary<string, string> Identifiers { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets a list of AIs recognised as Digital Link qualifiers.
    /// </summary>
    [JsonProperty("qualifiers")]
    [JsonPropertyName("qualifiers")]
    public IReadOnlyDictionary<string, string> Qualifiers { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets a list of AIs recognised as Digital Link data attributes.
    /// </summary>
    [JsonProperty("dataAttributes")]
    [JsonPropertyName("dataAttributes")]
    public IReadOnlyDictionary<string, string> DataAttributes { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Gets a list of unrecognised AIs.
    /// </summary>
    [JsonProperty("other")]
    [JsonPropertyName("other")]
    public IReadOnlyDictionary<string, string> Other { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Returns the structured data as JSON.
    /// </summary>
    /// <returns>The structured data as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(this);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the structured data as JSON.
    /// </summary>
    /// <returns>The structured data as JSON.</returns>
    public override string ToString() => ToJson();
}