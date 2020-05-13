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
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class JointResults.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Analysis.AnalysisResults" />
    /// <seealso cref="AnalysisResults" />
    public class JointResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the joint.
        /// </summary>
        /// <value>The name of the joint.</value>
        public string JointName { get; protected set; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
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
                    FillJointResponseSpectrum();
                }

                return _jointResponseSpectrum;
            }
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
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
                    FillJointDrifts();
                }

                return _jointDrifts;
            }
        }
#endif
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
                    FillJointAcceleration();
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
                    FillJointAccelerationAbsolute();
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
                    FillJointVelocity();
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
                    FillJointVelocityAbsolute();
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
                    FillJointDisplacement();
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
                    FillJointDisplacementAbsolute();
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
                    FillJointReaction();
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
                    FillModeShape();
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
                    FillAssembledJointMass();
                }

                return _assembledJointMass;
            }
        }
        #endregion
        
        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="JointResults"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        internal JointResults(ApiCSiApplication app, string name) : base(app)
        {
            JointName = name;
        }

        #endregion

        #region Fill
        /// <summary>
        /// Fills the results.
        /// </summary>
        public override void FillResults()
        {
#if BUILD_ETABS2016 || BUILD_ETABS2017
            FillJointDrifts();
#endif
            FillJointAcceleration();
            FillJointAccelerationAbsolute();
            FillJointVelocity();
            FillJointVelocityAbsolute();
            FillJointDisplacement();
            FillJointDisplacementAbsolute();
            FillJointReaction();
            FillModeShape();
        }

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
#if BUILD_ETABS2016 || BUILD_ETABS2017
            _jointDrifts.Clear();
#endif
            _jointAcceleration.Clear();
            _jointAccelerationAbsolute.Clear();
            _jointVelocity.Clear();
            _jointVelocityAbsolute.Clear();
            _jointDisplacement.Clear();
            _jointDisplacementAbsolute.Clear();
            _jointReaction.Clear();
            _modeShape.Clear();
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
       public void FillJointResponseSpectrum()
        {
            _jointResponseSpectrum = GetJointResponseSpectrum(JointName, NamedSet);
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Fills the joint drifts.
        /// </summary>
        public void FillJointDrifts()
        {
            _jointDrifts = GetJointDrifts(_apiAnalysisResults, JointName);
        }
#endif

        /// <summary>
        /// Fills the joint acceleration.
        /// </summary>
        public void FillJointAcceleration()
        {
            _jointAcceleration = GetJointAcceleration(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint acceleration absolute.
        /// </summary>
        public void FillJointAccelerationAbsolute()
        {
            _jointAccelerationAbsolute = GetJointAccelerationAbsolute(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint velocity.
        /// </summary>
        public void FillJointVelocity()
        {
            _jointVelocity = GetJointVelocity(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint velocity absolute.
        /// </summary>
        public void FillJointVelocityAbsolute()
        {
            _jointVelocityAbsolute = GetJointVelocityAbsolute(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint displacement.
        /// </summary>
        public void FillJointDisplacement()
        {
            _jointDisplacement = GetJointDisplacement(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint displacement absolute.
        /// </summary>
        public void FillJointDisplacementAbsolute()
        {
            _jointDisplacementAbsolute = GetJointDisplacementAbsolute(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint reaction.
        /// </summary>
        public void FillJointReaction()
        {
            _jointReaction = GetJointReaction(_apiAnalysisResults, JointName);
        }

        /// <summary>
        /// Fills the joint reaction.
        /// </summary>
        public void FillModeShape()
        {
            _modeShape = GetModeShape(_apiAnalysisResults, JointName);
        }


        /// <summary>
        /// Fills the assembled joint mass.
        /// </summary>
        public void FillAssembledJointMass()
        {
            _assembledJointMass = GetAssembledJointMass(_apiAnalysisResults, JointName);
        }
        #endregion

        #region Static

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
       public static List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>> GetJointResponseSpectrum(
            Results app, 
            string name, 
            string namedSet, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointResponseSpectrum(name,
                itemType,
                namedSet,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var coordinateSystems,
                out var directions,
                out var damping,
                out var percentSpectrumWidening,
                out var abscissaValues,
                out var ordinateValues);

            List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>> jointResults = new List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                JointResponseSpectrumIdentifier identifier =
                    new JointResponseSpectrumIdentifier
                    {
                        LoadCase = loadCases[i],
                        ObjectName = objectNames[i],
                        ElementName = elementNames[i],
                        CoordinateSystem = coordinateSystems[i],
                        Direction = directions[i]
                    };

                JointResponseSpectrum jointResponseSpectrum = new JointResponseSpectrum
                {
                    Damping = damping[i],
                    PercentSpectrumWidening = percentSpectrumWidening[i],
                    AbscissaValues = abscissaValues[i],
                    OrdinateValues = ordinateValues[i]
                };

                jointResults.Add(new Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>(identifier, jointResponseSpectrum));
            }

            return jointResults;
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets the joint drifts.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;JointLabelNameResultsIdentifier, JointDrifts&gt;&gt;.</returns>
        public static List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>> GetJointDrifts(
            IResults app,
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointDrifts(
                out var storyNames,
                out var labels,
                out var names,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var displacementsX,
                out var displacementsY,
                out var driftsX,
                out var driftsY);

            List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>> jointDrifts = new List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>>();
            for (int i = 0; i < loadCases.Length; i++)
            {
                JointLabelNameResultsIdentifier identifier =
                    new JointLabelNameResultsIdentifier
                    {
                        LoadCase = loadCases[i],
                        StepType = stepTypes[i],
                        StepNumber = stepNumbers[i],
                        StoryName = storyNames[i],
                        Label = labels[i],
                        JointName = names[i]
                    };

                JointDrifts drifts = new JointDrifts
                {
                    DisplacementX = displacementsX[i],
                    DisplacementY = displacementsY[i],
                    DriftX = driftsX[i],
                    DriftY = driftsY[i]
                };

                jointDrifts.Add(new Tuple<JointLabelNameResultsIdentifier, JointDrifts>(identifier, drifts));
            }

            return jointDrifts;
        }
#endif

        /// <summary>
        /// Gets the joint acceleration.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointAcceleration(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointAcceleration(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
            {
                LoadCase = t,
                ObjectName = objectNames[i],
                ElementName = elementNames[i],
                StepType = stepTypes[i],
                StepNumber = stepNumbers[i]
            })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the joint acceleration absolute.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointAccelerationAbsolute(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointAccelerationAbsolute(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the joint velocity.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointVelocity(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointVelocity(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the joint velocity absolute.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointVelocityAbsolute(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointVelocityAbsolute(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the joint displacement.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointDisplacement(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointDisplacement(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the joint displacement absolute.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointDisplacementAbsolute(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointDisplacementAbsolute(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the joint reaction.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, AnalysisLoads>> GetJointReaction(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.JointReaction(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var forces);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, AnalysisLoads>(result, forces[i]))
                .ToList();
        }

        /// <summary>
        /// Gets the mode shape.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetModeShape(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.ModeShape(name,
                itemType,
                out var objectNames,
                out var elementNames,
                out var loadCases,
                out var stepTypes,
                out var stepNumbers,
                out var displacements);

            return loadCases.Select((t, i) => new ObjectResultsIdentifier()
                {
                    LoadCase = t,
                    ObjectName = objectNames[i],
                    ElementName = elementNames[i],
                    StepType = stepTypes[i],
                    StepNumber = stepNumbers[i]
                })
                .Select((result, i) => new Tuple<ObjectResultsIdentifier, Deformations>(result, displacements[i]))
                .ToList();
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
         /// <summary>
        /// Gets the assembled joint mass for the specified point elements.
        /// </summary>
        /// <param name="name">The name of an existing object, element, or group of objects, depending on the value of the <paramref name="itemType" /> item.</param>
        /// <param name="massSourceName">The name of an existing mass source definition.
        /// If this value is left empty or unrecognized, data for all mass sources will be returned..</param>
        /// <param name="itemType">If this item is <see cref="eItemTypeElement.ObjectElement" />, the result request is for the elements corresponding to the object specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.Element" />, the result request is for the element specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.GroupElement" />, the result request is for the elements corresponding to all objects included in the group specified by the <paramref name="name" /> item.
        /// If this item is <see cref="eItemTypeElement.SelectionElement" />, the result request is for elements corresponding to all selected objects, and the <paramref name="name" /> item is ignored.</param>
        /// <returns>List&lt;AssembledJointMass&gt;.</returns>
        public static List<AssembledJointMass> GetAssembledJointMass(
            IResults app, 
            string name, 
            string massSourceName, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.AssembledJointMass(massSourceName, 
                name,
                itemType,
                out var pointElementNames,
                out var massSourceNames,
                out var masses);

            return pointElementNames.Select((t, i) => new AssembledJointMass()
                {
                    PointElementName = t,
                    Mass = masses[i],
                    MassSourceName = massSourceNames[i]
            })
                .ToList();
        }
#else
        /// <summary>
        /// Gets the assembled joint mass.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;AssembledJointMass&gt;.</returns>
        public static List<AssembledJointMass> GetAssembledJointMass(
            IResults app, 
            string name, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            app.AssembledJointMass(name,
                itemType,
                out var pointElementNames,
                out var masses);

            return pointElementNames.Select((t, i) => new AssembledJointMass()
                {
                    PointElementName = t,
                    Mass = masses[i]
                })
                .ToList();
        }
#endif
        #endregion
    }
}
