// --------------------------------------------------------------------------
// <copyright file="BaseSemantics.cs" company="Solidsoft Reply Ltd.">
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
// Represents a base semantics item.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents a base semantics item.
/// </summary>
internal class BaseSemantics : IReadOnlyDictionary<string, IList<string>> {

    /// <summary>
    /// The semantics item.
    /// </summary>
    private readonly IReadOnlyDictionary<string, IList<string>> _semantics;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseSemantics"/> class.
    /// </summary>
    /// <param name="semantics">The semantics.</param>
    internal protected BaseSemantics(IReadOnlyDictionary<string, IList<string>> semantics) {
        _semantics = semantics;
    }

    /// <summary>
    /// Gets a collection containing the keys in the dictionary.
    /// </summary>
    public IEnumerable<string> Keys => _semantics.Keys;

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    public IEnumerable<IList<string>> Values => _semantics.Values;

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _semantics.Count;

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>The value associated with the specified key.</returns>
    public IList<string> this[string key] => _semantics[key];

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>Semantics for the given key.</returns>
    public bool ContainsKey(string key) => _semantics.ContainsKey(key);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>An enumerator for the dictionary.</returns>
    public IEnumerator<KeyValuePair<string, IList<string>>> GetEnumerator() => _semantics.GetEnumerator();

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>True, if the key exists in the dictionary; otherwise false.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IList<string> value) => _semantics.TryGetValue(key, out value);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>An enumerator for the dictionary.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _semantics.GetEnumerator();
}