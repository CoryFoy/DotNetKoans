Imports Xunit

Public Class AboutNothing
    Inherits Koan
    <Koan(1)> _
    Public Sub NilIsNotAnObject()
        Assert.True(GetType(Object).IsAssignableFrom(Nothing)) 'not everything is an object
    End Sub

    <Koan(2)> _
    Public Sub YouGetNullPointerErrorsWhenCallingMethodsOnNil()
        'What is the Exception that is thrown when you call a method on a null object?
        'Don't be confused by the code below. It is using Anonymous Delegates which we will
        'cover later on. 
        Dim obj As Object = Nothing
        Assert.Throws(GetType(FillMeIn), Function() Nothing.ToString())
        'What's the message of the exception? What substring or pattern could you test
        'against in order to have a good idea of what the string is?
        Try
            obj.ToString()
        Catch ex As Exception
            Assert.Contains(IIf(TypeOf FILL_ME_IN Is String, CType(FILL_ME_IN, String), Nothing), ex.Message)
        End Try
    End Sub

    <Koan(3)> _
    Public Sub CheckingThatAnObjectIsNull()
        Dim obj As Object = Nothing
        Assert.True(obj = FILL_ME_IN)
    End Sub

    <Koan(4)> _
    Public Sub ABetterWayToCheckThatAnObjectIsNull()
        Dim obj As Object = Nothing
        Assert.Null(FILL_ME_IN)
    End Sub

    <Koan(5)> _
    Public Sub AWayNotToCheckThatAnObjectIsNull()
        Dim obj As Object = Nothing
        Assert.True(obj.Equals(Nothing))
    End Sub
End Class
