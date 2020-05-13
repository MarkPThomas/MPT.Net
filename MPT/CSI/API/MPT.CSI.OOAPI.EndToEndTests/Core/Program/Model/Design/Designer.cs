using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiDesigner = MPT.CSI.API.Core.Program.ModelBehavior.Designer;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    public abstract class Designer : IFill
    {
        protected static ApiDesigner _designer => Registry.Designer;


        public DesignResults Results { get; protected set; }

        public string Code { get; protected set; }

        // TODO: Make groups into object rather than names?
        public List<string> Groups { get; protected set; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        public bool AutogenerateLoadCombinations { get; protected set; }
#endif
        // TODO: Make CombinationsStrength into object rather than names?
        public List<string> CombinationsStrength { get; protected set; }

        #region Abstract Methods        
        /// <summary>
        /// Fills the data.
        /// </summary>
        public abstract void FillData();

        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void DeleteResults();

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void ResetOverwrites();

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void StartDesign();

        // === Get/Set ===
        /// <summary>
        /// Gets the code name.
        /// </summary>
        public abstract void FillCode();
        
        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetCode(string codeName);

        // ===

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        public abstract string GetDesignSection(string nameFrame);

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddDesignSection(string nameFrame, string nameSection);

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void RemoveDesignSection(string nameFrame);

        // ===
        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillGroups();

        /// <summary>
        /// Selects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddGroup(string nameGroup);

        /// <summary>
        /// Deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void RemoveGroup(string nameGroup);


        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetAutoSelectNull(string itemName);

        // ===

        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillComboStrength();

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddComboStrength(string nameLoadCombination);

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void RemoveComboStrength(string nameLoadCombination);

        // ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillComboAutoGenerate();

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetComboAutoGenerate(bool autoGenerate);

        // ===
#endif
        #endregion

        #region API Functions
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void deleteResults(IResettable app)
        {
            app.DeleteResults();
            Results.FillResultsAreAvailable();
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void resetOverwrites(IResettable app)
        {
            app.ResetOverwrites();
        }

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void startDesign(IDesignRun app)
        {
            app.StartDesign();
            Results.FillResultsAreAvailable();
        }

        // === Get/Set ===
        /// <summary>
        /// Gets the code name.
        /// </summary>
        protected void getCode(IDesignCode app)
        {
            Code = app.GetCode();
        }
        
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setCode(IDesignCode app)
        {
            app.SetCode(Code);
        }

        // ===

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        protected string getDesignSection(IDesignCode app, string nameFrame)
        {
            return app.GetDesignSection(nameFrame);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="nameFrame">Name of an existing frame object.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <param name="resetToLastAnalysisSection">True: The design section for the specified frame objects is reset to the last analysis section for the frame object.
        /// False: The design section is set to that specified by nameFrame.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setDesignSection(IDesignCode app, 
            string nameFrame,
            string nameSection,
            bool resetToLastAnalysisSection)
        {
            app.SetDesignSection(nameFrame, nameSection, resetToLastAnalysisSection);
        }

        // ===
        /// <summary>
        /// Retrieves the names of all groups selected for design.
        /// These groups are used in the design optimization process, where the optimization is applied at a group level.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getGroup(IAutoSection app)
        {
            Groups = new List<string>(app.GetGroup());
        }

        /// <summary>
        /// Selects or deselects a group for frame design.
        /// </summary>
        /// <param name="nameGroup">Name of an existing group.</param>
        /// <param name="selectForDesign">True: The specified group is selected as a design group for steel design.
        /// False: The group is not selected for steel design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setGroup(IAutoSection app, string nameGroup, bool selectForDesign)
        {
            app.SetGroup(nameGroup, selectForDesign);
        }
        
        /// <summary>
        /// Removes the auto select section assignments from all specified frame objects that have a steel frame design procedure.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setAutoSelectNull(IAutoSection app, string itemName)
        {
            app.SetAutoSelectNull(itemName);
        }
        // ===

        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getComboStrength(IComboStrength app)
        {
            CombinationsStrength = new List<string>(app.GetComboStrength());
        }

        /// <summary>
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for strength design.
        /// False: The combination is not selected for strength design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setComboStrength(IComboStrength app, string nameLoadCombination, bool selectLoadCombination)
        {
            app.SetComboStrength(nameLoadCombination, selectLoadCombination);
            if (selectLoadCombination)
            {
                if (!CombinationsStrength.Contains(nameLoadCombination)) CombinationsStrength.Add(nameLoadCombination);
            }
            else
            {
                CombinationsStrength.Remove(nameLoadCombination);
            }
        }

        // ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getComboAutoGenerate(IComboAuto app)
        {
            AutogenerateLoadCombinations = app.GetComboAutoGenerate();
        }

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setComboAutoGenerate(IComboAuto app, bool autoGenerate)
        {
            app.SetComboAutoGenerate(AutogenerateLoadCombinations);
        }

        // ===
#endif
        #endregion
    }
}
