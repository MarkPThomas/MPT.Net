// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ShellLayered.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class ShellLayered.
    /// </summary>
    /// <seealso cref="AreaSection{T}" />
    public class ShellLayered : ApiPropertyObject<ShellLayeredProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiAreaSection _apiAreaSection => getApiAreaSection(_apiApp);

        /// <summary>
        /// Gets the shell layer data.
        /// </summary>
        /// <returns>ReadOnlyCollection&lt;ShellLayerProperties&gt;.</returns>
        protected ReadOnlyCollection<ShellLayerProperties> GetLayers()
        {
            if (Properties.Layers.Count == 0)
            {
                Fill();
            }

            return new ReadOnlyCollection<ShellLayerProperties>(Properties.Layers);
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static ShellLayered Factory(ApiCSiApplication app, string uniqueName, ShellLayeredProperties properties = null)
        {
            ShellLayered areaSection = new ShellLayered(app, uniqueName) { _properties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellBase" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected ShellLayered(ApiCSiApplication app, string name) : base(app, name)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Fill the layer properties.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetShellLayer(Name,
                out var layerNames,
                out var distanceOffsets,
                out var thicknesses,
                out var layerTypes,
                out var numberOfIntegrationPoints,
                out var materialProperties,
                out var materialAngles,
                out var S11Types,
                out var S22Types,
                out var S12Types);

            if (!(_properties is ShellLayeredProperties properties)) return;
            properties.Layers.Clear();
            for (int i = 0; i < layerNames.Length; i++)
            {
                ShellLayerProperties layerProperties = new ShellLayerProperties
                {
                    LayerName = layerNames[i],
                    DistanceOffset = distanceOffsets[i],
                    Thickness = thicknesses[i],
                    LayerType = layerTypes[i],
                    NumberOfIntegrationPoints = numberOfIntegrationPoints[i],
                    MaterialName = materialProperties[i],
                    MaterialAngle = materialAngles[i],
                    S11Type = S11Types[i],
                    S22Type = S22Types[i],
                    S12Type = S12Types[i]
                };
                properties.Layers.Add(layerProperties);
            }
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(ShellLayeredProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
            set(properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, ShellLayeredProperties properties)
        {
            apiAreaSection.SetShellLayer(name,
                properties.Layers.Select(p => p.LayerName).ToArray(),
                properties.Layers.Select(p => p.DistanceOffset).ToArray(),
                properties.Layers.Select(p => p.Thickness).ToArray(),
                properties.Layers.Select(p => p.LayerType).ToArray(),
                properties.Layers.Select(p => p.NumberOfIntegrationPoints).ToArray(),
                properties.Layers.Select(p => p.MaterialName).ToArray(),
                properties.Layers.Select(p => p.MaterialAngle).ToArray(),
                properties.Layers.Select(p => p.S11Type).ToArray(),
                properties.Layers.Select(p => p.S22Type).ToArray(),
                properties.Layers.Select(p => p.S12Type).ToArray()
            );
        }
        #endregion
    }
}
