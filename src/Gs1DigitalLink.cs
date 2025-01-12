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

using Microsoft.Extensions.Logging;

/// <summary>
/// Represents a GS1 Digital Link.
/// </summary>
public class Gs1DigitalLink {

    /// <summary>
    /// A logger.
    /// </summary>
    private readonly ILogger<Gs1DigitalLink>? _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkUriOrElementStringAsSpan">A GS1 Digital Link URI or element string character span.</param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 element string is invalid.</exception>
    public Gs1DigitalLink(ReadOnlySpan<char> gs1DigitalLinkUriOrElementStringAsSpan, ILogger<Gs1DigitalLink>? logger = null) {
        _logger = logger;
        Value = string.Empty;
        var gs1DigitalLinkUriOrElementString = gs1DigitalLinkUriOrElementStringAsSpan.ToString();

        if (string.IsNullOrWhiteSpace((string)gs1DigitalLinkUriOrElementString)) {
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, nameof(Gs1DigitalLinkLib.Gs1DigitalLinkData), nameof(gs1DigitalLinkUriOrElementStringAsSpan));
            var message = Resources.Errors.ErrorMsgTheDigitalLinkUriCannotBeNullOrEmpty;
            throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, logger: logger ?? _logger);
        }

        try {
            // Validate the Digital Link
            Validate(gs1DigitalLinkUriOrElementString, nameof(Gs1DigitalLink), nameof(gs1DigitalLinkUriOrElementString), logger ?? _logger);

            // If we get here, the Digital Link is valid.
            Value = gs1DigitalLinkUriOrElementString;
            Uri = new Uri(gs1DigitalLinkUriOrElementString);
        }
        catch (Gs1DigitalLinkException gs1DlEx) {
            try {
                var innerGs1DigitalLink = gs1DigitalLinkUriOrElementString.ToGs1DigitalLink();

                Value = innerGs1DigitalLink.Value;
                Uri = innerGs1DigitalLink.Uri;
            }
            catch {
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, nameof(Gs1DigitalLinkData), nameof(gs1DigitalLinkUriOrElementStringAsSpan));
                throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, gs1DlEx.Message, logger: logger ?? _logger);
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">A GS1 Digital Link URI.</param>
    /// <param name="logger">A logger.</param>
    public Gs1DigitalLink(Uri gs1DigitalLinkUri, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLinkUri.OriginalString.AsSpan(), logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkUriOrElementString">A GS1 Digital Link URI or element string.</param>
    /// <param name="logger">A logger.</param>
    public Gs1DigitalLink(string gs1DigitalLinkUriOrElementString, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLinkUriOrElementString.AsSpan(), logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="gs1ElementString">A GS1 element string.</param>
    /// <param name="logger">A logger.</param>
    public Gs1DigitalLink(Gs1ElementString gs1ElementString, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1ElementString.Value.AsSpan(), logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLink"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkData">A GS1 Digital Link data.</param>
    /// <param name="logger">A logger.</param>
    public Gs1DigitalLink(Gs1DigitalLinkData gs1DigitalLinkData, ILogger<Gs1DigitalLink>? logger = null) {
        _logger = logger;
        var innerDigitalLink = gs1DigitalLinkData.ToGs1DigitalLink();
        Value = innerDigitalLink.Value;
        Uri = innerDigitalLink.Uri;
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
    public static UriAnalysis AnalyseUri(Uri uri, bool extended = false) =>
        Gs1DigitalLinkConvert.AnalyseUri(uri, extended, nameof(AnalyseUri), nameof(uri));

    /// <summary>
    /// Analyses a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <param name="extended">
    /// If true, the method performs an extended analysis, returning semantics, a
    /// structured representation of GS1 elements and an element string.
    /// </param>
    /// <returns>Analysis results.</returns>
    public static UriAnalysis AnalyseUri(string uri, bool extended = false) =>
        Gs1DigitalLinkConvert.AnalyseUri(uri, extended, nameof(AnalyseUri), nameof(uri));

    /// <summary>
    /// Analyses the semantics of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <returns>Semantic Analysis results.</returns>
    public static UriSemantics AnalyseUriSemantics(Uri uri) =>
        Gs1DigitalLinkConvert.AnalyseUriSemantics(uri.ToString());

    /// <summary>
    /// Analyses the semantics of a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <returns>Semantic Analysis results.</returns>
    public static UriSemantics AnalyseUriSemantics(string uri) =>
        Gs1DigitalLinkConvert.AnalyseUriSemantics(uri);

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

    /// <summary>
    /// Validates a GS1 Digital Link URI.
    /// </summary>
    /// <param name="uri">The Digital Link URI.</param>
    /// <param name="methodName">The public method name.</param>
    /// <param name="paramName">The public method parameter name.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 element string is invalid.</exception>
    private void Validate(string uri, string methodName, string paramName, ILogger? logger = null) {
        var uriAnalysis = AnalyseUri(uri);

        if (uriAnalysis.DetectedForm == DigitalLinkForm.Unknown) {
            var message = Resources.Errors.ErrorMsgUnableToDetermineTheFormOfTheDigitalLink;
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, methodName, paramName);
            throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidGs1DigitalLink, apiCall, message, logger: logger ?? logger ?? _logger);
        }
        else if (uriAnalysis.DetectedForm != DigitalLinkForm.Uncompressed) {
            // Decompress the URI if it is compressed, and re-analyse it.
            // We would generally expect that compression has only been applied
            // to valid Digital Links, but it is possible that a compressed
            // Digital Link may be invalid.
            uri = uri.ChangeGs1CompressionLevel(CompressionLevel.Uncompressed).Value;
            uriAnalysis = AnalyseUri(uri);
        }

        // Get the full list of GS1 AI pairs and then validate the analysed data.
        var gs1Pairs = Gs1DigitalLinkConvert.GetGs1AiPairs(uriAnalysis);
        Gs1DigitalLinkConvert.ValidateDigitalLink(uriAnalysis, gs1Pairs, methodName, paramName);
    }
}