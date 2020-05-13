// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="GeneralData.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers
{
    /// <summary>
    /// Class GeneralData.
    /// </summary>
    public class GeneralData : ModelProperty
    {
        // TODO: Reconcile these Color/Notes/GUID properties with ones directly placed in other objects.

        /// <summary>
        /// The display color assigned to the item.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; set; }

        /// <summary>
        /// The notes, if any, assigned to the item.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// The GUID (global unique identifier), if any, assigned to the item.
        /// </summary>
        /// <value>The unique identifier.</value>
        public string GUID { get; set; }




        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
