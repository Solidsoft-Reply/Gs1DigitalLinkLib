// --------------------------------------------------------------------------
// <copyright file="CheckDigitPositionTable.cs" company="Solidsoft Reply Ltd.">
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
// A table of GS1 Application Identifier check digit positions.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A table of GS1 Application Identifier check digit positions.
/// </summary>
public class CheckDigitPositionTable : IReadOnlyDictionary<string, CheckDigitPosition?> {

    /// <summary>
    /// The Check Digit Positions dictionary.
    /// </summary>
    private static readonly Dictionary<string, CheckDigitPosition?> _checkDigitPositions = [];

    /// <summary>
    /// The values, including nulls, in the Check Digit Positions dictionary.
    /// </summary>
    private static readonly IEnumerable<CheckDigitPosition?> _checkDigitPositionValuesSparse;

    /// <summary>
    /// The values, excluding nulls, in the Check Digit Positions dictionary.
    /// </summary>
    private static readonly IEnumerable<CheckDigitPosition> _checkDigitPositionValues;

    /// <summary>
    /// Initializes static members of the <see cref="CheckDigitPositionTable"/> class.
    /// </summary>
    static CheckDigitPositionTable() {

        _checkDigitPositions = DigitalLinkConvert.AiCheckDigitPositions;
        _checkDigitPositionValuesSparse = from v in _checkDigitPositions.Values
                                          select (CheckDigitPosition?)v;
        _checkDigitPositionValues = from v in _checkDigitPositionValuesSparse
                                    where v is not null
                                    select (CheckDigitPosition)v;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckDigitPositionTable"/> class.
    /// </summary>
    private CheckDigitPositionTable() {
    }

    /// <summary>
    /// Gets the number of GS1 Application Identifiers in the table.
    /// </summary>
    public int Count => _checkDigitPositions.Count;

    /// <summary>
    /// Gets the collection of GS1 Application Identifiers.
    /// </summary>
    public IEnumerable<string> Keys =>
        _checkDigitPositions.Keys;

    /// <summary>
    /// Gets the collection of non-null check digit positions.
    /// </summary>
    public IEnumerable<CheckDigitPosition> Values =>
        _checkDigitPositionValues;

    /// <summary>
    /// Gets the collection of check digit positions, including nulls.
    /// </summary>
    IEnumerable<CheckDigitPosition?> IReadOnlyDictionary<string, CheckDigitPosition?>.Values =>
         _checkDigitPositionValuesSparse;

    /// <summary>
    /// Gets the <see cref="CheckDigitPosition"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the <see cref="CheckDigitPosition"/>.</param>
    /// <returns>The <see cref="CheckDigitPosition"/>.</returns>
    public CheckDigitPosition? this[string index] => _checkDigitPositions[index] ?? null;

    /// <summary>
    /// Factory method to create a new <see cref="CheckDigitPositionTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="CheckDigitPositionTable"/> instance.</returns>
    public static CheckDigitPositionTable Create() => [];

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    public IEnumerator<KeyValuePair<string, CheckDigitPosition?>> GetEnumerator() =>
        _checkDigitPositions.GetEnumerator();

    /// <summary>
    /// Determines whether the table contains an element with the specified GS1 Application Identifier.
    /// </summary>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <returns>True, if the table contains the GS1 Application Identifier; otherwise false.</returns>
    public bool ContainsKey(string ai) =>
        _checkDigitPositions.ContainsKey(ai);

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <param name="checkDigitPosition">The Check Digit position.</param>
    /// <returns>True, if the GS1 Application IDentifier is located in the table; otherwise false.</returns>
    public bool TryGetValue(string ai, [MaybeNullWhen(false)] out CheckDigitPosition? checkDigitPosition) =>
        _checkDigitPositions.TryGetValue(ai, out checkDigitPosition);

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _checkDigitPositions.GetEnumerator();
}