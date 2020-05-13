using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class AluminumDesignPreferences<T> : AluminumDesignPreferences
        where T : AluminumDesignPreferenceProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected AluminumDesignPreferenceProperties _codeProperties;

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
        internal static AluminumDesignPreferences<T> Factory(T properties = null)
        {
            AluminumDesignPreferences<T> responseSpectrum = new AluminumDesignPreferences<T>()
                { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AluminumDesignPreferences{T}"/> class.
        /// </summary>
        protected AluminumDesignPreferences()
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

    public class AluminumDesignPreferences : DesignPreferences, IFrameDesign
    {
    }
}
