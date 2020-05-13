// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="IFill.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.OOAPI.Core.Support
{
    /// <summary>
    /// Object has data set from the application by using the API.
    /// </summary>
    public interface IFill
    {
        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        void FillData();
    }
}
