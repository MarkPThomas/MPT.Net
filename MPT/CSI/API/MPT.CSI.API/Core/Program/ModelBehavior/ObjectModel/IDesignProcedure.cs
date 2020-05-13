// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="IDesignProcedure.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has gettable/settable design procedures.
    /// </summary>
    public interface IDesignProcedure
    {
        /// <summary>
        /// Returns the design procedure for a frame object, based on the material.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        eDesignProcedureType GetDesignProcedure(string name);

        /// <summary>
        /// Sets the design procedure for frame objects.
        /// </summary>
        /// <param name="name">The name of an existing object or group, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="designProcedure">Design procedure type desired for the specified frame object.</param>
        /// <param name="itemType">If this item is <see cref="eItemType.Object" />, the assignments are made for the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.Group" />, the assignments are made for the objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemType.SelectedObjects" />, the assignments are made for all selected objects, and the <paramref name="name" /> item is ignored.</param>
        void SetDesignProcedure(string name,
            eDesignProcedure designProcedure,
            eItemType itemType = eItemType.Object);
    }
}