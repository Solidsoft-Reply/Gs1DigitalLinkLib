// --------------------------------------------------------------------------
// <copyright file="DataResources.cs" company="Solidsoft Reply Ltd.">
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
// A set of data resources for analysing GS1 Digital Links.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// A set of data resources for analysing GS1 Digital Links.
/// </summary>
public static class DataResources {

    /// <summary>
    /// Gets the GS1 Application Identifier separator character.
    /// </summary>
    public static string GroupSeparator => "\u001D";

    /// <summary>
    /// Gets a table of GS1 Application Identifier specifications.
    /// </summary>
    public static AiTable ApplicationIdentifiers { get; } = AiTable.Create();

    /// <summary>
    /// Gets a table of GS1 Application Identifier check digit positions.
    /// </summary>
    public static CheckDigitPositionTable CheckDigitPositions { get; } = CheckDigitPositionTable.Create();

    /// <summary>
    /// Gets a table of GS1 Application Identifier validation expressions.
    /// </summary>
    public static ValidationExpressionTable ValidationExpressions { get; } = ValidationExpressionTable.Create();

    /// <summary>
    /// Gets a set of GS1 Application Identifiers categories.
    /// </summary>
    public static Maps Maps { get; } = DigitalLinkConvert.AiMaps;

    /// <summary>
    /// Gets a table of GS1 Application Identifier qualifiers.
    /// </summary>
    public static QualifiersTable Qualifiers { get; } = QualifiersTable.Create();

    /// <summary>
    ///  Gets a list of GS1 Application Identifiers with 2-digit numeric values.
    /// </summary>
    public static TwoDigitAIs TwoDigitAIs { get; } = TwoDigitAIs.Create();

    /// <summary>
    ///  Gets a list of GS1 Application Identifiers with 3-digit numeric values.
    /// </summary>
    public static ThreeDigitAIs ThreeDigitAIs { get; } = ThreeDigitAIs.Create();

    /// <summary>
    ///  Gets a list of GS1 Application Identifiers with 4-digit numeric values.
    /// </summary>
    public static FourDigitAIs FourDigitAIs { get; } = FourDigitAIs.Create();

    /// <summary>
    /// Gets a table of GS1 Application Identifier short names.
    /// </summary>
    [Obsolete("This property supports the use of short names ('convenience alphas') which are obsolete. This property is retained for legacy purposes, only. It may be removed in a future version.")]
    public static ShortNamesTable ShortNames { get; } = ShortNamesTable.Create();

    /// <summary>
    /// Gets a table that maps short names to GS1 Application Identifiers.
    /// </summary>
    [Obsolete("This property supports the use of short names ('convenience alphas') which are obsolete. This property is retained for legacy purposes, only. It may be removed in a future version.")]
    public static ShortNamesToAIsTable ShortNamesToNumeric { get; } = ShortNamesToAIsTable.Create();
}