using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeBeam;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class CompositeBeamDesignPreferences<T> : CompositeBeamDesignPreferences
        where T : CompositeBeamDesignPreferenceProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected CompositeBeamDesignPreferenceProperties _codeProperties;

        /// <summary>
        /// The code properties associated with the response spectrum.
        /// </summary>
        /// <value>The code properties.</value>
        public T CodeProperties => (T)_codeProperties.Clone();
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>ResponseSpectrumCodeFunctions&lt;T&gt;.</returns>
        internal static CompositeBeamDesignPreferences<T> Factory(T properties = null)
        {
            CompositeBeamDesignPreferences<T> responseSpectrum = new CompositeBeamDesignPreferences<T>()
                { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeBeamDesignPreferences{T}"/> class.
        /// </summary>
        protected CompositeBeamDesignPreferences()
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public void SetProperties(T properties)
        {
            _codeProperties = properties;
        }
        #endregion
    }

    public class CompositeBeamDesignPreferences : DesignPreferences, IFrameDesign
    {
    }
}
