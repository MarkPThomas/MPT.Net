// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="Pier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiPier = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.PierLabel;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Pier.
    /// </summary>
    /// <seealso cref="PierSpandrelBase" />
    public class Pier : PierSpandrelBase
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The piers.</value>
        private ApiPier _pierLabel => getApiPier(_apiApp);

        /// <summary>
        /// The results
        /// </summary>
        private readonly PierResults _results;

        /// <summary>
        /// The story drifts
        /// </summary>
        private List<Tuple<PierSpandrelResultsIdentifier, Forces>> _forces;
        /// <summary>
        /// Gets or sets the story drifts.
        /// </summary>
        /// <value>The story drifts.</value>
        public List<Tuple<PierSpandrelResultsIdentifier, Forces>> Forces
        {
            get
            {
                if (_forces == null)
                {
                    FillPierForces();
                }

                return _forces;
            }
        }

        /// <summary>
        /// Gets or sets the piers by story.
        /// </summary>
        /// <value>The piers by story.</value>
        public List<PierProperties> PiersByStory { get; protected set; } = new List<PierProperties>();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="pierResults">The pier results.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Pier Factory(ApiCSiApplication app,
            PierResults pierResults, 
            string uniqueName)
        {
            Pier item = new Pier(app, pierResults, uniqueName);
            item.FillData();
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pier" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="pierResults">The pier results.</param>
        /// <param name="name">The name.</param>
        protected Pier(ApiCSiApplication app,
            PierResults pierResults,
            string name) : base(app, name)
        {
            _results = pierResults;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillPier();
#if BUILD_ETABS2016 || BUILD_ETABS2017
            FillSectionProperties();
#endif
        }
        #endregion

        #region Query
        /// <summary>
        /// Retrieves the names of all defined label properties.
        /// </summary>
        /// <param name="pier">The pier.</param>
        /// <returns>System.String[].</returns>
        public static List<string> GetNameList(ApiPier pier)
        {
            return (pier == null) ? new List<string>() : new List<string>(pier.GetNameList());
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// True: The Pier Label exists.
        /// </summary>
        public void FillPier()
        {
            Exists = _pierLabel.GetPier(Name);
        }

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the section properties for a specified pier.
        /// </summary>
        public void FillSectionProperties()
        {
            _pierLabel.GetSectionProperties(Name,
                out var storyNames,
                out var numberOfAreaObjects,
                out var numberOfLineObjects,
                out var axisAngles,
                out var widthBottom,
                out var thicknessBottom,
                out var widthTop,
                out var thicknessTop,
                out var materialPropertyNames,
                out var centerOfGravityBottomX,
                out var centerOfGravityBottomY,
                out var centerOfGravityBottomZ,
                out var centerOfGravityTopX,
                out var centerOfGravityTopY,
                out var centerOfGravityTopZ);

            PiersByStory = new List<PierProperties>();
            for (int i = 0; i < storyNames.Length; i++)
            {
                PierProperties pierProperties = new PierProperties
                {
                    StoryName = storyNames[i],
                    NumberOfAreaObjects = numberOfAreaObjects[i],
                    NumberOfLineObjects = numberOfLineObjects[i],
                    AxisAngles = axisAngles[i],
                    WidthBottom = widthBottom[i],
                    ThicknessBottom = thicknessBottom[i],
                    WidthTop = widthTop[i],
                    ThicknessTop = thicknessTop[i],
                    MaterialPropertyName = materialPropertyNames[i],
                    CenterOfGravityBottomX = centerOfGravityBottomX[i],
                    CenterOfGravityBottomY = centerOfGravityBottomY[i],
                    CenterOfGravityBottomZ = centerOfGravityBottomZ[i],
                    CenterOfGravityTopX = centerOfGravityTopX[i],
                    CenterOfGravityTopY = centerOfGravityTopY[i],
                    CenterOfGravityTopZ = centerOfGravityTopZ[i]
                };
                PiersByStory.Add(pierProperties);
            }
        }
#endif


        /// <summary>
        /// Fills the pier force.
        /// </summary>
        public void FillPierForces()
        {
            _forces = new List<Tuple<PierSpandrelResultsIdentifier, Forces>>();
            foreach (var pierResult in _results.Forces)
            {
                if (pierResult.Item1.PierSpandrelName == Name)
                {
                    _forces.Add(pierResult);
                }
            }
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Adds a new Pier Label.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="pierResults">The pier results.</param>
        /// <param name="name">The name.</param>
        /// <returns>Pier.</returns>
        internal static Pier AddPier(ApiCSiApplication app,
            PierResults pierResults, 
            string name)
        {
            ApiPier pier = getApiPier(app);
            List<string> existingItems = GetNameList(pier);
            if (existingItems.Contains(name)) return null;

            pier.SetPier(name);
            return Factory(app, pierResults, name);
        }


        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        internal override void Delete()
        {
            _pierLabel.Delete(Name);
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _pierLabel.ChangeName(Name, newName);
            changeName(newName);
        }
        #endregion
    }
}
#endif