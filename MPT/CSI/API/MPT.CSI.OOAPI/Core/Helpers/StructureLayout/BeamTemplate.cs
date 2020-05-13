// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="BeamTemplate.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Creates a new template model of a Beam.
    /// Do not use this function to add to an existing model.
    /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    /// </summary>
    public class BeamTemplate : ApiProperty
    {
        /// <summary>
        /// The number of spans for the beam.
        /// </summary>
        /// <value>The number of spans.</value>
        public int NumberOfSpans { get; set; }

        /// <summary>
        /// The length of each span. [L]
        /// </summary>
        /// <value>The length of the span.</value>
        public double SpanLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BeamTemplate" /> had restraints provided at the ends of each span..
        /// </summary>
        /// <value><c>true</c> if restraint; otherwise, <c>false</c>.</value>
        public bool AddRestraints { get; set; } = true;

        /// <summary>
        /// The frame section property used for all beams in the frame.
        /// This must either be Default or the name of a defined frame section property.
        /// </summary>
        /// <value>The beam.</value>
        public string Beam { get; set; } = "Default";


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
