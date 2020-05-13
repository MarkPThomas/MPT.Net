// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="SectionCuts.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions
{
    /// <summary>
    /// Class SectionCuts.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class SectionCuts : ObjectLists<SectionCut>
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public SectionCutResults Results { get; protected set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCuts"/> class.
        /// </summary>
        public SectionCuts() 
        {
            Results = new SectionCutResults(string.Empty);
        }
        #endregion


        #region Add/Remove   
        /// <summary>
        /// Adds a new section cut defined by a quadrilateral to the model or reinitializes an existing section cut to be defined by a quadrilateral.
        /// </summary>
        /// <param name="uniqueName">Name of the unique section cut.</param>
        /// <param name="group">The group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <param name="coordinate1">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="coordinate2">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="coordinate3">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="coordinate4">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddByQuadrilateral(
            string uniqueName,
            Group group,
            eSectionResultType sectionCutType,
            Coordinate3DCartesian coordinate1,
            Coordinate3DCartesian coordinate2,
            Coordinate3DCartesian coordinate3,
            Coordinate3DCartesian coordinate4)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(SectionCut.AddByQuadrilateral(
                uniqueName, 
                group, 
                sectionCutType, 
                coordinate1, 
                coordinate2, 
                coordinate3, 
                coordinate4));
            return true;
        }

        /// <summary>
        /// Adds a new section cut defined by a group to the model or reinitializes an existing section cut to be defined by a group.
        /// </summary>
        /// <param name="uniqueName">Name of the unique section cut.</param>
        /// <param name="group">The group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddByGroup(
            string uniqueName,
            Group group,
            eSectionResultType sectionCutType)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(SectionCut.AddByGroup(uniqueName, group, sectionCutType)); 
            return true;
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override SectionCut fillNewItem(string uniqueName)
        {
            return SectionCut.Factory(Results, uniqueName);
        }
        #endregion
    }
}
