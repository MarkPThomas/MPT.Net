// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-21-2018
// ***********************************************************************
// <copyright file="LinkProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.LinkProperties;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties
{
    /// <summary>
    /// Class LinkProperties.
    /// </summary>
    public class LinkProperty : ObjectProperties
    {
        public eLinkPropertyType LinkType { get; set; }
        public double Mass { get; set; }
        public double Weight { get; set; }
        public double RotationalInertia1 { get; set; }
        public double RotationalInertia2 { get; set; }
        public double RotationalInertia3 { get; set; }
        public double DefinedLength { get; set; }
        public double DefinedArea { get; set; }
        public double PDeltaM2I { get; set; }
        public double PDeltaM2J { get; set; }
        public double PDeltaM3I { get; set; }
        public double PDeltaM3J { get; set; }

        internal static LinkProperty Factory(string name)
        {
            LinkProperty property = new LinkProperty(name);
            return property;
        }

        protected LinkProperty(string name) : base(name)
        {
        }
    }
}
