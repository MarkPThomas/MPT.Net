﻿using System;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class AluminumDesigner : DesignerMetal<AluminumDesignResults>
    {
        #region Fields & Properties
        public AluminumDesignPreferences AluminumDesignPreferences { get; set; }
        #endregion

        #region Initialization


        internal AluminumDesigner(
            Groups groups,
            LoadCombinations loadCombinations,
            LoadCases loadCases)
            : base(
                groups,
                loadCombinations,
                loadCases)
        {

        }
        #endregion

        #region Actions
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        public override void DeleteResults()
        {

        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        public override void ResetOverwrites()
        {

        }

        /// <summary>
        /// Starts the frame design.
        /// </summary>
        public override void StartDesign()
        {

        }
        #endregion

        #region Get/Set
        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        public override void SetCode(string codeName)
        {

        }

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        public override void SetComboAutoGenerate(bool autoGenerate)
        {

        }
        #endregion

        #region Sections        
        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="frame">Frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        public override string GetDesignSection(Frame frame)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <param name="section">An existing frame section property to be used as the design section for the specified frame objects.</param>
        public override void AddDesignSection(Frame frame, FrameSection section)
        {

        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        public override void RemoveDesignSection(Frame frame)
        {

        }
        #endregion
    }
}
