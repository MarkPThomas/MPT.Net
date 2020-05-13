// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="SteelDesigner.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class SteelDesigner.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.DesignerMetal" />
    public sealed class SteelDesigner : DesignerMetal
    {

        public static SteelDesigner Instance { get; } = new SteelDesigner();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SteelDesigner()
        {
        }

        private SteelDesigner()
        {
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillCode();
            FillGroups();
            FillComboStrength();
            FillComboDeflection();
            FillTargetDisplacements();
            FillTargetPeriods();
        }

        #region Actions

        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteResults()
        {
            deleteResults(_designer.DesignSteel);
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ResetOverwrites()
        {
            resetOverwrites(_designer.DesignSteel);
        }

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void StartDesign()
        {
            startDesign(_designer.DesignSteel);
        }
        #endregion

        #region Get/Set
        /// <summary>
        /// Gets the code name.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillCode()
        {
            getCode(_designer.DesignSteel);
        }

        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetCode(string codeName)
        {
            Code = codeName;
            setCode(_designer.DesignSteel);
        }

        #endregion

        #region Sections
        // TODO: GetDesignSection return object?
        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string GetDesignSection(string nameFrame)
        {
            return getDesignSection(_designer.DesignSteel, nameFrame);
        }

        // TODO: AddDesignSection - pass in frame object and modify it - similar to material overwrite?
        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddDesignSection(string nameFrame, string nameSection)
        {
            setDesignSection(_designer.DesignSteel, nameFrame, nameSection, resetToLastAnalysisSection: false);
        }

        // TODO: RemoveDesignSection - pass in frame object and modify it - similar to material overwrite?
        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveDesignSection(string nameFrame)
        {
            setDesignSection(_designer.DesignSteel, nameFrame, string.Empty, resetToLastAnalysisSection: true);
        }

        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillGroups()
        {
            getGroup(_designer.DesignSteel);
        }

        /// <summary>
        /// Selects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddGroup(string nameGroup)
        {
            setGroup(_designer.DesignSteel, nameGroup, selectForDesign: true);
        }

        /// <summary>
        /// Deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveGroup(string nameGroup)
        {
            setGroup(_designer.DesignSteel, nameGroup, selectForDesign: false);
        }

        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetAutoSelectNull(string itemName)
        {
            setAutoSelectNull(_designer.DesignSteel, itemName);
        }
        #endregion

        #region Load Combinations
        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillComboStrength()
        {
            getComboStrength(_designer.DesignSteel);
        }

        /// <summary>
        /// Selects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddComboStrength(string nameLoadCombination)
        {
            setComboStrength(_designer.DesignSteel, nameLoadCombination, selectLoadCombination: true);
        }

        /// <summary>
        /// Deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveComboStrength(string nameLoadCombination)
        {
            setComboStrength(_designer.DesignSteel, nameLoadCombination, selectLoadCombination: false);
        }

        /// <summary>
        /// Gets the names of all load combinations used for deflection design.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillComboDeflection()
        {
            getComboDeflection(_designer.DesignSteel);
        }

        /// <summary>
        /// Selects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddComboDeflection(string nameLoadCombination)
        {
            setComboDeflection(_designer.DesignSteel, nameLoadCombination, selectLoadCombination: true);
        }

        /// <summary>
        /// Deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveComboDeflection(string nameLoadCombination)
        {
            setComboDeflection(_designer.DesignSteel, nameLoadCombination, selectLoadCombination: false);
        }
        #endregion

        #region Targets
        /// <summary>
        /// Retrieves lateral displacement targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTargetDisplacements()
        {
            getTargetDisplacements(_designer.DesignSteel);
        }

        /// <summary>
        /// Sets the target displacements.
        /// </summary>
        /// <param name="allSpecifiedTargetsActive">if set to <c>true</c> [all specified targets active].</param>
        public override void SetTargetDisplacements(bool allSpecifiedTargetsActive)
        {
            setTargetDisplacements(_designer.DesignSteel, allSpecifiedTargetsActive);
        }
        
        /// <summary>
        /// Retrieves time period targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTargetPeriods()
        {
            getTargetPeriods(_designer.DesignSteel);
        }

        /// <summary>
        /// Sets time period targets for steel design.
        /// </summary>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetTargetPeriods(bool allSpecifiedTargetsActive)
        {
            setTargetPeriods(_designer.DesignSteel, allSpecifiedTargetsActive);
        }
        #endregion
        
    }
}
