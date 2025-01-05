﻿// --------------------------------------------------------------------------
// <copyright file="QuantitativeValueSemanticsItem.cs" company="Solidsoft Reply Ltd.">
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
// Represents a quantitative value semantics item.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents a quantitative value semantics item.
/// </summary>
internal class QuantitativeValueSemanticsItem {

    /// <summary>
    /// Initializes a new instance of the <see cref="QuantitativeValueSemanticsItem"/> class.
    /// </summary>
    /// <param name="predicates">The list of predicates.</param>
    /// <param name="rec20">The Rec20.</param>
    public QuantitativeValueSemanticsItem(List<string> predicates, string rec20) {
        Predicates = predicates;
        Rec20 = rec20;
    }

    /// <summary>
    /// Gets the predicate list.
    /// </summary>
    public List<string> Predicates { get; init; }

    /// <summary>
    /// Gets the rec20.
    /// </summary>
    public string Rec20 { get; init; }
}