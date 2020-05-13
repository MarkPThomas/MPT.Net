#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    public class HybridUSection : FrameSection
    {
#region Fields & Properties

#endregion

#region Initialization
        internal new static RectangleSection Factory(
            ApiCSiApplication app, 
            Materials.Materials material,
            string uniqueName)
        {
            RectangleSection frameSection = new RectangleSection(app, material, uniqueName);
            frameSection.FillData();

            return frameSection;
        }

        protected RectangleSection(
            ApiCSiApplication app,
            Materials.Materials material, 
            string name, 
            eFrameSectionType type = eFrameSectionType.BuiltUpUHybrid) : base(app, material, name, type)
        {

        }
#endregion

#region Fill/Set
        /// <summary>
        /// Returns frame section property data for the section.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Fill()
        {
        }

        /// <summary>
        /// This function initializes a frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected override void set()
        {
            // TODO: SAP2000 - Finish HybridUSection set.
        }

#endregion
        /// <summary>
        /// Returns frame section property data for a steel hybrid U-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="D1">Web Depth (vertical, inside to inside of flanges). [L].</param>
        /// <param name="B1">Web Distance at Top (CL to CL). [L].</param>
        /// <param name="B2">Bottom Flange Width. [L].</param>
        /// <param name="B3">Top Flange Width (per each). [L].</param>
        /// <param name="B4">Bottom Flange Lip (Web CL to flange edge, may be zero). [L].</param>
        /// <param name="tw">Web Thickness. [L].</param>
        /// <param name="tf">Top Flange Thickness. [L].</param>
        /// <param name="tfb">Bottom Flange Thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetHybridUSection(string name,
            ref string nameMaterialTopFlange,
            ref string nameMaterialWeb,
            ref string nameMaterialBottomFlange,
            ref double D1,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double tw,
            ref double tf,
            ref double tfb,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }

        /// <summary>
        /// This function initializes frame section property data for a steel hybrid U-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="D1">Web Depth (vertical, inside to inside of flanges). [L].</param>
        /// <param name="B1">Web Distance at Top (CL to CL). [L].</param>
        /// <param name="B2">Bottom Flange Width. [L].</param>
        /// <param name="B3">Top Flange Width (per each). [L].</param>
        /// <param name="B4">Bottom Flange Lip (Web CL to flange edge, may be zero). [L].</param>
        /// <param name="tw">Web Thickness. [L].</param>
        /// <param name="tf">Top Flange Thickness. [L].</param>
        /// <param name="tfb">Bottom Flange Thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetHybridUSection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double D1,
            double B1,
            double B2,
            double B3,
            double B4,
            double tw,
            double tf,
            double tfb,
            int color = -1,
            string notes = "",
            string GUID = "")
        {

        }
    }
}
#endif
