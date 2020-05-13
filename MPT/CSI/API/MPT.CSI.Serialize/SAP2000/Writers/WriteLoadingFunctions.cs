using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions;
using MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions.ResponseSpectrum;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteLoadingFunctions
    {
        internal static void DefineLoadingFunctions(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_POWER_SPECTRAL_DENSITY_USER, SetFUNCTION_POWER_SPECTRAL_DENSITY_USER);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_STEADY_STATE_USER, SetFUNCTION_STEADY_STATE_USER);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_COSINE, SetFUNCTION_TIME_HISTORY_COSINE);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_RAMP, SetFUNCTION_TIME_HISTORY_RAMP);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_SAWTOOTH, SetFUNCTION_TIME_HISTORY_SAWTOOTH);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_SINE, SetFUNCTION_TIME_HISTORY_SINE);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_TRIANGULAR, SetFUNCTION_TIME_HISTORY_TRIANGULAR);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_TIME_HISTORY_USER, SetFUNCTION_TIME_HISTORY_USER);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_RESPONSE_SPECTRUM_USER, SetFUNCTION_RESPONSE_SPECTRUM_USER);
            writer.WriteSingleTable(SAP2000Tables.FUNCTION_RESPONSE_SPECTRUM_UBC97, SetFUNCTION_RESPONSE_SPECTRUM_UBC97);
        }

        private static void setFUNCTION_USER<T1, T2>(
            Model model, 
            List<Dictionary<string, string>> table,
            string xAxisLabel,
            string yAxisLabel,
            Action<Dictionary<string, string>, T1> action = null) 
            where T1: Function, IFunctionCurve<T2>
            where T2: FunctionPoint
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is T1 validFunction)) continue;
                foreach (T2 point in validFunction.FunctionCurve)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name)},
                        {xAxisLabel, Adaptor.fromDouble(point.X)},
                        {yAxisLabel, Adaptor.fromDouble(point.Y)}
                    };
                    action?.Invoke(tableRow, validFunction);
                    table.Add(tableRow);
                }
            }
        }

        /// <summary>
        /// Sets the function power spectral density user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_POWER_SPECTRAL_DENSITY_USER(Model model, List<Dictionary<string, string>> table)
        {
            setFUNCTION_USER<PowerSpectralDensityFunction, FrequencyValuePoint>(model, table, "Frequency", "Value");
        }

        #region Response Spectrum
        /// <summary>
        /// Sets the function response spectrum user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_RESPONSE_SPECTRUM_USER(Model model, List<Dictionary<string, string>> table)
        {
            setFUNCTION_USER<ResponseSpectrumUserFunction, PeriodAccelerationPoint>(
                model, table, 
                "Period", "Accel",
                (p, q) => { if (q.FunctionDampingRatio > 0) p["FuncDamp"] = Adaptor.fromDouble(q.FunctionDampingRatio); }
                );
        }

        /// <summary>
        /// Sets the function response spectrum ub C97.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_RESPONSE_SPECTRUM_UBC97(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is ResponseSpectrumCodeFunctions<UBC97SpectrumProperties> validFunction)) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name) },
                        {"FuncDamp", Adaptor.fromDouble(validFunction.FunctionDampingRatio) },
                        {"Ca", Adaptor.fromDouble(validFunction.CodeProperties.Ca) },
                        {"Cv", Adaptor.fromDouble(validFunction.CodeProperties.Cv) }
                    }
                );
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
            setFUNCTION_USER<SteadyStateFunction, FrequencyValuePoint>(model, table, "Frequency", "Value");
        }

        #region Time History
        /// <summary>
        /// Sets the function time history cosine.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_COSINE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is TimeHistoryCosineFunction validFunction)) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name) },
                        {"Period", Adaptor.fromDouble(validFunction.Period) },
                        {"Amplitude", Adaptor.fromDouble(validFunction.Amplitude) },
                        {"StepsPerCyc", Adaptor.fromDouble(validFunction.StepsPerCycle) },
                        {"NumCycles", Adaptor.fromDouble(validFunction.NumberOfCycles) }
                    }
                );
            }
        }


        /// <summary>
        /// Sets the function time history ramp.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_RAMP(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is TimeHistoryRampFunction validFunction)) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name) },
                        {"Amplitude", Adaptor.fromDouble(validFunction.Amplitude) },
                        {"RampTime", Adaptor.fromDouble(validFunction.RampTime) },
                        {"MaxTime", Adaptor.fromDouble(validFunction.MaxTime) }
                    }
                );
            }
        }


        /// <summary>
        /// Sets the function time history sawtooth.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_SAWTOOTH(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is TimeHistorySawtoothFunction validFunction)) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name) },
                        {"Period", Adaptor.fromDouble(validFunction.Period) },
                        {"Amplitude", Adaptor.fromDouble(validFunction.Amplitude) },
                        {"RampTime", Adaptor.fromDouble(validFunction.RampTime) },
                        {"NumCycles", Adaptor.fromDouble(validFunction.NumberOfCycles) }
                    }
                );
            }
        }


        /// <summary>
        /// Sets the function time history sine.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_SINE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is TimeHistorySineFunction validFunction)) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name) },
                        {"Period", Adaptor.fromDouble(validFunction.Period) },
                        {"Amplitude", Adaptor.fromDouble(validFunction.Amplitude) },
                        {"StepsPerCyc", Adaptor.fromDouble(validFunction.StepsPerCycle) },
                        {"NumCycles", Adaptor.fromDouble(validFunction.NumberOfCycles) }
                    }
                );
            }
        }


        /// <summary>
        /// Sets the function time history triangular.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_TRIANGULAR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Function function in model.Loading.Functions)
            {
                if (!(function is TimeHistoryTriangularFunction validFunction)) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Name", Adaptor.ToStringEntryLimited(validFunction.Name) },
                        {"Period", Adaptor.fromDouble(validFunction.Period) },
                        {"Amplitude", Adaptor.fromDouble(validFunction.Amplitude) },
                        {"NumCycles", Adaptor.fromDouble(validFunction.NumberOfCycles) }
                    }
                );
            }
        }


        /// <summary>
        /// Sets the function time history user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetFUNCTION_TIME_HISTORY_USER(Model model, List<Dictionary<string, string>> table)
        {
            setFUNCTION_USER<TimeHistoryUserFunction, TimeValuePoint>(model, table, "Time", "Value");
        }
        #endregion
    }
}
