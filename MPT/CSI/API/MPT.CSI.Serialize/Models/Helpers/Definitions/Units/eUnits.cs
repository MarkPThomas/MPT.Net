// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 10-02-2017
//
// Last Modified By : Mark
// Last Modified On : 10-02-2017
// ***********************************************************************
// <copyright file="eUnits.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.Units
{
    /// <summary>
    /// Standardized unit combinations available in the application.
    /// </summary>
    public enum eUnits
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The lb in f
        /// </summary>
        [Description("lb, in, F")]
        lb_in_F = 1,

        /// <summary>
        /// The lb ft f
        /// </summary>
        [Description("lb, ft, F")]
        lb_ft_F = 2,

        /// <summary>
        /// The kip in f
        /// </summary>
        [Description("kip, in, F")]
        kip_in_F = 3,

        /// <summary>
        /// The kip ft f
        /// </summary>
        [Description("kip, ft, F")]
        kip_ft_F = 4,

        /// <summary>
        /// The k n mm c
        /// </summary>
        [Description("KN, mm, C")]
        kN_mm_C = 5,

        /// <summary>
        /// The k n m c
        /// </summary>
        [Description("KN, m, C")]
        kN_m_C = 6,

        /// <summary>
        /// The KGF mm c
        /// </summary>
        [Description("kgf, mm, C")]
        kgf_mm_C = 7,

        /// <summary>
        /// The KGF m c
        /// </summary>
        [Description("kgf, m, C")]
        kgf_m_C = 8,

        /// <summary>
        /// The n mm c
        /// </summary>
        [Description("N, mm, C")]
        N_mm_C = 9,

        /// <summary>
        /// The n m c
        /// </summary>
        [Description("N, m, C")]
        N_m_C = 10,

        /// <summary>
        /// The ton mm c
        /// </summary>
        [Description("Ton, mm, C")]
        Ton_mm_C = 11,

        /// <summary>
        /// The ton m c
        /// </summary>
        [Description("Ton, m, C")]
        Ton_m_C = 12,

        /// <summary>
        /// The k n cm c
        /// </summary>
        [Description("KN, cm, C")]
        kN_cm_C = 13,

        /// <summary>
        /// The KGF cm c
        /// </summary>
        [Description("kgf, cm, C")]
        kgf_cm_C = 14,

        /// <summary>
        /// The n cm c
        /// </summary>
        [Description("N, cm, C")]
        N_cm_C = 15,

        /// <summary>
        /// The ton cm c
        /// </summary>
        [Description("Ton, cm, C")]
        Ton_cm_C = 16
    }
}
