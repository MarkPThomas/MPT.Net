// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="AreaModifiers.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign
{
    /// <summary>
    /// Represents the named set area modifiers in the application.
    /// A named set of property modifiers can be applied to a frame or area object during staged construction to change the property modifiers that were previously assigned or applied to the object. 
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.NamedAssign.IAreaModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class AreaModifiers : CSiApiBase, IAreaModifiers
    {
#region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaModifiers" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AreaModifiers(CSiApiSeed seed) : base(seed) { }

#endregion

#region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing area property modifier.
        /// </summary>
        /// <param name="currentName">The existing name of a defined area property modifier.</param>
        /// <param name="newName">The new name for the area property modifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.NamedAssign.ModifierArea.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the total number of defined area property modifiers in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Count()
        {
            return _sapModel.NamedAssign.ModifierArea.Count();
        }

        /// <summary>
        /// The function deletes a specified area property modifier.
        /// It returns an error if the specified property modifier can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing area property modifier.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.NamedAssign.ModifierArea.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.NamedAssign.ModifierArea.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
            return names;
        }

        // === Get/Set
        /// <summary>
        /// Returns the unitless modifier assignments.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public AreaModifier GetModifiers(string name)
        {
            double[] csiModifiers = new double[0];

            _callCode = _sapModel.NamedAssign.ModifierArea.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            AreaModifier modifiers = new AreaModifier();
            modifiers.FromArray(csiModifiers);
            return modifiers;
        }

        /// <summary>
        /// This function defines the modifier assignment for areas.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing areas.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(string name,
            AreaModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.NamedAssign.ModifierArea.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endregion
    }
}

#endif
