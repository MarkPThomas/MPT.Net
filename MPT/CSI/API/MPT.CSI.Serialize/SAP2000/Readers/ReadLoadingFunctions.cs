using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.ResponseSpectrum;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadLoadingFunctions
    {
        internal static void DefineLoadingFunctions(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_POWER_SPECTRAL_DENSITY_USER, SetFUNCTION_POWER_SPECTRAL_DENSITY_USER);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_STEADY_STATE_USER, SetFUNCTION_STEADY_STATE_USER);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_COSINE, SetFUNCTION_TIME_HISTORY_COSINE);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_RAMP, SetFUNCTION_TIME_HISTORY_RAMP);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_SAWTOOTH, SetFUNCTION_TIME_HISTORY_SAWTOOTH);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_SINE, SetFUNCTION_TIME_HISTORY_SINE);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_TRIANGULAR, SetFUNCTION_TIME_HISTORY_TRIANGULAR);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_USER, SetFUNCTION_TIME_HISTORY_USER);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_RESPONSE_SPECTRUM_USER, SetFUNCTION_RESPONSE_SPECTRUM_USER);
            reader.ReadSingleTable(SAP2000Tables.FUNCTION_RESPONSE_SPECTRUM_UBC97, SetFUNCTION_RESPONSE_SPECTRUM_UBC97);
        }

        /// <summary>
            /// Sets the function power spectral density user.
            /// </summary>
            /// <param name="model">The model.</param>
            /// <param name="table">The table.</param>
            internal static void SetFUNCTION_POWER_SPECTRAL_DENSITY_USER(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.PowerSpectralDensityUser;
            foreach (Dictionary<string, string> tableRow in table)
            {
                PowerSpectralDensityFunction function = (PowerSpectralDensityFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                FrequencyValuePoint point = new FrequencyValuePoint(
                    Adaptor.toDouble(tableRow["Frequency"]),
                    Adaptor.toDouble(tableRow["Value"])
                );
                function.FunctionCurve.Add(point);
            }
        }

        #region Response Spectrum
        /// <summary>
        /// Sets the function response spectrum user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_RESPONSE_SPECTRUM_USER(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.ResponseSpectrumUser;
            foreach (Dictionary<string, string> tableRow in table)
            {
                ResponseSpectrumUserFunction function = (ResponseSpectrumUserFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                if (tableRow.ContainsKey("FuncDamp")) function.FunctionDampingRatio = Adaptor.toDouble(tableRow["FuncDamp"]);

                PeriodAccelerationPoint point = new PeriodAccelerationPoint(
                    Adaptor.toDouble(tableRow["Period"]),
                    Adaptor.toDouble(tableRow["Accel"])
                    );
                function.FunctionCurve.Add(point);
            }
        }


        /// <summary>
        /// Sets the function response spectrum ub C97.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_RESPONSE_SPECTRUM_UBC97(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.ResponseSpectrumUser;
            foreach (Dictionary<string, string> tableRow in table)
            {
                ResponseSpectrumCodeFunctions<UBC97SpectrumProperties> function =
                    (ResponseSpectrumCodeFunctions<UBC97SpectrumProperties>)model.Loading.Functions.FillItem(tableRow["Name"]);
                function.FunctionDampingRatio = Adaptor.toDouble(tableRow["FuncDamp"]);
                function.CodeProperties.Ca = Adaptor.toDouble(tableRow["Ca"]);
                function.CodeProperties.Cv = Adaptor.toDouble(tableRow["Cv"]);
            }
        }
        #endregion

        /// <summary>
        /// Sets the function steady state user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_STEADY_STATE_USER(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.SteadyStateUser;
            foreach (Dictionary<string, string> tableRow in table)
            {
                SteadyStateFunction function = (SteadyStateFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                FrequencyValuePoint point = new FrequencyValuePoint(
                    Adaptor.toDouble(tableRow["Frequency"]),
                    Adaptor.toDouble(tableRow["Value"])
                );
                function.FunctionCurve.Add(point);
            }
        }

        #region Time History
        /// <summary>
        /// Sets the function time history cosine.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_COSINE(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.TimeHistoryCosine;
            foreach (Dictionary<string, string> tableRow in table)
            {
                TimeHistoryCosineFunction function = (TimeHistoryCosineFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                function.Period = Adaptor.toDouble(tableRow["Period"]);
                function.Amplitude = Adaptor.toDouble(tableRow["Amplitude"]);
                function.StepsPerCycle = Adaptor.toDouble(tableRow["StepsPerCyc"]);
                function.NumberOfCycles = Adaptor.toInteger(tableRow["NumCycles"]);
            }
        }


        /// <summary>
        /// Sets the function time history ramp.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_RAMP(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.TimeHistoryRamp;
            foreach (Dictionary<string, string> tableRow in table)
            {
                TimeHistoryRampFunction function = (TimeHistoryRampFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                function.Amplitude = Adaptor.toDouble(tableRow["Amplitude"]);
                function.RampTime = Adaptor.toDouble(tableRow["RampTime"]);
                function.MaxTime = Adaptor.toInteger(tableRow["MaxTime"]);
            }
        }


        /// <summary>
        /// Sets the function time history sawtooth.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_SAWTOOTH(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.TimeHistorySawtooth;
            foreach (Dictionary<string, string> tableRow in table)
            {
                TimeHistorySawtoothFunction function = (TimeHistorySawtoothFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                function.Period = Adaptor.toDouble(tableRow["Period"]);
                function.Amplitude = Adaptor.toDouble(tableRow["Amplitude"]);
                function.RampTime = Adaptor.toDouble(tableRow["RampTime"]);
                function.NumberOfCycles = Adaptor.toInteger(tableRow["NumCycles"]);
            }
        }


        /// <summary>
        /// Sets the function time history sine.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_SINE(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.TimeHistorySine;
            foreach (Dictionary<string, string> tableRow in table)
            {
                TimeHistorySineFunction function = (TimeHistorySineFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                function.Period = Adaptor.toDouble(tableRow["Period"]);
                function.Amplitude = Adaptor.toDouble(tableRow["Amplitude"]);
                function.StepsPerCycle = Adaptor.toDouble(tableRow["StepsPerCyc"]);
                function.NumberOfCycles = Adaptor.toInteger(tableRow["NumCycles"]);
            }
        }


        /// <summary>
        /// Sets the function time history triangular.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_TRIANGULAR(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.TimeHistoryTriangular;
            foreach (Dictionary<string, string> tableRow in table)
            {
                TimeHistoryTriangularFunction function = (TimeHistoryTriangularFunction)model.Loading.Functions.FillItem(tableRow["Name"]);
                function.Period = Adaptor.toDouble(tableRow["Period"]);
                function.Amplitude = Adaptor.toDouble(tableRow["Amplitude"]);
                function.NumberOfCycles = Adaptor.toInteger(tableRow["NumCycles"]);
            }
        }


        /// <summary>
        /// Sets the function time history user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_USER(Model model, List<Dictionary<string, string>> table)
        {
            Functions.FunctionType = eFunctionTypes.TimeHistoryUser;
            foreach (Dictionary<string, string> tableRow in table)
            {
                TimeHistoryUserFunction function = (TimeHistoryUserFunction)model.Loading.Functions.FillItem(tableRow["Name"]);

                TimeValuePoint point = new TimeValuePoint(
                    Adaptor.toDouble(tableRow["Time"]),
                    Adaptor.toDouble(tableRow["Value"])
                );
                function.FunctionCurve.Add(point);
            }
        }
        #endregion
    }
}
