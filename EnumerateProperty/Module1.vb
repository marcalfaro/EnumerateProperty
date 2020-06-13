Option Explicit On

Public Class Person
    Public Property firstName As String
    Public Property lastName As String
    Public Property residence As Address
End Class

Public Class Address
    Public Property street As String
    Public Property postalCode As String
End Class

Module Module1


    Sub Main()

        'Instantiate new class
        Dim student As New Person
        With student
            .firstName = "John"
            .lastName = "Smith"
            .residence = New Address With {.street = "123 New Street", .postalCode = "9999"}
        End With

        'You can also drill down nested property values...
        Console.WriteLine("Student Information:")
        Console.WriteLine($"Name: {GetPropValue(student, "lastName")}, {GetPropValue(student, "firstName")}")
        Console.WriteLine($"Street: {GetPropValue(student, "residence.street")} {GetPropValue(student, "residence.postalCode")}")


        Console.ReadLine()

    End Sub

    Function GetPropValue(ByVal obj As Object, ByVal propName As String) As Object
        If obj Is Nothing Then Return Nothing
        If String.IsNullOrWhiteSpace(propName) Then Return Nothing

        Dim nameParts As String() = propName.Split("."c)
        If nameParts.Length = 1 Then Return obj.[GetType]().GetProperty(propName).GetValue(obj, Nothing)

        For Each part As String In nameParts

            If obj Is Nothing Then Return Nothing

            Dim type As Type = obj.[GetType]()
            Dim info As Reflection.PropertyInfo = type.GetProperty(part)

            If info Is Nothing Then Return Nothing

            obj = info.GetValue(obj, Nothing)
        Next

        Return obj
    End Function

End Module
