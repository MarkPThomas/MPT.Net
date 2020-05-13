// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="PierLabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Represents a pier label object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.IPierLabel" />
    public class PierLabel : CSiApiBase, IPierLabel
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="PierLabel"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public PierLabel(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined pier label property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined pier label property.</param>
        /// <param name="nameNew">New name for the pier label property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.PierLabel.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified pier label property.
        /// </summary>
        /// <param name="name">The name of an existing pier label property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.PierLabel.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.PierLabel.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        /// <summary>
        /// True: The Pier Label exists.
        /// </summary>
        /// <param name="name">The name of an existing Pier Label.</param>
        public bool GetPier(string name)
        {
            _callCode = _sapModel.PierLabel.GetPier(name);
            return _callCode == 0;
        }

        /// <summary>
        /// Adds a new Pier Label.
        /// </summary>
        /// <param name="name">The name of a Pier Label. 
        /// If this is the name of an existing Pier label, that Pier label is modified, otherwise a new spandrel label is added.</param>
        public void SetPier(string name)
        {
            _callCode = _sapModel.PierLabel.SetPier(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the section properties for a specified pier.
        /// </summary>
        /// <param name="name">The name of an existing pier.</param>
        /// <param name="storyNames">The story names at which the pier exists.</param>
        /// <param name="numberOfAreaObjects">The number of area objects in the pier at each story.</param>
        /// <param name="numberOfLineObjects">The number of line objects in the pier at each story.</param>
        /// <param name="axisAngles">The pier local axis angle at each story, defined as the angle between the global x-axis and the pier local 2-axis.</param>
        /// <param name="widthBottom">The width of the pier at the bottom of each story.</param>
        /// <param name="thicknessBottom">The thickness of the pier at the bottom of each story.</param>
        /// <param name="widthTop">The width of the pier at the top of each story.</param>
        /// <param name="thicknessTop">The thickness of the pier at the top of each story.</param>
        /// <param name="materialPropertyNames">The name of the pier material property at each story.</param>
        /// <param name="centerOfGravityBottomX">The x-coordinate of the center of gravity at the bottom of each story.</param>
        /// <param name="centerOfGravityBottomY">The y-coordinate of the center of gravity at the bottom of each story.</param>
        /// <param name="centerOfGravityBottomZ">The z-coordinate of the center of gravity at the bottom of each story.</param>
        /// <param name="centerOfGravityTopX">The x-coordinate of the center of gravity at the top of each story.</param>
        /// <param name="centerOfGravityTopY">The y-coordinate of the center of gravity at the top of each story.</param>
        /// <param name="centerOfGravityTopZ">The z-coordinate of the center of gravity at the top of each story.</param>
        public void GetSectionProperties(string name,
            out string[] storyNames,
            out int[] numberOfAreaObjects,
            out int[] numberOfLineObjects,
            out double[] axisAngles,
            out double[] widthBottom,
            out double[] thicknessBottom,
            out double[] widthTop,
            out double[] thicknessTop,
            out string[] materialPropertyNames,
            out double[] centerOfGravityBottomX,
            out double[] centerOfGravityBottomY,
            out double[] centerOfGravityBottomZ,
            out double[] centerOfGravityTopX,
            out double[] centerOfGravityTopY,
            out double[] centerOfGravityTopZ)
        {
            storyNames = new string[0];
            numberOfAreaObjects = new int[0];
            numberOfLineObjects = new int[0];
            axisAngles = new double[0];
            widthBottom = new double[0];
            thicknessBottom = new double[0];
            widthTop = new double[0];
            thicknessTop = new double[0];
            materialPropertyNames = new string[0];
            centerOfGravityBottomX = new double[0];
            centerOfGravityBottomY = new double[0];
            centerOfGravityBottomZ = new double[0];
            centerOfGravityTopX = new double[0];
            centerOfGravityTopY = new double[0];
            centerOfGravityTopZ = new double[0];

            _callCode = _sapModel.PierLabel.GetSectionProperties(name,
                                    ref _numberOfItems,
                                    ref storyNames,
                                    ref axisAngles,
                                    ref numberOfAreaObjects,
                                    ref numberOfLineObjects,
                                    ref widthBottom,
                                    ref thicknessBottom,
                                    ref widthTop,
                                    ref thicknessTop,
                                    ref materialPropertyNames,
                                    ref centerOfGravityBottomX,
                                    ref centerOfGravityBottomY,
                                    ref centerOfGravityBottomZ,
                                    ref centerOfGravityTopX,
                                    ref centerOfGravityTopY,
                                    ref centerOfGravityTopZ);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
#endregion
    }
}
#endif