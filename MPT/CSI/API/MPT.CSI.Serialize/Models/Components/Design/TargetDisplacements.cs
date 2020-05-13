// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-25-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="TargetDisplacements.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    /// <summary>
    /// Class TargetDisplacements. This class cannot be inherited.
    /// </summary>
    public sealed class TargetDisplacements
    {
        #region Fields & Properties
        /// <summary>
        /// All specified displacement targets active
        /// </summary>
        private bool? _allSpecifiedDisplacementTargetsActive;
        /// <summary>
        /// True: All specified lateral displacement targets are active.
        /// False: They are inactive.
        /// </summary>
        /// <value><c>true</c> if [all specified displacement targets active]; otherwise, <c>false</c>.</value>
        public bool AllSpecifiedDisplacementTargetsActive
        {
            get
            {
                if (_allSpecifiedDisplacementTargetsActive == null)
                {

                }

                return _allSpecifiedDisplacementTargetsActive ?? false;
            }
        }

        /// <summary>
        /// The target displacements
        /// </summary>
        private List<TargetDisplacement> _targetDisplacements;
        /// <summary>
        /// Gets or sets the target displacements.
        /// </summary>
        /// <value>The target displacement.</value>
        public ReadOnlyCollection<TargetDisplacement> Displacements
        {
            get
            {
                if (_targetDisplacements == null)
                {
                }

                return new ReadOnlyCollection<TargetDisplacement>(_targetDisplacements);
            }
        }
        #endregion
    }
}
