using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    class SectionDesigner // SectionDesignerProperties
    {
        // TODO: Complete SectionDesigner as a composite section
#if !BUILD_ETABS2015
        ///// <summary>
        ///// Gets the section designer section.
        ///// </summary>
        ///// <param name="name">The name of an existing section designer property.</param>
        ///// <param name="nameMaterial">The name of the base material property for the section.</param>
        ///// <param name="shapeNames">The name of each shape in the section designer section.</param>
        ///// <param name="sectionTypes">The type of each shape in the section designer section.</param>
        ///// <param name="designType">The design option for the section.</param>
        ///// <param name="color">The display color assigned to the section.</param>
        ///// <param name="notes">The notes, if any, assigned to the section.</param>
        ///// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        ///// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        //public void GetSectionDesignerSection(string name,
        //    out string nameMaterial,
        //    out string[] shapeNames,
        //    out eSectionDesignerSectionType[] sectionTypes,
        //    out eSectionDesignerDesignOption designType,
        //    out int color,
        //    out string notes,
        //    out string GUID)
        //{

        //}

        ///// <summary>
        ///// Sets the section designer section.
        ///// </summary>
        ///// <param name="name">The name of an existing or new frame section property.
        ///// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        ///// <param name="nameMaterial">The name of the base material property for the section.</param>
        ///// <param name="designType">The design option for the section.
        ///// When <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.GeneralSteel" /> is assigned, the material property specified by the <paramref name="nameMaterial" /> item must be a steel material;
        ///// otherwise the program sets <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.NoDesign" />.
        ///// Similarly, when <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.ConcreteColumnCheck" /> or <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.ConcreteColumnDesign" /> is assigned, the material property specified by the <paramref name="nameMaterial" /> item must be a concrete material;
        ///// otherwise the program sets <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.NoDesign" />.</param>
        ///// <param name="color">The display color assigned to the section.
        ///// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        ///// <param name="notes">The notes, if any, assigned to the section.</param>
        ///// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        ///// If this item is input as Default, the program assigns a GUID to the section.</param>
        ///// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        //public void SetSectionDesignerSection(string name,
        //    string nameMaterial,
        //    eSectionDesignerDesignOption designType,
        //    int color = -1,
        //    string notes = "",
        //    string GUID = "")
        //{

        //}
#endif
    }
}
