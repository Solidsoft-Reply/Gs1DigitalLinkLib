// --------------------------------------------------------------------------
// <copyright file="Gs1ElementString.cs" company="Solidsoft Reply Ltd.">
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
// Represents a GS1 Element String.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// Represents a GS1 Element String.
/// </summary>
/// <param name="elementString">A GS1 Element String.</param>
public class Gs1ElementString(string elementString) {

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1ElementString"/> class.
    /// </summary>
    /// <param name="elementStringSpan">A GS1 Element String character span.</param>
    public Gs1ElementString(ReadOnlySpan<char> elementStringSpan)
        : this(elementStringSpan.ToString()) {
    }

    /// <summary>
    /// Gets the Digital Link URI as a string.
    /// </summary>
    public string Value { get; init; } = elementString;

    /// <summary>
    /// Gets the GS1 Element String as a span.
    /// </summary>
    /// <returns>The GS1 Element String as a span.</returns>
    public ReadOnlySpan<char> AsSpan() => Value.AsSpan();

    /// <summary>
    /// Gets the GS1 Digital Link URI as a string.
    /// </summary>
    /// <returns>The GS1 Element String as a string.</returns>
    public override string ToString() => Value;
}