Option Strict On
Option Explicit On

''' <summary>
''' Class representing the exponent Power of a number or variable, including both the characteristics of the Power, and operations of combination.
''' Currently this class' combination methods only work with numbers. Symbolic combinations are not supported.
''' </summary>
''' <remarks></remarks>
Public Class cSymbolicBlockPower
#Region "Constants"
    Private Const _POWER_CHAR As String = "^"
    Private Const _MULTIPLIER As String = "*"
    Private Const _MULTIPLIER_ALT As String = "-"
    Private Const _DIVISOR As String = "/"
    Private Const _OPENPARENTHESIS As String = "("
    Private Const _CLOSEPARENTHESIS As String = ")"
    Private Const _DECIMAL As String = "."
    Private Const _POWER_DENOMINATOR As String = "-"
    Private Const _TOLERANCE As Double = 0.0001
    Private Const _INFINITY As String = "Infinity"
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' True: The powers are assumed to have a format of ^(n/m)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property IsFractionFormat As Boolean?
    ''' <summary>
    ''' True: The powers are assumed to have a format of ^n.mmm
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property IsDecimalFormat As Boolean?
        Get
            Return Not IsFractionFormat
        End Get
    End Property


    Private _symbolicPower As cSymbolicFraction

    ''' <summary>
    ''' The Numerator value that is a decimal. This is not included in the integer component.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property Numerator As cSymbolicFraction
    ''' <summary>
    ''' The denominator value that is an integer. This is not included in the decimal component.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property Denominator As cSymbolicFraction


    '''' <summary>
    '''' The Numerator value that is an integer. This is not included in the decimal component.
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Friend ReadOnly Property NumeratorAsInteger As Integer = 1
    '''' <summary>
    '''' The Numerator value that is an decimal. This is not included in the integer component.
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Friend ReadOnly Property NumeratorAsDouble As Double = 1
    '''' <summary>
    '''' The denominator value that is an integer. This is not included in the decimal component.
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Friend ReadOnly Property DenominatorAsInteger As Integer = 1
    '''' <summary>
    '''' The denominator value that is an decimal. This is not included in the integer component.
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Friend ReadOnly Property DenominatorAsDouble As Double = 1

    ''' <summary>
    ''' The string representation of the combined result of the class properties.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property Power As String = String.Empty
#End Region

#Region "Initialization"
    Friend Sub New()
    End Sub

    Friend Sub New(ByVal newPower As String)
        ParsePowerNumeratorDenominator(newPower)
    End Sub
#End Region

#Region "Method: Friend"
    ''' <summary>
    ''' Populates the class properties based on the Power provided.
    ''' </summary>
    ''' <param name="newPower">The Power to be used to populate the class properties.</param>
    ''' <remarks></remarks>
    Friend Sub ParsePowerNumeratorDenominator(ByVal newPower As String)
        'Note: Keep combinations as integer or double until the end, in an attempt to maintain integers
        If String.IsNullOrEmpty(newPower) Then Return
        If _IsFractionFormat IsNot Nothing Then Return
        'Dim currentString As String = String.Empty
        'Dim isDenominator = False

        'Dim currentNumerator As String = String.Empty
        'Dim currentDenominator As String = String.Empty

        'Determine "/" format
        'If "/" format, isolate Numerator/denominator
        For i = 0 To newPower.Length - 1
            Dim letter As Char = newPower(i)

            If (_IsFractionFormat Is Nothing AndAlso letter = _DECIMAL) Then
                _IsFractionFormat = False
                Return
            ElseIf (_IsFractionFormat Is Nothing AndAlso letter = _DIVISOR) Then
                _IsFractionFormat = True
                Return
            End If
            'currentString &= letter

            'If (_IsFractionFormat Is Nothing AndAlso letter = _DECIMAL) Then
            '    _IsFractionFormat = False
            '    currentString &= letter
            'ElseIf (_IsFractionFormat Is Nothing AndAlso letter = _DIVISOR AndAlso Not isDenominator) Then
            '    _IsFractionFormat = True
            '    isDenominator = True
            '    currentNumerator = currentString
            '    '_Numerator *= New cSymbolicFraction(currentString)

            '    'If IsDecimalFormat Then
            '    '    _NumeratorAsDouble *= CDbl(currentString)
            '    'Else
            '    '    _NumeratorAsInteger *= CInt(currentString)
            '    'End If
            '    currentString = String.Empty
            'ElseIf letter = _DIVISOR AndAlso isDenominator Then   'Multiple divisions are occurring
            '    isDenominator = True
            '    _Denominator *= New cSymbolicFraction(currentString)
            '    'If IsDecimalFormat Then
            '    '    _DenominatorAsDouble *= CDbl(currentString)
            '    'Else
            '    '    _DenominatorAsInteger *= CInt(currentString)
            '    'End If
            '    currentString = String.Empty
            'ElseIf (letter = _MULTIPLIER OrElse
            '        (letter = _MULTIPLIER_ALT AndAlso i > 0)) Then
            '    _Numerator *= New cSymbolicFraction(currentString)
            '    'If IsDecimalFormat Then
            '    '    _NumeratorAsDouble *= CDbl(currentString)
            '    'Else
            '    '    _NumeratorAsInteger *= CInt(currentString)
            '    'End If
            '    If isDenominator Then isDenominator = False
            '    currentString = String.Empty
            'Else
            '    currentString &= letter
            'End If
        Next

        'If Not String.IsNullOrEmpty(currentString) Then
        '    If _IsFractionFormat Then
        '        _Denominator *= New cSymbolicFraction(currentString)
        '        'If IsDecimalFormat Then
        '        '    _DenominatorAsDouble *= CDbl(currentString)
        '        'Else
        '        '    _DenominatorAsInteger *= CInt(currentString)
        '        'End If
        '    Else
        '        _Numerator *= New cSymbolicFraction(currentString)
        '        'If IsDecimalFormat Then
        '        '    _NumeratorAsDouble *= CDbl(currentString)
        '        'Else
        '        '    _NumeratorAsInteger *= CInt(currentString)
        '        'End If
        '    End If
        'End If

        ''If _IsFractionFormat AndAlso
        ''   _DenominatorAsInteger = 1 AndAlso
        ''   Math.Abs(_DenominatorAsDouble - 1) < _TOLERANCE Then
        ''    _IsFractionFormat = False
        ''End If

        'updatePowerString()
    End Sub

    ''' <summary>
    ''' Checks the Power property of the block and returns true if it is negative, indicating a denominator position.
    ''' Else, returns False.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPowerDenominator() As Boolean
        Return IsPowerDenominator(Power)
    End Function

    ''' <summary>
    ''' Checks the Power property of the block provide and returns true if it is negative, indicating a denominator position.
    ''' Else, returns False.
    ''' </summary>
    ''' <param name="blockPower">The Power string to be checked for the status.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function IsPowerDenominator(ByVal blockPower As String) As Boolean
        If String.IsNullOrEmpty(blockPower) Then Return False

        Return blockPower(0) = _POWER_DENOMINATOR
    End Function


    ''' <summary>
    ''' Combines the Power Numerator &amp; denominator components as if their respective bases were being multiplied.
    ''' </summary>
    ''' <param name="otherPower">Power to multiply.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CombinePowersBaseMultiply(ByVal otherPower As cSymbolicBlockPower) As cSymbolicBlockPower
        Return CombinePowersBaseMultiply(otherPower, Me)
    End Function

    ''' <summary>
    ''' Combines the Power Numerator &amp; denominator components as if their respective bases were being multiplied.
    ''' It is assumed that the bases are the same.
    ''' </summary>
    ''' <param name="power1">First Power to multiply.</param>
    ''' <param name="power2">Second Power to multiply.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function CombinePowersBaseMultiply(ByVal power1 As cSymbolicBlockPower,
                                                     ByVal power2 As cSymbolicBlockPower) As cSymbolicBlockPower
        If power1 Is Nothing OrElse power2 Is Nothing Then Throw New ArgumentNullException("Power was null.")

        'Dim isNumeratorInteger As Boolean = (Not power1.NumeratorAsInteger = 1 OrElse Not power2.NumeratorAsInteger = 1)
        'Dim isDenominatorInteger As Boolean = (Not power1.DenominatorAsInteger = 1 OrElse Not power2.DenominatorAsInteger = 1)

        'Dim numerator1 As Double
        'If Not power1.NumeratorAsInteger = 1 Then
        '    numerator1 = power1.NumeratorAsDouble
        'Else
        '    numerator1 = power1.NumeratorAsInteger
        'End If

        'Dim denominator1 As Double
        'If Not power1.DenominatorAsInteger = 1 Then
        '    denominator1 = power1.DenominatorAsDouble
        'Else
        '    denominator1 = power1.DenominatorAsInteger
        'End If

        'Dim numerator2 As Double
        'If Not power2.NumeratorAsInteger = 1 Then
        '    numerator2 = power2.NumeratorAsDouble
        'Else
        '    numerator2 = power2.NumeratorAsInteger
        'End If

        'Dim denominator2 As Double
        'If Not power2.DenominatorAsInteger = 1 Then
        '    denominator2 = power2.DenominatorAsDouble
        'Else
        '    denominator2 = power2.DenominatorAsInteger
        'End If

        'Dim newNumerator As Double
        'Dim newDenominator As Double = 1

        'If (power1.IsFractionFormat OrElse power2.IsFractionFormat) Then
        '    If (Math.Abs(denominator1 - denominator2) < 0.0001) Then '' Denominators of powers are equal
        '        newNumerator = numerator1 + numerator2
        '        newDenominator = denominator1
        '    Else '' Common denominator is used
        '        newNumerator = numerator1 * denominator2 + numerator2 * denominator1
        '        newDenominator = denominator1 * denominator2
        '    End If
        'Else
        '    newNumerator = numerator1 + numerator2
        'End If

        Dim resultPower As New cSymbolicBlockPower
        With resultPower
            'If (isNumeratorInteger) Then
            '    ._NumeratorAsInteger = CType(newNumerator, Integer)
            'Else
            '    ._NumeratorAsDouble = newNumerator
            'End If
            'If (isDenominatorInteger) Then
            '    ._DenominatorAsInteger = CType(newDenominator, Integer)
            'Else
            '    ._DenominatorAsDouble = newDenominator
            'End If
            ._symbolicPower = power1._symbolicPower + power2._symbolicPower
            If (power1.IsFractionFormat OrElse power2.IsFractionFormat) Then ._IsFractionFormat = True

            .updatePowerString()
        End With

        Return resultPower
    End Function



    ''' <summary>
    ''' Combines the Power Numerator &amp; denominator components as if their respective bases were being divided.
    ''' </summary>
    ''' <param name="powerDenominator">Power that is part of a base in the denominator position.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function CombinePowersBaseDivide(ByVal powerDenominator As cSymbolicBlockPower) As cSymbolicBlockPower
        Return CombinePowersBaseDivide(Me, powerDenominator)
    End Function

    ''' <summary>
    ''' Combines the Power Numerator &amp; denominator components as if their respective bases were being divided.
    ''' </summary>
    ''' <param name="powerofBaseDenominator">Power that is part of a base in the denominator position.</param>
    ''' <param name="powerOfBaseNumerator">Power that is part of a base in the numerator position.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function CombinePowersBaseDivide(ByVal powerOfBaseNumerator As cSymbolicBlockPower,
                                                   ByVal powerofBaseDenominator As cSymbolicBlockPower) As cSymbolicBlockPower
        If powerOfBaseNumerator Is Nothing Then Throw New ArgumentNullException("Numerator was null.")
        If powerofBaseDenominator Is Nothing Then Throw New ArgumentNullException("Denominator was null.")

        Dim resultPower As New cSymbolicBlockPower
        With resultPower
            '._NumeratorAsInteger = powerOfBaseNumerator.NumeratorAsInteger - powerofBaseDenominator.NumeratorAsInteger
            '._NumeratorAsDouble = powerOfBaseNumerator.NumeratorAsDouble - powerofBaseDenominator.NumeratorAsDouble

            '._DenominatorAsInteger = powerOfBaseNumerator.DenominatorAsInteger - powerofBaseDenominator.DenominatorAsInteger
            '._DenominatorAsDouble = powerOfBaseNumerator.DenominatorAsDouble - powerofBaseDenominator.DenominatorAsDouble

            ._symbolicPower = powerOfBaseNumerator._symbolicPower - powerofBaseDenominator._symbolicPower

            If (powerOfBaseNumerator.IsFractionFormat OrElse powerofBaseDenominator.IsFractionFormat) Then ._IsFractionFormat = True

            .updatePowerString()
        End With

        Return resultPower
    End Function
#End Region

#Region "Method: Private"

    ''' <summary>
    ''' Updates the Power string property based on the current numerical properties. 
    ''' Returns the result that has been set in the class.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function updatePowerString() As String
        Dim currentString As String = String.Empty

        'If IsDecimalFormat Then
        '    _NumeratorAsDouble *= NumeratorAsInteger
        '    If Math.Abs(DenominatorAsDouble - 0) < _TOLERANCE Then
        '        currentString = _INFINITY
        '    Else
        '        currentString = CStr(NumeratorAsDouble / DenominatorAsDouble)
        '    End If

        'ElseIf IsFractionFormat Then
        '    If DenominatorAsInteger = 0 Then
        '        currentString = _INFINITY
        '    Else
        '        currentString = NumeratorAsInteger & _DIVISOR & DenominatorAsInteger
        '    End If
        'Else
        '    currentString = CStr(NumeratorAsInteger)
        'End If

        _Power = currentString

        Return currentString
    End Function
#End Region

End Class
