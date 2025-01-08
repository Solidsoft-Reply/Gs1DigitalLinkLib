// --------------------------------------------------------------------------
// <copyright file="Maps.cs" company="Solidsoft Reply Ltd.">
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
// A set of GS1 Application Identifiers categories.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Newtonsoft.Json;

using System.Text.Json.Serialization;

/// <summary>
/// A set of GS1 Application Identifiers categories.
/// </summary>
public record Maps() {

    /* Cannot use a primary constructor here because the Microsoft JSON serialiser does not support property
     * name attributes on constructor parameters (NewtonSoft.Json does) - January 2025.
     */

    /// <summary>
    ///  Initializes a new instance of the <see cref="Maps"/> class.
    /// </summary>
    /// <param name="identifiers">A list of GS1 Application identifiers categorised as Digital Link identifiers.</param>
    /// <param name="qualifiers">A list of GS1 Application identifiers categorised as Digital Link qualifiers.</param>
    /// <param name="dataAttributes">A list of GS1 Application identifiers categorised as Digital Link data attributes.</param>
    /// <param name="fnc1Elements">A list of GS1 Application identifiers categorised as 'FNC1' elements. These must be delimited using ASCII 29 in element strings, unless they appear at the end of the element string.</param>
    /// <param name="predefinedLengthElements">A list of GS1 Application identifiers categorised as non-'FNC1' elements. These should never be delimited using ASCII 29 in element strings.</param>
    internal Maps(
        IReadOnlyList<string> identifiers,
        IReadOnlyList<string> qualifiers,
        IReadOnlyList<string> dataAttributes,
        IReadOnlyList<string> fnc1Elements,
        IReadOnlyList<string> predefinedLengthElements)
        : this() {
            Identifiers = identifiers;
            Qualifiers = qualifiers;
            DataAttributes = dataAttributes;
            Fnc1Elements = fnc1Elements;
            PredefinedLengthElements = predefinedLengthElements;
    }

    /// <summary>
    /// Gets a list of GS1 Application identifiers categorised as Digital Link identifiers.
    /// </summary>
    [JsonProperty("identifiers")]
    [JsonPropertyName("identifiers")]
    public IReadOnlyList<string> Identifiers { get; init; } = [];

    /// <summary>
    /// Gets a list of GS1 Application identifiers categorised as Digital Link qualifiers.
    /// </summary>
    [JsonProperty("qualifiers")]
    [JsonPropertyName("qualifiers")]
    public IReadOnlyList<string> Qualifiers { get; init; } = [];

    /// <summary>
    /// Gets a list of GS1 Application identifiers categorised as Digital Link data attributes.
    /// </summary>
    [JsonProperty("dataAttributes")]
    [JsonPropertyName("dataAttributes")]
    public IReadOnlyList<string> DataAttributes { get; init; } = [];

    /// <summary>
    /// Gets a list of GS1 Application identifiers categorised as 'FNC1' elements. These must be delimited using ASCII 29 in element strings, unless they appear at the end of the element string.
    /// </summary>
    [JsonProperty("fnc1Elements")]
    [JsonPropertyName("fnc1Elements")]
#pragma warning disable VSSpell001 // Spell Check
    public IReadOnlyList<string> Fnc1Elements { get; init; } = [];
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Gets a list of GS1 Application identifiers categorised as non-'FNC1' elements. These should never be delimited using ASCII 29 in element strings.
    /// </summary>
    [JsonProperty("predefinedLengthElements")]
    [JsonPropertyName("predefinedLengthElements")]
    public IReadOnlyList<string> PredefinedLengthElements { get; init; } = [];

    /// <summary>
    /// Returns the Maps as JSON.
    /// </summary>
    /// <returns>The Maps as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(this);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the Maps as JSON.
    /// </summary>
    /// <returns>The Maps as JSON.</returns>
    public override string ToString() => ToJson();
}