// --------------------------------------------------------------------------
// <copyright file="AiTableEntry.cs" company="Solidsoft Reply Ltd.">
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
// Represents an AI Table entry.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Text.Json.Serialization;

using Newtonsoft.Json;

/// <summary>
/// Represents an AI Table entry.
/// </summary>
public record AiTableEntry() {

    /* Cannot use a primary constructor here because the Microsoft JSON serialiser does not support property
     * name attributes on constructor parameters (NewtonSoft.Json does) - January 2025.
     */

    /// <summary>
    /// Initializes a new instance of the <see cref="AiTableEntry"/> class.
    /// </summary>
    /// <param name="description">The AI description.</param>
    /// <param name="ai">The AI.</param>
    /// <param name="format">Format specifier for the AI.</param>
    /// <param name="type">The AI type, in the context of a Digital Link.</param>
    /// <param name="predefinedLength">A value indicating whether the AI has a predefined length.</param>
    /// <param name="regex">A regular expression for validating the AI value.</param>
    /// <param name="title">The AI data title.</param>
    /// <param name="shortName">The short name for the AI (legacy feature).</param>
    /// <param name="checkDigitPosition">The position of the check digit.</param>
    /// <param name="qualifiers">A list of qualifier AIs (applies only to identifier AIs).</param>
    internal AiTableEntry(
        string description,
        string ai,
        string format,
        AiTypes type,
        bool predefinedLength,
        string regex,
        string? title = null,
        string? shortName = null,
        CheckDigitPosition? checkDigitPosition = null,
        List<string>? qualifiers = null)
        : this() {
            Description = description;
            Ai = ai;
            Format = format;
            Type = type;
            PredefinedLength = predefinedLength;
            Regex = regex;
            Title = title;
            ShortName = shortName;
            CheckDigitPosition = checkDigitPosition;
            Qualifiers = qualifiers;
    }

    /// <summary>
    ///   Gets the AI description.
    /// </summary>
    [JsonPropertyName("description")]
    [JsonProperty("description")]
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the AI.
    /// </summary>
    [JsonPropertyName("ai")]
    [JsonProperty("ai")]
    public string Ai { get; init; } = string.Empty;

    /// <summary>
    /// Gets the format specifier for the AI.
    /// </summary>
    [JsonPropertyName("format")]
    [JsonProperty("format")]
    public string Format { get; init; } = string.Empty;

    /// <summary>
    /// Gets the AI type, in the context of a Digital Link.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonProperty("type")]
    public AiTypes Type { get; init; } = AiTypes.Identifier;

    /// <summary>
    /// Gets a value indicating whether the AI has a predefined length.
    /// </summary>
    [JsonPropertyName("predefinedLength")]
    [JsonProperty("predefinedLength")]
    public bool PredefinedLength { get; init; } = false;

    /// <summary>
    /// Gets a regular expression for validating the AI value.
    /// </summary>
    [JsonPropertyName("regex")]
    [JsonProperty("regex")]
    public string Regex { get; init; } = string.Empty;

    /// <summary>
    /// Gets the AI data title.
    /// </summary>
    [JsonPropertyName("title")]
    [JsonProperty("title")]
    public string? Title { get; init; } = null;

    /// <summary>
    /// Gets the short name for the AI (legacy feature).
    /// </summary>
    [JsonPropertyName("shortName")]
    [JsonProperty("shortName")]
    public string? ShortName { get; init; } = null;

    /// <summary>
    /// Gets the position of the check digit.
    /// </summary>
    [JsonPropertyName("checkDigitPosition")]
    [JsonProperty("checkDigitPosition")]
    public CheckDigitPosition? CheckDigitPosition { get; init; } = null;

    /// <summary>
    /// Gets a list of qualifier AIs (applies only to identifier AIs).
    /// </summary>
    [JsonPropertyName("qualifiers")]
    [JsonProperty("qualifiers")]
    public List<string>? Qualifiers { get; init; } = null;
}