// --------------------------------------------------------------------------
// <copyright file="Gs1DigitalLink.cs" company="Solidsoft Reply Ltd.">
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
// Represents a GS1 Digital Link.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// Represents a GS1 Digital Link.
/// </summary>
public class Gs1DigitalLink {

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="uri">A GS1 Digital Link URI character span.</param>
    public Gs1DigitalLink(ReadOnlySpan<char> uri) {
        if (Uri.TryCreate(uri.ToString(), UriKind.RelativeOrAbsolute, out Uri? createdUri)) {
            Value = createdUri.ToString();
            Uri = createdUri;
        }
        else {
            Value = string.Empty;
            Uri = null;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="uri">A GS1 Digital Link URI.</param>
    public Gs1DigitalLink(Uri uri)
        : this(uri.OriginalString.AsSpan()) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="uri">A GS1 Digital Link URI string.</param>
    public Gs1DigitalLink(string uri)
        : this(uri.AsSpan()) {
    }

    /// <summary>
    /// Gets the Digital Link URI as a string.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// Gets the Digital Link URI.
    /// </summary>
    public Uri? Uri { get; init; }

    /// <summary>
    /// Analyses a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <param name="extended">
    /// If true, the method performs an extended analysis, returning semantics, a
    /// structured representation of GS1 elements and an element string.
    /// </param>
    /// <returns>Analysis results.</returns>
    public static UriAnalytics AnalyseUri(Uri uri, bool extended = false) =>
        DigitalLinkConvert.AnalyseUri(uri, extended, nameof(AnalyseUri), nameof(uri));

    /// <summary>
    /// Analyses a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <param name="extended">
    /// If true, the method performs an extended analysis, returning semantics, a
    /// structured representation of GS1 elements and an element string.
    /// </param>
    /// <returns>Analysis results.</returns>
    public static UriAnalytics AnalyseUri(string uri, bool extended = false) =>
        DigitalLinkConvert.AnalyseUri(uri, extended, nameof(AnalyseUri), nameof(uri));

    /// <summary>
    /// Analyses the semantics of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <returns>Semantic Analysis results.</returns>
    public static UriSemantics AnalyseUriSemantics(Uri uri) =>
        DigitalLinkConvert.AnalyseUriSemantics(uri.ToString());

    /// <summary>
    /// Analyses the semantics of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <returns>Semantic Analysis results.</returns>
    public static UriSemantics AnalyseUriSemantics(string uri) =>
        DigitalLinkConvert.AnalyseUriSemantics(uri);

    /// <summary>
    /// Gets the GS1 Digital Link URI as a string.
    /// </summary>
    /// <returns>A string representation of the Digital Link.</returns>
    public override string ToString() => Value;

    /// <summary>
    /// Gets the GS1 Digital Link Uri as a span.
    /// </summary>
    /// <returns>The value as a span.</returns>
    public ReadOnlySpan<char> AsSpan() => Value.AsSpan();
}