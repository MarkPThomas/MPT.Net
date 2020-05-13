// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="JointResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;
using AnalysisLoads = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.Loads;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class JointResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class JointResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the joint.
        /// </summary>
        /// <value>The name of the joint.</value>
        public string JointName { get; protected set; }
        
        /// <summary>
        /// The name of an existing joint response spectrum named set.
        /// See <see cref="API.Core.Program.ModelBehavior.Definition.NamedSets.SetJointRespSpec" />.
        /// </summary>
        /// <value>The named set.</value>
        public string NamedSet { get; set; }

        private List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>> _jointResponseSpectrum;
        public List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>> JointResponseSpectrum
        {
            get
            {
                if (_jointResponseSpectrum == null)
                {

                }

                return _jointResponseSpectrum;
            }
        }

        /// <summary>
        /// The joint drifts
        /// </summary>
        private List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>> _jointDrifts;
        /// <summary>
        /// Gets or sets the joint drifts.
        /// </summary>
        /// <value>The joint drifts.</value>
        public List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>> JointDrifts
        {
            get
            {
                if (_jointDrifts == null)
                {

                }

                return _jointDrifts;
            }
        }

        /// <summary>
        /// The joint acceleration
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _jointAcceleration;
        /// <summary>
        /// Gets or sets the joint acceleration.
        /// </summary>
        /// <value>The joint acceleration.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointAcceleration
        {
            get
            {
                if (_jointAcceleration == null)
                {
                    
                }

                return _jointAcceleration;
            }
        }

        /// <summary>
        /// The joint acceleration absolute
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _jointAccelerationAbsolute;
        /// <summary>
        /// Gets or sets the joint acceleration absolute.
        /// </summary>
        /// <value>The joint acceleration absolute.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointAccelerationAbsolute
        {
            get
            {
                if (_jointAccelerationAbsolute == null)
                {
                   
                }

                return _jointAccelerationAbsolute;
            }
        }

        /// <summary>
        /// The joint velocity
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _jointVelocity;
        /// <summary>
        /// Gets or sets the joint velocity.
        /// </summary>
        /// <value>The joint velocity.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointVelocity
        {
            get
            {
                if (_jointVelocity == null)
                {
                   
                }

                return _jointVelocity;
            }
        }

        /// <summary>
        /// The joint velocity absolute
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _jointVelocityAbsolute;
        /// <summary>
        /// Gets or sets the joint velocity absolute.
        /// </summary>
        /// <value>The joint velocity absolute.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointVelocityAbsolute
        {
            get
            {
                if (_jointVelocityAbsolute == null)
                {
                    
                }

                return _jointVelocityAbsolute;
            }
        }

        /// <summary>
        /// The joint displacement
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _jointDisplacement;
        /// <summary>
        /// Gets or sets the joint displacement.
        /// </summary>
        /// <value>The joint displacement.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointDisplacement
        {
            get
            {
                if (_jointDisplacement == null)
                {
                    
                }

                return _jointDisplacement;
            }
        }

        /// <summary>
        /// The joint displacement absolute
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _jointDisplacementAbsolute;
        /// <summary>
        /// Gets or sets the joint displacement absolute.
        /// </summary>
        /// <value>The joint displacement absolute.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointDisplacementAbsolute
        {
            get
            {
                if (_jointDisplacementAbsolute == null)
                {
                   
                }

                return _jointDisplacementAbsolute;
            }
        }

        /// <summary>
        /// The joint reaction
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, AnalysisLoads>> _jointReaction;
        /// <summary>
        /// Gets or sets the joint reaction.
        /// </summary>
        /// <value>The joint reaction.</value>
        public List<Tuple<ObjectResultsIdentifier, AnalysisLoads>> JointReaction
        {
            get
            {
                if (_jointReaction == null)
                {
                 
                }

                return _jointReaction;
            }
        }

        /// <summary>
        /// The mode shape
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _modeShape;
        /// <summary>
        /// Gets or sets the mode shape.
        /// </summary>
        /// <value>The mode shape.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> ModeShape
        {
            get
            {
                if (_modeShape == null)
                {
                   
                }

                return _modeShape;
            }
        }

        /// <summary>
        /// The assembled joint mass
        /// </summary>
        private List<AssembledJointMass> _assembledJointMass;
        /// <summary>
        /// Gets or sets the assembled joint mass.
        /// </summary>
        /// <value>The assembled joint mass.</value>
        public List<AssembledJointMass> AssembledJointMass
        {
            get
            {
                if (_assembledJointMass == null)
                {
                   
                }

                return _assembledJointMass;
            }
        }
        #endregion
        
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="JointResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal JointResults(string name)
        {
            JointName = name;
        }

        #endregion

        #region Fill

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            _jointDrifts.Clear();
            _jointAcceleration.Clear();
            _jointAccelerationAbsolute.Clear();
            _jointVelocity.Clear();
            _jointVelocityAbsolute.Clear();
            _jointDisplacement.Clear();
            _jointDisplacementAbsolute.Clear();
            _jointReaction.Clear();
            _modeShape.Clear();
        }
        #endregion
    }
}
