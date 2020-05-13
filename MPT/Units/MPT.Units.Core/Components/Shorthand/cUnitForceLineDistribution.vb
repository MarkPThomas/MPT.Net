Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports MPT.Enums.EnumLibrary

''' <summary>
''' A schema Type that has various shorthand labels that correspond with particular Unit names filled into the schema.
''' Provides a list of shorthand labels allowed and returns a units object with a schema and Unit names appropriate to a chosen shorthand label.
''' </summary>
''' <remarks></remarks>
Public Class cUnitForceLineDistribution
#Region "Enumerations"
    ''' <summary>
    ''' List of the Unit shorthand names available for this schema Type.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum eUnit
        <Description("")> None
        <Description("plf")> PoundForcePerLinearFoot
        <Description("klf")> KipPerLinearFoot
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
    Friend Shared ReadOnly Property UnitDefault As eUnit = eUnit.PoundForcePerLinearFoot

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
        Dim unitLength As New cUnit(eUnitType.Length, unitIsNumerator:=False)

        Select Case shorthandUnitName
            Case eUnit.None
                ' Do nothing
            Case eUnit.PoundForcePerLinearFoot
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.PoundForce))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Foot))
            Case eUnit.KipPerLinearFoot
                unitForce.SetUnitName(GetEnumDescription(cUnitForce.eUnit.Kip))
                unitLength.SetUnitName(GetEnumDescription(cUnitLength.eUnit.Foot))
            Case Else
                ' Do nothing
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
        Unit = GetUnitsEnum(shorthandUnitName)
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
