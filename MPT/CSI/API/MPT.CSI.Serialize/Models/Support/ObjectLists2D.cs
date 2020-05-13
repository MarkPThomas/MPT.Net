// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-11-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="ObjectLists2D.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.Models.Support
{
    /// <summary>
    /// Class ObjectLists2D.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU">The type of the tu.</typeparam>
    /// <seealso cref="Serialize.Models.Support.ObjectLists{T}" />
    public abstract class ObjectLists2D<T,TU> :ObjectLists<T> 
        where T : StructureObjectWithCrossSection<TU>
        where TU : CrossSection
    {
        #region Fields & Properties

        /// <summary>
        /// The components properties
        /// </summary>
        internal StructureComponentsProperties<TU> ComponentsProperties;
        #endregion


        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectLists2D{T, K}" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        internal ObjectLists2D(StructureComponentsProperties<TU> componentsProperties) 
        {
            ComponentsProperties = componentsProperties;
        }
        #endregion
    }
}
