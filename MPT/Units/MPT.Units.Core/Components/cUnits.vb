Option Strict On
Option Explicit On

Imports MPT.Lists

''' <summary>
''' Aggregate object of Unit objects, which defines the schema of composite units and possibly contains the values used in the schema.
''' </summary>
''' <remarks></remarks>
Public Class cUnits
    Implements ICloneable

#Region "Constants"
    Private Const _POWER_CHAR As String = "^"
    Private Const _MULTIPLIER As String = "*"
    Private Const _MULTIPLIER_ALT As String = "-"
    Private Const _DIVISOR As String = "/"
    Private Const _OPENPARENTHESIS As String = "("
    Private Const _CLOSEPARENTHESIS As String = ")"
    Private Const _POWER_DENOMINATOR As String = "-"
#End Region

#Region "Properties: Private"
    ''' <summary>
    ''' List of all of the schema components in the units object.
    ''' </summary>
    ''' <remarks></remarks>
    Private _schemaComponents As New List(Of String)
#End Region

#Region "Properties: Public"
    Private _unitNumerators As New List(Of cUnit)
    ''' <summary>
    ''' List of Unit objects that are to be placed in the Numerator position.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UnitNumerators As List(Of cUnit)
        Get
            Dim newUnitNumerators As New List(Of cUnit)
            newUnitNumerators.InsertRange(0, _unitNumerators)
            Return newUnitNumerators
        End Get
    End Property

    Private _unitDenominators As New List(Of cUnit)
    ''' <summary>
    ''' List of Unit objects that are to be placed in the denominator position.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UnitDenominators As List(Of cUnit)
        Get
            Dim newUnitDenominators As New List(Of cUnit)
            newUnitDenominators.InsertRange(0, _unitDenominators)
            Return newUnitDenominators
        End Get
    End Property

    ''' <summary>
    ''' List of all Unit objects, both numerators and deniminators.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property UnitsAll As List(Of cUnit)
        Get
            Dim completeList As New List(Of cUnit)
            completeList.InsertRange(0, UnitNumerators)
            For Each unit As cUnit In UnitDenominators
                completeList.Add(unit)
            Next
            Return completeList
        End Get
    End Property

    ''' <summary>
    ''' Optional property. Label for the shorthand term to refer to for the Unit schema &amp; value combination. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShorthandLabel As String
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="cUnits"/> class.
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Creates a new object that is a copy of the current instance.
    ''' </summary>
    ''' <returns>A new object that is a copy of this instance.</returns>
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim myClone As New cUnits

        With myClone
            ._schemaComponents = _schemaComponents
            .ShorthandLabel = ShorthandLabel

            For Each unitNumerator As cUnit In UnitNumerators
                ._unitNumerators.Add(CType(unitNumerator.Clone, cUnit))
            Next

            For Each unitDenominator As cUnit In UnitDenominators
                ._unitDenominators.Add(CType(unitDenominator.Clone, cUnit))
            Next
        End With

        Return myClone
    End Function

    ''' <summary>
    ''' Returns 'True' if the object provided perfectly matches the existing object.
    ''' </summary>
    ''' <param name="unitsObject">External object to check for equality.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function Equals(ByVal unitsObject As Object) As Boolean
        If Not (TypeOf unitsObject Is cUnits) Then Return False
        Dim isMatch As Boolean = False
        Dim comparedObject As cUnits = TryCast(unitsObject, cUnits)

        'Check for any differences
        If comparedObject Is Nothing Then Return False
        With comparedObject
            If ListLibrary.ListsAreDifferent(._schemaComponents, _schemaComponents) Then Return False
            If Not ShorthandLabel = .ShorthandLabel Then Return False

            For Each unitOuter As cUnit In UnitNumerators
                isMatch = False
                For Each unitInner As cUnit In .UnitNumerators
                    If unitInner.Equals(unitOuter) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next

            For Each unitOuter As cUnit In UnitDenominators
                isMatch = False
                For Each unitInner As cUnit In .UnitDenominators
                    If unitInner.Equals(unitOuter) Then
                        isMatch = True
                        Exit For
                    End If
                Next
                If Not isMatch Then Return False
            Next
        End With

        Return True
    End Function
#End Region

#Region "Methods: Public"
    'Add/Remove Methods
    ''' <summary>
    ''' Adds the Unit object provided to the units object.
    ''' </summary>
    ''' <param name="unit">Unit object to add.</param>
    ''' <param name="index">If specified, the Unit is inserted at this index in the list if it is a valid index.</param>
    ''' <remarks></remarks>
    Public Sub AddUnit(ByVal unit As cUnit,
                       Optional ByVal index As Integer = -1)
        If unit.Numerator Then
            addUnitsNumerator(unit, index)
        Else
            addUnitsDenominator(unit, index)
        End If
    End Sub

    ''' <summary>
    ''' Updates each Unit of a matching Type to have the same Unit Name as the provided Unit object.
    ''' </summary>
    ''' <param name="unitNew">New Unit Name to replace all matching types by.</param>
    ''' <remarks></remarks>
    Public Sub ReplaceUnitByType(ByVal unitNew As cUnit)
        For Each unit As cUnit In _unitNumerators
            If unit.Type = unitNew.Type Then unit.SetUnitName(unitNew.Name)
        Next
        For Each unit As cUnit In _unitDenominators
            If unit.Type = unitNew.Type Then unit.SetUnitName(unitNew.Name)
        Next
    End Sub

    ''' <summary>
    ''' Removes the specified Unit object from the units object.
    ''' </summary>
    ''' <param name="unit">Unit object to remove.</param>
    ''' <remarks></remarks>
    Public Overloads Sub RemoveUnit(ByVal unit As cUnit)
        If unit.Numerator Then
            RemoveUnitsNumerator(unit)
        Else
            RemoveUnitsDenominator(unit)
        End If
    End Sub
    ''' <summary>
    ''' Removes the Unit object specified by position from the units object.
    ''' </summary>
    ''' <param name="index">The index within the Numerator or denominator set from which to remove a Unit.</param>
    ''' <param name="isNumerator">True: The Unit will be removed from the Numerator set, if it exists.
    ''' Else, the Unit will be removed from the denominator set, if it exists.</param>
    ''' <remarks></remarks>
    Public Overloads Sub RemoveUnit(ByVal index As Integer,
                                    ByVal isNumerator As Boolean)
        If isNumerator Then
            removeUnitsNumerator(index)
        Else
            removeUnitsDenominator(index)
        End If
    End Sub
    ''' <summary>
    ''' Removes all Unit objects of the specified type.
    ''' </summary>
    ''' <param name="unitType">Type of unit.</param>
    ''' <param name="isNumerator">True: The Unit will be removed from the Numerator set, if it exists.
    ''' Else, the Unit will be removed from the denominator set, if it exists.</param>
    Public Overloads Sub RemoveUnit(ByVal unitType As eUnitType, ByVal isNumerator As Boolean)
        If isNumerator Then
            removeUnitsNumerator(unitType)
        Else
            removeUnitsDenominator(unitType)
        End If
    End Sub
    
    'Schema Methods
    ''' <summary>
    ''' Returns a string of the units schema with the Unit types used.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSchemaLabel() As String
        Dim numeratorString As String = parseUnitsString(UnitNumerators, True)
        Dim denominatorString As String = parseUnitsString(UnitDenominators, True)

        Return combineNumeratorAndDenominator(numeratorString, denominatorString)
    End Function

    ''' <summary>
    ''' Returns a list of all schema elements used, including powers.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSchemaList() As List(Of String)
        Dim schemaList As New List(Of String)
        Dim schemaEntry As String

        For Each unitItem As cUnit In _unitNumerators
            schemaEntry = unitItem.GetUnitLabel(True, True)
            schemaList.Add(schemaEntry)
        Next
        For Each unitItem As cUnit In _unitDenominators
            schemaEntry = unitItem.GetUnitLabel(True, True)
            schemaList.Add(schemaEntry)
        Next

        Return schemaList
    End Function

    ''' <summary>
    ''' Determines if a set of units matches the schema of the current object.
    ''' </summary>
    ''' <param name="unitsCompare">Units object to compare to the current object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SchemasMatch(ByVal unitsCompare As cUnits) As Boolean

        _schemaComponents = GetSchemaList()

        If (Not unitsCompare.UnitNumerators.Count = UnitNumerators.Count AndAlso ' Consider 10^3*cyc/sec
            (UnitNumerators.Count > 1 AndAlso Not (IsNumeric(UnitNumerators(0).Name) AndAlso unitsCompare.UnitNumerators.Count = UnitNumerators.Count - 1))) Then Return False
        'If (Not unitsCompare.UnitNumerators.Count = UnitNumerators.Count) Then Return False
        For Each unitNumeratorCompare As cUnit In unitsCompare.UnitNumerators
            If Not numeratorMatch(unitNumeratorCompare) Then Return False
        Next

        If Not unitsCompare.UnitDenominators.Count = UnitDenominators.Count Then Return False
        For Each unitDenominatorCompare As cUnit In unitsCompare.UnitDenominators
            If Not denominatorMatch(unitDenominatorCompare) Then Return False
        Next

        Return True
    End Function

    'Units Methods
    ''' <summary>
    ''' Returns a string of the composite units label with the Unit names used.
    ''' </summary>
    ''' <param name="useDefaultsIfNotSet">True: If name is not specified, rather than displaying 'NA', the default unit name will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUnitsLabel(Optional ByVal useDefaultsIfNotSet As Boolean = False) As String
        Dim numeratorString As String = parseUnitsString(UnitNumerators, parseSchema := False, useDefaultsIfNotSet := useDefaultsIfNotSet)
        Dim denominatorString As String = parseUnitsString(UnitDenominators, parseSchema := False, useDefaultsIfNotSet :=  useDefaultsIfNotSet)

        Return CombineNumeratorAndDenominator(numeratorString, denominatorString)
    End Function

    ''' <summary>
    ''' Returns the list of all base units used in the composite Unit.
    ''' </summary>
    ''' <returns></returns>
    ''' <param name="withPowers">If true, then the Unit names also have the Power listed. 
    ''' Otherwise, only the list of units is returned.</param>
    ''' <remarks></remarks>
    Public Function GetUnitsList(Optional ByVal withPowers As Boolean = False) As List(Of String)
        Dim unitList As New List(Of String)
        Dim unitEntry As String = ""

        For Each unitItem As cUnit In _unitNumerators
            unitEntry = unitItem.GetUnitLabel(False, withPowers)
            unitList.Add(unitEntry)
        Next
        For Each unitItem As cUnit In _unitDenominators
            unitEntry = unitItem.GetUnitLabel(False, withPowers)
            unitList.Add(unitEntry)
        Next

        Return unitList
    End Function

    'Manipulate/Convert Methods
    ''' <summary>
    ''' Swaps the Numerator and denominator Unit sets.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SwapNumeratorsDenominators()
        Dim swapUnits As New List(Of cUnit)
        For Each unit As cUnit In _unitNumerators
            swapUnits.Add(unit)
        Next

        _unitNumerators.Clear()
        For Each unit As cUnit In _unitDenominators
            _unitNumerators.Add(unit)
        Next

        _unitDenominators = swapUnits
    End Sub


    ''' <summary>
    ''' Returns the conversion factor to convert the supplied units object to the current units object.
    ''' </summary>
    ''' <param name="unitsConvert">Units object that is to be converted to the current units object.
    ''' It is assumed that the schemas of the two units objects match. If they don't, 0 is returned.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Convert(ByVal unitsConvert As cUnits) As Double
        Dim unitsConversionFactor As Double = 1
        Dim unitConvert As cUnit

        For Each unitNumeratorCompare As cUnit In unitsConvert.UnitNumerators
            unitConvert = getUnitByMatchingTypeNumerator(unitNumeratorCompare)
            If unitConvert Is Nothing Then Return 0
            unitsConversionFactor *= (unitConvert.Convert(unitNumeratorCompare) ^ CDbl(unitNumeratorCompare.Power))
        Next

        For Each unitDenominatorCompare As cUnit In unitsConvert.UnitDenominators
            unitConvert = getUnitByMatchingTypeDenominator(unitDenominatorCompare)
            If unitConvert Is Nothing Then Return 0
            unitsConversionFactor /= (unitConvert.Convert(unitDenominatorCompare) ^ CDbl(unitDenominatorCompare.Power))
        Next

        Return unitsConversionFactor
    End Function

    ''' <summary>
    ''' Combines two units objects into one units object as if they have been multiplied/divided together.
    ''' A resulting conversion factor is returned.
    ''' </summary>
    ''' <param name="units">Units object to combine with the current units object.</param>
    ''' <param name="divideUnits">True: The provided set of units will be inverted before being multiplied by the current units.
    ''' Else, the two sets of units will be assumed multiplied together.</param>
    ''' <param name="simplifyUnits">True: Resulting combined units will be simplified between numerator/denominator, and also between different units of the same Type if a base list is provided.</param>
    ''' <param name="simplifiedUnitsList">If provided, the list allows simplification of different units of the same Type.</param>
    ''' <remarks></remarks>
    Public Function CombineUnits(ByVal units As cUnits,
                                Optional ByVal divideUnits As Boolean = False,
                                Optional ByVal simplifyUnits As Boolean = False,
                                Optional ByVal simplifiedUnitsList As List(Of String) = Nothing) As Double
        Dim unitConversionFactor As Double = 1

        If divideUnits Then units.SwapNumeratorsDenominators()

        If simplifyUnits Then
            unitConversionFactor = combineUnitsSet(units.UnitNumerators, _unitNumerators, simplifiedUnitsList)
            unitConversionFactor /= combineUnitsSet(units.UnitDenominators, _unitDenominators, simplifiedUnitsList)
            unitConversionFactor *= simplifyNumeratorsDenominators(simplifiedUnitsList)
        Else
            combineUnitsSet(units.UnitNumerators, _unitNumerators)
            combineUnitsSet(units.UnitDenominators, _unitDenominators)
        End If

        Return unitConversionFactor
    End Function
    
    ''' <summary>
    ''' Parses a string into a units object composed of Unit objects.
    ''' </summary>
    ''' <param name="unitsToParse">String to parse into a composite units object.</param>
    ''' <param name="addToExisting">If 'true', then the parsed string objects will be added to the existing class state. 
    ''' The default 'false' means that the class state will be overwritten.</param>
    ''' <remarks></remarks>
    Public Sub ParseStringToUnits(ByVal unitsToParse As String,
                                  Optional ByVal addToExisting As Boolean = False)
        If String.IsNullOrWhiteSpace(unitsToParse) Then Return
        unitsToParse = unitsToParse.Replace(" ", "*")
        Dim units As List(Of cUnit) = cSymbolicParser.ParseStringToUnits(unitsToParse)

        If Not addToExisting Then
            _unitNumerators.Clear()
            _unitDenominators.Clear()
        End If

        For Each unitItem As cUnit In units
            If unitItem.Numerator Then
                _unitNumerators.Add(unitItem)
            Else
                _unitDenominators.Add(unitItem)
            End If
        Next
    End Sub

    ' Query
    ''' <summary>
    ''' Returns true if all units have a Unit Name set.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AreUnitsSet() As Boolean
        If (UnitNumerators.Count = 0 AndAlso
            UnitDenominators.Count = 0) Then Return False

        For Each unit As cUnit In UnitNumerators
            If String.IsNullOrEmpty(unit.Name) Then Return False
        Next

        For Each unit As cUnit In UnitDenominators
            If String.IsNullOrEmpty(unit.Name) Then Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Returns true if all units have a Type set for the schema.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsSchemaSet() As Boolean
        If (UnitNumerators.Count = 0 AndAlso
            UnitDenominators.Count = 0) Then Return False

        For Each unit As cUnit In UnitNumerators
            If unit.Type = eUnitType.None Then Return False
        Next

        For Each unit As cUnit In UnitDenominators
            If unit.Type = eUnitType.None Then Return False
        Next

        Return True
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Adds a Unit to the collection of Numerator Unit objects.
    ''' </summary>
    ''' <param name="unit">Unit object to add.</param>
    ''' <param name="index">If specified, the Unit is inserted at this index in the list if it is a valid index.</param>
    ''' <remarks></remarks>
    Private Sub addUnitsNumerator(ByVal unit As cUnit,
                                  Optional ByVal index As Integer = -1)
        If index = -1 Then
            _unitNumerators.Add(unit)
        Else
            _unitNumerators.Insert(index, unit)
        End If
    End Sub
    ''' <summary>
    ''' Removes the specified Unit object from the units Numerator collection.
    ''' </summary>
    ''' <param name="unit">Unit object to remove.</param>
    ''' <remarks></remarks>
    Private Overloads Sub removeUnitsNumerator(ByVal unit As cUnit)
        Dim tempList As New List(Of cUnit)

        For Each unitNumerator As cUnit In _unitNumerators
            If Not isMatchingUnit(unit, unitNumerator) Then
                tempList.Add(unitNumerator)
            End If
        Next

        _unitNumerators = tempList
    End Sub
    ''' <summary>
    ''' Removes a Unit object from the Numerator collection based on the provided Unit Type.
    ''' </summary>
    ''' <param name="unitType">Type of Unit to remove from the list.</param>
    ''' <remarks></remarks>
    Private Overloads Sub removeUnitsNumerator(ByVal unitType As eUnitType)
        Dim tempList As New List(Of cUnit)

        For Each unit As cUnit In _unitNumerators
            If Not unitType = unit.Type Then
                tempList.Add(unit)
            End If
        Next

        _unitNumerators = tempList
    End Sub
    ''' <summary>
    ''' Removes a Unit object from the Numerator collection based on the provided index, if valid.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Private Overloads Sub removeUnitsNumerator(ByVal index As Integer)
        Dim tempList As New List(Of cUnit)

        If _unitNumerators.Count = 0 Then Exit Sub

        For i = 0 To _unitNumerators.Count - 1
            If Not i = index Then tempList.Add(_unitNumerators(i))
        Next
        _unitNumerators = tempList
    End Sub

    ''' <summary>
    ''' Adds a Unit to the collection of denominator Unit objects.
    ''' </summary>
    ''' <param name="unit">Unit object to add.</param>
    ''' <param name="index">If specified, the Unit is inserted at this index in the list if it is a valid index.</param>
    ''' <remarks></remarks>
    Private Sub addUnitsDenominator(ByVal unit As cUnit,
                                    Optional ByVal index As Integer = -1)
        If index = -1 Then
            _unitDenominators.Add(unit)
        Else
            _unitDenominators.Insert(index, unit)
        End If
    End Sub
    ''' <summary>
    ''' Removes the specified Unit object from the units denominator collection.
    ''' </summary>
    ''' <param name="unit">Unit object to remove.</param>
    ''' <remarks></remarks>
    Private Overloads Sub removeUnitsDenominator(ByVal unit As cUnit)
        Dim tempList As New List(Of cUnit)

        For Each unitDenominator As cUnit In _unitDenominators
            If Not isMatchingUnit(unit, unitDenominator) Then
                tempList.Add(unitDenominator)
            End If
        Next

        _unitDenominators = tempList
    End Sub
    ''' <summary>
    ''' Removes a Unit object from the denominator collection based on the provided Unit Type.
    ''' </summary>
    ''' <param name="unitType">Type of Unit to remove from the list.</param>
    ''' <remarks></remarks>
    Private Overloads Sub removeUnitsDenominator(ByVal unitType As eUnitType)
        Dim tempList As New List(Of cUnit)

        For Each unit As cUnit In _unitDenominators
            If Not unitType = unit.Type Then
                tempList.Add(unit)
            End If
        Next

        _unitDenominators = tempList
    End Sub
    ''' <summary>
    ''' Removes a Unit object from the denominator collection based on the provided index, if valid.
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Private Overloads Sub removeUnitsDenominator(ByVal index As Integer)
        Dim tempList As New List(Of cUnit)

        If _unitDenominators.Count = 0 Then Exit Sub

        For i = 0 To _unitDenominators.Count - 1
            If Not i = index Then tempList.Add(_unitDenominators(i))
        Next
        _unitDenominators = tempList
    End Sub

    ''' <summary>
    ''' Determines if the two Unit objects describe the same Unit in terms of overall placement &amp; quality.
    ''' </summary>
    ''' <param name="unitBase">First Unit to compare.</param>
    ''' <param name="unitCompare">Second Unit to compare.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function isMatchingUnit(ByVal unitBase As cUnit,
                                    ByVal unitCompare As cUnit) As Boolean
        If (unitBase.Type = unitCompare.Type AndAlso
                unitBase.Power = unitCompare.Power AndAlso
                unitBase.Name = unitCompare.Name) Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Returns a string representing the units object, showing either the schema structure or the units as written with Unit names.
    ''' </summary>
    ''' <param name="unitsList">List of units.</param>
    ''' <param name="parseSchema">True: The parsing will be only be done based on the schema of the provided Unit objects. 
    ''' False: The parsing will be on the Unit names of the provided Unit objects.</param>
    ''' <param name="useDefaultsIfNotSet">True: If name is not specified, rather than displaying 'NA', the default unit name will be used.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function parseUnitsString(ByVal unitsList As ICollection(Of cUnit),
                                             Optional ByVal parseSchema As Boolean = False,
                                             Optional ByVal useDefaultsIfNotSet As Boolean = False) As String
        Dim schemaString As String = String.Empty

        For Each unit As cUnit In unitsList
            Dim unitString As String = unit.GetUnitLabel(parseSchema, 
                                                         withPowers:= True, 
                                                         asList := False,
                                                         useDefaultsIfNA := useDefaultsIfNotSet)

            If Not String.IsNullOrEmpty(schemaString) Then schemaString &= _MULTIPLIER
            schemaString &= unitString
        Next

        If unitsList.Count > 1 Then
            schemaString = _OPENPARENTHESIS & schemaString & _CLOSEPARENTHESIS
        End If

        Return schemaString
    End Function

    ''' <summary>
    ''' Takes the provided Numerator &amp; denominator strings and depending on content, combines them as 'a', 'a/b', or '1/b'.
    ''' </summary>
    ''' <param name="numeratorString">String representing the Numerator components of the units object.</param>
    ''' <param name="denominatorString">String representing the denominator components of the units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function combineNumeratorAndDenominator(ByVal numeratorString As String,
                                                    ByVal denominatorString As String) As String
        If (Not String.IsNullOrEmpty(denominatorString) AndAlso
            Not String.IsNullOrEmpty(numeratorString)) Then

            Return numeratorString & _DIVISOR & denominatorString
        ElseIf (String.IsNullOrEmpty(numeratorString) AndAlso
                Not String.IsNullOrEmpty(denominatorString)) Then

            Return "1" & _DIVISOR & denominatorString
        ElseIf (Not String.IsNullOrEmpty(numeratorString) AndAlso
                String.IsNullOrEmpty(denominatorString)) Then
            ' Remove any parentheses surrounding the numerators
            numeratorString = numeratorString.Replace("(", "")
            numeratorString = numeratorString.Replace(")", "")
            Return numeratorString
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' For units in the same set (e.g. Numerator, or denominator), combines the units by either adding the Unit (if new) or adjusting the exponents (if the Unit already exists).
    ''' </summary>
    ''' <param name="unitsAdded">Set of units to add.</param>
    ''' <param name="unitsExisting">Existing set of units to add to.</param>
    ''' <param name="unitsBase">If specified, these are the units to simplify any mismatched units of matching types to.</param>
    ''' <remarks></remarks>
    Private Shared Function combineUnitsSet(ByVal unitsAdded As IEnumerable(Of cUnit),
                                ByRef unitsExisting As List(Of cUnit),
                                Optional ByVal unitsBase As List(Of String) = Nothing) As Double
        Dim unitExistsInSet As Boolean = False
        Dim unitConversionFactor As Double = 1

        For Each addedUnit As cUnit In unitsAdded
            unitExistsInSet = False

            For Each existingUnit As cUnit In unitsExisting
                If addedUnit.Type = existingUnit.Type Then
                    If addedUnit.Name = existingUnit.Name Then
                        simplifyUnitMultiply(existingUnit, addedUnit)
                        unitExistsInSet = True
                        Exit For
                    Else
                        If unitsBase IsNot Nothing Then
                            If ListLibrary.ExistsInList(addedUnit.Name, unitsBase) Then
                                unitConversionFactor *= addedUnit.Convert(existingUnit, addedUnit)
                                simplifyUnitMultiply(existingUnit, addedUnit)
                                unitExistsInSet = True
                                Exit For
                            ElseIf ListLibrary.ExistsInList(existingUnit.Name, unitsBase) Then
                                unitConversionFactor *= existingUnit.Convert(addedUnit, existingUnit)
                                simplifyUnitMultiply(existingUnit, addedUnit)
                                unitExistsInSet = True
                                Exit For
                            Else
                                'No simplification is performed as no matching Unit was found for conversion.
                            End If
                        Else
                            'No simplication is performed among non-matching units of the same Type without a specified base Unit.
                        End If
                    End If
                Else
                    'No simplification is performed among non-matching Unit types.
                End If
            Next

            If Not unitExistsInSet Then unitsExisting.Add(addedUnit)
        Next

        Return unitConversionFactor
    End Function

    ''' <summary>
    ''' Simplifies the two matching units by combining powers.
    ''' </summary>
    ''' <param name="existingUnit">Current Unit.</param>
    ''' <param name="addedUnit">Unit being added.</param>
    ''' <remarks></remarks>
    Private Shared Sub simplifyUnitMultiply(ByRef existingUnit As cUnit,
                                     ByRef addedUnit As cUnit)
       'existingUnit.SetUnitPower(existingUnit.MultiplyUnitPowers(addedUnit.Power))
       existingUnit.SetUnitPower(existingUnit.MultiplyUnitPowers(addedUnit))
    End Sub

    ''' <summary>
    ''' Checks the units in the Numerator and denominator sets. 
    ''' If the Unit exists in both, it is simplified based on powers such that it only exists in one set.
    ''' Unless the specific Unit is specified, if the units are of the same Type but different Unit, no simplification will be perfomed for a given Unit.
    ''' </summary>
    ''' <param name="unitsBase">If specified, these are the units to simplify any mismatched units of matching types to.</param>
    ''' <remarks></remarks>
    Private Function simplifyNumeratorsDenominators(Optional ByVal unitsBase As List(Of String) = Nothing) As Double
        Dim unitConversionFactor As Double = 1

        For Each numeratorUnit As cUnit In _unitNumerators
            For Each denominatorUnit As cUnit In _unitDenominators
                If numeratorUnit.Type = denominatorUnit.Type Then
                    If numeratorUnit.Name = denominatorUnit.Name Then
                        simplifyUnitNumeratorDenominator(numeratorUnit, denominatorUnit)
                        Exit For
                    Else
                        If unitsBase IsNot Nothing Then
                            If ListLibrary.ExistsInList(numeratorUnit.Name, unitsBase) Then
                                unitConversionFactor /= numeratorUnit.Convert(denominatorUnit, numeratorUnit)
                                simplifyUnitNumeratorDenominator(numeratorUnit, denominatorUnit)
                                Exit For
                            ElseIf ListLibrary.ExistsInList(denominatorUnit.Name, unitsBase) Then
                                unitConversionFactor *= denominatorUnit.Convert(numeratorUnit, denominatorUnit)
                                simplifyUnitNumeratorDenominator(numeratorUnit, denominatorUnit)
                                Exit For
                            Else
                                'No simplification is performed as no matching Unit was found for conversion.
                            End If
                        Else
                            'No simplication is performed among non-matching units of the same Type without a specified base Unit.
                        End If
                    End If
                Else
                    'No simplification is performed among non-matching Unit types.
                End If
            Next
        Next

        Return unitConversionFactor
    End Function

    ''' <summary>
    ''' Simplifies the two matching units by combining powers and swapping Numerator/denominator classification if necessary.
    ''' </summary>
    ''' <param name="numeratorUnit">Unit in the Numerator position.</param>
    ''' <param name="denominatorUnit">Unit in the denominator position.</param>
    ''' <remarks></remarks>
    Private Sub simplifyUnitNumeratorDenominator(ByRef numeratorUnit As cUnit,
                                                 ByRef denominatorUnit As cUnit)
        'numeratorUnit.SetUnitPower(numeratorUnit.DivideUnitPowers(denominatorUnit.Power))
        numeratorUnit.SetUnitPower(numeratorUnit.DivideUnitPowers(denominatorUnit))

        removeUnitsDenominator(denominatorUnit)

        'If cSymbolicBlockPower.IsPowerDenominator(numeratorUnit.Power) Then
        '    'Change sign
        '    numeratorUnit.SetUnitPower(Right(numeratorUnit.Power, numeratorUnit.Power.Length - 1))

        '    'Swap Numerator in denominator position
        '    addUnitsDenominator(numeratorUnit)
        '    removeUnitsNumerator(numeratorUnit)
        'End If
    End Sub

    ''' <summary>
    ''' Determines if the provided Unit matches any of the Numerator entities of the schema.
    ''' </summary>
    ''' <param name="unitCompare">Unit object to compare to the current schema.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function numeratorMatch(ByVal unitCompare As cUnit) As Boolean
        Dim unitSchemaComponent As String = unitCompare.GetUnitLabel(True, True)

        If _schemaComponents.Count = 0 Then _schemaComponents = GetSchemaList()

        For Each schemaComponent As String In _schemaComponents
            If unitSchemaComponent = schemaComponent Then Return True
        Next

        Return False
    End Function
    ''' <summary>
    ''' Determines if the provided Unit matches any of the denominator entities of the schema.
    ''' </summary>
    ''' <param name="unitCompare">Unit object to compare to the current schema.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function denominatorMatch(ByVal unitCompare As cUnit) As Boolean
        Return numeratorMatch(unitCompare)
    End Function

    ''' <summary>
    ''' Returns the Unit object within the current collection that has the same Type as the provided Unit in the Numerator position.
    ''' Returns Nothing if no match is found.
    ''' </summary>
    ''' <param name="unit">Unit object to use for finding a matching Unit object by Type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getUnitByMatchingTypeNumerator(ByVal unit As cUnit) As cUnit
        For Each unitBase As cUnit In UnitNumerators
            If unit.Type = unitBase.Type Then Return unitBase
        Next

        Return Nothing
    End Function
    ''' <summary>
    ''' Returns the Unit object within the current collection that has the same Type as the provided Unit in the denominator position.
    ''' Returns Nothing if no match is found.
    ''' </summary>
    ''' <param name="unit">Unit object to use for finding a matching Unit object by Type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getUnitByMatchingTypeDenominator(ByVal unit As cUnit) As cUnit
        For Each unitBase As cUnit In UnitDenominators
            If unit.Type = unitBase.Type Then Return unitBase
        Next

        Return Nothing
    End Function
#End Region
End Class
