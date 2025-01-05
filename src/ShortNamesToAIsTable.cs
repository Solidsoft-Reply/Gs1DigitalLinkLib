// --------------------------------------------------------------------------
// <copyright file="ShortNamesToAIsTable.cs" company="Solidsoft Reply Ltd.">
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
// A table that maps short names to GS1 Application Identifiers.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A table that maps short names to GS1 Application Identifiers. The table
/// maps short names to GS1 AIs. Short Names are also called 'convenience
/// alphas'. They are a legacy feature which was removed from version 1.1.3
/// of the GS1 Digital Link standard. Some resolver services may not support
/// short names.
/// </summary>
[Obsolete("This class is obsolete and is retained for legacy purposes only. It may be removed in a future version. Short names ('convenience alphas') were removed from version 1.1.3 of the GS1 Digital Link standard.")]
public class ShortNamesToAIsTable : IReadOnlyDictionary<string, string> {

    /// <summary>
    /// The Short Names to AIs dictionary.
    /// </summary>
    private static readonly Dictionary<string, string> _shortNamesToAIs = [];

    /// <summary>
    /// The regular expressions in the Short Names to AIs dictionary.
    /// </summary>
    private static readonly IEnumerable<string> _shortNamesToAIsValues;

    /// <summary>
    /// Initializes static members of the <see cref="ShortNamesToAIsTable"/> class.
    /// </summary>
    static ShortNamesToAIsTable() {

        _shortNamesToAIs = DigitalLinkConvert.AiShortNamesToAIs;
        _shortNamesToAIsValues = from v in _shortNamesToAIs.Values
                                 select v;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortNamesToAIsTable"/> class.
    /// </summary>
    private ShortNamesToAIsTable() {
    }

    /// <summary>
    /// Gets the number of short names in the table.
    /// </summary>
    public int Count => _shortNamesToAIs.Count;

    /// <summary>
    /// Gets the collection of short names.
    /// </summary>
    public IEnumerable<string> Keys =>
        _shortNamesToAIs.Keys;

    /// <summary>
    /// Gets the collection of GS1 Application Identifiers.
    /// </summary>
    public IEnumerable<string> Values =>
        _shortNamesToAIsValues;

    /// <summary>
    /// Gets the GS1 Application Identifier at the specified index.
    /// </summary>
    /// <param name="index">The index of the GS1 Application IDentifier.</param>
    /// <returns>The GS1 Application Identifier.</returns>
    public string this[string index] => _shortNamesToAIs[index];

    /// <summary>
    /// Factory method to create a new <see cref="ShortNamesToAIsTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="ShortNamesToAIsTable"/> instance.</returns>
    public static ShortNamesToAIsTable Create() => [];

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() =>
        _shortNamesToAIs.GetEnumerator();

    /// <summary>
    /// Determines whether the table contains the specified short name.
    /// </summary>
    /// <param name="shortName">The short name for a GS1 Application Identifier.</param>
    /// <returns>True, if the table contains the GS1 Application Identifier; otherwise false.</returns>
    public bool ContainsKey(string shortName) =>
        _shortNamesToAIs.ContainsKey(shortName);

    /// <summary>
    /// Gets the GS1 Application Identifier associated with the specified short name.
    /// </summary>
    /// <param name="shortName">The short name for a GS1 Application Identifier.</param>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <returns>True, if the GS1 Application Identifier is located in the table; otherwise false.</returns>
    public bool TryGetValue(string shortName, [MaybeNullWhen(false)] out string ai) =>
        _shortNamesToAIs.TryGetValue(shortName, out ai);

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _shortNamesToAIs.GetEnumerator();
}