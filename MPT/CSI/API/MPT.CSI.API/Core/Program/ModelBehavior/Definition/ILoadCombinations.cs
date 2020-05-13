﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ILoadCombinations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Implements the load combinations in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ILoadCombinations : IChangeableName, ICountable, IDeletable, IListableNames
    {
#else
    /// <summary>
    /// Implements the load combinations in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ILoadCombinations: IDeletable, IListableNames
    {
#endif
        #region Methods: Public
        /// <summary>
        /// Adds a new load combination.
        /// The new load combination must have a different name from all other load combinations and all load cases.
        /// If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="nameLoadCombo">The name of a new load combination.</param>
        /// <param name="comboType">The load combination type to add.</param>
        void Add(string nameLoadCombo,
            eLoadComboType comboType);

        /// <summary>
        /// Adds a new default design load combinations to the model.
        /// </summary>
        /// <param name="designSteel">True: Default steel design combinations are to be added to the model.</param>
        /// <param name="designConcrete">True: Default concrete design combinations are to be added to the model.</param>
        /// <param name="designAluminum">True: Default aluminum design combinations are to be added to the model</param>
        /// <param name="designColdFormed">True: Default cold formed design combinations are to be added to the model.</param>
        void AddDesignDefaultCombos(bool designSteel,
            bool designConcrete,
            bool designAluminum,
            bool designColdFormed);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the total number of load case and/or combinations included in a specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        int CountCase(string nameLoadCombo);

        /// <summary>
        /// Returns the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        string GetNote(string nameLoadCombo);

        /// <summary>
        /// Sets the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        void SetNote(string nameLoadCombo,
            string note);

        /// <summary>
        /// Sets the combination type for specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="comboType">The load combination type of the specified load combination.</param>
        void SetType(string nameLoadCombo,
            eLoadComboType comboType);
#endif

        /// <summary>
        /// Deletes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboName">The name of the load case or load combination to be deleted from the specified combination.</param>
        void DeleteCase(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboName);

        /// <summary>
        /// Returns all load cases and response combinations included in the load combination specified by the Name item.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboNames">This is an array of the names of the load cases or load combinations included in the load combination specified by the Name item.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case or combination indicated by the caseComboNames item.</param>
        void GetCaseList(string nameLoadCombo,
            out eCaseComboType[] caseComboType,
            out string[] caseComboNames,
            out double[] scaleFactor);

        /// <summary>
        /// Adds or modifies one load case or response combination in the list of cases included in the load combination specified by the Name item.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboNames">This is an array of the names of the load cases or load combinations included in the load combination specified by the Name item.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case or combination indicated by the caseComboNames item.</param>
        void SetCaseList(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboNames,
            double scaleFactor);

        /// <summary>
        /// This function gets the combination type for specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <returns>eLoadComboType.</returns>
        eLoadComboType GetType(string nameLoadCombo);
        #endregion
    }
}