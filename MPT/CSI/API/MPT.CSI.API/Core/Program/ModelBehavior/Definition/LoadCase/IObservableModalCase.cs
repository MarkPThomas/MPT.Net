// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-20-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IObservableModalCase.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Load case can get modal case used.
    /// </summary>
    public interface IObservableModalCase
    {
        /// <summary>
        /// Returns the modal case assigned to the specified load case.
        /// This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the specified load case are based.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        string GetModalCase(string name);
    }
}