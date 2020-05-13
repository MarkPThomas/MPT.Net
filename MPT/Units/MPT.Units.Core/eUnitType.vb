Option Strict On
Option Explicit On

Imports System.ComponentModel

''' <summary>
''' The basic Unit types from which all other units are derived.
''' </summary>
''' <remarks></remarks>
Public Enum eUnitType
    <Description("")> None
    ''' <summary>
    ''' In addition to being purely unitless, also can be assumed to have numerator/denominator that are the same, e.g. in/in.
    ''' </summary>
    <Description("Unitless")> Unitless
    <Description("Length")> Length
    <Description("Location")> Location
    <Description("Mass")> Mass
    <Description("Rotation")> Rotation
    <Description("Temperature")> Temperature
    <Description("Time")> Time
    ''' <summary>
    ''' Derived Unit, but commonly used instead of breaking units down into Mass*Length/Time^2.
    ''' </summary>
    ''' <remarks></remarks>
    <Description("Force")> Force
End Enum
