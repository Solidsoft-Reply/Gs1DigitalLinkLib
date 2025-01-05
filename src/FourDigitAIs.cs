// --------------------------------------------------------------------------
// <copyright file="FourDigitAIs.cs" company="Solidsoft Reply Ltd.">
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
// A list of GS1 Application Identifiers with 4-digit numeric values.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections;

/// <summary>
/// A list of GS1 Application Identifiers with 4-digit numeric values.
/// </summary>
public class FourDigitAIs : IReadOnlyList<string> {

    /// <summary>
    /// The Four-Digit AI table.
    /// </summary>
    private static readonly List<string> _fourDigitAis = [];

    /// <summary>
    /// Initializes static members of the <see cref="FourDigitAIs"/> class.
    /// </summary>
    static FourDigitAIs() {
        _fourDigitAis = (from pl in PrefixLengthTable.Create()
                        where pl.Value == 4
                        select pl.Key).ToList();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FourDigitAIs"/> class.
    /// </summary>
    private FourDigitAIs() {
    }

    /// <summary>
    /// Gets the count of entries in the list.
    /// </summary>
    public int Count => _fourDigitAis.Count;

    /// <summary>
    /// Gets a Four-Digit AI by index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A four-digit AI.</returns>
    public string this[int index] => _fourDigitAis[index];

    /// <summary>
    /// Factory method to create a new <see cref="FourDigitAIs"/> instance.
    /// </summary>
    /// <returns>An <see cref="FourDigitAIs"/> instance.</returns>
    public static FourDigitAIs Create() => [];

    /// <summary>
    /// Gets the enumerator for the list.
    /// </summary>
    /// <returns>A list enumerator.</returns>
    public IEnumerator<string> GetEnumerator() => _fourDigitAis.GetEnumerator();

    /// <summary>
    /// Gets the enumerator for the list.
    /// </summary>
    /// <returns>A list enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _fourDigitAis.GetEnumerator();
}
