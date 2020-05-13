using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    public abstract class TimeHistoryDirectIntegration : TimeHistory
    {
        #region Fields & Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase { get; protected set; }

        /// <summary>
        /// Gets or sets the proportional damping.
        /// </summary>
        /// <value>The damping proportional.</value>
        public DampingProportional DampingProportional { get; protected set; }
#endif
        #endregion

        #region Initialization


        protected TimeHistoryDirectIntegration(string name) : base(name)
        {
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            InitialCase.FillInitialCase();
            FillDampingProportional();
            FillTimeIntegration();
#endif
        }
        #endregion

        #region Fill/Set
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the proportional modal damping data assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillDampingProportional();

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
        public abstract void SetDampingPropertional(
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt);

        /// <summary>
        /// Returns the time integration data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillTimeIntegration(string name,
            out eTimeIntegrationType integrationType,
            out double alpha,
            out double beta,
            out double gamma,
            out double theta,
            out double alphaM);
        
        /// <summary>
        /// Sets time integration data for the specified load case.
        /// </summary>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetTimeIntegration(
            eTimeIntegrationType integrationType,
            double alpha,
            double beta,
            double gamma,
            double theta,
            double alphaM);
#endif
        #endregion

        #region API Functions

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // ====== Damping Proportional ======
        
        /// <summary>
        /// Returns the proportional modal damping data assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void fillDampingProportional(IDampingProportional app)
        {
            if (app == null) return;
            app.GetDampingProportional(Name,
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
        protected void setDampingProportional(
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt2)
        {

        }


        // ====== Time Integration ======

        /// <summary>
        /// Returns the time integration data assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getTimeIntegration()
        {

        }


        /// <summary>
        /// Sets time integration data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing integration time history load case.</param>
        /// <param name="integrationType">The time integration type.</param>
        /// <param name="alpha">The alphafactor (-1/3 &lt;= <paramref name="alpha" /> &lt;= 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <param name="beta">The beta factor (<paramref name="beta" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="gamma">The gamma factor (<paramref name="gamma" /> &gt;= 0.5).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Newmark" />, <see cref="eTimeIntegrationType.Collocation" /> or <see cref="eTimeIntegrationType.ChungHulbert" />.
        /// It is returned for informational purposes when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.HilberHughesTaylor" />.</param>
        /// <param name="theta">The theta factor (<paramref name="theta" /> &gt; 0).
        /// This item applies only when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.Wilson" /> or <see cref="eTimeIntegrationType.Collocation" />.</param>
        /// <param name="alphaM">The alpha-m factor.
        /// This item only applies when <paramref name="integrationType" /> = <see cref="eTimeIntegrationType.ChungHulbert" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setTimeIntegration(string name,
            eTimeIntegrationType integrationType,
            double alpha,
            double beta,
            double gamma,
            double theta,
            double alphaM)
        {

        }
#endif

        #endregion
    }
}
