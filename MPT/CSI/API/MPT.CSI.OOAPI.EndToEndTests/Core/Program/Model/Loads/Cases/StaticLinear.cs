// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="StaticLinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiStaticLinear = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticLinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Static linear load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    /// <seealso cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class StaticLinear : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The static linear API object.
        /// </summary>
        protected static ApiStaticLinear _staticLinear = _loadCases?.StaticLinear;

        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        /// <value>The loads.</value>
        public LoadsAppliedHelper Loads { get; protected set; }

        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static StaticLinear Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (StaticLinear)Registry.LoadCases[uniqueName];

            StaticLinear loadCase = new StaticLinear(uniqueName);
            if (_loadCases != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticLinear" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StaticLinear(string name) : base(name)
        {
            Loads = new LoadsAppliedHelper(name, _staticLinear);
            InitialCase = new InitialCaseHelper(name, _staticLinear);
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
            InitialCase.FillInitialCase();
            Loads.FillLoads();
        }


        // TODO: Work into factory
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name)
        {
            _staticLinear?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _staticLinear?.SetCase(Name);
            FillData();
        }
        #endregion
    }
}
