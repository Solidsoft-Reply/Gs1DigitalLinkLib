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

using Newtonsoft.Json;

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// Represents an AI Table entry.
/// </summary>
/// <param name="Description">The AI description.</param>
/// <param name="Ai">The AI.</param>
/// <param name="Format">Format specifier for the AI.</param>
/// <param name="Type">The AI type, in the context of a Digital Link.</param>
/// <param name="PredefinedLength">A value indicating whether the AI has a fixed length.</param>
/// <param name="Regex">A regular expression for validating the AI value.</param>
/// <param name="Title">The AI data title.</param>
/// <param name="ShortName"></param>
/// <param name="CheckDigitPosition"></param>
/// <param name="Qualifiers"></param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "This is a primary constructor.")]
public record AiTableEntry(
    [JsonProperty("description")]
    string Description,
    [JsonProperty("ai")]
    string Ai,
    [JsonProperty("format")]
    string Format,
    [JsonProperty("type")]
    AiTypes Type,
    [JsonProperty("predefinedLength")]
    bool PredefinedLength,
    [JsonProperty("regex")]
    string Regex,
    [JsonProperty("title")]
    string? Title = null,
    [JsonProperty("shortName")]
    string? ShortName = null,
    [JsonProperty("checkDigitPosition")]
    CheckDigitPosition? CheckDigitPosition = null,
    [JsonProperty("qualifiers")]
    List<string>? Qualifiers = null);