// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="FrameFireProofing.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Class FrameFireProofing.
    /// </summary>
    public class FrameFireProofing : ApiProperty
    {
        /// <summary>
        /// Type of fireproofing assigned.
        /// </summary>
        /// <value>The type of the fire proofing.</value>
        public eFireProofing FireProofingType { get; set; }

        /// <summary>
        /// When <see cref="FireProofingType" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.SprayedOnUserPerimeterDefine" /> this is the thickness of the sprayed on fireproofing.
        /// When <see cref="FireProofingType" /> = <see cref="eFireProofing.ConcreteEncased" /> this is the concrete cover dimension. [L]
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }

        /// <summary>
        /// This item applies only when <see cref="FireProofingType" /> = <see cref="eFireProofing.SprayedOnUserPerimeterDefine" />.
        /// It is the length of fireproofing applied measured around the perimeter of the frame object cross-section. [L]
        /// </summary>
        /// <value>The perimeter.</value>
        public double Perimeter { get; set; }

        /// <summary>
        /// This is the weight per unit volume of the fireproofing material. [F/L^3]
        /// </summary>
        /// <value>The density.</value>
        public double Density { get; set; }

        /// <summary>
        /// True: Fireproofing is assumed to be applied to the top flange of the section.
        /// False: Program assumes no fireproofing is applied to the section top flange.
        /// This flag applies for I, channel and double channel sections only when <see cref="FireProofingType" /> = <see cref="eFireProofing.SprayedOnProgramPerimeterCalc" /> or <see cref="eFireProofing.ConcreteEncased" />.
        /// </summary>
        /// <value><c>true</c> if [applied to top flange]; otherwise, <c>false</c>.</value>
        public bool AppliedToTopFlange { get; set; }

        /// <summary>
        /// True: Fireproofing is included in the structure self weight.
        /// </summary>
        /// <value><c>true</c> if [include in self weight]; otherwise, <c>false</c>.</value>
        public bool IncludeInSelfWeight { get; set; }

        /// <summary>
        /// True: Fireproofing is included gravity loads applied in the X, Y and Z directions.
        /// </summary>
        /// <value><c>true</c> if [include in gravity loads]; otherwise, <c>false</c>.</value>
        public bool IncludeInGravityLoads { get; set; }

        /// <summary>
        /// This item is either None or the name of an existing load pattern.
        /// If it is the name of a load pattern then the weight of the fireproofing is applied as a distributed load in the global Z direction in the load pattern.
        /// </summary>
        /// <value>The included load pattern.</value>
        public string IncludedLoadPattern { get; set; }


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