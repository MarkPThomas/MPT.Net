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
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    /// <summary>
    /// Class JointResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class JointResults : AnalysisResults
    {
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

        public List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>> JointResponseSpectrum { get; protected set; }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets or sets the joint drifts.
        /// </summary>
        /// <value>The joint drifts.</value>
        public List<Tuple<LabelNameResultsIdentifier, JointDrifts>> JointDrifts { get; protected set; }
#endif
        /// <summary>
        /// Gets or sets the joint acceleration.
        /// </summary>
        /// <value>The joint acceleration.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointAcceleration { get; protected set; }

        /// <summary>
        /// Gets or sets the joint acceleration absolute.
        /// </summary>
        /// <value>The joint acceleration absolute.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointAccelerationAbsolute { get; protected set; }

        /// <summary>
        /// Gets or sets the joint velocity.
        /// </summary>
        /// <value>The joint velocity.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointVelocity { get; protected set; }

        /// <summary>
        /// Gets or sets the joint velocity absolute.
        /// </summary>
        /// <value>The joint velocity absolute.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointVelocityAbsolute { get; protected set; }

        /// <summary>
        /// Gets or sets the joint displacement.
        /// </summary>
        /// <value>The joint displacement.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointDisplacement { get; protected set; }

        /// <summary>
        /// Gets or sets the joint displacement absolute.
        /// </summary>
        /// <value>The joint displacement absolute.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> JointDisplacementAbsolute { get; protected set; }

        /// <summary>
        /// Gets or sets the joint reaction.
        /// </summary>
        /// <value>The joint reaction.</value>
        public List<Tuple<ObjectResultsIdentifier, AnalysisLoads>> JointReaction { get; protected set; }


        /// <summary>
        /// Gets or sets the mode shape.
        /// </summary>
        /// <value>The mode shape.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> ModeShape { get; protected set; }


        /// <summary>
        /// Gets or sets the assembled joint mass.
        /// </summary>
        /// <value>The assembled joint mass.</value>
        public List<AssembledJointMass> AssembledJointMass { get; protected set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="JointResults" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public JointResults(string name)
        {
            JointName = name;
        }

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
            JointDrifts?.Clear();
#endif
            JointAcceleration?.Clear();
            JointAccelerationAbsolute?.Clear();
            JointVelocity?.Clear();
            JointVelocityAbsolute?.Clear();
            JointDisplacement?.Clear();
            JointDisplacementAbsolute?.Clear();
            JointReaction?.Clear();
            ModeShape?.Clear();
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
       public void FillJointResponseSpectrum()
        {
            JointResponseSpectrum = GetJointResponseSpectrum(JointName, NamedSet);
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Fills the joint drifts.
        /// </summary>
        public void FillJointDrifts()
        {
            JointDrifts = new List<Tuple<LabelNameResultsIdentifier, JointDrifts>>();
            if (Registry.JointDrifts.Count == 0)
            {
                Registry.JointDrifts = GetJointDrifts(JointName);
            }

            foreach (var result in Registry.JointDrifts)
            {
                if (result.Item1.JointName != JointName) continue;
                LabelNameResultsIdentifier identifier =
                    new LabelNameResultsIdentifier
                    {
                        LoadCase = result.Item1.LoadCase,
                        StepType = result.Item1.StepType,
                        StepNumber = result.Item1.StepNumber,
                        StoryName = result.Item1.StoryName,
                        Label = result.Item1.Label
                    };
                JointDrifts.Add(new Tuple<LabelNameResultsIdentifier, JointDrifts>(identifier, result.Item2));
            }
        }
#endif

        /// <summary>
        /// Fills the joint acceleration.
        /// </summary>
        public void FillJointAcceleration()
        {
            JointAcceleration = GetJointAcceleration(JointName);
        }

        /// <summary>
        /// Fills the joint acceleration absolute.
        /// </summary>
        public void FillJointAccelerationAbsolute()
        {
            JointAccelerationAbsolute = GetJointAccelerationAbsolute(JointName);
        }

        /// <summary>
        /// Fills the joint velocity.
        /// </summary>
        public void FillJointVelocity()
        {
            JointVelocity = GetJointVelocity(JointName);
        }

        /// <summary>
        /// Fills the joint velocity absolute.
        /// </summary>
        public void FillJointVelocityAbsolute()
        {
            JointVelocityAbsolute = GetJointVelocityAbsolute(JointName);
        }

        /// <summary>
        /// Fills the joint displacement.
        /// </summary>
        public void FillJointDisplacement()
        {
            JointDisplacement = GetJointDisplacement(JointName);
        }

        /// <summary>
        /// Fills the joint displacement absolute.
        /// </summary>
        public void FillJointDisplacementAbsolute()
        {
            JointDisplacementAbsolute = GetJointDisplacementAbsolute(JointName);
        }

        /// <summary>
        /// Fills the joint reaction.
        /// </summary>
        public void FillJointReaction()
        {
            JointReaction = GetJointReaction(JointName);
        }

        /// <summary>
        /// Fills the joint reaction.
        /// </summary>
        public void FillModeShape()
        {
            ModeShape = GetModeShape(JointName);
        }


        /// <summary>
        /// Fills the assembled joint mass.
        /// </summary>
        public void FillAssembledJointMass()
        {
            AssembledJointMass = GetAssembledJointMass(JointName);
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17 && !BUILD_CSiBridgev16 && !BUILD_CSiBridgev17
       public static List<Tuple<JointResponseSpectrumIdentifier, JointResponseSpectrum>> GetJointResponseSpectrum(
            string name, 
            string namedSet, 
            eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointResponseSpectrum(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;JointLabelNameResultsIdentifier, JointDrifts&gt;&gt;.</returns>
        public static List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>> GetJointDrifts(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointDrifts(
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

            List < Tuple < JointLabelNameResultsIdentifier, JointDrifts >> jointDrifts = new List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>>();
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointAcceleration(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointAcceleration(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointAccelerationAbsolute(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointAccelerationAbsolute(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointVelocity(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointVelocity(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointVelocityAbsolute(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointVelocityAbsolute(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointDisplacement(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointDisplacement(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetJointDisplacementAbsolute(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointDisplacementAbsolute(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, AnalysisLoads&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, AnalysisLoads>> GetJointReaction(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.JointReaction(name,
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;Tuple&lt;ObjectResultsIdentifier, Deformations&gt;&gt;.</returns>
        public static List<Tuple<ObjectResultsIdentifier, Deformations>> GetModeShape(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.ModeShape(name,
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
        public static List<AssembledJointMass> GetAssembledJointMass(string name, string massSourceName, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.AssembledJointMass(massSourceName, 
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
        /// <param name="name">The name.</param>
        /// <param name="itemType">LoadType of the item.</param>
        /// <returns>List&lt;AssembledJointMass&gt;.</returns>
        public static List<AssembledJointMass> GetAssembledJointMass(string name, eItemTypeElement itemType = eItemTypeElement.ObjectElement)
        {
            _analysisResults.AssembledJointMass(name,
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
    }
}
