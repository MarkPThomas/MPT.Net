// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Area.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Class Area.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.StructureObject2D" />
    public class Area : StructureObject2D
    {
        /// <summary>
        /// Gets the area object.
        /// </summary>
        /// <value>The area object.</value>
        protected static AreaObject _areaObject => Registry.ObjectModeler?.AreaObject;
        
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        // public ShearWallDesigner ShearWallDesigner => Registry.ShearWallDesigner;
        // public SlabDesigner SlabDesigner => Registry.SlabDesigner;
#endif

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public AreaResults Results { get; protected set; }

        /// <summary>
        /// True: Specified area object is an opening.
        /// </summary>
        /// <value><c>true</c> if this instance is opening; otherwise, <c>false</c>.</value>
        public bool IsOpening { get; protected set; }

        /// <summary>
        /// Gets or sets the design orientation.
        /// </summary>
        /// <value>The design orientation.</value>
        public eAreaDesignOrientation DesignOrientation { get; protected set; }
        /// <summary>
        /// Gets or sets the nodes.
        /// </summary>
        /// <value>The nodes.</value>
        public List<Node> Nodes { get; protected set; }

        /// <summary>
        /// Gets or sets the uniform loads.
        /// </summary>
        /// <value>The uniform loads.</value>
        public List<AreaLoadUniform> UniformLoads { get; protected set; } = new List<AreaLoadUniform>();
        /// <summary>
        /// Gets or sets the uniform to frame loads.
        /// </summary>
        /// <value>The uniform to frame loads.</value>
        public List<AreaLoadUniformToFrame> UniformToFrameLoads { get; protected set; } = new List<AreaLoadUniformToFrame>();
        /// <summary>
        /// Gets or sets the wind pressure loads.
        /// </summary>
        /// <value>The wind pressure loads.</value>
        public List<AreaLoadWindPressure> WindPressureLoads { get; protected set; } = new List<AreaLoadWindPressure>();

        /// <summary>
        /// The cross-section associated with the area.
        /// </summary>
        /// <value>The section.</value>
        public AreaSection Section { get; protected set; }



        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List&lt;Area&gt;.</returns>
        public static List<Area> GetAll()
        {
            List<Area> objects = new List<Area>();
            if (_areaObject == null) return objects;
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _areaObject.GetAllAreas(
                out var areaNames,
                out var designOrientations,
                out var numberOfBoundaryPts,
                out var pointDelimiters,
                out var pointNames,
                out var coordinates);
            for (int i = 0; i < pointNames.Length; i++)
            {
                // TODO: Debug here for how point names are delivered, usage for delimiters?
                List<string> pointNamesList = new List<string> {pointNames[i]};
                Area node = Factory(areaNames[i], designOrientations[i], pointNamesList);
                objects.Add(node);
            }
#else
            List<string> names = GetNameList();
            foreach (var name in names)
            {
                Area node = Factory(name);
                objects.Add(node);
            }
#endif
            return objects;
        }

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="designOrientation">The design orientation.</param>
        /// <param name="pointNames">The point names.</param>
        /// <returns>Area.</returns>
        public static Area Factory(string uniqueName,
            eAreaDesignOrientation designOrientation,
            List<string> pointNames)
        {
            if (Registry.Areas.Keys.Contains(uniqueName)) return Registry.Areas[uniqueName];

            List<Node> nodes = new List<Node>();
            foreach (var pointName in pointNames)
            {
                Node node = Node.Factory(pointName);
                nodes.Add(node);
            }
            Area area = new Area(uniqueName, nodes, designOrientation);

            if (_areaObject != null)
            {
                area.FillData();
            }

            Registry.Areas.Add(uniqueName, area);
            return area;
        }

        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Area.</returns>
        public static Area Factory(string uniqueName)
        {
            return Factory(uniqueName, _areaObject, Registry.Areas);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the names and labels of all defined objects.
        /// </summary>
        /// <returns>List&lt;UniqueLabelNamePair&gt;.</returns>
        public static List<UniqueLabelNamePair> GetLabelNameList()
        {
            return getLabelNameList(_areaObject);
        }
#endif

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<string> GetNameList()
        {
            return getNameList(_areaObject);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        public Area() : base(string.Empty)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Area(string name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="nodes">The nodes.</param>
        /// <param name="designOrientation">The design orientation.</param>
        public Area(string name, List<Node> nodes, eAreaDesignOrientation designOrientation = eAreaDesignOrientation.Other) : base(name)
        {
            Nodes = nodes;
            DesignOrientation = designOrientation;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            Results = new AreaResults(Name);
        }

        #region Query
        /// <summary>
        /// Returns the point objects that define an area object.
        /// </summary>
        /// <returns>List&lt;Node&gt;.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public List<Node> GetPoints()
        {
            if (_areaObject == null) return new List<Node>();
            
            string[] points = _areaObject.GetPoints(Name);

            return points.Select(Node.Factory).ToList();
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the unique name of an object, given the label and story level .
        /// </summary>
        public override void FillNameFromLabel()
        {
            getNameFromLabel(_areaObject);
        }

        /// <summary>
        /// Retrieves the label and story for a unique object name.
        /// </summary>
        public override void FillLabelFromName()
        {
            getLabelFromName(_areaObject);
        }

        /// <summary>
        /// Returns the names of all defined object properties for a given story.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameListOnStory()
        {
            return getNameListOnStory(_areaObject);
        }

        /// <summary>
        /// Retrieves whether the specified area object is an opening.
        /// </summary>
        public void GetOpening()
        {
            if (_areaObject == null) return;
            _areaObject.GetOpening(Name, out var isOpening);
            IsOpening = isOpening;
        }

        /// <summary>
        /// Designates an area object(s) as an opening.
        /// </summary>
        public void SetOpening()
        {
            _areaObject?.SetOpening(Name, IsOpening);
        }
#endif

        /// <summary>
        /// Retrieves the GUID for the specified object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillGUID()
        {
            getGUID(_areaObject);
        }

        /// <summary>
        /// Sets the GUID for the specified object.
        /// If the GUID is passed in as a blank string, the program automatically creates a GUID for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetGUID()
        {
            setGUID(_areaObject);
        }


        /// <summary>
        /// Retrieves the name of the element (analysis model) associated with a specified object in the object-based model.
        /// An error occurs if the analysis model does not exist.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillElement()
        {
            getElement(_areaObject);
        }

        /// <summary>
        /// Returns the 3x3 direction cosines to transform local coordinates to global coordinates by the equation [directionCosines]*[localCoordinates] = [globalCoordinates].
        /// Direction cosines returned are ordered by row, and then by column.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillTransformationMatrix()
        {
            getTransformationMatrix(_areaObject);
        }

        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillPoints()
        {
            string[] points = getPoints(_areaObject);
            foreach (var point in points)
            {
                Nodes.Add(Node.Factory(points[0]));
            }
        }
        #endregion

        #region Axes
        /// <summary>
        /// Retrieves the local axis angle assignment for the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLocalAxes()
        {
            getLocalAxes(_areaObject);
        }

        /// <summary>
        /// Returns the local axis angle assignment for the object.
        /// </summary>
        /// <param name="angleOffset">This is the angle 'a' that the local 2 and 3 axes are rotated about the positive local 1 axis, from the default orientation.
        /// The rotation for a positive angle appears counter clockwise when the local +1 axis is pointing toward you. [deg]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetLocalAxes(AngleLocalAxes angleOffset)
        {
            setLocalAxes(_areaObject, angleOffset);
            AngleOffset = angleOffset;
        }
        #endregion

        #region Creation
        /// <summary>
        /// Changes the name of an existing object.
        /// </summary>
        /// <param name="newName">The new name for the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_areaObject, newName);
        }


        /// <summary>
        /// The function deletes a specified frame object.
        /// It returns an error if the specified object cannot be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Delete()
        {
            delete(_areaObject);
        }
        #endregion

        #region Selection
        /// <summary>
        /// Retrieves the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void GetSelected()
        {
            getSelected(_areaObject);
        }

        /// <summary>
        /// Selects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Select()
        {
            base.Select();
            setSelected();
        }

        /// <summary>
        /// Deselects the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Deselect()
        {
            base.Deselect();
            setSelected();
        }

        /// <summary>
        /// Sets the selected status for an object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setSelected()
        {
            setSelected(_areaObject);
        }

#if BUILD_SAP2000v20
        /// <summary>
        /// Returns the names of the groups to which a specified object is assigned.
        /// </summary>
        public string[] GetGroupsAssigned()
        {
            return getGroupsAssigned(_areaObject);
        }
#endif
        #endregion

        #region Modifiers

        /// <summary>
        /// Deletes a modifier assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteModifiers()
        {
            deleteModifiers(_areaObject);
        }
        #endregion

        #region Cross-Section & Material Properties
        /// <summary>
        /// Returns the mass per unit length assignment from objects. [M/L]
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillMass()
        {
            getMass(_areaObject);
        }

        /// <summary>
        /// Assigns mass per unit area to objects.
        /// </summary>
        /// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        /// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        /// False: The specified mass is added to any existing mass already assigned to the object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetMass(double value, bool replace)
        {
            setMass(_areaObject, value, replace);
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteMass()
        {
            deleteMass(_areaObject);
        }

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// These overwrite the material assigned to the cross section used in the object.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillMaterialOverwrite()
        {
            getMaterialOverwrite(_areaObject);
        }


        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">An existing material property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void AddMaterialOverwrite(Material material)
        {
            setMaterialOverwrite(_areaObject, material);
        }

        /// <summary>
        /// Removes the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void RemoveMaterialOverwrite()
        {
            setMaterialOverwrite(_areaObject, null);
        }

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillSection()
        {
            string sectionName = getSection(_areaObject);
            Section = AreaSection.Factory(sectionName);
        }

        // IChangeableSection
        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="section">The section.</param>
        public void SetSection(AreaSection section)
        {
            if (section == null) return;
            Section = section;
            SetSection();
        }

        /// <summary>
        /// Assigns the section property to an object.
        /// </summary>
        public override void SetSection()
        {
            setSection(_areaObject, Section?.Name);
        }
        #endregion

        #region Area Properties
        #endregion

        #region Support & Connections
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the named spring property assignment for an object.
        /// </summary>
        public override void FillSpringAssignment()
        {
            getSpringAssignment(_areaObject);
        }

        /// <summary>
        /// Assigns an existing named spring property to objects.
        /// </summary>
        public override void SetSpringAssignment()
        {
            setSpringAssignment(_areaObject);
        }
#endif

        /// <summary>
        /// Deletes all spring assignments for the specified objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void DeleteSpring()
        {
            _areaObject?.DeleteSpring(Name);
            base.DeleteSpring();
        }
        #endregion

        #region Design
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        public override void FillPier()
        {
            getPier(_areaObject);
        }

        /// <summary>
        /// Adds the pier label assignment to the object.
        /// Any existing pier label is replaced.
        /// </summary>
        /// <param name="pier">The pier assignment.</param>
        public override void AddToPier(Pier pier)
        {
            addPier(_areaObject, pier);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromPier()
        {
            removePier(_areaObject);
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        public override void FillSpandrel()
        {
            getSpandrel(_areaObject);
        }

        /// <summary>
        /// Adds the spandrel label assignment to the object.
        /// Any existing spandrel label is replaced.
        /// </summary>
        /// <param name="spandrel">The spandrel assignment.</param>
        public override void AddToSpandrel(Spandrel spandrel)
        {
            addSpandrel(_areaObject, spandrel);
        }

        /// <summary>
        /// Removes the pier label from the object.
        /// </summary>
        public override void RemoveFromSpandrel()
        {
            removeSpandrel(_areaObject);
        }
#endif
        #endregion

        #region Loading
        // LoadTemperature
        /// <summary>
        /// Returns the temperature load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void FillLoadTemperature()
        {
            getLoadTemperature(_areaObject);
        }

        /// <summary>
        /// Assigns temperature loads to frame objects.
        /// </summary>
        /// <param name="temperatureLoad">The temperature load.</param>
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void SetLoadTemperature(LoadTemperature temperatureLoad, bool replace)
        {
            setLoadTemperature(_areaObject, temperatureLoad, replace);
        }

        /// <summary>
        /// Deletes the temperature load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadTemperature(string loadPattern)
        {
            _areaObject?.DeleteLoadTemperature(Name, loadPattern);
            deleteLoad(loadPattern, TemperatureLoads);
        }


        // LoadUniform
        /// <summary>
        /// Returns the uniform load assignments to objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadUniform()
        {
            if (_areaObject == null) return;
            _areaObject.GetLoadUniform(Name,
                out var names,
                out var loadPatterns,
                out var coordinateSystems,
                out var directionApplied,
                out var uniformLoadValues);
            for (int i = 0; i < names.Length; i++)
            {
                AreaLoadUniform areaLoadUniform = new AreaLoadUniform
                {
                    LoadPattern = loadPatterns[i],
                    Direction = directionApplied[i],
                    Value = uniformLoadValues[i],
                    CoordinateSystem = coordinateSystems[i]
                };

                UniformLoads.Add(areaLoadUniform);
            }
        }

        /// <summary>
        /// Assigns uniform loads to objects.
        /// </summary>
        /// <param name="loadUniform">The load uniform.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadUniform(AreaLoadUniform loadUniform,
            bool replace = true)
        {
            _areaObject?.SetLoadUniform(Name,
                loadUniform.LoadPattern,
                loadUniform.Direction,
                loadUniform.Value,
                loadUniform.CoordinateSystem,
                replace);
        }

        /// <summary>
        /// Deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadUniform(string loadPattern)
        {
            if (_areaObject == null) return;
            _areaObject.DeleteLoadUniform(Name, loadPattern);
            deleteLoad(loadPattern, UniformLoads);
        }


        // LoadUniformToFrame
        /// <summary>
        /// Returns the wind pressure load assignments to area objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadUniformToFrame()
        {
            if (_areaObject == null) return;
            _areaObject.GetLoadUniformToFrame(Name,
                out var loadPatterns,
                out var names,
                out var values,
                out var directions,
                out var distributionTypes,
                out var coordinateSystems);
            for (int i = 0; i < names.Length; i++)
            {
                AreaLoadUniformToFrame areaLoadUniformToFrame = new AreaLoadUniformToFrame
                {
                    LoadPattern = loadPatterns[i],
                    Direction = directions[i],
                    DistributionType = distributionTypes[i],
                    Value = values[i],
                    CoordinateSystem = coordinateSystems[i]
                };

                UniformToFrameLoads.Add(areaLoadUniformToFrame);
            }
        }

        /// <summary>
        /// Assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="uniformLoad">The uniform load.</param>
        /// <param name="replace">if set to <c>true</c> [replace].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadUniformToFrame(AreaLoadUniformToFrame uniformLoad,
            bool replace = true)
        {
            _areaObject?.SetLoadUniformToFrame(Name,
                uniformLoad.LoadPattern,
                uniformLoad.Value,
                uniformLoad.Direction,
                uniformLoad.DistributionType,
                replace: replace,
                coordinateSystem: uniformLoad.CoordinateSystem);
        }

        /// <summary>
        /// Deletes the uniform load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadUniformToFrame(string loadPattern)
        {
            if (_areaObject == null) return;
            _areaObject.DeleteLoadUniformToFrame(Name, loadPattern);
            deleteLoad(loadPattern, UniformToFrameLoads);
        }


        // LoadWindPressure
        /// <summary>
        /// Returns the wind pressure load assignments to area objects.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadWindPressure()
        {
            if (_areaObject == null) return;
            _areaObject.GetLoadWindPressure(Name,
                out var names,
                out var loadPatterns,
                out var windPressureTypes,
                out var pressureCoefficients);
            for (int i = 0; i < names.Length; i++)
            {
                AreaLoadWindPressure areaLoadWindPressure = new AreaLoadWindPressure
                {
                    LoadPattern = loadPatterns[i],
                    WindPressureType = windPressureTypes[i],
                    PressureCoefficient = pressureCoefficients[i]
                };

                WindPressureLoads.Add(areaLoadWindPressure);
            }
        }

        /// <summary>
        /// Assigns wind pressure loads to area objects.
        /// </summary>
        /// <param name="windPressure">The wind pressure.</param>
        /// <exception cref="CSiException">API_DEFALT_ERROR_CODE</exception>
        public void SetLoadWindPressure(AreaLoadWindPressure windPressure)
        {
            _areaObject?.SetLoadWindPressure(Name,
                windPressure.LoadPattern,
                windPressure.WindPressureType,
                windPressure.PressureCoefficient);
        }

        /// <summary>
        /// Deletes the wind pressure load assignments to the specified objects for the specified load pattern.
        /// </summary>
        /// <param name="loadPattern">The load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void DeleteLoadWindPressure(string loadPattern)
        {
            if (_areaObject == null) return;
            _areaObject.DeleteLoadWindPressure(Name, loadPattern);
            deleteLoad(loadPattern, WindPressureLoads);
        }
        #endregion
    }
}
