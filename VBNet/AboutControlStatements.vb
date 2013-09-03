Imports Xunit

Public Class AboutControlStatements
    Inherits Koan
    <Koan(1)> _
    Public Sub SingleLineIfThenStatements()
        Dim b As Boolean = False
        If True Then b = True
        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(2)> _
    Public Sub MultiLineIfStatements()
        Dim b As Boolean = False
        If True Then
            b = True
        End If
        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(3)> _
    Public Sub SingleLineIfThenElse()
        Dim b As Boolean
        If True Then b = True Else b = False
        Assert.Equal(FILL_ME_IN, b)
    End Sub

    <Koan(4)> _
    Public Sub MultiLineIfThenElseStatement()
        Dim b As Boolean
        If True Then
            b = True
        Else
            b = False
        End If
        Assert.Equal(FILL_ME_IN, b)
    End Sub


    <Koan(5)> _
    Public Sub TernaryOperators()
        Assert.Equal(FILL_ME_IN, (If(True, 1, 0)))
        Assert.Equal(FILL_ME_IN, (If(False, 1, 0)))
    End Sub

    'This is out of place for control statements, but necessary for Koan 8
    <Koan(6)> _
    Public Sub NullableTypes()
        Dim i As Integer = 0
        'i = null; //You can't do this
        Dim nullableInt As Integer? = Nothing 'but you can do this
        Assert.NotNull(FILL_ME_IN)
        Assert.Null(FILL_ME_IN)
    End Sub

    <Koan(7)> _
    Public Sub AlternateNullableTypesSyntax()
        Dim i As Integer = 0
        Dim nullableInt As Nullable(Of Integer) = Nothing 'but you can do this
        Assert.NotNull(FILL_ME_IN)
        Assert.Null(FILL_ME_IN)
    End Sub

    <Koan(8)> _
    Public Sub AssignIfNullOperator()
        Dim nullableInt As Integer? = Nothing
        Dim x As Integer = If(nullableInt, 42)
        Assert.Equal(FILL_ME_IN, x)
    End Sub

    <Koan(9)> _
    Public Sub IsOperators()
        Dim isKoan As Boolean = False
        Dim isAboutControlStatements As Boolean = False
        Dim isAboutMethods As Boolean = False
        Dim myType = Me
        If TypeOf myType Is Koan Then
            isKoan = True
        End If
        If TypeOf myType Is AboutControlStatements Then
            isAboutControlStatements = True
        End If
        ' The following is not possible. 
        ' In C# it would generate a warning.
        ' In VB.Net it is an Error
        '
        'If TypeOf myType Is AboutMethods Then
        '    isAboutMethods = True
        'End If
        Assert.Equal(FILL_ME_IN, isKoan)
        Assert.Equal(FILL_ME_IN, isAboutControlStatements)
        Assert.Equal(FILL_ME_IN, isAboutMethods)
    End Sub

    <Koan(10)> _
    Public Sub WhileStatement()
        Dim i As Integer = 1
        Dim result As Integer = 1
        While i <= 3
            result = result + i
            i += 1
        End While
        Assert.Equal(FILL_ME_IN, result)
    End Sub

    <Koan(11)> _
    Public Sub BreakStatement()
        Dim i As Integer = 1
        Dim result As Integer = 1
        While True
            If i > 3 Then
                Exit While
            End If
            result = result + i
            i += 1
        End While
        Assert.Equal(FILL_ME_IN, result)
    End Sub

    <Koan(12)> _
    Public Sub ContinueStatement()
        Dim i As Integer = 0
        Dim result = New List(Of Integer)()
        While i < 10
            i += 1
            If (i Mod 2) = 0 Then
                Continue While
            End If
            result.Add(i)
        End While
        Assert.Equal(FILL_ME_IN, result)
    End Sub

    <Koan(13)> _
    Public Sub ForStatement()
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Dim i As Integer = 0
        While i < List.Count
            List(i) = (List(i).ToUpper())
            i += 1
        End While
        Assert.Equal(FILL_ME_IN, List)
    End Sub

    <Koan(14)> _
    Public Sub ForEachStatement()
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Dim finalList = New List(Of String)()
        For Each item As String In List
            finalList.Add(item.ToUpper())
        Next
        Assert.Equal(FILL_ME_IN, List)
        Assert.Equal(FILL_ME_IN, finalList)
    End Sub

    <Koan(15)> _
    Public Sub ModifyingACollectionDuringForEach()
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Try
            For Each item As String In list
                list.Add(item.ToUpper())
            Next
        Catch ex As Exception
            Assert.Equal(GetType(FillMeIn), ex.GetType())
        End Try
    End Sub

    <Koan(16)> _
    Public Sub CatchingModificationExceptions()
        Dim whoCaughtTheException As String = "No one"
        Dim list = New List(Of String)({"fish", "and", "chips"})
        Try
            For Each item As String In list
                Try
                    list.Add(item.ToUpper())
                Catch
                    whoCaughtTheException = "When we tried to Add it"
                End Try
            Next
        Catch
            whoCaughtTheException = "When we tried to move to the next item in the list"
        End Try
        Assert.Equal(FILL_ME_IN, whoCaughtTheException)
    End Sub
End Class