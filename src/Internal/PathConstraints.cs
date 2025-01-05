// --------------------------------------------------------------------------
// <copyright file="PathConstraints.cs" company="Solidsoft Reply Ltd.">
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
// Represents path constraints.
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
/// Represents path constraints.
/// </summary>
internal class PathConstraints : IReadOnlyDictionary<string, IReadOnlyCollection<IReadOnlyCollection<string>>> {

    /// <summary>
    /// The path constraints.
    /// </summary>
    private static readonly IReadOnlyDictionary<string, IReadOnlyCollection<IReadOnlyCollection<string>>> _pathItem;

    /// <summary>
    /// Initializes static members of the <see cref="PathConstraints"/> class.
    /// </summary>
    static PathConstraints() {
        _pathItem = new Dictionary<string, IReadOnlyCollection<IReadOnlyCollection<string>>>() {
            {
                "00", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // sscc-path
                }
            },
            {
                "01", new List<IReadOnlyCollection<string>> {
                    new List<string> { "22", "10", "21" }, // gtin-path
                    new List<string> { "235" } // upui-path
                }
            },
            {
                "253", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // gdti-path
                }
            },
            {
                "255", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // gcn-path
                }
            },
            {
                "401", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // ginc-path
                }
            },
            {
                "402", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // gsin-path
                }
            },
            {
                "414", new List<IReadOnlyCollection<string>> {
                    new List<string> { "254" }, // gln-path
                    new List<string> { "7040" } // fid-path
                }
            },
            {
                "415", new List<IReadOnlyCollection<string>> {
                    new List<string> { "8020" } // payTo-path
                }
            },
            {
                "417", new List<IReadOnlyCollection<string>> {
                    new List<string> { }, // partyGln-path
                    new List<string> { "7040" } // eoid-path
                }
            },
            {
                "8003", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // grai-path
                }
            },
            {
                "8004", new List<IReadOnlyCollection<string>> {
                    new List<string> { }, // giai-path
                    new List<string> { "7040" } // mid-path
                }
            },
            {
                "8006", new List<IReadOnlyCollection<string>> {
                    new List<string> { "22", "10", "21" } // itip-path
                }
            },
            {
                "8010", new List<IReadOnlyCollection<string>> {
                    new List<string> { "8011" } // cpid-path
                }
            },
            {
                "8013", new List<IReadOnlyCollection<string>> {
                    new List<string> { } // gmn-path
                }
            },
            {
                "8017", new List<IReadOnlyCollection<string>> {
                    new List<string> { "8019" } // gsrnp-path
                }
            },
            {
                "8018", new List<IReadOnlyCollection<string>> {
                    new List<string> { "8019" } // gsrn-path
                }
            },
       };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PathConstraints"/> class.
    /// </summary>
    private PathConstraints() {
    }

    /// <summary>
    /// Gets a collection containing the AI keys.
    /// </summary>
    public IEnumerable<string> Keys => _pathItem.Keys;

    /// <summary>
    /// Gets a collection containing the path constraints.
    /// </summary>
    public IEnumerable<IReadOnlyCollection<IReadOnlyCollection<string>>> Values => _pathItem.Values;

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _pathItem.Count;

    /// <summary>
    /// Gets the path constraints associated with the specified AI key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>The path constraints.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>"
    public IReadOnlyCollection<IReadOnlyCollection<string>> this[string key] => _pathItem[key];

    /// <summary>
    /// Factory method to create a new <see cref="PathConstraints"/> instance.
    /// </summary>
    /// <returns>An <see cref="PathConstraints"/> instance.</returns>
    public static PathConstraints Create() => [];

    /// <summary>
    /// Determines whether the dictionary contains path constraints for the specified AI key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>True, if the AI key exists; otherwise false.</returns>
    public bool ContainsKey(string key) => _pathItem.ContainsKey(key);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator that iterates through the dictionary.</returns>
    public IEnumerator<KeyValuePair<string, IReadOnlyCollection<IReadOnlyCollection<string>>>> GetEnumerator() =>
        _pathItem.GetEnumerator();

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <param name="value">The path constraints.</param>
    /// <returns>True, if the AI exists in the table; otherwise false.</returns>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out IReadOnlyCollection<IReadOnlyCollection<string>> value) =>
        _pathItem.TryGetValue(key, out value);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator that iterates through the dictionary.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _pathItem.GetEnumerator();
}