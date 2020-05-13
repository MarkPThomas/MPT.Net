// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="FileData.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace MPT.CSI.Serialize.Models
{
    /// <summary>
    /// Class FileData.
    /// </summary>
    public class FileData 
    {
        #region Properties

        /// <summary>
        /// Filename of the current model file, with or without the full path.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; internal set; }
    

        /// <summary>
        /// Path to the current model file.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; internal set; }

        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="FileData"/> class.
        /// </summary>
        internal FileData() 
        {
        }

        #endregion
    }
}
