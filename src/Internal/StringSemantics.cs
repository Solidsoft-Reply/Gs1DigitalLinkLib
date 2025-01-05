// --------------------------------------------------------------------------
// <copyright file="StringSemantics.cs" company="Solidsoft Reply Ltd.">
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
// Represents a string semantics item.
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
/// Represents a string semantics item.
/// </summary>
internal class StringSemantics : BaseSemantics {

    /// <summary>
    /// The string semantics.
    /// </summary>
    private static readonly Dictionary<string, IList<string>> _stringSemantics;

    /// <summary>
    /// Initializes static members of the <see cref="StringSemantics"/> class.
    /// </summary>
    static StringSemantics() {
        _stringSemantics = new () {
            { "00", new List<string> { "gs1:sscc" } },
            { "01", new List<string> { "gs1:gtin", "schema:gtin" } },
            { "10", new List<string> { "gs1:hasBatchLot" } },
            { "21", new List<string> { "gs1:hasSerialNumber" } },
            { "22", new List<string> { "gs1:consumerProductVariant" } },
            { "235", new List<string> { "gs1:hasThirdPartyControlledSerialNumber" } },
            { "253", new List<string> { "gs1:gdti" } },
            { "254", new List<string> { "gs1:hasGLNextension" } },
            { "255", new List<string> { "gs1:gcn" } },
            { "401", new List<string> { "gs1:ginc" } },
            { "402", new List<string> { "gs1:gsin" } },
            { "403", new List<string> { "gs1:routingCode" } },
            { "414", new List<string> { "gs1:locationGLN" } },
            { "417", new List<string> { "gs1:partyGLN" } },
            { "4309", new List<string> { "gs1:geocode" } },
            { "4320", new List<string> { "gs1:serviceCodeDescription" } },
            { "8003", new List<string> { "gs1:grai" } },
            { "8004", new List<string> { "gs1:giai" } },
            { "8006", new List<string> { "gs1:itip" } },
            { "8010", new List<string> { "gs1:cpid" } },
            { "8011", new List<string> { "gs1:hasCPIDSerialNumber" } },
            { "8013", new List<string> { "gs1:gmn" } },
            { "8017", new List<string> { "gs1:gsrnp" } },
            { "8018", new List<string> { "gs1:gsrn" } },
            { "8019", new List<string> { "gs1:srin" } }
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringSemantics"/> class.
    /// </summary>
    private StringSemantics()
        : base(_stringSemantics) {
    }

    /// <summary>
    /// Factory method to create a new <see cref="StringSemantics"/> instance.
    /// </summary>
    /// <returns>An <see cref="StringSemantics"/> instance.</returns>
    public static StringSemantics Create() => [];
}