// --------------------------------------------------------------------------
// <copyright file="SemanticsTable.cs" company="Solidsoft Reply Ltd.">
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
// Represents 'required' and 'minimum length' semantics.
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
/// Represents 'required' and 'minimum length' semantics.
/// </summary>
internal class SemanticsTable : IReadOnlyDictionary<string, ISemantics> {

    /// <summary>
    /// The semantics table.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, ISemantics> _semanticsTable;

    /// <summary>
    /// Initializes static members of the <see cref="SemanticsTable"/> class.
    /// </summary>
    static SemanticsTable() {
        _semanticsTable = new Dictionary<string, ISemantics>() {
                { "01", new SemanticsRequired { "21", "235" } },
                { "00", new SemanticsRequired() },
                { "8006", new SemanticsRequired { "21" } },
                { "8010", new SemanticsRequired { "8011" } },
                { "8004", new SemanticsRequired() },
                { "8003", new SemanticsMinLength(15) },
                { "253", new SemanticsMinLength(14) },
                { "254", new SemanticsMinLength(14) }
            };
    }

    /// <summary>
    /// Gets a collection containing the keys in the dictionary.
    /// </summary>
    public IEnumerable<string> Keys => _semanticsTable.Keys;

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    public IEnumerable<ISemantics> Values => _semanticsTable.Values;

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _semanticsTable.Count;

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>The semantics for the AI key.</returns>
    public ISemantics this[string key] => _semanticsTable[key];

    /// <summary>
    /// Factory method to create a new <see cref="SemanticsTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="SemanticsTable"/> instance.</returns>
    public static SemanticsTable Create() => [];

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>True, if the dictionary contains the AI key; otherwise false.</returns>
    public bool ContainsKey(string key) => _semanticsTable.ContainsKey(key);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator that iterates through the dictionary.</returns>
    public IEnumerator<KeyValuePair<string, ISemantics>> GetEnumerator() =>
        _semanticsTable.GetEnumerator();

    /// <summary>
    /// Gets the value associated with the specified AI key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <param name="value">The semantics.</param>
    /// <returns>The value associated with the specified AI key.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out ISemantics value) =>
        _semanticsTable.TryGetValue(key, out value);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator that iterates through the dictionary.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _semanticsTable.GetEnumerator();
}