// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="ModalLoadParticipationResultsIdentifier.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.Enums;

namespace MPT.CSI.Serialize.Models.Helpers.Results
{
    /// <summary>
    /// Class ModalLoadParticipationResultsIdentifier.
    /// </summary>
    /// <seealso cref="ResultsIdentifier" />
    public class ModalLoadParticipationResultsIdentifier : ResultsIdentifier
    {
        /// <summary>
        /// The type of item for which the modal load participation is reported.
        /// </summary>
        /// <value>The type of the item.</value>
        public eModalLoadItemType ItemType { get; set; }

        /// <summary>
        /// Load Pattern, Acceleration, Link or Panel Zone.
        /// It specifies the type of item for which the modal load participation is reported.
        /// </summary>
        /// <value>The type of the item.</value>
        public string ItemTypeName => EnumLibrary.GetEnumDescription(ItemType);


        /// <summary>
        /// This is an array whose values depend on the ItemType.
        /// If the <see cref="ItemType" /> = <see cref="eModalLoadItemType.LoadPattern"/>, this is the name of the load pattern.
        /// If the <see cref="ItemType" /> = <see cref="eModalLoadItemType.Acceleration"/>, this is UX, UY, UZ, RX, RY, or RZ, indicating the acceleration direction.
        /// If the <see cref="ItemType" /> = <see cref="eModalLoadItemType.Link"/>, this is the name of the link followed by U1, U2, U3, R1, R2, or R3(in parenthesis), indicating the link degree of freedom for which the output is reported.
        /// If the <see cref="ItemType" /> = <see cref="eModalLoadItemType.PanelZone"/>, this is the name of the joint to which the panel zone is assigned, followed by U1, U2, U3, R1, R2, or R3(in parenthesis), indicating the degree of freedom for which the output is reported.
        /// </summary>
        /// <value>The item.</value>
        public string Item { get; set; }


        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
