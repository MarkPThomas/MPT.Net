// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 11-24-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-24-2018
// ***********************************************************************
// <copyright file="UniqueLabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// Represents the pairing of label name and story that uniquely identifies an object.
    /// </summary>
    public class UniqueLabel
    {
        /// <summary>
        /// The story that the element is located on.
        /// </summary>
        /// <value>The story.</value>
        public string Story { get; set; }

        /// <summary>
        /// The label associated with the element.
        /// This is only unique within a given story.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLabel"/> class.
        /// </summary>
        public UniqueLabel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLabel"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="story">The story.</param>
        public UniqueLabel(string label, string story)
        {
            Label = label;
            Story = story;
        }
    }
}
