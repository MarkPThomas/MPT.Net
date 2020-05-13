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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class MassSourceHelper.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class MassSourceHelper : CSiOoApiBaseBase
    {
        #region Fields & Properties

        /// <summary>
        /// The API object.
        /// </summary>
        protected IMassSource _apiMassSource;

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
        /// <param name="app">The application.</param>
        /// <param name="apiMass">The API mass.</param>
        /// <param name="caseName">Name of the case.</param>
        internal MassSourceHelper(ApiCSiApplication app, IMassSource apiMass, string caseName) : base(app)
        {
            _apiMassSource = apiMass;
            CaseName = caseName;
        }


        /// <summary>
        /// Retrieves the mass source to be used for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillMassSource()
        {
            _massSource = new MassSource(_apiApp, _apiMassSource?.GetMassSource(CaseName));
        }

        /// <summary>
        /// Sets the mass source to be used for the analysis case.
        /// </summary>
        /// <param name="massSource">The mass source.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMassSource(MassSource massSource)
        {
            _massSource = massSource;
            setMassSource();
        }

        /// <summary>
        /// Removes the mass source to be used for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveMassSource()
        {
            _massSource = null;
            setMassSource();
        }

        /// <summary>
        /// Sets the mass source.
        /// </summary>
        protected void setMassSource()
        {
            _apiMassSource?.SetMassSource(CaseName, MassSourceName);
        }
    }
}
