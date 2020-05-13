using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadMaterials
    {
        internal static void DefineMaterials(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_01_GENERAL, setMATERIAL_PROPERTIES_01_GENERAL);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_02_BASIC_MECHANICAL_PROPERTIES, setMATERIAL_PROPERTIES_02_BASIC_MECHANICAL_PROPERTIES);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03A_STEEL_DATA, setMATERIAL_PROPERTIES_03A_STEEL_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03B_CONCRETE_DATA, setMATERIAL_PROPERTIES_03B_CONCRETE_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03C_ALUMINUM_DATA, setMATERIAL_PROPERTIES_03C_ALUMINUM_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03D_COLD_FORMED_DATA, setMATERIAL_PROPERTIES_03D_COLD_FORMED_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03E_REBAR_DATA, setMATERIAL_PROPERTIES_03E_REBAR_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03F_TENDON_DATA, setMATERIAL_PROPERTIES_03F_TENDON_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03G_OTHER_DATA, setMATERIAL_PROPERTIES_03G_OTHER_DATA);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_04_USER_STRESS_STRAIN_CURVES, setMATERIAL_PROPERTIES_04_USER_STRESS_STRAIN_CURVES);
            reader.ReadSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_06_DAMPING_PARAMETERS, setMATERIAL_PROPERTIES_06_DAMPING_PARAMETERS);
        }

        /// <summary>
        /// Sets the material properties 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Material material = model.Components.Materials.FillItem(tableRow["Material"]);
                material.Type = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMaterialPropertyType>(tableRow["Type"]);
                material.SymmetryType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMaterialSymmetryType>(tableRow["SymType"]);
                material.Color = Adaptor.toInteger(tableRow["Color"]);
                material.Notes = tableRow["Notes"];
            }
        }


        /// <summary>
        /// Sets the material properties 02 basic mechanical properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_02_BASIC_MECHANICAL_PROPERTIES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];

                MaterialByTemperature materialByTemperature;
                MaterialMechanics materialMechanics;
                if (tableRow.ContainsKey("Temp"))
                {
                    materialByTemperature = MaterialByTemperature.Factory(name, material.Type, Adaptor.toDouble(tableRow["Temp"]));
                    materialMechanics = MaterialMechanics.Factory(
                        name,
                        material.SymmetryType,
                        materialByTemperature.Temperature);
                }
                else
                {
                    materialByTemperature = MaterialByTemperature.Factory(name, material.Type);
                    materialMechanics = MaterialMechanics.Factory(
                        name,
                        material.SymmetryType);
                }

                switch (materialMechanics)
                {
                    case MaterialMechanicsAnisotropic anisotropic:
                        anisotropic.AnisotropicProperties.ModulusOfElasticity.E1 = Adaptor.toDouble(tableRow["E1"]);
                        anisotropic.AnisotropicProperties.ModulusOfElasticity.E2 = Adaptor.toDouble(tableRow["E2"]);
                        anisotropic.AnisotropicProperties.ModulusOfElasticity.E3 = Adaptor.toDouble(tableRow["E3"]);

                        anisotropic.AnisotropicProperties.ShearModulus.G12 = Adaptor.toDouble(tableRow["G12"]);
                        anisotropic.AnisotropicProperties.ShearModulus.G13 = Adaptor.toDouble(tableRow["G13"]);
                        anisotropic.AnisotropicProperties.ShearModulus.G23 = Adaptor.toDouble(tableRow["G23"]);

                        anisotropic.AnisotropicProperties.PoissonsRatio.U12 = Adaptor.toDouble(tableRow["U12"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U13 = Adaptor.toDouble(tableRow["U13"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U23 = Adaptor.toDouble(tableRow["U23"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U14 = Adaptor.toDouble(tableRow["U14"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U24 = Adaptor.toDouble(tableRow["U24"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U34 = Adaptor.toDouble(tableRow["U34"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U15 = Adaptor.toDouble(tableRow["U15"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U25 = Adaptor.toDouble(tableRow["U25"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U35 = Adaptor.toDouble(tableRow["U35"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U45 = Adaptor.toDouble(tableRow["U45"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U16 = Adaptor.toDouble(tableRow["U16"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U26 = Adaptor.toDouble(tableRow["U26"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U36 = Adaptor.toDouble(tableRow["U36"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U46 = Adaptor.toDouble(tableRow["U46"]);
                        anisotropic.AnisotropicProperties.PoissonsRatio.U56 = Adaptor.toDouble(tableRow["U56"]);

                        anisotropic.AnisotropicProperties.ThermalCoefficient.A1 = Adaptor.toDouble(tableRow["A1"]);
                        anisotropic.AnisotropicProperties.ThermalCoefficient.A2 = Adaptor.toDouble(tableRow["A2"]);
                        anisotropic.AnisotropicProperties.ThermalCoefficient.A3 = Adaptor.toDouble(tableRow["A3"]);
                        anisotropic.AnisotropicProperties.ThermalCoefficient.A12 = Adaptor.toDouble(tableRow["A12"]);
                        anisotropic.AnisotropicProperties.ThermalCoefficient.A13 = Adaptor.toDouble(tableRow["A13"]);
                        anisotropic.AnisotropicProperties.ThermalCoefficient.A23 = Adaptor.toDouble(tableRow["A23"]);
                        break;

                    case MaterialMechanicsIsotropic isotropic:
                        isotropic.IsotropicProperties.ModulusOfElasticity = Adaptor.toDouble(tableRow["E1"]);
                        isotropic.IsotropicProperties.ShearModulus = Adaptor.toDouble(tableRow["G12"]);
                        isotropic.IsotropicProperties.PoissonsRatio = Adaptor.toDouble(tableRow["U12"]);
                        isotropic.IsotropicProperties.ThermalCoefficient = Adaptor.toDouble(tableRow["A1"]);
                        break;

                    case MaterialMechanicsOrthotropic orthotropic:
                        orthotropic.OrthotropicProperties.ModulusOfElasticity.E1 = Adaptor.toDouble(tableRow["E1"]);
                        orthotropic.OrthotropicProperties.ModulusOfElasticity.E2 = Adaptor.toDouble(tableRow["E2"]);
                        orthotropic.OrthotropicProperties.ModulusOfElasticity.E3 = Adaptor.toDouble(tableRow["E3"]);

                        orthotropic.OrthotropicProperties.ShearModulus.G12 = Adaptor.toDouble(tableRow["G12"]);
                        orthotropic.OrthotropicProperties.ShearModulus.G13 = Adaptor.toDouble(tableRow["G13"]);
                        orthotropic.OrthotropicProperties.ShearModulus.G23 = Adaptor.toDouble(tableRow["G23"]);

                        orthotropic.OrthotropicProperties.PoissonsRatio.U12 = Adaptor.toDouble(tableRow["U12"]);
                        orthotropic.OrthotropicProperties.PoissonsRatio.U13 = Adaptor.toDouble(tableRow["U13"]);
                        orthotropic.OrthotropicProperties.PoissonsRatio.U23 = Adaptor.toDouble(tableRow["U23"]);

                        orthotropic.OrthotropicProperties.ThermalCoefficient.A1 = Adaptor.toDouble(tableRow["A1"]);
                        orthotropic.OrthotropicProperties.ThermalCoefficient.A2 = Adaptor.toDouble(tableRow["A2"]);
                        orthotropic.OrthotropicProperties.ThermalCoefficient.A3 = Adaptor.toDouble(tableRow["A3"]);
                        break;

                    case MaterialMechanicsUniaxial uniaxial:
                        uniaxial.UniaxialProperties.ModulusOfElasticity = Adaptor.toDouble(tableRow["E1"]);
                        uniaxial.UniaxialProperties.ThermalCoefficient = Adaptor.toDouble(tableRow["A1"]);
                        break;
                }
                materialByTemperature.Mechanics = materialMechanics;
                materialByTemperature.WeightPerVolume = Adaptor.toDouble(tableRow["UnitWeight"]);
                materialByTemperature.MassPerVolume = Adaptor.toDouble(tableRow["UnitMass"]);

                material.SetMaterialByTemperature(materialByTemperature);
            }
        }

        #region Material - ByType
        /// <summary>
        /// Sets the material properties 03 a steel data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03A_STEEL_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                Steel steel = getMaterialProperties<Steel>(tableRow, material);
                steel.Properties.StressStrainCurveType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eSteelStressStrainCurveType>(tableRow["SSCurveOpt"]);
                steel.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                steel.Properties.Fy = Adaptor.toDouble(tableRow["Fy"]);
                steel.Properties.Fu = Adaptor.toDouble(tableRow["Fu"]);
                steel.Properties.Fye = Adaptor.toDouble(tableRow["EffFy"]);
                steel.Properties.Fue = Adaptor.toDouble(tableRow["EffFu"]);
                steel.Properties.StrainAtHardening = Adaptor.toDouble(tableRow["SHard"]);
                steel.Properties.StrainAtMaxStress = Adaptor.toDouble(tableRow["SMax"]);
                steel.Properties.StrainAtRupture = Adaptor.toDouble(tableRow["SRup"]);
                steel.Properties.FinalSlope = Adaptor.toDouble(tableRow["FinalSlope"]);
            }
        }


        /// <summary>
        /// Sets the material properties 03 b concrete data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03B_CONCRETE_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                Concrete concrete = getMaterialProperties<Concrete>(tableRow, material);
                concrete.Properties.StressStrainCurveType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eConcreteStressStrainCurveType>(tableRow["SSCurveOpt"]);
                concrete.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                concrete.Properties.fc = Adaptor.toDouble(tableRow["Fc"]);
                concrete.Properties.ShearStrengthReductionFactor = Adaptor.toDouble(tableRow["eFc"]);
                concrete.Properties.IsLightweight = Adaptor.fromYesNo(tableRow["LtWtConc"]);
                concrete.Properties.StrainUnconfinedCompressive = Adaptor.toDouble(tableRow["SFc"]);
                concrete.Properties.StrainUltimate = Adaptor.toDouble(tableRow["SCap"]);
                concrete.Properties.FinalSlope = Adaptor.toDouble(tableRow["FinalSlope"]);
                concrete.Properties.Angles.FrictionAngle = Adaptor.toDouble(tableRow["FAngle"]);
                concrete.Properties.Angles.DilatationalAngle = Adaptor.toDouble(tableRow["DAngle"]);
                // TODO: Implement Time Dependence Material Properties
                // TimeType="CEB-FIP 90"   TimeE=Yes   EFact=1 TimeCreep=Yes CreepFact = 1   TimeShrink=Yes ShrinkFact = 1   CreepType="Full Integration"
            }
        }


        /// <summary>
        /// Sets the material properties 03 c aluminum data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03C_ALUMINUM_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                Aluminum aluminum = getMaterialProperties<Aluminum>(tableRow, material);
                aluminum.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                aluminum.Properties.Fcy = Adaptor.toDouble(tableRow["Fcy"]);
                aluminum.Properties.Fty = Adaptor.toDouble(tableRow["Fty"]);
                aluminum.Properties.Ftu = Adaptor.toDouble(tableRow["Ftu"]);
                aluminum.Properties.Fsu = Adaptor.toDouble(tableRow["Fsu"]);
                aluminum.Properties.Alloy = tableRow["Alloy"];
                aluminum.Properties.AluminumType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eAluminumType>(tableRow["AlumType"]);
            }
        }


        /// <summary>
        /// Sets the material properties 03 d cold formed data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03D_COLD_FORMED_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                ColdFormed coldFormed = getMaterialProperties<ColdFormed>(tableRow, material);
                coldFormed.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                coldFormed.Properties.Fy = Adaptor.toDouble(tableRow["Fy"]);
                coldFormed.Properties.Fu = Adaptor.toDouble(tableRow["Fu"]);
            }
        }


        /// <summary>
        /// Sets the material properties 03 e rebar data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03E_REBAR_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                Rebar rebar = getMaterialProperties<Rebar>(tableRow, material);
                rebar.Properties.StressStrainCurveType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eRebarStressStrainCurveType>(tableRow["SSCurveOpt"]);
                rebar.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                rebar.Properties.Fy = Adaptor.toDouble(tableRow["Fy"]);
                rebar.Properties.Fu = Adaptor.toDouble(tableRow["Fu"]);
                rebar.Properties.Fye = Adaptor.toDouble(tableRow["EffFy"]);
                rebar.Properties.Fue = Adaptor.toDouble(tableRow["EffFu"]);
                rebar.Properties.StrainAtHardening = Adaptor.toDouble(tableRow["SHard"]);
                rebar.Properties.StrainUltimate = Adaptor.toDouble(tableRow["SCap"]);
                rebar.Properties.UseCaltransStressStrainDefaults = Adaptor.fromYesNo(tableRow["UseCTDef"]);
                rebar.Properties.FinalSlope = Adaptor.toDouble(tableRow["FinalSlope"]);
            }
        }


        /// <summary>
        /// Sets the material properties 03 f tendon data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03F_TENDON_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                TendonMaterial tendon = getMaterialProperties<TendonMaterial>(tableRow, material);
                tendon.Properties.StressStrainCurveType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eTendonStressStrainCurveType>(tableRow["SSCurveOpt"]);
                tendon.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                tendon.Properties.Fy = Adaptor.toDouble(tableRow["Fy"]);
                tendon.Properties.Fu = Adaptor.toDouble(tableRow["Fu"]);
                tendon.Properties.FinalSlope = Adaptor.toDouble(tableRow["FinalSlope"]);
            }
        }


        /// <summary>
        /// Sets the material properties 03 g other data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03G_OTHER_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                NoDesign other = getMaterialProperties<NoDesign>(tableRow, material);
                other.Properties.StressStrainHysteresisType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eHysteresisType>(tableRow["SSHysType"]);
                other.Properties.FrictionAngle = Adaptor.toDouble(tableRow["FAngle"]);
                other.Properties.DilatationalAngle = Adaptor.toDouble(tableRow["DAngle"]);
            }
        }


        /// <summary>
        /// Gets the material properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableRow">The table row.</param>
        /// <param name="material">The material.</param>
        /// <returns>T.</returns>
        private static T getMaterialProperties<T>(Dictionary<string, string> tableRow, Material material) where T : MaterialByTemperature
        {
            if (!tableRow.ContainsKey("Temp")) return (T)material.GetProperties();

            int temperatureIndex = material.Temperatures.IndexOf(Adaptor.toDouble(tableRow["Temp"]));
            return (T)material.GetProperties(temperatureIndex);
        }
        #endregion


        /// <summary>
        /// Sets the material properties 04 user stress strain curves.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_04_USER_STRESS_STRAIN_CURVES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                MaterialByTemperature materialByTemperature = getMaterialProperties<MaterialByTemperature>(tableRow, material);

                double stress = Adaptor.toDouble(tableRow["Stress"]);
                double strain = Adaptor.toDouble(tableRow["Strain"]);
                eStressStrainPointID pointId = eStressStrainPointID.None;
                if (tableRow.ContainsKey("PointID")) pointId = Enums.EnumLibrary.ConvertStringToEnumByDescription<eStressStrainPointID>(tableRow["PointID"]);

                materialByTemperature.StressStrainCurve.Add(new StressStrainPoint(stress, strain, pointId));
            }
        }

        // TODO: Implement Time Dependence Material Properties
        //TABLE:  "MATERIAL PROPERTIES 05A - TIME DEPENDENCE - CEB-FIP 90"
        //Material=4000Psi-1   CementCoeff=0.25   RelHumid=50   NotionSize=0.1   BetaSC=5   ShrinkStart=0
        //Material=MAT-4   CementCoeff=0.25   RelHumid=50   NotionSize=0.1   BetaSC=5   ShrinkStart=1

        //TABLE:  "MATERIAL PROPERTIES 05B - TIME DEPENDENCE - CEB-FIP 2010"
        //Material=MAT-5   RelHumid=50   ShrinkStart=2   CementType=42.5N

        //TABLE:  "MATERIAL PROPERTIES 05C - TIME DEPENDENCE - ACI 209R-92"
        //Material=MAT-2   RelHumid=50   ShrinkStart=0   A=2.3   Beta=0.92   CuringType=Moist Slump = 0.06858   FineAgg=50   AirContent=6   CementCont=700

        //TABLE:  "MATERIAL PROPERTIES 05D - TIME DEPENDENCE - USER STIFFNESS"
        //Material=MAT-10   Age=28   Multiplier=1

        //TABLE:  "MATERIAL PROPERTIES 05E - TIME DEPENDENCE - USER CREEP"
        //Material=MAT-10   A=1   B=1   Ho=0.1   AgeAtLoad=1   Days=0   CreepCoeff=0

        //TABLE:  "MATERIAL PROPERTIES 05F - TIME DEPENDENCE - USER SHRINKAGE"
        //Material=MAT-10   A=1   B=1   Ho=0.1   Age=0   ShrinkStrn=0

        //TABLE:  "MATERIAL PROPERTIES 05G - TIME DEPENDENCE - JTG D62-2004"
        //Material=MAT-8   RelHumid=50   ShrinkStart=1   CementCoeff=0.25

        //TABLE:  "MATERIAL PROPERTIES 05H - TIME DEPENDENCE - EUROCODE 2-2004"
        //Material=MAT-6   RelHumid=50   ShrinkStart=0   CementType="Class R"

        //TABLE:  "MATERIAL PROPERTIES 05I - TIME DEPENDENCE - AS 3600-2009"
        //Material=MAT-3   CompFactA=2.3   CompFactB=0.92   CreepCoeff=4.2   Environment="Temperate Inland"   DryShrink=0.0008

        //TABLE:  "MATERIAL PROPERTIES 05J - TIME DEPENDENCE - GL2000"
        //Material=MAT-7   RelHumid=50   ShrinkStart=1   CementType="Type II"

        //TABLE:  "MATERIAL PROPERTIES 05K - TIME DEPENDENCE - NZS 3101-2006"
        //Material=MAT-9   DeltaF=10   CementType=42.5N RelHumid = 50   CreepCoeff=4.2   AggTypeFact=1   DryShrink=0.0008


        //TABLE:  "MATERIAL PROPERTIES 06 - DAMPING PARAMETERS"
        /// <summary>
        /// Sets the material properties 06 damping parameters.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_06_DAMPING_PARAMETERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Material"];
                Material material = model.Components.Materials[name];
                MaterialByTemperature materialByTemperature = getMaterialProperties<MaterialByTemperature>(tableRow, material);
                materialByTemperature.DampingProperties.HystereticStiffnessCoefficient = Adaptor.toDouble(tableRow["HysStiff"]);
                materialByTemperature.DampingProperties.HystereticMassCoefficient = Adaptor.toDouble(tableRow["HysMass"]);
                materialByTemperature.DampingProperties.ViscousStiffnessCoefficient = Adaptor.toDouble(tableRow["VisStiff"]);
                materialByTemperature.DampingProperties.ViscousMassCoefficient = Adaptor.toDouble(tableRow["VisMass"]);
                materialByTemperature.DampingProperties.ModalDampingRatio = Adaptor.toDouble(tableRow["ModalRatio"]);
            }
        }

        // TODO: Implement Hysteresis Parameter Material Properties
        //TABLE:  "MATERIAL PROPERTIES 08A - HYSTERESIS PARAMETERS - PIVOT"
        //Material=MAT-4   Alpha1=10   Alpha2=10   Beta1=0.7   Beta2=0.7   Eta=0

        //TABLE:  "MATERIAL PROPERTIES 08B - HYSTERESIS PARAMETERS - CONCRETE"
        //Material=MAT-5   EnrgyDegFac=0

        //TABLE:  "MATERIAL PROPERTIES 08C - HYSTERESIS PARAMETERS - BRB HARDENING"
        //Material=MAT-6   IsSymmetric=Yes TensHardFac = 1   TensMaxPdef=25   TensAccPDef=50   TensProAcc=0

        //TABLE:  "MATERIAL PROPERTIES 08D - HYSTERESIS PARAMETERS - DEGRADING"
        //Material=MAT-7   IsSymmetric=Yes Tensf0 = 1   Tensf1=1   Tensf2=1   Tensx1=5   Tensx2=10   TensADWtFac=0   StiffWtFac=0   LrgSmlWtFac=0
        //Material=MAT-8   IsSymmetric=Yes Tensf0 = 1   Tensf1=1   Tensf2=1   Tensx1=5   Tensx2=10   TensADWtFac=0   StiffWtFac=0   LrgSmlWtFac=0
    }
}
