namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadConstraints
    {
        internal static void DefineConstraints(SAP2000Reader reader)
        {

        }

        internal static void AssignConstraints(SAP2000Reader reader)
        {

        }

        //TABLE:  "CONSTRAINT DEFINITIONS - BEAM"
        //Name=BEAM1 CoordSys = GLOBAL   Axis=Auto
        //Name=BEAM2 CoordSys = GLOBAL   Axis=X

        //    TABLE:  "CONSTRAINT DEFINITIONS - BODY"
        //Name=BODY1 CoordSys = GLOBAL   UX=Yes UY = Yes   UZ=Yes RX = Yes   RY=Yes RZ = Yes


        //TABLE:  "CONSTRAINT DEFINITIONS - DIAPHRAGM"
        //Name=DIAPH1 CoordSys = GLOBAL   Axis=Z MultiLevel = Yes
        //Name=DIAPH1_0.CoordSys=GLOBAL Axis = Z   MultiLevel=No
        //    Name = DIAPH1_3.CoordSys = GLOBAL   Axis=Z MultiLevel = No
        //Name=DIAPH1_6.CoordSys=GLOBAL Axis = Z   MultiLevel=No

        //    TABLE:  "CONSTRAINT DEFINITIONS - EQUAL"
        //Name=EQUAL1 CoordSys = GLOBAL   UX=Yes UY = Yes   UZ=Yes RX = Yes   RY=Yes RZ = Yes


        //TABLE:  "CONSTRAINT DEFINITIONS - LINE"
        //Name=LINE1 CoordSys = GLOBAL   UX=Yes UY = Yes   UZ=Yes RX = Yes   RY=Yes RZ = Yes


        //TABLE:  "CONSTRAINT DEFINITIONS - LOCAL"
        //Name=LOCAL1 U1 = Yes   U2=Yes U3 = Yes   R1=Yes R2 = Yes   R3=Yes

        //    TABLE:  "CONSTRAINT DEFINITIONS - PLATE"
        //Name=PLATE1 CoordSys = GLOBAL   Axis=Auto

        //    TABLE:  "CONSTRAINT DEFINITIONS - ROD"
        //Name=ROD1 CoordSys = GLOBAL   Axis=Auto

        //    TABLE:  "CONSTRAINT DEFINITIONS - WELD"
        //Name=WELD1 CoordSys = GLOBAL   UX=Yes UY = Yes   UZ=Yes RX = Yes   RY=Yes RZ = Yes   Tolerance=0.01
    }
}
