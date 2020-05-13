// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-27-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="Spandrel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    /// <summary>
    /// Class Spandrel.
    /// </summary>
    /// <seealso cref="PierSpandrelBase" />
    public class Spandrel : PierSpandrelBase
    {
        /// <summary>
        /// Gets the spandrel label.
        /// </summary>
        /// <value>The spandrel label.</value>
        protected static SpandrelLabel _spandrelLabel => Registry.ProgramDefinitions.Abstractions.SpandrelLabel;


        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public ShearWallResults Results { get; protected set; }

        /// <summary>
        /// True: Spandrel Label spans multiple story levels.
        /// </summary>
        /// <value><c>true</c> if this instance is multi story; otherwise, <c>false</c>.</value>
        public bool IsMultiStory { get; protected set; }

        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        public static Spandrel Factory(string uniqueName)
        {
            return Factory(uniqueName, _spandrelLabel, Registry.Spandrels);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spandrel" /> class.
        /// </summary>
        public Spandrel() : base(string.Empty) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Spandrel" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Spandrel(string name) : base(name) { }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillSpandrel();
            Results = new ShearWallResults(Name, isPier: false);
        }

        #region Query
        /// <summary>
        /// Retrieves the names of all defined spandrel label property.
        /// </summary>
        /// <param name="names">The spandrel label property names retrieved by the program.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        public static void GetNameList(out string[] names,
            out bool[] isMultiStory)
        {
            _spandrelLabel.GetNameList(out names, out isMultiStory);
        }

        /// <summary>
        /// Retrieves the names of all defined label propertie.
        /// </summary>
        /// <returns>System.String[].</returns>
        public static string[] GetNameList()
        {
            _spandrelLabel.GetNameList(out var names, out var isMultiStory);
            return names;
        }
        #endregion

        #region CRUD

        /// <summary>
        /// True: The Spandrel Label exists.
        /// </summary>
        public void FillSpandrel()
        {
            Exists = _spandrelLabel.GetSpandrel(Name, out var isMultiStory);
            IsMultiStory = isMultiStory;
        }

        /// <summary>
        /// Adds a new Spandrel Label.
        /// </summary>
        /// <param name="isMultiStory">if set to <c>true</c> [is multi story].</param>
        public void AddSpandrel(bool isMultiStory)
        { //TODO: Finish AddSpandrel
            _spandrelLabel.SetSpandrel(Name, IsMultiStory);
            addPierSpandrel();
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <inheritdoc />
        public override void ChangeName(string newName)
        {
            _spandrelLabel.ChangeName(Name, newName);
            changeName(newName);
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <inheritdoc />
        public override void Delete()
        {
            _spandrelLabel.Delete(Name);
        }
        #endregion
    }
}
