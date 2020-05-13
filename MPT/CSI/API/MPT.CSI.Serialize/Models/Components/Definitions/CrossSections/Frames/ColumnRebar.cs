// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="ColumnRebar.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class ColumnRebar.
    /// </summary>
    public class ColumnRebar
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the cross section.
        /// </summary>
        /// <value>The name of the cross section.</value>
        internal string CrossSectionName { get; set; }

        /// <summary>
        /// Gets or sets the detailing.
        /// </summary>
        /// <value>The detailing.</value>
        public ColumnRebarDetailing Detailing { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnRebar"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public ColumnRebar(string name) 
        {
            CrossSectionName = name;
        }
        #endregion
    }
}
