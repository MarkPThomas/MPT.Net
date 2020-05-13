// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-27-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-28-2019
// ***********************************************************************
// <copyright file="StructureObject2D.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class StructureObject2D.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="StructureObject" />
    public class StructureObject2D<T> : StructureObject
        where T : ObjectProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The points
        /// </summary>
        protected readonly Points _points;

        /// <summary>
        /// The cross sections
        /// </summary>
        protected readonly ObjectLists<T> _crossSections;

        /// <summary>
        /// Gets the point names.
        /// </summary>
        /// <value>The point names.</value>
        internal virtual List<string> PointNames { get; set; }

        /// <summary>
        /// The object points
        /// </summary>
        protected List<Point> _objectPoints;
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public virtual List<Point> Points
        {
            get
            {
                if (_objectPoints != null) return _objectPoints;

                _objectPoints = new List<Point>();
                foreach (var pointName in PointNames)
                {
                    Point point = _points.FillItem(pointName);
                    _objectPoints.Add(point);
                }

                return _objectPoints;
            }
        }

        /// <summary>
        /// The section name
        /// </summary>
        protected string _sectionName;
        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        internal virtual string SectionName
        {
            get => _sectionName;
            set => _sectionName = value;
        }

        /// <summary>
        /// The cross section
        /// </summary>
        protected T _crossSection;
        /// <summary>
        /// Gets or sets the cross section.
        /// </summary>
        /// <value>The cross section.</value>
        internal virtual T CrossSection => _crossSection ?? (_crossSection = null);
        #endregion  

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObjectWithCrossSection{T}" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal StructureObject2D(
            StructureComponentsProperties<T> componentsProperties,
            string name) : base(name)
        {
            _points = componentsProperties.Points;
            _crossSections = componentsProperties.CrossSections;
        }





        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <returns>System.String.</returns>
        protected string getSectionName()
        {
            return _sectionName;
        }

        /// <summary>
        /// Assigns the section property to an object.
        /// </summary>
        /// <param name="section">The the section property assigned to the object.</param>
        protected void setSection(T section)
        {
            _sectionName = section.Name;
            _crossSection = section;
        }
    }
}
