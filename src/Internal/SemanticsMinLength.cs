// --------------------------------------------------------------------------
// <copyright file="SemanticsMinLength.cs" company="Solidsoft Reply Ltd.">
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
// Represents a minimum length semantics item.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

/// <summary>
/// Represents a minimum length semantics item.
/// </summary>
internal struct SemanticsMinLength : ISemantics {

    /// <summary>
    /// The minimum length of the semantics.
    /// </summary>
    private int value;

    /// <summary>
    /// Initializes a new instance of the <see cref="SemanticsMinLength"/> struct.
    /// </summary>
    /// <param name="initialValue">The initial value of the minimum length.</param>
    public SemanticsMinLength(int initialValue) {
        value = initialValue;
    }

    /// <summary>
    /// Gets or sets the value of the semantics.
    /// </summary>
    public int Value {
        get { return value; }
        set { this.value = value; }
    }
}
