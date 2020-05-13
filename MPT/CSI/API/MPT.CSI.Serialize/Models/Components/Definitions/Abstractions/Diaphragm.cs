// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-22-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-11-2018
// ***********************************************************************
// <copyright file="Diaphragm.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Diaphragm.
    /// </summary>
    public class Diaphragm : IUniqueName
    {

        // TODO: Include all relevant objects
        // TODO: Implement eDiaphragmOption
        // TODO: Consider constraints in general
        #region Fields & Properties
        /// <summary>
        /// The nodes associated with the diaphragm.
        /// </summary>
        /// <value>The nodes.</value>
        public List<Point> Nodes { get; protected set; }

        /// <summary>
        /// The areas associated with the diaphragm.
        /// </summary>
        /// <value>The areas.</value>
        public List<Area> Areas { get; protected set; }

        /// <summary>
        /// The name of an existing diaphragm.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        /// <summary>
        /// True: The diaphragm is semi-rigid.
        /// False: The diaphragm is rigid.
        /// </summary>
        /// <value><c>true</c> if this instance is semi rigid; otherwise, <c>false</c>.</value>
        public bool IsSemiRigid { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Diaphragm.</returns>
        internal static Diaphragm Factory(string uniqueName)
        {
            Diaphragm diaphragm = new Diaphragm(uniqueName);
           
            return diaphragm;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Diaphragm" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Diaphragm(string name)
        {
            Name = name;
        }
        #endregion

        #region Get/Add/Remove
        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        /// <param name="area">The area.</param>
        /// <returns>Diaphragm.</returns>
        public Diaphragm GetDiaphragm(Area area)
        {
            string diaphragmName = ""; //_areaObject.GetDiaphragm(area.Name);
            return string.IsNullOrEmpty(diaphragmName) ? null : Factory(diaphragmName);
        }

        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>Diaphragm.</returns>
        public Diaphragm GetDiaphragm(Point node)
        {
            string diaphragmName = ""; //_pointObject.GetDiaphragm(node.Name, out var diaphragmOption, out var diaphragmName);
            return string.IsNullOrEmpty(diaphragmName) ? null : Factory(diaphragmName);
        }


        /// <summary>
        /// Adds to diaphragm.
        /// </summary>
        /// <param name="area">The area.</param>
        public void AddToDiaphragm(Area area)
        {
            if (Areas.Contains(area)) return;
            Areas.Add(area);
        }

        /// <summary>
        /// Removes from diaphragm.
        /// </summary>
        /// <param name="area">The area.</param>
        public void RemoveFromDiaphragm(Area area)
        {
            if (!Areas.Contains(area)) return;
            Areas.Remove(area);
        }


        /// <summary>
        /// Adds to diaphragm.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddToDiaphragm(Point node)
        {
            if (Nodes.Contains(node)) return;
            Nodes.Add(node);
        }

        /// <summary>
        /// Adds to diaphragm of bounding area.
        /// </summary>
        /// <param name="node">The node.</param>
        public void AddToDiaphragmOfBoundingArea(Point node)
        {
            if (Nodes.Contains(node)) return;
            // TODO: Finish - determine how to implement eDiaphragmOption 'inheriting from area object'
            //Points.Add(node);
        }

        /// <summary>
        /// Removes from diaphragm.
        /// </summary>
        /// <param name="node">The node.</param>
        public void RemoveFromDiaphragm(Point node)
        {
            if (!Nodes.Contains(node)) return;
            Nodes.Remove(node);
        }
        #endregion
    }
}