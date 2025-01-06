// --------------------------------------------------------------------------
// <copyright file="StructuredOutput.cs" company="Solidsoft Reply Ltd.">
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

using Newtonsoft.Json;

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// Lists of GS1 AIs reflecting their role in Digital Links.
/// </summary>
public record StructuredOutput {

    /// <summary>
    /// Gets a list of AIs recognised as Digital Link key identifiers.
    /// </summary>
    [JsonProperty("identifiers")]
    public IReadOnlyCollection<IReadOnlyDictionary<string, string>> Identifiers { get; init; } = new List<Dictionary<string, string>>();

    /// <summary>
    /// Gets a list of AIs recognised as Digital Link qualifiers.
    /// </summary>
    [JsonProperty("qualifiers")]
    public IReadOnlyCollection<IReadOnlyDictionary<string, string>> Qualifiers { get; init; } = new List<Dictionary<string, string>>();

    /// <summary>
    /// Gets a list of AIs recognised as Digital Link data attributes.
    /// </summary>
    [JsonProperty("dataAttributes")]
    public IReadOnlyCollection<IReadOnlyDictionary<string, string>> DataAttributes { get; init; } = new List<Dictionary<string, string>>();

    /// <summary>
    /// Gets a list of unrecognised AIs.
    /// </summary>
    [JsonProperty("other")]
    public IReadOnlyCollection<IReadOnlyDictionary<string, string>> Other { get; init; } = new List<Dictionary<string, string>>();
}