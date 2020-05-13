﻿using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ShearWall;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class WallDesignPreferences<T> : WallDesignPreferences where T : ShearWallDesignPreferenceProperties, new()
    {
        #region Fields & Properties
        /// <summary>
        /// The code properties
        /// </summary>
        protected ShearWallDesignPreferenceProperties _codeProperties;

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
        internal static WallDesignPreferences<T> Factory(T properties = null)
        {
            WallDesignPreferences<T> responseSpectrum = new WallDesignPreferences<T>()
                { _codeProperties = properties };
            return responseSpectrum;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WallDesignPreferences{T}"/> class.
        /// </summary>
        protected WallDesignPreferences()
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

    public class WallDesignPreferences : DesignPreferences
    {
    }
}
