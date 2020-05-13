// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="WallTemplate.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_SAP2000v19 || BUILD_SAP2000v20
namespace MPT.CSI.OOAPI.Core.Helpers.StructureLayout
{
    /// <summary>
    /// Creates a new template model of a Wall.
    /// Do not use this function to add to an existing model.
    /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
    /// </summary>
    public class WallTemplate : ApiProperty
    {
        /// <summary>
        /// The number of area objects in the global X direction of the wall.
        /// </summary>
        /// <value>The number of x divisions.</value>
        public int NumberOfXDivisions { get; set; }

        /// <summary>
        /// The number of area objects in the global Z direction of the wall.
        /// </summary>
        /// <value>The number of z divisions.</value>
        public int NumberOfZDivisions { get; set; }

        /// <summary>
        /// The width of each area object measured in the global X direction. [L]
        /// </summary>
        /// <value>The division width x.</value>
        public double DivisionWidthX { get; set; }

        /// <summary>
        /// The height of each area object measured in the global Z direction. [L]
        /// </summary>
        /// <value>The division width z.</value>
        public double DivisionWidthZ { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="WallTemplate" /> had restraints provided at the base.
        /// </summary>
        /// <value><c>true</c> if restraint; otherwise, <c>false</c>.</value>
        public bool AddRestraints { get; set; } = true;

        /// <summary>
        /// The shell section property used for all floor slabs in the frame.
        /// This must either be Default or the name of a defined shell section property.
        /// </summary>
        /// <value>The area.</value>
        public string Area { get; set; } = "Default";


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
