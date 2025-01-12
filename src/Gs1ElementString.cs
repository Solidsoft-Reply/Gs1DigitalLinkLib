// --------------------------------------------------------------------------
// <copyright file="Gs1ElementString.cs" company="Solidsoft Reply Ltd.">
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
// Represents a GS1 Element String.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

using Microsoft.Extensions.Logging;

/// <summary>
/// Represents a GS1 Element String.
/// </summary>
public class Gs1ElementString() {

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1ElementString"/> class.
    /// </summary>
    /// <param name="elementStringSpan">A GS1 Element String character span.</param>
    /// <param name="noValidation">
    /// If true, the GS1 element string is not validated. It may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 element string is invalid.</exception>
    public Gs1ElementString(ReadOnlySpan<char> elementStringSpan, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null)
        : this(elementStringSpan.ToString(), noValidation, logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1ElementString"/> class.
    /// </summary>
    /// <param name="gs1ElementStringOrDigitalLink">A GS1 Element String or Digital Link URI.</param>
    /// <param name="noValidation">
    /// If true, the GS1 element string is not validated. It may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 element string or Digital Link URI is invalid.</exception>
    public Gs1ElementString(string gs1ElementStringOrDigitalLink, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null)
        : this() {
        if (string.IsNullOrWhiteSpace(gs1ElementStringOrDigitalLink)) {
            var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, nameof(Gs1DigitalLinkData), nameof(gs1ElementStringOrDigitalLink));
            var message = Resources.Errors.ErrorMsgElementStringCannotBeNullOrEmpty;
            throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidElementString, apiCall, message, logger: logger);
        }

        Value = string.Empty;

        try {
            if (!noValidation) {
                // Generate a Digital Link to validate the element string data
                _ = gs1ElementStringOrDigitalLink.ToGs1DigitalLink();
            }

            // If we get here, and validations have been performed,
            // the Element String contains valid data.
            Value = gs1ElementStringOrDigitalLink;
        }
        catch (Gs1DigitalLinkException gs1DlEx) {
            try {
                var gs1ElementString = gs1ElementStringOrDigitalLink.ToGs1ElementString();

                Value = gs1ElementString.Value;
            }
            catch {
                var apiCall = string.Format(Resources.Errors.ErrorMsgPart0Param1, nameof(Gs1DigitalLinkData), nameof(gs1ElementStringOrDigitalLink));
                throw ConversionExtensionMethods.LogAndReturnException(Resources.Errors.ErrorTypeInvalidElementString, apiCall, gs1DlEx.Message, logger: logger);
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1ElementString"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkData">GS1 Digital Link data.</param>
    /// <param name="noValidation">
    /// If true, the GS1 element string is not validated. It may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 Digital Link data is invalid.</exception>
    public Gs1ElementString(Gs1DigitalLinkData gs1DigitalLinkData, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLinkData.ToGs1ElementString().Value, noValidation, logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1ElementString"/> class.
    /// </summary>
    /// <param name="gs1DigitalLink">A GS1 Digital Link.</param>
    /// <param name="noValidation">
    /// If true, the GS1 element string is not validated. It may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1 Digital Link is invalid.</exception>
    public Gs1ElementString(Gs1DigitalLink gs1DigitalLink, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLink.Value, noValidation, logger) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1ElementString"/> class.
    /// </summary>
    /// <param name="gs1DigitalLinkUri">A GS1 Digital Link URI.</param>
    /// <param name="noValidation">
    /// If true, the GS1 element string is not validated. It may contain invalid AIs and AI values.
    /// </param>
    /// <param name="logger">A logger.</param>
    /// <exception cref="Gs1DigitalLinkException">The GS1  Digital Link URI is invalid.</exception>
    public Gs1ElementString(Uri gs1DigitalLinkUri, bool noValidation = false, ILogger<Gs1DigitalLink>? logger = null)
        : this(gs1DigitalLinkUri.ToGs1ElementString().Value, noValidation, logger) {
    }

    /// <summary>
    /// Gets the Digital Link URI as a string.
    /// </summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>
    /// Gets the GS1 Element String as a span.
    /// </summary>
    /// <returns>The GS1 Element String as a span.</returns>
    public ReadOnlySpan<char> AsSpan() => Value.AsSpan();

    /// <summary>
    /// Gets the GS1 Digital Link URI as a string.
    /// </summary>
    /// <returns>The GS1 Element String as a string.</returns>
    public override string ToString() => Value;
}