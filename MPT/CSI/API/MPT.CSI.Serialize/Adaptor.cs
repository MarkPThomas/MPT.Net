using System.Globalization;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;

namespace MPT.CSI.Serialize
{
    public abstract class Adaptor
    {
        /// <summary>
        /// The program determined
        /// </summary>
        protected const string PROGRAM_DETERMINED = "Program Determined";

        public static string ConvertValue(object value)
        {
            switch (value)
            {
                case string valueString:
                    return ToStringEntryLimited(valueString);
                case int valueInt:
                    return fromInteger(valueInt);
                case double valueDouble:
                    return fromDouble(valueDouble);
                case bool valueBool:
                    return toYesNo(valueBool);
                case null:
                    return ToStringEntryLimited(PROGRAM_DETERMINED);
            }

            return "Error";
        }

        /// <summary>
        /// Returns value as a string surrounded by quotes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToStringEntry(string value)
        {
            value = value.Trim();
            return inQuotes(value);
        }

        /// <summary>
        /// Returns the value as a string surrounded by quotes only if there is at least one space in the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string ToStringEntryLimited(string value)
        {
            value = value.Trim();
            return value.Contains(" ") ? inQuotes(value) : value;
        }

        public static string inQuotes(string value)
        {
            return '"' + value + '"';
        }


        public static bool fromYesNo(string value)
        {
            return (string.CompareOrdinal(value.ToLower(), "yes") == 0);
        }

        /// <summary>
        /// Gets the nullable yes no.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool? fromNullableYesNo(string value)
        {
            return (value.ToLower() == PROGRAM_DETERMINED.ToLower()) ? new bool?() : fromYesNo(value);
        }

        public static string toYesNo(bool value)
        {
            return value ? "Yes" : "No";
        }

        public static string toNullableYesNo(bool? value)
        {
            return (value.HasValue) ? toYesNo(value.Value) : PROGRAM_DETERMINED;
        }

        public static TEnum toEnum<TEnum>(string value) where TEnum : struct
        {
            return Enums.EnumLibrary.ConvertStringToEnumByDescription<TEnum>(value);
        }

        /// <summary>
        /// Gets the nullable enum.
        /// </summary>
        /// <typeparam name="TEnum">The type of the t enum.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>System.Nullable&lt;TEnum&gt;.</returns>
        public static TEnum? toNullableEnum<TEnum>(string value) where TEnum : struct
        {
            return (value.ToLower() == PROGRAM_DETERMINED.ToLower())
                ? new TEnum?() 
                : Enums.EnumLibrary.ConvertStringToEnumByDescription<TEnum>(value);
        }

        public static string fromEnum<TEnum>(TEnum value, bool limitedQuotes = true) where TEnum : struct
        {
            return limitedQuotes
                    ? ToStringEntryLimited(Enums.EnumLibrary.GetEnumDescription(value))
                    : ToStringEntry(Enums.EnumLibrary.GetEnumDescription(value));
        }

        public static string fromNullableEnum<TEnum>(TEnum? value, bool limitedQuotes = true) where TEnum : struct
        {
            return (value.HasValue) 
                ? (limitedQuotes 
                    ? ToStringEntryLimited(Enums.EnumLibrary.GetEnumDescription(value)) 
                    : ToStringEntry(Enums.EnumLibrary.GetEnumDescription(value))) 
                : PROGRAM_DETERMINED;
        }

        /// <summary>
        /// Gets the nullable frame section.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="value">The value.</param>
        /// <returns>FrameSection.</returns>
        public static FrameSection toNullableFrameSection(Model model, string value)
        {
            return (PROGRAM_DETERMINED.ToLower() == value.ToLower()) ? model.Components.FrameSections[value] : null;
        }

        public static string fromNullableFrameSection(Model model, FrameSection value)
        {
            return (value == null) ? PROGRAM_DETERMINED : value.Name;
        }


        public static double toDouble(string value)
        {
            double.TryParse(value, out var numericValue);
            return numericValue;
        }

        public static string fromDouble(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }


        public static int toInteger(string value)
        {
            int.TryParse(value, out var numericValue);
            return numericValue;
        }

        public static string fromInteger(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
