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
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class TargetDisplacements. This class cannot be inherited.
    /// </summary>
    public sealed class TargetDisplacements
    {
        #region Fields & Properties

        /// <summary>
        /// The API target displacement
        /// </summary>
        private readonly ITargetDisplacement _apiTargetDisplacement;

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
                    Fill();
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
                    Fill();
                }

                return new ReadOnlyCollection<TargetDisplacement>(_targetDisplacements);
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetDisplacements"/> class.
        /// </summary>
        /// <param name="apiTargetDisplacement">The API target displacement.</param>
        public TargetDisplacements(ITargetDisplacement apiTargetDisplacement)
        {
            _apiTargetDisplacement = apiTargetDisplacement;
        }

        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves lateral displacement targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Fill()
        {
            _apiTargetDisplacement.GetTargetDisplacement(
                out var loadCase,
                out var namePoint,
                out var displacementTargets,
                out var allSpecifiedTargetsActive);

            _allSpecifiedDisplacementTargetsActive = allSpecifiedTargetsActive;

            _targetDisplacements = new List<TargetDisplacement>();
            for (int i = 0; i < loadCase.Length; i++)
            {
                TargetDisplacement targetDisplacement = new TargetDisplacement()
                {
                    PointName = namePoint[i],
                    Value = displacementTargets[i]
                };
                _targetDisplacements.Add(targetDisplacement);
            }
        }

        /// <summary>
        /// Adds the target displacements.
        /// </summary>
        /// <param name="targetDisplacements">The target displacements.</param>
        public void SetTargetDisplacements(List<TargetDisplacement> targetDisplacements)
        {
            set(targetDisplacements, AllSpecifiedDisplacementTargetsActive);
        }

        /// <summary>
        /// Activates the target displacements.
        /// </summary>
        public void ActivateTargetDisplacements()
        {
            set(_targetDisplacements, allSpecifiedTargetsActive: true);
        }

        /// <summary>
        /// Deactivates the target displacement.
        /// </summary>
        public void DeactivateTargetDisplacement()
        {
            set(_targetDisplacements, allSpecifiedTargetsActive: false);
        }

        /// <summary>
        /// Sets the target displacements.
        /// </summary>
        /// <param name="targetDisplacements">The target displacements.</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        private void set(List<TargetDisplacement> targetDisplacements, bool allSpecifiedTargetsActive)
        {
            _apiTargetDisplacement.SetTargetDisplacement(
                targetDisplacements.Select(o => o.LoadCase).ToArray(), 
                targetDisplacements.Select(o => o.PointName).ToArray(), 
                targetDisplacements.Select(o => o.Value).ToArray(), 
                allSpecifiedTargetsActive);

            _allSpecifiedDisplacementTargetsActive = allSpecifiedTargetsActive;
            _targetDisplacements = targetDisplacements;
        }
        #endregion
    }
}
