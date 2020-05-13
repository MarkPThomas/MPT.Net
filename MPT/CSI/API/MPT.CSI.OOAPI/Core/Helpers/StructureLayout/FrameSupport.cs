// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameSupport.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2016 || BUILD_ETABS2017
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;

namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Class FrameSupport.
    /// </summary>
    public class FrameSupport : ApiProperty
    {
        /// <summary>
        /// The name of the column frame object, beam frame object or wall area object which supports the beam at the node.
        /// </summary>
        /// <value>The name of the support.</value>
        public string SupportName { get; set; }

        /// <summary>
        /// The type of support at the node.
        /// </summary>
        /// <value>The support type i.</value>
        public eSupportType SupportType { get; set; }


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
#endif