Option Strict On
Option Explicit On

Imports System.ComponentModel

Imports MPT.Enums.EnumLibrary

''' <summary>
''' Base Unit of the type 'Time'.
''' Provides a list of Unit names allowed and performs Unit conversions.
''' </summary>
''' <remarks></remarks>
Friend Class cUnitTime
#Region "Enumerations"
    ''' <summary>
    ''' List of the Unit names available for this Unit type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> None

        <Description("sec")> Second

        <Description("min")> Minute

        <Description("hr")> Hour

        ''' <summary>
        ''' Day is taken as 24 hours.
        ''' </summary>
        <Description("Day")> Day

        ''' <summary>
        ''' Week is taken as 7 days of 24 hours.
        ''' </summary>
        <Description("Week")> Week

        ''' <summary>
        ''' The Month is a Unit of Time, used with calendars, which is approximately as long as some natural period related to the motion of the Moon (i.e. "Moonth"). 
        ''' The mean Month Length of the Gregorian calendar is 30.436875 days.
        ''' </summary>
        <Description("Month")> Month

        ''' <summary>
        ''' Gregorian calendar Year for one revolution of the earth around the sun.
        ''' </summary>
        <Description("Year")> Year
    End Enum
#End Region

#Region "Constants"
    Private Const _MINUTE_TO_SEC As Integer = 60
    Private Const _HOUR_TO_SEC As Integer = 3600
    ''' <summary>
    ''' Day is taken as 24 hours.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _DAY_TO_SEC As Integer = 86400
    ''' <summary>
    ''' Week is taken as 7 days of 24 hours.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _WEEK_TO_SEC As Integer = 604800
    ''' <summary>
    ''' The Month is a Unit of Time, used with calendars, which is approximately as long as some natural period related to the motion of the Moon (i.e. "Moonth"). 
    ''' The mean Month Length of the Gregorian calendar is 30.436875 days.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _MONTH_TO_SEC As Double = 2629746
    ''' <summary>
    ''' Gregorian calendar Year for one revolution of the earth around the sun.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const _YEAR_TO_SEC As Double = 31556952
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
    Friend Shared ReadOnly Property UnitDefault As eUnit = eUnit.Second

    ''' <summary>
    '''  List of Time units.
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
        Unit = eUnit.Second
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
            Case eUnit.Second 'Default value. No action needed.
            Case eUnit.Minute
                unitValue *= _MINUTE_TO_SEC
            Case eUnit.Hour
                unitValue *= _HOUR_TO_SEC
            Case eUnit.Day
                unitValue *= _DAY_TO_SEC
            Case eUnit.Week
                unitValue *= _WEEK_TO_SEC
            Case eUnit.Month
                unitValue *= _MONTH_TO_SEC
            Case eUnit.Year
                unitValue *= _YEAR_TO_SEC
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
            Case eUnit.minute
                unitValue /= _MINUTE_TO_SEC
            Case eUnit.hour
                unitValue /= _HOUR_TO_SEC
            Case eUnit.day
                unitValue /= _DAY_TO_SEC
            Case eUnit.week
                unitValue /= _WEEK_TO_SEC
            Case eUnit.month
                unitValue /= _MONTH_TO_SEC
            Case eUnit.year
                unitValue /= _YEAR_TO_SEC
            Case Else : Return 0
        End Select

        Return unitValue
    End Function
#End Region
End Class
