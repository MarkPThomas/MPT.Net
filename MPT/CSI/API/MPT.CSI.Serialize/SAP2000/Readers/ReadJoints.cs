using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.Masses;
using MPT.CSI.Serialize.Models.Components.Grids;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Abstractions;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Masses;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadJoints
    {
        internal static void DefineJoints(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.JOINT_COORDINATES, setJOINT_COORDINATES);
        }

        internal static void AssignJoints(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.JOINT_RESTRAINT_ASSIGNMENTS, setJOINT_RESTRAINT_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.JOINT_CONSTRAINT_ASSIGNMENTS, setJOINT_CONSTRAINT_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.JOINT_ADDED_MASS_ASSIGNMENTS, setJOINT_ADDED_MASS_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.JOINT_LOCAL_AXES_ASSIGNMENTS_1_TYPICAL, setJOINT_LOCAL_AXES_ASSIGNMENTS_1_TYPICAL);
            //reader.ReadSingleTable(SAP2000Tables.JOINT_LOCAL_AXES_ASSIGNMENTS_2_ADVANCED, setJOINT_LOCAL_AXES_ASSIGNMENTS_2_ADVANCED);
            reader.ReadSingleTable(SAP2000Tables.JOINT_MERGE_NUMBER_ASSIGNMENTS, setJOINT_MERGE_NUMBER_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.JOINT_PANEL_ZONE_ASSIGNMENTS, setJOINT_PANEL_ZONE_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.JOINT_SPRING_ASSIGNMENTS_1_UNCOUPLED, setJOINT_SPRING_ASSIGNMENTS_1_UNCOUPLED);
            reader.ReadSingleTable(SAP2000Tables.JOINT_SPRING_ASSIGNMENTS_2_COUPLED, setJOINT_SPRING_ASSIGNMENTS_2_COUPLED);
        }

        internal static void LoadJoints(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.JOINT_LOADS_FORCE, setJOINT_LOADS_FORCE);
            reader.ReadSingleTable(SAP2000Tables.JOINT_LOADS_GROUND_DISPLACEMENT, setJOINT_LOADS_GROUND_DISPLACEMENT);
        }

        /// <summary>
        /// Sets the joint coordinates.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_COORDINATES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                eCoordinateType coordinateType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eCoordinateType>(tableRow["CoordType"]);
                string jointName = tableRow["Joint"];

                switch (coordinateType)
                {
                    case eCoordinateType.Cartesian:
                        Coordinate3DCartesian coordinateCartesian = new Coordinate3DCartesian
                        {
                            X = Adaptor.toDouble(tableRow["XorR"]),
                            Y = Adaptor.toDouble(tableRow["Y"]),
                            Z = Adaptor.toDouble(tableRow["Z"])
                        };
                        model.Structure.Points.Add(coordinateCartesian, jointName);
                        break;
                    case eCoordinateType.Cylindrical:
                        // TODO: COnfirm output for cylindrical coord systems
                        Coordinate3DCylindrical coordinateCylindrical = new Coordinate3DCylindrical
                        {
                            Radius = Adaptor.toDouble(tableRow["XorR"]),
                            Theta = Adaptor.toDouble(tableRow["Y"]),
                            Z = Adaptor.toDouble(tableRow["Z"])
                        };
                        //model.Structure.Points.Add(coordinateCylindrical, jointName);
                        break;
                    case eCoordinateType.Spherical:
                        // TODO: COnfirm output for spherical coord systems
                        Coordinate3DSpherical coordinateSpherical = new Coordinate3DSpherical
                        {
                            Radius = Adaptor.toDouble(tableRow["XorR"]),
                            Theta = Adaptor.toDouble(tableRow["Y"]),
                            Phi = Adaptor.toDouble(tableRow["Z"])
                        };
                        //model.Structure.Points.Add(coordinateSpherical, jointName);
                        break;
                    default:
                        break;
                }

                Point point = model.Structure.Points[jointName];
                point.IsSpecialPoint = Adaptor.fromYesNo(tableRow["SpecialJt"]);
                point.CoordinateSystem = tableRow["CoordSys"];
                point.CoordinateType = coordinateType;
            }
        }


        /// <summary>
        /// Sets the joint restraint assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_RESTRAINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                DegreesOfFreedomLocal degreesOfFreedom = new DegreesOfFreedomLocal()
                {
                    U1 = !Adaptor.fromYesNo(tableRow["U1"]),
                    U2 = !Adaptor.fromYesNo(tableRow["U2"]),
                    U3 = !Adaptor.fromYesNo(tableRow["U3"]),
                    R1 = !Adaptor.fromYesNo(tableRow["R1"]),
                    R2 = !Adaptor.fromYesNo(tableRow["R2"]),
                    R3 = !Adaptor.fromYesNo(tableRow["R3"])
                };
                point.DegreesOfFreedom = degreesOfFreedom;
            }
        }


        /// <summary>
        /// Sets the joint constraint assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_CONSTRAINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                point.ConstraintNames.Add(tableRow["Constraint"]);
            }
        }


        /// <summary>
        /// Sets the joint added mass assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_ADDED_MASS_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                Mass mass = new Mass(
                    new MassProperties
                    {
                        U1 = Adaptor.toDouble(tableRow["Mass1"]),
                        U2 = Adaptor.toDouble(tableRow["Mass2"]),
                        U3 = Adaptor.toDouble(tableRow["Mass3"]),
                        R1 = Adaptor.toDouble(tableRow["MMI1"]),
                        R2 = Adaptor.toDouble(tableRow["MMI2"]),
                        R3 = Adaptor.toDouble(tableRow["MMI3"]),
                    },
                    (tableRow["CoordSys"] == "Joint Local"));

                point.Mass = mass;
            }
        }


        /// <summary>
        /// Sets the joint local axes assignments 1 typical.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_LOCAL_AXES_ASSIGNMENTS_1_TYPICAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                AngleLocalAxes localAxes = new AngleLocalAxes
                {
                    AngleA = Adaptor.toDouble(tableRow["AngleA"]),
                    AngleB = Adaptor.toDouble(tableRow["AngleB"]),
                    AngleC = Adaptor.toDouble(tableRow["AngleC"]),
                };
                point.LocalAxes = localAxes;
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                point.MergeNumber = Adaptor.toInteger(tableRow["MergeNumber"]);
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                double doublerPlateThickness = tableRow.ContainsKey("DoublerPl") ? Adaptor.toDouble(tableRow["DoublerPl"]) : 0;

                point.PanelZone.PanelZoneProperties.PropertyType = convertToPanelZonePropertyType(tableRow["PZFrom"], doublerPlateThickness);
                point.PanelZone.PanelZoneProperties.Thickness = doublerPlateThickness;
                if (tableRow.ContainsKey("MajorStiff"))
                {
                    point.PanelZone.PanelZoneProperties.K1 = Adaptor.toDouble(tableRow["MajorStiff"]);
                    point.PanelZone.PanelZoneProperties.K2 = Adaptor.toDouble(tableRow["MinorStiff"]);
                }

                if (tableRow.ContainsKey("PZLink"))
                    point.PanelZone.PanelZoneProperties.LinkProperty = tableRow["PZLink"];

                point.PanelZone.PanelZoneProperties.LocalAxisFrom = convertToPanelZoneLocalAxis(tableRow["AxesFrom"]);
                point.PanelZone.PanelZoneProperties.Connectivity = convertToPanelZonePropertyType(tableRow["PZConnect"]);
                if (tableRow.ContainsKey("PZAxesAngle"))
                    point.PanelZone.PanelZoneProperties.LocalAxisAngle = Adaptor.toDouble(tableRow["PZAxesAngle"]);
            }
        }

        /// <summary>
        /// Converts to panel zone local axis.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ePanelZoneLocalAxis.</returns>
        private static ePanelZoneLocalAxis convertToPanelZoneLocalAxis(string value)
        {
            switch (value)
            {
                case "Column":
                    return ePanelZoneLocalAxis.FromColumn;
                case "UserCoordinateSystemNames":
                    return ePanelZoneLocalAxis.UserDefined;
                default:
                    return ePanelZoneLocalAxis.FromColumn;
            }
        }

        /// <summary>
        /// Converts the type of to panel zone property.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ePanelZoneConnectivity.</returns>
        private static ePanelZoneConnectivity convertToPanelZonePropertyType(string value)
        {
            switch (value)
            {
                case "Beams to Other Objects":
                    return ePanelZoneConnectivity.ConnectsBeamsToObjects;
                case "Braces to Other Objects":
                    return ePanelZoneConnectivity.ConnectsBracesToObjects;
                default:
                    return ePanelZoneConnectivity.ConnectsBeamsToObjects;
            }
        }

        /// <summary>
        /// Converts the type of to panel zone property.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="doublePlateThickness">The double plate thickness.</param>
        /// <returns>ePanelZonePropertyType.</returns>
        private static ePanelZonePropertyType convertToPanelZonePropertyType(string value, double doublePlateThickness = 0)
        {
            switch (value)
            {
                case "User":
                    return ePanelZonePropertyType.FromSpringStiffness;
                case "Link":
                    return ePanelZonePropertyType.FromLink;
                case "Elastic":
                    return doublePlateThickness > 0 ? ePanelZonePropertyType.ElasticFromColumnAndDoublerPlate : ePanelZonePropertyType.ElasticFromColumn;
                default:
                    return ePanelZonePropertyType.ElasticFromColumn;
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                point.IsSpringCoupled = false;
                point.Stiffness.IsLocalCoordinateSystem = (tableRow["CoordSys"] == CoordinateSystems.Local);
                point.Stiffness.CoordinateSystem = tableRow["CoordSys"];
                point.Stiffness.U1 = Adaptor.toDouble(tableRow["U1"]);
                point.Stiffness.U2 = Adaptor.toDouble(tableRow["U2"]);
                point.Stiffness.U3 = Adaptor.toDouble(tableRow["U3"]);
                point.Stiffness.R1 = Adaptor.toDouble(tableRow["R1"]);
                point.Stiffness.R2 = Adaptor.toDouble(tableRow["R2"]);
                point.Stiffness.R3 = Adaptor.toDouble(tableRow["R3"]);
            }
        }


        /// <summary>
        /// Sets the joint spring assignments 2 coupled.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_SPRING_ASSIGNMENTS_2_COUPLED(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                point.IsSpringCoupled = true;
                point.StiffnessCoupled.IsLocalCoordinateSystem = (tableRow["CoordSys"] == CoordinateSystems.Local);
                point.StiffnessCoupled.CoordinateSystem = tableRow["CoordSys"];

                point.StiffnessCoupled.U1U1 = Adaptor.toDouble(tableRow["U1"]);
                point.StiffnessCoupled.U1U2 = Adaptor.toDouble(tableRow["U1U2"]);
                point.StiffnessCoupled.U1U3 = Adaptor.toDouble(tableRow["U1U3"]);
                point.StiffnessCoupled.U2U3 = Adaptor.toDouble(tableRow["U2U3"]);
                point.StiffnessCoupled.U3U3 = Adaptor.toDouble(tableRow["U3"]);

                point.StiffnessCoupled.U1R1 = Adaptor.toDouble(tableRow["U1R1"]);
                point.StiffnessCoupled.U2R1 = Adaptor.toDouble(tableRow["U2R1"]);
                point.StiffnessCoupled.U3R1 = Adaptor.toDouble(tableRow["U3R1"]);
                point.StiffnessCoupled.R1R1 = Adaptor.toDouble(tableRow["R1"]);

                point.StiffnessCoupled.U1R2 = Adaptor.toDouble(tableRow["U1R2"]);
                point.StiffnessCoupled.U2R2 = Adaptor.toDouble(tableRow["U2R2"]);
                point.StiffnessCoupled.U3R2 = Adaptor.toDouble(tableRow["U3R2"]);
                point.StiffnessCoupled.R1R2 = Adaptor.toDouble(tableRow["R1R2"]);
                point.StiffnessCoupled.R2R2 = Adaptor.toDouble(tableRow["R2"]);

                point.StiffnessCoupled.U1R3 = Adaptor.toDouble(tableRow["U1R3"]);
                point.StiffnessCoupled.U2R3 = Adaptor.toDouble(tableRow["U2R3"]);
                point.StiffnessCoupled.U3R3 = Adaptor.toDouble(tableRow["U3R3"]);
                point.StiffnessCoupled.R1R3 = Adaptor.toDouble(tableRow["R1R3"]);
                point.StiffnessCoupled.R2R3 = Adaptor.toDouble(tableRow["R2R3"]);
                point.StiffnessCoupled.R3R3 = Adaptor.toDouble(tableRow["R3"]);
            }
        }



        /// <summary>
        /// Sets the joint loads force.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_LOADS_FORCE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                NodeLoad nodeLoad = new NodeLoad
                {
                    CoordinateSystem = tableRow["CoordSys"],
                    LoadPattern = tableRow["LoadPat"],
                    Force = new Loads
                    {
                        F1 = Adaptor.toDouble(tableRow["F1"]),
                        F2 = Adaptor.toDouble(tableRow["F2"]),
                        F3 = Adaptor.toDouble(tableRow["F3"]),
                        M1 = Adaptor.toDouble(tableRow["M1"]),
                        M2 = Adaptor.toDouble(tableRow["M2"]),
                        M3 = Adaptor.toDouble(tableRow["M3"]),
                    }
                };
                point.Loads.Add(nodeLoad);
            }
        }


        /// <summary>
        /// Sets the joint loads ground displacement.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setJOINT_LOADS_GROUND_DISPLACEMENT(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Point point = model.Structure.Points[tableRow["Joint"]];
                NodeLoadDisplacement nodeLoad = new NodeLoadDisplacement
                {
                    CoordinateSystem = tableRow["CoordSys"],
                    LoadPattern = tableRow["LoadPat"],
                    Displacement = new Deformations
                    {
                        U1 = Adaptor.toDouble(tableRow["U1"]),
                        U2 = Adaptor.toDouble(tableRow["U2"]),
                        U3 = Adaptor.toDouble(tableRow["U3"]),
                        R1 = Adaptor.toDouble(tableRow["R1"]),
                        R2 = Adaptor.toDouble(tableRow["R2"]),
                        R3 = Adaptor.toDouble(tableRow["R3"]),
                    }
                };
                point.Displacements.Add(nodeLoad);
            }
        }
    }
}
