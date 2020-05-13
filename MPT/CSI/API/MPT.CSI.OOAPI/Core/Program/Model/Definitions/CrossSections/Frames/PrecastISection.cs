#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    public class PrecastISection : FrameSection
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
            eFrameSectionType type = eFrameSectionType.PreCastConcreteGirderI) : base(app, material, name, type)
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
            // TODO: SAP2000 - Finish PrecastISection set.
        }

#endregion

        /// <summary>
        /// Returns frame section property data for a precast concrete I girder frame section.
        /// </summary>
        /// <param name="name">The name of an existing precast concrete I girder frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="T1">The web thickness dimension T1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="T2">The web thickness dimension T2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="C1">The bottom flange chamfer dimension, denoted as C1 on the precast concrete I girder definition form.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPrecastI(string name,
            ref string fileName,
            ref string nameMaterial,
            ref double B1,
            ref double B2,
            ref double B3,
            ref double B4,
            ref double D1,
            ref double D2,
            ref double D3,
            ref double D4,
            ref double D5,
            ref double D6,
            ref double D7,
            ref double T1,
            ref double T2,
            ref double C1,
            ref int color,
            ref string notes,
            ref string GUID)
        {

        }


        /// <summary>
        /// This function initializes a precast concrete I girder frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete I girder definition form. B1 &gt; 0, [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete I girder definition form. B2 &gt; 0, [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete I girder definition form. B3 &gt;= 0, [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete I girder definition form. B4 &gt;= 0, [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete I girder definition form. D1 &gt; 0, [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete I girder definition form. D2 &gt; 0, [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete I girder definition form. D3 &gt;= 0, [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete I girder definition form. D4 &gt;= 0, [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete I girder definition form. D5 &gt; 0, [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete I girder definition form. D6 &gt;= 0, [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete I girder definition form. D7 &gt;= 0, [L].</param>
        /// <param name="T1">The web thickness dimension T1 defined on the precast concrete I girder definition form. T1 &gt; 0, [L].</param>
        /// <param name="T2">The web thickness dimension T2 defined on the precast concrete I girder definition form. T2 &gt; 0, [L].</param>
        /// <param name="C1">The bottom flange chamfer dimension, denoted as C1 on the precast concrete I girder definition form.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPrecastI(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            double T1,
            double T2,
            double C1,
            int color = -1,
            string notes = "",
            string GUID = "")
        {

        }
    }
}
#endif