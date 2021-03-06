﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="NoDesignProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class NoDesignProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials.MaterialTemplate" />
    public class NoDesignTemplate : MaterialTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoDesignTemplate" /> class.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="standard">The standard.</param>
        /// <param name="grade">The grade.</param>
        public NoDesignTemplate(
            eMaterialRegion region,
            string standard,
            string grade) : base(eMaterialPropertyType.NoDesign, region, standard, grade)
        {
        }

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
