// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Node.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Components.Grids;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Stiffnesses;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;
using MassObject = MPT.CSI.Serialize.Models.Components.Definitions.Masses.Mass;


namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Node.
    /// </summary>
    /// <seealso cref="StructureObject" />
    public class Point : StructureObject
    {
        #region Fields & Properties
        /// <summary>
        /// The results
        /// </summary>
        private JointResults _results;
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public JointResults Results => _results?? (_results = new JointResults(Name));

        /// <summary>
        /// The coordinates.
        /// </summary>
        protected Coordinate3DCartesian _coordinates;
        /// <summary>
        /// The x-axis coordinate.
        /// </summary>
        /// <value>The x.</value>
        public double X => _coordinates.X;

        /// <summary>
        /// The y-axis coordinate.
        /// </summary>
        /// <value>The y.</value>
        public double Y => _coordinates.Y;

        /// <summary>
        /// The z-axis coordinate.
        /// </summary>
        /// <value>The z.</value>
        public double Z => _coordinates.Z;

        public string CoordinateSystem { get; internal set; } = CoordinateSystems.Global;

        public eCoordinateType CoordinateType { get; internal set; }

        /// <summary>
        /// These are the restraint assignments for each local degree of freedom (DOF), where 'True' means the DOF is fixed.
        /// </summary>
        /// <value>The degrees of freedom.</value>
        public DegreesOfFreedomLocal DegreesOfFreedom { get; internal set; }

        /// <summary>
        /// The panel zone
        /// </summary>
        private PanelZone _panelZone;
        /// <summary>
        /// The panel zone associated with the node.
        /// </summary>
        /// <value>The panel zone.</value>
        public PanelZone PanelZone => _panelZone ?? (_panelZone = PanelZone.Factory(Name));

        /// <summary>
        /// Spring stiffness values for each decoupled degree of freedom.
        /// </summary>
        /// <value>The stiffness.</value>
        public Stiffness Stiffness { get; internal set; }

        /// <summary>
        /// Spring stiffness values for each coupled degree of freedom.
        /// </summary>
        /// <value>The stiffness coupled.</value>
        public StiffnessCoupled StiffnessCoupled { get; internal set; }

        /// <summary>
        /// Indicates if the spring assignments to a point object are coupled;
        /// that is, if there are off-diagonal terms in the 6x6 spring matrix for the point element.
        /// </summary>
        /// <value><c>true</c> if this instance is spring coupled; otherwise, <c>false</c>.</value>
        public bool IsSpringCoupled { get; internal set; }

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        /// <value>The mass.</value>
        public MassObject Mass { get; internal set; }


        internal List<string> ConstraintNames { get; set; } = new List<string>();
        /// <summary>
        /// Gets or sets the diaphragm.
        /// </summary>
        /// <value>The diaphragm.</value>
        public Diaphragm Diaphragm { get; internal set; }

        /// <summary>
        /// Gets or sets the loads.
        /// </summary>
        /// <value>The loads.</value>
        public List<NodeLoad> Loads { get; internal set; }

        /// <summary>
        /// Gets or sets the displacements.
        /// </summary>
        /// <value>The displacements.</value>
        public List<NodeLoadDisplacement> Displacements { get; internal set; }

        /// <summary>
        /// True: This instance is a special point.
        /// </summary>
        /// <value><c>true</c> if this instance is special point; otherwise, <c>false</c>.</value>
        public bool IsSpecialPoint { get; internal set; }


        public int MergeNumber { get; internal set; }

        public virtual AngleLocalAxes LocalAxes { get; internal set; } = new AngleLocalAxes();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        internal static Point Factory(string uniqueName)
        {
            Point item = new Point(uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Point(string name) : base(name) { }

        #endregion

        #region Creation
        /// <summary>
        /// Adds a point object to the model.
        /// The added point object will be tagged as a Special Point except if it was merged with another point object.
        /// Special points are allowed to exist in the model with no objects connected to them.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// If a point is merged with another point, this will be the name of the point object with which it was merged.
        /// </summary>
        /// <param name="coordinate">The coordinate.</param>
        /// <param name="uniqueName">Unique name of the point.
        /// If not supplied, the program will generate one automatically.</param>
        /// <returns>Point.</returns>
        internal static Point AddByCoordinate(
            Coordinate3DCartesian coordinate,
            string uniqueName = "")
        {
            Point item = Factory(uniqueName);
            item._coordinates = coordinate;
            return item;
        }
        #endregion
    }
}
