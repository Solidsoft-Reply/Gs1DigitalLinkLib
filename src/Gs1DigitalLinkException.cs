// --------------------------------------------------------------------------
// <copyright file="Gs1DigitalLinkException.cs" company="Solidsoft Reply Ltd.">
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
/// Represents an exception that occurs when handling GS1 Digital Links.
/// </summary>
[Serializable]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "VSSpell001:Spell Check", Justification = "Represents the term 'API'")]
public class Gs1DigitalLinkException : Exception {

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkException"/> class.
    /// </summary>
    public Gs1DigitalLinkException() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkException"/> class with
    /// a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public Gs1DigitalLinkException(string message)
        : base(message) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkException"/> class with
    /// a specified error message and error code.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="apiCall">The API call that resulted in this exception.</param>
    public Gs1DigitalLinkException(string message, string? apiCall)
        : base(message) {
        ApiCall = apiCall ?? string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkException"/> class with
    /// a specified error message and a reference to the inner exception that is the
    /// cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public Gs1DigitalLinkException(string message, Exception? innerException)
        : base(message, innerException) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Gs1DigitalLinkException"/> class with
    /// a specified error message, an error code, and a reference to the inner exception
    /// that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="apiCall">The API call that resulted in this exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public Gs1DigitalLinkException(string message, string? apiCall, Exception? innerException)
        : base(message, innerException) {
        ApiCall = apiCall ?? string.Empty;
    }

    /// <summary>
    /// Gets an optional numeric code or other identifier for this error.
    /// </summary>
    public string ApiCall { get; } = string.Empty;
}