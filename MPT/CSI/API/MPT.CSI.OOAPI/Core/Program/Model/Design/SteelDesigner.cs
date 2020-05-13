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
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiDesignSteel = MPT.CSI.API.Core.Program.ModelBehavior.Design.DesignSteel;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{

    /// <summary>
    /// Class SteelDesigner. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.DesignerMetal{DesignSteel}" />
    public sealed class SteelDesigner : DesignerMetal<ApiDesignSteel>
    {

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SteelDesigner"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="loadCases">The load cases.</param>
        internal SteelDesigner(
            ApiCSiApplication app,
            Groups groups,
            LoadCombinations loadCombinations,
            LoadCases loadCases) 
            : base(
                app, 
                groups,
                loadCombinations,
                loadCases,
                app.Model.Design.DesignSteel)
        {

        }
        #endregion

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
            setCode(_designer.DesignSteel, codeName);
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
            return getDesignSection(_designer.DesignSteel, frame.Name);
        }

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="frame">Frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        public override void FillDesignSection(Frame frame)
        {
            fillDesignSection(_designer.DesignSteel, frame);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <param name="section">An existing frame section property to be used as the design section for the specified frame objects.</param>
        public override void AddDesignSection(Frame frame, FrameSection section)
        {
            addDesignSection(_designer.DesignSteel, frame, section);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        public override void RemoveDesignSection(Frame frame)
        {
            removeDesignSection(_designer.DesignSteel, frame);
        }
        #endregion
    }
}
