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
using MPT.CSI.OOAPI.Core.Program.Model.Assignments.AppliedLoads;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    /// <summary>
    /// Represents a 2D structure such as a frame or area.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.StructureObject" />
    public abstract class StructureObject2D : StructureObject
    {
        /// <summary>
        /// The temperature loads assigned to the object.
        /// </summary>
        /// <value>The temperature loads.</value>
        public List<LoadTemperature> TemperatureLoads { get; protected set; } = new List<LoadTemperature>();

        /// <summary>
        /// Mass per unit length or pass per unit area assignment from objects, depending on whether the object is a line or area. [M/L] or [M/L^2]
        /// </summary>
        /// <value>The mass.</value>
        public double Mass { get; protected set; }

        /// <summary>
        /// The material overwrite assigned to the object.
        /// This overwrites the material used in the cross-section.
        /// </summary>
        /// <value>The material overwrite.</value>
        public Material MaterialOverwrite { get; protected set; }

        /// <summary>
        /// The pier assigned to the object.
        /// </summary>
        /// <value>The pier.</value>
        public Pier Pier { get; protected set; }

        /// <summary>
        /// The spandrel assigned to the object.
        /// </summary>
        /// <value>The spandrel.</value>
        public Spandrel Spandrel { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObject2D" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected StructureObject2D(string name) : base(name)
        { }

        #region Query
        // IObservablePoints
        /// <summary>
        /// Returns the names of the point objects that define an object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillPoints();
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
        public abstract void FillMaterialOverwrite();

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
        public abstract void FillSection();

        // IChangeableSection
        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        public abstract void SetSection();
        #endregion

        #region Design
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        // IPier
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        public abstract void FillPier();

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
        public abstract void FillSpandrel();

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
        /// <param name="replace">True: All previous loads, if any, assigned to the specified object(s), in the specified load pattern, are deleted before making the new assignment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetLoadTemperature(LoadTemperature temperatureLoad, bool replace);
        #endregion


        #region API Functions
        /// <summary>
        /// Returns the names of the point objects that define the object.
        /// The point names are listed in the positive order around the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String[].</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected string[] getPoints(IObservablePoints app)
        {
            return app.GetPoints(Name);
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
            Mass = app.GetMass(Name);
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
        }

        /// <summary>
        /// Deletes all mass assignments for the specified objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void deleteMass(IDeletableMass app)
        {
            app.DeleteMass(Name);
        }

        /// <summary>
        /// Returns the material overwrite assigned, if any.
        /// The material property name is indicated as None if there is no material overwrite assignment.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getMaterialOverwrite(IObservableMaterialOverwrite app)
        {
            string name = app.GetMaterialOverwrite(Name);
            MaterialOverwrite = HelperFunctions.Fill(name, Material.Factory);
            MaterialOverwrite = (name == Constants.None) ? null : Material.Factory(name);
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
            MaterialOverwrite = material;
        }

        /// <summary>
        /// Returns the section property name assigned.
        /// This item is None if there is no section property assigned to the element/object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected string getSection(IObservableSection app)
        {
            return app.GetSection(Name);
        }

        /// <summary>
        /// Assigns the section property to a frame object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="propertyName">The name of the section property assigned to the frame object.</param>
        protected void setSection(IChangeableSection app, string propertyName)
        {
            app.SetSection(Name, propertyName);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the pier label assignments of an object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getPier(IPier app)
        {
            Pier = Pier.Factory(app.GetPier(Name));
        }

        /// <summary>
        /// Adds the pier label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="pier">The pier assignment.</param>
        protected void addPier(IPier app, Pier pier)
        {
            app.SetPier(Name, pier.Name);
            Pier = pier;
        }

        /// <summary>
        /// Removes the pier label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void removePier(IPier app)
        {
            app.SetPier(Name, Constants.None);
            Pier = null;
        }

        /// <summary>
        /// Retrieves the spandrel label assignments of an object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getSpandrel(ISpandrel app)
        {
            Spandrel = Spandrel.Factory(app.GetSpandrel(Name));
        }

        /// <summary>
        /// Adds the pier label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="spandrel">The spandrel assignment.</param>
        protected void addSpandrel(ISpandrel app, Spandrel spandrel)
        {
            app.SetSpandrel(Name, spandrel.Name);
            Spandrel = spandrel;
        }

        /// <summary>
        /// Removes the spandrel label assignment of the object.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void removeSpandrel(ISpandrel app)
        {
            app.SetSpandrel(Name, Constants.None);
            Spandrel = null;
        }
#endif
        /// <summary>
        /// Returns the temperature load assignments to objects.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getLoadTemperature(ILoadTemperature app)
        {
            // TODO: Add to API project?
            app.GetLoadTemperature(Name,
                out var names,
                out var loadPatterns,
                out var temperatureLoadTypes,
                out var temperatureLoadValues,
                out var jointPatternNames);
            for (int i = 0; i < names.Length; i++)
            {
                LoadTemperature temperatureLoad = new LoadTemperature
                {
                    LoadPattern = loadPatterns[i],
                    TemperatureLoadType = temperatureLoadTypes[i],
                    Value = temperatureLoadValues[i],
                    JointPatternName = jointPatternNames[i]
                };

                TemperatureLoads.Add(temperatureLoad);
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
            // TODO: Add to API project?
            app.SetLoadTemperature(Name,
                temperatureLoad.LoadPattern,
                temperatureLoad.TemperatureLoadType,
                temperatureLoad.Value,
                temperatureLoad.JointPatternName,
                replace);
        }
        #endregion
        
    }
}
