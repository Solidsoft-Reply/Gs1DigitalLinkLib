// --------------------------------------------------------------------------
// <copyright file="ValidationExpressionTable.cs" company="Solidsoft Reply Ltd.">
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
// Represents the Validation Expression table.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

/// <summary>
/// A table of GS1 Application Identifier validation expressions. The table
/// provides regular expressions for validating the content of GS1 Application
/// Identifiers.
/// </summary>
public class ValidationExpressionTable : IReadOnlyDictionary<string, Regex> {

    /// <summary>
    /// The Validation Expression dictionary.
    /// </summary>
    private static readonly Dictionary<string, Regex> _validationExpressions = [];

    /// <summary>
    /// The regular expressions in the Validation Expression dictionary.
    /// </summary>
    private static readonly IEnumerable<Regex> _validationExpressionsValues;

    /// <summary>
    /// Initializes static members of the <see cref="ValidationExpressionTable"/> class.
    /// </summary>
    static ValidationExpressionTable() {

        _validationExpressions = Gs1DigitalLinkConvert.AiRegex;
        _validationExpressionsValues = from v in _validationExpressions.Values
                                       select v;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationExpressionTable"/> class.
    /// </summary>
    private ValidationExpressionTable() {
    }

    /// <summary>
    /// Gets the collection of GS1 Application Identifiers.
    /// </summary>
    public IEnumerable<string> Keys =>
        _validationExpressions.Keys;

    /// <summary>
    /// Gets the collection of regular expressions.
    /// </summary>
    public IEnumerable<Regex> Values =>
        _validationExpressionsValues;

    /// <summary>
    /// Gets the number of GS1 Application Identifiers in the table.
    /// </summary>
    public int Count => _validationExpressions.Count;

    /// <summary>
    /// Gets the <see cref="Regex"/> at the specified index.
    /// </summary>
    /// <param name="index">The index of the <see cref="Regex"/>.</param>
    /// <returns>The <see cref="Regex"/>.</returns>
    public Regex this[string index] => _validationExpressions[index];

    /// <summary>
    /// Factory method to create a new <see cref="ValidationExpressionTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="ValidationExpressionTable"/> instance.</returns>
    public static ValidationExpressionTable Create() => [];

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    public IEnumerator<KeyValuePair<string, Regex>> GetEnumerator() =>
        _validationExpressions.GetEnumerator();

    /// <summary>
    /// Determines whether the table contains an element with the specified GS1 Application Identifier.
    /// </summary>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <returns>True, if the table contains the GS1 Application Identifier; otherwise false.</returns>
    public bool ContainsKey(string ai) =>
        _validationExpressions.ContainsKey(ai);

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="ai">The GS1 Application Identifier.</param>
    /// <param name="expression">The validation expression.</param>
    /// <returns>True, if the GS1 Application IDentifier is located in the table; otherwise false.</returns>
    public bool TryGetValue(string ai, [MaybeNullWhen(false)] out Regex expression) =>
        _validationExpressions.TryGetValue(ai, out expression);

    /// <summary>
    /// Returns an enumerator that iterates through the table.
    /// </summary>
    /// <returns>An enumerator for the table.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _validationExpressions.GetEnumerator();

    /// <summary>
    /// Returns the Validation Expressions table as JSON.
    /// </summary>
    /// <returns>The Validation Expressions table as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
        System.Text.Json.JsonSerializer.Serialize(_validationExpressions);
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the Validation Expressions table as JSON.
    /// </summary>
    /// <returns>The Validation Expressions table as JSON.</returns>
    public override string ToString() => ToJson();
}