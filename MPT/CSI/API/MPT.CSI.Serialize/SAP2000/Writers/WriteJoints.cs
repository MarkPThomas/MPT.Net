using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteJoints
    {
        internal static void DefineJoints(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.JOINT_COORDINATES, setJOINT_COORDINATES);
        }

        internal static void AssignJoints(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.JOINT_RESTRAINT_ASSIGNMENTS, setJOINT_RESTRAINT_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.JOINT_CONSTRAINT_ASSIGNMENTS, setJOINT_CONSTRAINT_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.JOINT_ADDED_MASS_ASSIGNMENTS, setJOINT_ADDED_MASS_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.JOINT_LOCAL_AXES_ASSIGNMENTS_1_TYPICAL, setJOINT_LOCAL_AXES_ASSIGNMENTS_1_TYPICAL);
            //writer.WriteSingleTable(SAP2000Tables.JOINT_LOCAL_AXES_ASSIGNMENTS_2_ADVANCED, setJOINT_LOCAL_AXES_ASSIGNMENTS_2_ADVANCED);
            writer.WriteSingleTable(SAP2000Tables.JOINT_MERGE_NUMBER_ASSIGNMENTS, setJOINT_MERGE_NUMBER_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.JOINT_PANEL_ZONE_ASSIGNMENTS, setJOINT_PANEL_ZONE_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.JOINT_SPRING_ASSIGNMENTS_1_UNCOUPLED, setJOINT_SPRING_ASSIGNMENTS_1_UNCOUPLED);
            writer.WriteSingleTable(SAP2000Tables.JOINT_SPRING_ASSIGNMENTS_2_COUPLED, setJOINT_SPRING_ASSIGNMENTS_2_COUPLED);
        }

        internal static void LoadJoints(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.JOINT_LOADS_FORCE, setJOINT_LOADS_FORCE);
            writer.WriteSingleTable(SAP2000Tables.JOINT_LOADS_GROUND_DISPLACEMENT, setJOINT_LOADS_GROUND_DISPLACEMENT);
        }

        /// <summary>
        /// Sets the joint coordinates.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_COORDINATES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                    { "CoordType", Adaptor.fromEnum(point.CoordinateType) },
                    { "CoordSys", Adaptor.ToStringEntryLimited(point.CoordinateSystem) },
                    { "SpecialJt", Adaptor.toYesNo(point.IsSpecialPoint) },
                }; 

                switch (point.CoordinateType)
                {
                    case eCoordinateType.Cartesian:
                        tableRow["XorR"] = Adaptor.fromDouble(point.X);
                        tableRow["Y"] = Adaptor.fromDouble(point.Y);
                        tableRow["Z"] = Adaptor.fromDouble(point.Z);
                        break;
                    case eCoordinateType.Cylindrical:
                        // TODO: COnfirm output for cylindrical coord systems
                        //tableRow["XorR"] = Adaptor.fromDouble(point.Radius);
                        //tableRow["Y"] = Adaptor.fromDouble(point.Theta);
                        //tableRow["Z"] = Adaptor.fromDouble(point.Z);
                        break;
                    case eCoordinateType.Spherical:
                        // TODO: COnfirm output for spherical coord systems
                        //tableRow["XorR"] = Adaptor.fromDouble(point.Radius);
                        //tableRow["Y"] = Adaptor.fromDouble(point.Theta);
                        //tableRow["Z"] = Adaptor.fromDouble(point.Phi);
                        break;
                    default:
                        break;
                }
                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the joint restraint assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_RESTRAINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                        { "U1", Adaptor.toYesNo(!point.DegreesOfFreedom.U1) },
                        { "U2", Adaptor.toYesNo(!point.DegreesOfFreedom.U2) },
                        { "U3", Adaptor.toYesNo(!point.DegreesOfFreedom.U3) },
                        { "R1", Adaptor.toYesNo(!point.DegreesOfFreedom.R1) },
                        { "R2", Adaptor.toYesNo(!point.DegreesOfFreedom.R2) },
                        { "R3", Adaptor.toYesNo(!point.DegreesOfFreedom.R3) }
                    });
            }
        }


        /// <summary>
        /// Sets the joint constraint assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_CONSTRAINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                foreach (string constraintName in point.ConstraintNames)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                            { "Constraint", Adaptor.ToStringEntryLimited(constraintName) }
                        });
                }
            }
        }


        /// <summary>
        /// Sets the joint added mass assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_ADDED_MASS_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                        { "CoordSys", Adaptor.ToStringEntryLimited(point.Mass.IsLocalCoordinateSystem ? "Joint Local" : "") }, // TODO: Consider removing point.Mass.IsLocalCoordinateSystem bool and instead have string for coord system
                        { "Mass1", Adaptor.fromDouble(point.Mass.GetMass().U1) },
                        { "Mass2", Adaptor.fromDouble(point.Mass.GetMass().U2) },
                        { "Mass1", Adaptor.fromDouble(point.Mass.GetMass().U1) },
                        { "MMI1", Adaptor.fromDouble(point.Mass.GetMass().R1) },
                        { "MMI2", Adaptor.fromDouble(point.Mass.GetMass().R2) },
                        { "MMI3", Adaptor.fromDouble(point.Mass.GetMass().R3) }
                    });
            }
        }


        /// <summary>
        /// Sets the joint local axes assignments 1 typical.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_LOCAL_AXES_ASSIGNMENTS_1_TYPICAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                        { "AngleA", Adaptor.fromDouble(point.LocalAxes.AngleA) },
                        { "AngleB", Adaptor.fromDouble(point.LocalAxes.AngleB) },
                        { "AngleC", Adaptor.fromDouble(point.LocalAxes.AngleC) }
                    });
            }
        }

        //TABLE:  "JOINT LOCAL AXES ASSIGNMENTS 2 - ADVANCED"
        // TODO: Implement "JOINT LOCAL AXES ASSIGNMENTS 2 - ADVANCED"
        //Joint=8
        //LocalPlane=31
        //AxOption1="Two Joints"   AxCoordSys=GLOBAL AxCoordDir = Z   AxVecJt1=24   AxVecJt2=36
        //PlOption1="UserCoordinateSystemNames Vector"   PlCoordSys=GLOBAL CoordDir1 = X   CoordDir2=Y PlVecJt1 = 24   PlVecJt2=None
        //AxVecX = 0   AxVecY=0   AxVecZ = 1
        //PlVecX=1   PlVecY=2   PlVecZ=3


        /// <summary>
        /// Sets the joint merge number assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_MERGE_NUMBER_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                        { "MergeNumber", Adaptor.fromInteger(point.MergeNumber) }
                    });
            }
        }


        #region Panel Zone
        /// <summary>
        /// Sets the joint panel zone assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_PANEL_ZONE_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                    { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                };

                if (point.PanelZone.PanelZoneProperties.Thickness > 0)
                {
                    tableRow["DoublerPl"] = Adaptor.fromDouble(point.PanelZone.PanelZoneProperties.Thickness);
                }

                tableRow["PZFrom"] = Adaptor.ToStringEntryLimited(convertFromPanelZonePropertyType(point.PanelZone.PanelZoneProperties.PropertyType));
                table.Add(tableRow);

                if (point.PanelZone.PanelZoneProperties.K1 > 0)
                {
                    tableRow["MajorStiff"] = Adaptor.fromDouble(point.PanelZone.PanelZoneProperties.K1);
                }
                if (point.PanelZone.PanelZoneProperties.K2 > 0)
                {
                    tableRow["MinorStiff"] = Adaptor.fromDouble(point.PanelZone.PanelZoneProperties.K2);
                }

                if (!string.IsNullOrEmpty(point.PanelZone.PanelZoneProperties.LinkProperty))
                {
                    tableRow["PZLink"] = Adaptor.ToStringEntryLimited(point.PanelZone.PanelZoneProperties.LinkProperty);
                }

                tableRow["AxesFrom"] = Adaptor.ToStringEntryLimited(convertFromPanelZoneLocalAxis(point.PanelZone.PanelZoneProperties.LocalAxisFrom));
                tableRow["PZConnect"] = Adaptor.ToStringEntryLimited(convertFromPanelZonePropertyType(point.PanelZone.PanelZoneProperties.Connectivity));
                if (Math.Abs(point.PanelZone.PanelZoneProperties.LocalAxisAngle) > Constants.Tolerance)
                {
                    tableRow["PZAxesAngle"] = Adaptor.fromDouble(point.PanelZone.PanelZoneProperties.Thickness);
                }
            }
        }

        /// <summary>
        /// Converts to panel zone local axis.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ePanelZoneLocalAxis.</returns>
        private static string convertFromPanelZoneLocalAxis(ePanelZoneLocalAxis value)
        {
            switch (value)
            {
                case ePanelZoneLocalAxis.FromColumn:
                    return "Column";
                case ePanelZoneLocalAxis.UserDefined:
                    return "UserCoordinateSystemNames";
                default:
                    return "Column";
            }
        }

        /// <summary>
        /// Converts the type of to panel zone property.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ePanelZoneConnectivity.</returns>
        private static string convertFromPanelZonePropertyType(ePanelZoneConnectivity value)
        {
            switch (value)
            {
                case ePanelZoneConnectivity.ConnectsBeamsToObjects:
                    return "Beams to Other Objects";
                case ePanelZoneConnectivity.ConnectsBracesToObjects:
                    return "Braces to Other Objects";
                default:
                    return "Beams to Other Objects";
            }
        }

        /// <summary>
        /// Converts the type of to panel zone property.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="doublePlateThickness">The double plate thickness.</param>
        /// <returns>ePanelZonePropertyType.</returns>
        private static string convertFromPanelZonePropertyType(ePanelZonePropertyType value)
        {
            switch (value)
            {
                case ePanelZonePropertyType.FromSpringStiffness:
                    return "UserCoordinateSystemNames";
                case ePanelZonePropertyType.FromLink:
                    return "Link";
                default:
                    return "Elastic";
            }
        }
        #endregion


        /// <summary>
        /// Sets the joint spring assignments 1 uncoupled.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_SPRING_ASSIGNMENTS_1_UNCOUPLED(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                        { "CoordSys", Adaptor.ToStringEntryLimited(point.StiffnessCoupled.CoordinateSystem) },
                        { "U1", Adaptor.fromDouble(point.Stiffness.U1) },
                        { "U2", Adaptor.fromDouble(point.Stiffness.U2) },
                        { "U3", Adaptor.fromDouble(point.Stiffness.U3) },
                        { "R1", Adaptor.fromDouble(point.Stiffness.R1) },
                        { "R2", Adaptor.fromDouble(point.Stiffness.R2) },
                        { "R3", Adaptor.fromDouble(point.Stiffness.R3) }
                    });
            }
        }


        /// <summary>
        /// Sets the joint spring assignments 2 coupled.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_SPRING_ASSIGNMENTS_2_COUPLED(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                        { "CoordSys", Adaptor.ToStringEntryLimited(point.StiffnessCoupled.CoordinateSystem) },

                        { "U1", Adaptor.fromDouble(point.StiffnessCoupled.U1U1)},
                        { "U1U2", Adaptor.fromDouble(point.StiffnessCoupled.U1U2)},
                        { "U1U3", Adaptor.fromDouble(point.StiffnessCoupled.U1U3)},
                        { "U2U3", Adaptor.fromDouble(point.StiffnessCoupled.U2U3)},
                        { "U3", Adaptor.fromDouble(point.StiffnessCoupled.U3U3)},

                        { "U1R1", Adaptor.fromDouble(point.StiffnessCoupled.U1R1)},
                        { "U2R1", Adaptor.fromDouble(point.StiffnessCoupled.U2R1)},
                        { "U3R1", Adaptor.fromDouble(point.StiffnessCoupled.U3R1)},
                        { "R1", Adaptor.fromDouble(point.StiffnessCoupled.R1R1)},

                        { "U1R2", Adaptor.fromDouble(point.StiffnessCoupled.U1R2)},
                        { "U2R2", Adaptor.fromDouble(point.StiffnessCoupled.U2R2)},
                        { "U3R2", Adaptor.fromDouble(point.StiffnessCoupled.U3R2)},
                        { "R1R2", Adaptor.fromDouble(point.StiffnessCoupled.R1R2)},
                        { "R2", Adaptor.fromDouble(point.StiffnessCoupled.R2R2)},

                        { "U1R3", Adaptor.fromDouble(point.StiffnessCoupled.U1R3)},
                        { "U2R3", Adaptor.fromDouble(point.StiffnessCoupled.U2R3)},
                        { "U3R3", Adaptor.fromDouble(point.StiffnessCoupled.U3R3)},
                        { "R1R3", Adaptor.fromDouble(point.StiffnessCoupled.R1R3)},
                        { "R2R3", Adaptor.fromDouble(point.StiffnessCoupled.R2R3)},
                        { "R3", Adaptor.fromDouble(point.StiffnessCoupled.R3R3)}
                    });
            }
        }



        /// <summary>
        /// Sets the joint loads force.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_LOADS_FORCE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                foreach (NodeLoad nodeLoad in point.Loads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                            { "CoordSys", Adaptor.ToStringEntryLimited(nodeLoad.CoordinateSystem) },
                            { "LoadPat", Adaptor.ToStringEntryLimited(nodeLoad.LoadPattern) },
                            { "F1", Adaptor.fromDouble(nodeLoad.Force.F1) },
                            { "F2", Adaptor.fromDouble(nodeLoad.Force.F2) },
                            { "F3", Adaptor.fromDouble(nodeLoad.Force.F3) },
                            { "M1", Adaptor.fromDouble(nodeLoad.Force.M1) },
                            { "M2", Adaptor.fromDouble(nodeLoad.Force.M2) },
                            { "M3", Adaptor.fromDouble(nodeLoad.Force.M3) }
                        });
                }
            }
        }


        /// <summary>
        /// Sets the joint loads ground displacement.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_LOADS_GROUND_DISPLACEMENT(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Point point in model.Structure.Points)
            {
                foreach (NodeLoadDisplacement loadDisplacement in point.Displacements)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Joint", Adaptor.ToStringEntryLimited(point.Name) },
                            { "CoordSys", Adaptor.ToStringEntryLimited(loadDisplacement.CoordinateSystem) },
                            { "LoadPat", Adaptor.ToStringEntryLimited(loadDisplacement.LoadPattern) },
                            { "U1", Adaptor.fromDouble(loadDisplacement.Displacement.U1) },
                            { "U2", Adaptor.fromDouble(loadDisplacement.Displacement.U2) },
                            { "U3", Adaptor.fromDouble(loadDisplacement.Displacement.U3) },
                            { "R1", Adaptor.fromDouble(loadDisplacement.Displacement.R1) },
                            { "R2", Adaptor.fromDouble(loadDisplacement.Displacement.R2) },
                            { "R3", Adaptor.fromDouble(loadDisplacement.Displacement.R3) }
                        });
                }
            }
        }
    }
}
