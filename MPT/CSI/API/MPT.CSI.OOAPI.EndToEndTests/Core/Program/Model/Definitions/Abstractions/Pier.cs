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
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Pier.
    /// </summary>
    /// <seealso cref="PierSpandrelBase" />
    public class Pier : PierSpandrelBase
    {
        /// <summary>
        /// Gets the pier label.
        /// </summary>
        /// <value>The pier label.</value>
        protected static PierLabel _pierLabel => Registry.ProgramDefinitions.Abstractions.PierLabel;


        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public ShearWallResults Results { get; protected set; }

        /// <summary>
        /// Gets or sets the piers by story.
        /// </summary>
        /// <value>The piers by story.</value>
        public List<PierProperties> PiersByStory { get; protected set; } = new List<PierProperties>();

        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        public static Pier Factory(string uniqueName)
        {
            return Factory(uniqueName, _pierLabel, Registry.Piers);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Pier" /> class.
        /// </summary>
        public Pier() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pier" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Pier(string name) : base(name) { }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillPier();
            FillSectionProperties();
            Results = new ShearWallResults(Name);
        }

        #region Query
        /// <summary>
        /// Retrieves the names of all defined label properties.
        /// </summary>
        /// <returns>System.String[].</returns>
        public static string[] GetNameList()
        {
            return _pierLabel.GetNameList();
        }
        #endregion

        #region CRUD
        /// <summary>
        /// True: The Pier Label exists.
        /// </summary>
        public void FillPier()
        {
            Exists = _pierLabel.GetPier(Name);
        }

        /// <summary>
        /// Adds a new Pier Label.
        /// </summary>
        public void AddPier()
        {   //TODO: Finish AddPier
            _pierLabel.SetPier(Name);
            addPierSpandrel();
        }

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the section properties for a specified pier.
        /// </summary>
        public void FillSectionProperties()
        {
            // TODO: Add properties object to API project?
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
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _pierLabel.ChangeName(Name, newName);
            changeName(newName);
        }


        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        public override void Delete()
        {
            _pierLabel.Delete(Name);
        }
        #endregion
    }
}