﻿#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    public sealed class SteelColdFormedDesignResults : DesignResults
    {
        
        public SteelColdFormedDesignResults(ApiCSiApplication app, string name) : base(app)
        {
            Name = name;
        }
    }
}
#endif
