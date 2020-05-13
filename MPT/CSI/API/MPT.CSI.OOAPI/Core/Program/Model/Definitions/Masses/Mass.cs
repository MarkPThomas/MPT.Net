// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-23-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="Mass.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Helpers;
using ApiMass = MPT.CSI.API.Core.Helpers.Mass;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses
{
    /// <summary>
    /// Class Mass.
    /// </summary>
    public class Mass
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is local coordinate system.
        /// </summary>
        /// <value><c>true</c> if this instance is local coordinate system; otherwise, <c>false</c>.</value>
        public bool IsLocalCoordinateSystem { get; protected set; } = true;
        /// <summary>
        /// The mass
        /// </summary>
        private readonly ApiMass _mass;
        /// <summary>
        /// The mass as weight
        /// </summary>
        private MassWeight _massAsWeight;
        /// <summary>
        /// The mass as volume
        /// </summary>
        private MassVolume _massAsVolume;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mass"/> class.
        /// </summary>
        /// <param name="mass">The mass.</param>
        /// <param name="isLocalCoordinateSystem">if set to <c>true</c> [is local coordinate system].</param>
        internal Mass(ApiMass mass, bool isLocalCoordinateSystem = true)
        {
            _mass = mass;
            IsLocalCoordinateSystem = isLocalCoordinateSystem;
        }

        /// <summary>
        /// Gets the mass.
        /// </summary>
        /// <returns>ApiMass.</returns>
        public ApiMass GetMass()
        {
            return _mass;
        }

        /// <summary>
        /// Gets the mass by weight.
        /// </summary>
        /// <returns>MassWeight.</returns>
        public MassWeight GetMassByWeight()
        {
            // TODO: Finish GetMassByWeight
            return _massAsWeight;
        }

        /// <summary>
        /// Gets the mass by volume.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="materialProperty">The material property.</param>
        /// <returns>MassVolume.</returns>
        public MassVolume GetMassByVolume(string materialProperty)
        {
            // TODO: Finish GetMassByVolume.
            return _massAsVolume;
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="ArgumentException">Masses must both be in local coordinate systems in order to be added together.</exception>
        public static Mass operator +(Mass a, Mass b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (!a.IsLocalCoordinateSystem || !b.IsLocalCoordinateSystem)
            {
                throw new ArgumentException("Masses must both be in local coordinate systems in order to be added together.");
            }

            ApiMass massA = a.GetMass();
            ApiMass massB = b.GetMass();

            ApiMass mass = new ApiMass()
            {
                U1 = massA.U1 + massB.U1,
                U2 = massA.U2 + massB.U2,
                U3 = massA.U3 + massB.U3,
                R1 = massA.R1 + massB.R1,
                R2 = massA.R2 + massB.R2,
                R3 = massA.R3 + massB.R3
            };

            return new Mass(mass);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="ArgumentException">Masses must both be in local coordinate systems in order to be subtracted from each other.</exception>
        public static Mass operator -(Mass a, Mass b)
        {
            if (a == null) return b;
            if (b == null) return a;
            if (!a.IsLocalCoordinateSystem || !b.IsLocalCoordinateSystem)
            {
                throw new ArgumentException("Masses must both be in local coordinate systems in order to be subtracted from each other.");
            }

            ApiMass massA = a.GetMass();
            ApiMass massB = b.GetMass();

            ApiMass mass = new ApiMass()
            {
                U1 = massA.U1 - massB.U1,
                U2 = massA.U2 - massB.U2,
                U3 = massA.U3 - massB.U3,
                R1 = massA.R1 - massB.R1,
                R2 = massA.R2 - massB.R2,
                R3 = massA.R3 - massB.R3
            };

            return new Mass(mass);
        }
    }
}
