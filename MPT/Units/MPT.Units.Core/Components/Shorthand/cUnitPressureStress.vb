Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports MPT.Enums.EnumLibrary

''' <summary>
''' A schema type that has various shorthand labels that correspond with particular Unit names filled into the schema.
''' Provides a list of shorthand labels allowed and returns a units object with a schema and Unit names appropriate to a chosen shorthand label.
''' </summary>
''' <remarks></remarks>
Public Class cUnitPressureStress
#Region "Enumerations"
    ''' <summary>
    ''' List of the Unit shorthand names available for this schema Type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> None
        <Description("psi")> PoundPerSquareInch
        <Description("psf")> PoundPerSquareFoot
        <Description("ksi")> KipPerSquareInch
        <Description("ksf")> KipPerSquareFoot
        <Description("Pa")> Pascal
        <Description("kPa")> KiloPascal
        <Description("MPa")> MegaPascal
        <Description("GPa")> GigaPascal
    End Enum
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
    Friend Shared ReadOnly Property UnitDefault As eUnit = eUnit.PoundPerSquareInch

    ''' <summary>
    '''  List of pressure units.
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
    ''' Sets the shorthand label back to the default Type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetToDefault()
        Unit = _unitDefault
    End Sub
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Returns the units object derived from the provided shorthand units enumeration.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Function GetUnits() As cUnits
        Return GetUnits(Unit)
    End Function

    ''' <summary>
    ''' Returns the units object derived from the provided shorthand units Name.
    ''' If the Name does not match a valid shorthand Unit, Nothing is returned.
    ''' </summary>
    ''' <param name="shorthandUnitName">Name of the shorthand Unit to use for the units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function GetUnits(ByVal shorthandUnitName As String) As cUnits
        Dim currentUnit = GetUnitsEnum(shorthandUnitName)

        If Not currentUnit = eUnit.None Then
            Return GetUnits(currentUnit)
        Else
            Return GetUnits(UnitDefault)
        End If
    End Function

    ''' <summary>
    ''' Returns the units object derived from the provided shorthand units enumeration.
    ''' </summary>
    ''' <param name="shorthandUnitName">Shorthand Unit to use in generating the units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Overloads Shared Function GetUnits(ByVal shorthandUnitName As eUnit) As cUnits
        Dim unitForce As New cUnit(eUnitType.Force)
        Dim unitLength As New cUnit(eUnitType.Length, "2", unitIsNumerator:=False)

        Select Case shorthandUnitName
            Case eUnit.None
                'No action
            Case eUnit.PoundPerSquareInch
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.PoundForce))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Inch))
            Case eUnit.PoundPerSquareFoot
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.PoundForce))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Foot))
            Case eUnit.KipPerSquareInch
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.Kip))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Inch))
            Case eUnit.KipPerSquareFoot
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.Kip))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Foot))
            Case eUnit.Pascal
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.Newton))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Meter))
            Case eUnit.KiloPascal
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.KiloNewton))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Meter))
            Case eUnit.MegaPascal
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.MegaNewton))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Meter))
            Case eUnit.GigaPascal
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.GigaNewton))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Meter))
        End Select

        Dim unitCollection As New List(Of cUnit)
        With unitCollection
            .Add(unitForce)
            .Add(unitLength)
        End With

        Return assembleUnits(shorthandUnitName, unitCollection)
    End Function

    ''' <summary>
    ''' Gets the specific shorthand Unit enumeration by the provided string Name.
    ''' </summary>
    ''' <param name="shorthandUnitName">Name of the shorthand Unit to use.</param>
    ''' <remarks></remarks>
    Friend Shared Function GetUnitsEnum(ByVal shorthandUnitName As String) As eUnit
        If shorthandUnitName Is Nothing Then Return eUnit.None
        Return ConvertStringToEnumByDescription(Of eUnit)(shorthandUnitName)
    End Function

    ''' <summary>
    ''' Sets the specific shorthand Unit by the provided string Name.
    ''' </summary>
    ''' <param name="shorthandUnitName">Name of the shorthand Unit to use.</param>
    ''' <remarks></remarks>
    Friend Sub SetUnitByName(ByVal shorthandUnitName As String)
        Unit = ConvertStringToEnumByDescription(Of eUnit)(shorthandUnitName)
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Assembles the provided list of Unit objects into a single units object, and records the correspdonding shorthand label with the units object.
    ''' </summary>
    ''' <param name="shorthandUnit">Shorthand Unit that the operation is based on.</param>
    ''' <param name="componentUnits">List of Unit objects to assemble into one units object.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function assembleUnits(ByVal shorthandUnit As eUnit,
                                          ByVal componentUnits As IEnumerable(Of cUnit)) As cUnits
        Dim units As New cUnits
        With units
            If Not shorthandUnit = eUnit.None Then .shorthandLabel = GetEnumDescription(shorthandUnit)

            For Each componentUnit As cUnit In componentUnits
                .AddUnit(componentUnit)
            Next
        End With

        Return units
    End Function
#End Region

End Class
