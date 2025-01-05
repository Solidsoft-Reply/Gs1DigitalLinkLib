﻿// --------------------------------------------------------------------------
// <copyright file="ClassSemantics.cs" company="Solidsoft Reply Ltd.">
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
// Represents a class semantics item.
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
/// Represents a class semantics item.
/// </summary>
internal class ClassSemantics : BaseSemantics {

    /// <summary>
    /// The class semantics.
    /// </summary>
    private static readonly Dictionary<string, IList<string>> _classSemantics;

    /// <summary>
    /// Initializes static members of the <see cref="ClassSemantics"/> class.
    /// </summary>
    static ClassSemantics() {
        _classSemantics = new () {
            { "01", new List<string> { "gs1:Product", "schema:Product" } },
            { "414", new List<string> { "gs1:Place", "schema:Place" } },
            { "417", new List<string> { "gs1:Organization", "schema:Organization" } },
            { "00", new List<string> { "gs1:LogisticUnit" } },
            { "253", new List<string> { "gs1:Document", "foaf:Document", "schema:CreativeWork" } },
            { "255", new List<string> { "gs1:Coupon" } },
            { "401", new List<string> { "gs1:Consignment" } },
            { "402", new List<string> { "gs1:Shipment" } },
            { "8003", new List<string> { "gs1:ReturnableAsset" } },
            { "8004", new List<string> { "gs1:IndividualAsset" } },
            { "8006", new List<string> { "gs1:IndividualTradeItemPiece", "gs1:Product", "schema:Product" } },
            { "8010", new List<string> { "gs1:Component" } },
            { "8013", new List<string> { "gs1:ProductModel" } },
            { "8017", new List<string> { "gs1:ServiceProvider" } },
            { "8018", new List<string> { "gs1:ServiceRecipient" } },
            { "8019", new List<string> { "gs1:ServiceInstance" } }
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassSemantics"/> class.
    /// </summary>
    private ClassSemantics()
        : base(_classSemantics) {
    }

    /// <summary>
    /// Factory method to create a new <see cref="ClassSemantics"/> instance.
    /// </summary>
    /// <returns>An <see cref="ClassSemantics"/> instance.</returns>
    public static ClassSemantics Create() => [];
}