// --------------------------------------------------------------------------
// <copyright file="DateSemantics.cs" company="Solidsoft Reply Ltd.">
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
// Represents a date semantics item.
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
/// Represents a date semantics item.
/// </summary>
internal class DateSemantics : BaseSemantics {
    /// <summary>
    /// The date semantics.
    /// </summary>
    private static readonly Dictionary<string, IList<string>> _dateSemantics;

    /// <summary>
    /// Initializes static members of the <see cref="DateSemantics"/> class.
    /// </summary>
    static DateSemantics() {
        _dateSemantics = new () {
            { "11", new List<string> { "gs1:productionDate" } },
            { "12", new List<string> { "gs1:dueDate" } },
            { "13", new List<string> { "gs1:packagingDate" } },
            { "15", new List<string> { "gs1:bestBeforeDate" } },
            { "16", new List<string> { "gs1:sellByDate" } },
            { "17", new List<string> { "gs1:expirationDate" } },
            { "4326", new List<string> { "gs1:releaseDate" } },
            { "7006", new List<string> { "gs1:firstFreezeDate" } }
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateSemantics"/> class.
    /// </summary>
    private DateSemantics()
        : base(_dateSemantics) {
    }

    /// <summary>
    /// Factory method to create a new <see cref="DateSemantics"/> instance.
    /// </summary>
    /// <returns>An <see cref="DateSemantics"/> instance.</returns>
    public static DateSemantics Create() => [];
}
