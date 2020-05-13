using System.Linq;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.FrameSections
{
    public class RectangleSection : FrameSection
    {
        public static RectangleSection Factory(
            string uniqueName,
            eFrameSectionType frameType,
            double t3,
            double t2,
            double tf,
            double tw,
            double t2b,
            double tfb,
            CSiApplication app = null)
        {
            if (Registry.FrameSections.Keys.Contains(uniqueName)) return (RectangleSection)Registry.FrameSections[uniqueName];

            RectangleSection frameSection = (RectangleSection)FrameSection.Factory(uniqueName, t3, t2, tf, tw, t2b, tfb);
            
            if (app != null)
            {
                frameSection.GetRectangle(app);
            }
            Registry.FrameSections.Add(uniqueName, frameSection);
            return frameSection;
        }

        public static RectangleSection Factory(
            string uniqueName,
            CSiApplication app = null)
        {
            if (Registry.FrameSections.Keys.Contains(uniqueName)) return (RectangleSection)Registry.FrameSections[uniqueName];

            RectangleSection frameSection = (RectangleSection)FrameSection.Factory(uniqueName);

            if (app != null)
            {
                frameSection.GetRectangle(app);
            }
            Registry.FrameSections.Add(uniqueName, frameSection);
            return frameSection;
        }

        public RectangleSection(string name, eFrameSectionType type = eFrameSectionType.Rectangular) : base(name, type)
        {

        }

        public void GetRectangle(CSiApplication app)
        {
            app.Model.Definitions.Properties.FrameSection.GetRectangle(Name,
                out var fileName,
                out var nameMaterial,
                out var t3,
                out var t2,
                out var color,
                out var notes,
                out var guid);

            FileName = fileName;
            this.t3 = t3;
            this.t2 = t2;
            Color = color;
            Notes = notes;
            GUID = guid;
            Material = new Steel(nameMaterial);
            if (Material is Steel material)
            {
                material.FillData();
            }
        }
    }
}
