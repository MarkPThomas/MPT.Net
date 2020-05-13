using System.Collections.Generic;
using System.Linq;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Rebar;
using MPT.CSI.Serialize.Models.Components.Definitions.Masses;
using MPT.CSI.Serialize.Models.Components.Grids;
using MPT.CSI.Serialize.Models.Components.ProjectSettings.Misc;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Units;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal class ReadGeneralSettings
    {
        internal static void DefineGeneralSettings(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.PROGRAM_CONTROL, SetPROGRAM_CONTROL);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_DIMENSIONAL, SetPREFERENCES_DIMENSIONAL);
            reader.ReadSingleTable(SAP2000Tables.PROJECT_INFORMATION, SetPROJECT_INFORMATION);
            reader.ReadSingleTable(SAP2000Tables.ACTIVE_DEGREES_OF_FREEDOM, SetACTIVE_DEGREES_OF_FREEDOM);
            reader.ReadSingleTable(SAP2000Tables.COORDINATE_SYSTEMS, SetCOORDINATE_SYSTEMS);
            reader.ReadSingleTable(SAP2000Tables.GRID_LINES, SetGRID_LINES);
            reader.ReadSingleTable(SAP2000Tables.ANALYSIS_OPTIONS, SetANALYSIS_OPTIONS);
            reader.ReadSingleTable(SAP2000Tables.AUTO_WAVE_3_WAVE_CHARACTERISTICS_GENERAL, SetAUTO_WAVE_3_WAVE_CHARACTERISTICS_GENERAL);
            reader.ReadSingleTable(SAP2000Tables.JOINT_PATTERN_DEFINITIONS, SetJOINT_PATTERN_DEFINITIONS);
            reader.ReadSingleTable(SAP2000Tables.REBAR_SIZES, SetREBAR_SIZES);
        }

        /// <summary>
        /// Sets the program control.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetPROGRAM_CONTROL(Model model, List<Dictionary<string, string>> table)
        {
            Dictionary<string, string> row = table[0];
            model.Settings.Program.ProgramName = row["ProgramName"];
            model.Settings.Program.VersionName = row["Version"];

            model.Settings.ModelInformation.UnitsPresent.Units = Enums.EnumLibrary.ConvertStringToEnumByDescription<eUnits>(row["CurrUnits"]);
            model.Settings.ModelInformation.AutoRegenerateHingesAfterImport = Adaptor.fromYesNo(row["RegenHinge"]);

            model.Design.AluminumDesigner.Code = row["AlumCode"];
            model.Design.SteelColdFormedDesigner.Code = row["ColdCode"];
            model.Design.ConcreteDesigner.Code = row["ConcCode"];
            model.Design.SteelDesigner.Code = row["SteelCode"];
        }

        /// <summary>
        /// Sets the preferences dimensional.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetPREFERENCES_DIMENSIONAL(Model model, List<Dictionary<string, string>> table)
        {
            Dictionary<string, string> row = table[0];
            model.Settings.ModelInformation.DimensionalPreferences.AutoZoomStep = Adaptor.toInteger(row["AutoZoom"]);
            model.Settings.ModelInformation.DimensionalPreferences.ShrinkFactor = Adaptor.toInteger(row["ShrinkFact"]);
            model.Settings.ModelInformation.DimensionalPreferences.MaxLineLengthInTextFile = Adaptor.toInteger(row["TextFileLen"]);
            model.Settings.ModelInformation.DimensionalPreferences.MaxFontSize = Adaptor.toInteger(row["MaxFont"]);
            model.Settings.ModelInformation.DimensionalPreferences.MinFontSize = Adaptor.toInteger(row["MinFont"]);
            model.Settings.ModelInformation.DimensionalPreferences.PrinterLineThickness = Adaptor.toInteger(row["PLineThick"]);
            model.Settings.ModelInformation.DimensionalPreferences.ScreenLineThickness = Adaptor.toInteger(row["SLineThick"]);
            model.Settings.ModelInformation.DimensionalPreferences.ScreenSelectionTolerance = Adaptor.toInteger(row["SelectTol"]);
            model.Settings.ModelInformation.DimensionalPreferences.ScreenSnapTolerance = Adaptor.toInteger(row["SnapTol"]);
            model.Settings.ModelInformation.DimensionalPreferences.MergeTolerance = Adaptor.toDouble(row["MergeTol"]);
            model.Settings.ModelInformation.DimensionalPreferences.NudgeDistance = Adaptor.toDouble(row["Nudge"]);
            model.Settings.ModelInformation.DimensionalPreferences.FineGridSpacing = Adaptor.toDouble(row["FineGrid"]);
        }

        /// <summary>
        /// Sets the project information.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetPROJECT_INFORMATION(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                model.Settings.ProjectInformation.ProjectInfoItems.Add(tableRow["Item"]);
                string projectInfoData = string.Empty;
                if (tableRow.ContainsKey("Data"))
                {
                    projectInfoData = tableRow["Data"];
                }
                model.Settings.ProjectInformation.ProjectInfoData.Add(projectInfoData);
            }
        }

        /// <summary>
        /// Sets the active degrees of freedom.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetACTIVE_DEGREES_OF_FREEDOM(Model model, List<Dictionary<string, string>> table)
        {
            Dictionary<string, string> row = table[0];

            DegreesOfFreedomGlobal dof = new DegreesOfFreedomGlobal
            {
                UX = Adaptor.fromYesNo(row["UX"]),
                UY = Adaptor.fromYesNo(row["UY"]),
                UZ = Adaptor.fromYesNo(row["UZ"]),
                RX = Adaptor.fromYesNo(row["RX"]),
                RY = Adaptor.fromYesNo(row["RY"]),
                RZ = Adaptor.fromYesNo(row["RZ"])
            };

            model.Settings.ModelInformation.ActiveDegreesOfFreedom = dof;
        }


        /// <summary>
        /// Sets the coordinate systems.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetCOORDINATE_SYSTEMS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Name"];
                CoordinateSystem coordinateSystem = new CoordinateSystem(name)
                {
                    CoordinateType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eCoordinateType>(tableRow["Type"]),
                    OriginOffset = new Displacements
                    {
                        UX = Adaptor.toDouble(tableRow["X"]),
                        UY = Adaptor.toDouble(tableRow["Y"]),
                        UZ = Adaptor.toDouble(tableRow["Z"]),
                        RX = Adaptor.toDouble(tableRow["AboutX"]),
                        RY = Adaptor.toDouble(tableRow["AboutY"]),
                        RZ = Adaptor.toDouble(tableRow["AboutZ"]),
                    }
                };
                if (coordinateSystem.Name == CoordinateSystems.Global)
                {
                    model.Settings.ModelInformation.CoordinateSystems.GlobalCoordinateSystem = coordinateSystem;
                }
                else
                {
                    model.Settings.ModelInformation.CoordinateSystems.UserCoordinateSystems.Add(coordinateSystem);
                }
            }
        }


        /// <summary>
        /// Sets the grid lines.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetGRID_LINES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["CoordSys"];
                GridLine gridLine = new GridLine()
                {
                    AxisDirection = Enums.EnumLibrary.ConvertStringToEnumByDescription<eDirection>(tableRow["AxisDir"]),
                    GridID = tableRow["GridID"],
                    Coordinate = Adaptor.toDouble(tableRow["XRYZCoord"]),
                    LineType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLineType>(tableRow["LineType"]),
                    LineColor = tableRow["LineColor"],
                    Visible = Adaptor.fromYesNo(tableRow["Visible"]),
                    BubbleLocation = Enums.EnumLibrary.ConvertStringToEnumByDescription<eBubbleLocation>(tableRow["BubbleLoc"])
                };
                if (tableRow.ContainsKey("AllVisible"))
                {
                    gridLine.AllVisible = Adaptor.fromYesNo(tableRow["AllVisible"]);
                    gridLine.BubbleSize = Adaptor.toDouble(tableRow["BubbleSize"]);
                }
                
                GridLines gridLines = null;
                if (name == CoordinateSystems.Global)
                {
                    gridLines = model.Settings.ModelInformation.CoordinateSystems.GlobalCoordinateSystem.GridLines;
                }
                else
                {
                    CoordinateSystem coordinateSystem = model.Settings.ModelInformation.CoordinateSystems.UserCoordinateSystems.First(c => c.Name == name);
                    gridLines = coordinateSystem.GridLines;
                }
                gridLines?.Lines.Add(gridLine);
            }
        }

        /// <summary>
        /// Sets the analysis options.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetANALYSIS_OPTIONS(Model model, List<Dictionary<string, string>> table)
        {
            Dictionary<string, string> row = table[0];

            model.Analysis.Analyzer.SolverType =
                Enums.EnumLibrary.ConvertStringToEnumByDescription<eSolverType>(row["Solver"]);
            model.Analysis.Analyzer.SolverProcessType =
                Enums.EnumLibrary.ConvertStringToEnumByDescription<eSolverProcessType>(row["SolverProc"]);
            model.Analysis.Analyzer.Force32BitSolver = Adaptor.fromYesNo(row["Force32Bit"]);
            model.Analysis.Analyzer.StiffnessCase = row["StiffCase"];
            model.Analysis.Analyzer.UndeformedGeometryModificationType = row["GeomMod"];
            model.Analysis.Analyzer.HingeOption = row["HingeOpt"];
        }


        /// <summary>
        /// Sets the automatic wave 3 wave characteristics general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAUTO_WAVE_3_WAVE_CHARACTERISTICS_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            WaveCharacteristics autoWave = model.Settings.ModelInformation.GeneralWaveCharacteristics;
            Dictionary<string, string> row = table[0];

            autoWave.Characteristics = row["WaveChar"];
            autoWave.WaveType = row["WaveType"];
            autoWave.KinematicsFactor = Adaptor.toDouble(row["KinFactor"]);
            autoWave.StormWaterDepth = Adaptor.toDouble(row["SWaterDepth"]);
            autoWave.Height = Adaptor.toDouble(row["WaveHeight"]);
            autoWave.Period = Adaptor.toDouble(row["WavePeriod"]);
            autoWave.Theory = row["WaveTheory"];
        }


        /// <summary>
        /// Sets the joint pattern definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetJOINT_PATTERN_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                model.Settings.ModelInformation.PatternDefinitions.Add(tableRow["Pattern"]);
            }
        }


        /// <summary>
        /// Sets the rebar sizes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetREBAR_SIZES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Bar bar = model.Components.RebarSizes.FillItem(tableRow["RebarID"]);
                bar.Area = Adaptor.toDouble(tableRow["Area"]);
                bar.Diameter = Adaptor.toDouble(tableRow["Diameter"]);
            }
        }

        /// <summary>
        /// Sets the mass source.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetMASS_SOURCE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                MassSource massSource = model.Settings.ModelInformation.MassSources.FillItem(tableRow["MassSource"]);
                if (tableRow.ContainsKey("Elements"))
                {
                    massSource.IsFromElements = Adaptor.fromYesNo(tableRow["Elements"]);
                    massSource.IsFromMasses = Adaptor.fromYesNo(tableRow["Masses"]);
                    massSource.IsDefault = Adaptor.fromYesNo(tableRow["IsDefault"]);
                    massSource.IsFromLoads = Adaptor.fromYesNo(tableRow["Loads"]);
                }

                if (tableRow.ContainsKey("LoadPat"))
                {
                    massSource.LoadPatterns.Add(
                        new LoadPatternTuple
                        {
                            Load = model.Loading.Patterns[tableRow["LoadPat"]],
                            ScaleFactor = Adaptor.toDouble(tableRow["Multiplier"])
                        });
                }
            }
        }
    }
}
