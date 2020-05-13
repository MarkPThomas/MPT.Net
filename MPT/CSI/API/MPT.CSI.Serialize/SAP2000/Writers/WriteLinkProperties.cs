using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.LinkProperties;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteLinkProperties
    {
        internal static void DefineLinkProperties(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_01_GENERAL, setLINK_PROPERTY_DEFINITIONS_01_GENERAL);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_02_LINEAR, setLINK_PROPERTY_DEFINITIONS_02_LINEAR);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_03_MULTILINEAR, setLINK_PROPERTY_DEFINITIONS_03_MULTILINEAR);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_04_DAMPER, setLINK_PROPERTY_DEFINITIONS_04_DAMPER);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_05_GAP, setLINK_PROPERTY_DEFINITIONS_05_GAP);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_06_HOOK, setLINK_PROPERTY_DEFINITIONS_06_HOOK);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_07_RUBBER_ISOLATOR, setLINK_PROPERTY_DEFINITIONS_07_RUBBER_ISOLATOR);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_08_SLIDING_ISOLATOR, setLINK_PROPERTY_DEFINITIONS_08_SLIDING_ISOLATOR);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_09_TC_SLIDING_ISOLATOR, setLINK_PROPERTY_DEFINITIONS_09_TC_SLIDING_ISOLATOR);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_10_PLASTIC_WEN, setLINK_PROPERTY_DEFINITIONS_10_PLASTIC_WEN);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_11_MULTILINEAR_PLASTIC, setLINK_PROPERTY_DEFINITIONS_11_MULTILINEAR_PLASTIC);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_12_TRIPLE_PENDULUM_ISOLATOR, setLINK_PROPERTY_DEFINITIONS_12_TRIPLE_PENDULUM_ISOLATOR);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_14_BILINEAR_DAMPER, setLINK_PROPERTY_DEFINITIONS_14_BILINEAR_DAMPER);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_15_FRICTION_SPRING_DAMPER, setLINK_PROPERTY_DEFINITIONS_15_FRICTION_SPRING_DAMPER);
            //writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_DEFINITIONS_16_HIGH_DAMPING_RUBBER_ISOLATOR, setLINK_PROPERTY_DEFINITIONS_16_HIGH_DAMPING_RUBBER_ISOLATOR);
        }

        #region Link Properties
        /// <summary>
        /// Sets the link property definitions 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setLINK_PROPERTY_DEFINITIONS_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Link link in model.Structure.Links)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Link", Adaptor.ToStringEntryLimited(link.Name)},
                    { "Color", Adaptor.ToStringEntryLimited(link.Section.ColorName)},
                    { "Mass", Adaptor.fromDouble(link.Section.Mass)},
                    { "Weight", Adaptor.fromDouble(link.Section.Weight)},
                    { "RotInert1", Adaptor.fromDouble(link.Section.RotationalInertia1)},
                    { "RotInert2", Adaptor.fromDouble(link.Section.RotationalInertia2)},
                    { "RotInert3", Adaptor.fromDouble(link.Section.RotationalInertia3)},
                    { "DefLength", Adaptor.fromDouble(link.Section.DefinedLength)},
                    { "DefArea", Adaptor.fromDouble(link.Section.DefinedArea)},
                    { "PDM2I", Adaptor.fromDouble(link.Section.PDeltaM2I)},
                    { "PDM2J", Adaptor.fromDouble(link.Section.PDeltaM2J)},
                    { "PDM3I", Adaptor.fromDouble(link.Section.PDeltaM3I)},
                    { "PDM3J", Adaptor.fromDouble(link.Section.PDeltaM3J)},
                    { "LinkType", Adaptor.fromEnum(link.Section.LinkType)}
                };

                if (!string.IsNullOrEmpty(link.Section.Notes))
                {
                    tableRow["Notes"] = Adaptor.ToStringEntryLimited(link.Section.Notes);
                }

                if (!string.IsNullOrEmpty(link.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(link.GUID);
                }

                table.Add(tableRow);

                switch (link.Section.LinkType)
                {
                    // TODO: Finish LinkProperties
                    default:
                        break;
                }
            }
        }


        //TABLE:  "LINK PROPERTY DEFINITIONS 02 - LINEAR"
        //   Link=LIN2 DOF = U1   Fixed=No TransKE = 0.2   TransCE=0.1
        //   Link=LIN2 DOF = U2   Fixed=Yes
        /// <summary>
        /// Sets the link property definitions 02 linear.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_02_LINEAR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 03 - MULTILINEAR"
        //Link=LIN1 DOF = U1   Fixed=No NonLinear = Yes   TransKE=1   TransCE=0.1   Force=-1   Displ=-10
        //Link=LIN1 DOF = U1   Force=-1   Displ=-1
        //Link=LIN1 DOF = U1   Force=0   Displ=0
        //Link=LIN1 DOF = U1   Force=1   Displ=1
        //Link=LIN1 DOF = U1   Force=1   Displ=10
        //Link=LIN1 DOF = U2   Fixed=Yes
        /// <summary>
        /// Sets the link property definitions 03 multilinear.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_03_MULTILINEAR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 04 - DAMPER"
        //   Link=LIN4   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   TransK=0.4   TransC=0.5   CExp=1
        /// <summary>
        /// Sets the link property definitions 04 damper.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_04_DAMPER(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 05 - GAP"
        //   Link=LIN7   DOF=R2   Fixed=No   NonLinear=Yes   RotKE=0.1   RotCE=0.2   RotK=0.3   RotOpen=0.4
        //   Link=LIN8   DOF=R3   Fixed=No   NonLinear=Yes   RotKE=0.1   RotCE=0.2   RotK=0.3   RotOpen=0.4
        /// <summary>
        /// Sets the link property definitions 05 gap.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_05_GAP(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 06 - HOOK"
        //   Link=LIN9   DOF=U1   Fixed=Yes
        //   Link=LIN9   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   TransK=0.4   TransOpen=0.5
        /// <summary>
        /// Sets the link property definitions 06 hook.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_06_HOOK(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 07 - RUBBER ISOLATOR"
        //   Link=LIN11   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   TransK=0.4   TransYield=0.5   Ratio=0.6
        //   Link=LIN11   DOF=R1   Fixed=No   NonLinear=No   RotKE=0   RotCE=0
        //   Link=LIN11   DOF=R2   Fixed=Yes
        /// <summary>
        /// Sets the link property definitions 07 rubber isolator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_07_RUBBER_ISOLATOR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 08 - SLIDING ISOLATOR"
        //   Link=LIN12   DOF=U3   Fixed=No   NonLinear=Yes   TransKE=0.7   TransCE=0.6   DJ=0.5   TransK=0.1   Slow=0.2   Fast=0.3   Rate=0.4   Radius=1
        //   Link=LIN12   DOF=R3   Fixed=Yes
        /// <summary>
        /// Sets the link property definitions 08 sliding isolator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_08_SLIDING_ISOLATOR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 09 - TC  SLIDING ISOLATOR"
        //   Link=LIN13   DOF=U1   Fixed=No   NonLinear=Yes   TransKE=1   TransCE=0.1   TransK=0.1   TransKTens=0.2   GapComp=0.3   GapTens=0.4   TransC=0.5
        //   Link=LIN13   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0   TransCE=0   DJ=0   TransK=0   SlowComp=0   SlowTens=0   FastComp=0   FastTens=0   RateComp=0   RateTens=0   Radius=1
        /// <summary>
        /// Sets the link property definitions 09 tc sliding isolator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_09_TC_SLIDING_ISOLATOR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 10 - PLASTIC (WEN)"
        //   Link=LIN10   DOF=U1   Fixed=No   NonLinear=Yes   TransKE=1   TransCE=0.1   TransK=0.1   TransYield=0.2   Ratio=0.3   YieldExp=2
        //   Link=LIN10   DOF=R1   Fixed=Yes
        /// <summary>
        /// Sets the link property definitions 10 plastic wen.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_10_PLASTIC_WEN(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 11 - MULTILINEAR PLASTIC"
        //   Link=LIN3   DOF=U1   Fixed=No   NonLinear=Yes   TransKE=1   TransCE=0.1   HysType=Kinematic   Force=-1   Displ=-10
        //   Link=LIN3   DOF=U1   Force=-1   Displ=-1
        //   Link=LIN3   DOF=U1   Force=0   Displ=0
        //   Link=LIN3   DOF=U1   Force=1   Displ=1
        //   Link=LIN3   DOF=U1   Force=1   Displ=10
        /// <summary>
        /// Sets the link property definitions 11 multilinear plastic.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_11_MULTILINEAR_PLASTIC(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 12 - TRIPLE PENDULUM ISOLATOR"
        //   Link=LIN14   DOF=U1   Fixed=No   NonLinear=Yes   TransKE=1   TransCE=0.1   TransK=0.1   TransC=0
        //   Link=LIN14   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   HeightOut=0.4   HeightIn=0.5   SymOut=Yes   KOutTop=0.6   KInTop=0.2   SlowOutTop=0.7   SlowInTop=0.3   FastOutTop=0.8   FastInTop=0.4   RateOutTop=0.9 _
        //        RateInTop=0.5   RadOutTop=0.1   RadInTop=0.6   StopOutTop=0.2   StopInTop=0
        /// <summary>
        /// Sets the link property definitions 12 triple pendulum isolator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_12_TRIPLE_PENDULUM_ISOLATOR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 14 - BILINEAR DAMPER"
        //   Link=LIN5   DOF=U3   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   TransK=0.4   TransC=1.5   TransCy=0.6   ForceLimit=1.7
        /// <summary>
        /// Sets the link property definitions 14 bilinear damper.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_14_BILINEAR_DAMPER(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 15 - FRICTION SPRING DAMPER"
        //   Link=LIN17   DOF=R1   Fixed=No   NonLinear=Yes   RotKE=0.1   RotCE=0.2   RotK=0.7   RotK1=0.6   RotK2=0.5   RotU0=-0.5   RotUs=0.6   ActiveDir=Tension
        //   Link=LIN18   DOF=R1   Fixed=No   NonLinear=Yes   RotKE=0.1   RotCE=0.2   RotK=0.7   RotK1=0.6   RotK2=0.5   RotU0=-0.5   RotUs=0.6   ActiveDir=Both
        //   Link=LIN6   DOF=R1   Fixed=No   NonLinear=Yes   RotKE=0.1   RotCE=0.2   RotK=0.7   RotK1=0.6   RotK2=0.5   RotU0=-0.5   RotUs=0.6   ActiveDir=Compression
        /// <summary>
        /// Sets the link property definitions 15 friction spring damper.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_15_FRICTION_SPRING_DAMPER(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }

        //TABLE:  "LINK PROPERTY DEFINITIONS 16 - HIGH DAMPING RUBBER ISOLATOR"
        //   Link=LIN15   DOF=U1   Fixed=Yes
        //   Link=LIN15   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   Area=1   EffHeight=1   AddStiff=1000   NumHysTerms=1   CoStrain1=0.1   CoStrength1=5000   Resistance=0.3   CoStrainDam=0.3   StiffIter=Elastic
        //   Link=LIN16   DOF=U1   Fixed=Yes
        //   Link=LIN16   DOF=U2   Fixed=No   NonLinear=Yes   TransKE=0.1   TransCE=0.2   DJ=0.3   Area=1   EffHeight=1   AddStiff=1000   NumHysTerms=3   CoStrain1=0.1   CoStrength1=5000   CoStrain2=0.1   CoStrength2=5000   CoStrain3=0.1 _
        //        CoStrength3=5000   Resistance=0.3   CoStrainDam=0.3   StiffIter="Incremental Secant"
        //   Link=LIN16   DOF=U3   Fixed=No   NonLinear=Yes   TransKE=1   TransCE=2   DJ=3   Area=1   EffHeight=1   AddStiff=1000   NumHysTerms=3   CoStrain1=0.1   CoStrength1=5000   CoStrain2=0.1   CoStrength2=5000   CoStrain3=0.1   CoStrength3=5000 _
        //        Resistance=0.3   CoStrainDam=0.3   StiffIter="Incremental Secant"
        /// <summary>
        /// Sets the link property definitions 16 high damping rubber isolator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setLINK_PROPERTY_DEFINITIONS_16_HIGH_DAMPING_RUBBER_ISOLATOR(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
