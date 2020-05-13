// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-30-2018
// ***********************************************************************
// <copyright file="CSiOoApiBase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Support
{
    /// <summary>
    /// Represents the base unique object in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOOAPiName" />
    public abstract class CSiOoApiBase : CSiOOAPiName
    {
        /// <summary>
        /// Factories the specified unique name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="apiObject">The API object.</param>
        /// <param name="items">The items.</param>
        /// <returns>T.</returns>
        public static T Factory<T>(string uniqueName, CSiApiBase apiObject, Dictionary<string, T> items) where T : CSiOOAPiName, new()
        {
            if (items.Keys.Contains(uniqueName)) return items[uniqueName];

            T item = new T { Name = uniqueName };

            if (apiObject != null && Constants.FillAllProperties)
            {
                item.FillData();
            }

            items.Add(uniqueName, item);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiOoApiBase" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected CSiOoApiBase(ApiCSiApplication app, string name) : base(app, name)
        {
        }
    }
}
