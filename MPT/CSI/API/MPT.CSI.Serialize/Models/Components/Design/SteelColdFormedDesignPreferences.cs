using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class SteelColdFormedDesignPreferences<T> : SteelColdFormedDesignPreferences where T : ColdFormedDesignPreferenceProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected ColdFormedDesignPreferenceProperties _codeProperties;

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
        internal static SteelColdFormedDesignPreferences<T> Factory(T properties = null)
        {
            SteelColdFormedDesignPreferences<T> responseSpectrum = new SteelColdFormedDesignPreferences<T>()
                { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SteelColdFormedDesignPreferences{T}"/> class.
        /// </summary>
        protected SteelColdFormedDesignPreferences()
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

    public class SteelColdFormedDesignPreferences : DesignPreferences, IFrameDesign
    {
    }
}
