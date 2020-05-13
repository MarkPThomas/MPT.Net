// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 11-24-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-24-2018
// ***********************************************************************
// <copyright file="UniqueLabelNamePair.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Helpers
{
    /// <summary>
    /// Represents data used for uniquely identifying objects in the model.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Helpers.UniqueLabel" />
    public class UniqueLabelNamePair : UniqueLabel
    {
        /// <summary>
        /// The unique name of the object.
        /// This can be customized by the user in the application.
        /// </summary>
        /// <value>The name of the unique object.</value>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLabelNamePair"/> class.
        /// </summary>
        public UniqueLabelNamePair() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueLabelNamePair"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="label">The label.</param>
        /// <param name="story">The story.</param>
        public UniqueLabelNamePair(string name, string label, string story)
        {
            Name = name;
            Label = label;
            Story = story;
        }

        /// <summary>
        /// Gets the unique label.
        /// </summary>
        /// <returns>UniqueLabel.</returns>
        public UniqueLabel GetUniqueLabel()
        {
            return new UniqueLabel(Label, Story);
        }
    }
}
