// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-26-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="StructureObject2D.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Assignments;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Represents a 2D structure such as a frame or area.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.StructureObject" />
    public abstract class StructureObject2D<T> : StructureObject
        where T : CrossSection
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

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// The pier name
        /// </summary>
        private string _pierName;
        /// <summary>
        /// Gets the name of the pier.
        /// </summary>
        /// <value>The name of the pier.</value>
        internal string PierName
        {
            get
            {
                if (_pierName == null)
                {
                    FillPierName();
                }

                return _pierName;
            }
        }

        /// <summary>
        /// The pier
        /// </summary>
        private Pier _pier;
        /// <summary>
        /// The pier assigned to the object.
        /// </summary>
        /// <value>The pier.</value>
        public Pier Pier => _pier ?? (_pier = _piers.FillItem(PierName));

        /// <summary>
        /// The spandrel name
        /// </summary>
        private string _spandrelName;
        /// <summary>
        /// Gets the name of the spandrel.
        /// </summary>
        /// <value>The name of the spandrel.</value>
        internal string SpandrelName
        {
            get
            {
                if (_spandrelName == null)
                {
                    FillSpandrelName();
                }

                return _spandrelName;
            }
        }

        /// <summary>
        /// The spandrel
        /// </summary>
        private Spandrel _spandrel;
        /// <summary>
        /// The spandrel assigned to the object.
        /// </summary>
        /// <value>The spandrel.</value>
        public Spandrel Spandrel => _spandrel ?? (_spandrel = _spandrels.FillItem(SpandrelName));
#endif

        /// <summary>
        /// The point names
        /// </summary>
        private List<string> _pointNames;
        /// <summary>
        /// Gets the point names.
        /// </summary>
        /// <value>The point names.</value>
        internal List<string> PointNames
        {
            get
            {
                if (_pointNames == null)
                {
                    FillPointNames();
                }

                return _pointNames;
            }
        }

        /// <summary>
        /// The object points
        /// </summary>
        private List<Point> _objectPoints;
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>The points.</value>
        public List<Point> Points
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
        private string _sectionName;
        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        internal string SectionName
        {
            get
            {
                if (_sectionName == null)
                {
                    FillSectionName();
                }

                return _sectionName;
            }
        }

        /// <summary>
        /// The cross section
        /// </summary>
        private T _crossSection;
        /// <summary>
        /// Gets or sets the cross section.
        /// </summary>
        /// <value>The cross section.</value>
        internal T CrossSection => _crossSection ?? (_crossSection = _crossSections.FillItem(SectionName));

        /// <summary>
        /// The material overwrite name
        /// </summary>
        private string _materialOverwriteName;
        /// <summary>
        /// Gets the name of the material overwrite.
        /// </summary>
        /// <value>The name of the material overwrite.</value>
        internal string MaterialOverwriteName
        {
            get
            {
                if (_materialOverwriteName == null)
                {
                    FillMaterialOverwriteName();
                }

                return _materialOverwriteName;
            }
        }

        /// <summary>
        /// The material overwrite
        /// </summary>
        private Material _materialOverwrite;

        /// <summary>
        /// The material overwrite assigned to the object.
        /// This overwrites the material used in the cross-section.
        /// </summary>
        /// <value>The material overwrite.</value>
        public Material MaterialOverwrite => _materialOverwrite ?? 
                                             (_materialOverwrite = _materials.FillItem(MaterialOverwriteName));

        /// <summary>
        /// The material used.
        /// </summary>
        /// <value>The material used.</value>
        public Material MaterialUsed => MaterialOverwrite ?? CrossSection.Material;

        /// <summary>
        /// The mass
        /// </summary>
        private double? _mass;
        /// <summary>
        /// Mass per unit length or pass per unit area assignment from objects, depending on whether the object is a line or area. [M/L] or [M/L^2]
        /// </summary>
        /// <value>The mass.</value>
        public double Mass
        {
            get
            {
                if (_mass == null)
                {
                    FillMass();
                }

                return _mass ?? 0;
            }
        }


        /// <summary>
        /// The temperature loads
        /// </summary>
        private List<LoadTemperature> _temperatureLoads;
        /// <summary>
        /// The temperature loads assigned to the object.
        /// </summary>
        /// <value>The temperature loads.</value>
        public List<LoadTemperature> TemperatureLoads
        {
            get
            {
                if (_temperatureLoads == null)
                {
                    FillLoadTemperature();
                }

                return _temperatureLoads;
            }
        }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObject2D{T}" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal StructureObject2D(ApiCSiApplication app,
            StructureComponentsProperties<T> componentsProperties,
            string name) : base(app, name)
        {
            _points = componentsProperties.Points;
            _crossSections = componentsProperties.CrossSections;
            _materials = componentsProperties.Materials;
            _piers = componentsProperties.Piers;
            _spandrels = componentsProperties.Spandrels;
        }


        #region Query
        // IObservablePoints
        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal abstract void FillPointNames();

        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract List<string> GetPointNames();
        #endregion

        #region Axes
        // IChangeableLocalAxes
        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetLocalAxes(AngleLocalAxes angleOffset);
        #endregion


        #region Modifiers
        // IDeletableModifiers
        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void DeleteModifiers();
        #endregion

        #region Cross-Section & Material Properties

        // IMass
        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillMass();

        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetMass(double value,
            bool replace);

        // IDeletableMass
        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void DeleteMass();

        // IObservableMaterialOverwrite
        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal abstract void FillMaterialOverwriteName();

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract string GetMaterialOverwriteName();

        // IChangeableMaterialOverwrite
        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddMaterialOverwrite(Material material);

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void RemoveMaterialOverwrite();

        // IObservableSection
        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal abstract void FillSectionName();

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract string GetSectionName();
        #endregion

        #region Design
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        // IPier
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        internal abstract void FillPierName();

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
        internal abstract void FillSpandrelName();

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
#endif
        #endregion

        #region Loads
        // LoadTemperature
        /// <summary>
        /// Returns the temperature load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillLoadTemperature();

        /// <summary>
        /// Assigns temperature loads to objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddLoadTemperature(LoadTemperature temperatureLoad);

        /// <summary>
        /// Assigns temperature loads to objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void ReplaceLoadTemperature(LoadTemperature temperatureLoad);

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The name of the load pattern associated with the load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void DeleteLoadTemperature(string loadPattern);
        #endregion


        #region API Functions
        /// <summary>
        /// Returns the names of the point objects that define the object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String[].</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected List<string> getPoints(IObservablePoints app)
        {
            _pointNames = new List<string>(app.GetPoints(Name));
            return PointNames;
        }

        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLocalAxes(IChangeableLocalAxes app, AngleLocalAxes angleOffset)
        {
            app.SetLocalAxes(Name, angleOffset);
            _angleOffset = angleOffset;
        }

        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void deleteModifiers(IDeletableModifiers app)
        {
            app.DeleteModifiers(Name);
        }

        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getMass(IMass app)
        {
            _mass = app.GetMass(Name);
        }

        /// <summary>
        /// Assigns mass per unit length to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setMass(IMass app,
            double value,
            bool replace)
        {
            app.SetMass(Name, value, replace);
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
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void deleteMass(IDeletableMass app)
        {
            app.DeleteMass(Name);
            _mass = null;
        }

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected string getMaterialOverwriteName(IObservableMaterialOverwrite app)
        {
            _materialOverwriteName = app.GetMaterialOverwrite(Name);
            return MaterialOverwriteName;
        }

        /// <summary>
        /// Sets the material overwrite assignment for objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="material">The material.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setMaterialOverwrite(IChangeableMaterialOverwrite app, Material material)
        {
            app.SetMaterialOverwrite(Name, HelperFunctions.GetNameOrNone(material));
            _materialOverwrite = material;
            _materialOverwriteName = material.Name;
        }

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected string getSectionName(IObservableSection app)
        {
            _sectionName = app.GetSection(Name);
            return _sectionName;
        }

        /// <summary>
        /// Assigns the section property to an object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="section">The the section property assigned to the object.</param>
        protected void setSection(IChangeableSection app, T section)
        {
            app.SetSection(Name, section.Name);
            _sectionName = section.Name;
            _crossSection = section;
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String.</returns>
        protected string getPierName(IPier app)
        {
            _pierName = app.GetPier(Name);
            return _pierName;
        }

        /// <summary>
        /// Adds the pier label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="pier">The pier assignment.</param>
        protected void addPier(IPier app, Pier pier)
        {
            app.SetPier(Name, pier.Name);
            _pierName = pier.Name;
            _pier = pier;
        }

        /// <summary>
        /// Removes the pier label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void removePier(IPier app)
        {
            app.SetPier(Name, Constants.NONE);
            _pierName = string.Empty;
            _pier = null;
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String.</returns>
        protected string getSpandrelName(ISpandrel app)
        {
            _spandrelName = app.GetSpandrel(Name);
            return _spandrelName;
        }

        /// <summary>
        /// Adds the pier label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="spandrel">The spandrel assignment.</param>
        protected void addSpandrel(ISpandrel app, Spandrel spandrel)
        {
            app.SetSpandrel(Name, spandrel.Name);
            _spandrelName = spandrel.Name;
            _spandrel = spandrel;
        }

        /// <summary>
        /// Removes the spandrel label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void removeSpandrel(ISpandrel app)
        {
            app.SetSpandrel(Name, Constants.NONE);
            _spandrelName = string.Empty;
            _spandrel = null;
        }
#endif
        /// <summary>
        /// Returns the temperature load assignments to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getLoadTemperature(ILoadTemperature app)
        {
            app.GetLoadTemperature(Name,
                out var names,
                out var loadPatterns,
                out var temperatureLoadTypes,
                out var temperatureLoadValues,
                out var jointPatternNames);

            _temperatureLoads = new List<LoadTemperature>();
            for (int i = 0; i < names.Length; i++)
            {
                LoadTemperature temperatureLoad = new LoadTemperature
                {
                    LoadPattern = loadPatterns[i],
                    TemperatureLoadType = temperatureLoadTypes[i],
                    Value = temperatureLoadValues[i],
                    JointPatternName = jointPatternNames[i]
                };

                _temperatureLoads.Add(temperatureLoad);
            }
        }

        /// <summary>
        /// Assigns temperature loads to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoadTemperature(ILoadTemperature app,
            LoadTemperature temperatureLoad,
            bool replace)
        {
            app.SetLoadTemperature(Name,
                temperatureLoad.LoadPattern,
                temperatureLoad.TemperatureLoadType,
                temperatureLoad.Value,
                temperatureLoad.JointPatternName,
                replace);

            addOrReplace(replace, temperatureLoad, _temperatureLoads);
        }

        /// <summary>
        /// Deletes the load temperature.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadPattern">The load pattern.</param>
        protected void deleteLoadTemperature(ILoadTemperature app, string loadPattern)
        {
            app?.DeleteLoadTemperature(Name, loadPattern);
        }
        #endregion

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
    }
}
