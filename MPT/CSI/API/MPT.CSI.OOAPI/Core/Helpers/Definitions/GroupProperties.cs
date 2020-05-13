// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="GroupProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions
{
    /// <summary>
    /// Class GroupProperties.
    /// </summary>
    public class GroupProperties : ApiProperty
    {
        /// <summary>
        /// Display color for the group specified.
        /// </summary>
        /// <value>The color.</value>
        public int Color { get; set; } = -1;

        /// <summary>
        /// True: The group is specified to be used for selection.
        /// </summary>
        /// <value><c>true</c> if [specified for selection]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSelection { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining section cuts.
        /// </summary>
        /// <value><c>true</c> if [specified for section cut definition]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSectionCutDefinition { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining steel frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSteelDesign { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining concrete frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for concrete design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForConcreteDesign { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining stages for nonlinear static analysis.
        /// </summary>
        /// <value><c>true</c> if [specified for static nl active stage]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForStaticNLActiveStage { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for reporting auto seismic loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic seismic output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAutoSeismicOutput { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for reporting auto wind loads.
        /// </summary>
        /// <value><c>true</c> if [specified for automatic wind output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAutoWindOutput { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for reporting group masses and weight.
        /// </summary>
        /// <value><c>true</c> if [specified for mass and weight]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForMassAndWeight { get; set; } = true;

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// True: The group is specified to be used for defining steel joist design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for steel joist design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForSteelJoistDesign { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining wall design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for wall design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForWallDesign { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining base plate design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for base plate design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForBasePlateDesign { get; set; } = true;

        /// <summary>
        /// True: The group is specified to be used for defining connection design design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for connection design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForConnectionDesign { get; set; } = true;
#else
        /// <summary>
        /// True: The group is specified to be used for defining colf formed frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for cold formed design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForColdFormedDesign { get; set; } = true;
        
        /// <summary>
        /// True: The group is specified to be used for defining alumnimum frame design groups.
        /// </summary>
        /// <value><c>true</c> if [specified for aluminum design]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForAluminumDesign { get; set; } = true;
        
        /// <summary>
        /// True: The group is specified to be used for reporting bridge response output.
        /// </summary>
        /// <value><c>true</c> if [specified for bridge response output]; otherwise, <c>false</c>.</value>
        public bool SpecifiedForBridgeResponseOutput  { get; set; } = true;
#endif  

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
