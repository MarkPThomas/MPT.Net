using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class ConcreteDesignPreferences<T> : ConcreteDesignPreferences where T : ConcreteDesignPreferencesProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected ConcreteDesignPreferencesProperties _codeProperties;

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
        internal static ConcreteDesignPreferences<T> Factory(T properties = null) 
        {
            ConcreteDesignPreferences<T> responseSpectrum = new ConcreteDesignPreferences<T>()
                { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDesignPreferences{T}"/> class.
        /// </summary>
        protected ConcreteDesignPreferences()
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


    public abstract class ConcreteDesignPreferences : DesignPreferences, IFrameDesign
    {
    }
}
