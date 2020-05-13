// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="MassSourceHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions.Masses;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class MassSourceHelper.
    /// </summary>
    public class MassSourceHelper 
    {
        #region Fields & Properties
        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The mass source
        /// </summary>
        private MassSource _massSource;
        /// <summary>
        /// This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.
        /// </summary>
        /// <value>The name of the mass source.</value>
        public string MassSourceName => _massSource == null ? string.Empty : _massSource.Name;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MassSourceHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        internal MassSourceHelper(string caseName) 
        {
            CaseName = caseName;
        }

        /// <summary>
        /// Sets the mass source to be used for the analysis case.
        /// </summary>
        /// <param name="massSource">The mass source.</param>
        public void SetMassSource(MassSource massSource)
        {
            _massSource = massSource;
        }

        /// <summary>
        /// Removes the mass source to be used for the analysis case.
        /// </summary>
        public void RemoveMassSource()
        {
            _massSource = null;
        }
    }
}
