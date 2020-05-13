using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers.ProjectSettings;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadTableSetsExports
    {
        internal static void DefineTableSets(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.NAMED_SETS_DATABASE_TABLES_1_GENERAL, setNAMED_SETS_DATABASE_TABLES_1_GENERAL);
            reader.ReadSingleTable(SAP2000Tables.NAMED_SETS_DATABASE_TABLES_2_SELECTIONS, setNAMED_SETS_DATABASE_TABLES_2_SELECTIONS);
        }

        internal static void SetTableExports(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.TABLES_AUTOMATICALLY_SAVED_AFTER_ANALYSIS, setTABLES_AUTOMATICALLY_SAVED_AFTER_ANALYSIS);
        }

        /// <summary>
        /// Sets the named sets database tables 1 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setNAMED_SETS_DATABASE_TABLES_1_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                TableSet tableSet = model.OutputSettings.FillItem(tableRow["DBNamedSet"]);
                tableSet.SortOrder = tableRow["SortOrder"];
                tableSet.IsUnformatted = Adaptor.fromYesNo(tableRow["Unformatted"]);
                tableSet.ModeStart = Adaptor.toInteger(tableRow["ModeStart"]);
                string modeEnd = tableRow["ModeEnd"];
                if (modeEnd.ToUpper() == "ALL")
                {
                    tableSet.AllModesUsed = true;
                }
                else
                {
                    tableSet.AllModesUsed = false;
                    tableSet.ModeEnd = Adaptor.toInteger(modeEnd);
                }

                tableSet.BaseReactionXCoordinate = Adaptor.toDouble(tableRow["BaseReacX"]);
                tableSet.BaseReactionYCoordinate = Adaptor.toDouble(tableRow["BaseReacY"]);
                tableSet.BaseReactionZCoordinate = Adaptor.toDouble(tableRow["BaseReacZ"]);
                tableSet.ModalHistoryOutput = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiStepResultsOutput>(tableRow["ModalHist"]);
                tableSet.DirectIntegrationHistoryOutput = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiStepResultsOutput>(tableRow["DirectHist"]);
                tableSet.NonlinearStaticOutput = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiStepResultsOutput>(tableRow["NLStatic"]);
                tableSet.MultiStepStaticOutput = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiStepResultsOutput>(tableRow["Multistep"]);
                tableSet.LoadCombinationOutput = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadCombinationOutput>(tableRow["Combo"]);
                tableSet.SteadyStateOutput = tableRow["Steady"];
                tableSet.SteadyStateOutputOption = tableRow["SteadyOpt"];
                tableSet.PowerSpectralDensityOption = tableRow["PSD"];
            }
        }


        /// <summary>
        /// Sets the named sets database tables 2 selections.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setNAMED_SETS_DATABASE_TABLES_2_SELECTIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                TableSet tableSet = model.OutputSettings.FillItem(tableRow["DBNamedSet"]);
                switch (tableRow["SelectType"])
                {
                    case "Table":
                        tableSet.TableNames.Add(tableRow["Selection"]);
                        break;
                    case "LoadPattern":
                        LoadPattern loadPattern = model.Loading.Patterns.FillItem(tableRow["Selection"]);
                        tableSet.LoadPatterns.Add(loadPattern);
                        break;
                    case "LoadCase":
                        LoadCase loadCase = model.Loading.Cases.FillItem(tableRow["Selection"]);
                        tableSet.LoadCases.Add(loadCase);
                        break;
                    case "Combo":
                        LoadCombination loadCombo = model.Loading.Combinations.FillItem(tableRow["Selection"]);
                        tableSet.LoadCombinations.Add(loadCombo);
                        break;
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
            Dictionary<string, string> row = table[0];
            model.OutputSettings.SaveFile = Adaptor.fromYesNo(row["SaveFile"]);
            model.OutputSettings.FileName = row["FileName"];
            model.OutputSettings.NamedSet = model.OutputSettings.FillItem(row["NamedSet"]);
            model.OutputSettings.Group = model.Groupings.Groups.FillItem(row["Group"]);
        }
    }
}
