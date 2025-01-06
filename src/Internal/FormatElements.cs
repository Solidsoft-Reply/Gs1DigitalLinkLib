// --------------------------------------------------------------------------
// <copyright file="FormatElements.cs" company="Solidsoft Reply Ltd.">
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
// Represents a list of format elements.
// </summary>
// --------------------------------------------------------------------------

namespace Solidsoft.Reply.Gs1DigitalLinkLib.Internal;

using System.Collections;

/// <summary>
/// Represents a list of format elements.
/// </summary>
internal class FormatElements : IList<IExpected> {

    /// <summary>
    /// The list of format elements.
    /// </summary>
    private readonly List<IExpected> _formatElements = [];

    /// <summary>
    /// Gets a value indicating whether the list is read-only.
    /// </summary>
    public bool IsReadOnly =>
        false;

    /// <summary>
    /// Gets the number of elements contained in the list.
    /// </summary>
    public int Count =>
        _formatElements.Count;

    /// <summary>
    /// Gets or sets the format element at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns>A format element.</returns>
    public IExpected this[int index] {
        get => _formatElements[index];
        set => _formatElements[index] = value;
    }

    /// <summary>
    /// Adds an element to the list.
    /// </summary>
    /// <param name="item">The element.</param>
    public void Add(IExpected item) =>
        _formatElements.Add(item);

    /// <summary>
    /// Adds an element to the list.
    /// </summary>
    public void Clear() =>
        _formatElements.Clear();

    /// <summary>
    /// Determines whether the list contains a specific element.
    /// </summary>
    /// <param name="item">The element.</param>
    /// <returns>True, if the element exists; otherwise false.</returns>
    public bool Contains(IExpected item) =>
        _formatElements.Contains(item);

    /// <summary>
    /// Copies the elements of the list to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">An array of format elements.</param>
    /// <param name="arrayIndex">The start index.</param>
    public void CopyTo(IExpected[] array, int arrayIndex) =>
        _formatElements.CopyTo(array, arrayIndex);

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <returns>An enumerator that iterates through the list.</returns>
    public IEnumerator<IExpected> GetEnumerator() =>
        _formatElements.GetEnumerator();

    /// <summary>
    /// Determines the index of a specific format element in the list.
    /// </summary>
    /// <param name="item">The element.</param>
    /// <returns>Th index of a specific format element.</returns>
    public int IndexOf(IExpected item) =>
        _formatElements.IndexOf(item);

    /// <summary>
    /// Inserts an element into the list at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="item">The element.</param>
    public void Insert(int index, IExpected item) =>
        _formatElements.Insert(index, item);

    /// <summary>
    /// Removes the first occurrence of a specific element from the list.
    /// </summary>
    /// <param name="item">The element.</param>
    /// <returns>True, if the format element was found; otherwise false.</returns>
    public bool Remove(IExpected item) =>
        _formatElements.Remove(item);

    /// <summary>
    /// Removes the element at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    public void RemoveAt(int index) =>
        _formatElements.RemoveAt(index);

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <returns>An enumerator that iterates through the list.</returns>
    IEnumerator IEnumerable.GetEnumerator() =>
        _formatElements.GetEnumerator();
}