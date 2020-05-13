// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="Designer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.Design
{

    /// <summary>
    /// Class Designer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Designer" />
    public abstract class Designer<T> : Designer
        //where T : IAutoSection, IComboStrength
    {
        #region Fields & Properties  
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Designer{T}" /> class.
        /// </summary>
        /// <param name="groups">The groups.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        protected Designer(
            Groups groups,
            LoadCombinations loadCombinations) : base(groups, loadCombinations)
        { }
        #endregion

        #region Abstract Methods        
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        public abstract void DeleteResults();

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        public abstract void ResetOverwrites();

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        public abstract void SetComboAutoGenerate(bool autoGenerate);
        #endregion
    }

    /// <summary>
    /// Class Designer.
    /// </summary>
    /// <seealso cref="Designer" />
    public abstract class Designer 
    {
        #region Fields & Properties   
        /// <summary>
        /// The code
        /// </summary>
        protected string _code;
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; internal set; }


        /// <summary>
        /// The load combinations
        /// </summary>
        protected readonly LoadCombinations _loadCombinations;

        /// <summary>
        /// The groups
        /// </summary>
        protected readonly Groups _groups;

        /// <summary>
        /// The design groups
        /// </summary>
        private GroupsDesign _designGroups;
        /// <summary>
        /// Gets the design groups.
        /// </summary>
        /// <value>The design groups.</value>
        public GroupsDesign DesignGroups =>
            _designGroups ??
            (_designGroups = new GroupsDesign(_groups));


        public virtual bool AutogenerateLoadCombinations { get; internal set; }

        /// <summary>
        /// The load combinations strength
        /// </summary>
        private LoadCombinationsStrength _loadCombinationsStrength;
        /// <summary>
        /// Gets the load combinations strength.
        /// </summary>
        /// <value>The load combinations strength.</value>
        public LoadCombinationsStrength LoadCombinationsStrength =>
            _loadCombinationsStrength ??
            (_loadCombinationsStrength = new LoadCombinationsStrength(_loadCombinations));
        #endregion

        #region Initialization


        /// <summary>
        /// Initializes a new instance of the <see cref="Designer{T}" /> class.
        /// </summary>
        protected Designer(
            Groups groups,
            LoadCombinations loadCombinations)
        {
            _groups = groups;
            _loadCombinations = loadCombinations;
        }
        #endregion

        #region Abstract Methods        
        /// <summary>
        /// Starts the frame design.
        /// </summary>
        public abstract void StartDesign();

        // === Get/Set ===
        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        public abstract void SetCode(string codeName);

        // === Design Section
        // TODO: Consider injecting designer into frame and calling this internally based on frame assignments
        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="frame">Frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        public abstract string GetDesignSection(Frame frame);


        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <param name="section">An existing frame section property to be used as the design section for the specified frame objects.</param>
        public abstract void AddDesignSection(Frame frame, FrameSection section);

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        public abstract void RemoveDesignSection(Frame frame);
        #endregion
    }
}
