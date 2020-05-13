using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    public class MassSourceHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The API object.
        /// </summary>
        protected static IMassSource _app;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        private MassSource _massSource = null;
        /// <summary>
        /// This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.
        /// </summary>
        /// <value>The name of the mass source.</value>
        public string MassSourceName => _massSource == null ? string.Empty : _massSource.Name;
        #endregion

        public MassSourceHelper(string caseName, IMassSource app)
        {
            _app = app;
            CaseName = caseName;
        }


        /// <summary>
        /// Retrieves the mass source to be used for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillMassSource()
        {
            _massSource = new MassSource(_app?.GetMassSource(CaseName));
        }

        /// <summary>
        /// Sets the mass source to be used for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMassSource(MassSource massSource)
        {
            _massSource = massSource;
            setMassSource();
        }

        /// <summary>
        /// Removes the mass source to be used for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveMassSource()
        {
            _massSource = null;
            setMassSource();
        }

        /// <summary>
        /// Sets the mass source.
        /// </summary>
        protected void setMassSource()
        {
            _app?.SetMassSource(CaseName, MassSourceName);
        }
    }
}
