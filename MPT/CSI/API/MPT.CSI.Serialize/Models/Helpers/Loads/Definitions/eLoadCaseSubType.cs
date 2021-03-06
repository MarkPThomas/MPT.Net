﻿namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Enum eLoadCaseSubType
    /// </summary>
    public enum eLoadCaseSubType
    {
        None = -1,

        /// <summary>
        /// The error
        /// </summary>
        Error = 0,

        /// <summary>
        /// Nonlinear static.
        /// </summary>
        Nonlinear = 1,

        /// <summary>
        /// Nonlinear static staged construction.
        /// </summary>
        NonlinearStagedConstruction = 2,

        /// <summary>
        /// Eigen modes.
        /// </summary>
        Eigen = 3,

        /// <summary>
        /// Ritz modes.
        /// </summary>
        Ritz = 4,

        /// <summary>
        /// Transient linear modal time history.
        /// </summary>
        Transient = 5,

        /// <summary>
        /// Periodic linear modal time history.
        /// </summary>
        Periodic = 6
    }
}
