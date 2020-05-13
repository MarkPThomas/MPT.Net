using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Hinges;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadFrames
    {
        internal static void DefineFrames(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CONNECTIVITY_FRAME, setCONNECTIVITY_FRAME);
        }

        internal static void AssignFrames(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_ASSIGNMENTS, setFRAME_SECTION_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.FRAME_INSERTION_POINT_ASSIGNMENTS, setFRAME_INSERTION_POINT_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.FRAME_PROPERTY_MODIFIERS, setFRAME_PROPERTY_MODIFIERS);
            reader.ReadSingleTable(SAP2000Tables.FRAME_RELEASE_ASSIGNMENTS_1_GENERAL, setFRAME_RELEASE_ASSIGNMENTS_1_GENERAL);
            reader.ReadSingleTable(SAP2000Tables.FRAME_RELEASE_ASSIGNMENTS_2_PARTIAL_FIXITY, setFRAME_RELEASE_ASSIGNMENTS_2_PARTIAL_FIXITY);
            reader.ReadSingleTable(SAP2000Tables.FRAME_OUTPUT_STATION_ASSIGNMENTS, setFRAME_OUTPUT_STATION_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.FRAME_AUTO_MESH_ASSIGNMENTS, setFRAME_AUTO_MESH_ASSIGNMENTS);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_HINGE_ASSIGNS_02_USER_DEFINED_PROPERTIES, setFRAME_HINGE_ASSIGNS_02_USER_DEFINED_PROPERTIES);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_HINGE_ASSIGNS_09_HINGE_OVERWRITES, setFRAME_HINGE_ASSIGNS_09_HINGE_OVERWRITES);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_HINGE_ASSIGNS_12_AUTO_ASCE_41_13_STEEL_BEAM, setFRAME_HINGE_ASSIGNS_12_AUTO_ASCE_41_13_STEEL_BEAM);
            reader.ReadSingleTable(SAP2000Tables.FRAME_LOAD_TRANSFER_OPTIONS, setFRAME_LOAD_TRANSFER_OPTIONS);
            reader.ReadSingleTable(SAP2000Tables.FRAME_DESIGN_PROCEDURES, setFRAME_DESIGN_PROCEDURES);
        }

        internal static void LoadFrames(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.FRAME_LOADS_POINT, setFRAME_LOADS_POINT);
            reader.ReadSingleTable(SAP2000Tables.FRAME_LOADS_DISTRIBUTED, setFRAME_LOADS_DISTRIBUTED);
        }

        /// <summary>
        /// Sets the connectivity frame.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_FRAME(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Frame"];
                Frame frame = model.Structure.Frames.FillItem(name);
                if (tableRow.ContainsKey("GUID")) frame.GUID = tableRow["GUID"];
                frame.IsCurved = Adaptor.fromYesNo(tableRow["IsCurved"]);
                frame.PointNames.Add(tableRow["JointI"]);
                frame.PointNames.Add(tableRow["JointJ"]);
            }
        }


        /// <summary>
        /// Sets the frame section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.AutoSelectSectionName = tableRow["AutoSelect"];
                frame.SectionName = tableRow["AnalSect"];
                frame.AddMaterialOverwrite(model.Components.Materials.FillItem(tableRow["MatProp"]));
            }
        }


        /// <summary>
        /// Sets the frame insertion point assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_INSERTION_POINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.InsertionPoint.IsMirroredLocal2 = Adaptor.fromYesNo(tableRow["Mirror2"]);
                frame.InsertionPoint.IsMirroredLocal3 = Adaptor.fromYesNo(tableRow["Mirror3"]);
                frame.InsertionPoint.IsStiffnessTransformed = Adaptor.fromYesNo(tableRow["Transform"]);
                frame.InsertionPoint.CardinalPoint = Enums.EnumLibrary.ConvertStringToEnumByDescription<eCardinalInsertionPoint>(tableRow["CardinalPt"]);
                if (!(tableRow.ContainsKey("CoordSys"))) return;

                frame.InsertionPoint.CoordinateSystem = tableRow["CoordSys"];
                frame.InsertionPoint.OffsetDistancesI = new Displacements
                {
                    UX = Adaptor.toDouble(tableRow["JtOffsetXI"]),
                    UY = Adaptor.toDouble(tableRow["JtOffsetYI"]),
                    UZ = Adaptor.toDouble(tableRow["JtOffsetZI"]),
                };
                frame.InsertionPoint.OffsetDistancesJ = new Displacements
                {
                    UX = Adaptor.toDouble(tableRow["JtOffsetXJ"]),
                    UY = Adaptor.toDouble(tableRow["JtOffsetYJ"]),
                    UZ = Adaptor.toDouble(tableRow["JtOffsetZJ"]),
                };
            }
        }

        /// <summary>
        /// Sets the frame property modifiers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_PROPERTY_MODIFIERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameModifier.CrossSectionalArea = Adaptor.toDouble(tableRow["AMod"]);
                frame.FrameModifier.ShearV2 = Adaptor.toDouble(tableRow["AS2Mod"]);
                frame.FrameModifier.ShearV3 = Adaptor.toDouble(tableRow["AS3Mod"]);
                frame.FrameModifier.Torsion = Adaptor.toDouble(tableRow["JMod"]);
                frame.FrameModifier.BendingM2 = Adaptor.toDouble(tableRow["I22Mod"]);
                frame.FrameModifier.BendingM3 = Adaptor.toDouble(tableRow["I33Mod"]);
                frame.FrameModifier.MassModifier = Adaptor.toDouble(tableRow["MassMod"]);
                frame.FrameModifier.WeightModifier = Adaptor.toDouble(tableRow["WeightMod"]);
            }
        }


        /// <summary>
        /// Sets the frame release assignments 1 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_RELEASE_ASSIGNMENTS_1_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];

                frame.FrameReleaseI.EndRelease = new DegreesOfFreedomLocal
                {
                    U1 = Adaptor.fromYesNo(tableRow["PI"]),
                    U2 = Adaptor.fromYesNo(tableRow["V2I"]),
                    U3 = Adaptor.fromYesNo(tableRow["V3I"]),
                    R1 = Adaptor.fromYesNo(tableRow["TI"]),
                    R2 = Adaptor.fromYesNo(tableRow["M2I"]),
                    R3 = Adaptor.fromYesNo(tableRow["M3I"])
                };

                frame.FrameReleaseJ.EndRelease = new DegreesOfFreedomLocal
                {
                    U1 = Adaptor.fromYesNo(tableRow["PJ"]),
                    U2 = Adaptor.fromYesNo(tableRow["V2J"]),
                    U3 = Adaptor.fromYesNo(tableRow["V3J"]),
                    R1 = Adaptor.fromYesNo(tableRow["TJ"]),
                    R2 = Adaptor.fromYesNo(tableRow["M2J"]),
                    R3 = Adaptor.fromYesNo(tableRow["M3J"])
                };
            }
        }


        /// <summary>
        /// Sets the frame release assignments 2 partial fixity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_RELEASE_ASSIGNMENTS_2_PARTIAL_FIXITY(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];

                frame.FrameReleaseI.EndFixity = new Fixity
                {
                    U1 = Adaptor.toDouble(tableRow["PI"]),
                    U2 = Adaptor.toDouble(tableRow["V2I"]),
                    U3 = Adaptor.toDouble(tableRow["V3I"]),
                    R1 = Adaptor.toDouble(tableRow["TI"]),
                    R2 = Adaptor.toDouble(tableRow["M2I"]),
                    R3 = Adaptor.toDouble(tableRow["M3I"])
                };

                frame.FrameReleaseJ.EndFixity = new Fixity
                {
                    U1 = Adaptor.toDouble(tableRow["PJ"]),
                    U2 = Adaptor.toDouble(tableRow["V2J"]),
                    U3 = Adaptor.toDouble(tableRow["V3J"]),
                    R1 = Adaptor.toDouble(tableRow["TJ"]),
                    R2 = Adaptor.toDouble(tableRow["M2J"]),
                    R3 = Adaptor.toDouble(tableRow["M3J"])
                };

            }
        }

        /// <summary>
        /// Sets the frame output station assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_OUTPUT_STATION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.OutputStations.OutputStationType = convertToOutputStationType(tableRow["StationType"]);
                if (tableRow.ContainsKey("MinNumSta")) frame.OutputStations.MinStationNumber = Adaptor.toInteger(tableRow["MinNumSta"]);
                if (tableRow.ContainsKey("MaxStaSpcg")) frame.OutputStations.MaxStationSpacing = Adaptor.toDouble(tableRow["MaxStaSpcg"]);
                frame.OutputStations.NoOutputAndDesignAtPointLoads = Adaptor.fromYesNo(tableRow["AddAtPtLoad"]);
                frame.OutputStations.NoOutputAndDesignAtElementIntersections = Adaptor.fromYesNo(tableRow["AddAtElmInt"]);
            }
        }

        /// <summary>
        /// Converts the type of to output station.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eOutputStationType.</returns>
        private static eOutputStationType convertToOutputStationType(string value)
        {
            switch (value)
            {
                case "MinNumSta":
                    return eOutputStationType.MinStations;
                case "MaxStaSpcg":
                    return eOutputStationType.MaxSpacing;
                default:
                    return eOutputStationType.MinStations;
            }
        }


        /// <summary>
        /// Sets the frame automatic mesh assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_AUTO_MESH_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.AutoMesh.IsAutoMeshed = Adaptor.fromYesNo(tableRow["AutoMesh"]);
                frame.AutoMesh.IsAutoMeshedAtPoints = Adaptor.fromYesNo(tableRow["AtJoints"]);
                frame.AutoMesh.IsAutoMeshedAtLines = Adaptor.fromYesNo(tableRow["AtFrames"]);
                frame.AutoMesh.MinElementNumber = Adaptor.toInteger(tableRow["NumSegments"]);
                frame.AutoMesh.AutoMeshMaxLength = Adaptor.toDouble(tableRow["MaxLength"]);
            }
        }

        #region Hinges
        //TABLE:  "FRAME HINGE ASSIGNS 02 - USER DEFINED PROPERTIES"
        //Frame=40   AssignProp=FH1 DistType = RelDist   RelDist=0   AbsDist=0   ActualDist=0
        /// <summary>
        /// Sets the frame hinge assigns 02 user defined properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_HINGE_ASSIGNS_02_USER_DEFINED_PROPERTIES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                FrameHinge hinge = new FrameHinge();
                hinge.GeneratedPropertyName = tableRow["FH1"];
                //frame.Hinges

                throw new NotImplementedException();
            }
        }

        //TABLE:  "FRAME HINGE ASSIGNS 09 - HINGE OVERWRITES"
        //Frame=38   AutoDivide=No NoLoadDrop = No   LimNegStiff=10
        //Frame=40   AutoDivide=No NoLoadDrop = No   LimNegStiff=10
        /// <summary>
        /// Sets the frame hinge assigns 09 hinge overwrites.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_HINGE_ASSIGNS_09_HINGE_OVERWRITES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                throw new NotImplementedException();
            }
        }

        //TABLE:  "FRAME HINGE ASSIGNS 12 - AUTO ASCE 41-13 - STEEL BEAM"
        //Frame=38   DOF=M3 BeyondE = "To Zero"   DistType=RelDist RelDist = 0   AbsDist=0   ActualDist=0
        /// <summary>
        /// Sets the frame hinge assigns 12 automatic asce 41 13 steel beam.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_HINGE_ASSIGNS_12_AUTO_ASCE_41_13_STEEL_BEAM(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                throw new NotImplementedException();
            }
        }
        #endregion

        /// <summary>
        /// Sets the frame load transfer options.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_LOAD_TRANSFER_OPTIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.LoadTransferFromAreas = Adaptor.fromYesNo(tableRow["Transfer"]);
            }
        }


        /// <summary>
        /// Sets the frame design procedures.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_DESIGN_PROCEDURES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.DesignProcedure = Enums.EnumLibrary.ConvertStringToEnumByDescription<eFrameDesignProcedure>(tableRow["DesignProc"]);
            }
        }


        /// <summary>
        /// Sets the frame loads point.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_LOADS_POINT(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];

                RelativeAbsoluteCoordinate distanceFromI = new RelativeAbsoluteCoordinate(frame.Length)
                {
                    UseRelativeDistance = (tableRow["DistType"] == "RelDist")
                };
                if (distanceFromI.UseRelativeDistance)
                {
                    distanceFromI.RelativeDistance = Adaptor.toDouble(tableRow["RelDist"]);
                }
                else
                {
                    distanceFromI.ActualDistance = Adaptor.toDouble(tableRow["AbsDist"]);
                }

                FrameLoadPoint load = new FrameLoadPoint
                {
                    LoadPattern = tableRow["LoadPat"],
                    CoordinateSystem = tableRow["CoordSys"],
                    ForceType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadForceType>(tableRow["Type"]),
                    LoadDirection = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadDirection>(tableRow["Dir"]),
                    DistanceFromI = distanceFromI,
                    GUID = tableRow["GUID"],
                };

                if (tableRow.ContainsKey("Force")) load.PointLoadValue = Adaptor.toDouble(tableRow["Force"]);
                if (tableRow.ContainsKey("Moment")) load.PointLoadValue = Adaptor.toDouble(tableRow["Moment"]);

                frame.AddLoadPoint(load);
            }
        }


        /// <summary>
        /// Sets the frame loads distributed.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_LOADS_DISTRIBUTED(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];

                RelativeAbsoluteLength distanceFromI = new RelativeAbsoluteLength(frame.Length)
                {
                    UseRelativeDistance = (tableRow["DistType"] == "RelDist")
                };
                if (distanceFromI.UseRelativeDistance)
                {
                    distanceFromI.RelativeDistanceStart = Adaptor.toDouble(tableRow["RelDistA"]);
                    distanceFromI.RelativeDistanceEnd = Adaptor.toDouble(tableRow["RelDistB"]);
                }
                else
                {
                    distanceFromI.ActualDistanceStart = Adaptor.toDouble(tableRow["AbsDistA"]);
                    distanceFromI.ActualDistanceEnd = Adaptor.toDouble(tableRow["AbsDistB"]);
                }

                FrameLoadDistributed load = new FrameLoadDistributed
                {
                    LoadPattern = tableRow["LoadPat"],
                    CoordinateSystem = tableRow["CoordSys"],
                    ForceType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadForceType>(tableRow["Type"]),
                    LoadDirection = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadDirection>(tableRow["Dir"]),
                    DistanceFromI = distanceFromI,
                    GUID = tableRow["GUID"],
                };
                if (tableRow.ContainsKey("FOverLA")) load.StartLoadValue = Adaptor.toDouble(tableRow["FOverLA"]);
                if (tableRow.ContainsKey("MOverLA")) load.StartLoadValue = Adaptor.toDouble(tableRow["MOverLA"]);
                if (tableRow.ContainsKey("FOverLB")) load.EndLoadValue = Adaptor.toDouble(tableRow["FOverLB"]);
                if (tableRow.ContainsKey("MOverLB")) load.EndLoadValue = Adaptor.toDouble(tableRow["MOverLB"]);

                frame.AddLoadDistributed(load);
            }
        }
    }
}
