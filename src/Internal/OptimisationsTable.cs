// --------------------------------------------------------------------------
// <copyright file="OptimisationsTable.cs" company="Solidsoft Reply Ltd.">
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
// Represents the optimisations table.
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
/// Represents the optimisations table.
/// </summary>
internal class OptimisationsTable : IReadOnlyDictionary<string, IList<string>> {

    /// <summary>
    /// The optimisations table.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, IList<string>> _optimisationsTable;

    /// <summary>
    /// Initializes static members of the <see cref="OptimisationsTable"/> class.
    /// </summary>
    static OptimisationsTable() =>
        _optimisationsTable = new Dictionary<string, IList<string>>() {
            { "0A", new List<string> { "01", "22" } },
            { "0B", new List<string> { "01", "10" } },
            { "0C", new List<string> { "01", "21" } },
            { "0D", new List<string> { "01", "17" } },
            { "0E", new List<string> { "01", "7003" } },
            { "0F", new List<string> { "01", "30" } },
            { "1A", new List<string> { "01", "10", "21", "17" } },
            { "1B", new List<string> { "01", "15" } },
            { "1C", new List<string> { "01", "11" } },
            { "1D", new List<string> { "01", "16" } },
            { "1E", new List<string> { "01", "91" } },
            { "1F", new List<string> { "01", "10", "15" } },
            { "2A", new List<string> { "01", "3100" } },
            { "2B", new List<string> { "01", "3101" } },
            { "2C", new List<string> { "01", "3102" } },
            { "2D", new List<string> { "01", "3103" } },
            { "2E", new List<string> { "01", "3104" } },
            { "2F", new List<string> { "01", "3105" } },
            { "3A", new List<string> { "01", "3200" } },
            { "3B", new List<string> { "01", "3201" } },
            { "3C", new List<string> { "01", "3202" } },
            { "3D", new List<string> { "01", "3203" } },
            { "3E", new List<string> { "01", "3204" } },
            { "3F", new List<string> { "01", "3205" } },
            { "9A", new List<string> { "8010", "8011" } },
            { "9B", new List<string> { "8017", "8019" } },
            { "9C", new List<string> { "8018", "8019" } },
            { "9D", new List<string> { "414", "254" } },
            { "A0", new List<string> { "01", "3920" } },
            { "A1", new List<string> { "01", "3921" } },
            { "A2", new List<string> { "01", "3922" } },
            { "A3", new List<string> { "01", "3923" } },
            { "A4", new List<string> { "01", "3924" } },
            { "A5", new List<string> { "01", "3925" } },
            { "A6", new List<string> { "01", "3926" } },
            { "A7", new List<string> { "01", "3927" } },
            { "A8", new List<string> { "01", "3928" } },
            { "A9", new List<string> { "01", "3929" } },
            { "C0", new List<string> { "255", "3900" } },
            { "C1", new List<string> { "255", "3901" } },
            { "C2", new List<string> { "255", "3902" } },
            { "C3", new List<string> { "255", "3903" } },
            { "C4", new List<string> { "255", "3904" } },
            { "C5", new List<string> { "255", "3905" } },
            { "C6", new List<string> { "255", "3906" } },
            { "C7", new List<string> { "255", "3907" } },
            { "C8", new List<string> { "255", "3908" } },
            { "C9", new List<string> { "255", "3909" } },
            { "CA", new List<string> { "255", "3940" } },
            { "CB", new List<string> { "255", "3941" } },
            { "CC", new List<string> { "255", "3942" } },
            { "CD", new List<string> { "255", "3943" } },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="OptimisationsTable"/> class.
    /// </summary>
    private OptimisationsTable() {
    }

    /// <summary>
    /// Gets a collection containing the optimisation keys in the dictionary.
    /// </summary>
    public IEnumerable<string> Keys => _optimisationsTable.Keys;

    /// <summary>
    /// Gets a collection containing the sequences of AIs in the dictionary.
    /// </summary>
    public IEnumerable<IList<string>> Values => _optimisationsTable.Values;

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _optimisationsTable.Count;

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The optimisation key.</param>
    /// <returns>The sequence of AIs for the optimisation key.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public IList<string> this[string key] => _optimisationsTable[key];

    /// <summary>
    /// Factory method to create a new <see cref="OptimisationsTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="OptimisationsTable"/> instance.</returns>
    public static OptimisationsTable Create() => [];

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified optimisation key.
    /// </summary>
    /// <param name="key">The optimisation key.</param>
    /// <returns>True, if the dictionary contains the optimisation key; otherwise false.</returns>
    public bool ContainsKey(string key) =>
        _optimisationsTable.ContainsKey(key);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<KeyValuePair<string, IList<string>>> GetEnumerator() =>
        _optimisationsTable.GetEnumerator();

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The optimisation key.</param>
    /// <param name="value">The AI sequence.</param>
    /// <returns>True, if the optimisation key exists; otherwise false.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IList<string> value) =>
        _optimisationsTable.TryGetValue(key, out value);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _optimisationsTable.GetEnumerator();
}