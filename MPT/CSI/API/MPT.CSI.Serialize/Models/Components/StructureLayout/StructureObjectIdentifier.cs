// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="StructureObjectIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Struct ObjectIdentifier
    /// </summary>
    public class StructureObjectIdentifier
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
        /// The unique name of the element.
        /// This can be customized by the user in the application.
        /// </summary>
        /// <value>The name of the unique.</value>
        public string Name { get; set; }
    }
}
