// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-10-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-10-2019
// ***********************************************************************
// <copyright file="WriteDesignOverwrites.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    /// <summary>
    /// Class WriteDesignOverwrites.
    /// </summary>
    internal abstract class WriteDesignOverwrites
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
        protected static string getNullableFrameSection(FrameSection value)
        {
            return (value == null) ? PROGRAM_DETERMINED : value.Name;
        }

        /// <summary>
        /// Gets the nullable yes no.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected static string getNullableYesNo(bool? value)
        {
            return (value.HasValue) ? Adaptor.toYesNo(value.Value) : PROGRAM_DETERMINED;
        }

        /// <summary>
        /// Gets the nullable enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the t enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>System.Nullable&lt;TEnum&gt;.</returns>
        protected static string getNullableEnum<TEnum>(TEnum? value) where TEnum : struct
        {
            return (value.HasValue)
                ? Enums.EnumLibrary.GetEnumDescription(value.Value)
                : PROGRAM_DETERMINED;
                ;
        }
    }
}
