// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="ConcreteDesignResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;

using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class ConcreteDesignResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.DesignResults" />
    public sealed class ConcreteDesignResults : DesignResults
    {
        #region Static
        /// <summary>
        /// Retrieves summary results for concrete design of beams.
        /// Torsion results are not included for all codes.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;ConcreteResultsSummaryBeam&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<ConcreteResultsSummaryBeam> GetSummaryResultsBeam(
            IDesigner app,
            string name,
            eItemType itemType = eItemType.Object)
        {
            app.DesignConcrete.GetSummaryResultsBeam(name,
                out var frameNames,
                out var location,
                out var topCombo,
                out var topArea,
                out var botCombo,
                out var botArea,
                out var VMajorCombo,
                out var VMajorArea,
                out var TLCombo,
                out var TLArea,
                out var TTCombo,
                out var TTArea,
                out var errorSummaries,
                out var warningSummaries);

            List<ConcreteResultsSummaryBeam> results = new List<ConcreteResultsSummaryBeam>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                ConcreteResultsSummaryBeam result = new ConcreteResultsSummaryBeam()
                {
                    FrameName = frameNames[i],
                    Location = location[i],
                    TopCombo = topCombo[i],
                    TopArea = topArea[i],
                    BottomCombo = botCombo[i],
                    BottomArea = botArea[i],
                    VMajorCombo = VMajorCombo[i],
                    VMajorArea = VMajorArea[i],
                    TLCombo = TLCombo[i],
                    TLArea = TLArea[i],
                    TTCombo = TTCombo[i],
                    TTArea = TTArea[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Retrieves summary results for concrete design of columns.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;ConcreteResultsSummaryColumn&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<ConcreteResultsSummaryColumn> GetSummaryResultsColumn(
            IDesigner app,
            string name,
            eItemType itemType = eItemType.Object)
        {
            app.DesignConcrete.GetSummaryResultsColumn(name,
                out var frameNames,
                out var designOption,
                out var locations,
                out var PMMCombo,
                out var PMMArea,
                out var PMMRatio,
                out var VMajorCombo,
                out var AVMajor,
                out var VMinorCombo,
                out var AVMinor,
                out var errorSummaries,
                out var warningSummaries);

            List<ConcreteResultsSummaryColumn> results = new List<ConcreteResultsSummaryColumn>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                ConcreteResultsSummaryColumn result = new ConcreteResultsSummaryColumn()
                {
                    FrameName = frameNames[i],
                    Location = locations[i],
                    DesignOption = designOption[i],
                    PMMCombo = PMMCombo[i],
                    PMMArea = PMMArea[i],
                    PMMRatio = PMMRatio[i],
                    VMajorCombo = VMajorCombo[i],
                    AVMajor = AVMajor[i],
                    VMinorCombo = VMinorCombo[i],
                    AVMinor = AVMinor[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }

        /// <summary>
        /// Retrieves summary results for concrete design of joints.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;ConcreteResultsSummaryJoint&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<ConcreteResultsSummaryJoint> GetSummaryResultsJoint(
            IDesigner app,
            string name,
            eItemType itemType = eItemType.Object)
        {
            app.DesignConcrete.GetSummaryResultsJoint(name,
                out var frameNames,
                out var LCJSRatioMajor,
                out var JSRatioMajor,
                out var LCJSRatioMinor,
                out var JSRatioMinor,
                out var LCBCCRatioMajor,
                out var BCCRatioMajor,
                out var LCBCCRatioMinor,
                out var BCCRatioMinor,
                out var errorSummaries,
                out var warningSummaries);

            List<ConcreteResultsSummaryJoint> results = new List<ConcreteResultsSummaryJoint>();
            for (int i = 0; i < frameNames.Length; i++)
            {
                ConcreteResultsSummaryJoint result = new ConcreteResultsSummaryJoint()
                {
                    FrameName = frameNames[i],
                    JointShearRatioMajorCombo = LCJSRatioMajor[i],
                    JointShearRatioMajor = JSRatioMajor[i],
                    JointShearRatioMinorCombo = LCJSRatioMinor[i],
                    JointShearRatioMinor = JSRatioMinor[i],
                    BCCRatioMajorCombo = LCBCCRatioMajor[i],
                    BCCRatioMajor = BCCRatioMajor[i],
                    BCCRatioMinorCombo = LCBCCRatioMinor[i],
                    BCCRatioMinor = BCCRatioMinor[i],
                    ErrorSummary = errorSummaries[i],
                    WarningSummary = warningSummaries[i]
                };
                results.Add(result);
            }

            return results;
        }
        #endregion

        #region Fields & Properties

        /// <summary>
        /// The concrete results summary beam
        /// </summary>
        private List<ConcreteResultsSummaryBeam> _concreteResultsSummaryBeam;

        /// <summary>
        /// Gets the concrete results summary beam.
        /// </summary>
        /// <value>The concrete results summary beam.</value>
        public List<ConcreteResultsSummaryBeam> ConcreteResultsSummaryBeam =>
            _concreteResultsSummaryBeam ?? (_concreteResultsSummaryBeam = GetSummaryResultsBeam(_designer, Name));


        /// <summary>
        /// The concrete results summary column
        /// </summary>
        private List<ConcreteResultsSummaryColumn> _concreteResultsSummaryColumn;
        /// <summary>
        /// Gets the concrete results summary column.
        /// </summary>
        /// <value>The concrete results summary column.</value>
        public List<ConcreteResultsSummaryColumn> ConcreteResultsSummaryColumn =>
            _concreteResultsSummaryColumn ?? (_concreteResultsSummaryColumn = GetSummaryResultsColumn(_designer, Name));

        /// <summary>
        /// The concrete results summary joint
        /// </summary>
        private List<ConcreteResultsSummaryJoint> _concreteResultsSummaryJoint;
        /// <summary>
        /// Gets the concrete results summary joint.
        /// </summary>
        /// <value>The concrete results summary joint.</value>
        public List<ConcreteResultsSummaryJoint> ConcreteResultsSummaryJoint =>
            _concreteResultsSummaryJoint ?? (_concreteResultsSummaryJoint = GetSummaryResultsJoint(_designer, Name));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDesignResults"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public ConcreteDesignResults(ApiCSiApplication app, string name) : base(app)
        {
            Name = name;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillResultsAreAvailable();
        }
        #endregion

        #region Fill
        /// <summary>
        /// Fills the design results.
        /// </summary>
        public void FillDesignResults()
        {
            _concreteResultsSummaryBeam = GetSummaryResultsBeam(_designer, Name);
            _concreteResultsSummaryColumn = GetSummaryResultsColumn(_designer, Name);
            _concreteResultsSummaryJoint = GetSummaryResultsJoint(_designer, Name);
        }

        /// <summary>
        /// True: Design results are available.
        /// </summary>
        /// <returns><c>true</c> if design results are available, <c>false</c> otherwise.</returns>
        public override void FillResultsAreAvailable()
        {
            resultsAreAvailable(_designer.DesignConcrete);
            if (ResultsAreAvailable) return;

            _concreteResultsSummaryBeam?.Clear();
            _concreteResultsSummaryColumn?.Clear();
            _concreteResultsSummaryJoint?.Clear();
        }
        #endregion
    }
}
