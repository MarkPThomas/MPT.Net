// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-10-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="Diaphragms.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class Diaphragms.
    /// </summary>
    /// <seealso cref="ObjectLists{Diaphragm}" />
    public class Diaphragms : ObjectLists<Diaphragm>
    {
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Diaphragms"/> class.
        /// </summary>
        internal Diaphragms() 
        {
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Diaphragm fillNewItem(string uniqueName)
        {
            return Diaphragm.Factory(uniqueName);
        }
        #endregion
    }
}