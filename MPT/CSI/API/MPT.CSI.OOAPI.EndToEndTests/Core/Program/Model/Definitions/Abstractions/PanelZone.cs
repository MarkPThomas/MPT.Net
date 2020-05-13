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

using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class PanelZone.
    /// </summary>
    public class PanelZone
    {
        /// <summary>
        /// Gets the point object.
        /// </summary>
        /// <value>The point object.</value>
        protected static PointObject _pointObject => Registry.ObjectModeler?.PointObject;


        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public PanelZoneResults Results { get; protected set; }

        /// <summary>
        /// Name of the node associated with the panel zone.
        /// </summary>
        /// <value>The name of the node.</value>
        public string NodeName { get; protected set; }

        /// <summary>
        /// Method by which properties are determined for panel zones.
        /// </summary>
        /// <value>The type of the property.</value>
        public ePanelZonePropertyType PropertyType { get; set; }

        /// <summary>
        /// The thickness of the doubler plate.
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate" />. [L]
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }

        /// <summary>
        /// The spring stiffness for major axis bending (about the local 3 axis of the column and panel zone).
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]
        /// </summary>
        /// <value>The K1.</value>
        public double K1 { get; set; }

        /// <summary>
        /// The spring stiffness for minor axis bending (about the local 2 axis of the column and panel zone).
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromSpringStiffness" />. [FL/rad]
        /// </summary>
        /// <value>The K2.</value>
        public double K2 { get; set; }

        /// <summary>
        /// The name of the link property used to define the panel zone.
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromLink" />.
        /// </summary>
        /// <value>The link property.</value>
        public string LinkProperty { get; set; }

        /// <summary>
        /// Panel zone connection types.
        /// </summary>
        /// <value>The connectivity.</value>
        public ePanelZoneConnectivity Connectivity { get; set; }

        /// <summary>
        /// Method by which the local axis is defined.
        /// The <see cref="LocalAxisFrom" /> item can be <see cref="ePanelZoneLocalAxis.UserDefined" /> only when the <see cref="PropertyType" /> item is <see cref="ePanelZonePropertyType.FromLink" />.
        /// </summary>
        /// <value>The local axis from.</value>
        public ePanelZoneLocalAxis LocalAxisFrom { get; set; }

        /// <summary>
        /// This item applies only when <see cref="PropertyType" /> = <see cref="ePanelZonePropertyType.FromLink" /> and <see cref="LocalAxisFrom" /> = <see cref="ePanelZoneLocalAxis.UserDefined" />.
        /// It is the angle measured counter clockwise from the positive global X-axis to the local 2-axis of the panel zone. [deg]
        /// </summary>
        /// <value>The local axis angle.</value>
        public double LocalAxisAngle { get; set; }


        /// <summary>
        /// Returns the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>PanelZone.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static PanelZone Factory(string name)
        {
            PanelZone panelZone = new PanelZone(name) {Results = new PanelZoneResults(name)};
            panelZone.FillPanelZone();
            return panelZone;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PanelZone" /> class.
        /// </summary>
        /// <param name="name">The name of the node associated with the panel zone.</param>
        protected PanelZone(string name)
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
            if (_pointObject == null) return;
            _pointObject.GetPanelZone(NodeName,
                out var propertyType,
                out var thickness,
                out var k1Out,
                out var k2Out,
                out var linkProperty,
                out var connectivity,
                out var localAxisFrom,
                out var localAxisAngle);

            PropertyType = propertyType;
            Thickness = thickness;
            K1 = k1Out;
            K2 = k2Out;
            LinkProperty = linkProperty;
            Connectivity = connectivity;
            LocalAxisFrom = localAxisFrom;
            LocalAxisAngle = localAxisAngle;
        }

        /// <summary>
        /// Sets panel zone assignments to point objects. Any existing panel zone assignments are replaced by the new assignments.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPanelZone()
        {
            _pointObject?.SetPanelZone(NodeName,
                PropertyType,
                Thickness,
                K1,
                K2,
                LinkProperty,
                Connectivity,
                LocalAxisFrom,
                LocalAxisAngle);
        }

        /// <summary>
        /// Deletes all panel zone assignments from the specified point object(s).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeletePanelZone()
        {
            _pointObject?.DeletePanelZone(NodeName);
        }
    }
}
