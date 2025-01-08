// --------------------------------------------------------------------------
// <copyright file="ShortNamesTable.cs" company="Solidsoft Reply Ltd.">
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
// A table of GS1 Application Identifier short names.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A table of GS1 Application Identifier short names. The table provides short
/// names as substitutes for GS1 AIs.  Short Names are also called 'convenience
/// alphas'. They are a legacy feature which was removed from version 1.1.3 of
/// the GS1 Digital Link standard. Some resolver services may not support short
/// names.
/// </summary>
[Obsolete("This class is obsolete and is retained for legacy purposes only. It may be removed in a future version. Short names ('convenience alphas') were removed from version 1.1.3 of the GS1 Digital Link standard.")]
public class ShortNamesTable : IReadOnlyDictionary<string, string> {

    /// <summary>
    /// The Short Names dictionary.
    /// </summary>
    private static readonly Dictionary<string, string> _shortNames = [];

    /// <summary>
    /// The regular expressions in the Short Names dictionary.
    /// </summary>
    private static readonly IEnumerable<string> _shortNamesValues;

    /// <summary>
    /// Initializes static members of the <see cref="ShortNamesTable"/> class.
    /// </summary>
    static ShortNamesTable() {

        _shortNames = DigitalLinkConvert.AiShortNames;
        _shortNamesValues = from v in _shortNames.Values
                            select v;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortNamesTable"/> class.
    /// </summary>
    private ShortNamesTable() {
    }

    /// <summary>
    /// Gets the number of GS1 Application Identifiers in the table.
    /// </summary>
    public int Count => _shortNames.Count;

    /// <summary>
    /// Gets the collection of GS1 Application Identifiers.
    /// </summary>
    public IEnumerable<string> Keys =>
        _shortNames.Keys;

    /// <summary>
    /// Gets the collection of short names for GS1 Application Identifiers.
    /// </summary>
    public IEnumerable<string> Values =>
        _shortNamesValues;

    /// <summary>
    /// Gets the short name at the specified index.
    /// </summary>
    /// <param name="index">The index of the short name.</param>
    /// <returns>The short name.</returns>
    public string this[string index] => _shortNames[index];

    /// <summary>
    /// Factory method to create a new <see cref="ShortNamesTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="ShortNamesTable"/> instance.</returns>
    public static ShortNamesTable Create() => [];

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() =>
        _shortNames.GetEnumerator();

    /// <summary>
    /// Determines whether the table contains a specified GS1 Application Identifier.
    /// </summary>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <returns>True, if the table contains the GS1 Application Identifier; otherwise false.</returns>
    public bool ContainsKey(string ai) =>
        _shortNames.ContainsKey(ai);

    /// <summary>
    /// Gets the short name associated with the specified GS1 Application Identifier.
    /// </summary>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <param name="shortName">The short name for the GS1 APplication Identifier.</param>
    /// <returns>True, if the GS1 Application Identifier is located in the table; otherwise false.</returns>
    public bool TryGetValue(string ai, [MaybeNullWhen(false)] out string shortName) =>
        _shortNames.TryGetValue(ai, out shortName);

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _shortNames.GetEnumerator();

    /// <summary>
    /// Returns the Short Names table as JSON.
    /// </summary>
    /// <returns>The ShortNames table as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(_shortNames);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the Short Names table as JSON.
    /// </summary>
    /// <returns>The Short Names table as JSON.</returns>
    public override string ToString() => ToJson();
}