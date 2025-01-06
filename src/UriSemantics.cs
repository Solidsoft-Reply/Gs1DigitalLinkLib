// --------------------------------------------------------------------------
// <copyright file="UriSemantics.cs" company="Solidsoft Reply Ltd.">
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
// Represents the semantics of GS1 Digital Link URIs.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the semantics of GS1 Digital Link URIs.
/// </summary>
public class UriSemantics : IReadOnlyDictionary<string, object> {

    private readonly Dictionary<string, object> _uriSemantics;

    /// <summary>
    /// Initializes a new instance of the <see cref="UriSemantics"/> class.
    /// </summary>
    internal UriSemantics() {
        _uriSemantics = [];
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UriSemantics"/> class.
    /// </summary>
    /// <param name="uriSemantics">The URI semantics.</param>
    internal UriSemantics(Dictionary<string, object> uriSemantics) {
        _uriSemantics = uriSemantics;
    }

    /// <summary>
    /// Gets the keys.
    /// </summary>
    public IEnumerable<string> Keys => _uriSemantics.Keys;

    /// <summary>
    /// Gets the semantic values.
    /// </summary>
    public IEnumerable<object> Values => _uriSemantics.Values;

    /// <summary>
    /// Gets the count of semantic values.
    /// </summary>
    public int Count => _uriSemantics.Count;

    /// <summary>
    /// Gets a semantic value by key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>A semantic value.</returns>
    public object this[string key] => _uriSemantics[key];

    /// <summary>
    /// Determines whether the <see cref="UriSemantics"/> contains a key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>True, if the key is available; otherwise false.</returns>
    public bool ContainsKey(string key) =>
        _uriSemantics.ContainsKey(key);

    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
        _uriSemantics.GetEnumerator();

    /// <summary>
    /// Tries to get a semantic value by key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The semantic value.</param>
    /// <returns>True, if the key is found; otherwise false.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out object value) =>
        _uriSemantics.TryGetValue(key, out value);

    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    /// <returns>The enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}