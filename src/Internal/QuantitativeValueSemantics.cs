// --------------------------------------------------------------------------
// <copyright file="QuantitativeValueSemantics.cs" company="Solidsoft Reply Ltd.">
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

/// <summary>
/// Represents a quantitative value semantics element.
/// </summary>
internal class QuantitativeValueSemantics : IReadOnlyList<QuantitativeValueSemanticsElement> {

    /// <summary>
    /// The quantitative value semantics.
    /// </summary>
    private static readonly IReadOnlyList<QuantitativeValueSemanticsElement> _quantativeValueSemantics;

    /// <summary>
    /// Initializes static members of the <see cref="QuantitativeValueSemantics"/> class.
    /// </summary>
    static QuantitativeValueSemantics() {
        _quantativeValueSemantics = new List<QuantitativeValueSemanticsElement>() {
            new QuantitativeValueSemanticsElement("3100", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3101", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3102", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3103", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3104", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3105", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3200", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3201", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3202", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3203", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3204", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3205", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3560", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new QuantitativeValueSemanticsElement("3561", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new QuantitativeValueSemanticsElement("3562", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new QuantitativeValueSemanticsElement("3563", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new QuantitativeValueSemanticsElement("3564", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new QuantitativeValueSemanticsElement("3565", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new QuantitativeValueSemanticsElement("3570", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new QuantitativeValueSemanticsElement("3571", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new QuantitativeValueSemanticsElement("3572", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new QuantitativeValueSemanticsElement("3573", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new QuantitativeValueSemanticsElement("3574", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new QuantitativeValueSemanticsElement("3575", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new QuantitativeValueSemanticsElement("3300", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3301", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3302", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3303", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3304", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3305", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new QuantitativeValueSemanticsElement("3400", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3401", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3402", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3403", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3404", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3405", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new QuantitativeValueSemanticsElement("3150", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new QuantitativeValueSemanticsElement("3151", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new QuantitativeValueSemanticsElement("3152", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new QuantitativeValueSemanticsElement("3153", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new QuantitativeValueSemanticsElement("3154", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new QuantitativeValueSemanticsElement("3155", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new QuantitativeValueSemanticsElement("3160", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new QuantitativeValueSemanticsElement("3161", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new QuantitativeValueSemanticsElement("3162", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new QuantitativeValueSemanticsElement("3163", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new QuantitativeValueSemanticsElement("3164", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new QuantitativeValueSemanticsElement("3165", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new QuantitativeValueSemanticsElement("3600", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new QuantitativeValueSemanticsElement("3601", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new QuantitativeValueSemanticsElement("3602", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new QuantitativeValueSemanticsElement("3603", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new QuantitativeValueSemanticsElement("3604", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new QuantitativeValueSemanticsElement("3605", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new QuantitativeValueSemanticsElement("3610", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new QuantitativeValueSemanticsElement("3611", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new QuantitativeValueSemanticsElement("3612", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new QuantitativeValueSemanticsElement("3613", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new QuantitativeValueSemanticsElement("3614", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new QuantitativeValueSemanticsElement("3615", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new QuantitativeValueSemanticsElement("3650", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new QuantitativeValueSemanticsElement("3651", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new QuantitativeValueSemanticsElement("3652", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new QuantitativeValueSemanticsElement("3653", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new QuantitativeValueSemanticsElement("3654", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new QuantitativeValueSemanticsElement("3655", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new QuantitativeValueSemanticsElement("3640", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new QuantitativeValueSemanticsElement("3641", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new QuantitativeValueSemanticsElement("3642", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new QuantitativeValueSemanticsElement("3643", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new QuantitativeValueSemanticsElement("3644", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new QuantitativeValueSemanticsElement("3645", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new QuantitativeValueSemanticsElement("3660", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new QuantitativeValueSemanticsElement("3661", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new QuantitativeValueSemanticsElement("3662", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new QuantitativeValueSemanticsElement("3663", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new QuantitativeValueSemanticsElement("3664", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new QuantitativeValueSemanticsElement("3665", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new QuantitativeValueSemanticsElement("3350", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new QuantitativeValueSemanticsElement("3351", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new QuantitativeValueSemanticsElement("3352", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new QuantitativeValueSemanticsElement("3353", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new QuantitativeValueSemanticsElement("3354", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new QuantitativeValueSemanticsElement("3355", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new QuantitativeValueSemanticsElement("3360", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new QuantitativeValueSemanticsElement("3361", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new QuantitativeValueSemanticsElement("3362", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new QuantitativeValueSemanticsElement("3363", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new QuantitativeValueSemanticsElement("3364", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new QuantitativeValueSemanticsElement("3365", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new QuantitativeValueSemanticsElement("3680", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new QuantitativeValueSemanticsElement("3681", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new QuantitativeValueSemanticsElement("3682", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new QuantitativeValueSemanticsElement("3683", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new QuantitativeValueSemanticsElement("3684", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new QuantitativeValueSemanticsElement("3685", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new QuantitativeValueSemanticsElement("3670", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new QuantitativeValueSemanticsElement("3671", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new QuantitativeValueSemanticsElement("3672", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new QuantitativeValueSemanticsElement("3673", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new QuantitativeValueSemanticsElement("3674", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new QuantitativeValueSemanticsElement("3675", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new QuantitativeValueSemanticsElement("3690", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new QuantitativeValueSemanticsElement("3691", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new QuantitativeValueSemanticsElement("3692", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new QuantitativeValueSemanticsElement("3693", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new QuantitativeValueSemanticsElement("3694", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new QuantitativeValueSemanticsElement("3695", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new QuantitativeValueSemanticsElement("3630", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new QuantitativeValueSemanticsElement("3631", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new QuantitativeValueSemanticsElement("3632", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new QuantitativeValueSemanticsElement("3633", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new QuantitativeValueSemanticsElement("3634", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new QuantitativeValueSemanticsElement("3635", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new QuantitativeValueSemanticsElement("3620", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new QuantitativeValueSemanticsElement("3621", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new QuantitativeValueSemanticsElement("3622", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new QuantitativeValueSemanticsElement("3623", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new QuantitativeValueSemanticsElement("3624", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new QuantitativeValueSemanticsElement("3625", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new QuantitativeValueSemanticsElement("3280", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3281", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3282", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3283", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3284", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3285", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3270", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3271", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3272", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3273", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3274", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3275", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3130", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3131", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3132", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3133", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3134", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3135", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3290", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3291", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3292", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3293", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3294", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3295", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3480", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3481", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3482", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3483", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3484", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3485", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new QuantitativeValueSemanticsElement("3470", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3471", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3472", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3473", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3474", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3475", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new QuantitativeValueSemanticsElement("3330", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3331", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3332", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3333", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3334", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3335", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new QuantitativeValueSemanticsElement("3490", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3491", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3492", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3493", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3494", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3495", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new QuantitativeValueSemanticsElement("3220", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3221", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3222", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3223", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3224", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3225", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3210", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3211", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3212", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3213", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3214", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3215", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3110", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3111", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3112", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3113", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3114", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3115", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3230", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3231", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3232", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3233", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3234", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3235", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3420", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3421", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3422", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3423", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3424", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3425", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new QuantitativeValueSemanticsElement("3410", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3411", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3412", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3413", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3414", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3415", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new QuantitativeValueSemanticsElement("3310", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3311", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3312", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3313", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3314", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3315", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new QuantitativeValueSemanticsElement("3430", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3431", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3432", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3433", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3434", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3435", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new QuantitativeValueSemanticsElement("3250", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3251", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3252", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3253", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3254", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3255", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3240", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3241", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3242", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3243", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3244", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3245", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3120", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3121", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3122", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3123", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3124", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3125", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3460", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3461", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3462", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3463", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3464", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3465", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3450", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3451", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3452", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3453", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3454", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3455", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new QuantitativeValueSemanticsElement("3440", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3441", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3442", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3443", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3444", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3445", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new QuantitativeValueSemanticsElement("3320", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3321", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3322", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3323", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3324", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3325", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new QuantitativeValueSemanticsElement("3460", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3461", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3462", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3463", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3464", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3465", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new QuantitativeValueSemanticsElement("3510", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3511", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3512", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3513", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3514", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3515", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3500", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new QuantitativeValueSemanticsElement("3501", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new QuantitativeValueSemanticsElement("3502", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new QuantitativeValueSemanticsElement("3503", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new QuantitativeValueSemanticsElement("3504", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new QuantitativeValueSemanticsElement("3505", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new QuantitativeValueSemanticsElement("3140", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3141", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3142", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3143", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3144", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3145", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3520", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3521", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3522", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3523", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3524", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3525", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3540", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3541", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3542", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3543", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3544", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3545", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new QuantitativeValueSemanticsElement("3530", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new QuantitativeValueSemanticsElement("3531", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new QuantitativeValueSemanticsElement("3532", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new QuantitativeValueSemanticsElement("3533", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new QuantitativeValueSemanticsElement("3534", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new QuantitativeValueSemanticsElement("3535", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new QuantitativeValueSemanticsElement("3340", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3341", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3342", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3343", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3344", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3345", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new QuantitativeValueSemanticsElement("3550", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3551", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3552", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3553", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3554", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3555", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new QuantitativeValueSemanticsElement("3370", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new QuantitativeValueSemanticsElement("3371", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new QuantitativeValueSemanticsElement("3372", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new QuantitativeValueSemanticsElement("3373", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new QuantitativeValueSemanticsElement("3374", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new QuantitativeValueSemanticsElement("3375", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28"))
        };
    }

    /// <summary>
    /// Gets the AI keys of the <see cref="QuantitativeValueSemantics"/>.
    /// </summary>
    public List<string> AiKeys => _quantativeValueSemantics.Select(x => x.AI).ToList();

    /// <summary>
    /// Gets the number of elements contained in the <see cref="QuantitativeValueSemantics"/>.
    /// </summary>
    public int Count =>
        _quantativeValueSemantics.Count;

    /// <summary>
    /// Gets the <see cref="QuantitativeValueSemantics"/> element at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>The <see cref="QuantitativeValueSemanticsElement"/> element at the specified index.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public QuantitativeValueSemanticsElement this[int index] => _quantativeValueSemantics[index];

    /// <summary>
    /// Gets the <see cref="QuantitativeValueSemantics"/> element at the specified index.
    /// </summary>
    /// <param name="aiKey">The GS1 AI.</param>
    /// <returns>The <see cref="QuantitativeValueSemanticsElement"/> element at the specified index.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public QuantitativeValueSemanticsItem this[string aiKey] {
        get => _quantativeValueSemantics.Where(e => e.AI == aiKey).FirstOrDefault()?.QuantitativeValueSemanticsItem ?? new QuantitativeValueSemanticsItem([], string.Empty);
        set => throw new NotImplementedException();
    }

    /// <summary>
    /// Factory method to create a new <see cref="QuantitativeValueSemantics"/> instance.
    /// </summary>
    /// <returns>An <see cref="QuantitativeValueSemantics"/> instance.</returns>
    public static QuantitativeValueSemantics Create() => [];

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="QuantitativeValueSemantics"/>.
    /// </summary>
    /// <returns>An enumerator that iterates through the <see cref="QuantitativeValueSemantics"/>.</returns>
    public IEnumerator<QuantitativeValueSemanticsElement> GetEnumerator() =>
        _quantativeValueSemantics.GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="QuantitativeValueSemantics"/>.
    /// </summary>
    /// <returns>An enumerator that iterates through the <see cref="QuantitativeValueSemantics"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _quantativeValueSemantics.GetEnumerator();
}