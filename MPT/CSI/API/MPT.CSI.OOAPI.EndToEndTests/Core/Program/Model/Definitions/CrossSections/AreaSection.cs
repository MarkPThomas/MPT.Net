using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using AreaSectionApi = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections
{
    public class AreaSection : CrossSection
    {
        protected static AreaSectionApi _areaSection => Registry.Application.Model.Definitions.Properties.AreaSection;

        /// <summary>
        /// Gets or sets the section modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public AreaModifier Modifiers { get; protected set; }


        /// <summary>
        /// Gets all defined area sections.
        /// </summary>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public static List<AreaSection> GetAll()
        {
            List<AreaSection> objects = new List<AreaSection>();
            List<string> names = GetNameList();
            foreach (var name in names)
            {
                AreaSection areaSection = Factory(name);

                objects.Add(areaSection);
            }

            return objects;
        }

        
        public static AreaSection Factory(string uniqueName)
        {
            if (Registry.AreaSections.Keys.Contains(uniqueName)) return Registry.AreaSections[uniqueName];
            
            AreaSection areaSection = new AreaSection(uniqueName);

            if (_areaSection != null)
            {

            }
            Registry.AreaSections.Add(uniqueName, areaSection);
            return areaSection;
        }


        /// <summary>
        /// Returns the names of all defined frame properties.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public static List<string> GetNameList()
        {
            return new List<string>(getNameList(_areaSection));
        }

        public AreaSection(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {

        }

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_areaSection, newName);
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Delete()
        {
            delete(_areaSection);
        }
        #endregion

        #region Methods: Section

        #endregion

        #region Methods: Imported Section

        #endregion

        #region Methods: Modifiers
        /// <summary>
        /// Returns the modifier assignment for area properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetModifiers()
        {
            Modifiers = _areaSection.GetModifiers(Name);
        }

        /// <summary>
        /// This function defines the modifier assignment for area properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers()
        {
            _areaSection.SetModifiers(Name, Modifiers);
        }
        #endregion

        #region Methods: Get/Set

        #endregion
    }
}
