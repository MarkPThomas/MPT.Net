using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.TimeDependent;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the material properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface IMaterialProperties: IChangeableName, ICountable, IDeletable, IListableNames
    {
        #region Properties                              
        /// <summary>
        /// Gets the time dependent concrete.
        /// </summary>
        /// <value>The time dependent concrete.</value>
        TimeDependentConcrete TimeDependentConcrete { get; }

        /// <summary>
        /// Gets the time dependent tendon.
        /// </summary>
        /// <value>The time dependent tendon.</value>
        TimeDependentTendon TimeDependentTendon { get; }
        #endregion

        #region Methods: Query

        /// <summary>
        /// Returns the material type for the specified material.
        /// </summary>
        /// <param name="name">The name of an existing material propert.</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="symmetryType">Type of the material directional symmetry.</param>
        void GetMaterialType(string name,
            out eMaterialPropertyType materialType,
            out eMaterialSymmetryType symmetryType);

        #endregion

        #region Methods: Creation        

        /// <summary>
        /// Adds the material.
        /// Returns the name that the program ultimately assigns for the object.
        /// If no <paramref name="uniqueName" /> is specified, the program assigns a default name to the object.
        /// If a <paramref name="uniqueName" /> is specified and that name is not used for another object, the <paramref name="uniqueName" /> is assigned to the object; otherwise a default name is assigned to the object.
        /// </summary>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="region">The region name of the material property that is user-predefined in the file "CSiMaterialLibrary*.xml" located in subfolder "Property Libraries" under the installation.</param>
        /// <param name="standardName">The <paramref name="standardName"/> name of the material property with the specified <paramref name="materialType"/> within the specified region.</param>
        /// <param name="grade">The Grade name of the material property with the specified <paramref name="materialType"/> within the specified region and <paramref name="standardName"/>.</param>
        /// <param name="uniqueName">This is an optional user specified name for the material property. 
        /// If a <paramref name="uniqueName"/> is specified and that name is already used for another material property, the program ignores the <paramref name="uniqueName"/>.</param>
        string AddMaterial(
            eMaterialPropertyType materialType,
            string region,
            string standardName,
            string grade,
            string uniqueName = "");


        /// <summary>
        /// Returns some basic material property data.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="color">The display color assigned to the material.</param>
        /// <param name="notes">The notes, if any, assigned to the material.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the material.</param>
        void GetMaterial(string name,
            out eMaterialPropertyType materialType,
            out int color,
            out string notes,
            out string GUID);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // Deprecated. Use AddMaterial instead
        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="name">The name of an existing or new material property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added</param>
        /// <param name="materialType">Type of the material.</param>
        /// <param name="color">The display color assigned to the material.</param>
        /// <param name="notes">The notes, if any, assigned to the material.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the material.
        /// If this item is input as Default, the program assigns a GUID to the material</param>
        void SetMaterial(string name,
            eMaterialPropertyType materialType,
            int color = -1,
            string notes = "",
            string GUID = "");
#endif

        /// <summary>
        /// Returns the  additional material damping data for the material.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modalDampingRatio">The modal damping ratio.</param>
        /// <param name="viscousMassCoefficient">The mass coefficient for viscous proportional damping.</param>
        /// <param name="viscousStiffnessCoefficient">The stiffness coefficient for viscous proportional damping.</param>
        /// <param name="hystereticMassCoefficient">The mass coefficient for hysteretic proportional damping.</param>
        /// <param name="hystereticStiffnessCoefficient">The stiffness coefficient for hysteretic proportional damping.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetDamping(string name,
            out double modalDampingRatio,
            out double viscousMassCoefficient,
            out double viscousStiffnessCoefficient,
            out double hystereticMassCoefficient,
            out double hystereticStiffnessCoefficient,
            double temperature = 0);

        /// <summary>
        /// Sets the additional material damping data for the material.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modalDampingRatio">The modal damping ratio.</param>
        /// <param name="viscousMassCoefficient">The mass coefficient for viscous proportional damping.</param>
        /// <param name="viscousStiffnessCoefficient">The stiffness coefficient for viscous proportional damping.</param>
        /// <param name="hystereticMassCoefficient">The mass coefficient for hysteretic proportional damping.</param>
        /// <param name="hystereticStiffnessCoefficient">The stiffness coefficient for hysteretic proportional damping.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetDamping(string name,
            double modalDampingRatio,
            double viscousMassCoefficient,
            double viscousStiffnessCoefficient,
            double hystereticMassCoefficient,
            double hystereticStiffnessCoefficient,
            double temperature = 0);


        /// <summary>
        /// Returns the weight per unit volume and mass per unit volume of the material.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="weightPerVolume">The weight per unit volume for the material. [F/L^3].</param>
        /// <param name="massPerVolume">The mass per unit volume for the material. [M/L^3].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetWeightAndMass(string name,
            out double weightPerVolume,
            out double massPerVolume,
            double temperature = 0);

        /// <summary>
        /// Assigns weight per unit volume or mass per unit volume to a material property.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="perUnitVolumeOption">The per unit volume option.
        /// If the weight is specified, the corresponding mass is program calculated based on the specified weight. 
        /// Similarly, if the mass is specified, the corresponding weight is program calculated based on the specified mass.</param>
        /// <param name="value">This is either the weight per unit volume or the mass per unit volume, depending on the value of the <paramref name="perUnitVolumeOption"/> item. 
        /// [F/L^3] for <paramref name="perUnitVolumeOption"/> = 1 (weight), and [M/L^3] for <paramref name="perUnitVolumeOption"/> = 2 (mass).</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetWeightAndMass(string name,
            eMaterialPerVolumeOption perUnitVolumeOption,
            double value,
            double temperature = 0);



        /// <summary>
        /// Returns the temperatures at which properties are specified for a material.
        /// </summary>
        /// <param name="name">The name of an existing material pro.</param>
        /// <param name="temperatures">The different temperatures at which properties are specified for the material.</param>
        void GetTemperature(string name,
            out double[] temperatures);

        /// <summary>
        /// Assigns the temperatures at which properties are specified for a material. 
        /// This data is required only for materials whose properties are temperature dependent..
        /// </summary>
        /// <param name="name">The name of an existing material pro.</param>
        /// <param name="temperatures">The different temperatures at which properties are specified for the material.</param>
        void SetTemperature(string name,
            double[] temperatures);




        /// <summary>
        /// Returns the material stress-strain curve.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="pointIDs">The point identifier.
        /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.</param>
        /// <param name="strains">The strain at each point on the stress strain curve.</param>
        /// <param name="stresses">The stress at each point on the stress strain curve. [F/L^2].</param>
        /// <param name="sectionName">This item applies only if the specified material is concrete with a Mander concrete type.
        /// This is the frame section property for which the Mander stress-strain curve is retrieved.
        /// The section must be round or rectangular.</param>
        /// <param name="rebarArea">This item applies only if the specified material is rebar, which does not have a user-defined stress-strain curve and is specified to use Caltrans default controlling strain values, which are bar size dependent.
        /// This is the area of the rebar for which the stress-strain curve is retrieved.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetStressStrainCurve(string name,
            out eStressStrainPointID[] pointIDs,
            out double[] strains,
            out double[] stresses,
            string sectionName = "",
            double rebarArea = 0,
            double temperature = 0);


        /// <summary>
        /// Sets the material stress-strain curve for materials that are specified to have user-defined stress-strain curves.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="pointIDs">The point identifier.
        /// The point ID controls the color that will be displayed for hinges in a deformed shape plot.
        /// The point IDs must be input in numerically increasing order, except that 0 (None) values are allowed anywhere. 
        /// No duplicate values are allowed excepth for 0 (None).
        /// At least three points must be specified. </param>
        /// <param name="strains">The strain at each point on the stress strain curve.
        /// The strains must increase monotonically.
        /// At least three points must be specified. </param>
        /// <param name="stresses">The stress at each point on the stress strain curve. [F/L^2].
        /// Points that have a negative strain must have a zero or negative stress. 
        /// Similarly, points that have a positive strain must have a zero or positive stress.
        /// At least three points must be specified. </param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetStressStrainCurve(string name,
            eStressStrainPointID[] pointIDs,
            double[] strains,
            double[] stresses,
            double temperature = 0);
        #endregion

        #region Methods: Mechanical Properties           

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// This function gets the mass source data for an existing mass source.
        /// </summary>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source.</param>
        void GetMassSource(out bool massFromElements,
            out bool massFromMasses,
            out bool massFromLoads,
            out string[] namesLoadPatterns,
            out double[] scaleFactors);

        /// <summary>
        /// Adds a new mass source to the model or reinitializes an existing mass source.
        /// </summary>
        /// <param name="massFromElements">True: Element self mass is included in the mass.</param>
        /// <param name="massFromMasses">True: Assigned masses are included in the mass.</param>
        /// <param name="massFromLoads">True: Specified load patterns are included in the mass.</param>
        /// <param name="namesLoadPatterns">This is an array of load pattern names specified for the mass source when <paramref name="massFromLoads"/> = true.</param>
        /// <param name="scaleFactors">This is an array of load pattern multipliers specified for the mass source when <paramref name="massFromLoads"/> = true.</param>
        void SetMassSource(bool massFromElements,
            bool massFromMasses,
            bool massFromLoads,
            string[] namesLoadPatterns,
            double[] scaleFactors);
#endif

        /// <summary>
        /// Returns the mechanical properties for a material with an isotropic directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not isotropic.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="poissonsRatio">The poisson's ratio.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="shearModulus">The shear modulus.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetMechanicalPropertiesIsotropic(string name,
            out double modulusOfElasticity,
            out double poissonsRatio,
            out double thermalCoefficient,
            out double shearModulus,
            double temperature = 0);

        /// <summary>
        /// Sets the material directional symmetry type to isotropic, and assigns the isotropic mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="poissonsRatio">The poisson's ratio.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetMechanicalPropertiesIsotropic(string name,
            double modulusOfElasticity,
            double poissonsRatio,
            double thermalCoefficient,
            double temperature = 0);




        /// <summary>
        /// Returns the mechanical properties for a material with a uniaxial directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not uniaxial.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetMechanicalPropertiesUniaxial(string name,
            out double modulusOfElasticity,
            out double thermalCoefficient,
            double temperature = 0);

        /// <summary>
        /// Sets the material directional symmetry type to uniaxial, and assigns the uniaxial mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticity">The modulus of elasticity.</param>
        /// <param name="thermalCoefficient">The thermal coefficient.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetMechanicalPropertiesUniaxial(string name,
            double modulusOfElasticity,
            double thermalCoefficient,
            double temperature = 0);





        /// <summary>
        /// Returns the mechanical properties for a material with an anisotropic directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not anisotropic.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetMechanicalPropertiesAnisotropic(string name,
            out double[] modulusOfElasticities,
            out double[] poissonsRatios,
            out double[] thermalCoefficients,
            out double[] shearModuluses,
            double temperature = 0);

        /// <summary>
        /// Sets the material directional symmetry type to anisotropic, and assigns the anisotropic mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetMechanicalPropertiesAnisotropic(string name,
            double[] modulusOfElasticities,
            double[] poissonsRatios,
            double[] thermalCoefficients,
            double[] shearModuluses,
            double temperature = 0);




        /// <summary>
        /// Returns the mechanical properties for a material with an anisotropic directional symmetry type.
        /// The function returns an error if the symmetry type of the specified material is not orthotropic.
        /// TODO: Handle this
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetMechanicalPropertiesOrthotropic(string name,
            out double[] modulusOfElasticities,
            out double[] poissonsRatios,
            out double[] thermalCoefficients,
            out double[] shearModuluses,
            double temperature = 0);

        /// <summary>
        /// Sets the material directional symmetry type to anisotropic, and assigns the anisotropic mechanical properties.
        /// </summary>
        /// <param name="name">The name of an existing material property.</param>
        /// <param name="modulusOfElasticities">The modulus of elasticities.</param>
        /// <param name="poissonsRatios">The poisson's ratios.</param>
        /// <param name="thermalCoefficients">The thermal coefficients.</param>
        /// <param name="shearModuluses">The shear moduluses.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetMechanicalPropertiesOrthotropic(string name,
            double[] modulusOfElasticities,
            double[] poissonsRatios,
            double[] thermalCoefficients,
            double[] shearModuluses,
            double temperature = 0);
        #endregion

        #region Methods: Basic Types 
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the other material property data for aluminum materials.
        /// The function returns an error if the specified material is not aluminum.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing aluminum material property.</param>
        /// <param name="aluminumType">The type of aluminum.</param>
        /// <param name="alloy">The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.</param>
        /// <param name="Fcy">The compressive yield strength of aluminum. [F/L^2].</param>
        /// <param name="Fty">The tensile yield strength of aluminum. [F/L^2].</param>
        /// <param name="Ftu">The tensile ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="Fsu">The shear ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetAluminum(string name,
            ref eAluminumType aluminumType,
            ref string alloy,
            ref double Fcy,
            ref double Fty,
            ref double Ftu,
            ref double Fsu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for aluminum materials.
        /// </summary>
        /// <param name="name">The name of an existing aluminum material property.</param>
        /// <param name="aluminumType">The type of aluminum.</param>
        /// <param name="alloy">The Alloy designation for the aluminum, for example, 2014-T6 for wrought or 356.0-T7 for cast (mold or sand) aluminum.</param>
        /// <param name="Fcy">The compressive yield strength of aluminum. [F/L^2].</param>
        /// <param name="Fty">The tensile yield strength of aluminum. [F/L^2].</param>
        /// <param name="Ftu">The tensile ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="Fsu">The shear ultimate strength of aluminum. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>]
        void SetAluminum(string name,
            eAluminumType aluminumType,
            string alloy,
            double Fcy,
            double Fty,
            double Ftu,
            double Fsu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0);




        /// <summary>
        /// Returns the other material property data for cold formed materials.
        /// The function returns an error if the specified material is not cold formed.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing cold formed material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetColdFormed(string name,
            ref double Fy,
            ref double Fu,
            ref eHysteresisType stressStrainHysteresisType,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for cold formed materials.
        /// </summary>
        /// <param name="name">The name of an existing cold formed material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetColdFormed(string name,
            double Fy,
            double Fu,
            eHysteresisType stressStrainHysteresisType,
            double temperature = 0);
#endif

        /// <summary>
        /// Returns the other material property data for steel materials.
        /// The function returns an error if the specified material is not steel.
        ///  TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtMaxStress">The strain at maximum stress.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtRupture">The strain at rupture.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetSteel(string name,
            out double Fy,
            out double Fu,
            out double expectedFy,
            out double expectedFu,
            out eSteelStressStrainCurveType stressStrainCurveType,
            out eHysteresisType stressStrainHysteresisType,
            out double strainAtHardening,
            out double strainAtMaxStress,
            out double strainAtRupture,
            out double finalSlope,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for steel materials.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtMaxStress">The strain at maximum stress.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainAtRupture">The strain at rupture.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetSteel(string name,
            double Fy,
            double Fu,
            double expectedFy,
            double expectedFu,
            eSteelStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double strainAtHardening,
            double strainAtMaxStress,
            double strainAtRupture,
            double finalSlope,
            double temperature = 0);



        /// <summary>
        /// Returns the other material property data for tendon materials.
        /// The function returns an error if the specified material is not tendon.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        ///  <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetTendon(string name,
            out double Fy,
            out double Fu,
            out eTendonStressStrainCurveType stressStrainCurveType,
            out eHysteresisType stressStrainHysteresisType,
            out double finalSlope,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for tendon materials.
        /// </summary>
        /// <param name="name">The name of an existing tendon material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetTendon(string name,
            double Fy,
            double Fu,
            eTendonStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double finalSlope,
            double temperature = 0);




        /// <summary>
        /// Returns the other material property data for rebar materials.
        /// The function returns an error if the specified material is not rebar.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults"/> is False.</param>
        /// <param name="strainUltimate">The ultimate strain capacity. 
        /// This item must be larger than the <paramref name="strainAtHardening"/> item.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults"/> is False.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="useCaltransStressStrainDefaults">True: Program uses Caltrans default controlling strain values, which are bar size dependent.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetRebar(string name,
            out double Fy,
            out double Fu,
            out double expectedFy,
            out double expectedFu,
            out eRebarStressStrainCurveType stressStrainCurveType,
            out eHysteresisType stressStrainHysteresisType,
            out double strainAtHardening,
            out double strainUltimate,
            out double finalSlope,
            out bool useCaltransStressStrainDefaults,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for rebar materials.
        /// </summary>
        /// <param name="name">The name of an existing rebar material property.</param>
        /// <param name="Fy">The minimum yield stress. [F/L^2].</param>
        /// <param name="Fu">The minimum tensile stress. [F/L^2].</param>
        /// <param name="expectedFy">The expected yield stress. [F/L^2].</param>
        /// <param name="expectedFu">The expected tensile stress. [F/L^2].</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainAtHardening">The strain at the onset of strain hardening.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults"/> is False.</param>
        /// <param name="strainUltimate">The ultimate strain capacity. 
        /// This item must be larger than the <paramref name="strainAtHardening"/> item.
        /// This item applies only when parametric stress-strain curves are used and when <paramref name="useCaltransStressStrainDefaults"/> is False.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="useCaltransStressStrainDefaults">True: Program uses Caltrans default controlling strain values, which are bar size dependent.</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetRebar(string name,
            double Fy,
            double Fu,
            double expectedFy,
            double expectedFu,
            eRebarStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double strainAtHardening,
            double strainUltimate,
            double finalSlope,
            bool useCaltransStressStrainDefaults,
            double temperature = 0);


        /// <summary>
        /// Returns the other material property data for concrete materials.
        /// The function returns an error if the specified material is not concrete.
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="fc">The concrete compressive strength. [F/L^2].</param>
        /// <param name="isLightweight">True: The concrete is assumed to be lightweight concrete.</param>
        /// <param name="shearStrengthReductionFactor">The shear strength reduction factor for lightweight concrete.</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainUnconfinedCompressive">The strain at the unconfined compressive strength.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainUltimate">The ultimate unconfined strain capacity.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle"/> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle"/> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetConcrete(string name,
            out double fc,
            out bool isLightweight,
            out double shearStrengthReductionFactor,
            out eConcreteStressStrainCurveType stressStrainCurveType,
            out eHysteresisType stressStrainHysteresisType,
            out double strainUnconfinedCompressive,
            out double strainUltimate,
            out double finalSlope,
            out double frictionAngle,
            out double dilatationalAngle,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for concrete materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="fc">The concrete compressive strength. [F/L^2].</param>
        /// <param name="isLightweight">True: The concrete is assumed to be lightweight concrete.</param>
        /// <param name="shearStrengthReductionFactor">The shear strength reduction factor for lightweight concrete.</param>
        /// <param name="stressStrainCurveType">Type of the stress-strain curve.</param>
        /// <param name="stressStrainHysteresisType">The stress-strain hysteresis type.</param>
        /// <param name="strainUnconfinedCompressive">The strain at the unconfined compressive strength.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="strainUltimate">The ultimate unconfined strain capacity.
        /// This item applies only to parametric stress-strain curves.</param>
        /// <param name="finalSlope">This item applies only to parametric stress-strain curves. 
        /// It is a multiplier on the material modulus of elasticity, E. 
        /// This value multiplied times E gives the final slope of the curve.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle"/> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle"/> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetConcrete(string name,
            double fc,
            bool isLightweight,
            double shearStrengthReductionFactor,
            eConcreteStressStrainCurveType stressStrainCurveType,
            eHysteresisType stressStrainHysteresisType,
            double strainUnconfinedCompressive,
            double strainUltimate,
            double finalSlope,
            double frictionAngle = 0,
            double dilatationalAngle = 0,
            double temperature = 0);




        /// <summary>
        /// Returns the other material property data for no design type materials.
        /// The function returns an error if the specified material is not concrete.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle"/> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle"/> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void GetNoDesign(string name,
            out double frictionAngle,
            out double dilatationalAngle,
            double temperature = 0);

        /// <summary>
        /// Sets the other material property data for no design type materials.
        /// </summary>
        /// <param name="name">The name of an existing concrete material property.</param>
        /// <param name="frictionAngle">The Drucker-Prager friction angle, 0 &lt;= <paramref name="frictionAngle"/> &lt; 90. [deg].</param>
        /// <param name="dilatationalAngle">The Drucker-Prager dilatational angle, 0 &lt;= <paramref name="dilatationalAngle"/> &lt; 90. [deg].</param>
        /// <param name="temperature">This item applies only if the specified material has properties that are temperature dependent. 
        /// That is, it applies only if properties are specified for the material at more than one temperature.
        /// This item is the temperature at which the specified data is to be retrieved. 
        /// The temperature must have been previously defined for the material.</param>
        void SetNoDesign(string name,
            double frictionAngle = 0,
            double dilatationalAngle = 0,
            double temperature = 0);

        #endregion

    }
}