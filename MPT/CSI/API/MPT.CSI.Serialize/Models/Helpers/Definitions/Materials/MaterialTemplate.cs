// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-14-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-14-2018
// ***********************************************************************
// <copyright file="MaterialProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Materials
{
    /// <summary>
    /// Class MaterialProperties.
    /// </summary>
    public abstract class MaterialTemplate : ModelProperty
    {
        /// <summary>
        /// Gets or sets the type of material.
        /// </summary>
        /// <value>The type of material.</value>
        public eMaterialPropertyType MaterialType { get; protected set; }

        /// <summary>
        /// The region name of the material property that is user-predefined in the file "CSiMaterialLibrary*.xml" located in subfolder "Property Libraries" under the installation.
        /// </summary>
        /// <value>The region.</value>
        public eMaterialRegion Region { get; set; }

        /// <summary>
        /// The name of the material property with the specified <see cref="MaterialType"/> within the specified region.
        /// </summary>
        /// <value>The material standard.</value>
        public string Standard { get; set; }

        /// <summary>
        /// The Grade name of the material property with the specified <see cref="MaterialType"/> within the specified <see cref="Region"/> and <see cref="Standard"/>.
        /// </summary>
        /// <value>The material grade.</value>
        public string Grade { get; set; }

        /// <summary>
        /// This is an optional user specified name for the material property. 
        /// If no <see cref="Name"/> is specified, the program assigns a default name to the object.
        /// If a <see cref="Name"/> is specified and that name is not used for another object, the <see cref="Name"/> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialTemplate" /> class.
        /// </summary>
        /// <param name="materialType">Type of material.</param>
        /// <param name="region">The region.</param>
        /// <param name="standard">The material standard.</param>
        /// <param name="grade">The material grade.</param>
        protected MaterialTemplate(
            eMaterialPropertyType materialType, 
            eMaterialRegion region, 
            string standard, 
            string grade)
        {
            MaterialType = materialType;
            Region = region;
            Standard = standard;
            Grade = grade;
        }

        /// <summary>
        /// The region name of the material property that is user-predefined in the file "CSiMaterialLibrary*.xml" located in subfolder "Property Libraries" under the installation.
        /// </summary>
        /// <returns>System.String.</returns>
        public string RegionName( )
        {
            return Enums.EnumLibrary.GetEnumDescription(Region);
        }



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
