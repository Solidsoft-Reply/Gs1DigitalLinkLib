// --------------------------------------------------------------------------
// <copyright file="TwoDigitAIs.cs" company="Solidsoft Reply Ltd.">
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
// A list of GS1 Application Identifiers with 2-digit numeric values.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections;

/// <summary>
/// A list of GS1 Application Identifiers with 2-digit numeric values.
/// </summary>
public class TwoDigitAIs : IReadOnlyList<string> {

    /// <summary>
    /// The Two-Digit AI table.
    /// </summary>
    private static readonly List<string> _twoDigitAis = [];

    /// <summary>
    /// Initializes static members of the <see cref="TwoDigitAIs"/> class.
    /// </summary>
    static TwoDigitAIs() {
        _twoDigitAis = (from pl in PrefixLengthTable.Create()
                       where pl.Value == 2
                       select pl.Key).ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TwoDigitAIs"/> class.
    /// </summary>
    private TwoDigitAIs() {
    }

    /// <summary>
    /// Gets the count of entries in the list.
    /// </summary>
    public int Count => _twoDigitAis.Count;

    /// <summary>
    /// Gets a Two-Digit AI by index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A two-digit AI.</returns>
    public string this[int index] => _twoDigitAis[index];

    /// <summary>
    /// Factory method to create a new <see cref="TwoDigitAIs"/> instance.
    /// </summary>
    /// <returns>An <see cref="TwoDigitAIs"/> instance.</returns>
    public static TwoDigitAIs Create() => [];

    /// <summary>
    /// Gets the enumerator for the list.
    /// </summary>
    /// <returns>A list enumerator.</returns>
    public IEnumerator<string> GetEnumerator() => _twoDigitAis.GetEnumerator();

    /// <summary>
    /// Gets the enumerator for the list.
    /// </summary>
    /// <returns>A list enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _twoDigitAis.GetEnumerator();

    /// <summary>
    /// Returns the Two-Digits table as JSON.
    /// </summary>
    /// <returns>The Two-Digits table as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(_twoDigitAis);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the Two-Digits table as JSON.
    /// </summary>
    /// <returns>The Two-Digits table as JSON.</returns>
    public override string ToString() => ToJson();
}