using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteTableSetsExports
    {
        internal static void DefineTableSets(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.NAMED_SETS_DATABASE_TABLES_1_GENERAL, setNAMED_SETS_DATABASE_TABLES_1_GENERAL);
            writer.WriteSingleTable(SAP2000Tables.NAMED_SETS_DATABASE_TABLES_2_SELECTIONS, setNAMED_SETS_DATABASE_TABLES_2_SELECTIONS);
        }

        internal static void SetTableExports(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.TABLES_AUTOMATICALLY_SAVED_AFTER_ANALYSIS, setTABLES_AUTOMATICALLY_SAVED_AFTER_ANALYSIS);
        }

        /// <summary>
        /// Sets the named sets database tables 1 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setNAMED_SETS_DATABASE_TABLES_1_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (TableSet tableSet in model.OutputSettings)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"DBNamedSet", Adaptor.ToStringEntryLimited(tableSet.Name)},
                    {"SortOrder", Adaptor.ToStringEntryLimited(tableSet.SortOrder)},
                    {"Unformatted", Adaptor.toYesNo(tableSet.IsUnformatted)},
                    {"ModeStart", Adaptor.fromInteger(tableSet.ModeStart)},
                    {"ModeEnd", tableSet.AllModesUsed ? "All" : Adaptor.fromInteger(tableSet.ModeEnd)},
                    {"BaseReacX", Adaptor.fromDouble(tableSet.BaseReactionXCoordinate)},
                    {"BaseReacY", Adaptor.fromDouble(tableSet.BaseReactionYCoordinate)},
                    {"BaseReacZ", Adaptor.fromDouble(tableSet.BaseReactionZCoordinate)},
                    {"ModalHist", Adaptor.fromEnum(tableSet.ModalHistoryOutput)},
                    {"DirectHist", Adaptor.fromEnum(tableSet.DirectIntegrationHistoryOutput)},
                    {"NLStatic", Adaptor.fromEnum(tableSet.NonlinearStaticOutput)},
                    {"Multistep", Adaptor.fromEnum(tableSet.MultiStepStaticOutput)},
                    {"Combo", Adaptor.fromEnum(tableSet.LoadCombinationOutput)},
                    {"Steady", Adaptor.ToStringEntryLimited(tableSet.SteadyStateOutput)},
                    {"SteadyOpt", Adaptor.ToStringEntryLimited(tableSet.SteadyStateOutputOption)},
                    {"PSD", Adaptor.ToStringEntryLimited(tableSet.PowerSpectralDensityOption)},
                };

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the named sets database tables 2 selections.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setNAMED_SETS_DATABASE_TABLES_2_SELECTIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (TableSet tableSet in model.OutputSettings)
            {
                foreach (string tableName in tableSet.TableNames)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "DBNamedSet", Adaptor.ToStringEntryLimited(tableSet.Name) },
                            { "SelectType", "Table" },
                            { "Selection", Adaptor.ToStringEntryLimited(tableName) }
                        });
                }

                foreach (LoadPattern loadPattern in tableSet.LoadPatterns)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "DBNamedSet", Adaptor.ToStringEntryLimited(tableSet.Name) },
                            { "SelectType", "LoadPattern" },
                            { "Selection", Adaptor.ToStringEntryLimited(loadPattern.Name) }
                        });
                }

                foreach (LoadCase loadCase in tableSet.LoadCases)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "DBNamedSet", Adaptor.ToStringEntryLimited(tableSet.Name) },
                            { "SelectType", "LoadCase" },
                            { "Selection", Adaptor.ToStringEntryLimited(loadCase.Name) }
                        });
                }

                foreach (LoadCombination loadCombination in tableSet.LoadCombinations)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "DBNamedSet", Adaptor.ToStringEntryLimited(tableSet.Name) },
                            { "SelectType", "Combo" },
                            { "Selection", Adaptor.ToStringEntryLimited(loadCombination.Name) }
                        });
                }
            }
        }


        /// <summary>
        /// Sets the tables automatically saved after analysis.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTABLES_AUTOMATICALLY_SAVED_AFTER_ANALYSIS(Model model, List<Dictionary<string, string>> table)
        {
            table.Add(
                new Dictionary<string, string>
                {
                    {"SaveFile",  Adaptor.toYesNo(model.OutputSettings.SaveFile)},
                    {"FileName",  Adaptor.ToStringEntryLimited(model.OutputSettings.FileName)},
                    {"NamedSet",  Adaptor.ToStringEntryLimited(model.OutputSettings.NamedSet.Name)},
                    {"Group",  Adaptor.ToStringEntryLimited(model.OutputSettings.Group.Name)},
                });
        }
    }
}
