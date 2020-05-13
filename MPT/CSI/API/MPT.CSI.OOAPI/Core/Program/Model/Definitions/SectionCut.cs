// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="SectionCut.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Support;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions
{
    /// <summary>
    /// Class SectionCut.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOOAPiName" />
    public class SectionCut : CSiOOAPiName
    {
        #region Fields & Properties

        /// <summary>
        /// The results
        /// </summary>
        private readonly SectionCutResults _results;

        /// <summary>
        /// The analysis forces
        /// </summary>
        private List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> _analysisForces;
        /// <summary>
        /// Gets or sets the analysis forces.
        /// </summary>
        /// <value>The analysis forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> AnalysisForces
        {
            get
            {
                if (_analysisForces == null)
                {
                    FillAnalysisForces();
                }

                return _analysisForces;
            }
        }


        /// <summary>
        /// The design forces
        /// </summary>
        private List<Tuple<SectionCutResultsIdentifier, Forces>> _designForces;
        /// <summary>
        /// Gets or sets the design forces.
        /// </summary>
        /// <value>The design forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, Forces>> DesignForces
        {
            get
            {
                if (_designForces == null)
                {
                    FillDesignForces();
                }

                return _designForces;
            }
        }
        #endregion

        #region Initialization    
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="results">The results.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static SectionCut Factory(ApiCSiApplication app, SectionCutResults results, string uniqueName)
        {
            SectionCut item = new SectionCut(app, results, uniqueName);
            item.FillData();
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCut" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="results">The results.</param>
        /// <param name="name">The name.</param>
        protected SectionCut(ApiCSiApplication app, SectionCutResults results, string name) : base(app, name)
        {
            _results = results;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void FillData()
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

#else
            throw new NotImplementedException();
#endif
        }


        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="NotImplementedException"></exception>
        public override void ChangeName(string newName)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

#else
            throw new NotImplementedException();
#endif
        }


        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        internal override void Delete()
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

#else
            throw new NotImplementedException();
#endif
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a new quadrilateral to a section cut defined by quadrilaterals.
        /// </summary>
        /// <param name="xCoordinates">This is an array of four X coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="yCoordinates">This is an array of four Y coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="zCoordinates">This is an array of four Z coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddQuadrilateral(
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates)
        {

        }

        // === Get

        /// <summary>
        /// This function gets basic information about an existing section cut.
        /// </summary>
        /// <param name="groupName">The name of the group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <param name="numberQuadrilaterals">The number of quadrilateral cutting planes defined for the section cut.
        /// If this number is zero then the section cut is defined by the associated group.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetCutInfo(
            ref string groupName,
            ref eSectionResultType sectionCutType,
            ref int numberQuadrilaterals)
        {

        }

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {

        }

        /// <summary>
        /// Returns the coordinates of a quadrilateral cutting plane in a section cut defined by quadrilaterals.
        /// </summary>
        /// <param name="quadrilateralNumber">The number of a quadirilateral cutting plane in the section cut.</param>
        /// <param name="xCoordinates">This is an array of four X coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="yCoordinates">This is an array of four Y coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="zCoordinates">This is an array of four Z coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetQuadrilateral(
            int quadrilateralNumber,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates)
        {

        }

        // === Set

        /// <summary>
        /// Adds a new section cut defined by a group to the model or reinitializes an existing section cut to be defined by a group.
        /// </summary>
        /// <param name="groupName">The name of the group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddByGroup(
            string groupName,
            eSectionResultType sectionCutType)
        {

        }

        /// <summary>
        /// Adds a new section cut defined by a quadrilateral to the model or reinitializes an existing section cut to be defined by a quadrilateral.
        /// </summary>
        /// <param name="groupName">The name of the group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <param name="xCoordinates">This is an array of four X coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="yCoordinates">This is an array of four Y coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="zCoordinates">This is an array of four Z coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <exception cref="CSiException">
        /// xCoordinates + " has " + xCoordinates.Length + " items, but must have 4.
        /// or
        /// API_DEFAULT_ERROR_CODE</exception>
        public void AddByQuadrilateral(
            string groupName,
            eSectionResultType sectionCutType,
            ref double[] xCoordinates,
            ref double[] yCoordinates,
            ref double[] zCoordinates)
        {

        }

        // === Get/Set ===

        /// <summary>
        /// This function gets the advanced local axes data for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLocalAxesAdvancedAnalysis(
            ref bool isActive,
            ref eReferenceVector axisVectorOption,
            ref string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            ref int localPlaneByReferenceVector,
            ref eReferenceVector planeVectorOption,
            ref string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {

        }

        /// <summary>
        /// Sets the advanced local axes data for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="isActive">True: Advanced local axes exist.</param>
        /// <param name="axisVectorOption">Indicates the axis reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="axisCoordinateSystem">The coordinate system used to define the axis reference vector coordinate directions and the axis user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="axisVectorDirection">Indicates the axis reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Coordinate Direction.</param>
        /// <param name="axisPoint">Indicates the labels of two joints that define the axis reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is Two Joints.</param>
        /// <param name="axisReferenceVector">Defines the axis reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="axisVectorOption" /> is User Vector.</param>
        /// <param name="localPlaneByReferenceVector">This is 12, 13, 21, 23, 31 or 32, indicating that the local plane determined by the plane reference vector is the 1-2, 1-3, 2-1, 2-3, 3-1, or 3-2 plane.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeVectorOption">Indicates the plane reference vector option.
        /// This item applies only when <paramref name="isActive" /> is True.</param>
        /// <param name="planeCoordinateSystem">The coordinate system used to define the plane reference vector coordinate directions and the plane user vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction or User Vector.</param>
        /// <param name="planeVectorDirection">Indicates the plane reference vector primary and secondary coordinate directions, (0) and (1) respectively, taken at the object center in the specified coordinate system and used to determine the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Coordinate Direction.</param>
        /// <param name="planePoint">Indicates the labels of two joints that define the plane reference vector.
        /// Either of these joints may be specified as None to indicate the center of the specified object.
        /// If both joints are specified as None, they are not used to define the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is Two Joints.</param>
        /// <param name="planeReferenceVector">Defines the plane reference vector.
        /// This item applies when <paramref name="isActive" /> is True and <paramref name="planeVectorOption" /> is User Vector.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLocalAxesAdvancedAnalysis(
            bool isActive,
            eReferenceVector axisVectorOption,
            string axisCoordinateSystem,
            ref eReferenceVectorDirection[] axisVectorDirection,
            ref string[] axisPoint,
            ref double[] axisReferenceVector,
            int localPlaneByReferenceVector,
            eReferenceVector planeVectorOption,
            string planeCoordinateSystem,
            ref eReferenceVectorDirection[] planeVectorDirection,
            ref string[] planePoint,
            ref double[] planeReferenceVector)
        {

        }

        // ===

        /// <summary>
        /// This function gets the local axes angles for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="zRotation">Rotation about the z axis.</param>
        /// <param name="yRotation">The rotation about the Y' axis where Y' is the orientation of the Y axis after rotation about the Z axis.</param>
        /// <param name="xRotation">The rotation about the X'' axis where X'' is the orientation of the X axis after rotation about the Z axis and about the Y' axis.</param>
        /// <param name="isAdvanced">True: Advanced local axes are specified.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLocalAxesAnalysis(
            ref double zRotation,
            ref double yRotation,
            ref double xRotation,
            ref bool isAdvanced)
        {

        }

        /// <summary>
        /// Sets the local axes angles for an existing section cut whose result type is Analysis.
        /// </summary>
        /// <param name="zRotation">Rotation about the z axis.</param>
        /// <param name="yRotation">The rotation about the Y' axis where Y' is the orientation of the Y axis after rotation about the Z axis.</param>
        /// <param name="xRotation">The rotation about the X'' axis where X'' is the orientation of the X axis after rotation about the Z axis and about the Y' axis.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLocalAxesAnalysis(
            double zRotation,
            double yRotation,
            double xRotation)
        {

        }

        // ===

        /// <summary>
        /// This function gets the local axes angle for section cuts whose result type is Design (Wall, Spandrel or Slab).
        /// </summary>
        /// <param name="angle">For design local axes orientation type wall this is the angle from the global X to the local 2 axis.
        /// For orientation types spandrel and slab it is the angle from the global X to the local 1 axis.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLocalAxesAngleDesign(ref double angle)
        {

        }

        /// <summary>
        /// Sets the local axes angle for section cuts whose result type is Design (Wall, Spandrel or Slab).
        /// </summary>
        /// <param name="angle">For design local axes orientation type wall this is the angle from the global X to the local 2 axis.
        /// For orientation types spandrel and slab it is the angle from the global X to the local 1 axis.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLocalAxesAngleDesign(double angle)
        {

        }

        // ===

        /// <summary>
        /// This function gets the results location for an existing section cut.
        /// </summary>
        /// <param name="isDefault">True: The section cut results are reported at the default location.
        /// If so, the X, Y and Z items are ignored.</param>
        /// <param name="xCoordinate">The X coordinate of the section cut result location when it is not default.</param>
        /// <param name="yCoordinate">The Y coordinate of the section cut result location when it is not default.</param>
        /// <param name="zCoordinate">The Z coordinate of the section cut result location when it is not default.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetResultLocation(
            ref bool isDefault,
            ref double xCoordinate,
            ref double yCoordinate,
            ref double zCoordinate)
        {

        }

        /// <summary>
        /// Sets the results location for an existing section cut.
        /// </summary>
        /// <param name="isDefault">True: The section cut results are reported at the default location.
        /// If so, the X, Y and Z items are ignored.</param>
        /// <param name="xCoordinate">The X coordinate of the section cut result location when <paramref name="isDefault" /> is false.</param>
        /// <param name="yCoordinate">The Y coordinate of the section cut result location when <paramref name="isDefault" /> is false.</param>
        /// <param name="zCoordinate">The Z coordinate of the section cut result location when <paramref name="isDefault" /> is false.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetResultLocation(
            bool isDefault,
            double xCoordinate = 0,
            double yCoordinate = 0,
            double zCoordinate = 0)
        {

        }


        // ===

        /// <summary>
        /// This function gets the side of the elements from which results are obtained.
        /// </summary>
        /// <param name="side">This item is either 1 or 2 and indicates the side of the elements from which section cut results are obtained.
        /// Location depends on the section cut type, with 1 = Positive3, Top, Right, and 2 = Negative3, Bottom, Left for Analysis, Wall, or Spandrel/Slab.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetResultsSide(ref int side)
        {

        }

        /// <summary>
        /// Sets the side of the elements from which results are obtained.
        /// </summary>
        /// <param name="side">This item is either 1 or 2 and indicates the side of the elements from which section cut results are obtained.
        /// Location depends on the section cut type, with 1 = Positive3, Top, Right, and 2 = Negative3, Bottom, Left for Analysis, Wall, or Spandrel/Slab.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetResultsSide(int side)
        {

        }
#endif
        #endregion

        #region Results
        /// <summary>
        /// Fills the analysis forces.
        /// </summary>
        public void FillAnalysisForces()
        {
            _analysisForces = new List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>>();
            foreach (var result in _results.AnalysisForces)
            {
                if (result.Item1.SectionCutName == Name)
                {
                    _analysisForces.Add(result);
                }
            }
        }

        /// <summary>
        /// Fills the design forces.
        /// </summary>
        public void FillDesignForces()
        {
            _designForces = new List<Tuple<SectionCutResultsIdentifier, Forces>>();
            foreach (var result in _results.DesignForces)
            {
                if (result.Item1.SectionCutName == Name)
                {
                    _designForces.Add(result);
                }
            }
        }
        #endregion
    }
}
