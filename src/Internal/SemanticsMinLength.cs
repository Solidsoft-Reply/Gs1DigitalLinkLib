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
/// <param name="initalValue">The initial value of the minimum length.</param>
#pragma warning disable SA1009 // Closing parenthesis should be spaced correctly
internal readonly struct SemanticsMinLength(int initalValue) : ISemantics {
#pragma warning restore SA1009 // Closing parenthesis should be spaced correctly
    /// <summary>
    /// The minimum length of the semantics.
    /// </summary>
    private readonly int value = initalValue;

    /// <summary>
    /// Gets the value of the semantics.
    /// </summary>
    public readonly int Value => value;
}