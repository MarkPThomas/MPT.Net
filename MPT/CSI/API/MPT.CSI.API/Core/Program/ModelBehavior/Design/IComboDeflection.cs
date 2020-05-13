// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-03-2017
//
// Last Modified By : Mark
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IComboDeflection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements an interface for using load combinations for deflection-based design criteria.
    /// </summary>
    public interface IComboDeflection
    {
        // ===        
        /// <summary>
        /// Gets the names of all load combinations used for deflection design.
        /// </summary>
        string[] GetComboDeflection();

        /// <summary>
        /// Selects or deselects a load combination for deflection design.
        /// </summary>
        /// <param name="nameLoadCombination">Name of an existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design.
        /// False: The combination is not selected for deflection design.</param>
        void SetComboDeflection(string nameLoadCombination, bool selectLoadCombination);

    }
}