Option Strict On
Option Explicit On

Imports MPT.Enums.EnumLibrary
Imports MPT.Lists
Imports MPT.SymbolicMath

''' <summary>
''' Basic Unit class that includes the Type, Name, Power, and Numerator/denominator position.
''' </summary>
''' <remarks></remarks>
Public Class cUnit
    Implements ICloneable

#Region "Constants"
    Private Const _POWER_CHAR As String = "^"
    Private Const _DIVISOR As String = "/"
    Private Const _OPENPARENTHESIS As String = "("
    Private Const _CLOSEPARENTHESIS As String = ")"
#End Region

#Region "Properties: Public"
    ''' <summary>
    ''' Type of Unit based on the allowed enumerations. 
    ''' This limits what 'Unit' can be, as well as other class behavior.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type As eUnitType = eUnitType.None

    ''' <summary>
    ''' If true, then the Unit is in the Numerator position. If false, the Unit is in the denominator position.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Numerator As Boolean = True

    Private _name As String = String.Empty
    ''' <summary>
    ''' Specified Unit Name, such as 'in', 'ft', or 'm' for the 'Length' Type.
    ''' If none has been specified, the default unit name for each type is used.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Name As String
        Get
            If (String.IsNullOrEmpty(_name)) Then
                Return getUnitName(Type)
            Else
                Return _name
            End If
        End Get
    End Property

    Private _power As String = "1"
    ''' <summary>
    ''' The Power that a Unit is multiplied by, so long as it is greater than 0. e.g. 1/2, 2, etc.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Power As String
        Get
            Return _power
        End Get
    End Property

    '''Private ReadOnly _unitsList As New List(Of String)
    ''' <summary>
    '''List of units available for selection based on the Unit Type selected.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UnitsList As List(Of String)
        Get
            Return GetUnitsList(Type)
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="cUnit"/> class.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="cUnit"/> class.
    ''' </summary>
    ''' <param name="unitType">Type of unit.</param>
    ''' <param name="unitPower">The unit power.</param>
    ''' <param name="unitIsNumerator">if set to <c>true</c> [unit is numerator].</param>
    Public Sub New(ByVal unitType As eUnitType,
                  Optional ByVal unitPower As String = "1",
                  Optional ByVal unitIsNumerator As Boolean = True)
        Type = unitType
        SetUnitPower(unitPower)
        SetNumerator(unitIsNumerator)
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim myClone As New cUnit With {
            .Type = Type,
            .Numerator = Numerator,
            ._name = _name,
            ._power = _power
        }

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="otherUnit">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal otherUnit As Object) As Boolean
        If Not (TypeOf otherUnit Is cUnit) Then Return False

        Dim comparedObject As cUnit = TryCast(otherUnit, cUnit)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If Not .Type = Type Then Return False
            If Not .Numerator = Numerator Then Return False
            If Not .Name = Name Then Return False
            If Not .Power = Power Then Return False
            If ListLibrary.ListsAreDifferent(.UnitsList, UnitsList) Then Return False
        End With

        Return True
    End Function
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Returns the list of units allowed based on the specified Unit Type.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <param name="unitType">Unit Type for which a list is returned.</param>
    Public Shared Function GetUnitsList(ByVal unitType As eUnitType) As List(Of String)
        Select Case unitType
            Case eUnitType.None
                Return New List(Of String)
            Case eUnitType.Unitless
                Return getUnitsListAll()   'All are available for selection, with the assumption being that Numerator & denominator units are the same.
            Case eUnitType.Length
                Return cUnitLength.UnitsList
            Case eUnitType.Mass
                Return cUnitMass.UnitsList
            Case eUnitType.Rotation
                Return cUnitRotation.UnitsList
            Case eUnitType.Temperature
                Return cUnitTemperature.UnitsList
            Case eUnitType.Time
                Return cUnitTime.UnitsList
            Case eUnitType.Force
                Return cUnitForce.UnitsList
            Case Else
                Return New List(Of String)
        End Select
    End Function

    ''' <summary>
    ''' Returns the Unit cast as a label string (e.g. 1/Length^2).
    ''' </summary>
    ''' <param name="parseSchema">True: The schema form is returned (e.g. Length). 
    ''' Else, the Name is returned (e.g. in).</param>
    ''' <param name="withPowers">True: The Unit names also have the Power listed. 
    ''' Else, the Power is ignored and only the list of units is returned.</param>
    ''' <param name="asList">True: The label is listing out each component separately, so denominators are each written as '1/b'.</param>
    ''' <param name="useDefaultsIfNA">True: If name is not specified, rather than displaying 'NA', the default unit name will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUnitLabel(Optional ByVal parseSchema As Boolean = False,
                                 Optional ByVal withPowers As Boolean = False,
                                 Optional ByVal asList As Boolean = True,
                                 Optional ByVal useDefaultsIfNA As Boolean = False) As String
        Dim unitString As String

        If parseSchema Then
            unitString = GetEnumDescription(Type)
        Else
            unitString = _name
            If String.IsNullOrEmpty(unitString) Then
                If useDefaultsIfNA
                    unitString = Name
                Else 
                    unitString = "NA"
                End If
            End If
        End If

        If withPowers AndAlso Not String.IsNullOrEmpty(_power) Then
            Dim isFraction As Boolean = False

            For Each letter As Char In _power
                If letter = _DIVISOR Then
                    isFraction = True
                    Exit For
                End If
            Next
            If Not _power = "1" Then
                If isFraction Then
                    unitString &= (_POWER_CHAR & _OPENPARENTHESIS & _power & _CLOSEPARENTHESIS)
                Else
                    unitString &= (_POWER_CHAR & _power)
                End If
            End If

            ' Add '1/' to the entry if it is being listed individually. 
            If (Not Numerator AndAlso asList) Then unitString = "1" & _DIVISOR & unitString
        End If

        Return unitString
    End Function

    ''' <summary>
    ''' Sets the Name of the Unit.
    ''' If the Name is a common variation of the Name that should be used, the Name is converted.
    ''' It is recommended to set the name based on the enum description of the desired unit to ensure proper inference of unit types, and other related procedures.
    ''' </summary>
    ''' <param name="unitName">Unit Name to assign.</param>
    ''' <remarks></remarks>
    Public Sub SetUnitName(ByVal unitName As String)
        If String.IsNullOrWhiteSpace(unitName) Then Return

        ' Adjust Unit Name for Length
        If (String.Compare(unitName, "Inch", ignoreCase:=True) = 0) Then unitName = GetEnumDescription(cUnitLength.eUnit.Inch)

        ' Adjust Unit Name for forces
        If (String.Compare(unitName, "tonf", ignoreCase:=True) = 0) Then unitName = GetEnumDescription(cUnitForce.eUnit.TonForce)
        If (String.Compare(unitName, "KN", ignoreCase:=True) = 0) Then unitName = GetEnumDescription(cUnitForce.eUnit.KiloNewton)
        If (String.Compare(unitName, "Kgf", ignoreCase:=True) = 0) Then unitName = GetEnumDescription(cUnitForce.eUnit.KilogramForce)

        ' Adjust Unit for Time
        If (String.Compare(unitName, "s", ignoreCase:=True) = 0) Then unitName = GetEnumDescription(cUnitTime.eUnit.Second)

        _name = unitName
    End Sub

    ''' <summary>
    ''' If the Unit Type is not already set for the class, an attempt is made to set the Unit Type based on the current Unit string.
    ''' The resulting value is also returned.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetUnitTypeFromName() As eUnitType
        If String.IsNullOrEmpty(_name) Then Return eUnitType.None

        Dim unitTypes = GetEnumDescriptionList(eUnitType.None)

        ' For each Unit Type, search through the list of allowed Unit names to see if any match the one currently assigned to the class
        For Each unitType As String In unitTypes
            If unitType = GetEnumDescription(eUnitType.Unitless) Then Continue For  ' Ignored now for explicitl check below. Otherwise a false positive results.

            Dim unitTypeEnum As eUnitType = ConvertStringToEnumByDescription(Of eUnitType)(unitType)
            Dim unitNames = GetUnitsList(unitTypeEnum)

            For Each unitName As String In unitNames
                If isUnitMatch(unitName) Then
                    Type = unitTypeEnum
                    Return Type
                End If
            Next
        Next

        ' Unitless is checked explicitly, separately from the rest of the unit types
        Dim nameComponents As String() = _name.Split("/"c)
        If nameComponents.Length = 2 AndAlso nameComponents(0) = nameComponents(1) Then
            Return eUnitType.Unitless
        End If

        Return eUnitType.None
    End Function

    ''' <summary>
    ''' Sets the unit power value.
    ''' </summary>
    ''' <param name="unitPower">The unit power value. Must be numeric and positive. 
    ''' Fractions or decimals are allowed. 
    ''' For negative powers, the absolute value will be taken and the unit will be turned into a denominator.</param>
    Public Sub SetUnitPower(ByVal unitPower As String)
        If String.IsNullOrEmpty(unitPower) Then Return

        ' Check numerator for negative sign
        unitPower = unitPower.Trim()
        Dim isNegative As Boolean = (unitPower(0) = "-" AndALso unitPower.Length > 1)
        If isNegative Then
            unitPower = unitPower.Substring(1)
            Numerator = False
        End If

        ' Check numerator and denominator for being numeric
        Dim powerComponents As String() = unitPower.Split("/"c)
        If Not IsNumeric(powerComponents(0)) Then Return
        If (powerComponents.Length = 2 AndAlso Not IsNumeric(powerComponents(0))) Then Return
        
        _power = unitPower
    End Sub

    Public Sub SetNumerator(ByVal unitIsNumerator As Boolean)
        Numerator = ((Not Numerator AndAlso Not unitIsNumerator) OrElse 
                     (Numerator AndAlso unitIsNumerator))
    End Sub

    '''TODO: Test
    '''' <summary>
    '''' Returns a string of the combined powers when their bases are multiplied. 
    '''' If "/" is used for non-integers in both powers, this format is preserved if both the Numerator &amp; denominator are integers. 
    '''' Otherwise, the returned value will either be an integer or decimal.
    '''' </summary>
    '''' <param name="existingPower">Power of the current Unit. If not specified then it is taken as the class' current Power.</param>
    '''' <param name="multipliedPower">Power of the Unit to combine with the current Unit through multiplication.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function MultiplyUnitPowers(ByVal multipliedPower As String,
    '                                   Optional ByVal existingPower As String = "") As String
    '    If String.IsNullOrEmpty(existingPower) Then existingPower = _power

    '    Dim existingPowerSymbol As New cSymbolicBlockPower(existingPower)
    '    Dim addedPowerSymbol As New cSymbolicBlockPower(multipliedPower)
    '    Dim resultingPower As cSymbolicBlockPower = existingPowerSymbol.CombinePowersBaseMultiply(addedPowerSymbol)

    '    Return resultingPower.Power
    'End Function
    
    '''TODO: Test
    '''' <summary>
    '''' Returns a string of the combined powers when their bases are divided. 
    '''' If "/" is used for non-integers in both powers, this format is preserved if both the Numerator &amp; denominator are integers. 
    '''' Otherwise, the returned value will either be an integer or decimal.
    '''' </summary>
    '''' <param name="numeratorPower">Power of the current Unit. If not specified then it is taken as the class' current Power.</param>
    '''' <param name="denominatorPower">Power of the Unit to combine with the current Unit through division.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public Function DivideUnitPowers(ByVal denominatorPower As String,
    '                                 Optional ByVal numeratorPower As String = "") As String
    '    If String.IsNullOrEmpty(numeratorPower) Then numeratorPower = _power

    '    Dim numeratorPowerSymbol As New cSymbolicBlockPower(numeratorPower)
    '    Dim denominatorPowerSymbol As New cSymbolicBlockPower(denominatorPower)
    '    Dim resultingPower As cSymbolicBlockPower = numeratorPowerSymbol.CombinePowersBaseDivide(denominatorPowerSymbol)

    '    Return resultingPower.Power
    'End Function

    Public Function MultiplyUnitPowers(ByVal otherUnit As cUnit) As String
        Dim existingUnitSymbolic As ProductQuotientSet = unitAsSymbolic(Me)
        Dim otherUnitSymbolic As ProductQuotientSet= unitAsSymbolic(otherUnit)

        Dim newUnitSymbolic As ProductQuotientSet = existingUnitSymbolic * otherUnitSymbolic

        Return unitPowerSimplified(newUnitSymbolic)
    End Function

    ''TODO: Test
    ''' <summary>
    ''' Returns a string of the combined powers when their bases are divided. 
    ''' If "/" is used for non-integers in both powers, this format is preserved if both the Numerator &amp; denominator are integers. 
    ''' Otherwise, the returned value will either be an integer or decimal.
    ''' </summary>
    ''' <param name="numeratorPower">Power of the current Unit. If not specified then it is taken as the class' current Power.</param>
    ''' <param name="denominatorPower">Power of the Unit to combine with the current Unit through division.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DivideUnitPowers(ByVal otherUnit As cUnit) As String
        Dim existingUnitSymbolic As ProductQuotientSet = unitAsSymbolic(Me)
        Dim otherUnitSymbolic As ProductQuotientSet = unitAsSymbolic(otherUnit)

        Dim newUnitSymbolic As ProductQuotientSet
        'If (otherUnitSymbolic.Count > 1 AndAlso 
        '    otherUnitSymbolic(0).Unit.Label() = "1" AndAlso 
        '    otherUnitSymbolic(1).OperatorEquals("/")) Then
        '    newUnitSymbolic = existingUnitSymbolic * otherUnitSymbolic
        'Else
        '    newUnitSymbolic = existingUnitSymbolic / otherUnitSymbolic
        'End If
        newUnitSymbolic = existingUnitSymbolic / otherUnitSymbolic

        Return unitPowerSimplified(newUnitSymbolic)
    End Function
    

    Private Function unitAsSymbolic(ByVal unit As cUnit) As ProductQuotientSet
        Dim unitLabel As String = unitAsLabel(unit)
        Return New ProductQuotientSet(unitLabel)
    End Function

    Private Function unitAsLabel(ByVal unit As cUnit) As String
        Dim unitLabel As String = String.Empty
        If Not unit.Numerator Then
            unitLabel = "1/"
        End If
        unitLabel &= unit.Name
        If Not String.IsNullOrEmpty(unit.Power) Then
            unitLabel &= "^" & unit.Power
        End If
        Return unitLabel
    End Function

    Private Function unitPowerSimplified(ByVal unit As ProductQuotientSet) As String
        Dim newUnitSymbolicSimplified As IBase = unit.Simplify(isRecursive := True)
        Return newUnitSymbolicSimplified.PowerLabel()
    End Function

    ''' <summary>
    ''' Returns the conversion factor of changing from one Unit to another.
    ''' </summary>
    ''' <param name="unitToConvertFrom">Unit object to convert from.</param>
    ''' <param name="unitToConvertTo">Unit object to convert to.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function Convert(ByVal unitToConvertFrom As cUnit,
                                      Optional ByVal unitToConvertTo As cUnit = Nothing) As Double
        If unitToConvertFrom Is Nothing Then Return 0
        If unitToConvertTo Is Nothing Then unitToConvertTo = Me

        Return Convert(unitToConvertFrom.Name, unitToConvertTo.Name)
    End Function
    ''' <summary>
    ''' Returns the conversion factor of changing from one Unit to another.
    ''' </summary>
    ''' <param name="unitToConvertFrom">Name of the Unit to convert from. Must be present in the class list of allowed units.</param>
    ''' <param name="unitToConvertTo">Name of the Unit to convert to. Must be present in the class list of allowed units.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Function Convert(ByVal unitToConvertFrom As String,
                                  Optional ByVal unitToConvertTo As String = "") As Double
        If String.IsNullOrEmpty(unitToConvertTo) Then unitToConvertTo = Name

        Select Case Type
            Case eUnitType.Force
                Return cUnitForce.Convert(1, unitToConvertFrom, unitToConvertTo)

            Case eUnitType.Length
                Return cUnitLength.Convert(1, unitToConvertFrom, unitToConvertTo)

                'Case eUnitType.Location
                'TODO: Still in development
                'Return cUnitLocation.Convert(1, unitToConvertFrom, unitToConvertTo)
                'Return 1

            Case eUnitType.Mass
                Return cUnitMass.Convert(1, unitToConvertFrom, unitToConvertTo)

            Case eUnitType.Rotation
                Return cUnitRotation.Convert(1, unitToConvertFrom, unitToConvertTo)

            Case eUnitType.Temperature
                Return cUnitTemperature.Convert(1, unitToConvertFrom, unitToConvertTo)

            Case eUnitType.Time
                Return cUnitTime.Convert(1, unitToConvertFrom, unitToConvertTo)

            Case eUnitType.Unitless
                'Nothing to convert
                Return 1
            Case eUnitType.None
                'Nothing to convert
                Return 1
            Case Else
                'Nothing to convert
                Return 1
        End Select
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Returns a list of all allowed units for all Unit types.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function getUnitsListAll() As List(Of String)
        Dim unitListTemp As IList(Of String) = New List(Of String)

        unitListTemp = ListLibrary.AppendToList(unitListTemp, cUnitLength.UnitsList)
        unitListTemp = ListLibrary.AppendToList(unitListTemp, cUnitMass.UnitsList)
        unitListTemp = ListLibrary.AppendToList(unitListTemp, cUnitRotation.UnitsList)
        unitListTemp = ListLibrary.AppendToList(unitListTemp, cUnitTemperature.UnitsList)
        unitListTemp = ListLibrary.AppendToList(unitListTemp, cUnitTime.UnitsList)
        unitListTemp = ListLibrary.AppendToList(unitListTemp, cUnitForce.UnitsList)

        Return TryCast(unitListTemp, List(Of String))
    End Function


    ''' <summary>
    ''' Unit shorthand Name matches the currently recorded Name, and considering special cases.
    ''' </summary>
    ''' <param name="shorthandUnitName">Shorthand Unit Name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isUnitMatch(ByVal shorthandUnitName As String) As Boolean
        Return (isUnitMatchSIWithM(shorthandUnitName) OrElse
                isUnitMatchInch(shorthandUnitName) OrElse
                isUnitMatchDefault(shorthandUnitName))
    End Function

    ''' <summary>
    ''' Unit shorthand Name matches the currently recorded Name considering the following:
    ''' Cap sensitivity is important for SI units of milli vs. mega, e.g. mN vs. MN.
    ''' Otherwise, cap-insensitive comparison is intentional, as programs are not consistent on capitalization of units.
    ''' </summary>
    ''' <param name="shorthandUnitName">Shorthand Unit Name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isUnitMatchSIWithM(ByVal shorthandUnitName As String) As Boolean
        Return (_name.Length > 1 AndAlso
                Mid(_name, 1, 1).ToUpper = "M" AndAlso
                String.Compare(_name, shorthandUnitName, ignoreCase:=False) = 0)
    End Function

    ''' <summary>
    ''' Unit shorthand Name matches the currently recorded Name considering the following:
    ''' Since "in" is often a reserved word, this may often come in the form of "inches".
    ''' </summary>
    ''' <param name="shorthandUnitName">Shorthand Unit Name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isUnitMatchInch(ByVal shorthandUnitName As String) As Boolean
        Return (String.Compare(_name, "Inch", ignoreCase:=True) = 0 AndAlso
                String.Compare(shorthandUnitName, "in", ignoreCase:=True) = 0)
    End Function

    ''' <summary>
    ''' Unit shorthand Name matches the currently recorded Name.
    ''' </summary>
    ''' <param name="shorthandUnitName">Shorthand Unit Name to check.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function isUnitMatchDefault(ByVal shorthandUnitName As String) As Boolean
        Return (String.Compare(_name, shorthandUnitName, ignoreCase:=True) = 0)
    End Function

    Private Shared Function getUnitName(ByVal unitType As eUnitType) As String
        Select Case unitType
            Case eUnitType.None
                Return String.Empty
            Case eUnitType.Unitless
                Return String.Empty
            Case eUnitType.Length
                Return GetEnumDescription(cUnitLength.UnitDefault)
            Case eUnitType.Mass
                Return GetEnumDescription(cUnitMass.UnitDefault)
            Case eUnitType.Rotation
                Return GetEnumDescription(cUnitRotation.UnitDefault)
            Case eUnitType.Temperature
                Return GetEnumDescription(cUnitTemperature.UnitDefault)
            Case eUnitType.Time
                Return GetEnumDescription(cUnitTime.UnitDefault)
            Case eUnitType.Force
                Return GetEnumDescription(cUnitForce.UnitDefault)
            Case Else
                Return String.Empty
        End Select
    End Function
#End Region
End Class
