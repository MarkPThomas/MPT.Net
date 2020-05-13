namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadHinges
    {
        internal static void DefineHinges(SAP2000Reader reader)
        {

        }

        internal static void AssignHinges(SAP2000Reader reader)
        {

        }

        //TABLE:  "HINGES DEF 02 - NONINTERACTING - DEFORM CONTROL - GENERAL"
        //HingeName=FH1 DOFType = "Moment M3"   Symmetric=Yes BeyondE = "To Zero"   FDType=Moment-Rot UseYldForce = Yes   UseYldDispl=Yes LengthType = Absolute   SSAbsLen=1   HysType=Isotropic

        //    TABLE:  "HINGES DEF 03 - NONINTERACTING - DEFORM CONTROL - FORCE-DEFORM"
        //HingeName=FH1 FDPoint = -E   Force=-0.2   Displ=-8
        //HingeName=FH1 FDPoint = -D   Force=-0.2   Displ=-6
        //HingeName=FH1 FDPoint = -C   Force=-1.25   Displ=-6
        //HingeName=FH1 FDPoint = -B   Force=-1   Displ=0
        //HingeName=FH1 FDPoint = A   Force=0   Displ=0
        //HingeName=FH1 FDPoint = B   Force=1   Displ=0
        //HingeName=FH1 FDPoint = C   Force=1.25   Displ=6
        //HingeName=FH1 FDPoint = D   Force=0.2   Displ=6
        //HingeName=FH1 FDPoint = E   Force=0.2   Displ=8

        //TABLE:  "HINGES DEF 04 - NONINTERACTING - DEFORM CONTROL - ACCEPTANCE"
        //HingeName=FH1 ACPoint = IO   ACPos=2   ACNeg=-2
        //HingeName=FH1 ACPoint = LS   ACPos=4   ACNeg=-4
        //HingeName=FH1 ACPoint = CP   ACPos=6   ACNeg=-6
    }
}
