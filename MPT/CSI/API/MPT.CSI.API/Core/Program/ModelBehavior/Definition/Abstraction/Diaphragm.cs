// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-03-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="Diaphragm.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction
{
    /// <summary>
    /// Represents a diaphragm object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.IDiaphragm" />
    public class Diaphragm : CSiApiBase, IDiaphragm
    {
#region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="Diaphragm"/> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Diaphragm(CSiApiSeed seed) : base(seed)
        {

        }
#endregion

#region Methods: Interface
        /// <summary>
        /// Changes the name of a defined diaphragm property.
        /// </summary>
        /// <param name="nameExisting">Existing name of a defined diaphragm property.</param>
        /// <param name="nameNew">New name for the diaphragm property.</param>
        public void ChangeName(string nameExisting,
            string nameNew)
        {
            _callCode = _sapModel.Diaphragm.ChangeName(nameExisting, nameNew);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Deletes the specified diaphragm property.
        /// </summary>
        /// <param name="name">The name of an existing diaphragm property.</param>
        public void Delete(string name)
        {
            _callCode = _sapModel.Diaphragm.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.Diaphragm.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        /// <summary>
        /// Retrieves the specified diaphragm rigidity.
        /// True: Diaphragm is semi-rigid.
        /// False: Diaphragm is rigid.
        /// </summary>
        /// <param name="name">The name of an existing diaphragm.</param>
        public bool GetDiaphragm(string name)
        {
            bool semiRigid = false;
            _callCode = _sapModel.Diaphragm.GetDiaphragm(name, ref semiRigid);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return semiRigid;
        }

        /// <summary>
        /// Adds a new diaphragm, or modifies an existing diaphragm.
        /// </summary>
        /// <param name="name">The name of an existing diaphragm.</param>
        /// <param name="semiRigid">True: Diaphragm is semi-rigid.</param>
        public void SetDiaphragm(string name,
            bool semiRigid)
        {
            _callCode = _sapModel.Diaphragm.SetDiaphragm(name, semiRigid);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}
#endif