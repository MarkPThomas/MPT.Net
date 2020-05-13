// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 07-06-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 07-06-2017
// ***********************************************************************
// <copyright file="IEndLengthOffset.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel
{
    /// <summary>
    /// Object has end length offsets that are gettable and settable.
    /// </summary>
    public interface IEndLengthOffset
    {
        /// <summary>
        /// Returns the frame object end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="autoOffset">True: The end length offsets are automatically determined by the program from object connectivity.</param>
        /// <param name="lengthIEnd">The offset length along the 1-axis of the frame object at the I-End of the frame object. [L]</param>
        /// <param name="lengthJEnd">The offset along the 1-axis of the frame object at the J-End of the frame object. [L]</param>
        /// <param name="rigidZoneFactor">The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.</param>
        void GetEndLengthOffset(string name,
            out bool autoOffset,
            out double lengthIEnd,
            out double lengthJEnd,
            out double rigidZoneFactor);

        /// <summary>
        /// Assigns the line element end offsets along the 1-axis of the element.
        /// </summary>
        /// <param name="name">The name of an existing frame object.</param>
        /// <param name="autoOffset">True: The end length offsets are automatically determined by the program from object connectivity.</param>
        /// <param name="lengthIEnd">The offset length along the 1-axis of the frame object at the I-End of the frame object. [L]</param>
        /// <param name="lengthJEnd">The offset along the 1-axis of the frame object at the J-End of the frame object. [L]</param>
        /// <param name="rigidZoneFactor">The rigid zone factor.
        /// This is the fraction of the end offset length assumed to be rigid for bending and shear deformations.</param>
        void SetEndLengthOffset(string name,
            bool autoOffset,
            double lengthIEnd,
            double lengthJEnd,
            double rigidZoneFactor);
    }
}