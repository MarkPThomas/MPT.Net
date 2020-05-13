Option Strict On
Option Explicit On

Friend Class cSymbolicFraction
    Private Const NEGATIVE As Char = "-"c
    Private Const DIVISOR As Char = "/"c
    Private Const ADD As Char = "+"c
    Private Const SUBTRACT As Char = "-"c
    Private Const MULTIPLY As Char = "*"c
    Private Const DIVIDE As Char = "/"c
    Private Const OPEN_GROUP As Char = "("c
    Private Const CLOSE_GROUP As Char = ")"c
    Private Const INFINITY As String = "Infinity"

    Private _numerator As cSymbolicValue = New cSymbolicValue(1)
    Friend ReadOnly Property Numerator As String
        Get
            Return _numerator.AsString()
        End Get
    End Property

    Private _denominator As cSymbolicValue = New cSymbolicValue(1)
    Friend ReadOnly Property Denominator As String
        Get
            Return _denominator.AsString()
        End Get
    End Property

    Friend ReadOnly Property Value As String
        Get
            Dim packagedNumerator As String = packageValue(_numerator)
            Dim packagedDenominator As String = packageValue(_denominator)

            If (packagedDenominator = "1") Then
                Return signCharacter() & packagedNumerator
            Else
                Return signCharacter() & packagedNumerator & DIVISOR & packagedDenominator
            End If
        End Get
    End Property


    Friend Sub New(ByVal numerator As cSymbolicValue, Optional ByVal denominator As cSymbolicValue = Nothing)
        Initialize(numerator, denominator)
    End Sub

    Friend Sub New(ByVal numerator As String, Optional ByVal denominator As String = "")
        Dim symbolicNumerator As cSymbolicValue = Nothing
        If (Not String.IsNullOrEmpty(numerator)) Then
            symbolicNumerator = New cSymbolicValue(numerator)
        End If

        Dim symbolicDenominator As cSymbolicValue = Nothing
        If (Not String.IsNullOrEmpty(denominator)) Then
            symbolicDenominator = New cSymbolicValue(denominator)
        End If

        Initialize(symbolicNumerator, symbolicDenominator)
    End Sub

    Private Sub Initialize(ByVal symbolicNumerator As cSymbolicValue, Optional ByVal symbolicDenominator As cSymbolicValue = Nothing)
        '' Handle empty values
        If (symbolicNumerator Is Nothing OrElse
            String.IsNullOrEmpty(symbolicNumerator.AsString())) Then
            Return
        End If

        If (symbolicDenominator Is Nothing OrElse
            String.IsNullOrEmpty(symbolicDenominator.AsString())) Then
            symbolicDenominator = New cSymbolicValue(1)
        End If

        '' Handle sign
        If (isNegative(symbolicNumerator)) Then
            _sign *= -1
            _numerator = New cSymbolicValue(symbolicNumerator.AsString().Substring(1))
        Else
            _numerator = New cSymbolicValue(symbolicNumerator.AsString())
        End If

        If (isNegative(symbolicDenominator)) Then
            _sign *= -1
            _denominator = New cSymbolicValue(symbolicDenominator.AsString().Substring(1))
        Else
            _denominator = New cSymbolicValue(symbolicDenominator.AsString())
        End If
    End Sub

    Public Function ConsolidateAsString() As String
        Return consolidate(Me).ToString()
    End Function

    Public Function ConsolidateAsInteger() As Integer
        Return consolidate(Me).AsInteger()
    End Function

    Public Function ConsolidateAsFloat() As Double
        Return consolidate(Me).AsFloat()
    End Function


    Protected Shared Function consolidate(ByVal value1 As cSymbolicFraction) As cSymbolicValue
        Return (value1.numeratorWithSign / value1._denominator)
    End Function



    Private _sign As Integer = 1

    Private Function isNegative() As Boolean
        Return _sign = -1
    End Function
    Private Function isNegative(ByVal symbolicValue As cSymbolicValue) As Boolean
        Return (symbolicValue.AsString()(0) = NEGATIVE AndAlso symbolicValue.AsString().Count(Function(c As Char) c = NEGATIVE) = 1)
    End Function

    Private Function signCharacter() As String
        If (isNegative()) Then
            Return NEGATIVE
        Else
            Return String.Empty
        End If
    End Function

    Private Function numeratorWithSign() As cSymbolicValue
        Return New cSymbolicValue(signCharacter() & _numerator.AsString())
    End Function

    Private Shared Function hasSameDenominator(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        Return (String.CompareOrdinal(value1.Denominator, value2.Denominator) = 0)
    End Function

    Private Function packageValue(ByVal valueToPackage As cSymbolicValue) As String
        If (valueToPackage.AsString().Contains(ADD) OrElse
            (valueToPackage.AsString().Count(Function(c As Char) c = SUBTRACT) > 1) OrElse
            (valueToPackage.AsString().Count(Function(c As Char) c = NEGATIVE) = 1 AndAlso Not isNegative(valueToPackage)) OrElse
            valueToPackage.AsString().Contains(MULTIPLY) OrElse
            valueToPackage.AsString().Contains(DIVIDE)) Then

            Return OPEN_GROUP & valueToPackage.AsString() & CLOSE_GROUP
        Else
            Return valueToPackage.AsString()
        End If
    End Function


    Public Overrides Function ToString() As String
        Return MyBase.ToString() & " (" & Value & ")"
    End Function

    '' Add
    Public Shared Operator +(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As cSymbolicFraction
        If (value1 Is Nothing OrElse value2 Is Nothing) Then
            Throw New ArgumentNullException($"Operand was null.")
        End If

        If (hasSameDenominator(value1, value2)) Then
            Return New cSymbolicFraction(value1.numeratorWithSign + value2.numeratorWithSign,
                                         value1._denominator)
        Else
            Return New cSymbolicFraction(value1.numeratorWithSign * value2._denominator + value2.numeratorWithSign * value1._denominator,
                                         value1._denominator * value2._denominator)
        End If
    End Operator

    '' Subtract
    Public Shared Operator -(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As cSymbolicFraction
        If (value1 Is Nothing OrElse value2 Is Nothing) Then
            Throw New ArgumentNullException($"Operand was null.")
        End If

        If (hasSameDenominator(value1, value2)) Then
            Return New cSymbolicFraction(value1.numeratorWithSign - value2.numeratorWithSign,
                                         value1._denominator)
        Else
            Return New cSymbolicFraction(value1.numeratorWithSign * value2._denominator - value2.numeratorWithSign * value1._denominator,
                                         value1._denominator * value2._denominator)
        End If
    End Operator

    '' Multiply
    Public Shared Operator *(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As cSymbolicFraction
        If (value1 Is Nothing OrElse value2 Is Nothing) Then
            Throw New ArgumentNullException($"Operand was null.")
        End If

        Return New cSymbolicFraction(value1.numeratorWithSign * value2.numeratorWithSign, value1._denominator * value2._denominator)
    End Operator

    '' Divide
    Public Shared Operator /(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As cSymbolicFraction
        If (value1 Is Nothing OrElse value2 Is Nothing) Then
            Throw New ArgumentNullException($"Operand was null.")
        End If

        Return New cSymbolicFraction(value1.numeratorWithSign * value2._denominator, value1._denominator * value2.numeratorWithSign)
    End Operator

    '' Equals
    Public Shared Operator =(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        If (value1 Is Nothing AndAlso
            value2 Is Nothing) Then
            Return True
        ElseIf (value1 Is Nothing OrElse
                value2 Is Nothing) Then
            Return False
        End If

        Return value1.ConsolidateAsString() = value2.ConsolidateAsString()
    End Operator

    Public Shared Operator <>(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        If (value1 Is Nothing AndAlso
            value2 Is Nothing) Then
            Return False
        ElseIf (value1 Is Nothing OrElse
                value2 Is Nothing) Then
            Return True
        End If

        Return value1.ConsolidateAsString() <> value2.ConsolidateAsString()
    End Operator

    Public Shared Operator >(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        If (value1 Is Nothing AndAlso
            value2 Is Nothing) Then
            Return False
        ElseIf (value1 Is Nothing) Then
            Return False
        ElseIf (value2 Is Nothing) Then
            Return True
        End If

        Return value1.ConsolidateAsString() > value2.ConsolidateAsString()
    End Operator

    Public Shared Operator <(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        If (value1 Is Nothing AndAlso
            value2 Is Nothing) Then
            Return False
        ElseIf (value1 Is Nothing) Then
            Return True
        ElseIf (value2 Is Nothing) Then
            Return False
        End If

        Return value1.ConsolidateAsString() < value2.ConsolidateAsString()
    End Operator

    Public Shared Operator >=(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        If ((value1 Is Nothing AndAlso
             value2 Is Nothing) OrElse
            (value2 Is Nothing)) Then
            Return True
        ElseIf (value1 Is Nothing) Then
            Return False
        End If


        Return value1.ConsolidateAsString() >= value2.ConsolidateAsString()
    End Operator

    Public Shared Operator <=(ByVal value1 As cSymbolicFraction, ByVal value2 As cSymbolicFraction) As Boolean
        If ((value1 Is Nothing AndAlso
             value2 Is Nothing) OrElse
            (value1 Is Nothing)) Then
            Return True
        ElseIf (value2 Is Nothing) Then
            Return False
        End If


        Return value1.ConsolidateAsString() <= value2.ConsolidateAsString()
    End Operator
End Class
