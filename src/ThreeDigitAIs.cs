// --------------------------------------------------------------------------
// <copyright file="ThreeDigitAIs.cs" company="Solidsoft Reply Ltd.">
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
// A list of GS1 Application Identifiers with 3-digit numeric values.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections;

/// <summary>
/// A list of GS1 Application Identifiers (first two digits, only) with 3-digit numeric values.
/// </summary>
public class ThreeDigitAIs : IReadOnlyList<string> {

    /// <summary>
    /// The Three-Digit AI table.
    /// </summary>
    private static readonly List<string> _threeDigitAis = [];

    /// <summary>
    /// Initializes static members of the <see cref="ThreeDigitAIs"/> class.
    /// </summary>
    static ThreeDigitAIs() {
        _threeDigitAis = (from pl in PrefixLengthTable.Create()
                        where pl.Value == 3
                        select pl.Key).ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ThreeDigitAIs"/> class.
    /// </summary>
    private ThreeDigitAIs() {
    }

    /// <summary>
    /// Gets the count of entries in the list.
    /// </summary>
    public int Count => _threeDigitAis.Count;

    /// <summary>
    /// Gets a Three-Digit AI by index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A three-digit AI.</returns>
    public string this[int index] => _threeDigitAis[index];

    /// <summary>
    /// Factory method to create a new <see cref="ThreeDigitAIs"/> instance.
    /// </summary>
    /// <returns>An <see cref="ThreeDigitAIs"/> instance.</returns>
    public static ThreeDigitAIs Create() => [];

    /// <summary>
    /// Gets the enumerator for the list.
    /// </summary>
    /// <returns>A list enumerator.</returns>
    public IEnumerator<string> GetEnumerator() => _threeDigitAis.GetEnumerator();

    /// <summary>
    /// Gets the enumerator for the list.
    /// </summary>
    /// <returns>A list enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _threeDigitAis.GetEnumerator();

    /// <summary>
    /// Returns the Three-Digits table as JSON.
    /// </summary>
    /// <returns>The Three-Digits table as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(_threeDigitAis);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the Three-Digits table as JSON.
    /// </summary>
    /// <returns>The Three-Digits table as JSON.</returns>
    public override string ToString() => ToJson();
}