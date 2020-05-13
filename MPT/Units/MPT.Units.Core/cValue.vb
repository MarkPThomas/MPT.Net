Option Strict On
Option Explicit On

''' <summary>
''' Value object that contains a magnitude and associated units. 
''' This value can convert the value and units to a new set of units.
''' </summary>
''' <remarks></remarks>
Public Class cValue
#Region "Properties: Public"
    Private _magnitude As String
    ''' <summary>
    ''' Magnitude of the value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Magnitude As String
        Get
            Return _magnitude
        End Get
    End Property


    Private ReadOnly _units As New cUnitsController
    ''' <summary>
    ''' Units of the value.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Units As String
        Get
            Dim unitLabel As String = _units.units.shorthandLabel
            If String.IsNullOrEmpty(unitLabel) Then unitLabel = _units.units.GetUnitsLabel

            Return unitLabel
        End Get
    End Property
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Create new value.
    ''' </summary>
    ''' <param name="newMagnitude">Magnitude of the value.</param>
    ''' <param name="newUnits">Units of the value (e.g. kN*m/sec).</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal newMagnitude As Double,
                   ByVal newUnits As String)
        setMagnitude(newMagnitude.ToString())
        If unitsCanBeParsed(Magnitude, newUnits) Then _units.ParseStringToUnits(newUnits)
    End Sub
    
    ''' <summary>
    ''' Create new value.
    ''' </summary>
    ''' <param name="newMagnitude">Magnitude of the value. 
    ''' Must be numeric if Unit features are to be used.</param>
    ''' <param name="newUnits">Units of the value (e.g. kN*m/sec).</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal newMagnitude As String,
                   ByVal newUnits As String)
        setMagnitude(newMagnitude)
        If unitsCanBeParsed(Magnitude, newUnits) Then _units.ParseStringToUnits(newUnits)
    End Sub


    Public Sub New(ByVal magnitudeOrUnits As String)
        If cUnitsController.IsConsistent(magnitudeOrUnits) Then Return
        If unitsCanBeParsed(Magnitude, magnitudeOrUnits) Then _units.ParseStringToUnits(magnitudeOrUnits)

        '' TODO: Validate that units are of valid types. If not, set them blank and make value the magnitude
        If String.IsNullOrEmpty(Units) Then setMagnitude(magnitudeOrUnits)
    End Sub


    Public Sub New(ByVal newMagnitude As Double)
        setMagnitude(newMagnitude.ToString())
    End Sub
#End Region

#Region "Methods: Public"
    ''' <summary>
    ''' Converts the value according to the provided set of units.
    ''' </summary>
    ''' <param name="unitsToConvertTo">Units to convert the value to. These can be provided in two formats: 
    ''' 1. A new set of units. Must be in the same schema as the existing units (e.g. kN*m/sec).
    ''' 2. A list of units, if the value is to be converted to a consistent set of units for a given Unit Type (e.g. N, mm, hr).</param>
    ''' <remarks></remarks>
    Public Sub ConvertTo(ByVal unitsToConvertTo As String)
        If (Not IsNumeric(_magnitude) OrElse
            String.IsNullOrEmpty(Units)) Then Exit Sub

        ' Convert the value
        Dim conversionValue As Double = _units.ConvertTo(unitsToConvertTo)
        Dim valueNewNumeric As Double = CDbl(_magnitude) * conversionValue
        _magnitude = CStr(valueNewNumeric)

        ' Update the units
        If cUnitsController.IsConsistent(unitsToConvertTo) Then
            _units.MakeUnitsConsistent(unitsToConvertTo)
        Else
            _units.ParseStringToUnits(unitsToConvertTo)
        End If
    End Sub


    Public Sub ConvertFrom(ByVal magnitudeOfUnitsToConvertFrom As String,
                           ByVal unitsToConvertFrom As String)
        If (Not IsNumeric(_magnitude) OrElse
            Not IsNumeric(magnitudeOfUnitsToConvertFrom) OrElse
            String.IsNullOrEmpty(Units)) Then Exit Sub

        ' Convert the value
        Dim conversionValue As Double = _units.ConvertFrom(unitsToConvertFrom)
        Dim valueNewNumeric As Double = CDbl(magnitudeOfUnitsToConvertFrom) * conversionValue
        _magnitude = CStr(valueNewNumeric)
    End Sub
#End Region

#Region "Methods: Private"  
    ''' <summary>
    ''' Units are only set if the magnitude is numeric and units are not a list.
    ''' </summary>
    ''' <param name="newUnits"></param>
    ''' <returns></returns>
    Private Shared Function unitsCanBeParsed(ByVal newMagnitude As String, 
                                             ByVal newUnits As String) As Boolean
        Return ((String.IsNullOrEmpty(newMagnitude) OrElse IsNumeric(newMagnitude)) AndAlso
                (Not IsNumeric(newUnits) AndAlso Not cUnitsController.IsConsistent(newUnits)))
    End Function

    
    Private Sub setMagnitude(ByVal newMagnitude As String)
        If String.IsNullOrEmpty(newMagnitude) Then
            _magnitude = "0"
        Else
            _magnitude = newMagnitude
        End If
    End Sub
#End Region
End Class
