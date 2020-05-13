// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-10-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-10-2019
// ***********************************************************************
// <copyright file="ReadDesignOverwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    /// <summary>
    /// Class ReadDesignOverwrites.
    /// </summary>
    internal abstract class ReadDesignOverwrites
    {
        /// <summary>
        /// The program determined
        /// </summary>
        protected const string PROGRAM_DETERMINED = "Program Determined";

        /// <summary>
        /// Gets the nullable frame section.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="value">The value.</param>
        /// <returns>FrameSection.</returns>
        protected static FrameSection getNullableFrameSection(Model model, string value)
        {
            return (PROGRAM_DETERMINED.ToLower() == value.ToLower()) ? model.Components.FrameSections[value] : null;
        }

        /// <summary>
        /// Gets the nullable yes no.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected static bool? getNullableYesNo(string value)
        {
            return (value.ToLower() == PROGRAM_DETERMINED.ToLower()) ? new bool?() : Adaptor.fromYesNo(value);
        }

        /// <summary>
        /// Gets the nullable enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the t enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>System.Nullable&lt;TEnum&gt;.</returns>
        protected static TEnum? getNullableEnum<TEnum>(string value) where TEnum : struct
        {
            return (value.ToLower() == PROGRAM_DETERMINED.ToLower()) ?
                new TEnum?() :
                Enums.EnumLibrary.ConvertStringToEnumByDescription<TEnum>(value);
        }

        /// <summary>
        /// Gets the double table item.
        /// </summary>
        /// <param name="tableRow">The table row.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Double.</returns>
        protected static double? getNullableDouble(Dictionary<string, string> tableRow, string key)
        {
            return tableRow.ContainsKey(key) ? Adaptor.toDouble(tableRow[key]) : new double?();
        }
    }
}
