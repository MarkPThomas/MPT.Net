// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-26-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="StructureObjectWithCrossSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Represents a 2D structure such as a frame or area.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="StructureObject" />
    public abstract class StructureObjectWithCrossSection<T> : StructureObject2D<T>
        where T: CrossSection
    {
        #region Fields & Properties
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials _materials;

        /// <summary>
        /// The piers
        /// </summary>
        protected readonly Piers _piers;

        /// <summary>
        /// The spandrels
        /// </summary>
        protected readonly Spandrels _spandrels;

        /// <summary>
        /// The pier name
        /// </summary>
        private string _pierName;
        /// <summary>
        /// Gets the name of the pier.
        /// </summary>
        /// <value>The name of the pier.</value>
        internal virtual string PierName => _pierName;

        /// <summary>
        /// The pier
        /// </summary>
        private Pier _pier;
        /// <summary>
        /// The pier assigned to the object.
        /// </summary>
        /// <value>The pier.</value>
        public virtual Pier Pier => _pier ?? (_pier = null);

        /// <summary>
        /// The spandrel name
        /// </summary>
        private string _spandrelName;
        /// <summary>
        /// Gets the name of the spandrel.
        /// </summary>
        /// <value>The name of the spandrel.</value>
        internal virtual string SpandrelName => _spandrelName;

        /// <summary>
        /// The spandrel
        /// </summary>
        private Spandrel _spandrel;
        /// <summary>
        /// The spandrel assigned to the object.
        /// </summary>
        /// <value>The spandrel.</value>
        public Spandrel Spandrel => _spandrel ?? (_spandrel = null);
        
        
        /// <summary>
        /// The material overwrite name
        /// </summary>
        private string _materialOverwriteName;
        /// <summary>
        /// Gets the name of the material overwrite.
        /// </summary>
        /// <value>The name of the material overwrite.</value>
        internal virtual string MaterialOverwriteName => _materialOverwriteName;

        /// <summary>
        /// The material overwrite
        /// </summary>
        protected Material _materialOverwrite;
        /// <summary>
        /// The material overwrite assigned to the object.
        /// This overwrites the material used in the cross-section.
        /// </summary>
        /// <value>The material overwrite.</value>
        public virtual Material MaterialOverwrite => _materialOverwrite ?? (_materialOverwrite = null);

        /// <summary>
        /// The material used.
        /// </summary>
        /// <value>The material used.</value>
        public Material MaterialUsed => MaterialOverwrite ?? CrossSection.Material;
        
        // TODO: Make this an object, where only one overwrite for each code type is allowed.
        /// <summary>
        /// The design overwrites
        /// </summary>
        protected List<DesignOverwrites> _designOverwrites;
        /// <summary>
        /// Gets the design overwrites.
        /// </summary>
        /// <value>The design overwrites.</value>
        public virtual List<DesignOverwrites> DesignOverwrites => _designOverwrites ??
                                                                  (_designOverwrites = new List<DesignOverwrites>());

        /// <summary>
        /// The mass
        /// </summary>
        private double? _mass;
        /// <summary>
        /// Mass per unit length or pass per unit area assignment from objects, depending on whether the object is a line or area. [M/L] or [M/L^2]
        /// </summary>
        /// <value>The mass.</value>
        public virtual double Mass => _mass ?? 0;

        /// <summary>
        /// The temperature loads
        /// </summary>
        private List<LoadTemperature> _temperatureLoads;
        /// <summary>
        /// The temperature loads assigned to the object.
        /// </summary>
        /// <value>The temperature loads.</value>
        public virtual List<LoadTemperature> TemperatureLoads => _temperatureLoads;
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObjectWithCrossSection{T}" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal StructureObjectWithCrossSection(
            StructureComponentsProperties<T> componentsProperties,
            string name) : base(componentsProperties, name)
        {
            _materials = componentsProperties.Materials;
            _piers = componentsProperties.Piers;
            _spandrels = componentsProperties.Spandrels;
        }

        
        #region Cross-Section & Material Properties
        // IMass
        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        public abstract void SetMass(double value,
            bool replace);

        // IDeletableMass
        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        public abstract void DeleteMass();

        // IObservableMaterialOverwrite
        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <returns>System.String.</returns>
        public abstract string GetMaterialOverwriteName();

        // IChangeableMaterialOverwrite
        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">The material.</param>
        public abstract void AddMaterialOverwrite(Material material);

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        public abstract void RemoveMaterialOverwrite();

        // IObservableSection
        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <returns>System.String.</returns>
        public abstract string GetSectionName();
        #endregion

        #region Design
        // IPier
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public abstract string GetPierName();

        /// <summary>
        /// Adds the pier label assignment to the object.
        /// Any existing pier label is replaced.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        public abstract void AddToPier(Pier pier);

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public abstract void RemoveFromPier();

        // ISpandrel
        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        public abstract string GetSpandrelName();

        /// <summary>
        /// Adds the spandrel label assignment to the object.
        /// Any existing spandrel label is replaced.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        public abstract void AddToSpandrel(Spandrel spandrel);

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public abstract void RemoveFromSpandrel();
        #endregion

        #region Loads
        // LoadTemperature
        /// <summary>
        /// Assigns temperature loads to objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        public abstract void AddLoadTemperature(LoadTemperature temperatureLoad);

        /// <summary>
        /// Assigns temperature loads to objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        public abstract void ReplaceLoadTemperature(LoadTemperature temperatureLoad);

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        public abstract void DeleteLoadTemperature(string loadPattern);
        #endregion

        /// <summary>
        /// Converts the coordinates to points.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>List&lt;Point&gt;.</returns>
        internal static List<Point> ConvertCoordsToPoints(
            StructureComponentsProperties<T> componentsProperties,
            List<Coordinate3DCartesian> coordinates)
        {
            List<Point> points = new List<Point>();
            foreach (var coordinate in coordinates)
            {
                componentsProperties.Points.Add(coordinate);
                points.Add(componentsProperties.Points[coordinate]);
            }

            return points;
        }

        /// <summary>
        /// Adds or replaces the provided list property using the provided item.
        /// </summary>
        /// <typeparam name="TU">The type of the tu.</typeparam>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <param name="item">The item.</param>
        /// <param name="items">The items.</param>
        /// <returns>List&lt;T&gt;.</returns>
        protected List<TU> addOrReplace<TU>(bool replace, TU item, List<TU> items)
        {
            if (replace)
            {
                items = new List<TU>() { item };
            }
            else
            {
                if (items == null)
                {
                    items = new List<TU>();
                }
                items.Add(item);
            }

            return items;
        }

        
        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        protected void setMass(
            double value,
            bool replace)
        {
            if (replace)
            {
                _mass = value;
            }
            else
            {
                _mass += value;
            }
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        protected void deleteMass()
        {
            _mass = null;
        }

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <returns>System.String.</returns>
        protected string getMaterialOverwriteName()
        {
            return MaterialOverwriteName;
        }

        /// <summary>
        /// Sets the material overwrite assignment for objects.
        /// </summary>
        /// <param name="material">The material.</param>
        protected void setMaterialOverwrite(Material material)
        {
            _materialOverwrite = material;
            _materialOverwriteName = material.Name;
        }


        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        protected string getPierName()
        {
            return _pierName;
        }

        /// <summary>
        /// Adds the pier label assignment of the object.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        protected void addPier(Pier pier)
        {
            _pierName = pier.Name;
            _pier = pier;
        }

        /// <summary>
        /// Removes the pier label assignment of the object.
        /// </summary>
        protected void removePier()
        {
            _pierName = string.Empty;
            _pier = null;
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <returns>System.String.</returns>
        protected string getSpandrelName()
        {
            return _spandrelName;
        }

        /// <summary>
        /// Adds the pier label assignment of the object.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        protected void addSpandrel(Spandrel spandrel)
        {
            _spandrelName = spandrel.Name;
            _spandrel = spandrel;
        }

        /// <summary>
        /// Removes the spandrel label assignment of the object.
        /// </summary>
        protected void removeSpandrel()
        {
            _spandrelName = string.Empty;
            _spandrel = null;
        }

        /// <summary>
        /// Assigns temperature loads to objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        protected void setLoadTemperature(
            LoadTemperature temperatureLoad,
            bool replace)
        {
            addOrReplace(replace, temperatureLoad, _temperatureLoads);
        }
    }
}
