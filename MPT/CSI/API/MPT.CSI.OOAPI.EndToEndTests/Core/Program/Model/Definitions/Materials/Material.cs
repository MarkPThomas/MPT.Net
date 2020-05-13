using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public abstract class Material : DefinitionElement
    {
        protected static MaterialProperties _materialProperties => Registry.ProgramDefinitions?.Properties?.MaterialProperties;

        /// <summary>
        /// LoadType of the material.
        /// </summary>
        /// <value>The type.</value>
        public eMaterialPropertyType Type { get; protected set; }

        /// <summary>
        /// Mechanical properties of the material.
        /// </summary>
        /// <value>The mechanics.</value>
        public MaterialMechanics Mechanics { get; protected set; }

        public static List<Material> GetAllMaterialProperties()
        {
            List<Material> objects = new List<Material>();
            List<string> names = GetNameList(_materialProperties);

            foreach (var name in names)
            {
                Material material = Factory(name);
                if (material == null) continue;
                
                objects.Add(material);
            }

            return objects;
        }

        /// <summary>
        /// Returns a new material class.
        /// </summary>
        /// <param name="name">Unique material name.</param>
        /// <returns>Steel.</returns>
        public static Material Factory(string name)
        {
            if (Registry.Materials.Keys.Contains(name)) return Registry.Materials[name];

            Tuple<eMaterialPropertyType, eMaterialSymmetryType> materialTypes = GetMaterialType(name);

            Material material;

            switch (materialTypes.Item1)
            {
                case eMaterialPropertyType.Steel:
                    material = Steel.Factory(name);
                    break;

                case eMaterialPropertyType.Concrete:
                    material = Concrete.Factory(name);
                    break;

                case eMaterialPropertyType.Masonry:
                    material = Masonry.Factory(name);
                    break;

                case eMaterialPropertyType.Tendon:
                    material = TendonMaterial.Factory(name);
                    break;

                case eMaterialPropertyType.Rebar:
                    material = Rebar.Factory(name);
                    break;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                case eMaterialPropertyType.Aluminum:
                    material = Aluminum.Factory(name, app);
                    break;

                case eMaterialPropertyType.ColdFormed:
                    material = ColdFormed.Factory(name, app);
                    break;
#endif
                case eMaterialPropertyType.NoDesign:
                    return null;

                default:
                    return null;
            }
            
            Registry.Materials.Add(name, material);
            return material;
        }

        /// <summary>
        /// Returns the names of all defined material properties.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public static List<string> GetNameList(IListableNames app)
        {
            return new List<string>(app.GetNameList());
        }

        public static Tuple<eMaterialPropertyType, eMaterialSymmetryType> GetMaterialType(string name)
        {
            if (_materialProperties == null) return null;
            _materialProperties.GetMaterialType(name,
                out var materialType,
                out var symmetryType);
            return new Tuple<eMaterialPropertyType, eMaterialSymmetryType>(materialType, symmetryType);
        }

        protected Material(string name) : base(name)
        { }

        public override void FillData()
        {
            FillMaterial();
            FillMaterialProperties();
        }

        public void FillMaterial()
        {
            if (_materialProperties == null) return;

            _materialProperties.GetMaterial(Name,
                out var materialType,
                out var color,
                out var notes,
                out var guid);
            Type = materialType;
            Color = color;
            Notes = notes;
            GUID = guid;
        }

        public void FillMaterialProperties()
        {
            if (_materialProperties == null) return;

            _materialProperties.GetMaterialType(Name,
                out var materialType,
                out var symmetryType);

            Type = materialType;
            Mechanics = MaterialMechanics.Factory(Name, symmetryType);
        }



        public override void ChangeName(string newName)
        {
            _materialProperties.ChangeName(Name, newName);
        }

        public override void Delete()
        {
            _materialProperties.Delete(Name);
        }

    }
}
