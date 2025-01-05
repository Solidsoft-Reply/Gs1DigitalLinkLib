// --------------------------------------------------------------------------
// <copyright file="ExpectedLength.cs" company="Solidsoft Reply Ltd.">
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
// Represents an expected length of an AI value.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents an expected length of an AI value.
/// </summary>
internal class ExpectedLength : IExpected {

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpectedLength"/> class.
    /// </summary>
    /// <param name="dataType">The AI value data type (numeric or alphanumeric).</param>
    /// <param name="length">The length of the AI value.</param>
    public ExpectedLength(ExpectedTypes dataType, int length) {
        DataType = dataType;
        Value = length;
    }

    /// <summary>
    /// Gets the expected data type. May be numeric or alphanumeric.
    /// </summary>
    public ExpectedTypes DataType { get; init; }

    /// <summary>
    /// Gets the length of the AI value.
    /// </summary>
    public int Value { get; init; }
}
