using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    public abstract class DesignerMetal : Designer
    {

        // TODO: Make CombinationsDeflection into object rather than names?
        public List<string> CombinationsDeflection { get; protected set; }
        
        /// <summary>
        /// Name of the modal load case for which the target applies.
        /// </summary>
        /// <value>The modal case.</value>
        public string ModalCase { get; protected set; }

        /// <summary>
        /// True: All specified period targets are active.
        /// False: They are inactive.
        /// </summary>
        /// <value><c>true</c> if [all specified period targets active]; otherwise, <c>false</c>.</value>
        public bool AllSpecifiedPeriodTargetsActive { get; protected set; }

        /// <summary>
        /// True: All specified lateral displacement targets are active.
        /// False: They are inactive.
        /// </summary>
        /// <value><c>true</c> if [all specified displacement targets active]; otherwise, <c>false</c>.</value>
        public bool AllSpecifiedDisplacementTargetsActive { get; protected set; }

        /// <summary>
        /// Gets or sets the target periods.
        /// </summary>
        /// <value>The target period.</value>
        public List<TargetPeriod> TargetPeriods { get; protected set; } = new List<TargetPeriod>();

        /// <summary>
        /// Gets or sets the target displacements.
        /// </summary>
        /// <value>The target displacement.</value>
        public List<TargetDisplacement> TargetDisplacements { get; protected set; } = new List<TargetDisplacement>();

        #region Region
        /// <summary>
        /// Gets the names of all load combinations used for deflection design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillComboDeflection();

        /// <summary>
        /// Selects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddComboDeflection(string nameLoadCombination);

        /// <summary>
        /// Deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void RemoveComboDeflection(string nameLoadCombination);

        #endregion

        #region Targets

        /// <summary>
        /// Retrieves lateral displacement targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillTargetDisplacements();

        /// <summary>
        /// Sets the target displacements.
        /// </summary>
        /// <param name="allSpecifiedTargetsActive">if set to <c>true</c> [all specified targets active].</param>
        public abstract void SetTargetDisplacements(bool allSpecifiedTargetsActive);

        /// <summary>
        /// Retrieves time period targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillTargetPeriods();

        /// <summary>
        /// Sets time period targets for steel design.
        /// </summary>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetTargetPeriods(bool allSpecifiedTargetsActive);
        #endregion

        #region API Functions
        /// <summary>
        /// Gets the names of all load combinations used for deflection design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getComboDeflection(IComboDeflection app)
        {
            CombinationsDeflection = new List<string>(app.GetComboDeflection());
        }

        /// <summary>
        /// Selects or deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design.
        /// False: The combination is not selected for deflection design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setComboDeflection(IComboDeflection app, string nameLoadCombination, bool selectLoadCombination)
        {
            app.SetComboDeflection(nameLoadCombination, selectLoadCombination);
            if (selectLoadCombination)
            {
                if (!CombinationsDeflection.Contains(nameLoadCombination)) CombinationsDeflection.Add(nameLoadCombination);
            }
            else
            {
                CombinationsDeflection.Remove(nameLoadCombination);
            }
        }

        /// <summary>
        /// Retrieves lateral displacement targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getTargetDisplacements(ITargetDisplacement app)
        {
            app.GetTargetDisplacement(
                out var loadCase,
                out var namePoint,
                out var displacementTargets,
                out var allSpecifiedTargetsActive);

            TargetDisplacements.Clear();
            AllSpecifiedDisplacementTargetsActive = allSpecifiedTargetsActive;
            for (int i = 0; i < loadCase.Length; i++)
            {
                TargetDisplacement targetDisplacement = new TargetDisplacement()
                {
                    PointName = namePoint[i],
                    Value = displacementTargets[i]
                };
                TargetDisplacements.Add(targetDisplacement);
            }
        }

        /// <summary>
        /// Sets the target displacements.
        /// </summary>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        protected void setTargetDisplacements(ITargetDisplacement app, bool allSpecifiedTargetsActive)
        {
            string[] loadCases = new string[TargetDisplacements.Count];
            for (int i = 0; i < TargetPeriods.Count; i++)
            {
                loadCases[i] = TargetDisplacements[i].LoadCase;
            }

            string[] namePoints = new string[TargetDisplacements.Count];
            for (int i = 0; i < TargetPeriods.Count; i++)
            {
                namePoints[i] = TargetDisplacements[i].PointName;
            }

            double[] displacementTargets = new double[TargetDisplacements.Count];
            for (int i = 0; i < TargetPeriods.Count; i++)
            {
                displacementTargets[i] = TargetDisplacements[i].Value;
            }

            app.SetTargetDisplacement(loadCases, namePoints, displacementTargets, allSpecifiedTargetsActive);

            AllSpecifiedDisplacementTargetsActive = allSpecifiedTargetsActive;
        }

        /// <summary>
        /// Retrieves time period targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getTargetPeriods(ITargetPeriod app)
        {
            app.GetTargetPeriod(
                out var modalCase,
                out var modeNumbers,
                out var periodTargets,
                out var allSpecifiedTargetsActive);

            ModalCase = modalCase;
            AllSpecifiedPeriodTargetsActive = allSpecifiedTargetsActive;
            TargetPeriods.Clear();
            for (int i = 0; i < modeNumbers.Length; i++)
            {
                TargetPeriod targetPeriod = new TargetPeriod()
                {
                    ModeNumber = modeNumbers[i],
                    Value = periodTargets[i]
                };
                TargetPeriods.Add(targetPeriod);
            }
        }

        /// <summary>
        /// Sets time period targets for steel design.
        /// </summary>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setTargetPeriods(ITargetPeriod app, bool allSpecifiedTargetsActive)
        {
            int[] modeNumbers = new int[TargetPeriods.Count];
            for (int i = 0; i < TargetPeriods.Count; i++)
            {
                modeNumbers[i] = TargetPeriods[i].ModeNumber;
            }

            double[] periodTargets = new double[TargetPeriods.Count];
            for (int i = 0; i < TargetPeriods.Count; i++)
            {
                periodTargets[i] = TargetPeriods[i].Value;
            }

            app.SetTargetPeriod(ModalCase, modeNumbers, periodTargets, allSpecifiedTargetsActive);

            AllSpecifiedPeriodTargetsActive = allSpecifiedTargetsActive;
        }
        #endregion
    }
}
