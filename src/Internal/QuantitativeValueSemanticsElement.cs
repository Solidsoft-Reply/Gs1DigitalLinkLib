﻿// --------------------------------------------------------------------------
// <copyright file="QuantitativeValueSemanticsElement.cs" company="Solidsoft Reply Ltd.">
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
// Represents a quantitative value semantics element.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents a quantitative value semantics element.
/// </summary>
    /// <param name="ai">The AI key.</param>
    /// <param name="quantitativeValueSemanticsItem">The quantitative value semantics item.</param>
internal class QuantitativeValueSemanticsElement(string ai, QuantitativeValueSemanticsItem quantitativeValueSemanticsItem) {

    /// <summary>
    /// Gets the AI key.
    /// </summary>
    public string AI { get; init; } = ai;

    /// <summary>
    /// Gets the quantitative value semantics item.
    /// </summary>
    public QuantitativeValueSemanticsItem QuantitativeValueSemanticsItem { get; init; } = quantitativeValueSemanticsItem;
}