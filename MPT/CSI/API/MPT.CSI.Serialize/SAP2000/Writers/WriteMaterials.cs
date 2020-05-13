using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteMaterials
    {
        internal static void DefineMaterials(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_01_GENERAL, setMATERIAL_PROPERTIES_01_GENERAL);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_01T_MATERIAL_TEMPERATURES, setMATERIAL_PROPERTIES_01T_MATERIAL_TEMPERATURES);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_02_BASIC_MECHANICAL_PROPERTIES, setMATERIAL_PROPERTIES_02_BASIC_MECHANICAL_PROPERTIES);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03A_STEEL_DATA, setMATERIAL_PROPERTIES_03A_STEEL_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03B_CONCRETE_DATA, setMATERIAL_PROPERTIES_03B_CONCRETE_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03C_ALUMINUM_DATA, setMATERIAL_PROPERTIES_03C_ALUMINUM_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03D_COLD_FORMED_DATA, setMATERIAL_PROPERTIES_03D_COLD_FORMED_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03E_REBAR_DATA, setMATERIAL_PROPERTIES_03E_REBAR_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03F_TENDON_DATA, setMATERIAL_PROPERTIES_03F_TENDON_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_03G_OTHER_DATA, setMATERIAL_PROPERTIES_03G_OTHER_DATA);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_04_USER_STRESS_STRAIN_CURVES, setMATERIAL_PROPERTIES_04_USER_STRESS_STRAIN_CURVES);
            writer.WriteSingleTable(SAP2000Tables.MATERIAL_PROPERTIES_06_DAMPING_PARAMETERS, setMATERIAL_PROPERTIES_06_DAMPING_PARAMETERS);
        }

        /// <summary>
        /// Sets the material properties 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Material", Adaptor.ToStringEntryLimited(material.Name) },
                        { "Type", Adaptor.fromEnum(material.Type) },
                        { "SymType", Adaptor.fromEnum(material.SymmetryType) },
                        { "Color", Adaptor.fromInteger(material.Color) },
                        { "Notes", Adaptor.ToStringEntryLimited(material.Notes) }
                    });
            }
        }

        private static void setMATERIAL_PROPERTIES_01T_MATERIAL_TEMPERATURES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                // Table only generated for materials with multiple temperatures defined.
                if (material.PropertiesByTemperature.Count < 2) continue;
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    table.Add(tableRowMaterialNameAndTemperature(materialByTemperature));
                }
            }
        }

        /// <summary>
        /// Sets the material properties 02 basic mechanical properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_02_BASIC_MECHANICAL_PROPERTIES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialByTemperature);
                    tableRow["UnitWeight"] = Adaptor.fromDouble(materialByTemperature.WeightPerVolume);
                    tableRow["UnitMass"] = Adaptor.fromDouble(materialByTemperature.MassPerVolume);

                    switch (materialByTemperature.Mechanics)
                    {
                        case MaterialMechanicsAnisotropic anisotropic:
                            tableRow["E1"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ModulusOfElasticity.E1);
                            tableRow["E2"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ModulusOfElasticity.E2);
                            tableRow["E3"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ModulusOfElasticity.E3);

                            tableRow["G12"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ShearModulus.G12);
                            tableRow["G13"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ShearModulus.G13);
                            tableRow["G23"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ShearModulus.G23);

                            tableRow["U12"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U12);
                            tableRow["U13"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U13);
                            tableRow["U23"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U23);
                            tableRow["U14"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U14);
                            tableRow["U24"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U24);
                            tableRow["U34"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U34);
                            tableRow["U15"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U15);
                            tableRow["U25"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U25);
                            tableRow["U35"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U35);
                            tableRow["U45"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U45);
                            tableRow["U16"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U16);
                            tableRow["U26"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U26);
                            tableRow["U36"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U36);
                            tableRow["U46"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U46);
                            tableRow["U56"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.PoissonsRatio.U56);

                            tableRow["A1"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ThermalCoefficient.A1);
                            tableRow["A2"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ThermalCoefficient.A2);
                            tableRow["A3"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ThermalCoefficient.A3);
                            tableRow["A12"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ThermalCoefficient.A12);
                            tableRow["A13"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ThermalCoefficient.A13);
                            tableRow["A23"] = Adaptor.fromDouble(anisotropic.AnisotropicProperties.ThermalCoefficient.A23);
                            break;
                        case MaterialMechanicsIsotropic isotropic:
                            tableRow["E1"] = Adaptor.fromDouble(isotropic.IsotropicProperties.ModulusOfElasticity);
                            tableRow["G12"] = Adaptor.fromDouble(isotropic.IsotropicProperties.ShearModulus);
                            tableRow["U12"] = Adaptor.fromDouble(isotropic.IsotropicProperties.PoissonsRatio);
                            tableRow["A1"] = Adaptor.fromDouble(isotropic.IsotropicProperties.ThermalCoefficient);
                            break;
                        case MaterialMechanicsOrthotropic orthotropic:
                            tableRow["E1"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ModulusOfElasticity.E1);
                            tableRow["E2"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ModulusOfElasticity.E2);
                            tableRow["E3"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ModulusOfElasticity.E3);

                            tableRow["G12"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ShearModulus.G12);
                            tableRow["G13"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ShearModulus.G13);
                            tableRow["G23"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ShearModulus.G23);

                            tableRow["U12"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.PoissonsRatio.U12);
                            tableRow["U13"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.PoissonsRatio.U13);
                            tableRow["U23"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.PoissonsRatio.U23);

                            tableRow["A1"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ThermalCoefficient.A1);
                            tableRow["A2"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ThermalCoefficient.A2);
                            tableRow["A3"] = Adaptor.fromDouble(orthotropic.OrthotropicProperties.ThermalCoefficient.A3);
                            break;
                        case MaterialMechanicsUniaxial uniaxial:
                            tableRow["E1"] = Adaptor.fromDouble(uniaxial.UniaxialProperties.ModulusOfElasticity);
                            tableRow["A1"] = Adaptor.fromDouble(uniaxial.UniaxialProperties.ThermalCoefficient);
                            break;
                    }

                    table.Add(tableRow);
                }
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
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is Steel materialSteel)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialSteel);
                    tableRow["SSCurveOpt"] = Adaptor.fromEnum(materialSteel.Properties.StressStrainCurveType);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialSteel.Properties.StressStrainHysteresisType);
                    tableRow["Fy"] = Adaptor.fromDouble(materialSteel.Properties.Fy);
                    tableRow["Fu"] = Adaptor.fromDouble(materialSteel.Properties.Fu);
                    tableRow["EffFy"] = Adaptor.fromDouble(materialSteel.Properties.Fye);
                    tableRow["EffFu"] = Adaptor.fromDouble(materialSteel.Properties.Fue);
                    tableRow["SHard"] = Adaptor.fromDouble(materialSteel.Properties.StrainAtHardening);
                    tableRow["SMax"] = Adaptor.fromDouble(materialSteel.Properties.StrainAtMaxStress);
                    tableRow["SRup"] = Adaptor.fromDouble(materialSteel.Properties.StrainAtRupture);
                    tableRow["FinalSlope"] = Adaptor.fromDouble(materialSteel.Properties.FinalSlope);
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the material properties 03 b concrete data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03B_CONCRETE_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is Concrete materialConcrete)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialConcrete);
                    tableRow["SSCurveOpt"] = Adaptor.fromEnum(materialConcrete.Properties.StressStrainCurveType);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialConcrete.Properties.StressStrainHysteresisType);
                    tableRow["Fc"] = Adaptor.fromDouble(materialConcrete.Properties.fc);
                    tableRow["eFc"] = Adaptor.fromDouble(materialConcrete.Properties.ShearStrengthReductionFactor);
                    tableRow["LtWtConc"] = Adaptor.toYesNo(materialConcrete.Properties.IsLightweight);
                    tableRow["SFc"] = Adaptor.fromDouble(materialConcrete.Properties.StrainUnconfinedCompressive);
                    tableRow["SCap"] = Adaptor.fromDouble(materialConcrete.Properties.StrainUltimate);
                    tableRow["FinalSlope"] = Adaptor.fromDouble(materialConcrete.Properties.FinalSlope);
                    tableRow["FAngle"] = Adaptor.fromDouble(materialConcrete.Properties.Angles.FrictionAngle);
                    tableRow["DAngle"] = Adaptor.fromDouble(materialConcrete.Properties.Angles.DilatationalAngle);
                    // TODO: Implement Time Dependence Material Properties
                    // TimeType="CEB-FIP 90"   TimeE=Yes   EFact=1 TimeCreep=Yes CreepFact = 1   TimeShrink=Yes ShrinkFact = 1   CreepType="Full Integration"
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the material properties 03 c aluminum data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03C_ALUMINUM_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is Aluminum materialAluminum)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialAluminum);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialAluminum.Properties.StressStrainHysteresisType);
                    tableRow["Fcy"] = Adaptor.fromDouble(materialAluminum.Properties.Fcy);
                    tableRow["Fty"] = Adaptor.fromDouble(materialAluminum.Properties.Fty);
                    tableRow["Ftu"] = Adaptor.fromDouble(materialAluminum.Properties.Ftu);
                    tableRow["Fsu"] = Adaptor.fromDouble(materialAluminum.Properties.Fsu);
                    tableRow["Alloy"] = Adaptor.ToStringEntryLimited(materialAluminum.Properties.Alloy);
                    tableRow["AlumType"] = Adaptor.fromEnum(materialAluminum.Properties.AluminumType);
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the material properties 03 d cold formed data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03D_COLD_FORMED_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is ColdFormed materialColdFormed)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialColdFormed);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialColdFormed.Properties.StressStrainHysteresisType);
                    tableRow["Fy"] = Adaptor.fromDouble(materialColdFormed.Properties.Fy);
                    tableRow["Fu"] = Adaptor.fromDouble(materialColdFormed.Properties.Fu);
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the material properties 03 e rebar data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03E_REBAR_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is Rebar materialRebar)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialRebar);
                    tableRow["SSCurveOpt"] = Adaptor.fromEnum(materialRebar.Properties.StressStrainCurveType);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialRebar.Properties.StressStrainHysteresisType);
                    tableRow["Fy"] = Adaptor.fromDouble(materialRebar.Properties.Fy);
                    tableRow["Fu"] = Adaptor.fromDouble(materialRebar.Properties.Fu);
                    tableRow["EffFy"] = Adaptor.fromDouble(materialRebar.Properties.Fye);
                    tableRow["EffFu"] = Adaptor.fromDouble(materialRebar.Properties.Fue);
                    tableRow["SHard"] = Adaptor.fromDouble(materialRebar.Properties.StrainAtHardening);
                    tableRow["SCap"] = Adaptor.fromDouble(materialRebar.Properties.StrainUltimate);
                    tableRow["UseCTDef"] = Adaptor.toYesNo(materialRebar.Properties.UseCaltransStressStrainDefaults);
                    tableRow["FinalSlope"] = Adaptor.fromDouble(materialRebar.Properties.FinalSlope);
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the material properties 03 f tendon data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03F_TENDON_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is TendonMaterial materialTendonMaterial)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialTendonMaterial);
                    tableRow["SSCurveOpt"] = Adaptor.fromEnum(materialTendonMaterial.Properties.StressStrainCurveType);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialTendonMaterial.Properties.StressStrainHysteresisType);
                    tableRow["Fy"] = Adaptor.fromDouble(materialTendonMaterial.Properties.Fy);
                    tableRow["Fu"] = Adaptor.fromDouble(materialTendonMaterial.Properties.Fu);
                    tableRow["FinalSlope"] = Adaptor.fromDouble(materialTendonMaterial.Properties.FinalSlope);
                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the material properties 03 g other data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_03G_OTHER_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    if (!(materialByTemperature is NoDesign materialNoDesign)) return;
                    Dictionary<string, string> tableRow = tableRowMaterialNameAndTemperature(materialNoDesign);
                    tableRow["SSHysType"] = Adaptor.fromEnum(materialNoDesign.Properties.StressStrainHysteresisType);
                    tableRow["FAngle"] = Adaptor.fromDouble(materialNoDesign.Properties.FrictionAngle);
                    tableRow["DAngle"] = Adaptor.fromDouble(materialNoDesign.Properties.DilatationalAngle);
                    table.Add(tableRow);
                }
            }
        }

        private static Dictionary<string, string> tableRowMaterialNameAndTemperature(MaterialByTemperature material)
        {
            Dictionary<string, string> tableRow = new Dictionary<string, string>
                {{"Material", Adaptor.ToStringEntryLimited(material.Name)}};
            if (Math.Abs(material.Temperature) > Constants.Tolerance)
            {
                tableRow["Temp"] = Adaptor.fromDouble(material.Temperature);
            }

            return tableRow;
        }
        #endregion


        /// <summary>
        /// Sets the material properties 04 user stress strain curves.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setMATERIAL_PROPERTIES_04_USER_STRESS_STRAIN_CURVES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    foreach (StressStrainPoint point in materialByTemperature.StressStrainCurve)
                    {
                        Dictionary<string, string> tableRow = new Dictionary<string, string>
                        {
                            { "Material",  Adaptor.ToStringEntryLimited(material.Name) },
                            { "Stress",  Adaptor.fromDouble(point.Stress) },
                            { "Strain",  Adaptor.fromDouble(point.Strain) }
                        };
                        if (point.PointID != eStressStrainPointID.None)
                        {
                            tableRow["PointID"] = Adaptor.fromEnum(point.PointID);
                        }

                        table.Add(tableRow);
                    }
                }
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
            foreach (Material material in model.Components.Materials)
            {
                foreach (MaterialByTemperature materialByTemperature in material.PropertiesByTemperature)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Material",  Adaptor.ToStringEntryLimited(material.Name) },
                            { "HysStiff",  Adaptor.fromDouble(materialByTemperature.DampingProperties.HystereticStiffnessCoefficient) },
                            { "HysMass",  Adaptor.fromDouble(materialByTemperature.DampingProperties.HystereticMassCoefficient) },
                            { "VisStiff",  Adaptor.fromDouble(materialByTemperature.DampingProperties.ViscousStiffnessCoefficient) },
                            { "VisMass",  Adaptor.fromDouble(materialByTemperature.DampingProperties.ViscousMassCoefficient) },
                            { "ModalRatio",  Adaptor.fromDouble(materialByTemperature.DampingProperties.ModalDampingRatio) }
                        });
                }
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
