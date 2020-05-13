Option Strict On
Option Explicit On

Imports MPT.SymbolicMath

''' <summary>
''' Basic generic symbolic element that may compose a set of units, a symbolic equation, etc.
''' Such elements are assumed to be defined by either opening/closing parentheses, and/or multipliers, and/or divisors.
''' </summary>
''' <remarks></remarks>
Public Class cSymbolicBlock
#Region "Properties: Friend"
    Private ReadOnly _block As IBase
    
    ''' <summary>
    ''' String composing the base Type of the block object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property BlockName As String
        Get
            If _block Is Nothing Then Return String.Empty
           Return _block.BaseLabel()
        End Get
    End Property

    ''' <summary>
    ''' String composing the superscript associated with the block object.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property BlockSuperscript As String
        Get
            If _block Is Nothing Then Return String.Empty
            Return _block.PowerLabel()
        End Get
    End Property

    ''' <summary>
    ''' True: The block object is to be considered in the Numerator position. 
    ''' False: The block object is to be considered in the denominator position. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property IsNumerator As Boolean

#End Region
    
    Friend Sub New(ByVal value As ProductQuotientSet)
        _block = value
    End Sub

    Friend Sub New(ByVal value As PrimitiveUnit)
        _block = value
    End Sub
End Class
