using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using ProgramDiaphragm = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Diaphragm;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    public class Diaphragm
    {
        protected static ProgramDiaphragm _app => Registry.ProgramDefinitions.Abstractions.Diaphragm;

        // TODO: Include all relevant objects
        // TODO: Implement eDiaphragmOption
        // TODO: Consider constraints in general
        protected static PointObject _pointObject => Registry.ObjectModeler?.PointObject;
        protected static AreaObject _areaObject => Registry.ObjectModeler?.AreaObject;

        public List<Node> Nodes { get; protected set; }
        public List<Area> Areas { get; protected set; }

        /// <summary>
        /// The name of an existing diaphragm. 
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; protected set; }

        public static Diaphragm Factory(string uniqueName)
        {
            if (Registry.Diaphragms.Keys.Contains(uniqueName)) return Registry.Diaphragms[uniqueName];

            Diaphragm diaphragm = new Diaphragm(uniqueName);

            if (_pointObject != null)
            {
                // TODO: Add all point objects to diaphragm?
            }
            if (_areaObject != null)
            {
                // TODO: Add all area objects to diaphragm?
            }

            Registry.Diaphragms.Add(uniqueName, diaphragm);
            return diaphragm;
        }

        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        public static Diaphragm GetDiaphragm(Area area)
        {
            if (_areaObject == null) return null;
            string diaphragmName = _areaObject.GetDiaphragm(area.Name);
            return string.IsNullOrEmpty(diaphragmName) ? null : Factory(diaphragmName);
        }

        /// <summary>
        /// Retrieves the diaphragm for a specified object.
        /// </summary>
        public static Diaphragm GetDiaphragm(Node node)
        {
            if (_pointObject == null) return null;
            _pointObject.GetDiaphragm(node.Name, out var diaphragmOption, out var diaphragmName);
            return string.IsNullOrEmpty(diaphragmName) ? null : Factory(diaphragmName);
        }

        public Diaphragm(string name)
        {
            Name = name;
        }

        // TODO: Add all diaphragm methods. See diaphragm object.

        public void AddToDiaphragm(Area area)
        {
            if (Areas.Contains(area)) return;
            _areaObject?.SetDiaphragm(area.Name, Name);
            Areas.Add(area);
        }

        public void RemoveFromDiaphragm(Area area)
        {
            if (!Areas.Contains(area)) return;
            _areaObject?.SetDiaphragm(area.Name);
            Areas.Remove(area);
        }


        public void AddToDiaphragm(Node node)
        {
            if (Nodes.Contains(node)) return;
            _pointObject?.SetDiaphragm(node.Name, eDiaphragmOption.DefinedDiaphragm, Name);
            Nodes.Add(node);
        }

        public void AddToDiaphragmOfBoundingArea(Node node)
        {
            if (Nodes.Contains(node)) return;
            _pointObject?.SetDiaphragm(node.Name, eDiaphragmOption.FromShellObject, Name);
            // TODO: Finish - determine inheriting from area object
            //Nodes.Add(node);
        }

        public void RemoveFromDiaphragm(Node node)
        {
            if (!Nodes.Contains(node)) return;
            _pointObject?.SetDiaphragm(node.Name, eDiaphragmOption.Disconnect, Name);
            Nodes.Remove(node);
        }
    }
}
