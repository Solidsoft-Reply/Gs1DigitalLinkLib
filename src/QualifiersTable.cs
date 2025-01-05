// --------------------------------------------------------------------------
// <copyright file="QualifiersTable.cs" company="Solidsoft Reply Ltd.">
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
// A table of GS1 Application Identifier qualifiers.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A table of GS1 Application Identifier qualifiers. This table provides arrays
/// of GS1 qualifier Application Identifiers for a given GS1 identifier Application
/// Identifier.
/// </summary>
public class QualifiersTable : IReadOnlyDictionary<string, string[]> {

    /// <summary>
    /// The Qualifiers dictionary.
    /// </summary>
    private static readonly Dictionary<string, string[]> _qualifiers = [];

    /// <summary>
    /// The arrays or qualifiers in the Qualifiers dictionary.
    /// </summary>
    private static readonly IEnumerable<string[]> _qualifierValues;

    /// <summary>
    /// Initializes static members of the <see cref="QualifiersTable"/> class.
    /// </summary>
    static QualifiersTable() {

        _qualifiers = DigitalLinkConvert.AiQualifiers;
        _qualifierValues = from v in _qualifiers.Values
                            select v;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="QualifiersTable"/> class.
    /// </summary>
    private QualifiersTable() {
    }

    /// <summary>
    /// Gets the number of GS1 identifier Application Identifiers in the table.
    /// </summary>
    public int Count => _qualifiers.Count;

    /// <summary>
    /// Gets the collection of GS1 identifier Application Identifiers.
    /// </summary>
    public IEnumerable<string> Keys =>
        _qualifiers.Keys;

    /// <summary>
    /// Gets the collection of GS1 qualifier Application Identifiers.
    /// </summary>
    public IEnumerable<string[]> Values =>
        _qualifierValues;

    /// <summary>
    /// Gets the GS1 qualifier Application Identifiers at the specified index.
    /// </summary>
    /// <param name="index">The index of the GS1 qualifier Application Identifiers.</param>
    /// <returns>The GS1 qualifier Application Identifiers.</returns>
    public string[] this[string index] => _qualifiers[index];

    /// <summary>
    /// Factory method to create a new <see cref="QualifiersTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="QualifiersTable"/> instance.</returns>
    public static QualifiersTable Create() => [];

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator() =>
        _qualifiers.GetEnumerator();

    /// <summary>
    /// Determines whether the table contains a specified GS1 identifier Application Identifier.
    /// </summary>
    /// <param name="ai">The GS1 identifier Application Identifier.</param>
    /// <returns>True, if the table contains the GS1 identifier Application Identifier; otherwise false.</returns>
    public bool ContainsKey(string ai) =>
        _qualifiers.ContainsKey(ai);

    /// <summary>
    /// Gets the GS1 qualifier Application Identifiers associated with the specified GS1 identifier Application Identifier.
    /// </summary>
    /// <param name="ai">The GS1 identifier Application Identifier.</param>
    /// <param name="qualifiers">The GS1 qualifier Application Identifiers.</param>
    /// <returns>True, if the GS1 qualifier Application Identifier is located in the table; otherwise false.</returns>
    public bool TryGetValue(string ai, [MaybeNullWhen(false)] out string[] qualifiers) =>
        _qualifiers.TryGetValue(ai, out qualifiers);

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _qualifiers.GetEnumerator();
}