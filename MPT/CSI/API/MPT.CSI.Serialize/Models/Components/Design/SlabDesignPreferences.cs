using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Slab;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class SlabDesignPreferences<T> : SlabDesignPreferences where T : SlabDesignPreferenceProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected SlabDesignPreferenceProperties _codeProperties;

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
        internal static SlabDesignPreferences<T> Factory(T properties = null)
        {
            SlabDesignPreferences<T> responseSpectrum = new SlabDesignPreferences<T>()
                { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlabDesignPreferences{T}"/> class.
        /// </summary>
        protected SlabDesignPreferences()
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

    public class SlabDesignPreferences : DesignPreferences
    {
    }
}
