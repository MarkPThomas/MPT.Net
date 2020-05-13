Option Strict On
Option Explicit On

Imports MPT.SymbolicMath

''' <summary>
''' Takes a string and parses it into the mathematical symbolic object specified, such as units.
''' </summary>
''' <remarks></remarks>
Public Class cSymbolicParser
#Region "Constants"
    'Private Const _POWER As String = "^"
    'Private Const _MULTIPLIER As String = "*"
    'Private Const _MULTIPLIER_ALT As String = "-"
    'Private Const _DIVISOR As String = "/"
    'Private Const _OPEN_PARENTHESIS As String = "("
    'Private Const _CLOSE_PARENTHESIS As String = ")"
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns a list of Unit objects created from the provided string.
    ''' </summary>
    ''' <param name="value">String to transform into a list of Unit objects.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ParseStringToUnits(ByVal value As String) As List(Of cUnit)
        Dim blocksList As IEnumerable(Of cSymbolicBlock) = parseSymbolic(value)
        'TODO: Remove
        'Dim blocksList As IEnumerable(Of cSymbolicBlock) = parseBlockList(value)
        Dim unitBlocks As New List(Of cUnit)

        For Each symbolicBlock As cSymbolicBlock In blocksList
            Dim unitBlock As New cUnit
            With unitBlock
                .Numerator = symbolicBlock.IsNumerator
                If Not String.IsNullOrEmpty(symbolicBlock.BlockSuperscript) Then .SetUnitPower(symbolicBlock.BlockSuperscript)
                .SetUnitName(symbolicBlock.BlockName)
                .SetUnitTypeFromName()
            End With
            unitBlocks.Add(unitBlock)
        Next

        Return unitBlocks
    End Function

#End Region

#Region "Methods: Private"
    Private Shared Function parseSymbolic(ByVal value As String) As IEnumerable(Of cSymbolicBlock)
        Dim blocksListTotal As New List(Of cSymbolicBlock)
        Dim quotient As New ProductQuotientSet(value)
        For Each item As UnitOperatorPair In quotient
            Dim block as cSymbolicBlock
            If (TypeOf item.Unit Is PrimitiveUnit) Then
                block = New cSymbolicBlock(CType(item.Unit, PrimitiveUnit)) With {
                    .IsNumerator = isItemNumerator(item)
                }
                blocksListTotal.Add(block)
            ElseIf (TypeOf item.Unit Is ProductQuotientSet) Then
                Dim newSet As ProductQuotientSet = CType(item.Unit, ProductQuotientSet)
                Dim isNumerator As Boolean = isItemNumerator(item)
                If (newSet.Count > 1 AndAlso Not isNumerator) Then
                    For Each unitOperator As UnitOperatorPair In newSet
                        Dim unit As IBase = unitOperator.Unit
                        If (TypeOf unit Is PrimitiveUnit OrElse TypeOf unit Is ProductQuotientSet) Then
                            block = New cSymbolicBlock(CType(unit, PrimitiveUnit)) With {
                                .IsNumerator = isNumerator
                            }
                            blocksListTotal.Add(block)
                        End If
                    Next
                Else
                    block = New cSymbolicBlock(newSet) With {
                        .IsNumerator = isItemNumerator(item)
                    }
                    blocksListTotal.Add(block)
                End If
            End If
        Next

        Return blocksListTotal
    End Function

    Private Shared Function isItemNumerator(ByVal item As UnitOperatorPair) As Boolean
        Return Not item.Operator = "/"
    End Function

    '''' <summary>
    '''' Returns a list of symbolic block objects created from the provided string.
    '''' </summary>
    '''' <param name="value">String to transform into a list of symbolic block objects.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Shared Function parseBlockList(ByVal value As String) As IEnumerable(Of cSymbolicBlock)
    '    Dim blocksList As List(Of cSymbolicBlock) = parseBlockListItem(value, isGlobalNumerator:=True)
    '    If blocksList.Count > 1 Then
    '        blocksList = parseBlockListAll(blocksList)
    '    End If

    '    Return blocksList
    'End Function

    '''' <summary>
    '''' Parses all symbolic blocks into additional symbolic blocks until no additional parsing can be done. 
    '''' Returns a list of every symbolic block in the smalles/simplest form possible.
    '''' </summary>
    '''' <param name="blocksList">List of symbolic block items to further parse.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Shared Function parseBlockListAll(ByVal blocksList As IEnumerable(Of cSymbolicBlock)) As List(Of cSymbolicBlock)
    '    Dim blocksListTotal As New List(Of cSymbolicBlock)

    '    For Each symbolicBlock As cSymbolicBlock In blocksList
    '        Dim parsedBlockList As List(Of cSymbolicBlock) = parseBlockListItem(symbolicBlock.BlockName, isGlobalNumerator:=symbolicBlock.IsNumerator)

    '        If parsedBlockList.Count > 1 Then
    '            Dim subBlockList As List(Of cSymbolicBlock) = parseBlockListAll(parsedBlockList)

    '            For Each subSymbolicBlock As cSymbolicBlock In subBlockList
    '                blocksListTotal.Add(subSymbolicBlock)
    '            Next
    '        Else
    '            blocksListTotal.Add(symbolicBlock)
    '        End If
    '    Next

    '    Return blocksListTotal
    'End Function

    '''' <summary>
    '''' Parses the individual string into a symbolic block list of 1-level deep.
    '''' </summary>
    '''' <param name="value">String to parse into a symbolic block list.</param>
    '''' <param name="isGlobalNumerator">Designation of whether or not the string as a whole is considered to be in the Numerator position (true) or denominator position (false).</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Shared Function parseBlockListItem(ByVal value As String,
    '                                    ByVal isGlobalNumerator As Boolean) As List(Of cSymbolicBlock)
    '    'Note: These are not made as private class properties because unique instances are needed in the 
    '    '       recursive function that calls this function!
    '    Dim parenthesesCount As Integer = 0
    '    Dim parenthesesCounting As Boolean = False
    '    Dim powerCounting As Boolean = False
    '    Dim blockBase As String = String.Empty
    '    Dim blockPower As String = String.Empty
    '    Dim isLocalNumerator As Boolean = True
    '    Dim isNumerator As Boolean = isResultNumerator(isLocalNumerator, isGlobalNumerator)
    '    Dim blocksList As New List(Of cSymbolicBlock)

    '    For Each character As Char In value
    '        Select Case character
    '            Case CChar(_OPEN_PARENTHESIS)
    '                parenthesesCount += 1
    '                parenthesesCounting = True
    '                If parenthesesCount = 1 Then Continue For
    '            Case CChar(_CLOSE_PARENTHESIS)
    '                parenthesesCount -= 1
    '                If parenthesesCount = 0 Then
    '                    blocksList = updateBlocksList(blocksList, isNumerator, blockBase, blockPower)

    '                    blockBase = String.Empty
    '                    blockPower = String.Empty
    '                    parenthesesCounting = False
    '                    Continue For
    '                End If
    '            Case CChar(_DIVISOR)
    '                'Only consider characters outside of blocks and powers
    '                If Not parenthesesCounting Then
    '                    blocksList = updateBlocksList(blocksList, isNumerator, blockBase, blockPower)

    '                    powerCounting = False
    '                    blockBase = String.Empty
    '                    blockPower = String.Empty
    '                    isLocalNumerator = False
    '                    isNumerator = isResultNumerator(isLocalNumerator, isGlobalNumerator)
    '                    Continue For
    '                End If
    '            Case CChar(_MULTIPLIER), CChar(_MULTIPLIER_ALT)
    '                'Only consider characters outside of blocks and powers
    '                If Not parenthesesCounting Then
    '                    blocksList = updateBlocksList(blocksList, isNumerator, blockBase, blockPower)

    '                    powerCounting = False
    '                    blockBase = String.Empty
    '                    blockPower = String.Empty
    '                    isLocalNumerator = True
    '                    isNumerator = isResultNumerator(isLocalNumerator, isGlobalNumerator)
    '                    Continue For
    '                End If
    '            Case CChar(_POWER)
    '                'This is meant to occur only at the lowest level block
    '                'It always occurs after the base is done, so the switch is never reversed 
    '                'Assumed that powers are never stacked
    '                If Not parenthesesCounting Then
    '                    powerCounting = True
    '                    Continue For
    '                End If
    '            Case Else
    '                Continue For
    '        End Select
    '        aggregateBlockString(character, blockBase, blockPower, useCharacterForPower:=powerCounting)
    '    Next

    '    'If the string encompasses only one block, record it
    '    If (blocksList.Count = 0 AndAlso value.Length > 0) Then
    '        'Account for if Power is negative
    '        If cSymbolicBlockPower.IsPowerDenominator(blockPower) Then
    '            isLocalNumerator = False
    '            isNumerator = isResultNumerator(isLocalNumerator, isGlobalNumerator)
    '            blockPower = Right(blockPower, blockPower.Length - 1)
    '        End If

    '        blocksList = updateBlocksList(blocksList, isNumerator, blockBase, blockPower)
    '        blockBase = String.Empty
    '    End If

    '    'Record any unrecorded block
    '    If Not String.IsNullOrEmpty(blockBase) Then blocksList = updateBlocksList(blocksList, isNumerator, blockBase, blockPower)

    '    Return blocksList
    'End Function

    '''' <summary>
    '''' Adds the character to the appropriate string provided.
    '''' </summary>
    '''' <param name="character">Character to add to a string.</param>
    '''' <param name="blockName">The string that is used for the base/Name of the symbolic block.</param>
    '''' <param name="blockPower">The string that is used for the Power/exponent of the symbolic block.</param>
    '''' <param name="useCharacterForPower">True: Character will be added to the block Power parameter. 
    '''' Else, the character will be added to the block base parameter.</param>
    '''' <remarks></remarks>
    'Private Shared Sub aggregateBlockString(ByVal character As String,
    '                                 ByRef blockName As String,
    '                                 ByRef blockPower As String,
    '                                 ByVal useCharacterForPower As Boolean)
    '    If useCharacterForPower Then
    '        blockPower &= character
    '    Else
    '        blockName &= character
    '    End If
    'End Sub

    '''' <summary>
    '''' Returns a list of symbolic block objects with the new symbolic block object added, if valid.
    '''' Purpose of the function is to include validation checks of data.
    '''' If the symbolic block object is not valid, the original list provided is returned unaltered.
    '''' </summary>
    '''' <param name="blocksList">List to which the symbolic block is to be added.</param>
    '''' <param name="isNumerator">Specifies if the symbolic block is to be treated as a Numerator (true), else denominator (false).</param>
    '''' <param name="blockName">String to be used as the base Name of the symbolic object.</param>
    '''' <param name="blockPower">String to be used as the exponent of the symbolic object. May be blank.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Shared Function updateBlocksList(ByRef blocksList As List(Of cSymbolicBlock),
    '                                  ByVal isNumerator As Boolean,
    '                                  ByVal blockName As String,
    '                                  ByVal blockPower As String) As List(Of cSymbolicBlock)
    '    Dim newBlock As cSymbolicBlock = recordBlock(isNumerator, blockName, blockPower)
    '    Dim updatedBlocks As List(Of cSymbolicBlock) = blocksList

    '    If Not String.IsNullOrEmpty(newBlock.BlockName) Then updatedBlocks.Add(newBlock)

    '    Return updatedBlocks
    'End Function

    '''' <summary>
    '''' Returns the symbolic block object of the paramters provided if the parameters provided are valid.
    '''' Otherwise returns an empty object.
    '''' </summary>
    '''' <param name="isNumerator">Specifies if the symbolic block is to be treated as a Numerator (true), else denominator (false).</param>
    '''' <param name="blockName">String to be used as the base Name of the symbolic object. 
    '''' If blank, an empty object is returned.</param>
    '''' <param name="blockPower">String to be used as the exponent of the symbolic object. May be blank.</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Shared Function recordBlock(ByVal isNumerator As Boolean,
    '                             ByVal blockName As String,
    '                             Optional ByVal blockPower As String = "") As cSymbolicBlock

    '    Dim newBlock As New cSymbolicBlock

    '    If String.IsNullOrEmpty(blockName) Then Return newBlock

    '    With newBlock
    '        .IsNumerator = isNumerator
    '        '.BlockName = blockName
    '        '.BlockSuperscript = blockPower
    '    End With

    '    Return newBlock
    'End Function

    '''' <summary>
    '''' Determines if the current block is in the Numerator or denominator position based on the superposition of the original positiona and the locally determined position.
    '''' </summary>
    '''' <param name="isLocalNumerator">Position at the current block level as Numerator (true) or denominator (false).</param>
    '''' <param name="isGlobalNumerator">Position above the current block level as Numerator (true) or denominator (false).</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Shared Function isResultNumerator(ByVal isLocalNumerator As Boolean,
    '                                   ByVal isGlobalNumerator As Boolean) As Boolean
    '    Return Not (isLocalNumerator Xor isGlobalNumerator)
    'End Function
#End Region

End Class
