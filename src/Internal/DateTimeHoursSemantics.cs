// --------------------------------------------------------------------------
// <copyright file="DateTimeHoursSemantics.cs" company="Solidsoft Reply Ltd.">
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
// Represents a date-time hours semantics item.
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

/// <summary>
/// Represents a date-time hours semantics item.
/// </summary>
internal class DateTimeHoursSemantics : BaseSemantics {
    /// <summary>
    /// The date-time hours semantics.
    /// </summary>
    private static readonly Dictionary<string, IList<string>> _dateTimeHourSemantics;

    /// <summary>
    /// Initializes static members of the <see cref="DateTimeHoursSemantics"/> class.
    /// </summary>
    static DateTimeHoursSemantics() {
        _dateTimeHourSemantics = new () {
            { "4324", new List<string> { "gs1:deliverNotBeforeDateTime" } },
            { "4325", new List<string> { "gs1:deliverNotAfterDateTime" } }
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateTimeHoursSemantics"/> class.
    /// </summary>
    private DateTimeHoursSemantics()
        : base(_dateTimeHourSemantics) {
    }

    /// <summary>
    /// Factory method to create a new <see cref="DateTimeHoursSemantics"/> instance.
    /// </summary>
    /// <returns>An <see cref="DateTimeHoursSemantics"/> instance.</returns>
    public static DateTimeHoursSemantics Create() => [];
}
