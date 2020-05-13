// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-19-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="StageDatum.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Data associated with a particulat state datum entry.
    /// </summary>
    public class StageOperation : ApiProperty
    {
        /// <summary>
        /// The stage construction operation.
        /// </summary>
        /// <value>The operation.</value>
        public eStageOperations Operation { get; set; }

        /// <summary>
        /// The object type associated with the specified operation.<para />
        /// The following list shows which object types are applicable to each operation type:<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.AddStructure" />:  All object types;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.RemoveStructure" />:  All object types;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.LoadObjectsIfNew" />:  All object types;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.LoadObjects" />:  All object types;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionProperties" />:  All object types except Point;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />:  Group, Frame, Cable, Area;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.ChangeReleases" />:  Group, Frame;<para />
        /// <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />: All object types except Point;
        /// </summary>
        /// <value>The type of the object.</value>
        public eObjectType ObjectType { get; set; }

        /// <summary>
        /// The name of the object associated with the specified operation. <para />
        /// This is the name of a Group, Frame object, Cable object, Tendon object, Area object, Solid object, Link object or Point object, depending on the <see cref="ObjectType" /> item.
        /// </summary>
        /// <value>The name object.</value>
        public string NameObject { get; set; }


        /// <summary>
        /// The age of the added structure, at the time it is added, in days. <para />
        /// This item applies only to operations with <see cref="Operation" /> = <see cref="eStageOperations.AddStructure" />.
        /// </summary>
        /// <value>The age.</value>
        public double Age { get; set; }


        // TODO: Optional - Make LoadOrObjectType a type rather than string, that is based on the Operation type.
        /// <summary>
        /// A load type or an object type, depending on what is specified for the <see cref="Operation" /> item. <para />
        /// This item applies only to operation with <see cref="Operation" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is  Load or Accel, indicating the load type of an added load.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <see cref="ObjectType" /> item is Group, this is  Frame, Cable, Tendon, Area, Solid or Link, indicating the object type for which the section property is changed.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" /> and the <see cref="ObjectType" /> item is Group, this is Frame, Cable or Area, indicating the object type for which the section property modifiers are changed.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeReleases" /> and the <see cref="ObjectType" /> item is Group, this is Frame, indicating the object type for which the releases are changed.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, and the <see cref="ObjectType" /> item is not Group and not Point, this item is ignored and the type is picked up from the <see cref="ObjectType" /> item.
        /// </summary>
        /// <value>The type of the load or object.</value>
        public string LoadOrObjectType { get; set; }


        /// <summary>
        /// A load assignment or an object name, depending on what is specified for the <see cref="Operation" /> item. <para />
        /// This item applies only to Operation with <see cref="Operation" /> = <see cref="eStageOperations.LoadObjectsIfNew" />, <see cref="eStageOperations.LoadObjects" />, <see cref="eStageOperations.ChangeSectionProperties" />, <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, <see cref="eStageOperations.ChangeReleases" />, or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />, this is an array that includes the name of the load assigned to the operation. <para />
        /// If the associated LoadType item is Load, this item is the name of a defined load pattern.If the associated LoadType item is Accel , this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionProperties" /> or <see cref="eStageOperations.ChangeSectionPropertiesAndAge" />, this is the name of a Frame, Cable, Tendon, Area, Solid or Link object, depending on the object type specified.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeSectionPropertyModifiers" />, this is the name of a Frame, Cable or Area object, depending on the object type specified.<para />
        /// When <see cref="Operation" /> = <see cref="eStageOperations.ChangeReleases" />, this is the name of a Frame object.
        /// </summary>
        /// <value>The name of the load or object.</value>
        public string LoadOrObjectName { get; set; }
        


        /// <summary>
        /// This is the scale factor for the load assigned to the operation, if any. <para />
        /// [L/s^2] for Accel UX UY and UZ; otherwise unitless<para />
        /// This item applies only to operations with <see cref="Operation" /> = <see cref="eStageOperations.LoadObjectsIfNew" /> or <see cref="eStageOperations.LoadObjects" />.
        /// </summary>
        /// <value>The scale factor.</value>
        public double ScaleFactor { get; set; }


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
