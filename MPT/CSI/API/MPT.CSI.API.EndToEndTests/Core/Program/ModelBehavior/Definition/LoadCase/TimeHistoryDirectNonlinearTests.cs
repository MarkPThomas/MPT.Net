﻿using NUnit.Framework;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;



namespace MPT.CSI.API.EndToEndTests.Core.Program.ModelBehavior.Definition.LoadCase
{
    [TestFixture]
    public class TimeHistoryDirectNonlinear_Get : CsiGet
    {
      
        public void GetLoads(string name,
            ref eLoadType[] loadTypes,
            ref string[] loadNames,
            ref string[] functions,
            ref double[] scaleFactor,
            ref double[] timeFactor,
            ref double[] arrivalTime,
            ref string[] coordinateSystems,
            ref double[] angles)
        {
          
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        
        public void GetInitialCase(string name,
            ref string initialCase)
        {
          
        }
        
        
        public void GetDampingProportional(string name,
            ref eDampingTypeProportional dampingType,
            ref double massProportionalDampingCoefficient,
            ref double stiffnessProportionalDampingCoefficient,
            ref double periodOrFrequencyPt1,
            ref double periodOrFrequencyPt2,
            ref double dampingPt1,
            ref double dampingPt2)
        {
          
        }
        
        
        public void GetTimeStep(string name,
            ref int numberOfOutputSteps,
            ref double timeStepSize)
        {
          
        }

        
        public void GetTimeIntegration(string name,
            ref eTimeIntegrationType integrationType,
            ref double alpha,
            ref double beta,
            ref double gamma,
            ref double theta,
            ref double alphaM)
        {
          
        }
        
        
        public void GetMassSource(string name,
            ref string sourceName)
        {
          
        }
        
        
        public void GetGeometricNonlinearity(string name,
            ref eGeometricNonlinearity geometricNonlinearityType)
        {
          
        }

        
        public void GetSolutionControlParameters(string name,
            ref double maxSubstepSize,
            ref double minSubstepSize,
            ref int maxConstantStiffnessIterationsPerStep,
            ref int maxNewtonRaphsonIterationsPerStep,
            ref double relativeIterationConvergenceTolerance,
            ref bool useEventStepping,
            ref double relativeEventLumpingTolerance,
            ref int maxNumberLineSearches,
            ref double relativeLineSearchAcceptanceTolerance,
            ref double lineSearchStepFactor)
        {
          
        }
#endif
    }
    
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    [TestFixture]
    public class TimeHistoryDirectNonlinear_Set : CsiSet
    {
       
        public void SetCase(string name)
        {
          
        }

        
        public void SetInitialCase(string name,
            string initialCase)
        {
          
        }
        
        
        public void SetLoads(string name,
            eLoadType[] loadTypes,
            string[] loadNames,
            string[] functions,
            double[] scaleFactor,
            double[] timeFactor,
            double[] arrivalTime,
            string[] coordinateSystems,
            double[] angles)
        {
          
        }

        
        public void SetDampingProportional(string name,
            eDampingTypeProportional dampingType,
            double massProportionalDampingCoefficient,
            double stiffnessProportionalDampingCoefficient,
            double periodOrFrequencyPt1,
            double periodOrFrequencyPt2,
            double dampingPt1,
            double dampingPt2)
        {
          
        }

        
        public void SetTimeStep(string name,
            int numberOfOutputSteps,
            double timeStepSize)
        {
          
        }

        
        public void SetTimeIntegration(string name,
            eTimeIntegrationType integrationType,
            double alpha,
            double beta,
            double gamma,
            double theta,
            double alphaM)
        {
          
        }
        
        
        public void SetMassSource(string name,
            string sourceName)
        {
          
        }
        
        
        public void SetGeometricNonlinearity(string name,
            eGeometricNonlinearity geometricNonlinearityType)
        {
          
        }
      

        
        public void SetSolutionControlParameters(string name,
            double maxSubstepSize,
            double minSubstepSize,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor)
        {
          
        }
    }
#endif
}
