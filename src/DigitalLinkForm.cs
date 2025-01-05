// --------------------------------------------------------------------------
// <copyright file="DigitalLinkForm.cs" company="Solidsoft Reply Ltd.">
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
// The form of a Digital Link URI.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib;

/// <summary>
/// The form of a Digital Link URI.
/// </summary>
public enum DigitalLinkForm {

    /// <summary>
    /// The Digital Link URI form cannot be determined.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Uncompressed GS1 Digital Link.
    /// </summary>
    Uncompressed = 0,

    /// <summary>
    /// Partially compressed GS1 Digital Link.
    /// </summary>
    PartiallyCompressed,

    /// <summary>
    /// Fully compressed GS1 Digital Link.
    /// </summary>
    Compressed
}