// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="SlabWaffle.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class SlabWaffle.
    /// </summary>
    /// <seealso cref="SlabExtended{T}" />
    public class SlabWaffle : SlabExtended<SlabWaffleProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static SlabWaffle Factory(ApiCSiApplication app, string uniqueName, SlabWaffleProperties properties = null)
        {
            SlabWaffle areaSection = new SlabWaffle(app, uniqueName) { _properties = properties };
            if (properties == null)
            {
                areaSection.FillData();
            }

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SlabWaffle"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected SlabWaffle(ApiCSiApplication app, string name) : base(app, name) { }
        #endregion

        #region Fill/Set

        /// <summary>
        /// Fills this instance.
        /// </summary>
        public override void Fill()
        {
            _apiAreaSection.GetSlabWaffle(Name,
                out var overallDepth,
                out var slabThickness,
                out var stemWidthTop,
                out var stemWidthBottom,
                out var ribSpacingLocal1,
                out var ribSpacingLocal2);
            
            if (!(_properties is SlabWaffleProperties properties)) return;
            properties.OverallDepth = overallDepth;
            properties.SlabThickness = slabThickness;
            properties.StemWidthTop = stemWidthTop;
            properties.StemWidthBottom = stemWidthBottom;
            properties.RibSpacingLocal1 = ribSpacingLocal1;
            properties.RibSpacingLocal2 = ribSpacingLocal2;
        }

        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>SlabWaffle.</returns>
        internal static SlabWaffle Add(ApiCSiApplication app, string uniqueName, SlabWaffleProperties properties)
        {
            setApi(getApiAreaSection(app), uniqueName, properties);
            return Factory(app, uniqueName, properties);
        }

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(SlabWaffleProperties properties)
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
        protected static void setApi(ApiAreaSection apiAreaSection, string name, SlabWaffleProperties properties)
        {
            apiAreaSection.SetSlabWaffle(
                name,
                properties.OverallDepth,
                properties.SlabThickness,
                properties.StemWidthTop,
                properties.StemWidthBottom,
                properties.RibSpacingLocal1,
                properties.RibSpacingLocal2);
        }

        #endregion
    }
}
#endif
