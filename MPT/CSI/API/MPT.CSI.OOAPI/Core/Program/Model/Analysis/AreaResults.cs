﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="AreaResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class AreaResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class AreaResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }

        /// <summary>
        /// The area force shell
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, ShellForce>> _areaForceShell;
        /// <summary>
        /// Gets or sets the area force shell.
        /// </summary>
        /// <value>The area force shell.</value>
        public List<Tuple<ObjectPointResultsIdentifier, ShellForce>> AreaForceShell
        {
            get
            {
                if (_areaForceShell == null)
                {
                    FillAreaForceShell();
                }

                return _areaForceShell;
            }
        }

        /// <summary>
        /// The area joint force shell
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> _areaJointForceShell;
        /// <summary>
        /// Gets or sets the area joint force shell.
        /// </summary>
        /// <value>The area joint force shell.</value>
        public List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> AreaJointForceShell
        {
            get
            {
                if (_areaJointForceShell == null)
                {
                    FillAreaJointForceShell();
                }

                return _areaJointForceShell;
            }
        }

        /// <summary>
        /// The area stress shell
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, ShellStress>> _areaStressShell;
        /// <summary>
        /// Gets or sets the area stress shell.
        /// </summary>
        /// <value>The area stress shell.</value>
        public List<Tuple<ObjectPointResultsIdentifier, ShellStress>> AreaStressShell
        {
            get
            {
                if (_areaStressShell == null)
                {
                    FillAreaStressShell();
                }

                return _areaStressShell;
            }
        }

        /// <summary>
        /// The area stress shell layered
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>> _areaStressShellLayered;
        /// <summary>
        /// Gets or sets the area stress shell layered.
        /// </summary>
        /// <value>The area stress shell layered.</value>
        public List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>> AreaStressShellLayered
        {
            get
            {
                if (_areaStressShellLayered == null)
                {
                    FillAreaStressShellLayered();
                }

                return _areaStressShellLayered;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaResults" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public AreaResults(ApiCSiApplication app, string name) : base(app)
        {
            ObjectName = name;
        }
        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
            FillAreaForceShell();
            FillAreaJointForceShell();
            FillAreaStressShell();
            FillAreaStressShellLayered();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            AreaForceShell?.Clear();
            AreaJointForceShell?.Clear();
            AreaStressShell?.Clear();
            AreaStressShellLayered?.Clear();
        }

        /// <summary>
        /// Fills the area force shell.
        /// </summary>
        public void FillAreaForceShell()
        {
            _areaForceShell = GetAreaForceShell(_apiAnalysisResults, ObjectName);
        }

        /// <summary>
        /// Fills the area joint force shell.
        /// </summary>
        public void FillAreaJointForceShell()
        {
            _areaJointForceShell = GetAreaJointForceShell(_apiAnalysisResults, ObjectName);
        }

        /// <summary>
        /// Fills the area stress shell.
        /// </summary>
        public void FillAreaStressShell()
        {
            _areaStressShell = GetAreaStressShell(_apiAnalysisResults, ObjectName);
        }

        /// <summary>
        /// Fills the area stress shell layered.
        /// </summary>
        public void FillAreaStressShellLayered()
        {
            _areaStressShellLayered = GetAreaStressShellLayered(_apiAnalysisResults, ObjectName);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the area force shell.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, ShellForce&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, ShellForce>> GetAreaForceShell(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.AreaForceShell(name, itemType,
                out var objectNames,
                out var elementNames,
                out var pointNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var f11,
                out var f22,
                out var f12,
                out var fMax,
                out var fMin,
                out var fAngle,
                out var fvm,
                out var m11,
                out var m22,
                out var m12,
                out var mMax,
                out var mMin,
                out var mAngle,
                out var v13,
                out var v23,
                out var vMax,
                out var vAngle);

            List<Tuple<ObjectPointResultsIdentifier, ShellForce>> resultItems = new List<Tuple<ObjectPointResultsIdentifier, ShellForce>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                ObjectPointResultsIdentifier identifier =
                    new ObjectPointResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i],
                        ObjectName = objectNames[i],
                        ElementName = elementNames[i],
                        PointName = pointNames[i]
                    };

                ShellForce results = new ShellForce
                {
                    F11 = f11[i],
                    F22 = f22[i],
                    F12 = f12[i],
                    FMax = fMax[i],
                    FMin = fMin[i],
                    FAngle = fAngle[i],
                    FVM = fvm[i],
                    M11 = m11[i],
                    M22 = m22[i],
                    M12 = m12[i],
                    MMax = mMax[i],
                    MMin = mMin[i],
                    MAngle = mAngle[i],
                    V13 = v13[i],
                    V23 = v23[i],
                    VMax = vMax[i],
                    VAngle = vAngle[i]
                };

                resultItems.Add(new Tuple<ObjectPointResultsIdentifier, ShellForce>(identifier, results));
            }

            return resultItems;
        }


        /// <summary>
        /// Gets the area joint force shell.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> GetAreaJointForceShell(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.AreaJointForceShell(name, itemType,
                out var objectNames,
                out var elementNames,
                out var pointNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var jointForces);

            List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> resultItems = new List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                ObjectPointResultsIdentifier identifier =
                    new ObjectPointResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i],
                        ObjectName = objectNames[i],
                        ElementName = elementNames[i],
                        PointName = pointNames[i]
                    };

                resultItems.Add(new Tuple<ObjectPointResultsIdentifier, AnalysisLoads>(identifier, jointForces[i]));
            }

            return resultItems;
        }


        /// <summary>
        /// Gets the area stress shell.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, ShellStress&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, ShellStress>> GetAreaStressShell(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.AreaStressShell(name, itemType,
                out var objectNames,
                out var elementNames,
                out var pointNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var stressesTop,
                out var stressesBottom,
                out var s13Avg,
                out var s23Avg,
                out var sMaxAvg,
                out var sAngleAvg);

            List<Tuple<ObjectPointResultsIdentifier, ShellStress>> resultItems = new List<Tuple<ObjectPointResultsIdentifier, ShellStress>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                ObjectPointResultsIdentifier identifier =
                    new ObjectPointResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i],
                        ObjectName = objectNames[i],
                        ElementName = elementNames[i],
                        PointName = pointNames[i]
                    };

                ShellStress results = new ShellStress
                {
                    StressTop = stressesTop[i],
                    StressBottom = stressesBottom[i],
                    S13Avg = s13Avg[i],
                    S23Avg = s23Avg[i],
                    SMaxAvg = sMaxAvg[i],
                    SAngleAvg = sAngleAvg[i]
                };

                resultItems.Add(new Tuple<ObjectPointResultsIdentifier, ShellStress>(identifier, results));
            }

            return resultItems;
        }


        /// <summary>
        /// Gets the area stress shell layered.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectPointResultsIdentifier, LayeredShellStress&gt;&gt;.</returns>
        public static List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>> GetAreaStressShellLayered(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.AreaStressShellLayered(name, itemType,
                out var objectNames,
                out var elementNames,
                out var pointNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var stresses,
                out var s13Avg,
                out var s23Avg,
                out var sMaxAvg,
                out var sAngleAvg,
                out var layers,
                out var integrationPointNumbers,
                out var integrationPointLocations);

            List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>> resultItems = new List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                ObjectPointResultsIdentifier identifier =
                    new ObjectPointResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i],
                        ObjectName = objectNames[i],
                        ElementName = elementNames[i],
                        PointName = pointNames[i]
                    };

                LayeredShellStress results = new LayeredShellStress
                {
                    Stress = stresses[i],
                    S13Avg = s13Avg[i],
                    S23Avg = s23Avg[i],
                    SMaxAvg = sMaxAvg[i],
                    SAngleAvg = sAngleAvg[i],
                    Layer = layers[i],
                    IntegrationPointNumber = integrationPointNumbers[i],
                    IntegrationPointLocation = integrationPointLocations[i]
                };

                resultItems.Add(new Tuple<ObjectPointResultsIdentifier, LayeredShellStress>(identifier, results));
            }

            return resultItems;
        }
        #endregion
    }
}
