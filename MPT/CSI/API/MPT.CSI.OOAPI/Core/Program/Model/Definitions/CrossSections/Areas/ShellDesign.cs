// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ShellDesign.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class ShellDesign.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.ApiPropertyObject{ShellDesignProperties}" />
    public class ShellDesign : ApiPropertyObject<ShellDesignProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The area section.</value>
        protected ApiAreaSection _apiAreaSection => getApiAreaSection(_apiApp);
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static ShellDesign Factory(ApiCSiApplication app, string uniqueName, ShellDesignProperties properties = null)
        {
            ShellDesign areaSection = new ShellDesign(app, uniqueName) { _properties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellDesign" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected ShellDesign(ApiCSiApplication app, string name) : base(app, name)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Fill the layer properties.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetShellDesign(Name,
                out var nameMaterial,
                out var rebarLayout,
                out var designCoverTopDirection1,
                out var designCoverTopDirection2,
                out var designCoverBottomDirection1,
                out var designCoverBottomDirection2);

            _properties = new ShellDesignProperties
            {
                RebarLayout = rebarLayout,
                CoverTopDirection1 = designCoverTopDirection1,
                CoverTopDirection2 = designCoverTopDirection2,
                CoverBottomDirection1 = designCoverBottomDirection1,
                CoverBottomDirection2 = designCoverBottomDirection2,
                MaterialName = nameMaterial
            };
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(ShellDesignProperties properties)
        {
            setApi(_apiAreaSection, Name, properties);
            set(properties);
        }

        /// <summary>
        /// Sets the API.
        /// </summary>
        /// <param name="apiAreaSection">The API area section.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        protected static void setApi(ApiAreaSection apiAreaSection, string name, ShellDesignProperties properties)
        {
            apiAreaSection.SetShellDesign(name,
                properties.MaterialName,
                properties.RebarLayout,
                properties.CoverTopDirection1,
                properties.CoverTopDirection2,
                properties.CoverBottomDirection1,
                properties.CoverBottomDirection2);
        }
        #endregion
    }
}
