// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ConcreteDesigner.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class ConcreteDesigner.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.Designer" />
    public sealed class ConcreteDesigner : Designer
    {

        public static ConcreteDesigner Instance { get; } = new ConcreteDesigner();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ConcreteDesigner() { }

        private ConcreteDesigner() { }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillCode();
            FillGroups();

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillComboStrength();
#endif
        }

#region Actions
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteResults()
        {
            deleteResults(_designer.DesignConcrete);
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ResetOverwrites()
        {
            resetOverwrites(_designer.DesignConcrete);
        }
#else
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void DeleteResults()
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ResetOverwrites()
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }
#endif
        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void StartDesign()
        {
            startDesign(_designer.DesignConcrete);
        }
#endregion

#region Get/Set
        /// <summary>
        /// Gets the code name.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillCode()
        {
            getCode(_designer.DesignConcrete);
        }

        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetCode(string codeName)
        {
            Code = codeName;
            setCode(_designer.DesignConcrete);
        }
        #endregion

        #region Sections
        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string GetDesignSection(string nameFrame)
        {
            return getDesignSection(_designer.DesignConcrete, nameFrame);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddDesignSection(string nameFrame, string nameSection)
        {
            setDesignSection(_designer.DesignConcrete, nameFrame, nameSection, resetToLastAnalysisSection: false);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveDesignSection(string nameFrame)
        {
            setDesignSection(_designer.DesignConcrete, nameFrame, string.Empty, resetToLastAnalysisSection: true);
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillGroups()
        {
            getGroup(_designer.DesignConcrete);
        }

        /// <summary>
        /// Selects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddGroup(string nameGroup)
        {
            setGroup(_designer.DesignConcrete, nameGroup, selectForDesign: true);
        }

        /// <summary>
        /// Deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveGroup(string nameGroup)
        {
            setGroup(_designer.DesignConcrete, nameGroup, selectForDesign: false);
        }

        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void SetAutoSelectNull(string itemName)
        {
            setAutoSelectNull(_designer.DesignConcrete, itemName);
        }
#else
        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void FillGroups()
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void AddGroup(string nameGroup)
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void RemoveGroup(string nameGroup)
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void SetAutoSelectNull(string itemName)
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }
#endif
#endregion

#region Load Combinations
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void FillComboStrength()
        {
            getComboStrength(_designer.DesignConcrete);
        }

        /// <summary>
        /// Selects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void AddComboStrength(string nameLoadCombination)
        {
            setComboStrength(_designer.DesignConcrete, nameLoadCombination, selectLoadCombination: true);
        }

        /// <summary>
        /// Deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void RemoveComboStrength(string nameLoadCombination)
        {
            setComboStrength(_designer.DesignConcrete, nameLoadCombination, selectLoadCombination: false);
        }
#else
        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void FillComboStrength()
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void AddComboStrength(string nameLoadCombination)
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void RemoveComboStrength(string nameLoadCombination)
        { // ETABS Does not have this.
            throw new NotImplementedException();
        }
#endif
#endregion
    }
}
