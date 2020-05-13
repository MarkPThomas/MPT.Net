// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="DampingHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class DampingHelper.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IFill" />
    public class DampingHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The API object.
        /// </summary>
        protected static IDampingModal _app;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The damping type
        /// </summary>
        private eDampingType _dampingType;
        /// <summary>
        /// Hysteretic damping type for the load case.
        /// </summary>
        /// <value>The type of the damping.</value>
        public eDampingType DampingType
        {
            get
            {
                if (_dampingType == 0)
                {
                    FillDampingType();
                }

                return _dampingType;
            }
        }

        /// <summary>
        /// The damping constant
        /// </summary>
        private double? _dampingConstant;
        /// <summary>
        /// Constant modal damping for all modes (0 &lt;= damping &lt; 1) assigned to the load case.
        /// </summary>
        /// <value>The damping constant.</value>
        public double DampingConstant
        {
            get
            {
                if (_dampingConstant == null)
                {
                    FillDampingConstant();
                }

                return _dampingConstant ?? 0;
            }
        }

        /// <summary>
        /// The damping interpolated
        /// </summary>
        private List<DampingInterpolated> _dampingInterpolated;
        /// <summary>
        /// Gets or sets the interpolated damping.
        /// </summary>
        /// <value>The damping interpolated.</value>
        public List<DampingInterpolated> DampingInterpolated
        {
            get
            {
                if (_dampingConstant == null)
                {
                    FillDampingInterpolated();
                }

                return _dampingInterpolated;
            }
        }

        /// <summary>
        /// The damping proportional
        /// </summary>
        private DampingProportional _dampingProportional;
        /// <summary>
        /// Gets or sets the proportional damping.
        /// </summary>
        /// <value>The damping proportional.</value>
        public DampingProportional DampingProportional
        {
            get
            {
                if (_dampingProportional == null)
                {
                    FillDampingProportional();
                }

                return _dampingProportional;
            }
        }

        /// <summary>
        /// The damping overrides
        /// </summary>
        private List<DampingOverride> _dampingOverrides;
        /// <summary>
        /// The damping overwrites of mode #, overwrite.
        /// </summary>
        /// <value>The damping overwrites.</value>
        public List<DampingOverride> DampingOverrides
        {
            get
            {
                if (_dampingOverrides == null)
                {
                    FillDampingOverrides();
                }

                return _dampingOverrides;
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="DampingHelper"/> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="app">The application.</param>
        internal DampingHelper(IDampingModal app, string caseName)
        {
            _app = app;
            CaseName = caseName;
        }
        #endregion

        #region Damping
        /// <summary>
        /// Returns the hysteretic damping type for the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDampingType()
        {
            if (_app == null) return;
            _dampingType = _app.GetDampingType(CaseName);
        }


        /// <summary>
        /// Returns the constant modal damping for all modes (0 &lt;= damping &lt; 1) assigned to the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDampingConstant()
        {
            if (_app == null) return;
            _dampingConstant = _app.GetDampingConstant(CaseName);
        }


        /// <summary>
        /// Returns the interpolated modal damping data assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDampingInterpolated()
        {
            if (_app == null) return;
            _app.GetDampingInterpolated(CaseName,
                out var dampingType,
                out var periodsOrFrequencies,
                out var damping);

            _dampingInterpolated = new List<DampingInterpolated>();
            for (int i = 0; i < periodsOrFrequencies.Length; i++)
            {
                DampingInterpolated dampingInterpolated = new DampingInterpolated()
                {
                    DampingType = dampingType,
                    PeriodOrFrequency = periodsOrFrequencies[i],
                    Damping = damping[i]
                };
                _dampingInterpolated.Add(dampingInterpolated);
            }
        }


        /// <summary>
        /// Returns the proportional modal damping data assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDampingProportional()
        {
            if (_app == null) return;
            _app.GetDampingProportional(CaseName,
                out var dampingType,
                out var massProportionalDampingCoefficient,
                out var stiffnessProportionalDampingCoefficient,
                out var periodOrFrequencyPt1,
                out var periodOrFrequencyPt2,
                out var dampingPt1,
                out var dampingPt2);

            _dampingProportional = new DampingProportional()
            {
                DampingType = dampingType,
                MassProportionalDampingCoefficient = massProportionalDampingCoefficient,
                StiffnessProportionalDampingCoefficient = stiffnessProportionalDampingCoefficient,
                PeriodOrFrequencyPt1 = periodOrFrequencyPt1,
                PeriodOrFrequencyPt2 = periodOrFrequencyPt2,
                DampingPt1 = dampingPt1,
                DampingPt2 = dampingPt2
            };
        }


        /// <summary>
        /// Returns the modal damping overrides assigned to the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDampingOverrides()
        {
            if (_app == null) return;
            _app.GetDampingOverrides(CaseName,
                out var modes,
                out var damping);

            _dampingOverrides = new List<DampingOverride>();
            for (int i = 0; i < modes.Length; i++)
            {
                DampingOverride dampingOverride = new DampingOverride()
                {
                    ModeNumber = modes[i],
                    Damping = damping[i]
                };
                _dampingOverrides.Add(dampingOverride);
            }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets constant modal damping for the specified load case.
        /// </summary>
        /// <param name="damping">The constant damping for all modes (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDampingConstant(double damping)
        {
            // TODO: SAP2000 - SetDampingConstant
        }

        /// <summary>
        /// Returns the interpolated modal damping data assigned to the specified load case.
        /// </summary>
        /// <param name="dampingType">The interpolated modal damping type.</param>
        /// <param name="periodsOrFrequencies">The periods or frequencies, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeInterpolated.InterpolatedByFrequency" />.</param>
        /// <param name="damping">The damping for the specified period of frequency (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDampingInterpolated(
            eDampingTypeInterpolated dampingType,
            double[] periodsOrFrequencies,
            double[] damping)
        {
            // TODO: SAP2000 - SetDampingInterpolated
        }

        /// <summary>
        /// Sets proportional modal damping data for the specified load case.
        /// </summary>
        /// <param name="dampingType">The proportional modal damping type.</param>
        /// <param name="massProportionalDampingCoefficient">The mass proportional damping coefficient.</param>
        /// <param name="stiffnessProportionalDampingCoefficient">The stiffness proportional damping coefficient.</param>
        /// <param name="periodOrFrequencyPt1">The period or frequency for point 1, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="periodOrFrequencyPt2">The period or frequency for point 2, depending on the value of <paramref name="dampingType" />.
        /// [s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> and [cyc/s] for <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByFrequency" />.
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt1">The damping for point 1 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <param name="dampingPt2">The damping for point 2 (0 &lt;= <paramref name="dampingPt1" /> &lt; 1).
        /// This item applies only when <paramref name="dampingType" /> = <see cref="eDampingTypeProportional.ProportionalByPeriod" /> or <see cref="eDampingTypeProportional.ProportionalByFrequency" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDampingProportional(
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt2)
        {
            // TODO: SAP2000 - SetDampingProportional
        }

        /// <summary>
        /// Returns the modal damping overrides assigned to the specified load case.
        /// </summary>
        /// <param name="modes">The modes.</param>
        /// <param name="damping">The damping for the specified mode (0 &lt;= <paramref name="damping" /> &lt; 1).</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDampingOverrides(
            int[] modes,
            double[] damping)
        {
            // TODO: SAP2000 - SetDampingOverrides
        }
#endif
        #endregion
    }
}
