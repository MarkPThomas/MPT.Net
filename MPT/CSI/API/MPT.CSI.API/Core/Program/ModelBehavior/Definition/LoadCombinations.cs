// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="LoadCombinations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    /// <summary>
    /// Represents the load combinations in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.ILoadCombinations" />
    public class LoadCombinations : CSiApiBase, ILoadCombinations
    {
#else
    /// <summary>
    /// Represents the load combinations in the application.
    /// </summary>
    public class LoadCombinations : CSiApiBase, IDeletable, IListableNames
    {
#endif
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombinations" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public LoadCombinations(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public

        /// <summary>
        /// Adds a new load combination.
        /// The new load combination must have a different name from all other load combinations and all load cases.
        /// If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="nameLoadCombo">The name of a new load combination.</param>
        /// <param name="comboType">The load combination type to add.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(string nameLoadCombo,
            eLoadComboType comboType)
        {
            // TODO: Handle this: If the name is not unique, an error will be returned.
            _callCode = _sapModel.RespCombo.Add(nameLoadCombo, (int)comboType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Adds new default design load combinations to the model.
        /// </summary>
        /// <param name="designSteel">True: Default steel design combinations are to be added to the model.</param>
        /// <param name="designConcrete">True: Default concrete design combinations are to be added to the model.</param>
        /// <param name="designAluminum">True: Default aluminum design combinations are to be added to the model</param>
        /// <param name="designColdFormed">True: Default cold formed design combinations are to be added to the model.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddDesignDefaultCombos(bool designSteel,
            bool designConcrete,
            bool designAluminum,
            bool designColdFormed)
        {
            _callCode = _sapModel.RespCombo.AddDesignDefaultCombos(designSteel, designConcrete, designAluminum, designColdFormed);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Change name of load combination.
        /// The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
        /// </summary>
        /// <param name="nameLoadCombo">The existing name of a defined load combination.</param>
        /// <param name="newName">The new name for the combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ChangeName(string nameLoadCombo,
            string newName)
        {
            // TODO: Handle this: The new load combination name must be different from all other load combinations and all load cases. If the name is not unique, an error will be returned.
            _callCode = _sapModel.RespCombo.ChangeName(nameLoadCombo, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the total number of load combinations defined in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.RespCombo.Count();
        }

        /// <summary>
        /// Returns the total number of load case and/or combinations included in a specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="loadCaseComboCount">The number of load case and/or combinations included in the specified combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public int CountCase(string nameLoadCombo)
        {
            int loadCaseComboCount = 0;
            _callCode = _sapModel.RespCombo.CountCase(nameLoadCombo, ref loadCaseComboCount);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            return loadCaseComboCount;
        }
#endif

        /// <inheritdoc />
        /// <summary>
        /// Deletes the specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string nameLoadCombo)
        {
            _callCode = _sapModel.RespCombo.Delete(nameLoadCombo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Deletes one load case or load combination from the list of cases included in the specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboName">The name of the load case or load combination to be deleted from the specified combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteCase(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboName)
        {
            _callCode = _sapModel.RespCombo.DeleteCase(nameLoadCombo, 
                            EnumLibrary.Convert<eCaseComboType, CSiProgram.eCNameType>(caseComboType), 
                            caseComboName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        // === Get/Set ===
        /// <inheritdoc />
        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.RespCombo.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        // == Case List ==

        /// <summary>
        /// Returns all load cases and response combinations included in the load combination specified by the <paramref name="nameLoadCombo" /> item.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboTypes">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboNames">This is an array of the names of the load cases or load combinations included in the load combination specified by the <paramref name="nameLoadCombo" /> item.</param>
        /// <param name="scaleFactors">The scale factor multiplying the case or combination indicated by the <paramref name="caseComboNames" /> item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetCaseList(string nameLoadCombo,
            out eCaseComboType[] caseComboTypes,
            out string[] caseComboNames,
            out double[] scaleFactors)
        {
            caseComboNames = new string[0];
            scaleFactors = new double[0];
            CSiProgram.eCNameType[] csiCaseComboType = new CSiProgram.eCNameType[0];
            _callCode = _sapModel.RespCombo.GetCaseList(nameLoadCombo, ref _numberOfItems, ref csiCaseComboType, ref caseComboNames, ref scaleFactors);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            caseComboTypes = new eCaseComboType[csiCaseComboType.Length+1];
            for (int i = 0; i < csiCaseComboType.Length; i++)
            {
                caseComboTypes[i] = EnumLibrary.Convert(csiCaseComboType[i], caseComboTypes[i]);
            }
        }

        /// <summary>
        /// Adds or modifies one load case or response combination in the list of cases included in the load combination specified by the <see paramref="nameLoadCombo" /> item.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="caseComboType">This parameter indicates whether the item is an analysis case (LoadCase) or a load combination (LoadCombo).</param>
        /// <param name="caseComboName">This is the name of the load cases or load combination included in the load combination specified by the <paramref name="nameLoadCombo" /> item.</param>
        /// <param name="scaleFactor">The scale factor multiplying the case or combination indicated by the <paramref name="caseComboName" />  item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetCaseList(string nameLoadCombo,
            eCaseComboType caseComboType,
            string caseComboName,
            double scaleFactor)
        {
            CSiProgram.eCNameType csiCaseComboType = EnumLibrary.Convert<eCaseComboType, CSiProgram.eCNameType>(caseComboType);
            _callCode = _sapModel.RespCombo.SetCaseList(nameLoadCombo, ref csiCaseComboType, caseComboName, scaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // == Note ==

        /// <summary>
        /// Returns the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetNote(string nameLoadCombo)
        {
            string note = string.Empty;
            _callCode = _sapModel.RespCombo.GetNote(nameLoadCombo, ref note);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            return note;
        }

        /// <summary>
        /// Sets the user note for specified response combination. The note may be blank.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="note">The user note, if any, included with the specified combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNote(string nameLoadCombo,
            string note)
        {
            _callCode = _sapModel.RespCombo.SetNote(nameLoadCombo, note);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        // == Type ==

        /// <summary>
        /// This function gets the combination type for specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eLoadComboType GetType(string nameLoadCombo)
        {
            int csiCombo = 0;
            _callCode = _sapModel.RespCombo.GetTypeOAPI(nameLoadCombo, ref csiCombo);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eLoadComboType) csiCombo;
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the combination type for specified load combination.
        /// </summary>
        /// <param name="nameLoadCombo">The name of an existing load combination.</param>
        /// <param name="comboType">The load combination type of the specified load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetType(string nameLoadCombo,
            eLoadComboType comboType)
        {
            _callCode = _sapModel.RespCombo.SetTypeOAPI(nameLoadCombo, (int)comboType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
