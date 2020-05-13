Option Strict On
Option Explicit On

Friend Class cSymbolicValue

    Private ReadOnly _value As String = String.Empty

    Friend Shared Property Tolerance As Double = 0.0001

    Friend ReadOnly Property IsInteger As Boolean
    Friend ReadOnly Property IsFloat As Boolean
    Friend ReadOnly Property IsNumber As Boolean

    Friend Sub New(ByVal value As String)
        If Not String.IsNullOrWhiteSpace(value) Then
            _value = value
            IsNumber = IsNumeric(value)
            If IsNumber Then
                IsInteger = (CType(value, Double) Mod 1 = 0)
                IsFloat = Not IsInteger
            End If
        End If
    End Sub

    Friend Sub New(ByVal value As Double)
        _value = CType(value, String)
        IsFloat = True
        IsNumber = True
    End Sub

    Friend Sub New(ByVal value As Integer)
        _value = CType(value, String)
        IsInteger = True
        IsNumber = True
    End Sub


    Friend Function AsString() As String
        Return _value
    End Function

    Friend Function AsInteger() As Integer
        If (String.IsNullOrEmpty(_value) OrElse Not IsNumber) Then
            Return 0
        End If

        Dim value As Double = CType(_value, Double)
        Dim valueRounded As Integer = CType(Math.Round(value), Integer)
        Return valueRounded
    End Function

    Friend Function AsFloat() As Double
        If (String.IsNullOrEmpty(_value) OrElse Not IsNumber) Then
            Return 0
        End If

        Return CType(_value, Double)
    End Function

    Public Overrides Function ToString() As String
        If Not String.IsNullOrEmpty(_value) Then
            Return _value
        Else
            Return MyBase.ToString()
        End If
    End Function

    '' Add
    Public Shared Operator +(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As cSymbolicValue
        If (String.IsNullOrEmpty(value1.AsString())) Then
            Return value2
        ElseIf (String.IsNullOrEmpty(value2.AsString())) Then
            Return value1
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return New cSymbolicValue(value1.AsInteger() + value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return New cSymbolicValue(value1.AsFloat() + value2.AsFloat())
        Else
            Return New cSymbolicValue(value1.AsString() + "+" + value2.AsString())
        End If
    End Operator

    '' Subtract
    Public Shared Operator -(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As cSymbolicValue
        If (String.IsNullOrEmpty(value1.AsString()) OrElse
            String.IsNullOrEmpty(value2.AsString())) Then
            Return New cSymbolicValue(String.Empty)
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return New cSymbolicValue(value1.AsInteger() - value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return New cSymbolicValue(value1.AsFloat() - value2.AsFloat())
        Else
            Return New cSymbolicValue(value1.AsString() + "-" + value2.AsString())
        End If
    End Operator

    '' Multiply
    Public Shared Operator *(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As cSymbolicValue
        If (String.IsNullOrEmpty(value1.AsString()) OrElse
            String.IsNullOrEmpty(value2.AsString())) Then
            Return New cSymbolicValue(String.Empty)
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return New cSymbolicValue(value1.AsInteger() * value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return New cSymbolicValue(value1.AsFloat() * value2.AsFloat())
        Else
            Return New cSymbolicValue(value1.AsString() + "*" + value2.AsString())
        End If
    End Operator

    '' Divide
    Public Shared Operator /(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As cSymbolicValue
        If (String.IsNullOrEmpty(value1.AsString()) OrElse
            String.IsNullOrEmpty(value2.AsString())) Then
            Return New cSymbolicValue(String.Empty)
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            If value1.AsInteger() Mod value2.AsInteger() = 0 Then
                Return New cSymbolicValue(CType(Math.Round(value1.AsInteger() / value2.AsInteger()), Integer))
            Else
                Return New cSymbolicValue(value1.AsInteger() / value2.AsInteger())
            End If
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return New cSymbolicValue(value1.AsFloat() / value2.AsFloat())
        Else
            Return New cSymbolicValue(value1.AsString() + "/" + value2.AsString())
        End If
    End Operator

    '' Equals
    Public Shared Operator =(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As Boolean
        If (String.IsNullOrEmpty(value1.AsString()) AndAlso
            String.IsNullOrEmpty(value2.AsString())) Then
            Return True
        ElseIf (String.IsNullOrEmpty(value1.AsString()) OrElse
                String.IsNullOrEmpty(value2.AsString())) Then
            Return False
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return (value1.AsInteger() = value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return (Math.Abs(value1.AsFloat() - value2.AsFloat()) < cSymbolicValue.Tolerance)
        Else
            Return (String.CompareOrdinal(value1.AsString(), value2.AsString()) = 0)
        End If
    End Operator

    Public Shared Operator <>(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As Boolean
        If (String.IsNullOrEmpty(value1.AsString()) AndAlso
            String.IsNullOrEmpty(value2.AsString())) Then
            Return False
        ElseIf (String.IsNullOrEmpty(value1.AsString()) OrElse
                String.IsNullOrEmpty(value2.AsString())) Then
            Return True
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return Not (value1.AsInteger() = value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return Not (Math.Abs(value1.AsFloat() - value2.AsFloat()) < cSymbolicValue.Tolerance)
        Else
            Return Not (String.CompareOrdinal(value1.AsString(), value2.AsString()) = 0)
        End If
    End Operator

    Public Shared Operator >(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As Boolean
        If (String.IsNullOrEmpty(value1.AsString()) AndAlso
            String.IsNullOrEmpty(value2.AsString())) Then
            Return False
        ElseIf (String.IsNullOrEmpty(value2.AsString())) Then
            Return True
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return (value1.AsInteger() > value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return (value1.AsFloat() > value2.AsFloat())
        Else
            Return (String.CompareOrdinal(value1.AsString(), value2.AsString()) > 0)
        End If
    End Operator

    Public Shared Operator <(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As Boolean
        If (String.IsNullOrEmpty(value1.AsString()) AndAlso
            String.IsNullOrEmpty(value2.AsString())) Then
            Return False
        ElseIf (String.IsNullOrEmpty(value1.AsString())) Then
            Return True
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return (value1.AsInteger() < value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return (value1.AsFloat() < value2.AsFloat())
        Else
            Return (String.CompareOrdinal(value1.AsString(), value2.AsString()) < 0)
        End If
    End Operator

    Public Shared Operator >=(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As Boolean
        If ((String.IsNullOrEmpty(value1.AsString()) AndAlso
             String.IsNullOrEmpty(value2.AsString())) OrElse
            (String.IsNullOrEmpty(value2.AsString()))) Then
            Return True
        ElseIf (String.IsNullOrEmpty(value1.AsString())) Then
            Return False
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return (value1.AsInteger() >= value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return (value1.AsFloat() >= value2.AsFloat())
        Else
            Return (String.CompareOrdinal(value1.AsString(), value2.AsString()) >= 0)
        End If
    End Operator

    Public Shared Operator <=(ByVal value1 As cSymbolicValue, ByVal value2 As cSymbolicValue) As Boolean
        If ((String.IsNullOrEmpty(value1.AsString()) AndAlso
             String.IsNullOrEmpty(value2.AsString())) OrElse
            (String.IsNullOrEmpty(value1.AsString()))) Then
            Return True
        ElseIf (String.IsNullOrEmpty(value2.AsString())) Then
            Return False
        End If

        If (value1.IsInteger AndAlso value2.IsInteger) Then
            Return (value1.AsInteger() <= value2.AsInteger())
        ElseIf (value1.IsNumber AndAlso value2.IsNumber) Then
            Return (value1.AsFloat() <= value2.AsFloat())
        Else
            Return (String.CompareOrdinal(value1.AsString(), value2.AsString()) <= 0)
        End If
    End Operator
End Class
