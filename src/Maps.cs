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

using Newtonsoft.Json;

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// A set of GS1 Application Identifiers categories.
/// </summary>
/// <param name="Identifiers">A list of GS1 Application identifiers categorised as Digital Link identifiers.</param>
/// <param name="Qualifiers">A list of GS1 Application identifiers categorised as Digital Link qualifiers.</param>
/// <param name="DataAttributes">A list of GS1 Application identifiers categorised as Digital Link data attributes.</param>
/// <param name="Fnc1Elements">A list of GS1 Application identifiers categorised as 'FNC1' elements. These must be delimited using ASCII 29 in element strings, unless they appear at the end of the element string</param>
/// <param name="PredefinedLengthElements">A list of GS1 Application identifiers categorised as non-'FNC1' elements. These should never be delimited using ASCII 29 in element strings.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "This is a primary constructor.")]
public record Maps(
    [JsonProperty("identifiers")]
    IReadOnlyList<string> Identifiers,
    [JsonProperty("qualifiers")]
    IReadOnlyList<string> Qualifiers,
    [JsonProperty("dataAttributes")]
    IReadOnlyList<string> DataAttributes,
    [JsonProperty("fnc1Elements")]
    IReadOnlyList<string> Fnc1Elements,
    [JsonProperty("predefinedLengthElements")]
    IReadOnlyList<string> PredefinedLengthElements) {
}