using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    public class DampingHelper : IFill
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
        /// Hysteretic damping type for the load case.
        /// </summary>
        /// <value>The type of the damping.</value>
        public eDampingType DampingType { get; protected set; }

        /// <summary>
        /// Constant modal damping for all modes (0 &lt;= damping &lt; 1) assigned to the load case.
        /// </summary>
        /// <value>The damping constant.</value>
        public double DampingConstant { get; protected set; }

        /// <summary>
        /// Gets or sets the interpolated damping.
        /// </summary>
        /// <value>The damping interpolated.</value>
        public List<DampingInterpolated> DampingInterpolated { get; protected set; } = new List<DampingInterpolated>();

        /// <summary>
        /// Gets or sets the proportional damping.
        /// </summary>
        /// <value>The damping proportional.</value>
        public DampingProportional DampingProportional { get; protected set; }

        /// <summary>
        /// The damping overwrites of mode #, overwrite.
        /// </summary>
        /// <value>The damping overwrites.</value>
        public List<DampingOverride> DampingOverwrites { get; protected set; } = new List<DampingOverride>();
        #endregion

        #region Initialization

        public DampingHelper(string caseName, IDampingModal app)
        {
            _app = app;
            CaseName = caseName;
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public void FillData()
        {
            FillDampingType();
            FillDampingConstant();
            FillDampingInterpolated();
            FillDampingProportional();
            FillDampingOverrides();
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
            DampingType = _app.GetDampingType(CaseName);
        }


        /// <summary>
        /// Returns the constant modal damping for all modes (0 &lt;= damping &lt; 1) assigned to the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDampingConstant()
        {
            if (_app == null) return;
            DampingConstant = _app.GetDampingConstant(CaseName);
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

            for (int i = 0; i < periodsOrFrequencies.Length; i++)
            {
                DampingInterpolated dampingInterpolated = new DampingInterpolated()
                {
                    DampingType = dampingType,
                    PeriodOrFrequency = periodsOrFrequencies[i],
                    Damping = damping[i]
                };
                DampingInterpolated.Add(dampingInterpolated);
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

            DampingProportional = new DampingProportional()
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

            for (int i = 0; i < modes.Length; i++)
            {
                DampingOverride dampingOverride = new DampingOverride()
                {
                    ModeNumber = modes[i],
                    Damping = damping[i]
                };
                DampingOverwrites.Add(dampingOverride);
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
            // TODO: Finish for SAP2000
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

        }
#endif
        #endregion

        #region API Functions



        #endregion
    }
}
