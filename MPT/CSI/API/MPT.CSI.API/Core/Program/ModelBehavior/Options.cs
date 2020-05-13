// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-04-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="Options.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Represents model and application options in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IOptions" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class Options : CSiApiBase, IOptions
    {

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Options" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Options(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the program dimension and tolerance items.
        /// </summary>
        /// <param name="cuttingPlaneTol">The tolerance for 2D view cutting planes. [L]</param>
        /// <param name="worldSpacing">The plan fine grid spacing. [L]</param>
        /// <param name="nudgeValue">The plan nudge value. [L]</param>
        /// <param name="pixelClickSize">The screen selection tolerance in pixels.</param>
        /// <param name="pixelSnapSize">The screen snap tolerance in pixels.</param>
        /// <param name="screenLineThickness">The screen line thickness in pixels.</param>
        /// <param name="printLineThickness">The printer line thickness in pixels.</param>
        /// <param name="maxFont">The maximum graphic font size in points.</param>
        /// <param name="minFont">The minimum graphic font size in points.</param>
        /// <param name="zoomStep">The auto zoom step size in percent (0 &lt; ZoomStep &lt;= 100).</param>
        /// <param name="shrinkFactor">The shrink factor in percent (0 &lt; ShrinkFact &lt;= 100).</param>
        /// <param name="textFileMaxChar">The maximum line length in the text file (ShrinkFact &gt;= 80).</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetDimensions(out double cuttingPlaneTol,
            out double worldSpacing,
            out double nudgeValue,
            out int pixelClickSize,
            out int pixelSnapSize,
            out int screenLineThickness,
            out int printLineThickness,
            out int maxFont,
            out int minFont,
            out int zoomStep,
            out int shrinkFactor,
            out int textFileMaxChar)
        {
            cuttingPlaneTol = -1;
            worldSpacing = -1;
            nudgeValue = -1;
            pixelClickSize = -1;
            pixelSnapSize = -1;
            screenLineThickness = -1;
            printLineThickness = -1;
            maxFont = -1;
            minFont = -1;
            zoomStep = -1;
            shrinkFactor = -1;
            textFileMaxChar = -1;

            _callCode = _sapModel.Options.GetDimensions(ref cuttingPlaneTol, ref worldSpacing, ref nudgeValue, ref pixelClickSize, ref pixelSnapSize, ref screenLineThickness, ref printLineThickness, ref maxFont, ref minFont, ref zoomStep, ref shrinkFactor, ref textFileMaxChar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets program dimension and tolerance items.
        /// Inputting 0 for any item means that the item will be ignored by the program; that is, its current value will not be changed.
        /// </summary>
        /// <param name="cuttingPlaneTol">The tolerance for 2D view cutting planes. [L]</param>
        /// <param name="worldSpacing">The plan fine grid spacing. [L]</param>
        /// <param name="nudgeValue">The plan nudge value. [L]</param>
        /// <param name="pixelClickSize">The screen selection tolerance in pixels.</param>
        /// <param name="pixelSnapSize">The screen snap tolerance in pixels.</param>
        /// <param name="screenLineThickness">The screen line thickness in pixels.</param>
        /// <param name="printLineThickness">The printer line thickness in pixels.</param>
        /// <param name="maxFont">The maximum graphic font size in points.</param>
        /// <param name="minFont">The minimum graphic font size in points.</param>
        /// <param name="zoomStep">The auto zoom step size in percent (0 &lt; ZoomStep &lt;= 100).</param>
        /// <param name="shrinkFactor">The shrink factor in percent (0 &lt; ShrinkFact &lt;= 100).</param>
        /// <param name="textFileMaxChar">The maximum line length in the text file (ShrinkFact &gt;= 80).</param>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetDimensions(double cuttingPlaneTol,
            double worldSpacing,
            double nudgeValue,
            int pixelClickSize,
            int pixelSnapSize,
            int screenLineThickness,
            int printLineThickness,
            int maxFont,
            int minFont,
            int zoomStep,
            int shrinkFactor,
            int textFileMaxChar)
        {
            _callCode = _sapModel.Options.SetDimensions(cuttingPlaneTol, worldSpacing, nudgeValue, pixelClickSize, pixelSnapSize, screenLineThickness, printLineThickness, maxFont, minFont, zoomStep, shrinkFactor, textFileMaxChar);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
