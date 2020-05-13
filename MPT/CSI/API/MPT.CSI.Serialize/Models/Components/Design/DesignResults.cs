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


namespace MPT.CSI.Serialize.Models.Components.Design
{
    /// <summary>
    /// Class DesignResults.
    /// </summary>
    public abstract class DesignResults 
    {
        #region Fields & Properties
        /// <summary>
        /// The name of the object associated with the results.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        private bool? _resultsAreAvailable;

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <value><c>true</c> if [results are available]; otherwise, <c>false</c>.</value>
        public bool ResultsAreAvailable
        {
            get
            {
                if (_resultsAreAvailable == null)
                {
                    FillResultsAreAvailable();
                }

                return _resultsAreAvailable ?? false;
            }
        }
        #endregion

        #region Abstract
        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public abstract void FillResultsAreAvailable();

        /// <summary>
        /// Returns the names of the frame objects that did not pass the design check or have not yet been checked, if any.
        /// </summary>
        /// <param name="numberNotPassedOrChecked">The number of concrete frame objects that did not pass the design check or have not yet been checked.</param>
        /// <param name="numberDidNotPass">The number of concrete frame objects that did not pass the design check.</param>
        /// <param name="numberNotChecked">The number of concrete frame objects that have not yet been checked.</param>
        /// <param name="namesNotPassedOrChecked">This is an array that includes the name of each frame object that did not pass the design check or has not yet been checked.</param>
        public abstract void VerifyPassed(out int numberNotPassedOrChecked,
            out int numberDidNotPass,
            out int numberNotChecked,
            out string[] namesNotPassedOrChecked);

        /// <summary>
        /// Returns the names of the frame objects that have different analysis and design sections, if any.
        /// </summary>
        public abstract string[] VerifySections();
        #endregion
    }
}
