// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="JointResponseSpectrumIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;

namespace MPT.CSI.OOAPI.Core.Helpers.Results
{
    /// <summary>
    /// Class JointResponseSpectrumIdentifier.
    /// </summary>
    /// <seealso cref="ResultsIdentifier" />
    public class JointResponseSpectrumIdentifier : ResultsIdentifier
    {

        /// <summary>
        /// The model object associated with the result.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; set; }


        /// <summary>
        /// The element associated with the result.
        /// </summary>
        /// <value>The name of the element.</value>
        public string ElementName { get; set; }


        /// <summary>
        /// The coordinate systems in which the results are reported.
        /// </summary>
        /// <value>The coordinate system.</value>
        public string CoordinateSystem { get; set; }


        /// <summary>
        /// The directions for which the results are reported.
        /// </summary>
        /// <value>The direction.</value>
        public eDirection Direction { get; set; }


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
