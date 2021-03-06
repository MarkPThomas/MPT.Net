﻿Option Explicit On
Option Strict On

Imports CSiTester.ModelControl

Imports CSiTester.cEnumerations

Imports CSiTester.cLibPath

Public Class ConverterClassificationLevel2
    ''' <summary>
    ''' Converts the value from the program to the file.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertToFile(ByVal p_value As String) As classificationValueLevel_2
        Return ConvertStringToEnumByXMLAttribute(Of classificationValueLevel_2)(p_value)
    End Function

    ''' <summary>
    ''' Converts the value from the file to the program.
    ''' </summary>
    ''' <param name="p_value"></param>
    ''' <param name="p_values">List of documentation statuses.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function ConvertFromFile(ByVal p_value As classificationValueLevel_2,
                                           ByVal p_values As IList(Of String)) As String
        Return GetListItemMatchingEnumByXMLAttribute(p_value, p_values)
    End Function
End Class
