// --------------------------------------------------------------------------
// <copyright file="ExtractedData.cs" company="Solidsoft Reply Ltd.">
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
// Represents extracted data.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents extracted data.
/// </summary>
/// <param name="gs1DigitalLinkData">A dictionary of GS1 AIs.</param>
/// <param name="NonGs1KeyValuePairs">A dictionary of non-GS1 key-value pairs.</param>
/// <param name="OtherQueryStringContent">Any other query string content.</param>
/// <param name="FragmentSpecifier">A fragment specifier.</param>
/// <param name="UriStem">The URI stem.</param>
/// <param name="StructuredData">A structured representation of the GS1 elements in the Digital Link.</param>
/// <param name="Cursor">The current Cursor index.</param>
[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "This is a primary constructor.")]
internal record ExtractedData(
    Dictionary<string, string>? gs1DigitalLinkData,
    Dictionary<string, string>? NonGs1KeyValuePairs,
    string OtherQueryStringContent = "",
    string FragmentSpecifier = "",
    string UriStem = "",
    StructuredData? StructuredData = null,
    int Cursor = 0) {
}
