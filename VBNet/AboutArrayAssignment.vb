Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Xunit
Imports System.Linq

Public Class AboutArrayAssignment
    Inherits Koan
    'Parallel Assignments are a feature in Ruby which allow one
    'to set multiple variables at once using an array syntax. e.g.
    'first_name, last_name = ["John", "Smith"]
    'which would set first_name == "John" and last_name == "Smith"
    'This isn't available in VB.Net, but there are a few interesting assignment
    'tricks we can pick up.
    <Koan(1)> _
    Public Sub ImplicitAssignment()
        'Even though we don't specify types explicitly, the compiler
        'will pick one for us
        Dim name = "John"
        Assert.Equal(GetType(FillMeIn), name.GetType())
        'but only if it can. So this doesn't work
        'var array = nothing;
        'It also knows the type, so once the above is in place, this doesn't work:
        'name = 42;
    End Sub

    <Koan(2)> _
    Public Sub ImplicitArrayAssignmentWithSameTypes()
        'Even though we don't specify types explicitly, the compiler
        'will pick one for us
        Dim names = New String() {"John", "Smith"}
        Assert.Equal(GetType(FillMeIn), names.GetType())
        'but only if it can. So this doesn't work
        'var array = new[] { "John", 1 };
    End Sub

    <Koan(3)> _
    Public Sub MultipleAssignmentsOnSingleLine()
        'You can do multiple assignments on one line, but you 
        'still have to be explicit
        Dim firstName As String = "John", lastName As String = "Smith"
        Assert.Equal(FILL_ME_IN, firstName)
        Assert.Equal(FILL_ME_IN, lastName)
    End Sub
End Class