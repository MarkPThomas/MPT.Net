Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports MPT.Enums.EnumLibrary

''' <summary>
''' Derived Unit of the Type 'Force', which is being used as if it were a base Unit.
''' Provides a list of Unit names allowed and performs Unit conversions.
''' </summary>
''' <remarks></remarks>
Public Class cUnitForce
#Region "Enumerations"
    ''' <summary>
    ''' List of the Unit names available for this Unit Type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> None
        <Description("lb")> PoundForce
        <Description("Kip")> Kip
        <Description("N")> Newton
        <Description("kN")> KiloNewton
        <Description("MN")> MegaNewton
        <Description("GN")> GigaNewton
        <Description("kgf")> KilogramForce
        <Description("tf")> TonForce
    End Enum
#End Region

#Region "Constants"
    Private Const _GRAVITY_IMP As Double = 32.174   'lbm*ft/s^2

    'Within Imperial
    Private Const _KIP_TO_POUNDFORCE As Integer = 1000

    'Within SI
    Private Const _KILO As Integer = 1000
    Private Const _MEGA As Integer = 1000000
    Private Const _GIGA As Integer = 1000000000

    'Metric MKS
    Private Const _TONFORCE_TO_KILOGRAMFORCE As Integer = 1000
#End Region

#Region "Properties: Private"
    ''' <summary>
    ''' Conversion factor from Kilogram-Force to pound-Force.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly _KILOGRAMFORCE_TO_POUNDFORCE As Double = cUnitMass.Convert(1, cUnitMass.eUnit.Kilogram, cUnitMass.eUnit.PoundMass)

    ''' <summary>
    ''' Conversion factor from newtons to pound-Force.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared ReadOnly _NEWTON_TO_POUNDFORCE As Double = (_KILOGRAMFORCE_TO_POUNDFORCE * cUnitLength.Convert(1, cUnitLength.eUnit.Meter, cUnitLength.eUnit.Foot)) / _GRAVITY_IMP
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
    Friend Shared ReadOnly Property UnitDefault As eUnit = eUnit.PoundForce

    ''' <summary>
    '''  List of Force units.
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
            Case eUnit.PoundForce 'Default value. No action needed.
            Case eUnit.Kip
                unitValue *= _KIP_TO_POUNDFORCE
            Case eUnit.Newton
                unitValue *= _NEWTON_TO_POUNDFORCE
            Case eUnit.KiloNewton
                unitValue *= _KILO * _NEWTON_TO_POUNDFORCE
            Case eUnit.MegaNewton
                unitValue *= _MEGA * _NEWTON_TO_POUNDFORCE
            Case eUnit.GigaNewton
                unitValue *= _GIGA * _NEWTON_TO_POUNDFORCE
            Case eUnit.KilogramForce
                unitValue *= _KILOGRAMFORCE_TO_POUNDFORCE
            Case eUnit.TonForce
                unitValue *= (_TONFORCE_TO_KILOGRAMFORCE * _KILOGRAMFORCE_TO_POUNDFORCE)
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
            Case eUnit.Kip
                unitValue /= _KIP_TO_POUNDFORCE
            Case eUnit.Newton
                unitValue /= _NEWTON_TO_POUNDFORCE
            Case eUnit.KiloNewton
                unitValue /= _KILO * _NEWTON_TO_POUNDFORCE
            Case eUnit.MegaNewton
                unitValue /= _MEGA * _NEWTON_TO_POUNDFORCE
            Case eUnit.GigaNewton
                unitValue /= _GIGA * _NEWTON_TO_POUNDFORCE
            Case eUnit.KilogramForce
                unitValue /= _KILOGRAMFORCE_TO_POUNDFORCE
            Case eUnit.TonForce
                unitValue /= (_TONFORCE_TO_KILOGRAMFORCE * _KILOGRAMFORCE_TO_POUNDFORCE)
            Case Else : Return 0
        End Select

        Return unitValue
    End Function
#End Region
End Class
