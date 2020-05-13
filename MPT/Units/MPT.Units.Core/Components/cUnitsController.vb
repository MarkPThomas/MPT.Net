Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary
Imports MPT.Lists

''' <summary>
''' Handles the highest level controlling of units including:
''' Sets up schema based on selections from pre-defined lists, such as stress.
''' Sets up lists of available pre-defined shorthand labels (such as MPa) for the current units schema.
''' Translates between the base or derived units and other representations of the units, such as psi instead lb/in^2, or MPa instead of N/mm^2.
''' </summary>
''' <remarks></remarks>
Public Class cUnitsController
    Implements ICloneable

#Region "Enumerations"
    ''' <summary>
    ''' List of standard Unit types available for selection where the schema is already defined in the classes.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eUnitTypeStandard
        <Description("")> None
        ''' <summary>
        ''' Not included in any Type lists, as it indicates that a custom type of cUnits object is to be created.
        ''' </summary>
        ''' <remarks></remarks>
        <Description("Custom")> Custom
        ''' <summary>
        ''' Demand/Capacity ratio.
        ''' </summary>
        <Description("D/C")> D_C
        <Description("Strain")> Strain
        <Description("Length")> Length
        <Description("Area")> Area
        <Description("Volume")> Volume
        <Description("Displacement")> Displacement
        <Description("Displacement (Rotation)")> DisplacementRotational
        <Description("Velocity")> Velocity
        <Description("Acceleration")> Acceleration
        <Description("Rotation")> Rotation
        <Description("Angular Velocity")> AngularVelocity
        <Description("Angular Acceleration")> AngularAcceleration
        <Description("Mass")> Mass
        <Description("Weight")> Weight
        <Description("Density (Mass)")> Density_Mass
        <Description("Density (Weight)")> Density_Weight
        <Description("Temperature")> Temperature
        <Description("Time")> Time
        <Description("Period")> Period
        <Description("Frequency")> Frequency
        <Description("Force")> Force
        <Description("Moment")> Moment
        <Description("Stress")> Stress
        <Description("Pressure")> Pressure
        <Description("Pressure (Line)")> Pressure_Line
        <Description("Work")> Work
        <Description("Power")> Power
        <Description("Rotational Inertia")> RotationalInertia
        <Description("Section Modulus")> SectionModulus
        <Description("Radius of Gyration")> RadiusOfGyration
        <Description("Unitless")> Unitless
    End Enum

    ''' <summary>
    ''' List of schema types that contain shorthand labels.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum eUnitTypeShorthand
        <Description("")> None
        <Description("Linearly Distributed Force")> ForceLineDistribution
        <Description("Pressure or Stress")> PressureOrStress
        <Description("Work")> Work
        <Description("Power")> Power
        <Description("Speed")> Speed
        <Description("Angular Speed")> SpeedAngular
    End Enum
#End Region

#Region "Properties: Public"
    Private ReadOnly _quickUnitTypes As New List(Of String)
    ''' <summary>
    ''' List of a subset of 'all Unit types' for the most commonly defined units. 
    ''' Used for autogenerating pre-defined Unit schemas.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property QuickUnitTypes As List(Of String)
        Get
            Dim newList As New List(Of String)
            newList.InsertRange(0, _quickUnitTypes)
            Return newList
        End Get
    End Property

    Private _allUnitTypes As New List(Of String)
    ''' <summary>
    ''' List of all Unit types that have pre-defined schemas available.
    ''' Used for autogenerating pre-defined Unit schemas.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AllUnitTypes As List(Of String)
        Get
            Dim newList As New List(Of String)
            newList.InsertRange(0, _allUnitTypes)
            Return newList
        End Get
    End Property

    Private _shorthandUnitsAvailable As New List(Of String)
    ''' <summary>
    ''' List of all of the shorthand units currently available for selection.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ShorthandUnitsAvailable As List(Of String)
        Get
            Dim newList As New List(Of String)
            newList.InsertRange(0, _shorthandUnitsAvailable)
            Return newList
        End Get
    End Property

    Private _units As cUnits = New cUnits()
    ''' <summary>
    ''' Units object that contains a collection of Unit objects that defines a complete Unit, such as stress, speed, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Units As cUnits
        Get
            Return CType(_units.Clone(), cUnits)
        End Get
        Private Set(value As cUnits)
            _units = value
        End Set
    End Property

    Private _type As eUnitTypeStandard
    ''' <summary>
    ''' The pre-edfined Unit Type from which the units object is set up.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type As eUnitTypeStandard
        Get
            Return _type
        End Get
        Private Set
            If Not value = _type Then
                _type = value
                setUnitsSchemaByType()
                setShorthandTypes()
            End If
        End Set
    End Property

    Private _typeShorthand As eUnitTypeShorthand
    ''' <summary>
    ''' The pre-defined shorthand schema Type from which the units object is set up.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TypeShorthand As eUnitTypeShorthand
        Get
            Return _typeShorthand
        End Get
    End Property
#End Region

#Region "Initialization"
    Public Sub New()
        initialization()
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim myClone As New cUnitsController

        With myClone
            For Each shorthandUnit As String In shorthandUnitsAvailable
                .shorthandUnitsAvailable.Add(shorthandUnit)
            Next

            .units = CType(units.Clone, cUnits)
            .Type = Type
            ._typeShorthand = typeShorthand
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="other">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal other As Object) As Boolean
        If Not (TypeOf other Is cUnitsController) Then Return False

        Dim comparedObject As cUnitsController = TryCast(other, cUnitsController)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If ListLibrary.ListsAreDifferent(.shorthandUnitsAvailable, shorthandUnitsAvailable) Then Return False

            If Not .units.Equals(units) Then Return False

            If Not .Type = Type Then Return False
            If Not .typeShorthand = typeShorthand Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Public Conversion"
    ''' <summary>
    ''' Returns the conversion factor to convert the supplied units object to the current units object.
    ''' </summary>
    ''' <param name="unitsConvert">Units to be converted from to the current units object.
    ''' This can either be a list of consistent units, or a literal string of the Unit (e.g. kN-m/sec).
    ''' Any numbers that are not in a powers position will be ignored, and any number in a list of consistent units will be ignored.
    ''' For consistent units, only the first Unit is used if units of the same Type are repeated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertFrom(ByVal unitsConvert As String) As Double
        ' Generate new composite Unit object for conversion
        Dim unitsComposite As New cUnitsController()
        If isConsistent(unitsConvert) Then
            ' Formulate new units composite by converting each Type
            unitsComposite = CType(Me.Clone, cUnitsController)
            unitsComposite.MakeUnitsConsistent(unitsConvert)
        Else
            unitsComposite.ParseStringToUnits(unitsConvert)
        End If

        Return units.Convert(unitsComposite.units)
    End Function

    ''' <summary>
    ''' Returns the conversion factor to convert the current units object to the supplied units object.
    ''' </summary>
    ''' <param name="unitsConvert">Units to be converted to by the current units object.
    ''' This can either be a list of consistent units, or a literal string of the Unit (e.g. kN-m/sec).
    ''' Any numbers that are not in a powers position will be ignored, and any number in a list of consistent units will be ignored.
    ''' For consistent units, only the first Unit is used if units of the same Type are repeated.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertTo(ByVal unitsConvert As String) As Double
        Dim convertFromTemp As Double = ConvertFrom(unitsConvert)
        If Not Math.Abs(convertFromTemp - 0) < 10E-8 Then
            Return 1 / convertFromTemp
        Else
            Return 1
        End If
    End Function
#End Region

#Region "Methods: Public"
    ' Set Class Type
    ''' <summary>
    ''' Sets the schema Type based on the string Name of the schema. 
    ''' If no match is found between the enum descriptions and the supplied string, the Type is unchanged.
    ''' </summary>
    ''' <param name="schemaType">Name of the schema Type to set the class to.</param>
    ''' <remarks></remarks>
    Public Sub SetTypeByDescription(ByVal schemaType As String)
        Type = ConvertStringToEnumByDescription(Of eUnitTypeStandard)(schemaType)
    End Sub

    ''' <summary>
    ''' Sets the schema Type based on the shorthand Type provided.
    ''' For shorthands used for multiple types, a default is chosen unless a schema Type string Name is provided.
    ''' If no match is found, or the shorthand Type is 'None' the Type is unchanged.
    ''' </summary>
    ''' <param name="shorthandType">Shorthand Type that the class is to be using.</param>
    ''' <param name="schemaType">Name of the schema Type to set the class to.</param>
    ''' <remarks></remarks>
    Public Sub SetTypeByShorthand(ByVal shorthandType As eUnitTypeShorthand,
                                  Optional ByVal schemaType As String = "")
        Select Case shorthandType
            Case eUnitTypeShorthand.None
                If String.IsNullOrEmpty(schemaType) Then
                    'No action is taken
                Else
                    SetTypeByDescription(schemaType)
                End If
            Case eUnitTypeShorthand.ForceLineDistribution
                Type = eUnitTypeStandard.Pressure_Line
            Case eUnitTypeShorthand.PressureOrStress
                If String.IsNullOrEmpty(schemaType) Then
                    Type = eUnitTypeStandard.Stress
                Else
                    SetTypeByDescription(schemaType)
                End If
            Case eUnitTypeShorthand.Work
                Type = eUnitTypeStandard.Work
            Case eUnitTypeShorthand.Power
                Type = eUnitTypeStandard.Power
            Case eUnitTypeShorthand.Speed
                Type = eUnitTypeStandard.Velocity
            Case eUnitTypeShorthand.SpeedAngular
                If String.IsNullOrEmpty(schemaType) Then
                    Type = eUnitTypeStandard.Frequency
                Else
                    SetTypeByDescription(schemaType)
                End If
        End Select
    End Sub

    'Set Units Object
    ''' <summary>
    ''' Sets the Type, units object, and other related properties to match the specified shorthand Name that is provided as a string.
    ''' If no match is found for Unit values, they will be left empty.
    ''' If no match is found for the Unit Type, the units object will be empty.
    ''' </summary>
    ''' <param name="shorthandName">Name of the shorthand Unit to set.</param>
    ''' <remarks></remarks>
    Public Sub ParseStringToShorthandUnits(ByVal shorthandName As String)
        AssignShorthandUnits(shorthandName)
    End Sub

    ''' <summary>
    ''' Sets the Type, units object, and other related properties to match the specified Unit string that is provided, including shorthand units.
    ''' If no match is found for Unit values, they will be left empty.
    ''' If no match is found for the Unit Type, the units object will be empty.
    ''' </summary>
    ''' <param name="unitsToParse"></param>
    ''' <remarks></remarks>
    Public Sub ParseStringToUnits(ByVal unitsToParse As String)
        Dim shorthandType As eUnitTypeShorthand = GetShorthandTypeByName(unitsToParse)

        If shorthandType = eUnitTypeShorthand.None Then
            _units.ParseStringToUnits(unitsToParse)
        Else
            ParseStringToShorthandUnits(unitsToParse)
        End If
    End Sub

    ''' <summary>
    ''' Sets the Type, units object, and other related properties to match the specified shorthand Type and Name.
    ''' A schema Type Name should be provided for ambiguous cases where multiple types might apply to the same shorthand Type.
    ''' </summary>
    ''' <param name="shorthandName">Name of the shorthand Unit to set.</param>
    ''' <param name="schemaType">Name of the schema Type, in cases where multiple types might apply to the same shorthand Type.</param>
    ''' <remarks></remarks>
    Public Overloads Sub AssignShorthandUnits(ByVal shorthandName As String,
                                              Optional ByVal schemaType As String = "")
        If String.IsNullOrWhiteSpace(shorthandName) Then
            RemoveShorthandUnits()
            Exit Sub
        End If

        Dim shorthandType As eUnitTypeShorthand = GetShorthandTypeByName(shorthandName)
        assignShorthandUnits(shorthandType, shorthandName, schemaType)
    End Sub

    ''' <summary>
    ''' Removes the shorthand-specific properties of the units object, while leaving the rest of the properties intact.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RemoveShorthandUnits()
        _typeShorthand = eUnitTypeShorthand.none
        units.shorthandLabel = ""
    End Sub

    ' Shorthand names list
    ''' <summary>
    ''' Returns the list of possible shorthand names based on the current shorthand Type assigned to the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetShorthandNamesList() As List(Of String)
        Return GetShorthandNamesList(TypeShorthand)
    End Function
    ''' <summary>
    ''' Returns the list of possible shorthand names based on the provided shorthand Type enumeration.
    ''' </summary>
    ''' <param name="shorthandType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Overloads Function GetShorthandNamesList(ByVal shorthandType As eUnitTypeShorthand) As List(Of String)
        Select Case shorthandType
            Case eUnitTypeShorthand.ForceLineDistribution
                Return cUnitForceLineDistribution.UnitsList

            Case eUnitTypeShorthand.PressureOrStress
                Return cUnitPressureStress.UnitsList

            Case eUnitTypeShorthand.Work
                Return cUnitWork.UnitsList

            Case eUnitTypeShorthand.Power
                Return cUnitPower.UnitsList

            Case eUnitTypeShorthand.Speed
                Return cUnitSpeed.UnitsList

            Case eUnitTypeShorthand.SpeedAngular
                Return cUnitSpeedAngular.UnitsList

            Case Else
                'No action is taken, leaving an empty list.
                Return New List(Of String)
        End Select
    End Function


    ' Determine Shorthand Type
    ''' <summary>
    ''' Returns the shorthand enumeration based on the string Name provided.
    ''' </summary>
    ''' <param name="shorthandName">Name of the shorthand Unit to match to an enumeration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetShorthandTypeByName(ByVal shorthandName As String) As eUnitTypeShorthand
        If String.IsNullOrWhiteSpace(shorthandName) Then Return eUnitTypeShorthand.None

        If cUnitForceLineDistribution.UnitsList.Contains(shorthandName) Then Return eUnitTypeShorthand.ForceLineDistribution
        If cUnitPressureStress.UnitsList.Contains(shorthandName) Then Return eUnitTypeShorthand.PressureOrStress
        If cUnitWork.UnitsList.Contains(shorthandName) Then Return eUnitTypeShorthand.Work
        If cUnitPower.UnitsList.Contains(shorthandName) Then Return eUnitTypeShorthand.Power
        If cUnitSpeed.UnitsList.Contains(shorthandName) Then Return eUnitTypeShorthand.Speed
        If cUnitSpeedAngular.UnitsList.Contains(shorthandName) Then Return eUnitTypeShorthand.SpeedAngular

        Return eUnitTypeShorthand.None
    End Function

    ''' <summary>
    '''  Returns the shorthand enumeration based on the Unit Type enumeration of the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetShorthandTypeByUnitsType() As eUnitTypeShorthand
        Return GetShorthandTypeByUnitsType(Type)
    End Function
    ''' <summary>
    ''' Returns the shorthand enumeration based on the Unit Type enumeration provided. 
    ''' </summary>
    ''' <param name="unitType">Unit Type enumeration to be used to determine the shorthand enumeration Type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Overloads Function GetShorthandTypeByUnitsType(ByVal unitType As eUnitTypeStandard) As eUnitTypeShorthand
        Select Case unitType
            Case eUnitTypeStandard.Stress,
                 eUnitTypeStandard.Pressure
                Return eUnitTypeShorthand.PressureOrStress

            Case eUnitTypeStandard.Pressure_Line
                Return eUnitTypeShorthand.ForceLineDistribution

            Case eUnitTypeStandard.Work
                Return eUnitTypeShorthand.Work

            Case eUnitTypeStandard.Power
                Return eUnitTypeShorthand.Power

            Case eUnitTypeStandard.AngularVelocity,
                 eUnitTypeStandard.Frequency
                Return eUnitTypeShorthand.SpeedAngular

            Case eUnitTypeStandard.Velocity
                Return eUnitTypeShorthand.Speed

            Case Else
                Return eUnitTypeShorthand.None
        End Select
    End Function

    ''' <summary>
    ''' Returns the shorthand enumeration based on which one, if any, the current schema matches.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function GetShorthandTypeByUnits() As eUnitTypeShorthand
        Return GetShorthandTypeByUnits(Units)
    End Function
    ''' <summary>
    ''' Returns the shorthand enumeration based on which one, if any, the schema of the provided units object matches.
    ''' </summary>
    ''' <param name="unitsForShorthand">Units object to use for determining the shorthand enumeration.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Overloads Function GetShorthandTypeByUnits(ByVal unitsForShorthand As cUnits) As eUnitTypeShorthand
        Dim linearForce As New cUnitForceLineDistribution
        If unitsForShorthand.SchemasMatch(linearForce.GetUnits) Then Return eUnitTypeShorthand.ForceLineDistribution

        Dim stress As New cUnitPressureStress
        If unitsForShorthand.SchemasMatch(stress.GetUnits) Then Return eUnitTypeShorthand.PressureOrStress

        Dim work As New cUnitWork
        If unitsForShorthand.SchemasMatch(work.GetUnits) Then Return eUnitTypeShorthand.Work

        Dim power As New cUnitPower
        If unitsForShorthand.SchemasMatch(power.GetUnits) Then Return eUnitTypeShorthand.Power

        Dim speed As New cUnitSpeed
        If unitsForShorthand.SchemasMatch(speed.GetUnits) Then Return eUnitTypeShorthand.Speed

        Dim angularVelocity As New cUnitSpeedAngular
        If unitsForShorthand.SchemasMatch(angularVelocity.GetUnits) Then Return eUnitTypeShorthand.SpeedAngular

        Return eUnitTypeShorthand.none
    End Function

    ''' <summary>
    ''' True: Shorthand units are available for a given schema. 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsShorthandTypesAvailable() As Boolean
        Return _shorthandUnitsAvailable.Count > 0 
    End Function

    ' Consistent Units
    ''' <summary>
    ''' For each matching Unit Type, changes the current Unit to the one provided in the list.
    ''' </summary>
    ''' <param name="unitsToApply">String of units that are to be applied to every Type occurrence in the composite units object. 
    ''' Units should be demarcated by commas.</param>
    ''' <remarks></remarks>
    Public Sub MakeUnitsConsistent(ByVal unitsToApply As String)
        If Not IsConsistent(unitsToApply) Then Exit Sub

        Dim unitsList As List(Of String) = ListLibrary.ParseStringToList(unitsToApply, ",")
        MakeUnitsConsistent(unitsList)
    End Sub

    ''' <summary>
    ''' For each matching Unit Type, changes the current Unit to the one provided in the list.
    ''' </summary>
    ''' <param name="unitsToApply">List of units that are to be applied to every Type occurrence in the composite units object.</param>
    ''' <remarks></remarks>
    Public Sub MakeUnitsConsistent(ByVal unitsToApply As List(Of String))
        For Each unitName As String In unitsToApply
            ' Generate list of units objects from the names
            Dim unitCtrl As New cUnitsController
            unitCtrl.ParseStringToUnits(unitName)
            If unitCtrl.units.unitsAll.Count = 0 Then Continue For

            ' For consistent units, each Unit is assumed to be the first and only Unit in the Unit object
            Dim newUnit As cUnit = unitCtrl.units.unitsAll(0)

            ' Change all units of matching types to copies of the consistent Unit
            units.ReplaceUnitByType(CType(newUnit.Clone, cUnit))
        Next
    End Sub

    ''' <summary>
    ''' Determines if the units string provided is for consistent units.
    ''' This is determined by the presence of a ',' demarcator indicating a list of units.
    ''' </summary>
    ''' <param name="unitsToCheck">Any string containing units.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsConsistent(ByVal unitsToCheck As String) As Boolean
        If String.IsNullOrWhiteSpace(unitsToCheck) Then Return False
        Return unitsToCheck.Contains(",")
    End Function
#End Region

#Region "Methods: Private"
    Private Sub initialization()
        _allUnitTypes = GetEnumDescriptionList(eUnitTypeStandard.none)
        _allUnitTypes.Remove(GetEnumDescription(eUnitTypeStandard.none))
        _allUnitTypes.Remove(GetEnumDescription(eUnitTypeStandard.custom))
        _allUnitTypes.Sort()

        With _quickUnitTypes
            .Add(GetEnumDescription(eUnitTypeStandard.unitless))
            .Add(GetEnumDescription(eUnitTypeStandard.D_C))
            .Add(GetEnumDescription(eUnitTypeStandard.strain))
            .Add(GetEnumDescription(eUnitTypeStandard.displacement))
            .Add(GetEnumDescription(eUnitTypeStandard.displacementRotational))
            .Add(GetEnumDescription(eUnitTypeStandard.force))
            .Add(GetEnumDescription(eUnitTypeStandard.moment))
            .Add(GetEnumDescription(eUnitTypeStandard.stress))
            .Add(GetEnumDescription(eUnitTypeStandard.time))
            .Add(GetEnumDescription(eUnitTypeStandard.period))
            .Add(GetEnumDescription(eUnitTypeStandard.frequency))
        End With
        _quickUnitTypes.Sort()
    End Sub
    
    ''' <summary>
    ''' Sets the schema of the units object according to a preset arrangement based on specified Type.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub setUnitsSchemaByType()
        'No action taken as properties are to be customized
        If Type = eUnitTypeStandard.custom Then Exit Sub 'This preserves the current units when switching to 'custom' from another Type.

        units = New cUnits

        Select Case Type
            Case eUnitTypeStandard.acceleration
                setUnitsLength()
                setUnitsTime("2", False)
            Case eUnitTypeStandard.angularAcceleration
                setUnitsRotation()
                setUnitsTime("2", False)
            Case eUnitTypeStandard.angularVelocity
                setUnitsRotation()
                setUnitsTime(, False)
            Case eUnitTypeStandard.area
                setUnitsLength("2")
            Case eUnitTypeStandard.D_C
                setUnitsUnitless()
            Case eUnitTypeStandard.density_mass
                setUnitsMass()
                setUnitsLength("3", False)
            Case eUnitTypeStandard.density_weight
                setUnitsForce()
                setUnitsLength("3", False)
            Case eUnitTypeStandard.displacement
                setUnitsLength()
            Case eUnitTypeStandard.displacementRotational
                setUnitsRotation()
            Case eUnitTypeStandard.force
                setUnitsForce()
            Case eUnitTypeStandard.frequency
                setUnitsRotation()
                setUnitsTime(, False)
            Case eUnitTypeStandard.length
                setUnitsLength()
            Case eUnitTypeStandard.mass
                setUnitsMass()
            Case eUnitTypeStandard.moment
                setUnitsForce()
                setUnitsLength()
            Case eUnitTypeStandard.none
                'No action taken as properties are to be customized
            Case eUnitTypeStandard.period
                setUnitsTime()
            Case eUnitTypeStandard.power
                setUnitsForce()
                setUnitsLength()
                setUnitsTime(, False)
            Case eUnitTypeStandard.pressure
                setUnitsForce()
                setUnitsLength("2", False)
            Case eUnitTypeStandard.pressure_Line
                setUnitsForce()
                setUnitsLength(, False)
            Case eUnitTypeStandard.radiusOfGyration
                setUnitsLength()
            Case eUnitTypeStandard.rotation
                setUnitsRotation()
            Case eUnitTypeStandard.rotationalInertia
                setUnitsLength("4")
            Case eUnitTypeStandard.sectionModulus
                setUnitsLength("3")
            Case eUnitTypeStandard.strain
                setUnitsLength()
                setUnitsLength(, False)
            Case eUnitTypeStandard.stress
                setUnitsForce()
                setUnitsLength("2", False)
            Case eUnitTypeStandard.temperature
                setUnitsTemperature()
            Case eUnitTypeStandard.time
                setUnitsTime()
            Case eUnitTypeStandard.unitless
                setUnitsUnitless()
            Case eUnitTypeStandard.velocity
                setUnitsLength()
                setUnitsTime(, False)
            Case eUnitTypeStandard.volume
                setUnitsLength("3")
            Case eUnitTypeStandard.weight
                setUnitsForce()
            Case eUnitTypeStandard.work
                setUnitsForce()
                setUnitsLength()
        End Select
    End Sub
    
    ''' <summary>
    ''' The list of available shorthand units is updated based on the assigned type or units.
    ''' </summary>
    Private Sub setShorthandTypes()
        If Type = eUnitTypeStandard.custom Then 'Get list by matching schema to known schema types.
            _shorthandUnitsAvailable = GetShorthandNamesList(GetShorthandTypeByUnits(units))
        Else
            _shorthandUnitsAvailable = GetShorthandNamesList(GetShorthandTypeByUnitsType())
        End If
    End Sub

    ''' <summary>
    ''' Sets the Type, units object, and other related properties to match the specified shorthand Type and Name.
    ''' A schema Type Name should be provided for ambiguous cases where multiple types might apply to the same shorthand Type.
    ''' </summary>
    ''' <param name="shorthandType">Type of the shorthand Unit to set.</param>
    ''' <param name="shorthandName">Name of the shorthand Unit to set.</param>
    ''' <param name="schemaType">Name of the schema Type, in cases where multiple types might apply to the same shorthand Type.</param>
    ''' <remarks></remarks>
    Private Overloads Sub assignShorthandUnits(ByVal shorthandType As eUnitTypeShorthand,
                                               ByVal shorthandName As String,
                                               Optional ByVal schemaType As String = "")
        Dim tempUnits As cUnits = getShorthandUnits(shorthandType, shorthandName)
        If tempUnits Is Nothing Then Exit Sub

        _typeShorthand = shorthandType
        If Type = eUnitTypeStandard.none Then SetTypeByShorthand(_typeShorthand, schemaType)

        Units = tempUnits
        Units.shorthandLabel = shorthandName
    End Sub

    
    ''' <summary>
    ''' Returns the units object corresponding to the shorthand Type and shorthand Name. 
    ''' Schema and values are filled where matches are found. 
    ''' Unit values are returned as empty where no matches are found.
    ''' Units is returned as 'Nothing' if no shorthand Type match is found.
    ''' </summary>
    ''' <param name="shorthandType">Type of shorthand label that is being used (pressure, speed, etc.)</param>
    ''' <param name="shorthandName">Name of the shorthand label used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getShorthandUnits(ByVal shorthandType As eUnitTypeShorthand,
                                              ByVal shorthandName As String) As cUnits
        Select Case shorthandType
            Case eUnitTypeShorthand.ForceLineDistribution
                Return cUnitForceLineDistribution.GetUnits(shorthandName)

            Case eUnitTypeShorthand.PressureOrStress
                Return cUnitPressureStress.GetUnits(shorthandName)

            Case eUnitTypeShorthand.Work
                Return cUnitWork.GetUnits(shorthandName)

            Case eUnitTypeShorthand.Power
                Return cUnitPower.GetUnits(shorthandName)

            Case eUnitTypeShorthand.Speed
                Return cUnitSpeed.GetUnits(shorthandName)

            Case eUnitTypeShorthand.SpeedAngular
                Return cUnitSpeedAngular.GetUnits(shorthandName)

            Case Else
                Return Nothing
        End Select
    End Function

    'The methods below set up the units object according to the correlated base Unit.
    ''' <summary>
    ''' Adds a Force Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsForce(Optional ByVal power As String = "1",
                            Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.force, power, isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a Length Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsLength(Optional ByVal power As String = "1",
                               Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.length, power, isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a Mass Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsMass(Optional ByVal power As String = "1",
                            Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.mass, power, isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a Rotation Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsRotation(Optional ByVal power As String = "1",
                           Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.rotation, power, isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a Temperature Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsTemperature(Optional ByVal power As String = "1",
                            Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.temperature, power, isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a Time Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsTime(Optional ByVal power As String = "1",
                            Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.time, power, isNumerator)
        units.AddUnit(unit)
    End Sub
    ''' <summary>
    ''' Adds a Unitless Unit object to the units object.
    ''' </summary>
    ''' <param name="power">Power to which the Unit is multiplied. Values must be greater than 0, but may be fractions.</param>
    ''' <param name="isNumerator">If true, the Unit is in the Numerator position. If false, the Unit is in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub setUnitsUnitless(Optional ByVal power As String = "1",
                            Optional ByVal isNumerator As Boolean = True)
        Dim unit As New cUnit(eUnitType.unitless, power, isNumerator)
        units.AddUnit(unit)
    End Sub
#End Region

End Class
