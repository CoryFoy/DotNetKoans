Imports Xunit

Public Class AboutAsserts
    Inherits Koan
    'We shall contemplate truth by testing reality, via asserts.
    <Koan(1)> _
    Public Sub AssertTruth()
        Assert.True(False) 'This should be true
    End Sub

    'Enlightenment may be more easily achieved with appropriate messages
    <Koan(2)> _
    Public Sub AssertTruthWithMessage()
        Assert.True(False, "This should be true -- Please fix this")
    End Sub

    'To understand reality, we must compare our expectations against reality
    <Koan(3)> _
    Public Sub AssertEquality()
        Dim expectedValue = 3
        Dim actualValue = 1 + 1
        Assert.True(expectedValue = actualValue)
    End Sub

    'Some wasy of asserting equality are better than others
    <Koan(4)> _
    Public Sub ABetterWayOfAssertingEquality()
        Dim expectedValue = 3
        Dim actualValue = 1 + 1
        Assert.Equal(expectedValue, actualValue)
    End Sub

    'Sometimes we will ask you to fill in the values
    <Koan(5)> _
    Public Sub FillInValues()
        Assert.Equal(FILL_ME_IN, 1 + 1)
    End Sub
End Class