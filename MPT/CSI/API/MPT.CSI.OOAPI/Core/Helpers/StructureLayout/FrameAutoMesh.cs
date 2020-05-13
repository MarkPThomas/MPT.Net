// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameAutoMesh.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Class FrameAutoMesh.
    /// </summary>
    public class FrameAutoMesh : ApiProperty
    {
        /// <summary>
        /// True: The frame object automatically meshed by the program when the analysis model is created.
        /// </summary>
        /// <value><c>true</c> if this instance is automatic meshed; otherwise, <c>false</c>.</value>
        public bool IsAutoMeshed { get; set; }

        /// <summary>
        /// True: The frame object is automatically meshed at intermediate joints along its length.
        /// This item is applicable only when <see cref="IsAutoMeshed" /> = True.
        /// </summary>
        /// <value><c>true</c> if this instance is automatic meshed at points; otherwise, <c>false</c>.</value>
        public bool IsAutoMeshedAtPoints { get; set; }

        /// <summary>
        /// True: The frame object is automatically meshed at intersections with other frames, area object edges and solid object edges.
        /// This item is applicable only when <see cref="IsAutoMeshed" /> = True.
        /// </summary>
        /// <value><c>true</c> if this instance is automatic meshed at lines; otherwise, <c>false</c>.</value>
        public bool IsAutoMeshedAtLines { get; set; }

        /// <summary>
        /// The minimum number of elements into which the frame object is automatically meshed.
        /// If this item is zero, the number of elements is not checked when the automatic meshing is done.
        /// This item is applicable only when <see cref="IsAutoMeshed" /> = True.
        /// </summary>
        /// <value>The minimum element number.</value>
        public int MinElementNumber { get; set; }

        /// <summary>
        /// The maximum length of auto meshed frame elements.
        /// If this item is zero, the element length is not checked when the automatic meshing is done. [L]
        /// This item is applicable only when <see cref="IsAutoMeshed" /> = True.
        /// </summary>
        /// <value>The maximum length of the automatic mesh.</value>
        public double AutoMeshMaxLength { get; set; }


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
#endif