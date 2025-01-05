// --------------------------------------------------------------------------
// <copyright file="PrefixLengthTable.cs" company="Solidsoft Reply Ltd.">
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
// Represents the prefix length table.
// </summary>
// --------------------------------------------------------------------------
// The code in this file is derived in part from the GS1 Digital Link
// Compression Prototype which is licenced under the Apache License,
// Version 2.0.
//
// Copyright 2019 GS1 AISBL
//
// See https://github.com/gs1/GS1DigitalLinkCompressionPrototype for
// further details.
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the prefix length table.
/// </summary>
internal class PrefixLengthTable : IReadOnlyDictionary<string, int> {

    /// <summary>
    /// The prefix length table.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, int> _prefixTable;

    /// <summary>
    /// Initializes static members of the <see cref="PrefixLengthTable"/> class.
    /// </summary>
    static PrefixLengthTable() {
        _prefixTable = new Dictionary<string, int>() {
            { "00", 2 },
            { "01", 2 },
            { "02", 2 },
            { "10", 2 },
            { "11", 2 },
            { "12", 2 },
            { "13", 2 },
            { "15", 2 },
            { "16", 2 },
            { "17", 2 },
            { "20", 2 },
            { "21", 2 },
            { "22", 2 },
            { "23", 3 },
            { "24", 3 },
            { "25", 3 },
            { "30", 2 },
            { "31", 4 },
            { "32", 4 },
            { "33", 4 },
            { "34", 4 },
            { "35", 4 },
            { "36", 4 },
            { "37", 2 },
            { "39", 4 },
            { "40", 3 },
            { "41", 3 },
            { "42", 3 },
            { "43", 4 },
            { "70", 4 },
            { "71", 3 },
            { "72", 4 },
            { "80", 4 },
            { "81", 4 },
            { "82", 4 },
            { "90", 2 },
            { "91", 2 },
            { "92", 2 },
            { "93", 2 },
            { "94", 2 },
            { "95", 2 },
            { "96", 2 },
            { "97", 2 },
            { "98", 2 },
            { "99", 2 }
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PrefixLengthTable"/> class.
    /// </summary>
    private PrefixLengthTable() {
    }

    /// <summary>
    /// Gets a collection containing the keys in the dictionary.
    /// </summary>
    public IEnumerable<string> Keys => _prefixTable.Keys;

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    public IEnumerable<int> Values => _prefixTable.Values;

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _prefixTable.Count;

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>The length of the AI prefix.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>"
    public int this[string key] => _prefixTable[key];

    /// <summary>
    /// Factory method to create a new <see cref="PrefixLengthTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="PrefixLengthTable"/> instance.</returns>
    public static PrefixLengthTable Create() => [];

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>True, if the dictionary contains the key; otherwise false.</returns>
    public bool ContainsKey(string key) => _prefixTable.ContainsKey(key);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator for the dictionary.</returns>
    public IEnumerator<KeyValuePair<string, int>> GetEnumerator() => _prefixTable.GetEnumerator();

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <param name="value">The length of the AI prefix.</param>
    /// <returns>True, if the key was found; otherwise false.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out int value) => _prefixTable.TryGetValue(key, out value);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator for the dictionary.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _prefixTable.GetEnumerator();
}
