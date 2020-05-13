// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-24-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="PanelZone.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiPointObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.PointObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class PanelZone.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class PanelZone : CSiOoApiBaseBase
    {
        /// <summary>
        /// The API application point object.
        /// </summary>
        /// <value>The point object.</value>
        protected ApiPointObject _apiPointObject => getApiPointObject(_apiApp);


        /// <summary>
        /// The results
        /// </summary>
        private PanelZoneResults _results;
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public PanelZoneResults Results => _results ?? (_results = new PanelZoneResults(_apiApp, NodeName));

        /// <summary>
        /// Name of the node associated with the panel zone.
        /// </summary>
        /// <value>The name of the node.</value>
        public string NodeName { get; protected set; }

        /// <summary>
        /// The panel zone properties
        /// </summary>
        private PanelZoneProperties _panelZoneProperties;

        /// <summary>
        /// Gets the panel zone properties.
        /// </summary>
        /// <value>The panel zone properties.</value>
        public PanelZoneProperties PanelZoneProperties
        {
            get
            {
                if (_panelZoneProperties == null)
                {
                    FillPanelZone();
                }

                return _panelZoneProperties;
            }
        }


        /// <summary>
        /// Returns the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <returns>PanelZone.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static PanelZone Factory(ApiCSiApplication app, string name)
        {
            PanelZone panelZone = new PanelZone(app, name);
            return panelZone;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PanelZone" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name of the node associated with the panel zone.</param>
        protected PanelZone(ApiCSiApplication app, string name) : base(app)
        {
            NodeName = name;
        }

        /// <summary>
        /// Returns the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillPanelZone()
        {
            if (_apiPointObject == null) return;
            _apiPointObject.GetPanelZone(NodeName,
                out var propertyType,
                out var thickness,
                out var k1Out,
                out var k2Out,
                out var linkProperty,
                out var connectivity,
                out var localAxisFrom,
                out var localAxisAngle);

            _panelZoneProperties = new PanelZoneProperties
            {
                PropertyType = propertyType,
                Thickness = thickness,
                K1 = k1Out,
                K2 = k2Out,
                LinkProperty = linkProperty,
                Connectivity = connectivity,
                LocalAxisFrom = localAxisFrom,
                LocalAxisAngle = localAxisAngle
            };
        }

        /// <summary>
        /// Sets panel zone assignments to point objects. Any existing panel zone assignments are replaced by the new assignments.
        /// </summary>
        /// <param name="panelZoneProperties">The panel zone properties.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPanelZone(PanelZoneProperties panelZoneProperties)
        {
            _apiPointObject?.SetPanelZone(NodeName,
                panelZoneProperties.PropertyType,
                panelZoneProperties.Thickness,
                panelZoneProperties.K1,
                panelZoneProperties.K2,
                panelZoneProperties.LinkProperty,
                panelZoneProperties.Connectivity,
                panelZoneProperties.LocalAxisFrom,
                panelZoneProperties.LocalAxisAngle);

            _panelZoneProperties = panelZoneProperties;
        }

        /// <summary>
        /// Deletes all panel zone assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePanelZone()
        {
            _apiPointObject?.DeletePanelZone(NodeName);
        }
    }
}
