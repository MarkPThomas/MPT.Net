using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Rebar;
using MPT.CSI.Serialize.Models.Components.Definitions.Masses;
using MPT.CSI.Serialize.Models.Components.Grids;
using MPT.CSI.Serialize.Models.Components.ProjectSettings.Misc;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Units;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal class WriteGeneralSettings
    {
        internal static void DefineGeneralSettings(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.PROGRAM_CONTROL, SetPROGRAM_CONTROL);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_DIMENSIONAL, SetPREFERENCES_DIMENSIONAL);
            writer.WriteSingleTable(SAP2000Tables.PROJECT_INFORMATION, SetPROJECT_INFORMATION);
            writer.WriteSingleTable(SAP2000Tables.ACTIVE_DEGREES_OF_FREEDOM, SetACTIVE_DEGREES_OF_FREEDOM);
            writer.WriteSingleTable(SAP2000Tables.COORDINATE_SYSTEMS, SetCOORDINATE_SYSTEMS);
            writer.WriteSingleTable(SAP2000Tables.GRID_LINES, SetGRID_LINES);
            writer.WriteSingleTable(SAP2000Tables.ANALYSIS_OPTIONS, SetANALYSIS_OPTIONS);
            writer.WriteSingleTable(SAP2000Tables.AUTO_WAVE_3_WAVE_CHARACTERISTICS_GENERAL, SetAUTO_WAVE_3_WAVE_CHARACTERISTICS_GENERAL);
            writer.WriteSingleTable(SAP2000Tables.JOINT_PATTERN_DEFINITIONS, SetJOINT_PATTERN_DEFINITIONS);
            writer.WriteSingleTable(SAP2000Tables.REBAR_SIZES, SetREBAR_SIZES);
        }

        /// <summary>
        /// Sets the program control.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetPROGRAM_CONTROL(Model model, List<Dictionary<string, string>> table)
        {
            Dictionary<string, string> row = table[0];
            row["ProgramName"] = Adaptor.ToStringEntryLimited(model.Settings.Program.ProgramName);
            row["Version"] = Adaptor.ToStringEntryLimited(model.Settings.Program.VersionName);

            row["CurrUnits"] = Adaptor.fromEnum(model.Settings.ModelInformation.UnitsPresent.Units);
            row["RegenHinge"] = Adaptor.toYesNo(model.Settings.ModelInformation.AutoRegenerateHingesAfterImport);

            row["AlumCode"] = Adaptor.ToStringEntryLimited(model.Design.AluminumDesigner.Code);
            row["ColdCode"] = Adaptor.ToStringEntryLimited(model.Design.SteelColdFormedDesigner.Code);
            row["ConcCode"] = Adaptor.ToStringEntryLimited(model.Design.ConcreteDesigner.Code);
            row["SteelCode"] = Adaptor.ToStringEntryLimited(model.Design.SteelDesigner.Code);
        }

        /// <summary>
        /// Sets the preferences dimensional.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetPREFERENCES_DIMENSIONAL(Model model, List<Dictionary<string, string>> table)
        {
            Dictionary<string, string> row = table[0];
            row["AutoZoom"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.AutoZoomStep);
            row["ShrinkFact"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.ShrinkFactor);
            row["TextFileLen"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.MaxLineLengthInTextFile);
            row["MaxFont"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.MaxFontSize);
            row["MinFont"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.MinFontSize);
            row["PLineThick"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.PrinterLineThickness);
            row["SLineThick"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.ScreenLineThickness);
            row["SelectTol"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.ScreenSelectionTolerance);
            row["SnapTol"] = Adaptor.fromInteger(model.Settings.ModelInformation.DimensionalPreferences.ScreenSnapTolerance);
            row["MergeTol"] = Adaptor.fromDouble(model.Settings.ModelInformation.DimensionalPreferences.MergeTolerance);
            row["Nudge"] = Adaptor.fromDouble(model.Settings.ModelInformation.DimensionalPreferences.NudgeDistance);
            row["FineGrid"] = Adaptor.fromDouble(model.Settings.ModelInformation.DimensionalPreferences.FineGridSpacing);
        }

        /// <summary>
        /// Sets the project information.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetPROJECT_INFORMATION(Model model, List<Dictionary<string, string>> table)
        {
            for (int i = 0; i < model.Settings.ProjectInformation.ProjectInfoItems.Count; i++)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Item", model.Settings.ProjectInformation.ProjectInfoItems[i]}
                };
                if (!string.IsNullOrEmpty(model.Settings.ProjectInformation.ProjectInfoData[i]))
                {
                    tableRow.Add("Data", model.Settings.ProjectInformation.ProjectInfoData[i]);
                }
                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the active degrees of freedom.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetACTIVE_DEGREES_OF_FREEDOM(Model model, List<Dictionary<string, string>> table)
        {
            DegreesOfFreedomGlobal dof = model.Settings.ModelInformation.ActiveDegreesOfFreedom;
            table.Add(new Dictionary<string, string>
            {
                {"UX",  Adaptor.toYesNo(dof.UX)},
                {"UY",  Adaptor.toYesNo(dof.UY)},
                {"UZ",  Adaptor.toYesNo(dof.UZ)},
                {"RX",  Adaptor.toYesNo(dof.RX)},
                {"RY",  Adaptor.toYesNo(dof.RY)},
                {"RZ",  Adaptor.toYesNo(dof.RZ)}
            }
            );
        }


        /// <summary>
        /// Sets the coordinate systems.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetCOORDINATE_SYSTEMS(Model model, List<Dictionary<string, string>> table)
        {
            SetCOORDINATE_SYSTEM(model.Settings.ModelInformation.CoordinateSystems.GlobalCoordinateSystem, table);

            foreach (CoordinateSystem coordinateSystem in model.Settings.ModelInformation.CoordinateSystems.UserCoordinateSystems)
            {
                SetCOORDINATE_SYSTEM(coordinateSystem, table);
            }
        }

        private static void SetCOORDINATE_SYSTEM(CoordinateSystem coordinateSystem, List<Dictionary<string, string>> table)
        {
            table.Add(
                new Dictionary<string, string>
                {
                    {"Name", Adaptor.ToStringEntryLimited(coordinateSystem.Name)},
                    {"Type", Adaptor.fromEnum(coordinateSystem.CoordinateType)},
                    {"X", Adaptor.fromDouble(coordinateSystem.OriginOffset.UX)},
                    {"Y", Adaptor.fromDouble(coordinateSystem.OriginOffset.UY)},
                    {"Z", Adaptor.fromDouble(coordinateSystem.OriginOffset.UZ)},
                    {"AboutX", Adaptor.fromDouble(coordinateSystem.OriginOffset.RX)},
                    {"AboutY", Adaptor.fromDouble(coordinateSystem.OriginOffset.RY)},
                    {"AboutZ", Adaptor.fromDouble(coordinateSystem.OriginOffset.RZ)}
                }
            );
        }

        /// <summary>
            /// Sets the grid lines.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <param name="table">The table.</param>
        internal static void SetGRID_LINES(Model model, List<Dictionary<string, string>> table)
        {
            SetGRID_LINES(
                model.Settings.ModelInformation.CoordinateSystems.GlobalCoordinateSystem.Name,
                model.Settings.ModelInformation.CoordinateSystems.GlobalCoordinateSystem.GridLines, 
                table);
           
            foreach (CoordinateSystem coordinateSystem in model.Settings.ModelInformation.CoordinateSystems.UserCoordinateSystems)
            {
                SetGRID_LINES(
                    coordinateSystem.Name,
                    coordinateSystem.GridLines, 
                    table);
            }
        }

        private static void SetGRID_LINES(string coordinateSystemName, GridLines gridLines, List<Dictionary<string, string>> table)
        {
            foreach (GridLine gridLine in gridLines.Lines)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"CoordSys", Adaptor.ToStringEntryLimited(coordinateSystemName)},
                    {"AxisDir", Adaptor.fromEnum(gridLine.AxisDirection)},
                    {"GridID", Adaptor.ToStringEntryLimited(gridLine.GridID)},
                    {"XRYZCoord", Adaptor.fromDouble(gridLine.Coordinate)},
                    {"LineType", Adaptor.fromEnum(gridLine.LineType)},
                    {"LineColor", Adaptor.ToStringEntryLimited(gridLine.LineColor)},
                    {"Visible", Adaptor.toYesNo(gridLine.Visible)},
                    {"BubbleLoc", Adaptor.fromEnum(gridLine.BubbleLocation)}
                };
                if (gridLine.AllVisible)
                {
                    tableRow.Add("AllVisible", Adaptor.toYesNo(gridLine.AllVisible));
                    tableRow.Add("BubbleSize", Adaptor.fromDouble(gridLine.BubbleSize));
                }
                table.Add(tableRow);
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

            row["Solver"] = Adaptor.fromEnum(model.Analysis.Analyzer.SolverType);
            row["SolverProc"] = Adaptor.fromEnum(model.Analysis.Analyzer.SolverProcessType);
            row["Force32Bit"] = Adaptor.toYesNo(model.Analysis.Analyzer.Force32BitSolver);
            row["StiffCase"] = Adaptor.ToStringEntryLimited(model.Analysis.Analyzer.StiffnessCase);
            row["GeomMod"] = Adaptor.ToStringEntryLimited(model.Analysis.Analyzer.UndeformedGeometryModificationType);
            row["HingeOpt"] = Adaptor.ToStringEntryLimited(model.Analysis.Analyzer.HingeOption);
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

            row["WaveChar"] = Adaptor.ToStringEntryLimited(autoWave.Characteristics);
            row["WaveType"] = Adaptor.ToStringEntryLimited(autoWave.WaveType);
            row["KinFactor"] = Adaptor.fromDouble(autoWave.KinematicsFactor);
            row["SWaterDepth"] = Adaptor.fromDouble(autoWave.StormWaterDepth);
            row["WaveHeight"] = Adaptor.fromDouble(autoWave.Height);
            row["WavePeriod"] = Adaptor.fromDouble(autoWave.Period);
            row["WaveTheory"] = Adaptor.ToStringEntryLimited(autoWave.Theory);
        }


        /// <summary>
        /// Sets the joint pattern definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetJOINT_PATTERN_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (string patternDefinition in model.Settings.ModelInformation.PatternDefinitions)
            {
                table.Add(new Dictionary<string, string>{{"Pattern", patternDefinition }});
            }
        }


        /// <summary>
        /// Sets the rebar sizes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetREBAR_SIZES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Bar bar in model.Components.RebarSizes)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"RebarID", bar.Name},
                        {"Area", Adaptor.fromDouble(bar.Area)},
                        {"Diameter", Adaptor.fromDouble(bar.Diameter)}
                    }
                    );
            }
        }

        /// <summary>
        /// Sets the mass source.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetMASS_SOURCE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (MassSource massSource in model.Settings.ModelInformation.MassSources)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"MassSource", Adaptor.ToStringEntryLimited(massSource.Name)}
                };

                if (massSource.IsFromElements)
                {
                    tableRow.Add("Elements", Adaptor.toYesNo(massSource.IsFromElements));
                    tableRow.Add("Masses", Adaptor.toYesNo(massSource.IsFromMasses));
                    tableRow.Add("IsDefault", Adaptor.toYesNo(massSource.IsDefault));
                    tableRow.Add("Loads", Adaptor.toYesNo(massSource.IsFromLoads));
                }
                if (massSource.LoadPatterns.Any())
                {
                    tableRow.Add("LoadPat", Adaptor.ToStringEntryLimited(massSource.LoadPatterns[0].Load.Name));
                    tableRow.Add("Multiplier", Adaptor.fromDouble(massSource.LoadPatterns[0].ScaleFactor));
                }
                table.Add(tableRow);

                if (massSource.LoadPatterns.Count() <= 1) continue;

                for (int i = 1; i < massSource.LoadPatterns.Count(); i++)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            {"LoadPat", Adaptor.ToStringEntryLimited(massSource.LoadPatterns[i].Load.Name) },
                            {"Multiplier", Adaptor.fromDouble(massSource.LoadPatterns[i].ScaleFactor) }
                        }
                    );
                }
            }
        }
    }
}
