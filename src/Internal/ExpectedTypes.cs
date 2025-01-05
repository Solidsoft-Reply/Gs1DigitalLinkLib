// --------------------------------------------------------------------------
// <copyright file="ExpectedTypes.cs" company="Solidsoft Reply Ltd.">
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
// Represents the expected data type of a GS1 AI value.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents the expected data type of a GS1 AI value.
/// </summary>
internal enum ExpectedTypes {
    /// <summary>
    /// The expected data type is numeric.
    /// </summary>
    Numeric,

    /// <summary>
    /// The expected data type is alphanumeric.
    /// </summary>
    Alphanumeric
}
