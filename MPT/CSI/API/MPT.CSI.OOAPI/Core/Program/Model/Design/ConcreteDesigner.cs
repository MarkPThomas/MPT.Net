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
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
#if !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using ApiDesignConcrete = MPT.CSI.API.Core.Program.ModelBehavior.Design.DesignConcrete;
#endif

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
#if !BUILD_ETABS2016 && !BUILD_ETABS2017
    public sealed class ConcreteDesigner : Designer<ApiDesignConcrete>
    {
    #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDesigner"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        internal ConcreteDesigner(
            ApiCSiApplication app,
            Groups groups,
            LoadCombinations loadCombinations) : base(
            app,
            groups,
            loadCombinations,
            app.Model.Design.DesignConcrete) { }
    #endregion
#else
    /// <summary>
    /// Class ConcreteDesigner. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.Designer" />
    public sealed class ConcreteDesigner : Designer
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDesigner" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal ConcreteDesigner(ApiCSiApplication app) : base(app) { }
        #endregion
#endif
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
            setCode(_designer.DesignConcrete, codeName);
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
            return getDesignSection(_designer.DesignConcrete, frame.Name);
        }

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="frame">Frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        public override void FillDesignSection(Frame frame)
        {
            fillDesignSection(_designer.DesignConcrete, frame);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <param name="section">An existing frame section property to be used as the design section for the specified frame objects.</param>
        public override void AddDesignSection(Frame frame, FrameSection section)
        {
            addDesignSection(_designer.DesignConcrete, frame, section);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        public override void RemoveDesignSection(Frame frame)
        {
            removeDesignSection(_designer.DesignConcrete, frame);
        }
#endregion
    }
}