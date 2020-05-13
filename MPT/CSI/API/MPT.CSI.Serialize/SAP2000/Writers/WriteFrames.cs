using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Hinges;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteFrames
    {
        internal static void DefineFrames(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CONNECTIVITY_FRAME, setCONNECTIVITY_FRAME);
        }

        internal static void AssignFrames(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_ASSIGNMENTS, setFRAME_SECTION_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.FRAME_INSERTION_POINT_ASSIGNMENTS, setFRAME_INSERTION_POINT_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.FRAME_PROPERTY_MODIFIERS, setFRAME_PROPERTY_MODIFIERS);
            writer.WriteSingleTable(SAP2000Tables.FRAME_RELEASE_ASSIGNMENTS_1_GENERAL, setFRAME_RELEASE_ASSIGNMENTS_1_GENERAL);
            writer.WriteSingleTable(SAP2000Tables.FRAME_RELEASE_ASSIGNMENTS_2_PARTIAL_FIXITY, setFRAME_RELEASE_ASSIGNMENTS_2_PARTIAL_FIXITY);
            writer.WriteSingleTable(SAP2000Tables.FRAME_OUTPUT_STATION_ASSIGNMENTS, setFRAME_OUTPUT_STATION_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.FRAME_AUTO_MESH_ASSIGNMENTS, setFRAME_AUTO_MESH_ASSIGNMENTS);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_HINGE_ASSIGNS_02_USER_DEFINED_PROPERTIES, setFRAME_HINGE_ASSIGNS_02_USER_DEFINED_PROPERTIES);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_HINGE_ASSIGNS_09_HINGE_OVERWRITES, setFRAME_HINGE_ASSIGNS_09_HINGE_OVERWRITES);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_HINGE_ASSIGNS_12_AUTO_ASCE_41_13_STEEL_BEAM, setFRAME_HINGE_ASSIGNS_12_AUTO_ASCE_41_13_STEEL_BEAM);
            writer.WriteSingleTable(SAP2000Tables.FRAME_LOAD_TRANSFER_OPTIONS, setFRAME_LOAD_TRANSFER_OPTIONS);
            writer.WriteSingleTable(SAP2000Tables.FRAME_DESIGN_PROCEDURES, setFRAME_DESIGN_PROCEDURES);
        }

        internal static void LoadFrames(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.FRAME_LOADS_POINT, setFRAME_LOADS_POINT);
            writer.WriteSingleTable(SAP2000Tables.FRAME_LOADS_DISTRIBUTED, setFRAME_LOADS_DISTRIBUTED);
        }

        /// <summary>
        /// Sets the connectivity frame.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_FRAME(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                    { "JointI", Adaptor.ToStringEntryLimited(frame.PointNames[0]) },
                    { "JointJ", Adaptor.ToStringEntryLimited(frame.PointNames[1]) },
                    { "IsCurved", Adaptor.toYesNo(frame.IsCurved) },
                };

                if (!string.IsNullOrEmpty(frame.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(frame.GUID);
                }

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the frame section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                        { "AutoSelect", Adaptor.ToStringEntryLimited(frame.AutoSelectSectionName) },
                        { "AnalSect", Adaptor.ToStringEntryLimited(frame.SectionName) },
                        { "MatProp", Adaptor.ToStringEntryLimited(frame.MaterialOverwriteName) },
                    });
            }
        }


        /// <summary>
        /// Sets the frame insertion point assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_INSERTION_POINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                    { "Mirror2", Adaptor.toYesNo(frame.InsertionPoint.IsMirroredLocal2) },
                    { "Mirror3", Adaptor.toYesNo(frame.InsertionPoint.IsMirroredLocal3) },
                    { "Transform", Adaptor.toYesNo(frame.InsertionPoint.IsStiffnessTransformed) },
                    { "CardinalPt", Adaptor.fromEnum(frame.InsertionPoint.CardinalPoint) }
                };

                if (!string.IsNullOrEmpty(frame.InsertionPoint.CoordinateSystem))
                {
                    tableRow["CoordSys"] = Adaptor.ToStringEntryLimited(frame.InsertionPoint.CoordinateSystem);

                    tableRow["JtOffsetXI"] = Adaptor.fromDouble(frame.InsertionPoint.OffsetDistancesI.UX);
                    tableRow["JtOffsetYI"] = Adaptor.fromDouble(frame.InsertionPoint.OffsetDistancesI.UY);
                    tableRow["JtOffsetZI"] = Adaptor.fromDouble(frame.InsertionPoint.OffsetDistancesI.UZ);

                    tableRow["JtOffsetXJ"] = Adaptor.fromDouble(frame.InsertionPoint.OffsetDistancesJ.UX);
                    tableRow["JtOffsetYJ"] = Adaptor.fromDouble(frame.InsertionPoint.OffsetDistancesJ.UY);
                    tableRow["JtOffsetZJ"] = Adaptor.fromDouble(frame.InsertionPoint.OffsetDistancesJ.UZ);
                }

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the frame property modifiers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_PROPERTY_MODIFIERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                if (frame.FrameModifier == null) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                        { "AMod", Adaptor.fromDouble(frame.FrameModifier.CrossSectionalArea) },
                        { "AS2Mod", Adaptor.fromDouble(frame.FrameModifier.ShearV2) },
                        { "AS3Mod", Adaptor.fromDouble(frame.FrameModifier.ShearV3) },
                        { "JMod", Adaptor.fromDouble(frame.FrameModifier.Torsion) },
                        { "I22Mod", Adaptor.fromDouble(frame.FrameModifier.BendingM2) },
                        { "I33Mod", Adaptor.fromDouble(frame.FrameModifier.BendingM3) },
                        { "MassMod", Adaptor.fromDouble(frame.FrameModifier.MassModifier) },
                        { "WeightMod", Adaptor.fromDouble(frame.FrameModifier.WeightModifier) }
                    });
            }
        }


        /// <summary>
        /// Sets the frame release assignments 1 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_RELEASE_ASSIGNMENTS_1_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },

                        { "PI", Adaptor.toYesNo(frame.FrameReleaseI.EndRelease.U1) },
                        { "V2I", Adaptor.toYesNo(frame.FrameReleaseI.EndRelease.U2) },
                        { "V3I", Adaptor.toYesNo(frame.FrameReleaseI.EndRelease.U3) },
                        { "TI", Adaptor.toYesNo(frame.FrameReleaseI.EndRelease.R1) },
                        { "M2I", Adaptor.toYesNo(frame.FrameReleaseI.EndRelease.R2) },
                        { "M3I", Adaptor.toYesNo(frame.FrameReleaseI.EndRelease.R3) },

                        { "PJ", Adaptor.toYesNo(frame.FrameReleaseJ.EndRelease.U1) },
                        { "V2J", Adaptor.toYesNo(frame.FrameReleaseJ.EndRelease.U2) },
                        { "V3J", Adaptor.toYesNo(frame.FrameReleaseJ.EndRelease.U3) },
                        { "TJ", Adaptor.toYesNo(frame.FrameReleaseJ.EndRelease.R1) },
                        { "M2J", Adaptor.toYesNo(frame.FrameReleaseJ.EndRelease.R2) },
                        { "M3J", Adaptor.toYesNo(frame.FrameReleaseJ.EndRelease.R3) }
                    });
            }
        }


        /// <summary>
        /// Sets the frame release assignments 2 partial fixity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_RELEASE_ASSIGNMENTS_2_PARTIAL_FIXITY(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },

                        { "PI", Adaptor.fromDouble(frame.FrameReleaseI.EndFixity.U1) },
                        { "V2I", Adaptor.fromDouble(frame.FrameReleaseI.EndFixity.U2) },
                        { "V3I", Adaptor.fromDouble(frame.FrameReleaseI.EndFixity.U3) },
                        { "TI", Adaptor.fromDouble(frame.FrameReleaseI.EndFixity.R1) },
                        { "M2I", Adaptor.fromDouble(frame.FrameReleaseI.EndFixity.R2) },
                        { "M3I", Adaptor.fromDouble(frame.FrameReleaseI.EndFixity.R3) },

                        { "PJ", Adaptor.fromDouble(frame.FrameReleaseJ.EndFixity.U1) },
                        { "V2J", Adaptor.fromDouble(frame.FrameReleaseJ.EndFixity.U2) },
                        { "V3J", Adaptor.fromDouble(frame.FrameReleaseJ.EndFixity.U3) },
                        { "TJ", Adaptor.fromDouble(frame.FrameReleaseJ.EndFixity.R1) },
                        { "M2J", Adaptor.fromDouble(frame.FrameReleaseJ.EndFixity.R2) },
                        { "M3J", Adaptor.fromDouble(frame.FrameReleaseJ.EndFixity.R3) }
                    });
            }
        }

        /// <summary>
        /// Sets the frame output station assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_OUTPUT_STATION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                    { "StationType", Adaptor.ToStringEntryLimited(convertFromOutputStationType(frame.OutputStations.OutputStationType)) },
                    { "AddAtPtLoad", Adaptor.toYesNo(frame.OutputStations.NoOutputAndDesignAtPointLoads) },
                    { "AddAtElmInt", Adaptor.toYesNo(frame.OutputStations.NoOutputAndDesignAtElementIntersections) }
                };

                if (frame.OutputStations.MinStationNumber > 0)
                {
                    tableRow["MinNumSta"] = Adaptor.fromInteger(frame.OutputStations.MinStationNumber);
                }

                if (frame.OutputStations.MinStationNumber > 0)
                {
                    tableRow["MaxStaSpcg"] = Adaptor.fromDouble(frame.OutputStations.MaxStationSpacing);
                }

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Converts the type of to output station.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eOutputStationType.</returns>
        private static string convertFromOutputStationType(eOutputStationType value)
        {
            switch (value)
            {
                case eOutputStationType.MinStations:
                    return "MinNumSta";
                case eOutputStationType.MaxSpacing:
                    return "MaxStaSpcg";
                default:
                    return "MinNumSta";
            }
        }


        /// <summary>
        /// Sets the frame automatic mesh assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_AUTO_MESH_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                        { "AutoMesh", Adaptor.toYesNo(frame.AutoMesh.IsAutoMeshed) },
                        { "AtJoints", Adaptor.toYesNo(frame.AutoMesh.IsAutoMeshedAtPoints) },
                        { "AtFrames", Adaptor.toYesNo(frame.AutoMesh.IsAutoMeshedAtLines) },
                        { "NumSegments", Adaptor.fromInteger(frame.AutoMesh.MinElementNumber) },
                        { "MaxLength", Adaptor.fromDouble(frame.AutoMesh.AutoMeshMaxLength) }
                    });
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
            foreach (Frame frame in model.Structure.Frames)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                        { "Transfer", Adaptor.toYesNo(frame.LoadTransferFromAreas) }
                    });
            }
        }


        /// <summary>
        /// Sets the frame design procedures.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_DESIGN_PROCEDURES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) },
                        { "DesignProc", Adaptor.fromEnum(frame.DesignProcedure) }
                    });
            }
        }


        /// <summary>
        /// Sets the frame loads point.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_LOADS_POINT(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                foreach (FrameLoadPoint loadPoint in frame.PointLoads)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) }
                    };

                    if (loadPoint.DistanceFromI.UseRelativeDistance)
                    {
                        tableRow["DistType"] = "RelDist";
                        tableRow["RelDist"] = Adaptor.fromDouble(loadPoint.DistanceFromI.RelativeDistance);
                    }
                    else
                    {
                        tableRow["DistType"] = "AbsDist";
                        tableRow["AbsDist"] = Adaptor.fromDouble(loadPoint.DistanceFromI.ActualDistance);
                    }

                    tableRow["LoadPat"] = Adaptor.ToStringEntryLimited(loadPoint.LoadPattern);
                    tableRow["CoordSys"] = Adaptor.ToStringEntryLimited(loadPoint.CoordinateSystem);

                    if (!string.IsNullOrEmpty(loadPoint.GUID))
                    {
                        tableRow["GUID"] = Adaptor.ToStringEntryLimited(loadPoint.GUID);
                    }

                    tableRow["Type"] = Adaptor.fromEnum(loadPoint.ForceType);
                    tableRow["Dir"] = Adaptor.fromEnum(loadPoint.LoadDirection);
                    if (loadPoint.ForceType == eLoadForceType.Force)
                    {
                        tableRow["Force"] = Adaptor.fromDouble(loadPoint.PointLoadValue);
                    }
                    else if (loadPoint.ForceType == eLoadForceType.Moment)
                    {
                        tableRow["Moment"] = Adaptor.fromDouble(loadPoint.PointLoadValue);
                    }

                    table.Add(tableRow);
                }
            }
        }


        /// <summary>
        /// Sets the frame loads distributed.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_LOADS_DISTRIBUTED(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                foreach (FrameLoadDistributed distributedLoad in frame.DistributedLoads)
                {
                    Dictionary<string, string> tableRow = new Dictionary<string, string>
                    {
                        { "Frame", Adaptor.ToStringEntryLimited(frame.Name) }
                    };

                    if (distributedLoad.DistanceFromI.UseRelativeDistance)
                    {
                        tableRow["DistType"] = "RelDist";
                        tableRow["RelDistA"] = Adaptor.fromDouble(distributedLoad.DistanceFromI.RelativeDistanceStart);
                        tableRow["RelDistB"] = Adaptor.fromDouble(distributedLoad.DistanceFromI.RelativeDistanceEnd);
                    }
                    else
                    {
                        tableRow["DistType"] = "AbsDist";
                        tableRow["AbsDistA"] = Adaptor.fromDouble(distributedLoad.DistanceFromI.ActualDistanceStart);
                        tableRow["AbsDistB"] = Adaptor.fromDouble(distributedLoad.DistanceFromI.ActualDistanceEnd);
                    }

                    tableRow["LoadPat"] = Adaptor.ToStringEntryLimited(distributedLoad.LoadPattern);
                    tableRow["CoordSys"] = Adaptor.ToStringEntryLimited(distributedLoad.CoordinateSystem);

                    if (!string.IsNullOrEmpty(distributedLoad.GUID))
                    {
                        tableRow["GUID"] = Adaptor.ToStringEntryLimited(distributedLoad.GUID);
                    }

                    tableRow["Type"] = Adaptor.fromEnum(distributedLoad.ForceType);
                    tableRow["Dir"] = Adaptor.fromEnum(distributedLoad.LoadDirection);
                    if (distributedLoad.ForceType == eLoadForceType.Force)
                    {
                        tableRow["FOverLA"] = Adaptor.fromDouble(distributedLoad.StartLoadValue);
                        tableRow["FOverLB"] = Adaptor.fromDouble(distributedLoad.EndLoadValue);
                    }
                    else if (distributedLoad.ForceType == eLoadForceType.Moment)
                    {
                        tableRow["MOverLA"] = Adaptor.fromDouble(distributedLoad.StartLoadValue);
                        tableRow["MOverLB"] = Adaptor.fromDouble(distributedLoad.EndLoadValue);
                    }
                    
                    table.Add(tableRow);
                }
            }
        }
    }
}
