// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="SpandrelLabel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Represents a spandrel label object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.ISpandrelLabel" />
    public class SpandrelLabel : CSiApiBase, ISpandrelLabel
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="SpandrelLabel"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public SpandrelLabel(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined spandrel label property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined spandrel label property.</param>
        /// <param name="nameNew">New name for the spandrel label property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.SpandrelLabel.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified spandrel label property.
        /// </summary>
        /// <param name="name">The name of an existing spandrel label property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.SpandrelLabel.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            bool[] isMultiStory = new bool[0];
            _callCode = _sapModel.SpandrelLabel.GetNameList(ref _numberOfItems, ref names, ref isMultiStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        /// <summary>
        /// Retrieves the names of all defined spandrel label property.
        /// </summary>
        /// <param name="names">The spandrel label property names retrieved by the program.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        public void GetNameList(out string[] names,
            out bool[] isMultiStory)
        {
            names = new string[0];
            isMultiStory = new bool[0];
            _callCode = _sapModel.SpandrelLabel.GetNameList(ref _numberOfItems, ref names, ref isMultiStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// True: The Spandrel Label exists.
        /// </summary>
        /// <param name="name">The name of an existing Spandrel Label.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        public bool GetSpandrel(string name,
            out bool isMultiStory)
        {
            isMultiStory = false;
            _callCode = _sapModel.SpandrelLabel.GetSpandrel(name, ref isMultiStory);
            return _callCode == 0;
        }

        /// <summary>
        /// Adds a new Spandrel Label.
        /// </summary>
        /// <param name="name">The name of a Spandrel Label. 
        /// If this is the name of an existing spandrel label, that spandrel label is modified, otherwise a new spandrel label is added.</param>
        /// <param name="isMultiStory">True: Spandrel Label spans multiple story levels .</param>
        public void SetSpandrel(string name,
            bool isMultiStory)
        {
            _callCode = _sapModel.SpandrelLabel.SetSpandrel(name, isMultiStory);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion

    }
}
#endif