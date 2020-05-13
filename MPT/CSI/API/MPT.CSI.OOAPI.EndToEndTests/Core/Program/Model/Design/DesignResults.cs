// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-03-2018
// ***********************************************************************
// <copyright file="DesignResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiDesigner = MPT.CSI.API.Core.Program.ModelBehavior.Designer;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class DesignResults.
    /// </summary>
    public abstract class DesignResults : IFill
    {
        /// <summary>
        /// Gets the designer.
        /// </summary>
        /// <value>The designer.</value>
        protected static ApiDesigner _designer => Registry.Designer;

        /// <summary>
        /// The name of the object associated with the results.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <value><c>true</c> if [results are available]; otherwise, <c>false</c>.</value>
        public bool ResultsAreAvailable { get; protected set; }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public abstract void FillData();

        #region Region

#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
            /// <summary>
            /// True: Design results are available.
            /// </summary>
            /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public abstract void FillResultsAreAvailable();
#endif

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void VerifyPassed(out int numberNotPassedOrChecked,
            out int numberDidNotPass,
            out int numberNotChecked,
            out string[] namesNotPassedOrChecked);

        /// <summary>
        /// Returns the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        public abstract string[] VerifySections();
#endif
        #endregion

        #region API Functions
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        /// <summary>
        /// Returns the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void verifyPassed(IDesignCode app,
            out int numberNotPassedOrChecked,
            out int numberDidNotPass,
            out int numberNotChecked,
            out string[] namesNotPassedOrChecked)
        {

        }

        /// <summary>
        /// Returns the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected string[] verifySections(IDesignCode app)
        {

        }
#endif
#if !BUILD_ETABS2015 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        protected bool resultsAreAvailable(IDesignRun app)
        {
            ResultsAreAvailable = app.ResultsAreAvailable();
            return ResultsAreAvailable;
        }
#endif
#endregion
    }
}
