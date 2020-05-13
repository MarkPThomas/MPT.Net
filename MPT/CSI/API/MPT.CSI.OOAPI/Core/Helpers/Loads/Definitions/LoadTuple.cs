// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="LoadTuple.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions
{
    /// <summary>
    /// Class LoadTuple.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LoadTuple<T> : ApiProperty
    {
        /// <summary>
        /// Gets or sets the load.
        /// </summary>
        /// <value>The load.</value>
        public T Load { get; set; }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        public double Scale { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadTuple{T}"/> class.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <param name="scale">The scale.</param>
        public LoadTuple(T load, double scale = 1)
        {
            Load = load;
            Scale = scale;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
