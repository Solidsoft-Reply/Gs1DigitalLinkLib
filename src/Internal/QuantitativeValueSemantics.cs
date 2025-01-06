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
        _quantativeValueSemantics = [
            new ("3100", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new ("3101", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new ("3102", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new ("3103", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new ("3104", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new ("3105", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "KGM")),
            new ("3200", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new ("3201", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new ("3202", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new ("3203", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new ("3204", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new ("3205", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "LBR")),
            new ("3560", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new ("3561", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new ("3562", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new ("3563", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new ("3564", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new ("3565", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "APZ")),
            new ("3570", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new ("3571", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new ("3572", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new ("3573", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new ("3574", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new ("3575", new QuantitativeValueSemanticsItem(["gs1:netWeight"], "ONZ")),
            new ("3300", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new ("3301", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new ("3302", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new ("3303", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new ("3304", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new ("3305", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "KGM")),
            new ("3400", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new ("3401", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new ("3402", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new ("3403", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new ("3404", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new ("3405", new QuantitativeValueSemanticsItem(["gs1:grossWeight"], "LBR")),
            new ("3150", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new ("3151", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new ("3152", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new ("3153", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new ("3154", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new ("3155", new QuantitativeValueSemanticsItem(["gs1:netContent"], "LTR")),
            new ("3160", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new ("3161", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new ("3162", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new ("3163", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new ("3164", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new ("3165", new QuantitativeValueSemanticsItem(["gs1:netContent"], "MTQ")),
            new ("3600", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new ("3601", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new ("3602", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new ("3603", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new ("3604", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new ("3605", new QuantitativeValueSemanticsItem(["gs1:netContent"], "QT")),
            new ("3610", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new ("3611", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new ("3612", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new ("3613", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new ("3614", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new ("3615", new QuantitativeValueSemanticsItem(["gs1:netContent"], "GLL")),
            new ("3650", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new ("3651", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new ("3652", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new ("3653", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new ("3654", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new ("3655", new QuantitativeValueSemanticsItem(["gs1:netContent"], "FTQ")),
            new ("3640", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new ("3641", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new ("3642", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new ("3643", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new ("3644", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new ("3645", new QuantitativeValueSemanticsItem(["gs1:netContent"], "INQ")),
            new ("3660", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new ("3661", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new ("3662", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new ("3663", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new ("3664", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new ("3665", new QuantitativeValueSemanticsItem(["gs1:netContent"], "YDQ")),
            new ("3350", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new ("3351", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new ("3352", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new ("3353", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new ("3354", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new ("3355", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "LTR")),
            new ("3360", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new ("3361", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new ("3362", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new ("3363", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new ("3364", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new ("3365", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "MTQ")),
            new ("3680", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new ("3681", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new ("3682", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new ("3683", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new ("3684", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new ("3685", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "FTQ")),
            new ("3670", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new ("3671", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new ("3672", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new ("3673", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new ("3674", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new ("3675", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "INQ")),
            new ("3690", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new ("3691", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new ("3692", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new ("3693", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new ("3694", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new ("3695", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "YDQ")),
            new ("3630", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new ("3631", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new ("3632", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new ("3633", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new ("3634", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new ("3635", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "GLL")),
            new ("3620", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new ("3621", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new ("3622", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new ("3623", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new ("3624", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new ("3625", new QuantitativeValueSemanticsItem(["gs1:grossVolume"], "QT")),
            new ("3280", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new ("3281", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new ("3282", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new ("3283", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new ("3284", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new ("3285", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "FOT")),
            new ("3270", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new ("3271", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new ("3272", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new ("3273", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new ("3274", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new ("3275", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "INH")),
            new ("3130", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new ("3131", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new ("3132", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new ("3133", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new ("3134", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new ("3135", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "MTR")),
            new ("3290", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new ("3291", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new ("3292", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new ("3293", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new ("3294", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new ("3295", new QuantitativeValueSemanticsItem(["gs1:outOfPackageDepth"], "YRD")),
            new ("3480", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new ("3481", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new ("3482", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new ("3483", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new ("3484", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new ("3485", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "FOT")),
            new ("3470", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new ("3471", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new ("3472", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new ("3473", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new ("3474", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new ("3475", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "INH")),
            new ("3330", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new ("3331", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new ("3332", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new ("3333", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new ("3334", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new ("3335", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "MTR")),
            new ("3490", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new ("3491", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new ("3492", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new ("3493", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new ("3494", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new ("3495", new QuantitativeValueSemanticsItem(["gs1:inPackageDepth"], "YRD")),
            new ("3220", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new ("3221", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new ("3222", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new ("3223", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new ("3224", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new ("3225", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "FOT")),
            new ("3210", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new ("3211", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new ("3212", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new ("3213", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new ("3214", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new ("3215", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "INH")),
            new ("3110", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new ("3111", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new ("3112", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new ("3113", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new ("3114", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new ("3115", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "MTR")),
            new ("3230", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new ("3231", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new ("3232", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new ("3233", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new ("3234", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new ("3235", new QuantitativeValueSemanticsItem(["gs1:outOfPackageLength"], "YRD")),
            new ("3420", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new ("3421", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new ("3422", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new ("3423", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new ("3424", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new ("3425", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "FOT")),
            new ("3410", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new ("3411", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new ("3412", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new ("3413", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new ("3414", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new ("3415", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "INH")),
            new ("3310", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new ("3311", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new ("3312", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new ("3313", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new ("3314", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new ("3315", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "MTR")),
            new ("3430", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new ("3431", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new ("3432", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new ("3433", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new ("3434", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new ("3435", new QuantitativeValueSemanticsItem(["gs1:inPackageLength"], "YRD")),
            new ("3250", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new ("3251", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new ("3252", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new ("3253", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new ("3254", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new ("3255", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "FOT")),
            new ("3240", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new ("3241", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new ("3242", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new ("3243", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new ("3244", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new ("3245", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "INH")),
            new ("3120", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new ("3121", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new ("3122", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new ("3123", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new ("3124", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new ("3125", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "MTR")),
            new ("3460", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new ("3461", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new ("3462", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new ("3463", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new ("3464", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new ("3465", new QuantitativeValueSemanticsItem(["gs1:outOfPackageWidth"], "YRD")),
            new ("3450", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new ("3451", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new ("3452", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new ("3453", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new ("3454", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new ("3455", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "FOT")),
            new ("3440", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new ("3441", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new ("3442", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new ("3443", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new ("3444", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new ("3445", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "INH")),
            new ("3320", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new ("3321", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new ("3322", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new ("3323", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new ("3324", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new ("3325", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "MTR")),
            new ("3460", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new ("3461", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new ("3462", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new ("3463", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new ("3464", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new ("3465", new QuantitativeValueSemanticsItem(["gs1:inPackageWidth"], "YRD")),
            new ("3510", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new ("3511", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new ("3512", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new ("3513", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new ("3514", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new ("3515", new QuantitativeValueSemanticsItem(["gs1:netArea"], "FTK")),
            new ("3500", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new ("3501", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new ("3502", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new ("3503", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new ("3504", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new ("3505", new QuantitativeValueSemanticsItem(["gs1:netArea"], "INK")),
            new ("3140", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new ("3141", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new ("3142", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new ("3143", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new ("3144", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new ("3145", new QuantitativeValueSemanticsItem(["gs1:netArea"], "MTK")),
            new ("3520", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new ("3521", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new ("3522", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new ("3523", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new ("3524", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new ("3525", new QuantitativeValueSemanticsItem(["gs1:netArea"], "YDK")),
            new ("3540", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new ("3541", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new ("3542", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new ("3543", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new ("3544", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new ("3545", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "FTK")),
            new ("3530", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new ("3531", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new ("3532", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new ("3533", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new ("3534", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new ("3535", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "INK")),
            new ("3340", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new ("3341", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new ("3342", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new ("3343", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new ("3344", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new ("3345", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "MTK")),
            new ("3550", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new ("3551", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new ("3552", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new ("3553", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new ("3554", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new ("3555", new QuantitativeValueSemanticsItem(["gs1:grossArea"], "YDK")),
            new ("3370", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new ("3371", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new ("3372", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new ("3373", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new ("3374", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28")),
            new ("3375", new QuantitativeValueSemanticsItem(["gs1:massPerUnitArea"], "28"))
        ];
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