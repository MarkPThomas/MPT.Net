Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary

''' <summary>
''' Base Unit of the Type 'Length'. 
''' Provides a list of Unit names allowed and performs Unit conversions.
''' </summary>
''' <remarks></remarks>
Public Class cUnitLength
#Region "Enumerations"
    ''' <summary>
    ''' List of the Unit names available for this Unit Type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shadows Enum eUnit
        <Description("")> None
        <Description("in")> Inch
        <Description("ft")> Foot
        <Description("yd")> Yard
        <Description("Mile")> Mile
        <Description("Micron")> Micron
        <Description("mm")> Millimeter
        <Description("cm")> Centimeter
        <Description("m")> Meter
        <Description("km")> Kilometer
    End Enum
#End Region

#Region "Constants"
    Private Const _MILLIMETER_TO_INCH As Double = 1 / 25.4

    'Within Imperial
    Private Const _FOOT_TO_INCH As Integer = 12
    Private Const _YARD_TO_FOOT As Integer = 3
    Private Const _MILE_TO_FOOT As Integer = 5280

    'Within SI
    Private Const _MICRON_TO_MILLIMETER As Double = 0.001
    Private Const _CENTIMETER_TO_MILLIMETER As Integer = 10
    Private Const _METER_TO_MILLIMETER As Integer = 1000
    Private Const _KILOMETER_TO_MILLIMETER As Integer = 1000000
#End Region

#Region "Properties: Friend"
    ''' <summary>
    ''' Specified Unit.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property Unit As eUnit

    ''' <summary>
    ''' Default Unit set for this Unit Type.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property UnitDefault As eUnit = eUnit.Inch

    ''' <summary>
    '''  List of Length units.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared ReadOnly Property UnitsList As List(Of String)
        Get
            Dim newUnitsList As New List(Of String)
            newUnitsList.InsertRange(0, GetEnumDescriptionList(eUnit.None))
            Return newUnitsList
        End Get
    End Property
#End Region

#Region "Initialization"
    Friend Sub New()
        SetToDefault()
    End Sub

    ''' <summary>
    ''' Sets the Unit Type back to the default Unit Type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetToDefault()
        Unit = _unitDefault
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Converts the unit name to the corresponding enum, if it exists. Otherwise, returns None.
    ''' </summary>
    ''' <param name="unitName">Name of the unit.</param>
    ''' <returns>eUnit.</returns>
    Friend Shared Function ToEnum(ByVal unitName As String) As eUnit
        Return ConvertStringToEnumByDescription(Of eUnit)(unitName)
    End Function

    ''' <summary>
    ''' Converts the specified from unit value.
    ''' </summary>
    ''' <param name="fromUnitValue">From unit value.</param>
    ''' <param name="fromUnitType">Type of unit to convert from.</param>
    ''' <param name="toUnitType">Type of unit to convert to. Default is the unit default type.</param>
    ''' <returns>System.Double.</returns>
    Friend Shared Function Convert(ByVal fromUnitValue As Double,
                                   ByVal fromUnitType As String,
                                   Optional ByVal toUnitType As String = "") As Double
        Return Convert(fromUnitValue, ToEnum(fromUnitType), ToEnum(toUnitType))
    End Function

    ''' <summary>
    ''' Converts the specified from unit value.
    ''' </summary>
    ''' <param name="fromUnitValue">From unit value.</param>
    ''' <param name="fromUnitType">Type of unit to convert from.</param>
    ''' <param name="toUnitType">Type of unit to convert to. Default is the unit default type.</param>
    ''' <returns>System.Double.</returns>
    Friend Shared Function Convert(ByVal fromUnitValue As Double,
                           ByVal fromUnitType As eUnit,
                           Optional ByVal toUnitType As eUnit = eUnit.None) As Double
        If (Not toUnitType = eUnit.None AndAlso fromUnitType = toUnitType) Then Return fromUnitValue

        'Convert to default Unit
        Dim value As Double = ConvertToBase(fromUnitValue, fromUnitType)

        'If necessary, convert to other Unit
        If (Not toUnitType = eUnit.None AndAlso Not toUnitType = UnitDefault) Then
            value = ConvertFromBase(value, toUnitType)
        End If

        Return value
    End Function
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Converts the provided Unit to the default Unit Type and returns the value for the conversion, which is multiplied by the supplied value.
    ''' </summary>
    ''' <param name="unitValue">Conversion value. Typically starts as '1'.</param>
    ''' <param name="unitSourceType">Unit to convert to the default Unit Type.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConvertToBase(ByVal unitValue As Double,
                                    ByVal unitSourceType As eUnit) As Double
        Select Case unitSourceType
            Case eUnit.Inch 'Default value. No action needed.
            Case eUnit.Foot
                unitValue *= _FOOT_TO_INCH
            Case eUnit.Yard
                unitValue *= (_YARD_TO_FOOT * _FOOT_TO_INCH)
            Case eUnit.Mile
                unitValue *= (_MILE_TO_FOOT * _FOOT_TO_INCH)
            Case eUnit.Micron
                unitValue *= (_MICRON_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case eUnit.Millimeter
                unitValue *= _MILLIMETER_TO_INCH
            Case eUnit.Centimeter
                unitValue *= (_CENTIMETER_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case eUnit.Meter
                unitValue *= (_METER_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case eUnit.Kilometer
                unitValue *= (_KILOMETER_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case Else : Return 0
        End Select

        Return unitValue
    End Function

    ''' <summary>
    ''' Converts the provided Unit from the default Unit Type to the specified Unit Type and returns the value for the conversion, which supplied value is divided by.
    ''' </summary>
    ''' <param name="unitValue">Conversion value. Typically starts as "1" if it is not passed along from a prior conversion method.</param>
    ''' <param name="unitTargetType">Unit object to which the conversion is to take place.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ConvertFromBase(ByVal unitValue As Double,
                                      ByVal unitTargetType As eUnit) As Double

        Select Case unitTargetType
            Case eUnit.foot
                unitValue /= _FOOT_TO_INCH
            Case eUnit.yard
                unitValue /= (_YARD_TO_FOOT * _FOOT_TO_INCH)
            Case eUnit.mile
                unitValue /= (_MILE_TO_FOOT * _FOOT_TO_INCH)
            Case eUnit.micron
                unitValue /= (_MICRON_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case eUnit.millimeter
                unitValue /= _MILLIMETER_TO_INCH
            Case eUnit.centimeter
                unitValue /= (_CENTIMETER_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case eUnit.meter
                unitValue /= (_METER_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case eUnit.kilometer
                unitValue /= (_KILOMETER_TO_MILLIMETER * _MILLIMETER_TO_INCH)
            Case Else : Return 0
        End Select

        Return unitValue
    End Function
#End Region
End Class
