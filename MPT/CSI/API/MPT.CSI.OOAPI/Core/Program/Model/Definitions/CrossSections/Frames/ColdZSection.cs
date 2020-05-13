#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    public class ColdZSection : FrameSection
    {
#region Fields & Properties

#endregion

#region Initialization
        internal new static ColdZSection Factory(
            ApiCSiApplication app, 
            Materials.Materials material,
            string uniqueName)
        {
            ColdZSection frameSection = new ColdZSection(app, material, uniqueName);
            frameSection.FillData();

            return frameSection;
        }

        protected ColdZSection(
            ApiCSiApplication app, 
            Materials.Materials material,
            string name, 
            eFrameSectionType type = eFrameSectionType.ColdZ) : base(app, material, name, type)
        {

        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
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
            // TODO: SAP2000 - Finish ColdZSection set.
        }

#endregion
        /// <summary>
        /// Returns frame section property data for a cold formed Z-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramref name="lipAngle" />  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetColdZ(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double t3,
            ref double t2,
            ref double thickness,
            ref double radius,
            ref double lipDepth,
            ref double lipAngle,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }

        /// <summary>
        /// This function initializes frame section property data for a cold formed Z-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramref name="lipAngle" />  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetColdZ(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            double lipAngle,
            int color = -1,
            string notes = "",
            string GUID = "")
        {

        }
    }
}
#endif
